using System;
using System.Data;
using System.Text;
using Common.Logic;
using BAF.O2JAM2DataInfo;
using Common.DataInfo;
using lg = Common.API.LanguageAPI;
namespace BAF.O2JAM2API
{
    public class AccountInfoAPI
    {
        Message msg = null;
        public AccountInfoAPI(byte[] packet)
        {
            msg = new Message(packet, (uint)packet.Length);
        }
        /// <summary>
        /// 查看该玩家的帐号信息
        /// </summary>
        /// <returns></returns>
        public Message O2JAM2_Account_Query()
        {
            System.Data.DataSet result = null;
            string serverIP = null;
            string account = "";
            string userNick = "";
            int action = 0;
            try
            {
                serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ServerIP).m_bValueBuffer);
                TLV_Structure tlv = new TLV_Structure(TagName.O2JAM2_UserID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserID).m_bValueBuffer);
                action = (int)tlv.toInteger();
                if (action == 1)
                {
                    account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserID).m_bValueBuffer);
                }
                else if (action == 2)
                {
                    userNick = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserName).m_bValueBuffer);
                }

                SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)  + lg.O2JAM2API_AccountInfoAPI_Account + account + lg.O2JAM2API_AccountInfoAPI_AccountInfo);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)  + lg.O2JAM2API_AccountInfoAPI_Account + account + lg.O2JAM2API_AccountInfoAPI_AccountInfo);
                result = AccountInfo.O2JAM2_Account_Query(serverIP, account,userNick,action);
                if (result !=null && result.Tables[0].Rows.Count>0)
                {
                    Query_Structure[] structList = new Query_Structure[result.Tables[0].Rows.Count];
                    for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                    {
                        Query_Structure strut = new Query_Structure(9);
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(result.Tables[0].Rows[0].ItemArray[0]));
                        strut.AddTagKey(TagName.CR_PSTID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes); ;
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(result.Tables[0].Rows[0].ItemArray[1]));
                        strut.AddTagKey(TagName.CR_Passord, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);

                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(result.Tables[0].Rows[0].ItemArray[2]));
                        strut.AddTagKey(TagName.CR_UserID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(result.Tables[0].Rows[0].ItemArray[3]));
                        strut.AddTagKey(TagName.CR_ACCOUNT, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);

                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(result.Tables[0].Rows[0].ItemArray[4]));
                        strut.AddTagKey(TagName.CR_NickName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        //bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(result.Tables[0].Rows[0].ItemArray[5]));
                        //strut.AddTagKey(TagName.CR_SEX, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(result.Tables[0].Rows[0].ItemArray[6]));
                        strut.AddTagKey(TagName.O2JAM2_Id2, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                       // bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(result.Tables[0].Rows[0].ItemArray[7]));
                        //strut.AddTagKey(TagName.O2JAM2_ServerIP, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(result.Tables[0].Rows[0].ItemArray[8]));
                        strut.AddTagKey(TagName.O2JAM2_Rdate, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);

                        structList[i] = strut;
                    }
                        return Message.COMMON_MES_RESP(structList, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_ACCOUNT_QUERY_RESP,9);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.O2JAM2API_AccountInfoAPI_NoAccount, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_ACCOUNT_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

                }
            }
            catch (System.Exception)
            {
                return Message.COMMON_MES_RESP(0, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_ACCOUNT_QUERY_RESP,TagName.O2JAM2_Status,TagFormat.TLV_INTEGER);
            }
        }
        /// <summary>
        /// 查看该玩家是否被激活
        /// </summary>
        /// <returns></returns>
        public Message O2JAM2_AccountActive_Query()
        {
            System.Data.DataSet result = null;
            int status = -1;
            string serverIP = null;
            string account = null;
            string passwd = null;
            string number = null;
            try
            {
                //serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_ServerIP).m_bValueBuffer);
              //  account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_ACCOUNT).m_bValueBuffer);
                passwd = account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_Id2).m_bValueBuffer);
                number = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_Id1).m_bValueBuffer);
                SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)  + lg.O2JAM2API_AccountInfoAPI_Account + account + lg.O2JAM2API_AccountInfoAPI_ActiveState);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)  + lg.O2JAM2API_AccountInfoAPI_Account + account + lg.O2JAM2API_AccountInfoAPI_ActiveState);
                result = AccountInfo.O2JAM2_AccountActive_Query(account,passwd,number);
                if (result !=null && result.Tables[0].Rows.Count>0)
                {
                    //密码错误
                    if (!result.Tables[0].Rows[0].ItemArray[3].Equals(passwd))
                    {
                        status = 2;
                        byte[] bgMsg_Status = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, status);
                        TLV_Structure Msg_Status = new TLV_Structure(TagName.O2JAM2_Status, (uint)bgMsg_Status.Length, bgMsg_Status);
                        byte[] baMsg_Pass = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, result.Tables[0].Rows[0].ItemArray[3]);
                        TLV_Structure Msg_Pass = new TLV_Structure(TagName.O2JAM2_Id2, (uint)baMsg_Pass.Length, baMsg_Pass);
                        Packet_Body body = new Packet_Body(new TLV_Structure[] { Msg_Status, Msg_Pass }, 2);
                        Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(), Msg_Category.O2JAM2_ADMIN,
                            ServiceKey.O2JAM2_ACCOUNTACTIVE_QUERY_RESP, body.m_uiBodyLen);
                        return new Message(new Packet(head, body));

                    }
                    //激活码未被使用过
                    else if (Convert.ToInt32(result.Tables[0].Rows[0].ItemArray[5].ToString())==0)
                    {
                        status = 3;
                        return Message.COMMON_MES_RESP(status, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_ACCOUNTACTIVE_QUERY_RESP, TagName.CR_STATUS, TagFormat.TLV_INTEGER);

                    }
                    // 激活码已被使用
                    else if (Convert.ToInt32(result.Tables[0].Rows[0].ItemArray[5].ToString())==1)
                    {
                        status = 4;
                        byte[] bgMsg_Status = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, status);
                        TLV_Structure Msg_Status = new TLV_Structure(TagName.O2JAM2_Status, (uint)bgMsg_Status.Length, bgMsg_Status);
                        byte[] baMsg_Account = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,UserValidate.validData(result.Tables[0].Rows[0].ItemArray[1]));
                        TLV_Structure Msg_Account = new TLV_Structure(TagName.O2JAM2_UserName, (uint)baMsg_Account.Length, baMsg_Account);
                        Packet_Body body = new Packet_Body(new TLV_Structure[] { Msg_Status, Msg_Account }, 2);
                        Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(), Msg_Category.O2JAM2_ADMIN,
                            ServiceKey.O2JAM2_ACCOUNTACTIVE_QUERY_RESP, body.m_uiBodyLen);
                        return new Message(new Packet(head, body));
                    }
                    else
                    {
                        return Message.COMMON_MES_RESP(1, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_ACCOUNTACTIVE_QUERY_RESP, TagName.O2JAM2_Status, TagFormat.TLV_INTEGER);

                    }
                    /*// 查询帐号未被激活
                    else if (!result.Tables[0].Rows[0].ItemArray[3].Equals(account))
                    {
                        status = 5;
                    }
                    // 查询帐号已被激活
                    else if (result.Tables[0].Rows[0].ItemArray[3].Equals(account))
                    {
                        status = 6;
                    }*/
                }
                return Message.COMMON_MES_RESP(1, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_ACCOUNTACTIVE_QUERY_RESP, TagName.O2JAM2_Status, TagFormat.TLV_INTEGER);

            }
            catch (System.Exception)
            {
                return Message.COMMON_MES_RESP(1, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_ACCOUNTACTIVE_QUERY_RESP, TagName.O2JAM2_Status, TagFormat.TLV_INTEGER);
            }
        }
		/// <summary>
		/// 查看服务器所有停封帐号
		/// </summary>
		/// <returns></returns>
		public Message O2JAM2Member_banishment_QueryAll(int index,int pageSize)
		{
			string serverIP = null;
			DataSet ds = null;
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ServerIP).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.O2JAM2API_AccountInfoAPI_AllBanAccount);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.O2JAM2API_AccountInfoAPI_AllBanAccount);
				ds = AccountInfo.O2JAM2_Banishment_QueryAll(serverIP);
				if (null != ds && ds.Tables[0].Rows.Count > 0)
				{
					//总页数
					int pageCount = 0;
					pageCount = ds.Tables[0].Rows.Count % pageSize;
					if (pageCount > 0)
					{
						pageCount = ds.Tables[0].Rows.Count / pageSize + 1;
					}
					else
						pageCount = ds.Tables[0].Rows.Count / pageSize;

					if (index + pageSize > ds.Tables[0].Rows.Count)
					{
						pageSize = ds.Tables[0].Rows.Count - index;
					}
					Query_Structure[] structList = new Query_Structure[pageSize];
					for (int i = index; i < index + pageSize; i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length+1);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.O2JAM2_UserIndexID,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.O2JAM2_UserID,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.O2JAM2_StopTime,TagFormat.TLV_TIMESTAMP,(uint)bytes.Length,bytes);
						//总页数
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
						structList[i - index] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_MEMBERBANISHMENT_QUERY_RESP, 4);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.O2JAM2API_AccountInfoAPI_NoBanAccount, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_MEMBERBANISHMENT_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

				}
			}
			catch (System.Exception e)
			{
				return Message.COMMON_MES_RESP(e.Message, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_MEMBERBANISHMENT_QUERY_RESP);
			}
		}
		/// <summary>
		/// 查看玩家服务器封停状态
		/// </summary>
		/// <returns></returns>
		public Message O2JAM2Member_banishment_Query()
		{
			string serverIP = null;
			string account = null;
			int stopStatus = 0;
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ServerIP).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserID).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+lg.O2JAM2API_AccountInfoAPI_BanState);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+lg.O2JAM2API_AccountInfoAPI_BanState);
				stopStatus = AccountInfo.O2JAM2_Banishment_Query(serverIP,account);
				return Message.COMMON_MES_RESP(stopStatus,Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_MEMBERSTOPSTATUS_QUERY_RESP,TagName.O2JAM2_StopStatus,TagFormat.TLV_INTEGER);
			}
			catch(System.Exception e)
			{
				return Message.COMMON_MES_RESP(e.Message,Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_MEMBERSTOPSTATUS_QUERY_RESP);
			}
		}
		/// <summary>
		/// 查看本地被封停的帐号
		/// </summary>
		/// <returns></returns>
		public Message O2JAM2BanishmentLocal_Query()
		{
			DataSet ds = null;
			string serverIP = null;
			string account = null;
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ServerIP).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserID).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>"+lg.O2JAM2API_AccountInfoAPI_Account + account + lg.O2JAM2API_AccountInfoAPI_BanInfo);
				Console.WriteLine(DateTime.Now + " - "+lg.API_Display + lg.O2JAM2API_BAF + "+>"+lg.O2JAM2API_AccountInfoAPI_Account + account + lg.O2JAM2API_AccountInfoAPI_BanInfo);
				ds = AccountInfo.O2JAM2_BanishmentLocal_Query(serverIP, account);
				if (ds!=null && ds.Tables[0].Rows.Count > 0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length); 
						object reason;
						if (ds.Tables[0].Rows[i].IsNull(2) == false)
							reason = ds.Tables[0].Rows[i].ItemArray[2];
						else
							reason = "";
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, reason);
						strut.AddTagKey(TagName.O2JAM2_REASON, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_MEMBERLOCAL_BANISHMENT_RESP,1);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.O2JAM2API_AccountInfoAPI_NoAccountBanInfo, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_MEMBERLOCAL_BANISHMENT_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.O2JAM2API_AccountInfoAPI_NoAccountBanInfo, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_MEMBERLOCAL_BANISHMENT_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

			}

		}
		/// <summary>
		/// 服务器玩家帐号解封
		/// </summary>
		/// <returns></returns>
		public Message O2JAM2MemberOpen_Update()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			string account = null;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ServerIP).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserID).m_bValueBuffer);
				result = AccountInfo.O2JAM2_Banishment_Open(operateUserID,serverIP,account);
				if(result == 1)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+lg.O2JAM2API_AccountInfoAPI_AccountUnlock + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+lg.O2JAM2API_AccountInfoAPI_AccountUnlock + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_ACCOUNT_OPEN_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+lg.O2JAM2API_AccountInfoAPI_AccountUnlock + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+lg.O2JAM2API_AccountInfoAPI_AccountUnlock + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_ACCOUNT_OPEN_RESP);
				}
			}
			catch(System.Exception e)
			{
				return Message.COMMON_MES_RESP(e.Message,Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_ACCOUNT_OPEN_RESP);
			}
		}
		/// <summary>
		/// 停封服务器玩家帐号
		/// </summary>
		/// <returns></returns>
		public Message O2JAM2MemberClose_Update()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			string account = null;
			string reason = null;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ServerIP).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserID).m_bValueBuffer);
				reason = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_REASON).m_bValueBuffer);

				result = AccountInfo.O2JAM2_Banishment_Close(operateUserID, serverIP, account, reason);
				if (result == -1)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) +  account + lg.O2JAM2API_AccountInfoAPI_NoAccount);
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) +  account + lg.O2JAM2API_AccountInfoAPI_NoAccount);
					return Message.COMMON_MES_RESP(lg.O2JAM2API_AccountInfoAPI_NoAccount, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_ACCOUNT_CLOSE_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
				else if(result == 1)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+lg.O2JAM2API_AccountInfoAPI_BanAccount + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+lg.O2JAM2API_AccountInfoAPI_BanAccount + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_ACCOUNT_CLOSE_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+lg.O2JAM2API_AccountInfoAPI_BanAccount + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+lg.O2JAM2API_AccountInfoAPI_BanAccount + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_ACCOUNT_CLOSE_RESP);
				}
			}
			catch(System.Exception e)
			{
				return Message.COMMON_MES_RESP(e.Message,Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_ACCOUNT_CLOSE_RESP);
			}
		}
		/// <summary>
		/// 剔除玩家登陆帐号
		/// </summary>
		/// <returns></returns>
		public Message O2JAM2UserLogin_Delete()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			string account = null;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ServerIP).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserID).m_bValueBuffer);
				result = AccountInfo.O2JAM2_UserLogin_Delete(operateUserID,serverIP,account);
				if(result == 1)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+lg.O2JAM2API_AccountInfoAPI_AccountKick + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+lg.O2JAM2API_AccountInfoAPI_AccountKick + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_USERLOGIN_DELETE_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+lg.O2JAM2API_AccountInfoAPI_AccountKick + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+lg.O2JAM2API_AccountInfoAPI_AccountKick + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_USERLOGIN_DELETE_RESP);
				}
			}
			catch(System.Exception e)
			{
				return Message.COMMON_MES_RESP(lg.O2JAM2API_AccountInfoAPI_NoAccount,Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_USERLOGIN_DELETE_RESP);
			}
		}


    }
}
