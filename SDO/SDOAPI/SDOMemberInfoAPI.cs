using System;
using System.Data;
using SDO.SDODataInfo;
using Common.Logic;
using Common.DataInfo;
using lg = Common.API.LanguageAPI;
namespace SDO.SDOAPI
{
	/// <summary>
	/// SDOMemberInfoAPI 的摘要说明。
	/// </summary>
	public class SDOMemberInfoAPI
	{
		Message msg;
		public SDOMemberInfoAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public SDOMemberInfoAPI(byte[] packet)
		{
			msg = new Message(packet,(uint)packet.Length);
		}
        /// <summary>
        /// 查看玩家服务器激活状态
        /// </summary>
        /// <returns></returns>
		public Message SDOMemberInfo_Query()
		{
			string serverIP = null;
			string account = null;
			DataSet ds = null;
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.SDOAPI_SDOItemLogInfoAPI_Account + account + lg.SDOAPI_SDOMemberInfoAPI_ActiveState + "!");
				ds = MemberInfo.memberInfo_Query(serverIP,account);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length + 1);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.SDO_Account, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						//9you帐号
						int youID;
						if(ds.Tables[0].Rows[i].IsNull(1)==false)
							youID = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[1]);
						else
							youID = 0;
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, youID);
						strut.AddTagKey(TagName.SDO_9YouAccount, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.SDO_NickName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_BOOLEAN, ds.Tables[0].Rows[i].ItemArray[3]);
						strut.AddTagKey(TagName.SDO_SEX, TagFormat.TLV_BOOLEAN, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, ds.Tables[0].Rows[i].ItemArray[4]);
						strut.AddTagKey(TagName.SDO_RegistDate, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						//firstLoginTime
						object firstLoginTime;
						if(ds.Tables[0].Rows[i].IsNull(5)==false)
							firstLoginTime = ds.Tables[0].Rows[i].ItemArray[5];
						else
							firstLoginTime = DateTime.Now;
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, firstLoginTime);
						strut.AddTagKey(TagName.SDO_FirstLogintime, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						object lastLoginTime;
						if(ds.Tables[0].Rows[i].IsNull(6)==false)
							lastLoginTime = ds.Tables[0].Rows[i].ItemArray[6];
						else
							lastLoginTime = DateTime.Now;
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, lastLoginTime);
						strut.AddTagKey(TagName.SDO_LastLogintime, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
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
                    return Message.COMMON_MES_RESP(lg.SDOAPI_SDOMemberInfoAPI_NoActived, Msg_Category.SDO_ADMIN, ServiceKey.SDO_ACCOUNT_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }
			}
			catch(System.Exception e)
			{
                Console.WriteLine(e.Message);
				return Message.COMMON_MES_RESP(lg.SDOAPI_SDOMemberInfoAPI_NoActived,Msg_Category.SDO_ADMIN,ServiceKey.SDO_ACCOUNT_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
		}
        /// <summary>
        /// 查看服务器所有停封帐号
        /// </summary>
        /// <returns></returns>
        public Message SDOMember_banishment_QueryAll(int index,int pageSize)
        {
            string serverIP = null;
            DataSet ds = null;
            try
            {
                serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDOMemberInfoAPI_AllBanAccount);
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
                    return Message.COMMON_MES_RESP(lg.SDOAPI_SDOMemberInfoAPI_NoBanAccount, Msg_Category.SDO_ADMIN, ServiceKey.SDO_MEMBERBANISHMENT_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

                }
            }
            catch (System.Exception e)
            {
                return Message.COMMON_MES_RESP(lg.SDOAPI_SDOMemberInfoAPI_NoBanAccount, Msg_Category.SDO_ADMIN, ServiceKey.SDO_MEMBERBANISHMENT_QUERY_RESP);
            }
        }
		/// <summary>
		/// 查看玩家服务器封停状态
		/// </summary>
		/// <returns></returns>
        public Message SDOMember_banishment_Query()
		{
			string serverIP = null;
			string account = null;
			int stopStatus = 0;
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.SDOAPI_SDOItemLogInfoAPI_Account+account+lg.SDOAPI_SDOMemberInfoAPI_BanInfo + "!");
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
        public Message SDOBanishmentLocal_Query()
        {
            DataSet ds = null;
            string serverIP = null;
            string account = null;
            try
            {
                serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
                account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + account + lg.SDOAPI_SDOMemberInfoAPI_BanInfo);
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
                    return Message.COMMON_MES_RESP(lg.SDOAPI_SDOMemberInfoAPI_NoBanInfo, Msg_Category.SDO_ADMIN, ServiceKey.SDO_MEMBERLOCAL_BANISHMENT_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP(lg.SDOAPI_SDOMemberInfoAPI_NoBanInfo, Msg_Category.SDO_ADMIN, ServiceKey.SDO_MEMBERLOCAL_BANISHMENT_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

            }

        }
		/// <summary>
		/// 不激活玩家的跳舞毯状态
		/// </summary>
		/// <returns></returns>
		public Message SDOMemberDanceClose_Update()
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
				result = MemberInfo.memberDanceInfo_Close(operateUserID, serverIP, account);
				if(result == 1)
				{
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.SDOAPI_SDOItemLogInfoAPI_Account+account+lg.SDOAPI_SDOMemberInfoAPI_PadBan + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_MEMBERDANCE_CLOSE_RESP);
				}
				else
				{
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.SDOAPI_SDOItemLogInfoAPI_Account+account+lg.SDOAPI_SDOMemberInfoAPI_PadBan + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_MEMBERDANCE_CLOSE_RESP);
				}
			}
			catch(System.Exception e)
			{
				return Message.COMMON_MES_RESP(e.Message,Msg_Category.SDO_ADMIN,ServiceKey.SDO_MEMBERDANCE_CLOSE_RESP);
			}
		}
		/// <summary>
		/// 激活玩家的跳舞毯状态
		/// </summary>
		/// <returns></returns>
		public Message SDOMemberDanceOpen_Update()
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
				result = MemberInfo.memberDanceInfo_Active(operateUserID, serverIP, account);
				if(result == 1)
				{
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.SDOAPI_SDOItemLogInfoAPI_Account+account+lg.SDOAPI_SDOMemberInfoAPI_PadActive + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_MEMBERDANCE_OPEN_RESP);
				}
				else
				{
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.SDOAPI_SDOItemLogInfoAPI_Account+account+lg.SDOAPI_SDOMemberInfoAPI_PadActive + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_MEMBERDANCE_OPEN_RESP);
				}
			}
			catch(System.Exception e)
			{
				return Message.COMMON_MES_RESP(e.Message,Msg_Category.SDO_ADMIN,ServiceKey.SDO_MEMBERDANCE_OPEN_RESP);
			}
		}
		/// <summary>
        /// 服务器玩家帐号解封
		/// </summary>
		/// <returns></returns>
		public Message SDOMemberOpen_Update()
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
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.SDOAPI_SDOItemLogInfoAPI_Account+account+lg.SDOAPI_SDOMemberInfoAPI_AccountUnlock + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_ACCOUNT_OPEN_RESP);
				}
				else
				{
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.SDOAPI_SDOItemLogInfoAPI_Account+account+lg.SDOAPI_SDOMemberInfoAPI_AccountUnlock + lg.API_Failure + "!");
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
		public Message SDOMemberClose_Update()
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
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + " " + account + lg.SDOAPI_SDOCharacterInfoAPI_NoAccount + "!");
                    return Message.COMMON_MES_RESP(lg.SDOAPI_SDOCharacterInfoAPI_NoAccount + "!", Msg_Category.SDO_ADMIN, ServiceKey.SDO_ACCOUNT_CLOSE_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
                }
				else if(result == 1)
				{
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.SDOAPI_SDOItemLogInfoAPI_Account+account+lg.SDOAPI_SDOMemberInfoAPI_AccountLock + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_ACCOUNT_CLOSE_RESP);
				}
				else
				{
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.SDOAPI_SDOItemLogInfoAPI_Account+account+lg.SDOAPI_SDOMemberInfoAPI_AccountLock + lg.API_Failure + "!");
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
		public Message SDO_login_Query()
		{
			string serverIP = null;
			string account = null;
			DataSet ds = null;
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.SDOAPI_SDOItemLogInfoAPI_Account+account+lg.SDOAPI_SDOMemberInfoAPI_CurrentState + "!");
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
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[3]);
						strut1.AddTagKey(TagName.SDO_ipaddr,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						structList[i]=strut1;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_USERLOGIN_STATUS_QUERY_RESP,4);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDOAPI_SDOMemberInfoAPI_NoCurrentInfo,Msg_Category.SDO_ADMIN,ServiceKey.SDO_USERLOGIN_STATUS_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception e)
			{
				Console.WriteLine(e.Message);
				return Message.COMMON_MES_RESP(lg.SDOAPI_SDOMemberInfoAPI_NoCurrentInfo,Msg_Category.SDO_ADMIN,ServiceKey.SDO_USERLOGIN_STATUS_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public Message SDOUserNick_Update()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			string account = null;
			string nickName = null;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
				nickName =System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_NickName).m_bValueBuffer); 
				result = MemberInfo.UserNick_Update(operateUserID, serverIP, account,nickName);
				if(result == 1)
				{
					SqlHelper.log.WriteLog("超级舞者+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+account+"更新呢称成功!");
					Console.WriteLine(DateTime.Now+" - 超级舞者+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+account+"更新呢称成功!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_USERNICK_UPDATE_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog("超级舞者+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+account+"更新呢称失败!");
					Console.WriteLine(DateTime.Now+" - 超级舞者+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+account+"更新呢称失败!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_USERNICK_UPDATE_RESP);
				}
			}
			catch(System.Exception e)
			{
				return Message.COMMON_MES_RESP(e.Message,Msg_Category.SDO_ADMIN,ServiceKey.SDO_USERNICK_UPDATE_RESP);
			}
		}
		/// <summary>
		/// 即时查看跳舞毯关键字
		/// </summary>
		/// <returns></returns>
		public Message SDO_PADKeyWord_Query()
		{
			string keyID = null;
			string keyWord = null;
			DataSet ds = null;
			try
			{
				keyID = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_KeyID).m_bValueBuffer);
				keyWord = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_KeyWord).m_bValueBuffer);
				SqlHelper.log.WriteLog("浏览超级舞者+>跳舞毯信息!");
				Console.WriteLine(DateTime.Now+" - 浏览超级舞者+>跳舞毯信息!");
				ds = MemberInfo.SDO_PADKeyWord_Query(keyID,keyWord);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[1];
					Query_Structure strut1 = new Query_Structure(4);
					byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[0].ItemArray[0]);
					strut1.AddTagKey(TagName.SDO_KeyID,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[0].ItemArray[1]);
					strut1.AddTagKey(TagName.SDO_KeyWord,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[0].ItemArray[2]);
					strut1.AddTagKey(TagName.SDO_Master,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[0].ItemArray[3]);
					strut1.AddTagKey(TagName.SDO_MasterID,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
					structList[0]=strut1;
					return Message.COMMON_MES_RESP(structList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_PADKEYWORD_QUERY_RESP,4);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有该序列号",Msg_Category.SDO_ADMIN,ServiceKey.SDO_PADKEYWORD_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception e)
			{
				Console.WriteLine(e.Message);
				return Message.COMMON_MES_RESP("没有该序列号",Msg_Category.SDO_ADMIN,ServiceKey.SDO_PADKEYWORD_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}

		}
	}
}
