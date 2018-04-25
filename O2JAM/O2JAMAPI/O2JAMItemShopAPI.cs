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
	/// SDOItemShopAPI 的摘要说明。
	/// </summary>
	public class O2JAMItemShopAPI
	{
		Message msg = null;
		public O2JAMItemShopAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public O2JAMItemShopAPI(byte[] packet)
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
			int sex = -1;
            DataSet ds = null;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_ServerIP).m_bValueBuffer);
				itemName1 = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_ITEM_NAME).m_bValueBuffer);
				TLV_Structure tlvStrut = new TLV_Structure(TagName.o2jam_Sex,4,msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_Sex).m_bValueBuffer);
				sex =(int)tlvStrut.toInteger();
				SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.O2JAM_ItemShopInfoAPI_GameItem);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.O2JAM_ItemShopInfoAPI_GameItem);
                //请求所有分类道具列表
                ds = ItemShopInfo.itemShop_QueryAll(serverIP,itemName1,sex);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
                        //道具编号
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[0]);
                        strut.AddTagKey(TagName.o2jam_ITEM_INDEX_ID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        //道具名
                        object itemName;
                        if (ds.Tables[0].Rows[i].IsNull(1) == false)
                            itemName = ds.Tables[0].Rows[i].ItemArray[1];
                        else
                            itemName = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, itemName);
						strut.AddTagKey(TagName.o2jam_ITEM_NAME, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						//性别
						sex = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[2]);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, sex);
                        strut.AddTagKey(TagName.o2jam_Sex, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        //G币
                        int GCash = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, GCash);
                        strut.AddTagKey(TagName.o2jam_GEM, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						//M币
                        int MCash = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, MCash);
                        strut.AddTagKey(TagName.o2jam_MCASH, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        structList[i] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.O2JAM_ADMIN, ServiceKey.O2JAM_ITEM_QUERY_RESP, 5);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.O2JAM_ItemShopInfoAPI_NoItem, Msg_Category.O2JAM_ADMIN, ServiceKey.O2JAM_ITEM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

                }

            }
            catch (System.Exception ex)
            {
                return Message.COMMON_MES_RESP(lg.O2JAM_ItemShopInfoAPI_NoItem, Msg_Category.O2JAM_ADMIN, ServiceKey.O2JAM_ITEM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }
        }
		/// <summary>
		/// 查看玩家身上道具
		/// </summary>
		/// <returns></returns>
		public Message itemShop_Owner_Query()
		{
			string serverIP = null;
			string account = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_UserID).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+account+lg.O2JAM_ItemShopInfoAPI_PersonalItem);
				Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+account+lg.O2JAM_ItemShopInfoAPI_PersonalItem);
				//请求玩家身上的道具
				ds = ItemShopInfo.itemShop_Query(serverIP,account);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[1];
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[0].ItemArray.Length+1);
						//道具编号
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[0]);
						strut.AddTagKey(TagName.o2jam_ITEM_INDEX_ID,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						//道具名
						//bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,UserValidate.validData(ds.Tables[0].Rows[0].ItemArray[1]));
						//strut.AddTagKey(TagName.o2jam_ITEM_NAME,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						//道具1
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[1]);
						strut.AddTagKey(TagName.O2JAM_EQUIP1,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						//道具2
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[2]);
						strut.AddTagKey(TagName.O2JAM_EQUIP2,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);		
						//道具3
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[3]);
                        strut.AddTagKey(TagName.O2JAM_EQUIP3, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						//道具4
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[4]);
						strut.AddTagKey(TagName.O2JAM_EQUIP4,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						//道具5
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[5]);
						strut.AddTagKey(TagName.O2JAM_EQUIP5,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);		
						//道具6
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[6]);
						strut.AddTagKey(TagName.O2JAM_EQUIP6, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						//道具7
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[7]);
						strut.AddTagKey(TagName.O2JAM_EQUIP7,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						//道具8
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[8]);
						strut.AddTagKey(TagName.O2JAM_EQUIP8,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);		
						//道具9
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[9]);
						strut.AddTagKey(TagName.O2JAM_EQUIP9, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						//道具10
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[10]);
						strut.AddTagKey(TagName.O2JAM_EQUIP10,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						//道具11
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[11]);
						strut.AddTagKey(TagName.O2JAM_EQUIP11,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);		
						//道具12
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[12]);
						strut.AddTagKey(TagName.O2JAM_EQUIP12, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						//道具13
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[13]);
						strut.AddTagKey(TagName.O2JAM_EQUIP13,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						//道具14
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[14]);
						strut.AddTagKey(TagName.O2JAM_EQUIP14,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);		
						//道具15
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[15]);
						strut.AddTagKey(TagName.O2JAM_EQUIP15, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						//道具16
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[16]);
						strut.AddTagKey(TagName.O2JAM_EQUIP16,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						//包裹1
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[17]);
						strut.AddTagKey(TagName.O2JAM_BAG1,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						//包裹2
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[18]);
						strut.AddTagKey(TagName.O2JAM_BAG2,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);		
						//包裹3
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[19]);
						strut.AddTagKey(TagName.O2JAM_BAG3, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						//包裹4
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[20]);
						strut.AddTagKey(TagName.O2JAM_BAG4,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						//包裹5
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[21]);
						strut.AddTagKey(TagName.O2JAM_BAG5,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);		
						//包裹6
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[22]);
						strut.AddTagKey(TagName.O2JAM_BAG6, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						//包裹7
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[23]);
						strut.AddTagKey(TagName.O2JAM_BAG7,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						//包裹8
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[24]);
						strut.AddTagKey(TagName.O2JAM_BAG8,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);		
						//包裹9
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[25]);
						strut.AddTagKey(TagName.O2JAM_BAG9, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						//包裹10
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[26]);
						strut.AddTagKey(TagName.O2JAM_BAG10,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						//包裹11
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[27]);
						strut.AddTagKey(TagName.O2JAM_BAG11,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);		
						//包裹12
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[28]);
						strut.AddTagKey(TagName.O2JAM_BAG12, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						//包裹13
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[29]);
						strut.AddTagKey(TagName.O2JAM_BAG13,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						//包裹14
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[30]);
						strut.AddTagKey(TagName.O2JAM_BAG14,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);		
						//包裹15
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[31]);
						strut.AddTagKey(TagName.O2JAM_BAG15, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						//包裹16
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[32]);
						strut.AddTagKey(TagName.O2JAM_BAG16,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[33]);
						strut.AddTagKey(TagName.O2JAM_BAG17, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						//包裹18
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[34]);
						strut.AddTagKey(TagName.O2JAM_BAG18,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						//包裹19
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[35]);
						strut.AddTagKey(TagName.O2JAM_BAG19,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);		
						//包裹20
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[36]);
						strut.AddTagKey(TagName.O2JAM_BAG20, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						//包裹21
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[37]);
						strut.AddTagKey(TagName.O2JAM_BAG21,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						//包裹22
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[38]);
						strut.AddTagKey(TagName.O2JAM_BAG22,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);		
						//包裹23
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[39]);
						strut.AddTagKey(TagName.O2JAM_BAG23, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						//包裹24
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[40]);
						strut.AddTagKey(TagName.O2JAM_BAG24,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						//包裹25
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[41]);
						strut.AddTagKey(TagName.O2JAM_BAG25,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						//包裹26
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[42]);
						strut.AddTagKey(TagName.O2JAM_BAG26,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);		
						//包裹27
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[43]);
						strut.AddTagKey(TagName.O2JAM_BAG27, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						//包裹28
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[44]);
						strut.AddTagKey(TagName.O2JAM_BAG28,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						//包裹29
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[45]);
						strut.AddTagKey(TagName.O2JAM_BAG29,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);		
						//包裹30
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[46]);
						strut.AddTagKey(TagName.O2JAM_BAG30, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);	
						structList[0] = strut;
					return Message.COMMON_MES_RESP(structList,Msg_Category.O2JAM_ADMIN,ServiceKey.O2JAM_ITEM_QUERY_RESP,47);
				}
				else
					return Message.COMMON_MES_RESP(lg.O2JAM_ItemShopInfoAPI_NoItemOnPlayer,Msg_Category.O2JAM_ADMIN,ServiceKey.O2JAM_ITEM_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);

			}
			catch(System.Exception ex)
			{
                Console.WriteLine(ex.Message);
			   return Message.COMMON_MES_RESP(lg.O2JAM_ItemShopInfoAPI_NoItemOnPlayer,Msg_Category.O2JAM_ADMIN,ServiceKey.O2JAM_ITEM_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 删除玩家身上道具
		/// </summary>
		/// <returns></returns>
		public Message ItemShop_Delete()
		{
			int operateUserID  = 0;
			int userIndexID = 0;
			string serverIP = null;
			int pos = 0;
			int flag = 0;
			int result = -1;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.o2jam_USER_INDEX_ID,4,msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_USER_INDEX_ID).m_bValueBuffer);
				userIndexID  =(int)strut.toInteger();				
				strut = new TLV_Structure(TagName.o2jam_POSITION,4,msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_POSITION).m_bValueBuffer);
				pos  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.o2jam_TypeFlag,4,msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_TypeFlag).m_bValueBuffer);
				flag  =(int)strut.toInteger();
				result = ItemShopInfo.itemShop_Delete(operateUserID,serverIP,userIndexID,pos,flag);
				if(result==1)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+userIndexID + lg.API_Delete + lg.O2JAM_ItemShopInfoAPI_PersonalItem + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+userIndexID+lg.API_Delete + lg.O2JAM_ItemShopInfoAPI_PersonalItem + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.O2JAM_ADMIN,ServiceKey.O2JAM_ITEM_UPDATE_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+userIndexID+lg.API_Delete + lg.O2JAM_ItemShopInfoAPI_PersonalItem + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+userIndexID+lg.API_Delete + lg.O2JAM_ItemShopInfoAPI_PersonalItem + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.O2JAM_ADMIN,ServiceKey.O2JAM_ITEM_UPDATE_RESP); 
				}
			}
			catch(Common.Logic.Exception ex)
			{	
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.O2JAM_ADMIN,ServiceKey.O2JAM_ITEM_UPDATE_RESP);
			}
		}
		/// <summary>
		/// 添加玩家礼物盒的道具
		/// </summary>
		/// <returns></returns>
		public Message GiftBox_MessageItem_Add()
		{
			int result = -1;
			string serverIP = null;
			int operateUserID = 0;
			int userIndexID = 0;
			int itemCode = 0 ;
			int expireDate ;
			int gem = 0;
			int mCash = 0;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.o2jam_USER_INDEX_ID,4,msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_USER_INDEX_ID).m_bValueBuffer);
				userIndexID  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.o2jam_ITEM_INDEX_ID,4,msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_ITEM_INDEX_ID).m_bValueBuffer);
				itemCode  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.o2jam_GEM,4,msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_GEM).m_bValueBuffer);
				gem  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.o2jam_MCASH,4,msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_MCASH).m_bValueBuffer);
				mCash  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.o2jam_WriteDate,4,msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_WriteDate).m_bValueBuffer);
				expireDate = (int)strut.toInteger();
				result = ItemShopInfo.GiftBox_Insert(operateUserID,serverIP,userIndexID,itemCode,gem,mCash,expireDate);
				if(result==1)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+userIndexID+lg.O2JAM_ItemShopInfoAPI_GiftItem+itemCode+lg.API_Add + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+userIndexID+lg.O2JAM_ItemShopInfoAPI_GiftItem+itemCode+lg.API_Add + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.O2JAM_ADMIN,ServiceKey.O2JAM_GIFTBOX_CREATE_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+userIndexID+lg.O2JAM_ItemShopInfoAPI_GiftItem+itemCode+lg.API_Add + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+userIndexID+lg.O2JAM_ItemShopInfoAPI_GiftItem+itemCode+lg.API_Add + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.O2JAM_ADMIN,ServiceKey.O2JAM_GIFTBOX_CREATE_RESP);
				}
			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP("添加失败",Msg_Category.O2JAM_ADMIN,ServiceKey.O2JAM_GIFTBOX_CREATE_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 查看玩家礼物盒的道具
		/// </summary>
		/// <returns></returns>
		public Message giftBox_Query()
		{
			string serverIP = null;
			int userIndexID = 0;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_ServerIP).m_bValueBuffer);
				TLV_Structure tlvStrut = new TLV_Structure(TagName.o2jam_USER_INDEX_ID,4,msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_USER_INDEX_ID).m_bValueBuffer);
				userIndexID =(int)tlvStrut.toInteger();
				SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+userIndexID+lg.O2JAM_ItemShopInfoAPI_GiftItem);
				Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+userIndexID+lg.O2JAM_ItemShopInfoAPI_GiftItem);
				//请求所有分类道具列表
				ds = ItemShopInfo.O2JAMGiftBox_Query(serverIP,userIndexID);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						//道具ID
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.o2jam_ITEM_INDEX_ID,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						//道具名
						object itemName;
						if(ds.Tables[0].Rows[i].IsNull(1)==false)
							itemName = ds.Tables[0].Rows[i].ItemArray[1];
						else
							itemName = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,itemName);
						strut.AddTagKey(TagName.o2jam_ITEM_NAME,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);

						//G币
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.o2jam_GEM,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						//M币
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[3]);
						strut.AddTagKey(TagName.o2jam_MCASH,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);                        
						//发送人呢称
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[4]);
						strut.AddTagKey(TagName.o2jam_SenderNickName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						
						//日期
						object sendDate;
						if(ds.Tables[0].Rows[i].IsNull(5)==false)
							sendDate = ds.Tables[0].Rows[i].ItemArray[5];
						else
							sendDate = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP,sendDate);
						strut.AddTagKey(TagName.o2jam_ReadDate,TagFormat.TLV_TIMESTAMP,(uint)bytes.Length,bytes);
					
                        structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.O2JAM_ADMIN,ServiceKey.O2JAM_GIFTBOX_QUERY_RESP,6);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.O2JAM_ItemShopInfoAPI_NoItemOnGift,Msg_Category.O2JAM_ADMIN,ServiceKey.O2JAM_GIFTBOX_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
                return Message.COMMON_MES_RESP(ex.Message, Msg_Category.O2JAM_ADMIN, ServiceKey.O2JAM_GIFTBOX_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
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
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.o2jam_USER_INDEX_ID,4,msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_USER_INDEX_ID).m_bValueBuffer);
				userIndexID  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.o2jam_ITEM_INDEX_ID,4,msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_ITEM_INDEX_ID).m_bValueBuffer);
				itemCode  =(int)strut.toInteger();
				result = ItemShopInfo.giftBox_Delete(operateUserID,serverIP,userIndexID,itemCode);
				if(result==1)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+userIndexID+lg.O2JAM_ItemShopInfoAPI_GiftItem+itemCode+lg.API_Delete + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+userIndexID+lg.O2JAM_ItemShopInfoAPI_GiftItem+itemCode+lg.API_Delete + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.O2JAM_ADMIN,ServiceKey.O2JAM_ITEM_UPDATE_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+userIndexID+lg.O2JAM_ItemShopInfoAPI_GiftItem+itemCode+lg.API_Delete + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+userIndexID+lg.O2JAM_ItemShopInfoAPI_GiftItem+itemCode+lg.API_Delete + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.O2JAM_ADMIN,ServiceKey.O2JAM_ITEM_UPDATE_RESP); 
				}
			}
			catch(System.Exception ex)
			{
			   return Message.COMMON_MES_RESP(lg.O2JAM_ItemShopInfoAPI_NoItemOnGift,Msg_Category.O2JAM_ADMIN,ServiceKey.O2JAM_ITEM_UPDATE_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 查看玩家上下线状态
		/// </summary>
		/// <returns></returns>
		public Message ItemName_Query()
		{
			string serverIP = null;
			int itemCode = 0;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_ServerIP).m_bValueBuffer);
				TLV_Structure tlvstrut = new TLV_Structure(TagName.o2jam_ITEM_INDEX_ID,4,msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_ITEM_INDEX_ID).m_bValueBuffer);
				itemCode  =(int)tlvstrut.toInteger();
				SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_ItemShopInfoAPI_ItemName);
				Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_ItemShopInfoAPI_ItemName);
				ds = ItemShopInfo.itemShop_Name_Query(serverIP,itemCode);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[1];
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[0].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[0]);
						strut.AddTagKey(TagName.o2jam_ITEM_INDEX_ID,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[0].ItemArray[1]);
						strut.AddTagKey(TagName.o2jam_ITEM_NAME,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[2]);
						strut.AddTagKey(TagName.o2jam_GEM,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[3]);
						strut.AddTagKey(TagName.o2jam_ITEMCASH,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						structList[0]=strut;
					return Message.COMMON_MES_RESP(structList,Msg_Category.O2JAM_ADMIN,ServiceKey.O2JAM_ITEMNAME_QUERY_RESP,4);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.O2JAM_ItemShopInfoAPI_NoItemName,Msg_Category.O2JAM_ADMIN,ServiceKey.O2JAM_ITEMNAME_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP(lg.O2JAM_ItemShopInfoAPI_NoItemName, Msg_Category.O2JAM_ADMIN, ServiceKey.O2JAM_ITEMNAME_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

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
			int kind = 0;
			int buyType = -1;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_UserID).m_bValueBuffer);
				TLV_Structure tlvStrut = new TLV_Structure(TagName.o2jam_KIND, 4, msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_KIND).m_bValueBuffer);
				kind = (int)tlvStrut.toInteger();
				tlvStrut = new TLV_Structure(TagName.O2JAM_BuyType, 4, msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM_BuyType).m_bValueBuffer);
				buyType = (int)tlvStrut.toInteger();
				tlvStrut = new TLV_Structure(TagName.o2jam_BeginDate, 3, msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_BeginDate).m_bValueBuffer);
				beginDate = tlvStrut.toDate();
				tlvStrut = new TLV_Structure(TagName.o2jam_EndDate, 3, msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_EndDate).m_bValueBuffer);
				endDate = tlvStrut.toDate();
				SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+account+lg.O2JAM_ItemShopInfoAPI_ConsumeRecord);
				Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP+CommonInfo.serverIP_Query(serverIP)+lg.O2JAM_CharacterInfoAPI_Account+account+lg.O2JAM_ItemShopInfoAPI_ConsumeRecord);
				ds = ItemShopInfo.userConsume_Query(serverIP,account,kind,buyType,beginDate,endDate);
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
						strut.AddTagKey(TagName.o2jam_UserID, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.o2jam_ITEM_INDEX_ID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
                        strut.AddTagKey(TagName.o2jam_ITEM_NAME, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[3]);
						strut.AddTagKey(TagName.o2jam_GEM, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[4]);
                        strut.AddTagKey(TagName.o2jam_MCASH, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, ds.Tables[0].Rows[i].ItemArray[5]);
                        strut.AddTagKey(TagName.o2jam_REG_DATE, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[6]);
						strut.AddTagKey(TagName.o2jam_ReceiverID, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[7]);
						strut.AddTagKey(TagName.o2jam_ReceiverNickName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
                        structList[i - index] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.O2JAM_ADMIN, ServiceKey.O2JAM_CONSUME_QUERY_RESP, 9);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.O2JAM_ItemShopInfoAPI_NoConsumeRecord, Msg_Category.O2JAM_ADMIN, ServiceKey.O2JAM_CONSUME_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
                }
			}
			catch(System.Exception)
			{
                return Message.COMMON_MES_RESP(lg.O2JAM_ItemShopInfoAPI_NoConsumeRecord, Msg_Category.O2JAM_ADMIN, ServiceKey.O2JAM_CONSUME_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
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
            int kind = -1;
			int buyType=-1;
            DataSet ds = null;
            try
            {
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_UserID).m_bValueBuffer);
				TLV_Structure tlvStrut = new TLV_Structure(TagName.o2jam_KIND, 4, msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_KIND).m_bValueBuffer);
				kind = (int)tlvStrut.toInteger();
				tlvStrut = new TLV_Structure(TagName.O2JAM_BuyType, 4, msg.m_packet.m_Body.getTLVByTag(TagName.O2JAM_BuyType).m_bValueBuffer);
				buyType = (int)tlvStrut.toInteger();
				tlvStrut = new TLV_Structure(TagName.o2jam_BeginDate, 3, msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_BeginDate).m_bValueBuffer);
				beginDate = tlvStrut.toDate();
				tlvStrut = new TLV_Structure(TagName.o2jam_EndDate, 3, msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_EndDate).m_bValueBuffer);
				endDate = tlvStrut.toDate();
                SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.O2JAM_CharacterInfoAPI_Account + account + lg.O2JAM_ItemShopInfoAPI_SumConsumeRecord);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.O2JAM_CharacterInfoAPI_Account + account + lg.O2JAM_ItemShopInfoAPI_SumConsumeRecord);
                ds = ItemShopInfo.userConsume_QuerySum(serverIP, account, kind,buyType, beginDate, endDate);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
				    Query_Structure[] structList = new Query_Structure[1];
					Query_Structure strut = new Query_Structure(2);
					byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[0].ItemArray[0]);
					strut.AddTagKey(TagName.o2jam_GEM, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[0].ItemArray[1]);
					strut.AddTagKey(TagName.o2jam_MCASH, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
					structList[0] = strut;
                    return Message.COMMON_MES_RESP(structList, Msg_Category.O2JAM_ADMIN, ServiceKey.O2JAM_CONSUME_SUM_QUERY_RESP,2);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.O2JAM_ItemShopInfoAPI_NoSumConsumeRecord, Msg_Category.O2JAM_ADMIN, ServiceKey.O2JAM_CONSUME_SUM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }
            }
            catch (System.Exception)
            {
                return Message.COMMON_MES_RESP(lg.O2JAM_ItemShopInfoAPI_NoSumConsumeRecord, Msg_Category.O2JAM_ADMIN, ServiceKey.O2JAM_CONSUME_SUM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }

        }
	}
}
