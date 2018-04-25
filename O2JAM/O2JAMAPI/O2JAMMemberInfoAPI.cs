using System;
using System.Data;
using O2JAM.O2JAMDataInfo;
using Common.Logic;
using Common.DataInfo;
using lg = Common.API.LanguageAPI;
namespace O2JAM.O2JAMAPI
{
	/// <summary>
	/// SDOMemberInfoAPI 的摘要说明。
	/// </summary>
	public class O2JAMMemberInfoAPI
	{
		Message msg;
		public O2JAMMemberInfoAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public O2JAMMemberInfoAPI(byte[] packet)
		{
			msg = new Message(packet,(uint)packet.Length);
		}
        /// <summary>
        /// 查看玩家服务器激活状态
        /// </summary>
        /// <returns></returns>
		public Message O2JAMMemberInfo_Query()
		{
			string serverIP = null;
			string account = null;
			DataSet ds = null;
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+serverIP+lg.O2JAM_CharacterInfoAPI_Account+account+lg.O2JAM_MemberInfoAPI_ActiveState);
				Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+serverIP+lg.O2JAM_CharacterInfoAPI_Account+account+lg.O2JAM_MemberInfoAPI_ActiveState);
				ds = MemberInfo.memberInfo_Query(serverIP,account);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length + 1);
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0]);
                        strut.AddTagKey(TagName.SDO_Account, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[1]);
                        strut.AddTagKey(TagName.SDO_9YouAccount, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
                        strut.AddTagKey(TagName.SDO_NickName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_BOOLEAN, ds.Tables[0].Rows[i].ItemArray[3]);
                        strut.AddTagKey(TagName.SDO_SEX, TagFormat.TLV_BOOLEAN, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_DATE, ds.Tables[0].Rows[i].ItemArray[4]);
                        strut.AddTagKey(TagName.SDO_RegistDate, TagFormat.TLV_DATE, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_DATE, ds.Tables[0].Rows[i].ItemArray[5]);
                        strut.AddTagKey(TagName.SDO_FirstLogintime, TagFormat.TLV_DATE, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_DATE, ds.Tables[0].Rows[i].ItemArray[6]);
                        strut.AddTagKey(TagName.SDO_LastLogintime, TagFormat.TLV_DATE, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[7]);
                        strut.AddTagKey(TagName.SDO_Ispad, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, 1);
                        strut.AddTagKey(TagName.SDO_ActiveStatus, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        structList[i] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.SDO_ADMIN, ServiceKey.SDO_ACCOUNT_QUERY_RESP, 9);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.O2JAM_MemberInfoAPI_NoActived, Msg_Category.SDO_ADMIN, ServiceKey.SDO_ACCOUNT_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }
			}
			catch(System.Exception e)
			{
                Console.WriteLine(e.Message);
				return Message.COMMON_MES_RESP(lg.O2JAM_MemberInfoAPI_NoActived,Msg_Category.SDO_ADMIN,ServiceKey.SDO_ACCOUNT_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
		}
        /// <summary>
        /// 查看服务器所有停封帐号
        /// </summary>
        /// <returns></returns>
        public Message O2JAMMember_banishment_QueryAll(int index,int pageSize)
        {
            string serverIP = null;
            DataSet ds = null;
            try
            {
                serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
                SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.O2JAM_MemberInfoAPI_AllBanAccount);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.O2JAM_MemberInfoAPI_AllBanAccount);
                ds = MemberInfo.SDO_Banishment_QueryAll(serverIP);
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
						strut.AddTagKey(TagName.SDO_UserIndexID,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.SDO_Account,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.SDO_StopTime,TagFormat.TLV_TIMESTAMP,(uint)bytes.Length,bytes);
                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
                        structList[i - index] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.SDO_ADMIN, ServiceKey.SDO_MEMBERBANISHMENT_QUERY_RESP, 4);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.O2JAM_MemberInfoAPI_NoBanAccount, Msg_Category.SDO_ADMIN, ServiceKey.SDO_MEMBERBANISHMENT_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

                }
            }
            catch (System.Exception e)
            {
                return Message.COMMON_MES_RESP(e.Message, Msg_Category.SDO_ADMIN, ServiceKey.SDO_MEMBERBANISHMENT_QUERY_RESP);
            }
        }
		/// <summary>
		/// 查看玩家服务器封停状态
		/// </summary>
		/// <returns></returns>
        public Message O2JAMMember_banishment_Query()
		{
			string serverIP = null;
			string account = null;
			int stopStatus = 0;
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+serverIP+lg.O2JAM_CharacterInfoAPI_Account+account+lg.O2JAM_MemberInfoAPII_BanState);
				Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+serverIP+lg.O2JAM_CharacterInfoAPI_Account+account+lg.O2JAM_MemberInfoAPII_BanState);
				stopStatus = MemberInfo.SDO_Banishment_Query(serverIP,account);
				return Message.COMMON_MES_RESP(stopStatus,Msg_Category.SDO_ADMIN,ServiceKey.SDO_MEMBERSTOPSTATUS_QUERY_RESP,TagName.SDO_StopStatus,TagFormat.TLV_INTEGER);
			}
			catch(System.Exception e)
			{
				return Message.COMMON_MES_RESP(e.Message,Msg_Category.SDO_ADMIN,ServiceKey.SDO_MEMBERSTOPSTATUS_QUERY_RESP);
			}
		}
        /// <summary>
        /// 查看本地被封停的帐号
        /// </summary>
        /// <returns></returns>
        public Message O2JAMBanishmentLocal_Query()
        {
            DataSet ds = null;
            string serverIP = null;
            string account = null;
            try
            {
                serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
                account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
                SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" +lg.O2JAM_CharacterInfoAPI_Account + account + lg.O2JAM_MemberInfoAPI_BanInfo);
                Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.O2JAM_O2JAM + "+>"+lg.O2JAM_CharacterInfoAPI_Account + account + lg.O2JAM_MemberInfoAPI_BanInfo);
                ds = MemberInfo.SDO_BanishmentLocal_Query(serverIP, account);
                if (ds!=null && ds.Tables[0].Rows.Count > 0)
                {
                    Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
                        object context;
                        if (ds.Tables[0].Rows[i].IsNull(0) == false)
                            context = ds.Tables[0].Rows[i].ItemArray[0];
                        else
                            context = "";
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, context);
                        strut.AddTagKey(TagName.SDO_Context, TagFormat.TLV_STRING, (uint)bytes.Length, bytes); 
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, ds.Tables[0].Rows[i].ItemArray[1]);
                        strut.AddTagKey(TagName.SDO_StopTime, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        object reason;
                        if (ds.Tables[0].Rows[i].IsNull(2) == false)
                            reason = ds.Tables[0].Rows[i].ItemArray[2];
                        else
                            reason = "";
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, reason);
                        strut.AddTagKey(TagName.SDO_REASON, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        structList[i] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.SDO_ADMIN, ServiceKey.SDO_MEMBERLOCAL_BANISHMENT_RESP,3);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.O2JAM_MemberInfoAPI_NoAccountBanInfo, Msg_Category.SDO_ADMIN, ServiceKey.SDO_MEMBERLOCAL_BANISHMENT_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP(lg.O2JAM_MemberInfoAPI_NoAccountBanInfo, Msg_Category.SDO_ADMIN, ServiceKey.SDO_MEMBERLOCAL_BANISHMENT_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

            }

        }
		/// <summary>
        /// 服务器玩家帐号解封
		/// </summary>
		/// <returns></returns>
		public Message O2JAMMemberOpen_Update()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			string account = null;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
				result = MemberInfo.SDO_Banishment_Open(operateUserID,serverIP,account);
				if(result == 1)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+serverIP+lg.O2JAM_CharacterInfoAPI_Account+account+lg.O2JAM_MemberInfoAPI_AccountUnlock + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+serverIP+lg.O2JAM_CharacterInfoAPI_Account+account+lg.O2JAM_MemberInfoAPI_AccountUnlock + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_ACCOUNT_OPEN_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+serverIP+lg.O2JAM_CharacterInfoAPI_Account+account+lg.O2JAM_MemberInfoAPI_AccountUnlock + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+serverIP+lg.O2JAM_CharacterInfoAPI_Account+account+lg.O2JAM_MemberInfoAPI_AccountUnlock + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_ACCOUNT_OPEN_RESP);
				}
			}
			catch(System.Exception e)
			{
				return Message.COMMON_MES_RESP(e.Message,Msg_Category.SDO_ADMIN,ServiceKey.SDO_ACCOUNT_OPEN_RESP);
			}
		}
		/// <summary>
        /// 停封服务器玩家帐号
		/// </summary>
		/// <returns></returns>
		public Message O2JAMMemberClose_Update()
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
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
                reason = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_REASON).m_bValueBuffer);

                result = MemberInfo.SDO_Banishment_Close(operateUserID, serverIP, account, reason);
                if (result == -1)
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.O2JAM_CharacterInfoAPI_Account + account + lg.O2JAM_CharacterInfoAPI_NoAccount);
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.O2JAM_CharacterInfoAPI_Account + account + lg.O2JAM_CharacterInfoAPI_NoAccount);
                    return Message.COMMON_MES_RESP(lg.O2JAM_CharacterInfoAPI_NoAccount, Msg_Category.SDO_ADMIN, ServiceKey.SDO_ACCOUNT_CLOSE_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
                }
				else if(result == 1)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+serverIP+lg.O2JAM_CharacterInfoAPI_Account+account+lg.O2JAM_MemberInfoAPI_BanAccount + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+serverIP+lg.O2JAM_CharacterInfoAPI_Account+account+lg.O2JAM_MemberInfoAPI_BanAccount + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_ACCOUNT_CLOSE_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+serverIP+lg.O2JAM_CharacterInfoAPI_Account+account+lg.O2JAM_MemberInfoAPI_BanAccount + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+serverIP+lg.O2JAM_CharacterInfoAPI_Account+account+lg.O2JAM_MemberInfoAPI_BanAccount + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_ACCOUNT_CLOSE_RESP);
				}
			}
			catch(System.Exception e)
			{
				return Message.COMMON_MES_RESP(e.Message,Msg_Category.SDO_ADMIN,ServiceKey.SDO_ACCOUNT_CLOSE_RESP);
			}
		}
		/// <summary>
		/// 即时查看玩家当前状态(服务器/房间/在线状态)
		/// </summary>
		/// <returns></returns>
		public Message O2JAM_login_Query()
		{
			string serverIP = null;
			string account = null;
			DataSet ds = null;
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+serverIP+lg.O2JAM_CharacterInfoAPI_Account+account+lg.O2JAM_MemberInfoAPI_CurrentStatus);
				Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+serverIP+lg.O2JAM_CharacterInfoAPI_Account+account+lg.O2JAM_MemberInfoAPI_CurrentStatus);
				ds = MemberInfo.T_o2jam_login_Query(serverIP,account);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut1 = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[0]);
						strut1.AddTagKey(TagName.SDO_Account,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[1]);
						strut1.AddTagKey(TagName.SDO_MAINCH,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[2]);
						strut1.AddTagKey(TagName.SDO_SUBCH,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						structList[i]=strut1;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_USERLOGIN_STATUS_QUERY_RESP,3);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.O2JAM_MemberInfoAPI_NoCurrentStatus,Msg_Category.SDO_ADMIN,ServiceKey.SDO_USERLOGIN_STATUS_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception e)
			{
				Console.WriteLine(e.Message);
				return Message.COMMON_MES_RESP(lg.O2JAM_MemberInfoAPI_NoCurrentStatus,Msg_Category.SDO_ADMIN,ServiceKey.SDO_USERLOGIN_STATUS_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}

		}
	}
}
