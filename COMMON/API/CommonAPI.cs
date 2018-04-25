using System;
using System.Data;
using Common.Logic;
using Common.DataInfo;
namespace Common.API
{
	/// <summary>
	/// CommonAPI 的摘要说明。
	/// </summary>
	public class CommonAPI
	{
		/// <summary>
		/// 用户ID
		/// </summary>
		private int userByID = 0;
		/// <summary>
		/// 用户名
		/// </summary>
		private string userName = null;
		/// <summary>
		/// 密码
		/// </summary>
		private string password = null;
		/// <summary>
		/// MAC码
		/// </summary>
		private string mac = null;
		/// <summary>
		/// 消息
		/// </summary>
		private string msg = null;
		private DateTime connTime ;
		Message message = null;
		public CommonAPI(int userID_,string msg_,byte[] packet)
		{
			this.UserByID = userID_;
			this.Msg = msg_;
			message = new Message(packet,(uint)packet.Length);

		}
		public CommonAPI(string userNm,string passwd,string mac,DateTime connTime_,string msg_)
		{
			this.UserName = userNm;
			this.PassWord = passwd;
			this.MAC = mac;
			this.Msg = msg_;
			connTime  = connTime_;
		}
		public Message packConnectResp()
		{
			Common.DataInfo.GMUserInfo.insertMac(this.userName,this.PassWord,this.MAC);
			return Message.Common_CONNECT_RESP(this.Msg);
		}
		public Message packDisConnectResp()
		{
			return Message.Common_DISCONNECT_RESP(this.userByID,msg);
		}
        /// <summary>
        /// 请求所有游戏服务器IP列表
        /// </summary>
        /// <returns></returns>
        public Message packCreateServerInfoResp()
        {
            int result = -1;
            int operateUserID = 0;
            int gameID = 0;
            int gameDBID = 0;
            string gameIP = null;
            string gameCity = null;
            int gameFlag  = 0;
            GMLogAPI logAPI = new GMLogAPI();
            try
            {
                TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, message.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
                operateUserID = (int)strut.toInteger();
                strut = new TLV_Structure(TagName.ServerInfo_GameID, 4, message.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_GameID).m_bValueBuffer);
                gameID = (int)strut.toInteger();
                strut = new TLV_Structure(TagName.ServerInfo_GameDBID, 4, message.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_GameDBID).m_bValueBuffer);
                gameDBID = (int)strut.toInteger();
                gameIP = System.Text.Encoding.Default.GetString(message.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_IP).m_bValueBuffer);
                gameCity = System.Text.Encoding.Default.GetString(message.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);
                userName = System.Text.Encoding.Default.GetString(message.m_packet.m_Body.getTLVByTag(TagName.UserName).m_bValueBuffer);
                PassWord = System.Text.Encoding.Default.GetString(message.m_packet.m_Body.getTLVByTag(TagName.PassWord).m_bValueBuffer);
                strut = new TLV_Structure(TagName.ServerInfo_GameFlag, 4, message.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_GameFlag).m_bValueBuffer);
                gameFlag = (int)strut.toInteger();
                result = CommonInfo.LinkServerIP_Create(operateUserID, gameIP,UserName,PassWord,gameCity,gameID,gameDBID,gameFlag);
               if (result == 0)
                {
				    
                    logAPI.writeTitle(LanguageAPI.API_Add +LanguageAPI.API_CommonAPI_NewServer, LanguageAPI.API_Add +LanguageAPI.API_CommonAPI_NewServer + LanguageAPI.API_Success + "！");
                    logAPI.writeContent(LanguageAPI.API_CommonAPI_GameID,LanguageAPI.API_CommonAPI_ServerIP, LanguageAPI.API_CommonAPI_GameCity);
                    logAPI.writeContent(Convert.ToString(gameID), gameIP, gameCity);
                    Console.WriteLine(logAPI.Buffer.ToString());
                    return Message.COMMON_MES_RESP("SUCESS",Msg_Category.COMMON,ServiceKey.LINK_SERVERIP_CREATE_RESP);
                }
                else
                {
                    logAPI.writeTitle(LanguageAPI.API_Add +LanguageAPI.API_CommonAPI_NewServer, LanguageAPI.API_Add +LanguageAPI.API_CommonAPI_NewServer + LanguageAPI.API_Failure + "！");
                    logAPI.writeContent(LanguageAPI.API_CommonAPI_GameID,LanguageAPI.API_CommonAPI_ServerIP, LanguageAPI.API_CommonAPI_GameCity);
                    logAPI.writeContent(Convert.ToString(gameID), gameIP, gameCity);
                    Console.WriteLine(logAPI.Buffer.ToString());
                    return Message.COMMON_MES_RESP("FAILURE", Msg_Category.COMMON, ServiceKey.LINK_SERVERIP_CREATE_RESP);

                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP("FAILURE", Msg_Category.COMMON, ServiceKey.LINK_SERVERIP_CREATE_RESP);
            }
        }
		/// <summary>
		/// 请求所有游戏服务器IP列表
		/// </summary>
		/// <returns></returns>
		public Message packDelServerInfoResp()
		{
			int result = -1;
			int operateUserID = 0;
			int idx= 0;
			string gameIP = null;
			GMLogAPI logAPI = new GMLogAPI();
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, message.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.ServerInfo_Idx, 4, message.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_Idx).m_bValueBuffer);
				idx = (int)strut.toInteger();
				gameIP = System.Text.Encoding.Default.GetString(message.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_IP).m_bValueBuffer);

				result = CommonInfo.LinkServerIP_Delete(operateUserID, idx,gameIP);
				if (result == 1)
				{
					logAPI.writeTitle(LanguageAPI.API_Delete +LanguageAPI.API_CommonAPI_NewServer, LanguageAPI.API_Delete +LanguageAPI.API_CommonAPI_NewServer + LanguageAPI.API_Success + "！");
					logAPI.writeContent(LanguageAPI.API_CommonAPI_GameID,LanguageAPI.API_CommonAPI_ServerIP, LanguageAPI.API_CommonAPI_GameCity);
					logAPI.writeContent(Convert.ToString(idx), gameIP, gameIP);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.COMMON,ServiceKey.LINK_SERVERIP_DELETE_RESP);
				}
				else
				{
					logAPI.writeTitle(LanguageAPI.API_Delete +LanguageAPI.API_CommonAPI_NewServer, LanguageAPI.API_Delete +LanguageAPI.API_CommonAPI_NewServer + LanguageAPI.API_Failure + "！");
					logAPI.writeContent(LanguageAPI.API_CommonAPI_GameID,LanguageAPI.API_CommonAPI_ServerIP, LanguageAPI.API_CommonAPI_GameCity);
					logAPI.writeContent(Convert.ToString(idx), gameIP, gameIP);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.COMMON, ServiceKey.LINK_SERVERIP_DELETE_RESP);

				}

			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP("FAILURE", Msg_Category.COMMON, ServiceKey.LINK_SERVERIP_DELETE_RESP);
			}
		}
        /// <summary>
        /// 请求所有游戏服务器IP列表
        /// </summary>
        /// <returns></returns>
        public Message packServerInfoALLResp()
        {
            DataSet ds = CommonInfo.serverIP_QueryAll();
            if (ds!=null && ds.Tables[0].Rows.Count <= 0)
            {
                return Message.COMMON_MES_RESP(LanguageAPI.API_CommonAPI_GameListEmpty, Msg_Category.COMMON, ServiceKey.SERVERINFO_IP_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }
            else
            {
                Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
                    byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[0]);
                    strut.AddTagKey(TagName.ServerInfo_Idx, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                    bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[1]);
                    strut.AddTagKey(TagName.ServerInfo_IP, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                    bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
                    strut.AddTagKey(TagName.ServerInfo_City, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                    bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[3]);
                    strut.AddTagKey(TagName.ServerInfo_GameID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                    bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[4]);
                    strut.AddTagKey(TagName.ServerInfo_GameName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                    bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[5]);
                    strut.AddTagKey(TagName.ServerInfo_GameDBID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                    bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[6]);
                    strut.AddTagKey(TagName.ServerInfo_GameFlag, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                    structList[i] = strut;
                }
                return Message.COMMON_MES_RESP(structList, Msg_Category.COMMON, ServiceKey.SERVERINFO_IP_QUERY_RESP, 7);

            }
        }
		/// <summary>
		/// 请求游戏服务器IP列表
		/// </summary>
		/// <returns></returns>
		public Message packServerInfoResp()
		{
			int gameID = 0;
			int gameDbID = 0 ;
			TLV_Structure tlvStrut = new TLV_Structure(TagName.ServerInfo_GameID,4,message.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_GameID).m_bValueBuffer);
			gameID = (int)tlvStrut.toInteger();
			tlvStrut = new TLV_Structure(TagName.ServerInfo_GameDBID,4,message.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_GameDBID).m_bValueBuffer);
			gameDbID = (int)tlvStrut.toInteger();
			DataSet ds = CommonInfo.serverIP_Query(gameID,gameDbID);
			if(ds!=null && ds.Tables[0].Rows.Count<=0)
			{
				return Message.COMMON_MES_RESP(LanguageAPI.API_CommonAPI_GameListEmpty,Msg_Category.COMMON,ServiceKey.SERVERINFO_IP_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
			else
			{
				Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
				for(int i=0;i<ds.Tables[0].Rows.Count;i++)
				{
					Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);	
					byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[0]);
					strut.AddTagKey(TagName.ServerInfo_IP,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
					strut.AddTagKey(TagName.ServerInfo_City,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[2]);
					strut.AddTagKey(TagName.ServerInfo_GameName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
					structList[i]=strut;
				}
				return Message.COMMON_MES_RESP(structList,Msg_Category.COMMON,ServiceKey.SERVERINFO_IP_QUERY_RESP,3);

			}
		}
		/// <summary>
		/// 查看工具操作记录
		/// </summary>
		/// <returns>工具操作记录消息集</returns>
		public Message UserOperateLog_Query(int index,int pageSize)
		{
			int userID = 0;
			DateTime beginDate;
			DateTime endDate;
			DataSet ds = null;
			try
			{
				TLV_Structure tlvStrut = new TLV_Structure(TagName.User_ID, 4, message.m_packet.m_Body.getTLVByTag(TagName.User_ID).m_bValueBuffer);
				userID = (int)tlvStrut.toInteger();
				tlvStrut = new TLV_Structure(TagName.SDO_BeginTime, 3, message.m_packet.m_Body.getTLVByTag(TagName.BeginTime).m_bValueBuffer);
				beginDate = tlvStrut.toDate();
				tlvStrut = new TLV_Structure(TagName.SDO_EndTime, 3, message.m_packet.m_Body.getTLVByTag(TagName.EndTime).m_bValueBuffer);
				endDate = tlvStrut.toDate();
				ds = CommonInfo.OperateLog_Query(userID, beginDate, endDate);
				if (ds != null && ds.Tables[0].Rows.Count > 0)
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
						strut.AddTagKey(TagName.RealName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						string gameName;
						if(ds.Tables[0].Rows[i].IsNull(1)==false)
							gameName = ds.Tables[0].Rows[i].ItemArray[1].ToString();
						else
							gameName = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,gameName);
						strut.AddTagKey(TagName.GameName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						string City;
						if(ds.Tables[0].Rows[i].IsNull(2)==false)
							City = ds.Tables[0].Rows[i].ItemArray[2].ToString();
						else
							City = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,City);
						strut.AddTagKey(TagName.SDO_City, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						string RealAct;
						if(ds.Tables[0].Rows[i].IsNull(3)==false)
							RealAct = ds.Tables[0].Rows[i].ItemArray[3].ToString();
						else
							RealAct = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,RealAct);
						strut.AddTagKey(TagName.Real_ACT, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, ds.Tables[0].Rows[i].ItemArray[4]);
						strut.AddTagKey(TagName.ACT_Time, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
						structList[i - index] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.COMMON, ServiceKey.GMTOOLS_OperateLog_Query_RESP, 6);

				}
				else
				{
					return Message.COMMON_MES_RESP(LanguageAPI.API_CommonAPI_NoLog, Msg_Category.COMMON, ServiceKey.GMTOOLS_OperateLog_Query_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

				}
			}
			catch (Common.Logic.Exception ex)
			{
				return Message.COMMON_MES_RESP(ex.Message, Msg_Category.COMMON, ServiceKey.GMTOOLS_OperateLog_Query_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}


		}
		public Message UpdateList_Query()
		{
			DataSet ds = null;
			try
			{
				SqlHelper.log.WriteLog(LanguageAPI.API_CommonAPI_UpdateInfo);
				Console.WriteLine(DateTime.Now+" -" + LanguageAPI.API_CommonAPI_UpdateInfo);
				ds = CommonInfo.UpdateList_Query();
				if (ds != null && ds.Tables[0].Rows.Count > 0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
						strut.AddTagKey(TagName.Update_ID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						string updateModule;
						if(ds.Tables[0].Rows[i].IsNull(1)==false)
							updateModule = ds.Tables[0].Rows[i].ItemArray[1].ToString();
						else
							updateModule = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,updateModule);
						strut.AddTagKey(TagName.Update_Module, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						string updateContext;
						if(ds.Tables[0].Rows[i].IsNull(2)==false)
							updateContext = ds.Tables[0].Rows[i].ItemArray[2].ToString();
						else
							updateContext = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,updateContext);
						strut.AddTagKey(TagName.Update_Context, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP,Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[3]));
						strut.AddTagKey(TagName.Update_Date, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.COMMON, ServiceKey.GMTOOLS_UPDATELIST_QUERY_RESP, 4);

				}
				else
				{
					return Message.COMMON_MES_RESP(LanguageAPI.API_CommonAPI_NoUpdateInfo, Msg_Category.COMMON, ServiceKey.GMTOOLS_UPDATELIST_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

				}
			}
			catch (System.Exception ex)
			{
				return Message.COMMON_MES_RESP(ex.Message, Msg_Category.COMMON, ServiceKey.GMTOOLS_UPDATELIST_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		public Message BUGList_Query(int index,int pageSize)
		{
			DataSet ds = null;
			try
			{
				SqlHelper.log.WriteLog(LanguageAPI.API_CommonAPI_BugInfo);
				Console.WriteLine(DateTime.Now+" - "+LanguageAPI.API_CommonAPI_BugInfo);
				ds = CommonInfo.BugList_Query();
				if (ds != null && ds.Tables[0].Rows.Count > 0)
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
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
						strut.AddTagKey(TagName.Bug_ID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						string bugSubject;
						if(ds.Tables[0].Rows[i].IsNull(1)==false)
							bugSubject = ds.Tables[0].Rows[i].ItemArray[1].ToString();
						else
							bugSubject = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,bugSubject);
						strut.AddTagKey(TagName.Bug_Subject, TagFormat.TLV_STRING,(uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.GameName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[3]);
						strut.AddTagKey(TagName.ModuleName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						string BugContext;
						if(ds.Tables[0].Rows[i].IsNull(4)==false)
							BugContext = ds.Tables[0].Rows[i].ItemArray[4].ToString();
						else
							BugContext = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,BugContext);
						strut.AddTagKey(TagName.Bug_Context, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP,Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[5]));
						strut.AddTagKey(TagName.Bug_Date, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[6]));
						strut.AddTagKey(TagName.User_ID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						string bugSender;
						if(ds.Tables[0].Rows[i].IsNull(7)==false)
							bugSender = ds.Tables[0].Rows[i].ItemArray[7].ToString();
						else
							bugSender = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,bugSender);
						strut.AddTagKey(TagName.RealName, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[8]));
						strut.AddTagKey(TagName.Bug_Process, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						string bugResult;
						if(ds.Tables[0].Rows[i].IsNull(9)==false)
							bugResult = ds.Tables[0].Rows[i].ItemArray[9].ToString();
						else
							bugResult = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,bugResult);
						strut.AddTagKey(TagName.Bug_Result, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
						structList[i - index] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.COMMON, ServiceKey.GMTOOLS_BUGLIST_QUERY_RESP, 11);
				}
				else
				{
					return Message.COMMON_MES_RESP(LanguageAPI.API_CommonAPI_NoBugInfo, Msg_Category.COMMON, ServiceKey.GMTOOLS_BUGLIST_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
			}
			catch (System.Exception ex)
			{
				return Message.COMMON_MES_RESP(LanguageAPI.API_CommonAPI_NoBugInfo, Msg_Category.COMMON, ServiceKey.GMTOOLS_BUGLIST_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 提交GMTOOLS新BUG信息
		/// </summary>
		/// <returns></returns>
		public Message Bug_Insert()
		{
			int userbyID = 0;
			string bugSubject = null;
			string bugContext = null;
			int gameID = 0;;
			int bugType = 0;
			//int bugSender =0;
			//string result = null ;
			TLV_Structure tlvStrut = new TLV_Structure(TagName.UserByID,4,message.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
			userbyID = (int)tlvStrut.toInteger();
			bugSubject = System.Text.Encoding.Default.GetString(message.m_packet.m_Body.getTLVByTag(TagName.Bug_Subject).m_bValueBuffer);
			bugContext = System.Text.Encoding.Default.GetString(message.m_packet.m_Body.getTLVByTag(TagName.Bug_Context).m_bValueBuffer);
			tlvStrut = new TLV_Structure(TagName.GameID,4,message.m_packet.m_Body.getTLVByTag(TagName.GameID).m_bValueBuffer);
			gameID = (int)tlvStrut.toInteger();	
			tlvStrut = new TLV_Structure(TagName.Bug_Type,4,message.m_packet.m_Body.getTLVByTag(TagName.Bug_Type).m_bValueBuffer);
			bugType = (int)tlvStrut.toInteger();			
			//tlvStrut = new TLV_Structure(TagName.Bug_Sender, 4, message.m_packet.m_Body.getTLVByTag(TagName.Bug_Sender).m_bValueBuffer);
			//bugSender = (int)tlvStrut.toInteger();
			//tlvStrut = new TLV_Structure(TagName.Bug_Date, 4, message.m_packet.m_Body.getTLVByTag(TagName.Bug_Date).m_bValueBuffer);
			//bugDate = tlvStrut.toDate();
			int status = CommonInfo.BugList_Insert(userbyID,bugSubject,bugContext,gameID,bugType);
			if(status==1)
			{
				SqlHelper.log.WriteLog(LanguageAPI.ServerSocket_ServerSocket_GMTools_Title+" bug "+bugSubject + LanguageAPI.API_Submit + LanguageAPI.API_Success);
				Console.WriteLine(DateTime.Now+" - gmtools bug "+bugSubject + LanguageAPI.API_Submit + LanguageAPI.API_Success);
				return Message.COMMON_MES_RESP(LanguageAPI.ServerSocket_ServerSocket_GMTools_Title +" bug "+LanguageAPI.API_Submit + LanguageAPI.API_Success,Msg_Category.COMMON,ServiceKey.GMTOOLS_BUGLIST_UPDATE_RESP);
			}
			else
			{
				SqlHelper.log.WriteLog(LanguageAPI.ServerSocket_ServerSocket_GMTools_Title+" bug "+bugSubject + LanguageAPI.API_Submit + LanguageAPI.API_Failure);
				Console.WriteLine(DateTime.Now+" - gmtools bug "+bugSubject + LanguageAPI.API_Submit + LanguageAPI.API_Failure);
				return Message.COMMON_MES_RESP(LanguageAPI.ServerSocket_ServerSocket_GMTools_Title +" bug "+LanguageAPI.API_Submit + LanguageAPI.API_Failure,Msg_Category.COMMON,ServiceKey.GMTOOLS_BUGLIST_UPDATE_RESP);

			}
		}
		/// <summary>
		/// bug处理
		/// </summary>
		/// <returns></returns>
		public Message Bug_Update()
		{
			int action = 0;
			int userbyID = 0;
			int bugID = 0;
			string bugSubject = "";
			string bugContext = "";
			int gameID = 0;;
			int bugType = 0;
			string result = "";
			TLV_Structure tlvStrut = new TLV_Structure(TagName.Status,4,message.m_packet.m_Body.getTLVByTag(TagName.Status).m_bValueBuffer);
			action = (int)tlvStrut.toInteger();
			tlvStrut = new TLV_Structure(TagName.UserByID,4,message.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
			userbyID = (int)tlvStrut.toInteger();
			tlvStrut = new TLV_Structure(TagName.Bug_ID,4,message.m_packet.m_Body.getTLVByTag(TagName.Bug_ID).m_bValueBuffer);
			bugID = (int)tlvStrut.toInteger();
			if(action==1)
			{
				result = System.Text.Encoding.Default.GetString(message.m_packet.m_Body.getTLVByTag(TagName.Bug_Result).m_bValueBuffer);
			}
			else
			{
				bugSubject = System.Text.Encoding.Default.GetString(message.m_packet.m_Body.getTLVByTag(TagName.Bug_Subject).m_bValueBuffer);
				bugContext = System.Text.Encoding.Default.GetString(message.m_packet.m_Body.getTLVByTag(TagName.Bug_Context).m_bValueBuffer);
				tlvStrut = new TLV_Structure(TagName.GameID,4,message.m_packet.m_Body.getTLVByTag(TagName.GameID).m_bValueBuffer);
				gameID = (int)tlvStrut.toInteger();	
				tlvStrut = new TLV_Structure(TagName.Bug_Type,4,message.m_packet.m_Body.getTLVByTag(TagName.Bug_Type).m_bValueBuffer);
				bugType = (int)tlvStrut.toInteger();

			}
			int status = CommonInfo.BugList_Update(userbyID,bugID,gameID,bugType,bugSubject,bugContext,result);
			if(status==1)
			{
				SqlHelper.log.WriteLog(LanguageAPI.ServerSocket_ServerSocket_GMTools_Title+" bugID "+bugID + LanguageAPI.API_Handle + LanguageAPI.API_Success);
				Console.WriteLine(DateTime.Now+" - "+LanguageAPI.ServerSocket_ServerSocket_GMTools_Title+" bug ID"+bugID + LanguageAPI.API_Handle + LanguageAPI.API_Success);
				return Message.COMMON_MES_RESP(LanguageAPI.ServerSocket_ServerSocket_GMTools_Title +" bug "+LanguageAPI.API_Handle + LanguageAPI.API_Success,Msg_Category.COMMON,ServiceKey.GMTOOLS_BUGLIST_UPDATE_RESP);
			}
			else
			{
				SqlHelper.log.WriteLog(LanguageAPI.ServerSocket_ServerSocket_GMTools_Title+" bugID "+bugID + LanguageAPI.API_Handle + LanguageAPI.API_Failure);
				Console.WriteLine(DateTime.Now+" - "+LanguageAPI.ServerSocket_ServerSocket_GMTools_Title+" bug ID"+bugID + LanguageAPI.API_Handle + LanguageAPI.API_Failure);
				return Message.COMMON_MES_RESP(LanguageAPI.ServerSocket_ServerSocket_GMTools_Title +" bug "+LanguageAPI.API_Handle + LanguageAPI.API_Failure,Msg_Category.COMMON,ServiceKey.GMTOOLS_BUGLIST_UPDATE_RESP);

			}
		}
		public int UserByID 
		{
			get
			{
				return this.userByID;
			}
			set
			{
				this.userByID = value;
			}
		}
		public string UserName
		{
			get
			{
				return this.userName;
			}
			set
			{
				this.userName =value;
			}
		}
		public string PassWord
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password =value;
			}
		}
		public DateTime ConnTime
		{
			get
			{
				return this.connTime;
			}
			set
			{
				this.connTime = value;
			}
		}
		public string Msg
		{
			get
			{
				return this.msg;
			}
			set
			{
				this.msg = value;
			}
		}
		public string MAC
		{
			get
			{
				return this.mac;
			}
			set
			{
				this.mac = value;
			}
		}
	}
}
