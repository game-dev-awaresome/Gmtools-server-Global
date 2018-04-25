using System;
using System.Text;
using Common.API;
using Common.Logic;
using Common.DataInfo;
using UserCharge.UserChargeInfo;
using lg = Common.API.LanguageAPI;
namespace UserCharge.UserChargeAPI
{
    public class CardDetailInfoAPI
    {
        Message msg = null;
        public CardDetailInfoAPI(byte[] packet)
        {
            msg = new Message(packet, (uint)packet.Length);
        }
        /// <summary>
        /// 玩家充值记录信息
        /// </summary>
        /// <returns></returns>
        public Message CardDetailInfo_Query(int index, int pageSize)
        {
            string account = null;
            string cardID = null;
            string cardPwd = null;
            int actionType = 0;
            string msg1 = null;
            System.Data.DataSet ds = null;
            try
            {
                account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CARD_username).m_bValueBuffer);
                cardID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CARD_cardnum).m_bValueBuffer);
                cardPwd = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CARD_cardpass).m_bValueBuffer);
                TLV_Structure tlvStrut = new TLV_Structure(TagName.CARD_ActionType, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CARD_ActionType).m_bValueBuffer);
                actionType = (int)tlvStrut.toInteger();
                if (actionType == 1)
                {
                    msg1 = lg.CardDetail_OneCard;
                }
                else
                {
                    msg1 = lg.CardDetail_LeisureCard;
                }
                SqlHelper.log.WriteLog(lg.API_Display + lg.NineYou + lg.CardDetail_Account + account + msg1 + lg.CardDetail_FillDetail);
                Console.WriteLine(DateTime.Now + " -" + lg.API_Display + lg.NineYou + lg.CardDetail_Account + account + msg1 + lg.CardDetail_FillDetail);
                ds = CardDetailInfo.CardDetailInfo_Query(account, cardID, cardPwd, actionType);
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
                        //byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
                        //strut.AddTagKey(TagName.CARD_PDID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        //bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[1]);
                        //strut.AddTagKey(TagName.CARD_PDkey, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[1]);
                        strut.AddTagKey(TagName.CARD_PDCardType, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
                        strut.AddTagKey(TagName.CARD_PDFrom, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[3]));
                        strut.AddTagKey(TagName.CARD_cardnum, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[4]));
                        strut.AddTagKey(TagName.CARD_cardpass, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[5]);
                        strut.AddTagKey(TagName.CARD_price, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[6]);
                        strut.AddTagKey(TagName.CARD_PDaction, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                       // bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[8]));
                       // strut.AddTagKey(TagName.CARD_PDuserid, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[7]));
                        strut.AddTagKey(TagName.CARD_PDusername, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                       // bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[10]));
                       // strut.AddTagKey(TagName.CARD_PDgetuserid, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[8]);
                        strut.AddTagKey(TagName.CARD_PDgetusername, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[9]));
						strut.AddTagKey(TagName.CARD_PDdate, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));

                        structList[i - index] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERCHARGEDETAIL_QUERY_RESP, 10);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.CardDetail_NoChargeRecord, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERCHARGEDETAIL_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP(lg.CardDetail_NoChargeRecord, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERCHARGEDETAIL_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }

        }
        /// <summary>
        /// 查看玩家充值合计记录
        /// </summary>
        /// <returns></returns>
        public Message CardDetailInfo_QuerySum()
        {
            string account = null;
            string cardID = null;
            string cardPwd = null;
            int actionType = 0;
            System.Data.DataSet ds = null;
            try
            {
                account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CARD_username).m_bValueBuffer);
                cardID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CARD_cardnum).m_bValueBuffer);
                cardPwd = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CARD_cardpass).m_bValueBuffer);
                TLV_Structure tlvStrut = new TLV_Structure(TagName.CARD_ActionType, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CARD_ActionType).m_bValueBuffer);
                actionType = (int)tlvStrut.toInteger();
                SqlHelper.log.WriteLog(lg.API_Display + lg.NineYou + lg.CardDetail_Account + account + lg.CardDetail_SumFillDetail);
                Console.WriteLine(DateTime.Now + " -" + lg.API_Display + lg.NineYou + lg.CardDetail_Account + account + lg.CardDetail_SumFillDetail);
                ds = CardDetailInfo.CardDetailInfo_QuerySum(account, cardID, cardPwd, actionType);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return Message.COMMON_MES_RESP(Convert.ToDouble(ds.Tables[0].Rows[0].ItemArray[0]), Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERDETAIL_SUM_QUERY_RESP, TagName.CARD_SumTotal, TagFormat.TLV_MONEY);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.CardDetail_NoSumChargeRecord, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERDETAIL_SUM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }
            }
            catch (System.Exception ex)
            {
                return Message.COMMON_MES_RESP(lg.CardDetail_NoSumChargeRecord, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERDETAIL_SUM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }

        }
        /// <summary>
        /// 玩家消费记录信息
        /// </summary>
        /// <returns></returns>
        public Message CardUserConsume_Query(int index, int pageSize)
        {
            string account = null;
            int actionType = 0;
            System.Data.DataSet ds = null;
            try
            {
                account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CARD_username).m_bValueBuffer);
                TLV_Structure tlvStrut = new TLV_Structure(TagName.CARD_ActionType, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CARD_ActionType).m_bValueBuffer);
                actionType = (int)tlvStrut.toInteger();
                SqlHelper.log.WriteLog(lg.API_Display + lg.NineYou + lg.CardDetail_Account + account + lg.CardDetail_ConsumeRecord);
                Console.WriteLine(DateTime.Now + " -" + lg.API_Display + lg.NineYou + lg.CardDetail_Account + account + lg.CardDetail_ConsumeRecord);
                ds = CardDetailInfo.CardUserConsumeInfo_Query(account, actionType);
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
                        strut.AddTagKey(TagName.CARD_UDID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        //bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[1]);
                        //strut.AddTagKey(TagName.CARD_UDkey, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[1]);
                        strut.AddTagKey(TagName.CARD_UDusedo, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
                        strut.AddTagKey(TagName.CARD_UDdirect, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        //bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]));
                        //strut.AddTagKey(TagName.CARD_UDuserid, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[3]));
                        strut.AddTagKey(TagName.CARD_UDusername, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[4]));
                       // strut.AddTagKey(TagName.CARD_UDgetuserid, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        //bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[7]));
                        strut.AddTagKey(TagName.CARD_UDgetusername, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[5]));
                        strut.AddTagKey(TagName.CARD_UDcoins, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[6]);
                        strut.AddTagKey(TagName.CARD_UDtype, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[7]));
                        strut.AddTagKey(TagName.CARD_UDdate, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);

                       // bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[11]);
                        //strut.AddTagKey(TagName.CARD_UDip, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));

                        structList[i - index] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERCONSUME_QUERY_RESP, 9);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.CardDetail_NoConsumeRecord, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERCONSUME_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP(lg.CardDetail_NoConsumeRecord, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERCONSUME_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }
        }
        /// <summary>
        /// 查看玩家消费合计记录
        /// </summary>
        /// <returns></returns>
        public Message CardUserConsumeInfo_QuerySum()
        {
            string serverIP = null;
            string account = null;
            int actionType = 0;
            System.Data.DataSet ds = null;
            try
            {
                account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CARD_username).m_bValueBuffer);
                TLV_Structure tlvStrut = new TLV_Structure(TagName.CARD_ActionType, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CARD_ActionType).m_bValueBuffer);
                actionType = (int)tlvStrut.toInteger();
                SqlHelper.log.WriteLog(lg.API_Display + lg.NineYou + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.CardDetail_Account + account + lg.CardDetail_SumConsumeRecord);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.NineYou + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.CardDetail_Account + account + lg.CardDetail_SumConsumeRecord);
                ds = CardDetailInfo.CardUserConsumeInfo_QuerySum(account, actionType);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return Message.COMMON_MES_RESP(Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]), Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERCONSUME_SUM_QUERY_RESP, TagName.CARD_SumTotal, TagFormat.TLV_INTEGER);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.CardDetail_NoSumConsumeRecord, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERCONSUME_SUM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }
            }
            catch (System.Exception ex)
            {
                return Message.COMMON_MES_RESP(lg.CardDetail_NoSumConsumeRecord, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERCONSUME_SUM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }

        }
		public Message UserNick_Query()
		{
			string account = "";
			string nickName = "";
			System.Data.DataSet ds = null;
			try
			{
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.UserName).m_bValueBuffer);
				ds = CardDetailInfo.UserName_Query(account,nickName);
				Query_Structure[] structList = new Query_Structure[1];
				if (ds != null && ds.Tables[0].Rows.Count > 0)
				{
					Query_Structure strut = new Query_Structure(1);
					nickName = ds.Tables[0].Rows[0].ItemArray[1].ToString();
					byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, nickName);
					strut.AddTagKey(TagName.CARD_nickname, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					structList[0] = strut;
					return Message.COMMON_MES_RESP(structList, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERNICK_QUERY_RESP, 1);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.CardDetail_NoRegisterInfo, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERNICK_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP(lg.CardDetail_NoRegisterInfo, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERNICK_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);				
			}
		}
		private uint toRet(byte[] ba,int index)
		{
			uint ret=0;
			for(int i=0,j=index;i<2 && j<index+2;i++,j++)
			{
				ret+=(uint)ba[j]<<8*i;
			}
			return ret;
		}
		private string escape(string s)
		{

			StringBuilder sb = new StringBuilder();
			System.Text.RegularExpressions.Regex objAlphaPatt=new System.Text.RegularExpressions.Regex("[^a-zA-Z]");
			byte[] ba = System.Text.Encoding.Unicode.GetBytes(s);
			//char[] ca = s.ToCharArray();

			for (int i = 0; i < ba.Length; i += 2)
			{    /**///// BE SURE 2's 
					uint result = toRet(ba,i);
				if(result>255)
				{
					sb.Append("%u");
					sb.Append(ba[i + 1].ToString("X2"));
					sb.Append(ba[i].ToString("X2"));
				}
				else
				{
					sb.Append(BitConverter.ToChar(ba,i));
				}
			}
			return sb.ToString();
            
		}
        /// <summary>
        /// 用户注册信息查询
        /// </summary>
        /// <returns></returns>
        public Message UserInfo_Query()
        {
            string account = "";
            string nickName = "";
            int actionType = 0;
			System.Collections.ArrayList list = null;
            try
            {
                account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CARD_username).m_bValueBuffer);
                TLV_Structure tlvStrut = new TLV_Structure(TagName.CARD_ActionType, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CARD_ActionType).m_bValueBuffer);
                actionType = (int)tlvStrut.toInteger();
                nickName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CARD_nickname).m_bValueBuffer);
				//nickName = escape(nickName);
				/*ds = CardDetailInfo.UserName_Query(account,nickName);
				if (ds != null && ds.Tables[0].Rows.Count > 0)
				{
					userID =Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0].ToString());
					nickName = ds.Tables[0].Rows[0].ItemArray[1].ToString();
				}*/
                SqlHelper.log.WriteLog(lg.API_Display + lg.NineYou + lg.CardDetail_Account + account + lg.CardDetail_RegisterInfo);
                Console.WriteLine(DateTime.Now + " -" + lg.API_Display + lg.NineYou + lg.CardDetail_Account + account + lg.CardDetail_RegisterInfo);

                //ds = CardDetailInfo.UserRegistInfo_Query(userID, actionType);
				if(account.Length>0)
					list = MD5EncryptAPI.You9Register(account,0);
				else
				{
					list = MD5EncryptAPI.You9Register(nickName,1);
				}
                Query_Structure[] structList = new Query_Structure[1];
				if (list != null && list.Count>0)
				{
					Query_Structure strut = new Query_Structure((uint)list.Count+1);
					byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32( list[0]));
					strut.AddTagKey(TagName.CARD_use_userid, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, list[1].ToString());
					strut.AddTagKey(TagName.CARD_username, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,  list[2]);
					strut.AddTagKey(TagName.CARD_nickname, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, list[3]);
					strut.AddTagKey(TagName.CARD_realname, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_DATE, Convert.ToDateTime(list[4]));
					strut.AddTagKey(TagName.CARD_birthday, TagFormat.TLV_DATE, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, list[5]);
					strut.AddTagKey(TagName.CARD_cardtype, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, list[6]);
					strut.AddTagKey(TagName.CARD_id, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, list[7]);
					strut.AddTagKey(TagName.CARD_email, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, list[8]);
					strut.AddTagKey(TagName.CARD_occupation, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,list[9]);
					strut.AddTagKey(TagName.CARD_education, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, list[10]);
					strut.AddTagKey(TagName.CARD_marriage, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, list[11]);
					strut.AddTagKey(TagName.CARD_constellation, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,list[12]);
					strut.AddTagKey(TagName.CARD_shx, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,list[13]);
					strut.AddTagKey(TagName.CARD_city, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,list[14]);
					strut.AddTagKey(TagName.CARD_address, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,list[15]);
					strut.AddTagKey(TagName.CARD_phone, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, list[16]);
					strut.AddTagKey(TagName.CARD_qq, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, list[17]);
					strut.AddTagKey(TagName.CARD_intro, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, list[18]);
					strut.AddTagKey(TagName.CARD_msn, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, list[19]);
					strut.AddTagKey(TagName.CARD_mobilephone, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					structList[0] = strut;
					return Message.COMMON_MES_RESP(structList, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERINFO_QUERY_RESP, 20);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.CardDetail_NoRegisterInfo, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERINFO_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

            }
            catch (System.Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP(lg.CardDetail_NoRegisterInfo, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERINFO_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }
        }
		/// <summary>
		/// 重置玩家身份证信息
		/// </summary>
		/// <returns></returns>
		public Message CardUserInfo_Clear()
		{
			int operateUserID = 0;
			string account = null;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CARD_username).m_bValueBuffer);
				int result = CardDetailInfo.UserRegistInfo_Clear(operateUserID, account);
				if (result == 1)
				{
					SqlHelper.log.WriteLog(lg.CardDetail_Reset + lg.NineYou + lg.CardDetail_Account + account + lg.CardDetail_IDInfo + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now+" - "+lg.CardDetail_Reset + lg.NineYou + lg.CardDetail_Account + account + lg.CardDetail_IDInfo + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCCESS", Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERINFO_CLEAR_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.CardDetail_Reset + lg.NineYou + lg.CardDetail_Account + account + lg.CardDetail_IDInfo + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now+" - "+lg.CardDetail_Reset + lg.NineYou + lg.CardDetail_Account + account + lg.CardDetail_IDInfo + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERINFO_CLEAR_RESP);
				}
			}
			catch (System.Exception ex)
			{
				return Message.COMMON_MES_RESP(lg.CardDetail_NoRegisterInfo, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERINFO_CLEAR_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 重置玩家身份证信息
		/// </summary>
		/// <returns></returns>
		public Message CardUserSecure_Clear()
		{
			string account = null;
			//string nickName = "";
			int userID = 0;
			//System.Data.DataSet ds = null;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				int operateUserID = (int)strut.toInteger();
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CARD_username).m_bValueBuffer);
				//ds = CardDetailInfo.UserName_Query(account,nickName);
				//if (ds != null && ds.Tables[0].Rows.Count > 0)
				//{
					//userID =Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0].ToString());
					int result = CardDetailInfo.UserSecureCodeInfo_Clear(operateUserID, account);
					if (result == 1)
					{
						SqlHelper.log.WriteLog(lg.CardDetail_Reset + lg.NineYou + lg.CardDetail_Account + account + lg.CardDetail_SecurityInfo + lg.API_Success + "!");
						Console.WriteLine(DateTime.Now+" - " + lg.CardDetail_Reset + lg.NineYou + lg.CardDetail_Account + account + lg.CardDetail_SecurityInfo + lg.API_Success + "!");
						return Message.COMMON_MES_RESP("SUCCESS", Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERSECURE_CLEAR_RESP);
					}
					else
					{
						SqlHelper.log.WriteLog(lg.CardDetail_Reset + lg.NineYou + lg.CardDetail_Account + account + lg.CardDetail_SecurityInfo + lg.API_Failure + "!");
						Console.WriteLine(DateTime.Now+" - " + lg.CardDetail_Reset + lg.NineYou + lg.CardDetail_Account + account + lg.CardDetail_SecurityInfo + lg.API_Failure + "!");
						return Message.COMMON_MES_RESP("FAILURE", Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERSECURE_CLEAR_RESP);
					}
			}
			catch (System.Exception ex)
			{
				return Message.COMMON_MES_RESP(lg.CardDetail_NoRegisterInfo, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERINFO_CLEAR_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 重置玩家身份证信息
		/// </summary>
		/// <returns></returns>
		public Message MemberInfo_Lock()
		{
			string account = null;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				int operateUserID = (int)strut.toInteger();
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CARD_username).m_bValueBuffer);
				DateTime rightNow = Convert.ToDateTime("2010-12-31");
				DateTime sinceDate = new DateTime(1970,1,1);
				System.TimeSpan dif = rightNow.Subtract(sinceDate);
				long difm = long.Parse(decimal.Round(decimal.Parse(dif.TotalMilliseconds.ToString()),0).ToString()); 
				int result = CardDetailInfo.MemberInfo_Lock(operateUserID, account,difm);
				if (result == 1)
				{
					SqlHelper.log.WriteLog(lg.CardDetail_Lock + lg.NineYou + lg.CardDetail_Account + account + lg.CardDetail_UserInfo + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now+" - " +lg.CardDetail_Lock + lg.NineYou + lg.CardDetail_Account + account + lg.CardDetail_UserInfo + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCCESS", Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERLOCK_UPDATE_RESP);
				}
				else if(result == -1)
				{
					return Message.COMMON_MES_RESP(lg.CardDetail_Locked, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERLOCK_UPDATE_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.CardDetail_Lock + lg.NineYou + lg.CardDetail_Account + account + lg.CardDetail_UserInfo + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now+" - "+lg.CardDetail_Lock + lg.NineYou + lg.CardDetail_Account + account + lg.CardDetail_UserInfo + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERLOCK_UPDATE_RESP);
				}
			}
			catch (System.Exception ex)
			{
				return Message.COMMON_MES_RESP(lg.CardDetail_NoRegisterInfo, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERLOCK_UPDATE_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}

    }
}
