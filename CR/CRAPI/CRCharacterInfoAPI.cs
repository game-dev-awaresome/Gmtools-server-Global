using System;

using System.Text;
using Common.Logic;
using Common.DataInfo;
using CR.CRDataInfo;
using lg = Common.API.LanguageAPI;
namespace CR.CRAPI
{
    public class CRCharacterInfoAPI
    {
        Message msg = null;
        public CRCharacterInfoAPI(byte[] packet)
        {
            msg = new Message(packet, (uint)packet.Length);
        }
		/// <summary>
		/// 玩家资料信息查询
		/// </summary>
		/// <returns></returns>
        public Message CR_CharacterInfo_Query()
        {
            System.Data.DataSet ds = null;
            string serverIP = null;
            string account = "";
            string nickName = "";
            int actionType = 0;
            try
            {
                serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_ServerIP).m_bValueBuffer);
                TLV_Structure strut1 = new TLV_Structure(TagName.CR_ACTION, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CR_ACTION).m_bValueBuffer);
                actionType = (int)strut1.toInteger();
                if (actionType == 1)
                {
                    account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_ACCOUNT).m_bValueBuffer);
                }
                else if (actionType == 2)
                {
                    nickName = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_NickName).m_bValueBuffer);
                }
                SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_AccountInfoAPI_Account + account + lg.CR_AccountInfoAPI_AccountInfo);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_AccountInfoAPI_Account + account + lg.CR_AccountInfoAPI_AccountInfo);
                ds = CRCharacterInfo.CR_CharacterInfo_Query(serverIP, account,nickName,actionType);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[0].ItemArray[0]));
                        strut.AddTagKey(TagName.CR_ACCOUNT, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,UserValidate.validData(ds.Tables[0].Rows[0].ItemArray[1]));
                        strut.AddTagKey(TagName.CR_NickName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[2]));
                        strut.AddTagKey(TagName.CR_PSTID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[3]));
                        strut.AddTagKey(TagName.CR_EXP, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[4]));
                        strut.AddTagKey(TagName.CR_Money, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[5]));
                        strut.AddTagKey(TagName.CR_RMB, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[6]));
                        strut.AddTagKey(TagName.CR_SEX, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[7]));
                        strut.AddTagKey(TagName.CR_License, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[8]));
                        strut.AddTagKey(TagName.CR_RaceTotal, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[9]));
                        strut.AddTagKey(TagName.CR_RaceWon, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[10]));
                        strut.AddTagKey(TagName.CR_ExpOrder, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[11]));
                        strut.AddTagKey(TagName.CR_WinRateOrder, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[12]));
                        strut.AddTagKey(TagName.CR_WinNumOrder, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        structList[i] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.CR_ADMIN, ServiceKey.CR_CHARACTERINFO_QUERY_RESP, 13);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.CR_AccountInfoAPI_NoAccount, Msg_Category.CR_ADMIN, ServiceKey.CR_CHARACTERINFO_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

                }

            }
            catch (System.Exception)
            {
                return Message.COMMON_MES_RESP(lg.CR_AccountInfoAPI_NoAccount, Msg_Category.CR_ADMIN, ServiceKey.CR_CHARACTERINFO_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

            }
        }
        /// <summary>
        /// 玩家资料信息修改
        /// </summary>
        /// <returns></returns>
        public Message CR_CharacterInfo_Update()
        {
            int operateUserID = 0;
            int result = -1;
            string serverIP = null;
            int pstID = 0;
            int license = 0;
            int experience = 0;
            int money = 0;
			int rmb = 0;
            try
            {
                TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
                operateUserID = (int)strut.toInteger();
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_ServerIP).m_bValueBuffer);
                strut = new TLV_Structure(TagName.CR_PSTID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CR_PSTID).m_bValueBuffer);
                pstID = (int)strut.toInteger();
                strut = new TLV_Structure(TagName.CR_License, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CR_License).m_bValueBuffer);
                license = (int)strut.toInteger();
                strut = new TLV_Structure(TagName.CR_EXP, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CR_EXP).m_bValueBuffer);
                experience = (int)strut.toInteger();
                strut = new TLV_Structure(TagName.CR_Money, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CR_Money).m_bValueBuffer);
                money = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.CR_RMB, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CR_RMB).m_bValueBuffer);
				rmb = (int)strut.toInteger();

                result = CRCharacterInfo.characterInfo_Update(operateUserID, serverIP, pstID,experience, license, money,rmb);
                if (result == -1)
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_AccountInfoAPI_Account + pstID + lg.CR_AccountInfoAPI_NoAccount);
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_AccountInfoAPI_Account + pstID + lg.CR_AccountInfoAPI_NoAccount);
                    return Message.COMMON_MES_RESP(lg.CR_AccountInfoAPI_NoAccount, Msg_Category.CR_ADMIN, ServiceKey.CR_CHARACTERINFO_UPDATE_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }
                else if (result == 1)
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_AccountInfoAPI_Account + pstID + lg.API_Update + lg.CR_CharacterInfoAPI_CharacterInfo + lg.API_Success + "!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_AccountInfoAPI_Account + pstID + lg.API_Update + lg.CR_CharacterInfoAPI_CharacterInfo + lg.API_Success + "!");
                    return Message.COMMON_MES_RESP("SUCESS", Msg_Category.CR_ADMIN, ServiceKey.CR_CHARACTERINFO_UPDATE_RESP);
                }
                else
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_AccountInfoAPI_Account + pstID + lg.API_Update + lg.CR_CharacterInfoAPI_CharacterInfo + lg.API_Failure + "!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_AccountInfoAPI_Account + pstID + lg.API_Update + lg.CR_CharacterInfoAPI_CharacterInfo + lg.API_Failure + "!");
                    return Message.COMMON_MES_RESP("FAILURE", Msg_Category.CR_ADMIN, ServiceKey.CR_CHARACTERINFO_UPDATE_RESP);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP(ex.Message, Msg_Category.CR_ADMIN, ServiceKey.CR_CHARACTERINFO_UPDATE_RESP);
            }

        }

		/// <summary>
		/// 玩家昵称信息修改
		/// </summary>
		/// <returns></returns>
		public Message CR_NickName_Update()
		{
			int operateUserID = 0;
			int result = -1;
			string serverIP = null;
			int pstID = 0;
			string nickname = null;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_ServerIP).m_bValueBuffer);
				strut = new TLV_Structure(TagName.CR_PSTID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CR_PSTID).m_bValueBuffer);
				pstID = (int)strut.toInteger();
				nickname = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_NickName).m_bValueBuffer);
				

				result = CRCharacterInfo.nickName_Update(operateUserID, serverIP, pstID,nickname);
				if (result == -1)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_AccountInfoAPI_Account + pstID + lg.CR_AccountInfoAPI_NoAccount);
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_AccountInfoAPI_Account + pstID + lg.CR_AccountInfoAPI_NoAccount);
					return Message.COMMON_MES_RESP(lg.CR_AccountInfoAPI_NoAccount, Msg_Category.CR_ADMIN, ServiceKey.CR_NICKNAME_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
				else if (result == 1)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_AccountInfoAPI_Account + pstID + lg.API_Update + lg.CR_CharacterInfoAPI_NickNameInfo + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_AccountInfoAPI_Account + pstID + lg.API_Update + lg.CR_CharacterInfoAPI_NickNameInfo + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.CR_ADMIN, ServiceKey.CR_NICKNAME_QUERY_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_AccountInfoAPI_Account + pstID + lg.API_Update + lg.CR_CharacterInfoAPI_NickNameInfo + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_AccountInfoAPI_Account + pstID +lg.API_Update + lg.CR_CharacterInfoAPI_NickNameInfo + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.CR_ADMIN, ServiceKey.CR_NICKNAME_QUERY_RESP);
				}
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message, Msg_Category.CR_ADMIN, ServiceKey.CR_NICKNAME_QUERY_RESP);
			}
		}

		/// <summary>
		/// 玩家上线、下线时间查询
		/// </summary>
		/// <returns></returns>
		public Message CR_Login_Logout_Query()
		{
			System.Data.DataSet ds = null;
			string serverIP = null;
			string userID = "";
			string nickName = "";
			int actionType = 0;
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_ServerIP).m_bValueBuffer);
				TLV_Structure strut1 = new TLV_Structure(TagName.CR_ACTION, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CR_ACTION).m_bValueBuffer);
				actionType = (int)strut1.toInteger();
				if (actionType == 1)
				{
					userID = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_UserID).m_bValueBuffer);					
				}
				else if (actionType == 2)
				{
					nickName = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_NickName).m_bValueBuffer);
				}
				SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_AccountInfoAPI_Account + userID + lg.CR_CharacterInfoAPI_OnlineStatus);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_AccountInfoAPI_Account + userID + lg.CR_CharacterInfoAPI_OnlineStatus);
				ds = CRCharacterInfo.CR_Login_Logout_Query(serverIP, userID,nickName,actionType);
				if (ds != null && ds.Tables[0].Rows.Count > 0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[0]));
						strut.AddTagKey(TagName.CR_UserName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[1]));
						strut.AddTagKey(TagName.CR_NickName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[2]));
						strut.AddTagKey(TagName.CR_Last_Login, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[3]));
						strut.AddTagKey(TagName.CR_Last_Logout, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);						
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]));
						strut.AddTagKey(TagName.CR_Last_Playing_Time, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[5]));
						strut.AddTagKey(TagName.CR_Total_Time, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.CR_ADMIN, ServiceKey.CR_LOGIN_LOGOUT_QUERY_RESP, 6);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.CR_AccountInfoAPI_NoAccount, Msg_Category.CR_ADMIN, ServiceKey.CR_LOGIN_LOGOUT_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

				}

			}
			catch (System.Exception)
			{
				return Message.COMMON_MES_RESP(lg.CR_AccountInfoAPI_NoAccount, Msg_Category.CR_ADMIN, ServiceKey.CR_LOGIN_LOGOUT_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

			}
		}
    }
}
