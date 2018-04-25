using System;
using System.Data;
using System.Text;
using SDO.SDODataInfo;
using Common.Logic;
using Common.DataInfo;
using lg = Common.API.LanguageAPI;
namespace SDO.SDOAPI
{
	/// <summary>
	/// SDOCharacterInfoAPI 的摘要说明。
	/// </summary>
	public class SDOCharacterInfoAPI
	{
		Message msg = null;
		public SDOCharacterInfoAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public SDOCharacterInfoAPI(byte[] packet)
		{
			msg = new Message(packet,(uint)packet.Length);
			
		}
		/// <summary>
		/// 玩家人物资料信息
		/// </summary>
		/// <returns></returns>
		public Message SDOCharInfo_Query()
		{
			string serverIP = null;
			string account = null;
			string userNick = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
				userNick = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_NickName).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Display + lg.SDOAPI_SDO +"+>"+lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+account+ lg.SDOAPI_SDOCharacterInfoAPI_AccountInfo);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+" "+account + lg.SDOAPI_SDOCharacterInfoAPI_AccountInfo + "!");
				ds = CharacterInfo.characterInfo_Query(serverIP,account,userNick);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.SDO_UserIndexID,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.SDO_Account,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[15]);
						strut.AddTagKey(TagName.SDO_NickName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.SDO_Level,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[3]);
						strut.AddTagKey(TagName.SDO_Exp,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[4]);
						strut.AddTagKey(TagName.SDO_GameTotal,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[5]);
						strut.AddTagKey(TagName.SDO_GameWin,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[6]);
						strut.AddTagKey(TagName.SDO_DogFall,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[7]);
						strut.AddTagKey(TagName.SDO_GameFall,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[8]);
						strut.AddTagKey(TagName.SDO_Reputation,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[9]);
						strut.AddTagKey(TagName.SDO_GCash,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[10]);
						strut.AddTagKey(TagName.SDO_MCash,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						object address;
						if(ds.Tables[0].Rows[i].IsNull(11)==false)
							address = ds.Tables[0].Rows[i].ItemArray[11];
						else
							address = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,address);
						strut.AddTagKey(TagName.SDO_Address,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						object age;
						if(ds.Tables[0].Rows[i].IsNull(12)==false)
							age = ds.Tables[0].Rows[i].ItemArray[12];
						else
							age = 0;
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,age);
						strut.AddTagKey(TagName.SDO_Age,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						object city;
						if(ds.Tables[0].Rows[i].IsNull(13)==false)
							city = ds.Tables[0].Rows[i].ItemArray[13];
						else
							city = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,city);
						strut.AddTagKey(TagName.SDO_City,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_BOOLEAN,ds.Tables[0].Rows[i].ItemArray[14]);
						strut.AddTagKey(TagName.SDO_SEX,TagFormat.TLV_BOOLEAN,(uint)bytes.Length,bytes);
						structList[i]=strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHARACTERINFO_QUERY_RESP,16);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDOAPI_SDOCharacterInfoAPI_NoRelativeInfo,Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHARACTERINFO_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.SDOAPI_SDOCharacterInfoAPI_NoRelativeInfo, Msg_Category.SDO_ADMIN, ServiceKey.SDO_CHARACTERINFO_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 玩家资料信息修改
		/// </summary>
		/// <returns></returns>
		public Message SDOCharacterInfo_Update()
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
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
				strut = new TLV_Structure(TagName.SDO_Level,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Level).m_bValueBuffer);
				level  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_Exp,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Exp).m_bValueBuffer);
				experience  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_GameTotal,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_GameTotal).m_bValueBuffer);
				battle  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_GameWin,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_GameWin).m_bValueBuffer);
				win  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_DogFall,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_DogFall).m_bValueBuffer);
				draw  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_GameFall,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_GameFall).m_bValueBuffer);
				lose  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_MCash, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_MCash).m_bValueBuffer);
				MCash = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_GCash, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_GCash).m_bValueBuffer);
				GCash = (int)strut.toInteger();
				result = CharacterInfo.characterInfo_Update(operateUserID,serverIP,account,level,experience,battle,win,draw,lose,MCash,GCash);
				if (result == -1)
				{
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) +" "+ account + lg.SDOAPI_SDOCharacterInfoAPI_NoAccount);
					return Message.COMMON_MES_RESP(lg.SDOAPI_SDOCharacterInfoAPI_NoAccount, Msg_Category.SDO_ADMIN, ServiceKey.SDO_CHARACTERINFO_UPDATE_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
				else if(result==1)
				{
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+" "+account + lg.API_Update + lg.SDOAPI_SDOCharacterInfoAPI_AccountInfo + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHARACTERINFO_UPDATE_RESP);
				}
				else
				{
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+" "+account + lg.API_Update + lg.SDOAPI_SDOCharacterInfoAPI_AccountInfo + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHARACTERINFO_UPDATE_RESP);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHARACTERINFO_UPDATE_RESP);
			}

		}
		public Message SDOEmailQuery()
		{
			string  result = null;
			string account = null;
			try
			{
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
				result = CharacterInfo.SDOEmail_Query(account);
				if (result != null)
				{
					SqlHelper.log.WriteLog("久游用户中心+>玩家" + account + "邮件地址!");
					Console.WriteLine(DateTime.Now + " - 久游用户中心+>玩家" + account + "邮件地址!");
					return Message.COMMON_MES_RESP(result, Msg_Category.SDO_ADMIN, ServiceKey.SDO_EMAIL_QUERY_RESP, TagName.SDO_Email, TagFormat.TLV_STRING);
				}
				else
				{
					SqlHelper.log.WriteLog("久游用户中心+>玩家" + account + "邮件地址!");
					Console.WriteLine(DateTime.Now + " - 久游用户中心+>玩家" + account + "邮件地址!");
					return Message.COMMON_MES_RESP("该玩家EMAIL为空!", Msg_Category.SDO_ADMIN, ServiceKey.SDO_EMAIL_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch (System.Exception ex)
			{
				SqlHelper.log.WriteLog("久游用户中心+>玩家" + account + "邮件地址!");
				Console.WriteLine(DateTime.Now + " - 久游用户中心+>玩家" + account + "邮件地址!");
				return Message.COMMON_MES_RESP("该玩家EMAIL为空!", Msg_Category.SDO_ADMIN, ServiceKey.SDO_EMAIL_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		public Message sendEmailPasswdMsg()
		{
			int result = 0;
			int operateUserID = 0;
			string serverIP = null;
			string account = null;
			string email = null;
			string password = null;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
				email = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Email).m_bValueBuffer);
				password = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.PassWord).m_bValueBuffer);
				result = CharacterInfo.sendEmailPasswd(operateUserID,account, email, password);
				if (result == 1)
				{
					SqlHelper.log.WriteLog("久游用户中心+>玩家" + account + "发送密码成功!");
					Console.WriteLine(DateTime.Now + " - 久游用户中心玩家" + account + "发送密码成功!");
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.SDO_ADMIN, ServiceKey.SDO_PASSWORD_RECOVERY_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog("久游用户中心+>玩家" + account + "发送密码失败!");
					Console.WriteLine(DateTime.Now + " - 久游用户中心+>玩家" + account + "发送密码失败!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.SDO_ADMIN, ServiceKey.SDO_PASSWORD_RECOVERY_RESP);
				}

			}
			catch (System.Exception)
			{
				SqlHelper.log.WriteLog("久游用户中心+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "玩家" + account + "发送密码失败!");
				Console.WriteLine(DateTime.Now + " - 久游用户中心+>玩家" + account + "发送密码失败!");
				return Message.COMMON_MES_RESP("FAILURE", Msg_Category.SDO_ADMIN, ServiceKey.SDO_PASSWORD_RECOVERY_RESP);
			}

		}
	}
}
