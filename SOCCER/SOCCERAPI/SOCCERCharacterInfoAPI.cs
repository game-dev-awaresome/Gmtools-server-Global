/*
 * Add by KeHuaQing 
 * 2006-09-14
 */
using System;
using System.Text;
using Common.Logic;
using Common.DataInfo;
using Soccer.SOCCERDataInfo;
using lg = Common.API.LanguageAPI;
namespace Soccer.SOCCERAPI
{
	/// <summary>
	/// SOCCERCharacterInfoAPI 的摘要说明。
	/// </summary>
	public class SOCCERCharacterInfoAPI
	{
		Message msg = null;
		public SOCCERCharacterInfoAPI(byte[] packet)
		{
			msg = new Message(packet, (uint)packet.Length);
		}

		/// <summary>
		/// 查询用户信息
		/// </summary>
		/// <returns></returns>
		public Message Soccer_Characterinfo_Query()
		{
			System.Data.DataSet ds = null;
			string serverIP = null;
			string str_type = "";
			string str_string = "";
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_ServerIP).m_bValueBuffer);
				str_type = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_Type).m_bValueBuffer);
				str_string = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_String).m_bValueBuffer);

				SqlHelper.log.WriteLog(lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.Soccer_CharacterInfoAPI_Account + str_string +lg.Soccer_CharacterInfoAPI_AccountInfo);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.Soccer_CharacterInfoAPI_Account + str_string + lg.Soccer_CharacterInfoAPI_AccountInfo);
				ds = SOCCERCharacterInfo.Soccer_Characterinfo_Query(serverIP,str_type,str_string);
				if (ds !=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for (int i = 0;i < ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[0]));
						strut.AddTagKey(TagName.Soccer_loginId, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[1]));
						strut.AddTagKey(TagName.Soccer_charidx, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[2]));
						strut.AddTagKey(TagName.Soccer_charname, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[3]));
						strut.AddTagKey(TagName.Soccer_charsex, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]));
						strut.AddTagKey(TagName.Soccer_charpos, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[5]));
						strut.AddTagKey(TagName.Soccer_charpoint, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[6]));
						strut.AddTagKey(TagName.Soccer_charlevel, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[7]));
						strut.AddTagKey(TagName.Soccer_charexp, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[8]));
						strut.AddTagKey(TagName.Soccer_match, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[9]));
						strut.AddTagKey(TagName.Soccer_win, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[10]));
						strut.AddTagKey(TagName.Soccer_lose, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[11]));
						strut.AddTagKey(TagName.Soccer_drop, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[12]));
						strut.AddTagKey(TagName.Soccer_draw, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_CHARACTERINFO_QUERY_RESP, 13);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.Soccer_CharacterInfoAPI_NoAccount, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_CHARACTERINFO_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
			}
			catch (System.Exception ex)
			{
				 return Message.COMMON_MES_RESP(ex.Message, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_CHARACTERINFO_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}

		/// <summary>
		/// 删除用户状态查询
		/// </summary>
		/// <param name="index">第几页</param>
		/// <param name="pageSize">每页记录数</param>
		/// <returns></returns>
		public Message Soccer_DeletedCharacterinfo_Query(int index, int pageSize)
		{
			System.Data.DataSet ds = null;
			string serverIP = null;
			string str_type = "";
			string str_string = "";
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_ServerIP).m_bValueBuffer);
				str_type = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_Type).m_bValueBuffer);
				str_string = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_String).m_bValueBuffer);

				SqlHelper.log.WriteLog(lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.Soccer_CharacterInfoAPI_Account + str_string + lg.Soccer_CharacterInfoAPI_AccountInfo);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.Soccer_CharacterInfoAPI_Account + str_string + lg.Soccer_CharacterInfoAPI_AccountInfo);
				ds = SOCCERCharacterInfo.Soccer_DeletedCharacterinfo_Query(serverIP,str_type,str_string);
				if (ds !=null && ds.Tables[0].Rows.Count>0)
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
					for (int i=index;i<index+pageSize;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length + 1);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[0]));
						strut.AddTagKey(TagName.Soccer_loginId, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[1]));
						strut.AddTagKey(TagName.Soccer_charidx, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[2]));
						strut.AddTagKey(TagName.Soccer_charname, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[3]));
						strut.AddTagKey(TagName.Soccer_charsex, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]));
						strut.AddTagKey(TagName.Soccer_charpos, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[5]));
						strut.AddTagKey(TagName.Soccer_charpoint, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[6]));
						strut.AddTagKey(TagName.Soccer_charlevel, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[7]));
						strut.AddTagKey(TagName.Soccer_charexp, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[8]));
						strut.AddTagKey(TagName.Soccer_match, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[9]));
						strut.AddTagKey(TagName.Soccer_win, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[10]));
						strut.AddTagKey(TagName.Soccer_lose, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[11]));
						strut.AddTagKey(TagName.Soccer_drop, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[12]));
						strut.AddTagKey(TagName.Soccer_draw, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[13]));
						strut.AddTagKey(TagName.Soccer_deleted_date, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[14]));
						strut.AddTagKey(TagName.Soccer_status, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
						structList[i - index] = strut;

					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_DELETEDCHARACTERINFO_QUERY_RESP, 16);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.Soccer_CharacterInfoAPI_NoBan, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_DELETEDCHARACTERINFO_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
			}
			catch (System.Exception ex)
			{
				return Message.COMMON_MES_RESP(ex.Message, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_DELETEDCHARACTERINFO_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 修改用户G币
		/// </summary>
		/// <returns></returns>
		public Message Soccer_Gamepoint_Modify()
		{
			int operateUserID = 0;
			int result = -1;
			string serverIP = "";
			int char_idx = 0;
			int point = 0;
			string admid = "";
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_ServerIP).m_bValueBuffer);
				strut = new TLV_Structure(TagName.Soccer_charidx, 4, msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_charidx).m_bValueBuffer);
				char_idx = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.Soccer_charpoint, 4, msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_charpoint).m_bValueBuffer);
				point = (int)strut.toInteger();
				admid = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_admid).m_bValueBuffer);

				result = SOCCERCharacterInfo.Gamepoint_Modify(operateUserID,serverIP,char_idx,point,admid);
				if (result == 0)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.Soccer_CharacterInfoAPI_Account + char_idx + lg.API_Update + lg.Soccer_CharacterInfoAPI_GCash + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.Soccer_CharacterInfoAPI_Account +  char_idx + lg.API_Update + lg.Soccer_CharacterInfoAPI_GCash + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_CHARPOINT_QUERY_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)  + lg.Soccer_CharacterInfoAPI_Account +  char_idx + lg.API_Update + lg.Soccer_CharacterInfoAPI_GCash + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.Soccer_CharacterInfoAPI_Account + char_idx +lg.API_Update + lg.Soccer_CharacterInfoAPI_GCash + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_CHARPOINT_QUERY_RESP);
				}
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_CHARPOINT_QUERY_RESP);
			}
		}

		/// <summary>
		/// 查询停封玩家信息
		/// </summary>
		/// <param name="index">第几页</param>
		/// <param name="pageSize">每页记录数</param>
		/// <returns></returns>
		public Message Soccer_AccountState_Query(int index, int pageSize)
		{
			System.Data.DataSet ds = null;
			string serverIP = null;
			string str_type = "";
			string str_string = "";
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_ServerIP).m_bValueBuffer);
				str_type = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_Type).m_bValueBuffer);
				str_string = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_String).m_bValueBuffer);

				SqlHelper.log.WriteLog(lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.Soccer_CharacterInfoAPI_Account +  str_string + lg.Soccer_CharacterInfoAPI_BanInfo);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)  + lg.Soccer_CharacterInfoAPI_Account + str_string + lg.Soccer_CharacterInfoAPI_BanInfo);
				
				ds = SOCCERCharacterInfo.Soccer_AccountState_Query(serverIP,str_type,str_string);
				if (ds !=null && ds.Tables[0].Rows.Count>0)
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
					for (int i=index;i<index+pageSize;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length + 1);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[0]));
						strut.AddTagKey(TagName.Soccer_loginId, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[1]));
						strut.AddTagKey(TagName.Soccer_regDate, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[2]));
						strut.AddTagKey(TagName.Soccer_m_id, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
						structList[i - index] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_ACCOUNTSTATE_QUERY_RESP, 4);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.Soccer_CharacterInfoAPI_NoBan, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_ACCOUNTSTATE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
			}
			catch (System.Exception ex)
			{
				return Message.COMMON_MES_RESP(ex.Message, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_ACCOUNTSTATE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}

		/// <summary>
		/// 查询停封角色信息
		/// </summary>
		/// <param name="index">第几页</param>
		/// <param name="pageSize">每页记录数</param>
		/// <returns></returns>
		public Message Soccer_CharacterState_Query(int index, int pageSize)
		{
			System.Data.DataSet ds = null;
			string serverIP = null;
			string str_type = "";
			string str_string = "";
			string str_ddate = "";
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_ServerIP).m_bValueBuffer);
				str_ddate = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_deleted_date).m_bValueBuffer);
				str_type = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_Type).m_bValueBuffer);
				str_string = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_String).m_bValueBuffer);

				SqlHelper.log.WriteLog(lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.Soccer_CharacterInfoAPI_Account +  str_string + lg.Soccer_CharacterInfoAPI_AccountInfo);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)  + lg.Soccer_CharacterInfoAPI_Account + str_string + lg.Soccer_CharacterInfoAPI_AccountInfo);
				ds = SOCCERCharacterInfo.Soccer_CharacterState_Query(serverIP,str_ddate,str_type,str_string);
				if (ds !=null && ds.Tables[0].Rows.Count>0)
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
					for (int i=index;i<index+pageSize;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length + 1);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[0]));
						strut.AddTagKey(TagName.Soccer_loginId, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[1]));
						strut.AddTagKey(TagName.Soccer_charidx, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[2]));
						strut.AddTagKey(TagName.Soccer_charname, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[3]));
						strut.AddTagKey(TagName.Soccer_charsex, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[4]));
						strut.AddTagKey(TagName.Soccer_c_date, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[5]));
						strut.AddTagKey(TagName.Soccer_deleted_date, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
						structList[i - index] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_CHARACTERSTATE_QUERY_RESP, 7);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.Soccer_CharacterInfoAPI_NoBan, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_CHARACTERSTATE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
			}
			catch (System.Exception ex)
			{
				return Message.COMMON_MES_RESP(ex.Message, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_CHARACTERSTATE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}

		/// <summary>
		/// 修改玩家停封信息
		/// </summary>
		/// <returns></returns>
		public Message AccountState_Modify()
		{
			int operateUserID = 0;
			int result = -1;
			string serverIP = "";
			string loginid = "";
			int m_id = 0;
			int m_auth = 0;

			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_ServerIP).m_bValueBuffer);
				loginid = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_loginId).m_bValueBuffer);
				strut = new TLV_Structure(TagName.Soccer_m_id, 4, msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_m_id).m_bValueBuffer);
				m_id = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.Soccer_m_auth, 4, msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_m_auth).m_bValueBuffer);
				m_auth = (int)strut.toInteger();
				

				result = SOCCERCharacterInfo.AccountState_Modify(operateUserID,serverIP,loginid,m_id,m_auth);
				if (result == 0)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.Soccer_CharacterInfoAPI_Account +  loginid + lg.API_Update + lg.Soccer_CharacterInfoAPI_BanInfo + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)  + lg.Soccer_CharacterInfoAPI_Account +  loginid + lg.API_Update + lg.Soccer_CharacterInfoAPI_BanInfo + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_ACCOUNTSTATE_MODIFY_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)  + lg.Soccer_CharacterInfoAPI_Account +  loginid + lg.API_Update + lg.Soccer_CharacterInfoAPI_BanInfo + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.Soccer_CharacterInfoAPI_Account +  loginid + lg.API_Update + lg.Soccer_CharacterInfoAPI_BanInfo + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_ACCOUNTSTATE_MODIFY_RESP);
				}
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_ACCOUNTSTATE_MODIFY_RESP);
			}
		}

		/// <summary>
		/// 修改角色停封信息
		/// </summary>
		/// <returns></returns>
		public Message CharacterState_Modify()
		{
			int operateUserID = 0;
			int result = -1;
			string serverIP = "";
			int char_idx = 0;
			string str_ddate = "";

			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_ServerIP).m_bValueBuffer);
				str_ddate = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_deleted_date).m_bValueBuffer);
				strut = new TLV_Structure(TagName.Soccer_charidx, 4, msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_charidx).m_bValueBuffer);
				char_idx = (int)strut.toInteger();
				

				result = SOCCERCharacterInfo.CharacterState_Modify(operateUserID,serverIP,char_idx,str_ddate);
				if (result == 0)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.Soccer_CharacterInfoAPI_Character + char_idx + lg.API_Update + lg.Soccer_CharacterInfoAPI_BanInfo + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.Soccer_CharacterInfoAPI_Character + char_idx + lg.API_Update + lg.Soccer_CharacterInfoAPI_BanInfo + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_CHARACTERSTATE_MODIFY_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.Soccer_CharacterInfoAPI_Character + char_idx + lg.API_Update + lg.Soccer_CharacterInfoAPI_BanInfo + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.Soccer_CharacterInfoAPI_Character + char_idx + lg.API_Update + lg.Soccer_CharacterInfoAPI_BanInfo + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_CHARACTERSTATE_MODIFY_RESP);
				}
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_CHARACTERSTATE_MODIFY_RESP);
			}
		}

		/// <summary>
		/// 检查角色信息
		/// </summary>
		/// <returns></returns>
		public Message Soccer_CharCheck()
		{
			System.Data.DataSet ds = null;
			string serverIP = null;
			int char_idx = 0;
			string kind = "";
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_ServerIP).m_bValueBuffer);
				TLV_Structure struts = new TLV_Structure(TagName.Soccer_charidx, 4, msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_charidx).m_bValueBuffer);
				char_idx = (int)struts.toInteger();
				kind = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_kind).m_bValueBuffer);

				SqlHelper.log.WriteLog(lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP +  CommonInfo.serverIP_Query(serverIP) + lg.Soccer_CharacterInfoAPI_Account + char_idx + lg.Soccer_CharacterInfoAPI_AccountInfo);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.Soccer_CharacterInfoAPI_Account + char_idx + lg.Soccer_CharacterInfoAPI_AccountInfo);
				ds = SOCCERCharacterInfo.Soccer_CharCheck(serverIP,char_idx,kind);
				if (kind == "socket")
				{
					if (ds !=null && ds.Tables[0].Rows.Count>0)
					{
						Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
						for (int i = 0;i < ds.Tables[0].Rows.Count;i++)
						{

							Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
							byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[1]));
							strut.AddTagKey(TagName.Soccer_char_max, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[2]));
							strut.AddTagKey(TagName.Soccer_char_cnt, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]));
							strut.AddTagKey(TagName.Soccer_ret, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);						
							structList[i] = strut;
						}
						return Message.COMMON_MES_RESP(structList, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_CHARCHECK_QUERY_RESP, 3);
					}
					else
					{
						return Message.COMMON_MES_RESP(lg.Soccer_CharacterInfoAPI_NoBan, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_CHARCHECK_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
					}
				}
				else
				{
					if (ds !=null && ds.Tables[1].Rows.Count>0)
					{
						Query_Structure[] structList = new Query_Structure[ds.Tables[1].Rows.Count];
						for (int i = 0;i < ds.Tables[1].Rows.Count;i++)
						{

							Query_Structure strut = new Query_Structure((uint)ds.Tables[1].Rows[i].ItemArray.Length);
							byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[1].Rows[i].ItemArray[1]));
							strut.AddTagKey(TagName.Soccer_char_max, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[1].Rows[i].ItemArray[2]));
							strut.AddTagKey(TagName.Soccer_char_cnt, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[1].Rows[i].ItemArray[3]));
							strut.AddTagKey(TagName.Soccer_ret, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);						
							structList[i] = strut;
						}
						return Message.COMMON_MES_RESP(structList, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_CHARCHECK_QUERY_RESP, 3);
					}
					else
					{
						return Message.COMMON_MES_RESP(lg.Soccer_CharacterInfoAPI_NoBan, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_CHARCHECK_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
					}
				}
			}
			catch (System.Exception ex)
			{
				return Message.COMMON_MES_RESP(ex.Message, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_CHARCHECK_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 恢复角色信息
		/// </summary>
		/// <returns></returns>
		public Message Soccer_CharItems_Recovery()
		{
			int operateUserID = 0;
			int result = -1;
			string serverIP = "";
			int char_idx = 0;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_ServerIP).m_bValueBuffer);
				strut = new TLV_Structure(TagName.Soccer_charidx, 4, msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_charidx).m_bValueBuffer);
				char_idx = (int)strut.toInteger();
				

				result = SOCCERCharacterInfo.Soccer_CharItems_Recovery(operateUserID,serverIP,char_idx);
				if (result == 0)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)  + lg.Soccer_CharacterInfoAPI_Account +  char_idx + lg.Soccer_CharacterInfoAPI_Recovery + lg.Soccer_CharacterInfoAPI_Character + lg.API_Update + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.Soccer_CharacterInfoAPI_Account + char_idx + lg.Soccer_CharacterInfoAPI_Recovery + lg.Soccer_CharacterInfoAPI_Character + lg.API_Update + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_CHARITEMS_RECOVERY_QUERY_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)  + lg.Soccer_CharacterInfoAPI_Account +  char_idx + lg.Soccer_CharacterInfoAPI_Recovery + lg.Soccer_CharacterInfoAPI_Character + lg.API_Update + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.Soccer_Soccer + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)  + lg.Soccer_CharacterInfoAPI_Account +  char_idx + lg.Soccer_CharacterInfoAPI_Recovery + lg.Soccer_CharacterInfoAPI_Character + lg.API_Update + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_CHARITEMS_RECOVERY_QUERY_RESP);
				}
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_CHARITEMS_RECOVERY_QUERY_RESP);
			}
		}
	}
}
