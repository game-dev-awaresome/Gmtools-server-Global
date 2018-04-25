using System;

using System.Text;
using O2JAM.O2JAMDataInfo;
using Common.Logic;
using Common.DataInfo;
using lg = Common.API.LanguageAPI;
namespace O2JAM.O2JAMAPI
{
    class O2JAMItemLogInfoAPI
    {
        Message msg = null;
        public O2JAMItemLogInfoAPI(byte[] packet)
        {
            msg = new Message(packet, (uint)packet.Length);
        }
        /// <summary>
        /// 玩家的充值明细查询
        /// </summary>
        /// <returns></returns>
        public Message userChargeDetail_Query(int index,int pageSize)
        {
            string serverIP = null;
            string account = null;
            DateTime beginDate ;
            DateTime endDate;
            System.Data.DataSet ds = null;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
                account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
                TLV_Structure tlvStrut = new TLV_Structure(TagName.SDO_BeginTime,3,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_BeginTime).m_bValueBuffer);
				beginDate =tlvStrut.toDate();
                tlvStrut = new TLV_Structure(TagName.SDO_EndTime,3,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_EndTime).m_bValueBuffer);
				endDate =tlvStrut.toDate();
                SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.O2JAM_CharacterInfoAPI_Account + account + lg.O2JAM_ItemLogInfoAPI_FillDetail);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.O2JAM_CharacterInfoAPI_Account + account + lg.O2JAM_ItemLogInfoAPI_FillDetail);
                //请求玩家身上的道具
                ds = ItemLogInfo.userChargeDetail_Query(serverIP,account,beginDate,endDate);
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
                    for (int i = 0; i < index + pageSize; i++)
                    {
                        Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length+1);
                        //用户ID
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0]);
                        strut.AddTagKey(TagName.SDO_Account, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        //充值日期
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, ds.Tables[0].Rows[i].ItemArray[1]);
                        strut.AddTagKey(TagName.SDO_ShopTime, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        //充值金额
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[2]);
                        strut.AddTagKey(TagName.SDO_MCash, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
                        structList[i - index] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERMCASH_QUERY_RESP, 3);
                }
                else
                    return Message.COMMON_MES_RESP(lg.O2JAM_ItemLogInfoAPI_NoChargeRecord, Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERMCASH_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

            }
            catch (Common.Logic.Exception ex)
            {
                return Message.COMMON_MES_RESP(ex.Message, Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERMCASH_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }
        }
        /// <summary>
        /// 玩家的充值明细合计
        /// </summary>
        /// <returns></returns>
        public Message userChargeSum_Query()
        {
            System.Data.DataSet result = null;
            string serverIP = null;
            string account = null;
            DateTime beginDate;
            DateTime endDate;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
                account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
                TLV_Structure tlvStrut = new TLV_Structure(TagName.SDO_BeginTime, 3, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_BeginTime).m_bValueBuffer);
                beginDate = tlvStrut.toDate();
                tlvStrut = new TLV_Structure(TagName.SDO_EndTime, 3, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_EndTime).m_bValueBuffer);
                endDate = tlvStrut.toDate();
                SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.O2JAM_CharacterInfoAPI_Account + account + lg.O2JAM_ItemLogInfoAPI_SumFillDetail);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.O2JAM_CharacterInfoAPI_Account + account + lg.O2JAM_ItemLogInfoAPI_SumFillDetail);
                result = ItemLogInfo.userChargeSum_Query(serverIP, account, beginDate, endDate);
                if (result != null && result.Tables[0].Rows.Count>0)
                {

                    return Message.COMMON_MES_RESP(Convert.ToInt32(result.Tables[0].Rows[0].ItemArray[1]), Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERCHARAGESUM_QUERY_RESP, TagName.SDO_ChargeSum, TagFormat.TLV_INTEGER);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.O2JAM_ItemLogInfoAPI_NoSumChargeRecord, Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERCHARAGESUM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }

            }
            catch (System.Exception ex)
            {
                return Message.COMMON_MES_RESP(lg.O2JAM_ItemLogInfoAPI_NoSumChargeRecord, Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERCHARAGESUM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }

        }
        /// <summary>
        /// 玩家的Ｇ币查询
        /// </summary>
        /// <returns></returns>
        public Message userGCash_Query()
        {
            System.Data.DataSet result = null;
            string serverIP = null;
            string account = null;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
                account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
                SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.O2JAM_CharacterInfoAPI_Account + account + lg.O2JAM_ItemLogInfoAPI_GCash + lg.O2JAM_ItemLogInfoAPI_Sum);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.O2JAM_CharacterInfoAPI_Account + account + lg.O2JAM_ItemLogInfoAPI_GCash + lg.O2JAM_ItemLogInfoAPI_Sum);
                result = ItemLogInfo.userGCash_Query(serverIP, account);
                if (result != null && result.Tables[0].Rows.Count > 0)
                {

                    return Message.COMMON_MES_RESP(Convert.ToInt32(result.Tables[0].Rows[0].ItemArray[2]), Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERGCASH_QUERY_RESP, TagName.SDO_GCash, TagFormat.TLV_INTEGER);
                }
                else
                {
                    return Message.COMMON_MES_RESP(0, Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERGCASH_QUERY_RESP, TagName.SDO_GCash, TagFormat.TLV_INTEGER);
                }

            }
            catch (System.Exception)
            {
                return Message.COMMON_MES_RESP(0, Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERGCASH_QUERY_RESP, TagName.SDO_GCash, TagFormat.TLV_INTEGER);
            }

        }
        /// <summary>
        /// 玩家的G币补发
        /// </summary>
        /// <returns></returns>
        public Message O2JAM_GCash_Update()
        {
            int result = -1;
            int operateUserID = 0;
            int userIndexID = 0;
            string serverIP = null;
            int GCash = 0;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.o2jam_USER_INDEX_ID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_USER_INDEX_ID).m_bValueBuffer);
				userIndexID = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
                operateUserID = (int)strut.toInteger();
                strut = new TLV_Structure(TagName.o2jam_GEM, 4, msg.m_packet.m_Body.getTLVByTag(TagName.o2jam_GEM).m_bValueBuffer);
                GCash = (int)strut.toInteger();
                result = ItemLogInfo.O2JAM_UserMcash_addG(operateUserID, serverIP, userIndexID, GCash);
                if (result == -1)
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.O2JAM_CharacterInfoAPI_Account + userIndexID + lg.O2JAM_CharacterInfoAPI_NoAccount);
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.O2JAM_CharacterInfoAPI_Account + userIndexID + lg.O2JAM_CharacterInfoAPI_NoAccount);
                    return Message.COMMON_MES_RESP(lg.O2JAM_CharacterInfoAPI_NoAccount, Msg_Category.O2JAM_ADMIN, ServiceKey.O2JAM_USERGCASH_UPDATE_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

                }
                else if (result == 1)
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.O2JAM_CharacterInfoAPI_Account + userIndexID + lg.O2JAM_ItemLogInfoAPI_Compensate + GCash + lg.O2JAM_ItemLogInfoAPI_GCash + lg.API_Success + "!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.O2JAM_CharacterInfoAPI_Account + userIndexID + lg.O2JAM_ItemLogInfoAPI_Compensate + GCash + lg.O2JAM_ItemLogInfoAPI_GCash + lg.API_Success + "!");
                    return Message.COMMON_MES_RESP("SUCESS", Msg_Category.O2JAM_ADMIN, ServiceKey.O2JAM_USERGCASH_UPDATE_RESP);
                }
                else
                {
                    SqlHelper.log.WriteLog(lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.O2JAM_CharacterInfoAPI_Account + userIndexID + lg.O2JAM_ItemLogInfoAPI_Compensate + GCash + lg.O2JAM_ItemLogInfoAPI_GCash + lg.API_Failure + "!");
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.O2JAM_O2JAM + "+>" + lg.API_CommonAPI_ServerIP + serverIP + lg.O2JAM_CharacterInfoAPI_Account + userIndexID + lg.O2JAM_ItemLogInfoAPI_Compensate + GCash + lg.O2JAM_ItemLogInfoAPI_GCash + lg.API_Failure + "!");
                    return Message.COMMON_MES_RESP("FAILURE", Msg_Category.O2JAM_ADMIN, ServiceKey.O2JAM_USERGCASH_UPDATE_RESP);
                }
            }
            catch (Common.Logic.Exception ex)
            {
                return Message.COMMON_MES_RESP(ex.Message, Msg_Category.O2JAM_ADMIN, ServiceKey.O2JAM_USERGCASH_UPDATE_RESP);
            }

        }

    }
}
