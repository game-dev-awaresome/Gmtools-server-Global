using System;
using System.Text;
using Common.Logic;
using Common.DataInfo;
using CR.CRDataInfo;
using lg = Common.API.LanguageAPI;

namespace CR.CRAPI
{
    public class CRCallBoardAPI
    {
        Message msg = null;
        public CRCallBoardAPI(byte[] packet)
        {
            msg = new Message(packet, (uint)packet.Length);
        }
        /// <summary>
        /// 查询频道列表
        /// </summary>
        /// <param name="account"></param>
        /// <param name="serverIP"></param>
        /// <returns></returns>
        public Message  CR_Channel_QueryAll()
        {
            System.Data.DataSet ds = null;
            string serverIP = null;

            try
            {
                serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_ServerIP).m_bValueBuffer);
                SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_ChannelList);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_ChannelList);
                ds = CRCallBoardInfo.Channel_QueryAll(serverIP);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
                        strut.AddTagKey(TagName.CR_ChannelID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[1]);
                        strut.AddTagKey(TagName.CR_ChannelName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        structList[i] = strut;
                    }
                   return Message.COMMON_MES_RESP(structList, Msg_Category.CR_ADMIN, ServiceKey.CR_CHANNEL_QUERY_RESP,2);

                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.CR_CallBoardAPI_NoChannelList, Msg_Category.CR_ADMIN, ServiceKey.CR_CHANNEL_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }
            }
            catch (System.Exception ex)
            {
                return Message.COMMON_MES_RESP(lg.CR_CallBoardAPI_NoChannelList, Msg_Category.CR_ADMIN, ServiceKey.CR_CHANNEL_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }


        }
        public Message CR_CallBoard_Query(int index, int pageSize)
        {
            string serverIP = null;
            DateTime validTime;
            int publicID = 0;
            int valid = 0;
            int action = 0;
            System.Data.DataSet ds = null;
            try
            {
                serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_ServerIP).m_bValueBuffer);
                TLV_Structure tlv = new TLV_Structure(TagName.CR_ValidTime, 6, msg.m_packet.m_Body.getTLVByTag(TagName.CR_ValidTime).m_bValueBuffer);
                validTime = tlv.toTimeStamp();
                tlv = new TLV_Structure(TagName.CR_PublishID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CR_PublishID).m_bValueBuffer);
                publicID = (int)tlv.toInteger();
                //tlv = new TLV_Structure(TagName.CR_Valid, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CR_Valid).m_bValueBuffer);
                //valid = (int) tlv.toInteger();
                tlv = new TLV_Structure(TagName.CR_ACTION, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CR_ACTION).m_bValueBuffer);
                action = (int) tlv.toInteger();
                SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo);
                ds = CRCallBoardInfo.CallBoardInfo_Query(serverIP, validTime, publicID, valid, action);
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
                        Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length + 3);
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
                        strut.AddTagKey(TagName.CR_BoardID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        string context = "";
                        string context1 = "";
                        string context2 = "";
                        if (ds.Tables[0].Rows[i].IsNull(1)==false)
                        {
                            if ((ds.Tables[0].Rows[i].ItemArray[1].ToString().Length / 150)>2)
                            {
                                context = ds.Tables[0].Rows[i].ItemArray[1].ToString().Substring(0, 150);
                                context1 = ds.Tables[0].Rows[i].ItemArray[1].ToString().Substring(150,150);
                                context2 = ds.Tables[0].Rows[i].ItemArray[1].ToString().Substring(300, ds.Tables[0].Rows[i].ItemArray[1].ToString().Length - 300);
                            }
                            else if ((ds.Tables[0].Rows[i].ItemArray[1].ToString().Length / 150) > 1)
                            {
                                context = ds.Tables[0].Rows[i].ItemArray[1].ToString().Substring(0, 150);
                                context1 = ds.Tables[0].Rows[i].ItemArray[1].ToString().Substring(150, ds.Tables[0].Rows[i].ItemArray[1].ToString().Length - 150);
                            }
                            else
                            {
                                context = ds.Tables[0].Rows[i].ItemArray[1].ToString();

                            }
                        }
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, context);
                        strut.AddTagKey(TagName.CR_BoardContext, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, context1);
                        strut.AddTagKey(TagName.CR_BoardContext1, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, context2);
                        strut.AddTagKey(TagName.CR_BoardContext2, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[2]));
                        strut.AddTagKey(TagName.CR_BoardColor, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]));
                        strut.AddTagKey(TagName.CR_PublishID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]));
                        strut.AddTagKey(TagName.CR_DayLoop, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[5]));
                        strut.AddTagKey(TagName.CR_ValidTime, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[6]));
                        strut.AddTagKey(TagName.CR_InValidTime, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[7]));
                        strut.AddTagKey(TagName.CR_SPEED, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        //状态
                        int status = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[8]);
                        if (status == -1)
                            status = 0;
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, status);
                        strut.AddTagKey(TagName.CR_STATUS, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                         //生效格式
                        int expire = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[9]);
                        if (expire <= 0)
                            expire = 1;
                        else
                            expire = 0;
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, expire);
                        strut.AddTagKey(TagName.CR_Expire, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
                        structList[i - index] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.CR_ADMIN, ServiceKey.CR_CALLBOARD_QUERY_RESP, 13);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.CR_CallBoardAPI_NoNoticeInfo, Msg_Category.CR_ADMIN, ServiceKey.CR_CALLBOARD_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP(lg.CR_CallBoardAPI_NoNoticeInfo, Msg_Category.CR_ADMIN, ServiceKey.CR_CALLBOARD_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

            }
        }
        public Message CR_CallBoard_QueryAll(int index,int pageSize)
		{
			string serverIP = null;
			System.Data.DataSet ds = null;
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_ServerIP).m_bValueBuffer);
                SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo);
                ds = CRCallBoardInfo.CallBoardInfo_QueryAll(serverIP);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
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
					for(int i=index;i<index+pageSize;i++)
					{
                        Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length + 1);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
                        strut.AddTagKey(TagName.CR_BoardID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[1]));
						strut.AddTagKey(TagName.CR_BoardContext,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[2]));
                        strut.AddTagKey(TagName.CR_BoardColor, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]));
                        strut.AddTagKey(TagName.CR_PublishID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]));
                        strut.AddTagKey(TagName.CR_DayLoop, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP,Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[5]));
                        strut.AddTagKey(TagName.CR_ValidTime, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[6]));
                        strut.AddTagKey(TagName.CR_InValidTime, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[7]));
                        strut.AddTagKey(TagName.CR_SPEED, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        //状态
                        int status = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[8]);
                        if (status == -1)
                            status = 0;
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, status);
                        strut.AddTagKey(TagName.CR_STATUS, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

                        //生效格式
                        int expire = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[9]);
                        if (expire == -1)
                            expire = 0;
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, expire);
                        strut.AddTagKey(TagName.CR_Expire, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
                        structList[i-index]=strut;
					}
                    return Message.COMMON_MES_RESP(structList, Msg_Category.CR_ADMIN, ServiceKey.CR_CALLBOARD_QUERY_RESP, 11);
				}
				else
				{
                    return Message.COMMON_MES_RESP(lg.CR_CallBoardAPI_NoNoticeInfo, Msg_Category.CR_ADMIN, ServiceKey.CR_CALLBOARD_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch(Common.Logic.Exception ex)
			{
				Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP(lg.CR_CallBoardAPI_NoNoticeInfo, Msg_Category.CR_ADMIN, ServiceKey.CR_CALLBOARD_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

			} 
		}
        /// <summary>
        /// 发布公告表
        /// </summary>
        /// <returns></returns>
        public Message CR_CallBoardInfo_Insert()
        {
			string err_channels = null;
            int result = -1;
            int userByID = 0;
            int operID = 0;
            string serverIP = null;
            string callBoardContext = null;
            string callBoardColor = null;
            int status = 0;
            int license = 0;
            DateTime validTime;
            DateTime invalidTime;
            int speed = 0;
            int everyDay = 0;
            string channel = null;
            try
            {
                TLV_Structure tlv = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
                userByID = (int)tlv.toInteger();
                serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_ServerIP).m_bValueBuffer);
                tlv = new TLV_Structure(TagName.CR_PublishID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CR_PublishID).m_bValueBuffer);
                operID = (int)tlv.toInteger();
                callBoardContext = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_BoardContext).m_bValueBuffer);
                string callBoardContext1 = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_BoardContext1).m_bValueBuffer);
                string callBoardContext2 = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_BoardContext2).m_bValueBuffer);
                callBoardContext = callBoardContext + callBoardContext1 + callBoardContext2;
                callBoardColor = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_BoardColor).m_bValueBuffer);
                tlv = new TLV_Structure(TagName.CR_STATUS, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CR_STATUS).m_bValueBuffer);
                status= (int)tlv.toInteger();
                tlv = new TLV_Structure(TagName.CR_ValidTime,6, msg.m_packet.m_Body.getTLVByTag(TagName.CR_ValidTime).m_bValueBuffer);
                validTime = tlv.toTimeStamp();
                tlv = new TLV_Structure(TagName.CR_InValidTime,6, msg.m_packet.m_Body.getTLVByTag(TagName.CR_InValidTime).m_bValueBuffer);
                invalidTime = tlv.toTimeStamp();
                tlv = new TLV_Structure(TagName.CR_SPEED, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CR_SPEED).m_bValueBuffer);
                speed = (int)tlv.toInteger();
                tlv = new TLV_Structure(TagName.CR_DayLoop, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CR_DayLoop).m_bValueBuffer);
                everyDay = (int)tlv.toInteger();
                channel = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_Channel).m_bValueBuffer);
                err_channels = CRCallBoardInfo.CallBoardInfo_ADD(userByID, serverIP, callBoardContext, callBoardColor, status, validTime, invalidTime, operID, speed, everyDay, license, channel);
				string[] channels = err_channels.Split(',');
                if (channels.Length == 1)
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo + callBoardContext + lg.API_Add + lg.API_Success + "!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo + callBoardContext + lg.API_Add + lg.API_Success + "!");
                    return Message.COMMON_MES_RESP("SUCESS", Msg_Category.CR_ADMIN, ServiceKey.CR_CALLBOARD_CREATE_RESP);
                }
                else
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo + callBoardContext + lg.API_Add + lg.API_Failure + "!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo + callBoardContext + lg.API_Add + lg.API_Failure + "!");
                    return Message.COMMON_MES_RESP(err_channels, Msg_Category.CR_ADMIN, ServiceKey.CR_CALLBOARD_CREATE_RESP);
                }
            }
            catch (System.Exception ex)
            {
                SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo + callBoardContext + lg.API_Add + lg.API_Failure + "!");
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo + callBoardContext + lg.API_Add + lg.API_Failure + "!");
                return Message.COMMON_MES_RESP(ex.Message, Msg_Category.CR_ADMIN, ServiceKey.CR_CALLBOARD_CREATE_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
            }

        }
		/// <summary>
		/// 遗漏频道重新插入
		/// </summary>
		/// <returns></returns>
		public Message CR_ErrorChannel_Insert()
		{
			string err_channels = null;
			int userByID = 0;
			int BoardID = 0;
			string serverIP = null;
			string channel = null;
			try
			{
				TLV_Structure tlv = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userByID = (int)tlv.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_ServerIP).m_bValueBuffer);				
				tlv = new TLV_Structure(TagName.CR_BoardID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CR_BoardID).m_bValueBuffer);
				BoardID = (int)tlv.toInteger();
				channel = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_Channel).m_bValueBuffer);
				err_channels = CRCallBoardInfo.Channels_ADD(userByID, serverIP, BoardID, channel);
				string[] channels = err_channels.Split(',');
				if (channels.Length == 1)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.API_Add + lg.CR_CallBoardAPI_ChannelInfo + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.API_Add + lg.CR_CallBoardAPI_ChannelInfo + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.CR_ADMIN, ServiceKey.CR_ERRORCHANNEL_QUERY_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_ChannelInfo + err_channels + lg.API_Add + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_ChannelInfo + err_channels + lg.API_Add + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP(err_channels, Msg_Category.CR_ADMIN, ServiceKey.CR_ERRORCHANNEL_QUERY_RESP);
				}
			}
			catch (System.Exception ex)
			{
				SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_ChannelInfo + err_channels + lg.API_Add + lg.API_Failure + "!");
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_ChannelInfo + err_channels + lg.API_Add + lg.API_Failure + "!");
				return Message.COMMON_MES_RESP(ex.Message, Msg_Category.CR_ADMIN, ServiceKey.CR_ERRORCHANNEL_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
		}
        /// <summary>
        /// 更新公告表
        /// </summary>
        /// <returns></returns>
        public Message CR_CallBoardInfo_Update()
        {
            int result = -1;
            int userByID = 0;
            string serverIP = null;
            int boardID = 0;
            string callBoardContext = null;
            string callBoardColor = null;
            DateTime validTime;
            DateTime invalidTime;
            int everyDay = 0;
            try
            {
                TLV_Structure tlv = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
                userByID = (int)tlv.toInteger();
                serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_ServerIP).m_bValueBuffer);
                tlv = new TLV_Structure(TagName.CR_BoardID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CR_BoardID).m_bValueBuffer);
                boardID = (int)tlv.toInteger();
                callBoardContext = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_BoardContext).m_bValueBuffer);
                string callBoardContext1 = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_BoardContext1).m_bValueBuffer);
                string callBoardContext2 = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_BoardContext2).m_bValueBuffer);
                callBoardContext = callBoardContext + callBoardContext1 + callBoardContext2;
                callBoardColor = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_BoardColor).m_bValueBuffer);
                tlv = new TLV_Structure(TagName.CR_ValidTime, 6, msg.m_packet.m_Body.getTLVByTag(TagName.CR_ValidTime).m_bValueBuffer);
                validTime = tlv.toTimeStamp();
                tlv = new TLV_Structure(TagName.CR_InValidTime, 6, msg.m_packet.m_Body.getTLVByTag(TagName.CR_InValidTime).m_bValueBuffer);
                invalidTime = tlv.toTimeStamp();
                tlv = new TLV_Structure(TagName.CR_DayLoop, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CR_DayLoop).m_bValueBuffer);
                everyDay = (int)tlv.toInteger();
                result = CRCallBoardInfo.CallBoardInfo_Update(userByID, serverIP, boardID, callBoardContext, callBoardColor, validTime, invalidTime, everyDay);
                if (result == 1)
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo + callBoardContext + lg.API_Update + lg.API_Success + "!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo + callBoardContext + lg.API_Update + lg.API_Success + "!");
                    return Message.COMMON_MES_RESP("SUCESS", Msg_Category.CR_ADMIN, ServiceKey.CR_CALLBOARD_UPDATE_RESP);
                }
                else
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo + callBoardContext + lg.API_Update + lg.API_Failure + "!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo + callBoardContext + lg.API_Update + lg.API_Failure + "!");
                    return Message.COMMON_MES_RESP("FAILURE", Msg_Category.CR_ADMIN, ServiceKey.CR_CALLBOARD_UPDATE_RESP);
                }
            }
            catch (System.Exception ex)
            {
                SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo + callBoardContext + lg.API_Update + lg.API_Failure + "!");
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo + callBoardContext + lg.API_Update + lg.API_Failure + "!");
                return Message.COMMON_MES_RESP(ex.Message, Msg_Category.CR_ADMIN, ServiceKey.CR_CALLBOARD_UPDATE_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
            }
        }
        /// <summary>
        /// 删除公告表
        /// </summary>
        /// <returns></returns>
        public Message CR_CallBoardInfo_Delete()
        {
            int result = -1;
            int userByID = 0;
            string serverIP = null;
            int boardId = 0;
            try
            {
                TLV_Structure tlv = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
                userByID = (int)tlv.toInteger();
                serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_ServerIP).m_bValueBuffer);
                tlv = new TLV_Structure(TagName.CR_BoardID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CR_BoardID).m_bValueBuffer);
                boardId = (int)tlv.toInteger();
                result = CRCallBoardInfo.CallBoardInfo_Delete(userByID, serverIP, boardId);
                if (result == 1)
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo + boardId + lg.API_Delete + lg.API_Success + "!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo + boardId + lg.API_Delete + lg.API_Success + "!");
                    return Message.COMMON_MES_RESP("SUCESS", Msg_Category.CR_ADMIN, ServiceKey.CR_CALLBOARD_DELETE_RESP);
                }
                else
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo + boardId + lg.API_Delete + lg.API_Failure + "!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo + boardId + lg.API_Delete + lg.API_Failure + "!");
                    return Message.COMMON_MES_RESP("FAILURE", Msg_Category.CR_ADMIN, ServiceKey.CR_CALLBOARD_DELETE_RESP);
                }
            }
            catch (System.Exception ex)
            {
                SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo + boardId + lg.API_Delete + lg.API_Failure + "!");
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_CallBoardAPI_AllNoticeInfo + boardId + lg.API_Delete + lg.API_Failure + "!");
                return Message.COMMON_MES_RESP(ex.Message, Msg_Category.CR_ADMIN, ServiceKey.CR_CALLBOARD_DELETE_RESP);
            }

        }
    }
}
