using System;
using System.Data;
using System.Text;
using Common.Logic;
using Common.DataInfo;
using Audition.AUDataInfo;
using lg = Common.API.LanguageAPI;
//[劲舞团GM工具操作API]
namespace Audition.AUAPI
{
    public class AUMemberInfoAPI
    {
        Message msg = null;
        public AUMemberInfoAPI(byte[] packet)
        {
            msg = new Message(packet, (uint)packet.Length);

        }
        /// <summary>
        /// 查看服务器所有停封帐号
        /// </summary>
        /// <returns></returns>
        public Message Audition_banishment_QueryAll(int index, int pageSize)
        {
            string serverIP = null;
            System.Data.DataSet ds = null;
            try
            {
                serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
                SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.AU_AUMemberInfoAPI_AllBanAccount);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.AU_AUMemberInfoAPI_AllBanAccount);
                ds = AUMemberInfo.Audition_Banishment_QueryAll(serverIP);
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
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length + 1);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.AU_ACCOUNT, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.AU_UserNick, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						//总页数
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
          
          
						structList[i - index] = strut;
					}
                    return Message.COMMON_MES_RESP(structList, Msg_Category.AU_ADMIN, ServiceKey.AU_ACCOUNTREMOTE_QUERY_RESP, 3);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.AU_AUMemberInfoAPI_NoBanAccount, Msg_Category.AU_ADMIN, ServiceKey.AU_ACCOUNTREMOTE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

                }
            }
            catch (System.Exception e)
            {
                return Message.COMMON_MES_RESP(e.Message, Msg_Category.AU_ADMIN, ServiceKey.AU_ACCOUNTREMOTE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }
        }
        /// <summary>
        /// 查看该玩家的帐号信息
        /// </summary>
        /// <returns></returns>
        public Message Audition_Account_Query()
        {
            string serverIP = null;
            string account = null;
            try
            {
                serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
                account = AUMemberInfo.Audition_Identity9you_Query(serverIP,System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ACCOUNT).m_bValueBuffer));
                SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.AU_AUAvatarListAPI_Account+account + lg.AU_AUCharacterInfoAPI_AccountInfo);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.AU_AUAvatarListAPI_Account+account + lg.AU_AUCharacterInfoAPI_AccountInfo);
                 System.Data.DataSet ds = AUMemberInfo.Audition_Account_Query(serverIP, account);
                 Query_Structure[] structList = new Query_Structure[1];
                 if (ds != null && ds.Tables[0].Rows.Count > 0)
                 {
                     Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[0].ItemArray.Length);
                     byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]));
                     strut.AddTagKey(TagName.AU_UserSN, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                     bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[0].ItemArray[1]);
                     strut.AddTagKey(TagName.AU_UserID, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                     bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[0].ItemArray[2].ToString().Trim());
                     strut.AddTagKey(TagName.AU_UserNick, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                     bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToUInt32(ds.Tables[0].Rows[0].ItemArray[3]));
                     strut.AddTagKey(TagName.AU_SexIndex, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                     structList[0] = strut;
                     return Message.COMMON_MES_RESP(structList, Msg_Category.AU_ADMIN, ServiceKey.AU_ACCOUNT_QUERY_RESP,4);
                 }
                 else
                 {
                     return Message.COMMON_MES_RESP(lg.AU_AUCharacterInfoAPI_NoAccount, Msg_Category.AU_ADMIN, ServiceKey.AU_ACCOUNT_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

                 }
            }
            catch (System.Exception e)
            {
                return Message.COMMON_MES_RESP(lg.AU_AUCharacterInfoAPI_NoAccount, Msg_Category.AU_ADMIN, ServiceKey.AU_ACCOUNT_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }
        }
        /// <summary>
        /// 查看玩家服务器封停状态
        /// </summary>
        /// <returns></returns>
        public Message Audition_banishment_Query()
        {
            string serverIP = null;
            string userNick = null;
            int stopStatus = 0;
            try
            {
                serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
                userNick = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_UserNick).m_bValueBuffer);
                SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.AU_AUAvatarListAPI_Account+userNick + lg.AU_AUMemberInfoAPI_BanState);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.AU_AUAvatarListAPI_Account+userNick + lg.AU_AUMemberInfoAPI_BanState);
                stopStatus = AUMemberInfo.Audition_BanishmentAccount_Query(serverIP, userNick);
                return Message.COMMON_MES_RESP(stopStatus, Msg_Category.AU_ADMIN, ServiceKey.AU_ACCOUNT_BANISHMENT_QUERY_RESP, TagName.AU_STOPSTATUS, TagFormat.TLV_INTEGER);
            }
            catch (System.Exception e)
            {
                return Message.COMMON_MES_RESP(e.Message, Msg_Category.AU_ADMIN, ServiceKey.AU_ACCOUNT_BANISHMENT_QUERY_RESP);
            }
        }
        /// <summary>
        /// 查看本地被封停的帐号
        /// </summary>
        /// <returns></returns>
        public Message Audition_BanishmentLocal_Query()
        {
            System.Data.DataSet ds = null;
            string serverIP = null;
            string account = null;
            try
            {
                serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
                account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ACCOUNT).m_bValueBuffer);
                //account = AUMemberInfo.Audition_Identity9you_Query(serverIP,account);
				SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.AU_AUAvatarListAPI_Account + account + lg.AU_AUMemberInfoAPI_BanInfo);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.AU_AUAvatarListAPI_Account + account + lg.AU_AUMemberInfoAPI_BanInfo);
                ds = AUMemberInfo.Audition_BanishmentLocal_Query(serverIP, account);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0]);
                        strut.AddTagKey(TagName.AU_ACCOUNT, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[1]);
                        strut.AddTagKey(TagName.AU_UserNick, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
                        strut.AddTagKey(TagName.AU_ServerIP, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        object reason;
                        if (ds.Tables[0].Rows[i].IsNull(3) == false)
                            reason = ds.Tables[0].Rows[i].ItemArray[3];
                        else
                            reason = "";
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, reason);
                        strut.AddTagKey(TagName.AU_Reason, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, ds.Tables[0].Rows[i].ItemArray[4]);
                        strut.AddTagKey(TagName.AU_BanDate, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        structList[i] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.AU_ADMIN, ServiceKey.AU_ACCOUNTLOCAL_QUERY_RESP,5);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.AU_AUMemberInfoAPI_NoAccountBanInfo, Msg_Category.AU_ADMIN, ServiceKey.AU_ACCOUNTLOCAL_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP(lg.AU_AUMemberInfoAPI_NoAccountBanInfo, Msg_Category.AU_ADMIN, ServiceKey.AU_ACCOUNTLOCAL_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }
        }
        /// <summary>
        /// 服务器玩家帐号解封
        /// </summary>
        /// <returns></returns>
        public Message Audition_AccountOpen_Update()
        {
            int result = -1;
            int operateUserID = 0;
            string serverIP = null;
            string account = null;
            string nickName = null;
            try
            {
                TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
                operateUserID = (int)strut.toInteger();
                serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
                account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ACCOUNT).m_bValueBuffer);
                nickName = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_UserNick).m_bValueBuffer);
                result = AUMemberInfo.Audition_Banishment_Open(operateUserID, serverIP, account,nickName);
                if (result == -1)
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.AU_AUAvatarListAPI_Account+account + lg.AU_AUMemberInfoAPI_AlreadyUnlock);
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.AU_AUAvatarListAPI_Account+account + lg.AU_AUCharacterInfoAPI_NoAccount);
                    return Message.COMMON_MES_RESP(lg.AU_AUMemberInfoAPI_AlreadyUnlock, Msg_Category.AU_ADMIN, ServiceKey.AU_ACCOUNT_OPEN_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }

                else if (result == 1)
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.AU_AUAvatarListAPI_Account+account + lg.AU_AUMemberInfoAPI_AccountUnlock + lg.API_Success + "!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.AU_AUAvatarListAPI_Account+account + lg.AU_AUMemberInfoAPI_AccountUnlock + lg.API_Success + "!");
                    return Message.COMMON_MES_RESP("SUCESS", Msg_Category.AU_ADMIN, ServiceKey.AU_ACCOUNT_OPEN_RESP);
                }
                else
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.AU_AUAvatarListAPI_Account+account + lg.AU_AUMemberInfoAPI_AccountUnlock + lg.API_Failure + "!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.AU_AUAvatarListAPI_Account+account + lg.AU_AUMemberInfoAPI_AccountUnlock + lg.API_Failure + "!");
                    return Message.COMMON_MES_RESP("FAILURE", Msg_Category.AU_ADMIN, ServiceKey.AU_ACCOUNT_OPEN_RESP);
                }
            }
            catch (System.Exception e)
            {
                return Message.COMMON_MES_RESP(e.Message, Msg_Category.AU_ADMIN, ServiceKey.AU_ACCOUNT_OPEN_RESP);
            }
        }
        /// <summary>
        /// 停封服务器玩家帐号
        /// </summary>
        /// <returns></returns>
        public Message Audition_AccountClose_Update()
        {
            int result = -1;
            int operateUserID = 0;
            string serverIP = null;
            string account = null;
            string nickName = null;
            string reason = null;
            try
            {
                TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
                operateUserID = (int)strut.toInteger();
                serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
                account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ACCOUNT).m_bValueBuffer);
                nickName = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_UserNick).m_bValueBuffer);
                reason = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_Reason).m_bValueBuffer);
                result = AUMemberInfo.Audition_Banishment_Close(operateUserID, serverIP, account,nickName,reason);
                if (result == -1)
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.AU_AUAvatarListAPI_Account+account + lg.AU_AUMemberInfoAPI_AlreadyBan);
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.AU_AUAvatarListAPI_Account+account + lg.AU_AUCharacterInfoAPI_NoAccount);
                    return Message.COMMON_MES_RESP(lg.AU_AUMemberInfoAPI_AlreadyBan, Msg_Category.AU_ADMIN, ServiceKey.AU_ACCOUNT_CLOSE_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }
                else if (result == 1)
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.AU_AUAvatarListAPI_Account+account + lg.AU_AUMemberInfoAPI_BanAccount + lg.API_Success + "!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.AU_AUAvatarListAPI_Account+account + lg.AU_AUMemberInfoAPI_BanAccount + lg.API_Success + "!");
                    return Message.COMMON_MES_RESP("SUCESS", Msg_Category.AU_ADMIN, ServiceKey.AU_ACCOUNT_CLOSE_RESP);
                }
                else
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.AU_AUAvatarListAPI_Account+account + lg.AU_AUMemberInfoAPI_BanAccount + lg.API_Failure + "!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.AU_AUAvatarListAPI_Account+account + lg.AU_AUMemberInfoAPI_BanAccount + lg.API_Failure + "!");
                    return Message.COMMON_MES_RESP("FAILURE", Msg_Category.AU_ADMIN, ServiceKey.AU_ACCOUNT_CLOSE_RESP);
                }
            }
            catch (System.Exception e)
            {
                return Message.COMMON_MES_RESP(e.Message, Msg_Category.AU_ADMIN, ServiceKey.AU_ACCOUNT_CLOSE_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }
        }
		/// <summary>
		/// 更新劲舞团玩家呢称
		/// </summary>
		/// <returns></returns>
		public Message Audition_NickName_Update()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			string account = null;
			string nickName = null;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ACCOUNT).m_bValueBuffer);
				nickName = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_UserNick).m_bValueBuffer);
				result = AUMemberInfo.Audition_UserNick_Update(operateUserID, serverIP, account,nickName);
				if (result == 1)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.AU_AUAvatarListAPI_Account+account + lg.API_Update + lg.AU_AUMemberInfoAPI_NickName +  lg.API_Success + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.AU_AUAvatarListAPI_Account+account + lg.API_Update + lg.AU_AUMemberInfoAPI_NickName +  lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.AU_ADMIN, ServiceKey.AU_USERNICK_UPDATE_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.AU_AUAvatarListAPI_Account+account + lg.API_Update + lg.AU_AUMemberInfoAPI_NickName +  lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.AU_AUAvatarListAPI_Account+account + lg.API_Update + lg.AU_AUMemberInfoAPI_NickName +  lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.AU_ADMIN, ServiceKey.AU_USERNICK_UPDATE_RESP);
				}
			}
			catch (System.Exception e)
			{
				return Message.COMMON_MES_RESP(e.Message, Msg_Category.AU_ADMIN, ServiceKey.AU_USERNICK_UPDATE_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
    }
}
