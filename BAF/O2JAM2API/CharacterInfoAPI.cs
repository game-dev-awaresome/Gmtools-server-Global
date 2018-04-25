using System;
using System.Data;
using System.Text;
using Common.Logic;
using BAF.O2JAM2DataInfo;
using Common.DataInfo;
using lg = Common.API.LanguageAPI;
namespace BAF.O2JAM2API
{
	/// <summary>
	/// CharacterInfoAPI 的摘要说明。
	/// </summary>
	public class CharacterInfoAPI
	{
		Message msg = null;
		public CharacterInfoAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public CharacterInfoAPI(byte[] packet)
		{
			msg = new Message(packet,(uint)packet.Length);
		}
		/// <summary>
		/// 玩家人物资料信息
		/// </summary>
		/// <returns></returns>
		public Message O2JAM2CharInfo_Query()
		{
			string serverIP = null;
			string account = null;
			string userNick = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserID).m_bValueBuffer);
				userNick = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserNick).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+lg.O2JAM2API_CharacterInfoAPI_CharacterInfo);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+lg.O2JAM2API_CharacterInfoAPI_CharacterInfo);
				ds = CharacterInfo.characterInfo_Query(serverIP,account,userNick);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut = new Query_Structure(12);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.O2JAM2_UserIndexID,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.O2JAM2_UserID,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.O2JAM2_UserNick, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[3]);
						strut.AddTagKey(TagName.O2JAM2_Level,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[4]);
						strut.AddTagKey(TagName.O2JAM2_Exp,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[5]);
						strut.AddTagKey(TagName.O2JAM2_TOTAL,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[6]);
						strut.AddTagKey(TagName.O2JAM2_Win,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[7]);
						strut.AddTagKey(TagName.O2JAM2_Draw,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[8]);
						strut.AddTagKey(TagName.O2JAM2_Lose,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[9]);
						strut.AddTagKey(TagName.O2JAM2_GCash,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[10]);
						strut.AddTagKey(TagName.O2JAM2_MCash,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_BOOLEAN,ds.Tables[0].Rows[i].ItemArray[11]);
						strut.AddTagKey(TagName.O2JAM2_Sex,TagFormat.TLV_BOOLEAN,(uint)bytes.Length,bytes);
						structList[i]=strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_CHARACTERINFO_QUERY_RESP,12);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.O2JAM2API_CharacterInfoAPI_NoCharacterInfo,Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_CHARACTERINFO_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.O2JAM2API_CharacterInfoAPI_NoCharacterInfo, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_CHARACTERINFO_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 玩家资料信息修改
		/// </summary>
		/// <returns></returns>
		public Message O2JAM2CharacterInfo_Update()
		{
			int result = -1;
			string serverIP = null;
			string account = null;
			int level = 0;
			int experience =0; 
			int operateUserID = 0;
			int battle = 0;
			int win = 0;
			int draw = 0;
			int lose = 0;
			int MCash = 0;
			int GCash = 0;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserID).m_bValueBuffer);
				strut = new TLV_Structure(TagName.O2JAM2_Level,4,msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_Level).m_bValueBuffer);
				level  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.O2JAM2_Exp,4,msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_Exp).m_bValueBuffer);
				experience  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.O2JAM2_TOTAL,4,msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_TOTAL).m_bValueBuffer);
				battle  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.O2JAM2_Win,4,msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_Win).m_bValueBuffer);
				win  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.O2JAM2_Draw,4,msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_Draw).m_bValueBuffer);
				draw  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.O2JAM2_Lose,4,msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_Lose).m_bValueBuffer);
				lose  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.O2JAM2_GCash, 4, msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_GCash).m_bValueBuffer);
				GCash = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.O2JAM2_MCash, 4, msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_MCash).m_bValueBuffer);
				MCash = (int)strut.toInteger();
				result = CharacterInfo.characterInfo_Update(operateUserID,serverIP,account,level,experience,battle,win,draw,lose,MCash,GCash);
				if (result == -1)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + serverIP +  account + lg.O2JAM2API_AccountInfoAPI_NoAccount);
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) +  account + lg.O2JAM2API_AccountInfoAPI_NoAccount);
					return Message.COMMON_MES_RESP(lg.O2JAM2API_AccountInfoAPI_NoAccount, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_CHARACTERINFO_UPDATE_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
				else if(result==1)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.O2JAM2API_AccountInfoAPI_Account+account + lg.API_Update + lg.O2JAM2API_CharacterInfoAPI_CharacterInfo + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+ lg.API_Update + lg.O2JAM2API_CharacterInfoAPI_CharacterInfo + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_CHARACTERINFO_UPDATE_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + serverIP+lg.O2JAM2API_AccountInfoAPI_Account+account+ lg.API_Update + lg.O2JAM2API_CharacterInfoAPI_CharacterInfo + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+ lg.API_Update + lg.O2JAM2API_CharacterInfoAPI_CharacterInfo + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_CHARACTERINFO_UPDATE_RESP);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.O2JAM2API_AccountInfoAPI_NoAccount,Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_CHARACTERINFO_UPDATE_RESP);
			}

		}
		/// <summary>
		/// 玩家等级经验信息查询 
		/// </summary>
		/// <returns></returns>
		public Message Baf_LevelExp_Query()
		{
			string serverIP = null;
			System.Data.DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.O2JAM2API_CharacterInfoAPI_CharacterLevelInfo);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.O2JAM2API_CharacterInfoAPI_CharacterLevelInfo);
				ds = CharacterInfo.LevelInfo_Query();
				if (ds != null && ds.Tables[0].Rows.Count > 0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						Byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
						strut.AddTagKey(TagName.O2JAM2_Level, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[1]));
						strut.AddTagKey(TagName.O2JAM2_Exp, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                       
						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_LEVELEXP_QUERY_RESP, 2);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.O2JAM2API_CharacterInfoAPI_NoCharacterLevelInfo, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_LEVELEXP_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_LEVELEXP_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
	}
}
