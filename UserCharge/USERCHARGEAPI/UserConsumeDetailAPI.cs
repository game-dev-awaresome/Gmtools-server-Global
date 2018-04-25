using System;

using System.Text;
using Common.API;
using Common.Logic;
using Common.DataInfo;
using UserCharge.UserChargeInfo;
using lg = Common.API.LanguageAPI;
namespace UserCharge.UserChargeAPI
{
    public class UserConsumeDetailAPI
    {
        Message msg = null;
        public UserConsumeDetailAPI(byte[] packet)
        {
            msg = new Message(packet, (uint)packet.Length); 
        }
         /// <summary->
        /// 玩家人物资料信息
        /// </summary>
        /// <returns></returns>
        public Message UserConsumeDetail_Query()
        {
            string account = null;
            string cardID = null;
            string cardPwd = null;
            int actionType = 0;
            System.Data.DataSet ds = null;
            try
            {
                account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CARD_use_userid).m_bValueBuffer);
                cardID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CARD_cardnum).m_bValueBuffer);
                cardPwd = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CARD_cardpass).m_bValueBuffer);
                TLV_Structure tlvStrut = new TLV_Structure(TagName.CARD_ActionType, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CARD_ActionType).m_bValueBuffer);
                actionType = (int)tlvStrut.toInteger();
                SqlHelper.log.WriteLog(lg.API_Display + lg.NineYou + lg.CardDetail_Account + lg.UserConsumeDetail_ConsumeInfo);
                Console.WriteLine(DateTime.Now + " - "+lg.API_Display + lg.NineYou + lg.CardDetail_Account + lg.UserConsumeDetail_ConsumeInfo);
                ds = CardDetailInfo.CardDetailInfo_Query(account, cardID, cardPwd, actionType);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
                        strut.AddTagKey(TagName.CARD_PDID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[1]);
                        strut.AddTagKey(TagName.CARD_PDkey, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
                        strut.AddTagKey(TagName.CARD_PDCardType, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[3]);
                        strut.AddTagKey(TagName.CARD_PDFrom, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]));
                        strut.AddTagKey(TagName.CARD_cardnum, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[5]));
                        strut.AddTagKey(TagName.CARD_cardpass, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_MONEY, Convert.ToDouble(ds.Tables[0].Rows[i].ItemArray[6]));
                        strut.AddTagKey(TagName.CARD_price, TagFormat.TLV_MONEY, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[7]));
                        strut.AddTagKey(TagName.CARD_PDaction, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[8]));
                        strut.AddTagKey(TagName.CARD_PDuserid, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[9]);
                        strut.AddTagKey(TagName.CARD_PDusername, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[10]));
                        strut.AddTagKey(TagName.CARD_PDgetuserid, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[11]);
                        strut.AddTagKey(TagName.CARD_PDgetusername, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        structList[i] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERCHARGEDETAIL_QUERY, 11);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.UserConsumeDetail_NoConsumeInfo, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERCHARGEDETAIL_QUERY, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }

            }
            catch (Common.Logic.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP(ex.Message, Msg_Category.CARD_ADMIN, ServiceKey.CARD_USERCHARGEDETAIL_QUERY, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }
        }

    }
}
