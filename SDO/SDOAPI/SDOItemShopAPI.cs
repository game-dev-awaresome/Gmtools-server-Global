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
	/// SDOItemShopAPI 的摘要说明。
	/// </summary>
	public class SDOItemShopAPI
	{
		Message msg = null;
		public SDOItemShopAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public SDOItemShopAPI(byte[] packet)
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
            int bigType = 0;
            int smallType = 0;
            DataSet ds = null;
			string Name = null;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
                //道具大类
                TLV_Structure tlvStrut = new TLV_Structure(TagName.SDO_BigType, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_BigType).m_bValueBuffer);
                bigType = (int)tlvStrut.toInteger();

                //道具小类
                tlvStrut = new TLV_Structure(TagName.SDO_SmallType, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_SmallType).m_bValueBuffer);
                smallType = (int)tlvStrut.toInteger();
				Name = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ItemName).m_bValueBuffer);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDOItemShopAPI_GameItem + "!");
                //请求所有分类道具列表
                ds = ItemShopInfo.itemShop_QueryAll(serverIP, bigType, smallType,Name);
                if (ds!=null && ds.Tables[0].Rows.Count > 0)
                {
                    Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.SDO_BigType, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.SDO_SmallType, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						//道具编号
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.SDO_ItemCode, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						//道具名
						object itemName;
						if (ds.Tables[0].Rows[i].IsNull(3) == false)
							itemName = ds.Tables[0].Rows[i].ItemArray[3];
						else
							itemName = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, itemName);
						strut.AddTagKey(TagName.SDO_ItemName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						//道具使用次数
						int timelimits = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]);
						if (timelimits == -1)
							timelimits = 0;
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, timelimits);
						strut.AddTagKey(TagName.SDO_TimesLimit, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						//使用时限
						//道具使用次数
						int dayslimits = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[5]);
						if (dayslimits == -1)
							dayslimits = 0;
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, dayslimits);
						strut.AddTagKey(TagName.SDO_DaysLimit, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						structList[i] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.SDO_ADMIN, ServiceKey.SDO_ITEMSHOP_QUERY_RESP, 6);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.SDOAPI_SDOItemShopAPI_NoItem, Msg_Category.SDO_ADMIN, ServiceKey.SDO_ITEMSHOP_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

                }

            }
            catch (System.Exception ex)
            {
                return Message.COMMON_MES_RESP(ex.Message, Msg_Category.SDO_ADMIN, ServiceKey.SDO_ITEMSHOP_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }
        }
		/// <summary>
		/// 查看玩家身上道具
		/// </summary>
		/// <returns></returns>
		public Message itemShop_Owner_Query(int index,int pageSize)
		{
			string serverIP = null;
			int userIndexID = 0;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				TLV_Structure tlvStrut = new TLV_Structure(TagName.SDO_UserIndexID,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_UserIndexID).m_bValueBuffer);
				userIndexID =(int)tlvStrut.toInteger();
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.SDOAPI_SDOItemLogInfoAPI_Account + userIndexID + lg.SDOAPI_SDOItemShopAPI_PersonalItem + "!");
				//请求玩家身上的道具
				ds = ItemShopInfo.itemShop_Query(serverIP,userIndexID);	
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
						//道具编号
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.SDO_ItemCode,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						//道具名
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[1]));
						strut.AddTagKey(TagName.SDO_ItemName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						//最小等级
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.SDO_MinLevel,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						//道具位置
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[3]);
						strut.AddTagKey(TagName.SDO_Postion,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						//道具使用次数
                        int timelimits = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]);
                        if (timelimits == -1)
                            timelimits = 0;
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, timelimits);
						strut.AddTagKey(TagName.SDO_TimesLimit,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						//道具时间限制
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP,ds.Tables[0].Rows[i].ItemArray[5]);
                        strut.AddTagKey(TagName.SDO_DateLimit, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
                        structList[i - index] = strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_ITEMSHOP_BYOWNER_QUERY_RESP,7);
				}
				else
					return Message.COMMON_MES_RESP(lg.SDOAPI_SDOItemShopAPI_NoItemOnPlayer,Msg_Category.SDO_ADMIN,ServiceKey.SDO_ITEMSHOP_BYOWNER_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);

			}
			catch(Common.Logic.Exception ex)
			{
                Console.WriteLine(ex.Message);
			   return Message.COMMON_MES_RESP(lg.SDOAPI_SDOItemShopAPI_NoItemOnPlayer,Msg_Category.SDO_ADMIN,ServiceKey.SDO_ITEMSHOP_BYOWNER_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
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
			int userIndexID = 0;
			string serverIP = null;
			int itemCode = 0 ;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_UserIndexID,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_UserIndexID).m_bValueBuffer);
				userIndexID  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_ItemCode,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ItemCode).m_bValueBuffer);
				itemCode  =(int)strut.toInteger();
				result = ItemShopInfo.itemShop_Delete(operateUserID,serverIP,userIndexID,itemCode);
				if(result==1)
				{
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.API_Delete + lg.SDOAPI_SDOItemLogInfoAPI_Account+userIndexID+lg.SDOAPI_SDOItemShopAPI_PersonalItem+itemCode + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_ITEMSHOP_DELETE_RESP);
				}
				else
				{
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.API_Delete + lg.SDOAPI_SDOItemLogInfoAPI_Account+userIndexID+lg.SDOAPI_SDOItemShopAPI_PersonalItem+itemCode + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_ITEMSHOP_DELETE_RESP); 
				}
			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.SDO_ADMIN,ServiceKey.SDO_ITEMSHOP_DELETE_RESP);
			}

		}
		/// <summary>
		/// 查看玩家礼物盒的道具
		/// </summary>
		/// <returns></returns>
		public Message giftBox_Query(int index,int pageSize)
		{
			string serverIP = null;
			int userIndexID = 0;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				TLV_Structure tlvStrut = new TLV_Structure(TagName.SDO_UserIndexID,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_UserIndexID).m_bValueBuffer);
				userIndexID =(int)tlvStrut.toInteger();
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.SDOAPI_SDOItemLogInfoAPI_Account+userIndexID+lg.SDOAPI_SDOItemShopAPI_GiftItem + "!");
				//请求所有分类道具列表
				ds = ItemShopInfo.SDOGiftBox_Query(serverIP,userIndexID);
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
						//道具名
						object itemName;
						if(ds.Tables[0].Rows[i].IsNull(0)==false)
							itemName = ds.Tables[0].Rows[i].ItemArray[0];
						else
							itemName = "";
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,itemName);
						strut.AddTagKey(TagName.SDO_ItemName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);

						//大类
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.SDO_BigType,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						//小类
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.SDO_SmallType,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);                        
						//使用期限
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP,ds.Tables[0].Rows[i].ItemArray[3]);
						strut.AddTagKey(TagName.SDO_DateLimit,TagFormat.TLV_TIMESTAMP,(uint)bytes.Length,bytes);
						//使用次数
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[4]);
						strut.AddTagKey(TagName.SDO_TimesLimit,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);

						//主题
						object title;
						if(ds.Tables[0].Rows[i].IsNull(5)==false)
							title = ds.Tables[0].Rows[i].ItemArray[5];
						else
							title = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,title);
						strut.AddTagKey(TagName.SDO_Title,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						//内容
						object context;
						if(ds.Tables[0].Rows[i].IsNull(6)==false)
							context = ds.Tables[0].Rows[i].ItemArray[6];
						else
							context = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,context);
						strut.AddTagKey(TagName.SDO_Context,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);

						//道具编号
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[7]);
						strut.AddTagKey(TagName.SDO_ItemCode,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						
						//赠送日期
						object sendDate;
						if(ds.Tables[0].Rows[i].IsNull(8)==false)
							sendDate = ds.Tables[0].Rows[i].ItemArray[8];
						else
							sendDate = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP,sendDate);
						strut.AddTagKey(TagName.SDO_SendTime,TagFormat.TLV_TIMESTAMP,(uint)bytes.Length,bytes);

                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
                        structList[i - index] = strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_GIFTBOX_QUERY_RESP,10);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDOAPI_SDOItemShopAPI_NoItemOnGift,Msg_Category.SDO_ADMIN,ServiceKey.SDO_GIFTBOX_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
                return Message.COMMON_MES_RESP(lg.SDOAPI_SDOItemShopAPI_NoItemOnGift, Msg_Category.SDO_ADMIN, ServiceKey.SDO_GIFTBOX_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 添加玩家礼物盒上的道具
		/// </summary>
		/// <returns></returns>
		public Message GiftBox_MessageItem_Add()
		{
			int operateUserID  = 0;
			int userIndexID = 0;
			string serverIP = null;
			int itemCode = 0;
			string title = null;
			string context = null;
			int timesLimit = 0;
			DateTime dateLimit;
			int result = -1;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_ItemCode,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ItemCode).m_bValueBuffer);
				itemCode  =(int)strut.toInteger();				
				strut = new TLV_Structure(TagName.SDO_UserIndexID,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_UserIndexID).m_bValueBuffer);
				userIndexID  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_TimesLimit,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_TimesLimit).m_bValueBuffer);
				timesLimit  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_DateLimit,3,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_DateLimit).m_bValueBuffer);
				dateLimit  =strut.toDate();
				title = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Title).m_bValueBuffer);
				context = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Context).m_bValueBuffer);
				result = ItemShopInfo.giftBox_Insert(operateUserID,serverIP,userIndexID,itemCode,title,context,timesLimit,dateLimit);
				if(result==1)
				{
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.API_Add + lg.SDOAPI_SDOItemLogInfoAPI_Account + userIndexID + lg.SDOAPI_SDOItemShopAPI_GiftItem + itemCode + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_GIFTBOX_CREATE_RESP);
				}
				else
				{
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.API_Add + lg.SDOAPI_SDOItemLogInfoAPI_Account + userIndexID + lg.SDOAPI_SDOItemShopAPI_GiftItem + itemCode + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_GIFTBOX_CREATE_RESP);
				}
			}
			catch(Common.Logic.Exception ex)
			{	
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.SDO_ADMIN,ServiceKey.SDO_GIFTBOX_CREATE_RESP);
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
			int userIndexID = 0;
			string serverIP = null;
			int itemCode = 0 ;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_UserIndexID,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_UserIndexID).m_bValueBuffer);
				userIndexID  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_ItemCode,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ItemCode).m_bValueBuffer);
				itemCode  =(int)strut.toInteger();
				result = ItemShopInfo.giftBox_Delete(operateUserID,serverIP,userIndexID,itemCode);
				if(result==1)
				{
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.API_Delete + lg.SDOAPI_SDOItemLogInfoAPI_Account + userIndexID + lg.SDOAPI_SDOItemShopAPI_GiftItem + itemCode + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_ITEMSHOP_DELETE_RESP);
				}
				else
				{
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.API_Delete + lg.SDOAPI_SDOItemLogInfoAPI_Account + userIndexID + lg.SDOAPI_SDOItemShopAPI_GiftItem + itemCode + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_ITEMSHOP_DELETE_RESP); 
				}
			}
			catch(System.Exception ex)
			{
			   return Message.COMMON_MES_RESP(ex.Message,Msg_Category.SDO_ADMIN,ServiceKey.SDO_ITEMSHOP_DELETE_RESP,TagName.ERROR_Msg, TagFormat.TLV_STRING);
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
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+ lg.SDOAPI_SDOItemLogInfoAPI_Account +account+lg.SDOAPI_SDOItemShopAPI_OnlineStatus+"!");
				ds = ItemShopInfo.userOnline_Query(serverIP,account);	
				if(ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.SDO_UserIndexID,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.SDO_Account,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP,ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.SDO_LoginTime,TagFormat.TLV_TIMESTAMP,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP,ds.Tables[0].Rows[i].ItemArray[3]);
						strut.AddTagKey(TagName.SDO_LogoutTime,TagFormat.TLV_TIMESTAMP,(uint)bytes.Length,bytes);
						structList[i]=strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_USERONLINE_QUERY_RESP,4);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDOAPI_SDOItemShopAPI_NoOnlineStatus,Msg_Category.SDO_ADMIN,ServiceKey.SDO_USERONLINE_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP(lg.SDOAPI_SDOItemShopAPI_NoOnlineStatus, Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERONLINE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

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
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
				TLV_Structure tlvStrut = new TLV_Structure(TagName.SDO_BeginTime,3,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_BeginTime).m_bValueBuffer);
				beginDate  =tlvStrut.toDate();
				tlvStrut = new TLV_Structure(TagName.SDO_EndTime,3,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_EndTime).m_bValueBuffer);
				endDate  =tlvStrut.toDate();
				tlvStrut = new TLV_Structure(TagName.SDO_MoneyType,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_MoneyType).m_bValueBuffer);
				moneyType  =(int)tlvStrut.toInteger();
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+ lg.SDOAPI_SDOItemLogInfoAPI_Account +account+lg.SDOAPI_SDOItemShopAPI_TradeRecord+"!");
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
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0]);
                        strut.AddTagKey(TagName.SDO_Account, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[1]);
                        strut.AddTagKey(TagName.SDO_ProductID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
                        strut.AddTagKey(TagName.SDO_ProductName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[3]);
                        strut.AddTagKey(TagName.SDO_MoneyCost, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[4]);
                        strut.AddTagKey(TagName.SDO_MoneyType, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, ds.Tables[0].Rows[i].ItemArray[5]);
                        strut.AddTagKey(TagName.SDO_ShopTime, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
                        structList[i - index] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.SDO_ADMIN, ServiceKey.SDO_CONSUME_QUERY_RESP, 7);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.SDOAPI_SDOItemShopAPI_NoChargeRecord, Msg_Category.SDO_ADMIN, ServiceKey.SDO_CONSUME_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
                }
			}
			catch(System.Exception ex)
			{
                return Message.COMMON_MES_RESP(lg.SDOAPI_SDOItemShopAPI_NoChargeRecord, Msg_Category.SDO_ADMIN, ServiceKey.SDO_CONSUME_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 查看道具使用期限
		/// </summary>
		/// <returns></returns>
		public Message ItemLimit_Query()
		{
			DataSet ds = null;
			int itemcode = 0;
			try
			{
				TLV_Structure tlvStrut = new TLV_Structure(TagName.SDO_ProductID,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ProductID).m_bValueBuffer);
				itemcode  =(int)tlvStrut.toInteger();
				ds = ItemShopInfo.itemlimit_Query(itemcode);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					return Message.COMMON_MES_RESP(Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0].ToString()), Msg_Category.SDO_ADMIN, ServiceKey.SDO_DAYSLIMIT_QUERY_RESP,TagName.SDO_DaysLimit,TagFormat.TLV_INTEGER);
				}
				else

					return Message.COMMON_MES_RESP("没有该道具的期限", Msg_Category.SDO_ADMIN, ServiceKey.SDO_DAYSLIMIT_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);

			}
			catch (System.Exception e)
			{
				return Message.COMMON_MES_RESP("没有该道具的期限", Msg_Category.SDO_ADMIN, ServiceKey.SDO_DAYSLIMIT_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);

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
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
                account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
                TLV_Structure tlvStrut = new TLV_Structure(TagName.SDO_BeginTime, 3, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_BeginTime).m_bValueBuffer);
                beginDate = tlvStrut.toDate();
                tlvStrut = new TLV_Structure(TagName.SDO_EndTime, 3, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_EndTime).m_bValueBuffer);
                endDate = tlvStrut.toDate();
                tlvStrut = new TLV_Structure(TagName.SDO_MoneyType, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_MoneyType).m_bValueBuffer);
                moneyType = (int)tlvStrut.toInteger();
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDOItemLogInfoAPI_Account + account + lg.SDOAPI_SDOItemShopAPI_ConsumeRecord + "!");
                ds = ItemShopInfo.userConsume_QuerySum(serverIP, account, moneyType, beginDate, endDate);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return Message.COMMON_MES_RESP(Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]), Msg_Category.SDO_ADMIN, ServiceKey.SDO_CONSUME_QUERY_RESP, TagName.SDO_ChargeSum, TagFormat.TLV_INTEGER);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.SDOAPI_SDOItemShopAPI_NoChargeRecord, Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERCONSUMESUM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }
            }
            catch (System.Exception ex)
            {
                return Message.COMMON_MES_RESP(lg.SDOAPI_SDOItemShopAPI_NoChargeRecord, Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERCONSUMESUM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
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
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
                senderUserID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_SendUserID).m_bValueBuffer);
                receiveserID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ReceiveNick).m_bValueBuffer);
                if (senderUserID.Length < 0)
                    senderUserID = receiveserID;
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDOItemLogInfoAPI_Account + senderUserID + lg.SDOAPI_SDOItemShopAPI_TradeRecord + "!");
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
                    return Message.COMMON_MES_RESP(structList, Msg_Category.SDO_ADMIN, ServiceKey.SDO_ITEMSHOP_TRADE_QUERY_RESP, 7);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.SDOAPI_SDOItemShopAPI_NoTradeRecord, Msg_Category.SDO_ADMIN, ServiceKey.SDO_ITEMSHOP_TRADE_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
                }
			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP(lg.SDOAPI_SDOItemShopAPI_NoTradeRecord,Msg_Category.SDO_ADMIN,ServiceKey.SDO_ITEMSHOP_TRADE_QUERY_RESP,TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
	}
}
