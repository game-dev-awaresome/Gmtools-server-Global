using System;
using System.Text;
using Audition.AUDataInfo;
using Common.Logic;
using Common.DataInfo;
using lg = Common.API.LanguageAPI;
namespace Audition.AUAPI
{
    public class AUCharacterInfoAPI
    {
        Message msg = null;
		public AUCharacterInfoAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
        public AUCharacterInfoAPI(byte[] packet)
		{
			msg = new Message(packet,(uint)packet.Length);
			
		}
        /// <summary>
        /// 玩家等级经验信息查询 
        /// </summary>
        /// <returns></returns>
        public Message AuditionLevelExp_Query()
        {
            string serverIP = null;
            System.Data.DataSet ds = null;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
                SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) +lg.AU_AUCharacterInfoAPI_CharacterLevelInfo);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.AU_AUCharacterInfoAPI_CharacterLevelInfo);
                ds = AUCharacterInfo.LevelInfo_Query(serverIP);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
                        Byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
                        strut.AddTagKey(TagName.AU_Level, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[1]));
                        strut.AddTagKey(TagName.AU_Exp, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                       
                        structList[i] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.AU_ADMIN, ServiceKey.AU_LEVELEXP_QUERY_RESP, 2);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.AU_AUCharacterInfoAPI_NoCharacterLevelInfo, Msg_Category.AU_ADMIN, ServiceKey.AU_LEVELEXP_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP(ex.Message, Msg_Category.AU_ADMIN, ServiceKey.AU_LEVELEXP_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }

        }
		/// <summary>
		/// 玩家人物资料信息
		/// </summary>
		/// <returns></returns>
		public Message AuditionCharInfo_Query()
		{
			string serverIP = null;
			string account = null;
            string userNick = null;
			System.Data.DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ACCOUNT).m_bValueBuffer);
                userNick = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_UserNick).m_bValueBuffer);
                SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+account + lg.AU_AUCharacterInfoAPI_CharacterInfo);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+account + lg.AU_AUCharacterInfoAPI_CharacterInfo);
				ds = AUCharacterInfo.characterInfo_Query(serverIP,account,userNick);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
						strut.AddTagKey(TagName.AU_UserSN,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.AU_UserID,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
                        strut.AddTagKey(TagName.AU_UserNick, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]));
						strut.AddTagKey(TagName.AU_Level,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]));
						strut.AddTagKey(TagName.AU_Exp,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[5]));
						strut.AddTagKey(TagName.AU_Point,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[6]));
						strut.AddTagKey(TagName.AU_Money,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[7]));
                        strut.AddTagKey(TagName.AU_Cash, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[8]));
						strut.AddTagKey(TagName.AU_Ranking,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[9]);
                        strut.AddTagKey(TagName.AU_Sex, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[10]);
                        strut.AddTagKey(TagName.AU_UserEMail, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						structList[i]=strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.AU_ADMIN,ServiceKey.AU_CHARACTERINFO_QUERY_RESP,11);
				}
				else
				{
                    return Message.COMMON_MES_RESP(lg.AU_AUCharacterInfoAPI_NoCharacterInfo, Msg_Category.AU_ADMIN, ServiceKey.AU_CHARACTERINFO_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP(lg.AU_AUCharacterInfoAPI_NoCharacterInfo, Msg_Category.AU_ADMIN, ServiceKey.AU_CHARACTERINFO_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 玩家资料信息修改
		/// </summary>
		/// <returns></returns>
		public Message AuditionCharacterInfo_Update()
		{
			int result = -1;
			string serverIP = null;
            int userSN = 0;
			string account = null;
			int level = 0;
			int experience =0; 
			int operateUserID = 0;
			int MCash = 0;
			int GCash = 0;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
                strut = new TLV_Structure(TagName.AU_UserSN, 4, msg.m_packet.m_Body.getTLVByTag(TagName.AU_UserSN).m_bValueBuffer);
                userSN = (int)strut.toInteger();
                account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ACCOUNT).m_bValueBuffer);
                strut = new TLV_Structure(TagName.AU_Level, 4, msg.m_packet.m_Body.getTLVByTag(TagName.AU_Level).m_bValueBuffer);
				level  =(int)strut.toInteger();
                strut = new TLV_Structure(TagName.AU_Exp, 4, msg.m_packet.m_Body.getTLVByTag(TagName.AU_Exp).m_bValueBuffer);
				experience  =(int)strut.toInteger();
                strut = new TLV_Structure(TagName.AU_Money, 4, msg.m_packet.m_Body.getTLVByTag(TagName.AU_Money).m_bValueBuffer);
                MCash = (int)strut.toInteger();
               // strut = new TLV_Structure(TagName.AU_Money, 4, msg.m_packet.m_Body.getTLVByTag(TagName.AU_Money).m_bValueBuffer);
                //GCash = (int)strut.toInteger();
                result = AUCharacterInfo.characterInfo_Update(operateUserID, serverIP,userSN,account, level, experience, MCash);
                if (result == -1)
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+account + lg.AU_AUCharacterInfoAPI_NoAccount);
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+account + lg.AU_AUCharacterInfoAPI_NoAccount);
                    return Message.COMMON_MES_RESP(lg.AU_AUCharacterInfoAPI_NoAccount, Msg_Category.AU_ADMIN, ServiceKey.AU_CHARACTERINFO_UPDATE_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
                }
                else if(result==1)
				{
                    SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+account + lg.API_Update + lg.AU_AUCharacterInfoAPI_CharacterInfo + lg.API_Success + "!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+account + lg.API_Update + lg.AU_AUCharacterInfoAPI_CharacterInfo + lg.API_Success + "!");
                    return Message.COMMON_MES_RESP("SUCESS", Msg_Category.AU_ADMIN, ServiceKey.AU_CHARACTERINFO_UPDATE_RESP);
				}
				else
				{
                    SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+account + lg.API_Update + lg.AU_AUCharacterInfoAPI_CharacterInfo + lg.API_Failure + "!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+account + lg.API_Update + lg.AU_AUCharacterInfoAPI_CharacterInfo + lg.API_Failure + "!");
                    return Message.COMMON_MES_RESP("FAILURE", Msg_Category.AU_ADMIN, ServiceKey.AU_CHARACTERINFO_UPDATE_RESP);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP(ex.Message, Msg_Category.AU_ADMIN, ServiceKey.AU_CHARACTERINFO_UPDATE_RESP);
			}

		}
      
    }
}
