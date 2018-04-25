using System;

using System.Text;
using Common.API;
using Common.Logic;
using Common.DataInfo;
using UserCharge.UserChargeInfo;
using lg = Common.API.LanguageAPI;
namespace UserCharge.UserChargeAPI
{
    public class UserCashPurchaseAPI
    {
        Message msg = null;
        public UserCashPurchaseAPI(byte[] packet)
        {
            msg = new Message(packet, (uint)packet.Length);

        }
        /// <summary>
        /// 玩家G币购买记录
        /// </summary>
        /// <returns></returns>
        public Message UserGCashPurchase_Query(int index,int pageSize)
        {
            string serverIP = null;
            string buyMan = null;
            string presentMan = null;
            DateTime beginDate;
            DateTime endDate;
            string sex = null;
            int cateGory = 0;
            string isPresent = null;
            string isGift = null;
            System.Data.DataSet ds = null;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
                buyMan = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_username).m_bValueBuffer);
                presentMan = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_getusername).m_bValueBuffer);
                TLV_Structure tlvStrut = new TLV_Structure(TagName.AuShop_BeginDate, 3, msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_BeginDate).m_bValueBuffer);
                beginDate = tlvStrut.toDate();
                tlvStrut = new TLV_Structure(TagName.AuShop_EndDate, 3, msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_EndDate).m_bValueBuffer);
                endDate = tlvStrut.toDate();
                sex = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_psex).m_bValueBuffer);
                isPresent = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_ispresent).m_bValueBuffer);
                isGift = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_islover).m_bValueBuffer);
                tlvStrut = new TLV_Structure(TagName.AuShop_pcategory,4, msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_pcategory).m_bValueBuffer);
                cateGory = (int)tlvStrut.toInteger();
                SqlHelper.log.WriteLog(lg.API_Display + lg.NineYou + lg.CardDetail_Account + presentMan + lg.UserCashPurchase_GCash);
                Console.WriteLine(DateTime.Now + " -" + lg.API_Display + lg.NineYou + lg.CardDetail_Account + presentMan + lg.UserCashPurchase_GCash);
                ds = UserCashPurchase.UserGCashPurchase_Query(serverIP, buyMan, presentMan, beginDate, endDate, sex, cateGory, isGift, isPresent);
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
                        Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length+1);
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0]);
                        strut.AddTagKey(TagName.AuShop_pname, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
                        strut.AddTagKey(TagName.AuShop_buytime, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[2]));
                        strut.AddTagKey(TagName.AuShop_allprice, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[3]);
                        strut.AddTagKey(TagName.AuShop_username, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[4]);
                        strut.AddTagKey(TagName.AuShop_getusername, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[5]));
                        strut.AddTagKey(TagName.AuShop_buytime2, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[6]);
                        strut.AddTagKey(TagName.AuShop_zone, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[7]);
                        strut.AddTagKey(TagName.AuShop_buyip, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
                   
                        structList[i - index] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.AUSHOP_ADMIN, ServiceKey.AUSHOP_USERGPURCHASE_QUERY_RESP, 9);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.UserCashPurchase_NoGCash, Msg_Category.AUSHOP_ADMIN, ServiceKey.AUSHOP_USERGPURCHASE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP(lg.UserCashPurchase_NoGCash, Msg_Category.AUSHOP_ADMIN, ServiceKey.AUSHOP_USERGPURCHASE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }
        }
        /// <summary>
        /// 查看玩家G币购买记录合计
        /// </summary>
        /// <returns></returns>
        public Message UserGCashPurchase_QuerySum()
        {
            string serverIP = null;
            string buyMan = null;
            string presentMan = null;
            DateTime beginDate;
            DateTime endDate;
            string sex = null;
            int cateGory = 0;
            string isPresent = null;
            string isGift = null;
            System.Data.DataSet ds = null;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
                buyMan = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_username).m_bValueBuffer);
                presentMan = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_getusername).m_bValueBuffer);
                TLV_Structure tlvStrut = new TLV_Structure(TagName.AuShop_BeginDate, 3, msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_BeginDate).m_bValueBuffer);
                beginDate = tlvStrut.toDate();
                tlvStrut = new TLV_Structure(TagName.AuShop_EndDate, 3, msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_EndDate).m_bValueBuffer);
                endDate = tlvStrut.toDate();
                sex = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_psex).m_bValueBuffer);
                isPresent = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_ispresent).m_bValueBuffer);
                isGift = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_islover).m_bValueBuffer);
                tlvStrut = new TLV_Structure(TagName.AuShop_pcategory, 4, msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_pcategory).m_bValueBuffer);
                cateGory = (int)tlvStrut.toInteger();
                SqlHelper.log.WriteLog(lg.API_Display + lg.NineYou + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.CardDetail_Account + buyMan + lg.UserCashPurchase_SumGCash);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.NineYou + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.CardDetail_Account + buyMan + lg.UserCashPurchase_SumGCash);
                ds = UserCashPurchase.UserGCashPurchaseSum_Query(serverIP, buyMan, presentMan, beginDate, endDate, sex, cateGory, isGift, isPresent);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return Message.COMMON_MES_RESP(Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]), Msg_Category.AUSHOP_ADMIN, ServiceKey.AUSHOP_USERGPURCHASE_SUM_QUERY_RESP,TagName.AuShop_GCashSum, TagFormat.TLV_INTEGER);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.UserCashPurchase_NoSumGCash, Msg_Category.AUSHOP_ADMIN, ServiceKey.AUSHOP_USERGPURCHASE_SUM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }
            }
            catch (System.Exception ex)
            {
                return Message.COMMON_MES_RESP(lg.UserCashPurchase_NoSumGCash, Msg_Category.AUSHOP_ADMIN, ServiceKey.AUSHOP_USERGPURCHASE_SUM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }

        }
        /// <summary->
        /// 玩家M币购买记录
        /// </summary>
        /// <returns></returns>
        public Message UserMCashPurchase_Query(int index,int pageSize)
        {
            string serverIP = null;
            string buyMan = null;
            string presentMan = null;
            DateTime beginDate;
            DateTime endDate;
            string sex = null;
            int cateGory = 0;
            string isPresent = null;
            string isGift = null;
            System.Data.DataSet ds = null;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
                buyMan = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_username).m_bValueBuffer);
                presentMan = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_getusername).m_bValueBuffer);
                TLV_Structure tlvStrut = new TLV_Structure(TagName.AuShop_BeginDate, 3, msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_BeginDate).m_bValueBuffer);
                beginDate = tlvStrut.toDate();
                tlvStrut = new TLV_Structure(TagName.AuShop_EndDate, 3, msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_EndDate).m_bValueBuffer);
                endDate = tlvStrut.toDate();
                sex = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_psex).m_bValueBuffer);
                isPresent = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_ispresent).m_bValueBuffer);
                isGift = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_islover).m_bValueBuffer);
                tlvStrut = new TLV_Structure(TagName.AuShop_pcategory, 4, msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_pcategory).m_bValueBuffer);
                cateGory = (int)tlvStrut.toInteger();
                SqlHelper.log.WriteLog(lg.API_Display + lg.NineYou + lg.CardDetail_Account + buyMan + lg.UserCashPurchase_MCash);
                Console.WriteLine(DateTime.Now + " -" + lg.API_Display + lg.NineYou + lg.CardDetail_Account + buyMan + lg.UserCashPurchase_MCash);
                ds = UserCashPurchase.UserMCashPurchase_Query(serverIP, buyMan, presentMan, beginDate, endDate, sex, cateGory, isGift, isPresent);
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
                        Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length+1);
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0]);
                        strut.AddTagKey(TagName.AuShop_pname, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[1]);
                        strut.AddTagKey(TagName.AuShop_buytime, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[2]));
                        strut.AddTagKey(TagName.AuShop_allprice, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[3]);
                        strut.AddTagKey(TagName.AuShop_username, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[4]);
                        strut.AddTagKey(TagName.AuShop_getusername, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[5]));
                        strut.AddTagKey(TagName.AuShop_buytime2, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[6]);
                        strut.AddTagKey(TagName.AuShop_zone, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[7]);
                        strut.AddTagKey(TagName.AuShop_buyip, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);

                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
                  
                        structList[i - index] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.AUSHOP_ADMIN, ServiceKey.AUSHOP_USERMPURCHASE_QUERY_RESP, 9);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.UserCashPurchase_NoMCash, Msg_Category.AUSHOP_ADMIN, ServiceKey.AUSHOP_USERMPURCHASE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP(lg.UserCashPurchase_NoMCash, Msg_Category.AUSHOP_ADMIN, ServiceKey.AUSHOP_USERMPURCHASE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }
        }
        /// <summary>
        /// 查看玩家M币购买记录合计
        /// </summary>
        /// <returns></returns>
        public Message UserMCashPurchase_QuerySum()
        {
            string serverIP = null;
            string buyMan = null;
            string presentMan = null;
            DateTime beginDate;
            DateTime endDate;
            string sex = null;
            int cateGory = 0;
            string isPresent = null;
            string isGift = null;
            System.Data.DataSet ds = null;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
                buyMan = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_username).m_bValueBuffer);
                presentMan = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_getusername).m_bValueBuffer);
                TLV_Structure tlvStrut = new TLV_Structure(TagName.AuShop_BeginDate, 3, msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_BeginDate).m_bValueBuffer);
                beginDate = tlvStrut.toDate();
                tlvStrut = new TLV_Structure(TagName.AuShop_EndDate, 3, msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_EndDate).m_bValueBuffer);
                endDate = tlvStrut.toDate();
                sex = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_psex).m_bValueBuffer);
                isPresent = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_ispresent).m_bValueBuffer);
                isGift = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_islover).m_bValueBuffer);
                tlvStrut = new TLV_Structure(TagName.AuShop_pcategory, 4, msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_pcategory).m_bValueBuffer);
                cateGory = (int)tlvStrut.toInteger();
                SqlHelper.log.WriteLog(lg.API_Display + lg.NineYou + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.CardDetail_Account + buyMan + lg.UserCashPurchase_SumMCash);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.NineYou + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.CardDetail_Account + buyMan + lg.UserCashPurchase_SumMCash);
                ds = UserCashPurchase.UserMCashPurchaseSum_Query(serverIP, buyMan, presentMan, beginDate, endDate, sex, cateGory, isGift, isPresent);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return Message.COMMON_MES_RESP(Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]), Msg_Category.AUSHOP_ADMIN, ServiceKey.AUSHOP_USERMPURCHASE_SUM_QUERY_RESP,TagName.AuShop_MCashSum, TagFormat.TLV_INTEGER);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.UserCashPurchase_NoSumMCash, Msg_Category.AUSHOP_ADMIN, ServiceKey.AUSHOP_USERMPURCHASE_SUM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }
            }
            catch (System.Exception ex)
            {
                return Message.COMMON_MES_RESP(lg.UserCashPurchase_NoSumMCash, Msg_Category.AUSHOP_ADMIN, ServiceKey.AUSHOP_USERMPURCHASE_SUM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }

        }
        /// <summary->
        /// 玩家积分查询
        /// </summary>
        /// <returns></returns>
        public Message UserIntegral_Query()
        {
            string serverIP = null;
            string account = null;

            System.Data.DataSet ds = null;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
                account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_userid).m_bValueBuffer);
                SqlHelper.log.WriteLog(lg.API_Display + lg.NineYou + lg.CardDetail_Account + account + lg.UserCashPurchase_Integral);
                Console.WriteLine(DateTime.Now + " -" + lg.API_Display + lg.NineYou + lg.CardDetail_Account + account + lg.UserCashPurchase_Integral);
                ds = UserCashPurchase.UserIntegral_Query(serverIP, account);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0]);
                        strut.AddTagKey(TagName.AuShop_username, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[1]));
                        strut.AddTagKey(TagName.AuShop_aup, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[2]));
                        strut.AddTagKey(TagName.AuShop_usefeesum, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]));
                        strut.AddTagKey(TagName.AuShop_useaupsum, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]));
                        strut.AddTagKey(TagName.AuShop_buyitemsum, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        structList[i] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.AUSHOP_ADMIN, ServiceKey.AUSHOP_USERINTERGRAL_QUERY_RESP,5);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.UserCashPurchase_NoIntegral, Msg_Category.AUSHOP_ADMIN, ServiceKey.AUSHOP_USERINTERGRAL_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP(lg.UserCashPurchase_NoIntegral, Msg_Category.AUSHOP_ADMIN, ServiceKey.AUSHOP_USERINTERGRAL_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }
        }
        /// <summary->
        /// 道具回收兑换记录
        /// </summary>
        /// <returns></returns>
        public Message UserAvatarItemRev_Query(int index,int pageSize)
        {
            string serverIP = null;
            string account = null;
            DateTime begDate;
            DateTime endDate;
            System.Data.DataSet ds = null;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
                account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_userid).m_bValueBuffer);
                TLV_Structure tlvStrut = new TLV_Structure(TagName.AuShop_BeginDate, 4, msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_BeginDate).m_bValueBuffer);
                begDate = tlvStrut.toDate();
                tlvStrut = new TLV_Structure(TagName.AuShop_EndDate, 4, msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_EndDate).m_bValueBuffer);
                endDate = tlvStrut.toDate();
                SqlHelper.log.WriteLog(lg.API_Display + lg.NineYou + lg.CardDetail_Account + account + lg.UserCashPurchase_ItemChange);
                Console.WriteLine(DateTime.Now + " -" + lg.API_Display + lg.NineYou + lg.CardDetail_Account + account + lg.UserCashPurchase_ItemChange);
                ds = UserCashPurchase.UserAvatarItemRev_Query(serverIP, account, begDate, endDate);
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
                        Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length+1);
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0]);
                        strut.AddTagKey(TagName.AuShop_username, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[1]));
                        strut.AddTagKey(TagName.AuShop_pinttime, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
                        strut.AddTagKey(TagName.AuShop_buyip, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[3]);
						strut.AddTagKey(TagName.AuShop_pmark3, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]));
						strut.AddTagKey(TagName.AuShop_aup, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[5]));
						strut.AddTagKey(TagName.AuShop_price, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                   
                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
                      
                        structList[i - index] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.AUSHOP_ADMIN, ServiceKey.AUSHOP_USERMPURCHASE_QUERY_RESP,7);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.UserCashPurchase_NoItemChange, Msg_Category.AUSHOP_ADMIN, ServiceKey.AUSHOP_USERMPURCHASE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP(lg.UserCashPurchase_NoItemChange, Msg_Category.AUSHOP_ADMIN, ServiceKey.AUSHOP_USERMPURCHASE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }
        }
		/// <summary->
		/// 道具回收兑换详细记录
		/// </summary>
		/// <returns></returns>
		public Message UserAvatarItemRevDetail_Query()
		{
			string serverIP = null;
			string orderID = null;
			System.Data.DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AU_ServerIP).m_bValueBuffer);
				orderID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.AuShop_orderid).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Display + lg.NineYou + lg.CardDetail_Account + orderID + lg.UserCashPurchase_ItemChangeDetails);
				Console.WriteLine(DateTime.Now + " -" + lg.API_Display + lg.NineYou + lg.CardDetail_Account + orderID + lg.UserCashPurchase_ItemChangeDetails);
				ds = UserCashPurchase.UserAvatarItemRevDetail_Query(serverIP, orderID);
				if (ds != null && ds.Tables[0].Rows.Count > 0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[0].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.AuShop_pname, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.AuShop_username, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.AuShop_getusername, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[3]));
						strut.AddTagKey(TagName.AuShop_buytime2, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[4]);
						strut.AddTagKey(TagName.AuShop_zone, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.AUSHOP_ADMIN, ServiceKey.AUSHOP_AVATARECOVER_DETAIL_QUERY_RESP,5);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.UserCashPurchase_NoItemChangeDetails, Msg_Category.AUSHOP_ADMIN, ServiceKey.AUSHOP_AVATARECOVER_DETAIL_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.UserCashPurchase_NoItemChangeDetails, Msg_Category.AUSHOP_ADMIN, ServiceKey.AUSHOP_AVATARECOVER_DETAIL_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
        
    }
}
