using System;
using System.Data;
using System.Text;
using Audition.AUDataInfo;
using Common.Logic;
using Common.DataInfo;
using lg = Common.API.LanguageAPI;
namespace Audition.AUAPI
{
	/// <summary>
	/// AUItemShopAPI 的摘要说明。
	/// </summary>
	public class AUAvatarListAPI
	{
		Message msg = null;
		public AUAvatarListAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public AUAvatarListAPI(byte[] packet)
		{
			msg = new Message(packet,(uint)packet.Length);
			
		}
        /// <summary>
        /// 查看游戏里面所有道具
        /// </summary>
        /// <returns></returns>
        public Message AvatarList_QueryALL()
        {
            string serverIP = null;
            string item_Name = null;
            int sex = 0;
            DataSet ds = null;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
                //道具名
                item_Name = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ItemName).m_bValueBuffer);

                SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.AU_AUAvatarListAPI_AllGameItem);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.AU_AUAvatarListAPI_AllGameItem);
                //请求所有分类道具列表
                ds = AUAvatarListInfo.AvatarList_QueryAll(serverIP, item_Name);
                if (ds!=null && ds.Tables[0].Rows.Count > 0)
                {
                    Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
                        //道具编号
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
                        strut.AddTagKey(TagName.AU_ItemID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        //道具名
                        object itemName;
                        if (ds.Tables[0].Rows[i].IsNull(1) == false)
                            itemName = ds.Tables[0].Rows[i].ItemArray[1];
                        else
                            itemName = "";
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, itemName);
                        strut.AddTagKey(TagName.AU_ItemName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                      
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[2]));
                        strut.AddTagKey(TagName.AU_SexIndex, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]));
                        strut.AddTagKey(TagName.AU_Cash, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        structList[i] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.AU_ADMIN, ServiceKey.AU_ITEMSHOP_QUERY_RESP, 4);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.AU_AUAvatarListAPI_NoGameItem, Msg_Category.AU_ADMIN, ServiceKey.AU_ITEMSHOP_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

                }

            }
            catch (System.Exception ex)
            {
                return Message.COMMON_MES_RESP(lg.AU_AUAvatarListAPI_NoGameItem, Msg_Category.AU_ADMIN, ServiceKey.AU_ITEMSHOP_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }
        }
		/// <summary>
		/// 查看玩家身上道具
		/// </summary>
		/// <returns></returns>
		public Message AvatarList_Owner_Query(int index,int pageSize)
		{
			string serverIP = null;
			int userIndexID = 0;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
                TLV_Structure tlvStrut = new TLV_Structure(TagName.AU_UserSN, 4, msg.m_packet.m_Body.getTLVByTag(TagName.AU_UserSN).m_bValueBuffer);
				userIndexID =(int)tlvStrut.toInteger();
				SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+userIndexID+"身上道具信息!");
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+userIndexID + "身上道具信息!");
				//请求玩家身上的道具
				ds = AUAvatarListInfo.AvatarList_Query(serverIP,userIndexID);	
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
                    for (int i = index; i < index + pageSize; i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length+1);
                        //道具分类
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0]);
                        strut.AddTagKey(TagName.AU_EquipState, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);	

                        //道具编号
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[1]));
						strut.AddTagKey(TagName.AU_ItemID,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						//道具名
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[2]);
                        strut.AddTagKey(TagName.AU_ItemName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);

						//使用者性别
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]));
                        strut.AddTagKey(TagName.AU_SexIndex, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]));
						strut.AddTagKey(TagName.AU_Cash,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
                        structList[i - index] = strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.AU_ADMIN,ServiceKey.AU_ITEMSHOP_BYOWNER_QUERY_RESP,6);
				}
				else
                    return Message.COMMON_MES_RESP("该玩家身上没有道具", Msg_Category.AU_ADMIN, ServiceKey.AU_ITEMSHOP_BYOWNER_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

			}
			catch(System.Exception ex)
			{
                Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP("该玩家身上没有道具", Msg_Category.AU_ADMIN, ServiceKey.AU_ITEMSHOP_BYOWNER_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
        /// <summary>
        /// 添加玩家礼物盒上的道具
        /// </summary>
        /// <returns></returns>
        public Message AvatarList_Insert()
        {
            int operateUserID = 0;
            string serverIP = null;
            int sendSN = 0;
            string sendNick = null;
            int itemCode = 0;
            int recvSN = 0;
            string recvNick = null;
            string demo = null;
            DateTime sendDate;
            int result = -1;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
                TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
                operateUserID = (int)strut.toInteger();
                strut = new TLV_Structure(TagName.AU_ItemID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.AU_ItemID).m_bValueBuffer);
                itemCode = (int)strut.toInteger();
                strut = new TLV_Structure(TagName.AU_SendSN, 4, msg.m_packet.m_Body.getTLVByTag(TagName.AU_SendSN).m_bValueBuffer);
                sendSN = (int)strut.toInteger();
                sendNick = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_SendNick).m_bValueBuffer);
                strut = new TLV_Structure(TagName.AU_RecvSN, 4, msg.m_packet.m_Body.getTLVByTag(TagName.AU_RecvSN).m_bValueBuffer);
                recvSN = (int)strut.toInteger();
                recvNick = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_RecvNick).m_bValueBuffer);
                strut = new TLV_Structure(TagName.AU_RecvDate, 3, msg.m_packet.m_Body.getTLVByTag(TagName.AU_RecvDate).m_bValueBuffer);
                sendDate = strut.toDate();
                demo = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_Demo).m_bValueBuffer);
                result = AUAvatarListInfo.AvatarList_Insert(operateUserID, serverIP, sendSN, sendNick, itemCode, recvSN, recvNick, sendDate,demo);
                if (result == 1)
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+sendSN + "的道具" + itemCode + "添加成功!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+sendSN + "礼物盒的道具" + itemCode + "添加成功!");
                    return Message.COMMON_MES_RESP("SUCESS", Msg_Category.AU_ADMIN, ServiceKey.AU_ITEMSHOP_CREATE_RESP);
                }
                else
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+sendSN + "的道具" + itemCode + "添加失败!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+sendSN + "的道具" + itemCode + "添加失败!");
                    return Message.COMMON_MES_RESP("FAILURE", Msg_Category.AU_ADMIN, ServiceKey.AU_ITEMSHOP_CREATE_RESP);
                }
            }
            catch (Common.Logic.Exception ex)
            {
                return Message.COMMON_MES_RESP(ex.Message, Msg_Category.AU_ADMIN, ServiceKey.AU_ITEMSHOP_CREATE_RESP);
            }
        }
        /// <summary>
        /// 添加玩家礼物盒上的道具
        /// </summary>
        /// <returns></returns>
        public Message AvatarList_BatchInsert()
        {
            int operateUserID = 0;
            string serverIP = null;
            string itemCode = null;
            string recvNick = null;
            string demo = null;
            int period = 0;
            int result = -1;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
                TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
                operateUserID = (int)strut.toInteger();
                itemCode = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ItemStyle).m_bValueBuffer);
              // recvSN = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_RecvSN).m_bValueBuffer);
               // recvNick = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_RecvNick).m_bValueBuffer);
                strut = new TLV_Structure(TagName.AU_Period, 4, msg.m_packet.m_Body.getTLVByTag(TagName.AU_Period).m_bValueBuffer);
                period = (int)strut.toInteger();
                demo = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_Reason).m_bValueBuffer);
                result = AUAvatarListInfo.AvatarList_BatchInsert(operateUserID, serverIP,itemCode,period, demo);
                if (result == 1)
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+recvNick +lg.AU_AUAvatarListAPI_GiftItem + itemCode + lg.API_Add + lg.API_Success + "!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+recvNick + lg.AU_AUAvatarListAPI_GiftItem + itemCode + lg.API_Add + lg.API_Success + "!");
                    return Message.COMMON_MES_RESP("SUCESS", Msg_Category.AU_ADMIN, ServiceKey.AU_ITEMSHOP_CREATE_RESP);
                }
                else
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+recvNick + lg.AU_AUAvatarListAPI_GiftItem + itemCode + lg.API_Add + lg.API_Failure + "!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+recvNick + lg.AU_AUAvatarListAPI_GiftItem + itemCode + lg.API_Add + lg.API_Failure + "!");
                    return Message.COMMON_MES_RESP("FAILURE", Msg_Category.AU_ADMIN, ServiceKey.AU_ITEMSHOP_CREATE_RESP);
                }
            }
            catch (System.Exception ex)
            {
                return Message.COMMON_MES_RESP(ex.Message, Msg_Category.AU_ADMIN, ServiceKey.AU_ITEMSHOP_CREATE_RESP);
            }
        }
		/// <summary>
		/// 删除玩家身上道具
		/// </summary>
		/// <returns></returns>
		public Message AvatarList_Delete()
		{
			int result = -1;
			int operateUserID = 0;
			int userIndexID = 0;
			string serverIP = null;
			int itemCode = 0 ;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
                strut = new TLV_Structure(TagName.AU_UserSN, 4, msg.m_packet.m_Body.getTLVByTag(TagName.AU_UserSN).m_bValueBuffer);
				userIndexID  =(int)strut.toInteger();
                strut = new TLV_Structure(TagName.AU_ItemID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.AU_ItemID).m_bValueBuffer);
				itemCode  =(int)strut.toInteger();
                result = AUAvatarListInfo.AvatarList_Delete(operateUserID, serverIP, userIndexID, itemCode);
				if(result==1)
				{
                    SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+userIndexID + lg.AU_AUAvatarListAPI_AllBodyItem + itemCode + lg.API_Delete + lg.API_Success + "!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+userIndexID + lg.AU_AUAvatarListAPI_AllBodyItem + itemCode + lg.API_Delete + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.AU_ADMIN,ServiceKey.AU_ITEMSHOP_DELETE_RESP);
				}
				else
				{
                    SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+userIndexID + lg.AU_AUAvatarListAPI_AllBodyItem + itemCode + lg.API_Delete + lg.API_Failure + "!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+userIndexID + lg.AU_AUAvatarListAPI_AllBodyItem + itemCode + lg.API_Delete + lg.API_Failure + "!");
                    return Message.COMMON_MES_RESP("FAILURE", Msg_Category.AU_ADMIN, ServiceKey.AU_ITEMSHOP_DELETE_RESP); 
				}
			}
			catch(System.Exception ex)
			{
                return Message.COMMON_MES_RESP("FAILURE", Msg_Category.AU_ADMIN, ServiceKey.AU_ITEMSHOP_DELETE_RESP);
			}

		}
		/// <summary>
		/// 查看玩家消费记录
		/// </summary>
		/// <returns></returns>
		public Message UserConsume_Query(int index,int pageSize)
		{
			string serverIP = null;
            string senderUserID = "";
            string sendUserNick = "";
			DateTime beginDate;
			DateTime endDate ;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
                senderUserID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_SendUserID).m_bValueBuffer);
                sendUserNick = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_SendNick).m_bValueBuffer);
                if (senderUserID.Length < 0)
                    senderUserID = sendUserNick;
                TLV_Structure tlvStrut = new TLV_Structure(TagName.AU_BeginTime, 3, msg.m_packet.m_Body.getTLVByTag(TagName.AU_BeginTime).m_bValueBuffer);
				beginDate  =tlvStrut.toDate();
                tlvStrut = new TLV_Structure(TagName.AU_EndTime, 3, msg.m_packet.m_Body.getTLVByTag(TagName.AU_EndTime).m_bValueBuffer);
				endDate  =tlvStrut.toDate();
                SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+senderUserID + lg.AU_AUAvatarListAPI_ConsumeRecord);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+senderUserID + lg.AU_AUAvatarListAPI_ConsumeRecord);
                ds = AUAvatarListInfo.userConsume_Query(serverIP, senderUserID, sendUserNick, beginDate, endDate);
                if (ds!=null && ds.Tables[0].Rows.Count > 0)
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
                        strut.AddTagKey(TagName.AU_SendSN, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[1]));
                        strut.AddTagKey(TagName.AU_RecvSN, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
                        strut.AddTagKey(TagName.AU_SendNick, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[3]);
                        strut.AddTagKey(TagName.AU_RecvNick, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[4]));
                        strut.AddTagKey(TagName.AU_SendDate, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[5]);
                        strut.AddTagKey(TagName.AU_ItemName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[6]));
                        strut.AddTagKey(TagName.AU_Cash, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
                        structList[i - index] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.AU_ADMIN, ServiceKey.AU_CONSUME_QUERY_RESP, 8);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.AU_AUAvatarListAPI_NoConsumeRecord, Msg_Category.AU_ADMIN, ServiceKey.AU_CONSUME_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }
			}
			catch(System.Exception ex)
			{
                return Message.COMMON_MES_RESP(lg.AU_AUAvatarListAPI_NoConsumeRecord, Msg_Category.AU_ADMIN, ServiceKey.AU_CONSUME_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
        /// <summary>
        /// 查看玩家消费记录合计
        /// </summary>
        /// <returns></returns>
        public Message UserConsume_QuerySum()
        {
            string serverIP = null;
            string senderUserID = "";
            string sendUserNick = "";
            DateTime beginDate;
            DateTime endDate;
            DataSet ds = null;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
                senderUserID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_SendUserID).m_bValueBuffer);
                sendUserNick = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_SendNick).m_bValueBuffer);
                if (senderUserID.Length < 0)
                    senderUserID = sendUserNick; TLV_Structure tlvStrut = new TLV_Structure(TagName.AU_BeginTime, 3, msg.m_packet.m_Body.getTLVByTag(TagName.AU_BeginTime).m_bValueBuffer);
                beginDate = tlvStrut.toDate();
                tlvStrut = new TLV_Structure(TagName.AU_EndTime, 3, msg.m_packet.m_Body.getTLVByTag(TagName.AU_EndTime).m_bValueBuffer);
                endDate = tlvStrut.toDate();
                SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+senderUserID + lg.AU_AUAvatarListAPI_SumConsumeRecord);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+senderUserID + lg.AU_AUAvatarListAPI_SumConsumeRecord);
                ds = AUAvatarListInfo.userConsume_QuerySum(serverIP, senderUserID,sendUserNick,beginDate, endDate);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return Message.COMMON_MES_RESP(Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]), Msg_Category.AU_ADMIN, ServiceKey.AU_USERCONSUMESUM_QUERY_RESP, TagName.AU_Cash, TagFormat.TLV_INTEGER);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.AU_AUAvatarListAPI_NoSumConsumeRecord, Msg_Category.AU_ADMIN, ServiceKey.AU_USERCONSUMESUM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }
            }
            catch (System.Exception ex)
            {
                return Message.COMMON_MES_RESP(lg.AU_AUAvatarListAPI_NoSumConsumeRecord, Msg_Category.AU_ADMIN, ServiceKey.AU_USERCONSUMESUM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }

        }
		/// <summary>
		/// 查看玩家交易记录
		/// </summary>
		/// <returns></returns>
		public Message UserTrade_Query(int index,int pageSize)
		{
			string serverIP = null;
			string senderUserID = "";
            string receiveserID = "";
            string sendUserNick = "";
            string recvUserNick = "";
            DateTime beginDate;
            DateTime endDate;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
                senderUserID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_SendUserID).m_bValueBuffer);
                receiveserID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_RecvUserID).m_bValueBuffer);
                if (senderUserID.Length < 0)
                    senderUserID = receiveserID;
                sendUserNick = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_SendNick).m_bValueBuffer);
                recvUserNick = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_RecvNick).m_bValueBuffer);
                TLV_Structure tlvStrut = new TLV_Structure(TagName.AU_BeginTime, 3, msg.m_packet.m_Body.getTLVByTag(TagName.AU_BeginTime).m_bValueBuffer);
                beginDate = tlvStrut.toDate();
                tlvStrut = new TLV_Structure(TagName.AU_EndTime, 3, msg.m_packet.m_Body.getTLVByTag(TagName.AU_EndTime).m_bValueBuffer);
                endDate = tlvStrut.toDate();
                SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+senderUserID + lg.AU_AUAvatarListAPI_TradeRecord);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+senderUserID + lg.AU_AUAvatarListAPI_TradeRecord);
                ds = AUAvatarListInfo.userTrade_Query(serverIP, senderUserID, receiveserID, sendUserNick, recvUserNick, beginDate, endDate);
                if (ds!=null && ds.Tables[0].Rows.Count > 0)
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
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
                        strut.AddTagKey(TagName.AU_SendSN, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[1]));
                        strut.AddTagKey(TagName.AU_RecvSN, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
                        strut.AddTagKey(TagName.AU_SendNick, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[3]);
                        strut.AddTagKey(TagName.AU_RecvNick, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[4]));
                        strut.AddTagKey(TagName.AU_SendDate, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[5]);
                        strut.AddTagKey(TagName.AU_ItemName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[6]));
                        strut.AddTagKey(TagName.AU_Cash, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
    
                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
                        structList[i - index] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.AU_ADMIN, ServiceKey.AU_ITEMSHOP_TRADE_QUERY_RESP, 8);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.AU_AUAvatarListAPI_NoTradeRecord, Msg_Category.AU_ADMIN, ServiceKey.AU_ITEMSHOP_TRADE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }
			}
			catch(System.Exception ex)
			{
                return Message.COMMON_MES_RESP(lg.AU_AUAvatarListAPI_NoTradeRecord, Msg_Category.AU_ADMIN, ServiceKey.AU_ITEMSHOP_TRADE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 查看玩家交易记录合计
		/// </summary>
		/// <returns></returns>
		public Message UserTrade_QuerySum()
		{
			string serverIP = null;
			string senderUserID = "";
			string receiveserID = "";
			string sendUserNick = "";
			string recvUserNick = "";
			DateTime beginDate;
			DateTime endDate;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
				senderUserID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_SendUserID).m_bValueBuffer);
				receiveserID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_RecvUserID).m_bValueBuffer);
				if (senderUserID.Length < 0)
					senderUserID = receiveserID;
				sendUserNick = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_SendNick).m_bValueBuffer);
				recvUserNick = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_RecvNick).m_bValueBuffer);
				TLV_Structure tlvStrut = new TLV_Structure(TagName.AU_BeginTime, 3, msg.m_packet.m_Body.getTLVByTag(TagName.AU_BeginTime).m_bValueBuffer);
				beginDate = tlvStrut.toDate();
				tlvStrut = new TLV_Structure(TagName.AU_EndTime, 3, msg.m_packet.m_Body.getTLVByTag(TagName.AU_EndTime).m_bValueBuffer);
				endDate = tlvStrut.toDate();
				SqlHelper.log.WriteLog(lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+senderUserID + lg.AU_AUAvatarListAPI_SumTradeRecord);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.AU_AU + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.AU_AUAvatarListAPI_Account+senderUserID + lg.AU_AUAvatarListAPI_SumTradeRecord);
				ds = AUAvatarListInfo.userTradeSum_Query(serverIP, senderUserID, receiveserID, sendUserNick, recvUserNick, beginDate, endDate);
				if (ds != null && ds.Tables[0].Rows.Count > 0)
				{
					return Message.COMMON_MES_RESP(Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]), Msg_Category.AU_ADMIN, ServiceKey.AU_USERCHARAGESUM_QUERY_RESP, TagName.AU_Cash, TagFormat.TLV_INTEGER);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.AU_AUAvatarListAPI_NoSumTradeRecord, Msg_Category.AU_ADMIN, ServiceKey.AU_USERCHARAGESUM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
			}
			catch (System.Exception ex)
			{
				return Message.COMMON_MES_RESP(lg.AU_AUAvatarListAPI_NoSumTradeRecord, Msg_Category.AU_ADMIN, ServiceKey.AU_USERCHARAGESUM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
	}
}
