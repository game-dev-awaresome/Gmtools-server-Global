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
	/// SDOItemShopAPI 的摘要说明。
	/// </summary>
	public class ItemShopAPI
	{
		Message msg = null;
		public ItemShopAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public ItemShopAPI(byte[] packet)
		{
			msg = new Message(packet,(uint)packet.Length);
			
		}
        /// <summary>
        /// 查看游戏里面所有道具
        /// </summary>
        /// <returns></returns>
        public Message itemShop_QueryALL()
        {
            string serverIP = null;
            string itemName1 = null;
			string userID = null;
            DataSet ds = null;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ServerIP).m_bValueBuffer);
                itemName1 = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ItemName).m_bValueBuffer);
				userID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserID).m_bValueBuffer);
                SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.O2JAM2API_ItemShopAPI_AllGameItem);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.O2JAM2API_ItemShopAPI_AllGameItem);
                //请求所有分类道具列表
                ds = ItemShopInfo.itemShop_QueryAll(serverIP, itemName1, userID);
                if (ds!=null && ds.Tables[0].Rows.Count > 0)
                {
                    Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
                        //道具编号
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[0]);
                        strut.AddTagKey(TagName.O2JAM2_ItemCode, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        //道具名
                        object itemName;
                        if (ds.Tables[0].Rows[i].IsNull(1) == false)
                            itemName = ds.Tables[0].Rows[i].ItemArray[1];
                        else
                            itemName = "";
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, itemName);
                        strut.AddTagKey(TagName.O2JAM2_ItemName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						//道具名
						object descrpt;
						if (ds.Tables[0].Rows[i].IsNull(2) == false)
							descrpt = ds.Tables[0].Rows[i].ItemArray[2];
						else
							descrpt = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, descrpt);
						strut.AddTagKey(TagName.O2JAM2_Context, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        //道具使用次数
                        int timelimits = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]);
                        if (timelimits == -1)
                            timelimits = 0;
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, timelimits);
                        strut.AddTagKey(TagName.O2JAM2_Timeslimt, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        //使用时限
                        //道具使用次数
                        int dayslimits = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]);
                        if (dayslimits == -1)
                            dayslimits = 0;
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, dayslimits);
                        strut.AddTagKey(TagName.O2JAM2_DayLimit, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        structList[i] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_ITEMSHOP_QUERY_RESP, 5);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.O2JAM2API_ItemShopAPI_NoGameItem, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_ITEMSHOP_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

                }

            }
            catch (System.Exception ex)
            {
                return Message.COMMON_MES_RESP(lg.O2JAM2API_AccountInfoAPI_NoAccount, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_ITEMSHOP_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }
        }
		/// <summary>
		/// 查看玩家身上道具
		/// </summary>
		/// <returns></returns>
		public Message itemShop_Owner_Query()
		{
			string serverIP = null;
			string userID = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ServerIP).m_bValueBuffer);
				userID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserID).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+userID+lg.O2JAM2API_ItemShopAPI_AllBodyItem);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+userID+lg.O2JAM2API_ItemShopAPI_AllBodyItem);
				//请求玩家身上的道具
				ds = ItemShopInfo.AvatarItemList_Query(serverIP,userID);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						//道具编号
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.O2JAM2_ItemCode,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						//道具名
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[1]));
						strut.AddTagKey(TagName.O2JAM2_ItemName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						//最小等级
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.O2JAM2_Level,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						//道具位置
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[3]);
						strut.AddTagKey(TagName.O2JAM2_Position,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						//道具使用次数
                        int timelimits = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]);
                        if (timelimits == -1)
                            timelimits = 0;
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, timelimits);
						strut.AddTagKey(TagName.O2JAM2_Timeslimt,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						//道具时间限制
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP,ds.Tables[0].Rows[i].ItemArray[5]);
                        strut.AddTagKey(TagName.O2JAM2_DateLimit, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_AVATORLIST_QUERY_RESP,6);
				}
				else
					return Message.COMMON_MES_RESP(lg.O2JAM2API_ItemShopAPI_NoBodyItem,Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_AVATORLIST_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);

			}
			catch(System.Exception ex)
			{
                Console.WriteLine(ex.Message);
			   return Message.COMMON_MES_RESP(lg.O2JAM2API_AccountInfoAPI_NoAccount,Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_AVATORLIST_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 删除玩家身上道具
		/// </summary>
		/// <returns></returns>
		public Message ItemShop_Delete()
		{
			int result = -1;
			int operateUserID = 0;
			string userID = null;
			string serverIP = null;
			int itemCode = 0 ;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				userID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserID).m_bValueBuffer);
				strut = new TLV_Structure(TagName.O2JAM2_ItemCode,4,msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ItemCode).m_bValueBuffer);
				itemCode  =(int)strut.toInteger();
				result = ItemShopInfo.itemShop_Delete(operateUserID,serverIP,userID,itemCode);
				if(result==1)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+userID+lg.O2JAM2API_ItemShopAPI_AllBodyItem+itemCode+lg.API_Delete + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+userID+lg.O2JAM2API_ItemShopAPI_AllBodyItem+itemCode+lg.API_Delete + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_ITEMSHOP_DELETE_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+userID+lg.O2JAM2API_ItemShopAPI_AllBodyItem+itemCode+lg.API_Delete + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+userID+lg.O2JAM2API_ItemShopAPI_AllBodyItem+itemCode+lg.API_Delete + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_ITEMSHOP_DELETE_RESP); 
				}
			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP(lg.O2JAM2API_AccountInfoAPI_NoAccount,Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_ITEMSHOP_DELETE_RESP);
			}

		}
		/// <summary>
		/// 查看玩家礼物盒的道具
		/// </summary>
		/// <returns></returns>
		public Message giftBox_Query(int index,int pageSize)
		{
			string serverIP = null;
			string userID = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ServerIP).m_bValueBuffer);
				userID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserID).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+userID+lg.O2JAM2API_ItemShopAPI_GiftItem);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+userID+lg.O2JAM2API_ItemShopAPI_GiftItem);
				//请求所有分类道具列表
				ds = ItemShopInfo.O2JAM2GiftBox_Query(serverIP,userID);
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
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length+1);

						//道具编号
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.O2JAM2_ItemCode,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						//道具名
						object itemName;
						if(ds.Tables[0].Rows[i].IsNull(1)==false)
							itemName = ds.Tables[0].Rows[i].ItemArray[1];
						else
							itemName = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,itemName);
						strut.AddTagKey(TagName.O2JAM2_ItemName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
                    
						//使用次数
						int timeslimits = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[2]);
						if (timeslimits == -1)
							timeslimits = 0;
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, timeslimits);
						strut.AddTagKey(TagName.O2JAM2_DayLimit, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

						//使用期限
						int dayslimits = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]);
						if (dayslimits == -1)
							dayslimits = 0;
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, dayslimits);
						strut.AddTagKey(TagName.O2JAM2_DayLimit, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

						//主题
						object title;
						if(ds.Tables[0].Rows[i].IsNull(4)==false)
							title = ds.Tables[0].Rows[i].ItemArray[4];
						else
							title = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,title);
						strut.AddTagKey(TagName.O2JAM2_Title,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						//内容
						object context;
						if(ds.Tables[0].Rows[i].IsNull(5)==false)
							context = ds.Tables[0].Rows[i].ItemArray[5];
						else
							context = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,context);
						strut.AddTagKey(TagName.O2JAM2_Context,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	
						
						//赠送日期
						object sendDate;
						if(ds.Tables[0].Rows[i].IsNull(6)==false)
							sendDate = ds.Tables[0].Rows[i].ItemArray[6];
						else
							sendDate = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP,sendDate);
						strut.AddTagKey(TagName.SDO_SendTime,TagFormat.TLV_TIMESTAMP,(uint)bytes.Length,bytes);

                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
                        structList[i - index] = strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_MESSAGE_QUERY_RESP,8);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.O2JAM2API_ItemShopAPI_NoGiftItem,Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_MESSAGE_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
                return Message.COMMON_MES_RESP(lg.O2JAM2API_AccountInfoAPI_NoAccount, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_MESSAGE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 添加玩家礼物盒上的道具
		/// </summary>
		/// <returns></returns>
		public Message GiftBox_MessageItem_Add()
		{
			int operateUserID  = 0;
			string userID = null;
			string serverIP = null;
			int itemCode = 0;
			string title = null;
			string context = null;
			int timesLimit = 0;
			int dayLimit  =0;
			int result = -1;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.O2JAM2_ItemCode,4,msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ItemCode).m_bValueBuffer);
				itemCode  =(int)strut.toInteger();				
				userID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserID).m_bValueBuffer);
				strut = new TLV_Structure(TagName.O2JAM2_Timeslimt,4,msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_Timeslimt).m_bValueBuffer);
				timesLimit  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.O2JAM2_DayLimit,4,msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_DayLimit).m_bValueBuffer);
				dayLimit  =(int)strut.toInteger();
				title = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_Title).m_bValueBuffer);
				context = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_Context).m_bValueBuffer);
				result = ItemShopInfo.giftBox_Insert(operateUserID,serverIP,userID,itemCode,title,context,timesLimit,dayLimit);
				if(result==1)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+userID+lg.O2JAM2API_ItemShopAPI_GiftItem+itemCode+lg.API_Add + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+userID+lg.O2JAM2API_ItemShopAPI_GiftItem+itemCode+lg.API_Add + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_MESSAGE_CREATE_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+userID+lg.O2JAM2API_ItemShopAPI_GiftItem+itemCode+lg.API_Add + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+userID+lg.O2JAM2API_ItemShopAPI_GiftItem+itemCode+lg.API_Add + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_MESSAGE_CREATE_RESP);
				}
			}
			catch(System.Exception ex)
			{	
				return Message.COMMON_MES_RESP(lg.O2JAM2API_AccountInfoAPI_NoAccount,Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_MESSAGE_CREATE_RESP);
			}
		}
		/// <summary>
		/// 删除玩家礼物盒上道具
		/// </summary>
		/// <returns></returns>
		public Message GiftBox_MessageItem_Delete()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
		    string userID = null;
			int itemCode = 0 ;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				userID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserID).m_bValueBuffer);
				strut = new TLV_Structure(TagName.O2JAM2_ItemCode,4,msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ItemCode).m_bValueBuffer);
				itemCode  =(int)strut.toInteger();
				result = ItemShopInfo.giftBox_Delete(operateUserID,serverIP,userID,itemCode);
				if(result==1)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+userID+lg.O2JAM2API_ItemShopAPI_GiftItem+itemCode + lg.API_Delete + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+userID+lg.O2JAM2API_ItemShopAPI_GiftItem+itemCode + lg.API_Delete + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_ITEMSHOP_DELETE_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+userID+lg.O2JAM2API_ItemShopAPI_GiftItem+itemCode+lg.API_Delete + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+userID+lg.O2JAM2API_ItemShopAPI_GiftItem+itemCode+lg.API_Delete + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_ITEMSHOP_DELETE_RESP); 
				}
			}
			catch(System.Exception ex)
			{
			   return Message.COMMON_MES_RESP(lg.O2JAM2API_AccountInfoAPI_NoAccount,Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_ITEMSHOP_DELETE_RESP,TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 查看玩家上下线状态
		/// </summary>
		/// <returns></returns>
		public Message UserOnline_Query()
		{
			string serverIP = null;
			string account = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserID).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+lg.O2JAM2API_ItemShopAPI_OnlineStatus);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+lg.O2JAM2API_ItemShopAPI_OnlineStatus);
				ds = ItemShopInfo.userOnline_Query(serverIP,account);	
				if(ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.O2JAM2_UserIndexID,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.O2JAM2_UserID,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP,ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.SDO_LoginTime,TagFormat.TLV_TIMESTAMP,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP,ds.Tables[0].Rows[i].ItemArray[3]);
						strut.AddTagKey(TagName.SDO_LogoutTime,TagFormat.TLV_TIMESTAMP,(uint)bytes.Length,bytes);	
						structList[i]=strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.O2JAM2_ADMIN,ServiceKey.SDO_USERONLINE_QUERY_RESP,4);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.O2JAM2API_ItemShopAPI_NoOnlineStatus,Msg_Category.O2JAM2_ADMIN,ServiceKey.SDO_USERONLINE_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP(lg.O2JAM2API_ItemShopAPI_NoOnlineStatus, Msg_Category.O2JAM2_ADMIN, ServiceKey.SDO_USERONLINE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

			}

		}
		/// <summary>
		/// 查看玩家消费记录
		/// </summary>
		/// <returns></returns>
		public Message UserConsume_Query(int index,int pageSize)
		{
			string serverIP = null;
			string account = null;
			DateTime beginDate;
			DateTime endDate ;
			int moneyType=-1;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserID).m_bValueBuffer);
				TLV_Structure tlvStrut = new TLV_Structure(TagName.O2JAM2_BeginDate,3,msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_BeginDate).m_bValueBuffer);
				beginDate  =tlvStrut.toDate();
				tlvStrut = new TLV_Structure(TagName.O2JAM2_ENDDate,3,msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ENDDate).m_bValueBuffer);
				endDate  =tlvStrut.toDate();
				tlvStrut = new TLV_Structure(TagName.O2JAM2_ComsumeType,4,msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ComsumeType).m_bValueBuffer);
				moneyType  =(int)tlvStrut.toInteger();
				SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+lg.O2JAM2API_ItemShopAPI_ConsumeRecord);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.O2JAM2API_AccountInfoAPI_Account+account+lg.O2JAM2API_ItemShopAPI_ConsumeRecord);
				ds = ItemShopInfo.userConsume_Query(serverIP,account,moneyType,beginDate,endDate);
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
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[0]);
                        strut.AddTagKey(TagName.O2JAM2_ComsumeCode, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[1]);
                        strut.AddTagKey(TagName.O2JAM2_MoneyType, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[2]);
                        strut.AddTagKey(TagName.O2JAM2_MCash, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, ds.Tables[0].Rows[i].ItemArray[3]);
                        strut.AddTagKey(TagName.O2JAM2_BeginDate, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        //使用期限
						int daylimits = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]);
						if (daylimits == -1)
							daylimits = 0;
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, daylimits);
						strut.AddTagKey(TagName.O2JAM2_DayLimit, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						
						//使用次数
						int timeslimits = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[5]);
						if (timeslimits == -1)
							timeslimits = 0;
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, timeslimits);
						strut.AddTagKey(TagName.O2JAM2_Timeslimt, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[6]);
						strut.AddTagKey(TagName.O2JAM2_ItemName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);

                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
                        structList[i - index] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_CONUMSE_QUERY_RESP, 8);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.O2JAM2API_ItemShopAPI_NoConsumeRecord, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_CONUMSE_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
                }
			}
			catch(System.Exception ex)
			{
                return Message.COMMON_MES_RESP(ex.Message, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_CONUMSE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
        /// <summary>
        /// 查看玩家消费记录
        /// </summary>
        /// <returns></returns>
        public Message UserConsume_QuerySum()
        {
            string serverIP = null;
            string account = null;
            DateTime beginDate;
            DateTime endDate;
            int moneyType = -1;
            DataSet ds = null;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ServerIP).m_bValueBuffer);
                account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserID).m_bValueBuffer);
                TLV_Structure tlvStrut = new TLV_Structure(TagName.O2JAM2_BeginDate, 3, msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_BeginDate).m_bValueBuffer);
                beginDate = tlvStrut.toDate();
                tlvStrut = new TLV_Structure(TagName.O2JAM2_ENDDate, 3, msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ENDDate).m_bValueBuffer);
                endDate = tlvStrut.toDate();
                tlvStrut = new TLV_Structure(TagName.O2JAM2_ComsumeType, 4, msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ComsumeType).m_bValueBuffer);
                moneyType = (int)tlvStrut.toInteger();
                SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.O2JAM2API_AccountInfoAPI_Account + account + lg.O2JAM2API_ItemShopAPI_SumConsumeRecord);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.O2JAM2API_AccountInfoAPI_Account + account + lg.O2JAM2API_ItemShopAPI_SumConsumeRecord);
                ds = ItemShopInfo.userConsume_QuerySum(serverIP, account, moneyType, beginDate, endDate);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return Message.COMMON_MES_RESP(Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]), Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_CONUMSE_QUERY_SUM_RESP, TagName.SDO_ChargeSum, TagFormat.TLV_INTEGER);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.O2JAM2API_ItemShopAPI_NoSumConsumeRecord, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_CONUMSE_QUERY_SUM_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }
            }
            catch (System.Exception ex)
            {
                return Message.COMMON_MES_RESP(lg.O2JAM2API_AccountInfoAPI_NoAccount, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_CONUMSE_QUERY_SUM_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }

        }
		/// <summary>
		/// 查看玩家交易记录
		/// </summary>
		/// <returns></returns>
		public Message UserTrade_Query(int index,int pageSize)
		{
			string serverIP = null;
			string senderUserID = null;
            string receiveserID = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_ServerIP).m_bValueBuffer);
                senderUserID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserID).m_bValueBuffer);
                receiveserID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM2_UserNick).m_bValueBuffer);
                if (senderUserID.Length < 0)
                    senderUserID = receiveserID;
                SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.O2JAM2API_AccountInfoAPI_Account + senderUserID + lg.O2JAM2API_ItemShopAPI_TradeRecord);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM2API_BAF + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.O2JAM2API_AccountInfoAPI_Account + senderUserID + lg.O2JAM2API_ItemShopAPI_TradeRecord);
                ds = ItemShopInfo.userTrade_Query(serverIP, senderUserID, receiveserID);
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
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[0]);
                        strut.AddTagKey(TagName.SDO_ItemCode, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[1]);
                        strut.AddTagKey(TagName.SDO_ItemName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[2]);
                        strut.AddTagKey(TagName.SDO_SendIndexID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[3]);
                        strut.AddTagKey(TagName.SDO_SendUserID, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, ds.Tables[0].Rows[i].ItemArray[4]);
                        strut.AddTagKey(TagName.SDO_SendTime, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[5]);
                        strut.AddTagKey(TagName.SDO_ReceiveNick, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
    
                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
                        structList[i - index] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_TRADE_QUERY_RESP, 7);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.O2JAM2API_ItemShopAPI_NoTradeRecord, Msg_Category.O2JAM2_ADMIN, ServiceKey.O2JAM2_TRADE_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
                }
			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.O2JAM2_ADMIN,ServiceKey.O2JAM2_TRADE_QUERY_RESP,TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
	}
}
