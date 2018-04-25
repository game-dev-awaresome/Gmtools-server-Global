using System;
using System.Data;
using System.Text;
using O2JAM.O2JAMDataInfo;
using Common.Logic;
using Common.DataInfo;
using lg = Common.API.LanguageAPI;
namespace O2JAM.O2JAMAPI
{
	/// <summary>
	/// SDOCharacterInfoAPI 的摘要说明。
	/// </summary>
	public class O2JAMCharacterInfoAPI
	{
		Message msg = null;
		public O2JAMCharacterInfoAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public O2JAMCharacterInfoAPI(byte[] packet)
		{
			msg = new Message(packet,(uint)packet.Length);
			
		}
		/// <summary>
		/// 玩家人物资料信息
		/// </summary>
		/// <returns></returns>
		public Message O2JAMCharInfo_Query()
		{
			string serverIP = null;
			string account = null;
            string userNick = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_UserID).m_bValueBuffer);
                userNick = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_UserNick).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+account+lg.O2JAM_CharacterInfoAPI_CharacterInfo);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+account+lg.O2JAM_CharacterInfoAPI_CharacterInfo);
				ds = CharacterInfo.characterInfo_Query(serverIP,account,userNick);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.o2jam_USER_INDEX_ID,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.o2jam_USER_ID,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
                        strut.AddTagKey(TagName.o2jam_USER_NICKNAME, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]));
						strut.AddTagKey(TagName.o2jam_Sex,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[4]);
						strut.AddTagKey(TagName.o2jam_Level,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[5]);
						strut.AddTagKey(TagName.o2jam_Exp,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[6]);
						strut.AddTagKey(TagName.o2jam_Battle,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[7]);
						strut.AddTagKey(TagName.o2jam_Win,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[8]);
						strut.AddTagKey(TagName.o2jam_Draw,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[9]);
						strut.AddTagKey(TagName.o2jam_Lose,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP,ds.Tables[0].Rows[i].ItemArray[10]);
						strut.AddTagKey(TagName.o2jam_REG_DATE,TagFormat.TLV_TIMESTAMP,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[12]);
						strut.AddTagKey(TagName.o2jam_GEM,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[13]);
						strut.AddTagKey(TagName.o2jam_MCASH,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						structList[i]=strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.O2JAM_ADMIN,ServiceKey.O2JAM_CHARACTERINFO_QUERY_RESP,13);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.O2JAM_CharacterInfoAPI_NoCharacterInfo,Msg_Category.O2JAM_ADMIN,ServiceKey.O2JAM_CHARACTERINFO_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
                return Message.COMMON_MES_RESP(lg.O2JAM_CharacterInfoAPI_NoCharacterInfo, Msg_Category.O2JAM_ADMIN, ServiceKey.O2JAM_CHARACTERINFO_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 玩家资料信息修改
		/// </summary>
		/// <returns></returns>
		public Message O2JAMCharacterInfo_Update()
		{
			int result = -1;
			string serverIP = null;
			int userIndexID = 0;
			string account = null;
			int level = 0;
			int experience =0; 
			int operateUserID = 0;
			int battle = 0;
			int win = 0;
			int draw = 0;
			int lose = 0;
			int GCash = 0;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_UserID).m_bValueBuffer);
				strut = new TLV_Structure(TagName.o2jam_USER_INDEX_ID,4,msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_USER_INDEX_ID).m_bValueBuffer);
				userIndexID =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.o2jam_Level,4,msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_Level).m_bValueBuffer);
				level  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.o2jam_Exp,4,msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_Exp).m_bValueBuffer);
				experience  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.o2jam_Battle,4,msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_Battle).m_bValueBuffer);
				battle  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.o2jam_Win,4,msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_Win).m_bValueBuffer);
				win  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.o2jam_Draw,4,msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_Draw).m_bValueBuffer);
				draw  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.o2jam_Lose,4,msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_Lose).m_bValueBuffer);
				lose  =(int)strut.toInteger();
                strut = new TLV_Structure(TagName.o2jam_GEM, 4, msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_GEM).m_bValueBuffer);
                GCash = (int)strut.toInteger();
				result = CharacterInfo.characterInfo_Update(operateUserID,serverIP,account,userIndexID,level,experience,battle,win,draw,lose,GCash);
                if (result == -1)
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.O2JAM_CharacterInfoAPI_Account + account + lg.O2JAM_CharacterInfoAPI_NoAccount);
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.O2JAM_CharacterInfoAPI_Account + account + lg.O2JAM_CharacterInfoAPI_NoAccount);
                    return Message.COMMON_MES_RESP(lg.O2JAM_CharacterInfoAPI_NoAccount, Msg_Category.O2JAM_ADMIN, ServiceKey.O2JAM_CHARACTERINFO_UPDATE_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
                }
                else if(result==1)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+account+ lg.API_Update + lg.O2JAM_CharacterInfoAPI_CharacterInfo + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+account+lg.API_Update + lg.O2JAM_CharacterInfoAPI_CharacterInfo + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.O2JAM_ADMIN,ServiceKey.O2JAM_CHARACTERINFO_UPDATE_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+account+lg.API_Update + lg.O2JAM_CharacterInfoAPI_CharacterInfo + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+account+lg.API_Update + lg.O2JAM_CharacterInfoAPI_CharacterInfo + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.O2JAM_ADMIN,ServiceKey.O2JAM_CHARACTERINFO_UPDATE_RESP);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.O2JAM_ADMIN,ServiceKey.O2JAM_CHARACTERINFO_UPDATE_RESP);
			}

		}
      
	}
}
