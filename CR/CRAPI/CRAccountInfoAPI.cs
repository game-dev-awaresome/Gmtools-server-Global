using System;

using System.Text;
using Common.Logic;
using CR.CRDataInfo;
using Common.DataInfo;
using lg = Common.API.LanguageAPI;
namespace CR.CRAPI
{
    public class CRAccountInfoAPI
    {
        Message msg = null;
        public CRAccountInfoAPI(byte[] packet)
        {
            msg = new Message(packet, (uint)packet.Length);
        }
        /// <summary>
        /// 查看该玩家的帐号信息
        /// </summary>
        /// <returns></returns>
        public Message CR_Account_Query()
        {
            System.Data.DataSet result = null;
            string serverIP = "";
            string account = "";
            string userNick = "";
            int action = 0;
            try
            {
                serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_ServerIP).m_bValueBuffer);
                TLV_Structure tlv = new TLV_Structure(TagName.CR_ACTION, 4, msg.m_packet.m_Body.getTLVByTag(TagName.CR_ACTION).m_bValueBuffer);
                action = (int)tlv.toInteger();
                if (action == 1)
                {
                    account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_ACCOUNT).m_bValueBuffer);
                }
                else if (action == 2)
                {
                    userNick = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_NickName).m_bValueBuffer);
                }

                SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_AccountInfoAPI_Account + account + lg.CR_AccountInfoAPI_AccountInfo);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_AccountInfoAPI_Account + account + lg.CR_AccountInfoAPI_AccountInfo);
                result = CRAccountInfo.CR_Account_Query(serverIP, account,userNick,action);
                if (result !=null && result.Tables[0].Rows.Count>0)
                {
                    Query_Structure[] structList = new Query_Structure[result.Tables[0].Rows.Count];
                    for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                    {
                        Query_Structure strut = new Query_Structure(9);
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(result.Tables[0].Rows[0].ItemArray[0]));
                        strut.AddTagKey(TagName.CR_PSTID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes); ;
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(result.Tables[0].Rows[0].ItemArray[1]));
                        strut.AddTagKey(TagName.CR_Passord, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);

                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(result.Tables[0].Rows[0].ItemArray[2]));
                        strut.AddTagKey(TagName.CR_UserID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(result.Tables[0].Rows[0].ItemArray[3]));
                        strut.AddTagKey(TagName.CR_ACCOUNT, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);

                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(result.Tables[0].Rows[0].ItemArray[4]));
                        strut.AddTagKey(TagName.CR_NickName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(result.Tables[0].Rows[0].ItemArray[5]));
                        strut.AddTagKey(TagName.CR_SEX, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(result.Tables[0].Rows[0].ItemArray[6]));
                        strut.AddTagKey(TagName.CR_NUMBER, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(result.Tables[0].Rows[0].ItemArray[7]));
                        strut.AddTagKey(TagName.CR_ServerIP, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(result.Tables[0].Rows[0].ItemArray[8]));
                        strut.AddTagKey(TagName.CR_ActiveDate, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);

                        structList[i] = strut;
                    }
                        return Message.COMMON_MES_RESP(structList, Msg_Category.CR_ADMIN, ServiceKey.CR_ACCOUNT_QUERY_RESP,9);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.CR_AccountInfoAPI_NoAccount, Msg_Category.CR_ADMIN, ServiceKey.CR_ACCOUNT_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

                }
            }
            catch (System.Exception)
            {
                return Message.COMMON_MES_RESP(0, Msg_Category.CR_ADMIN, ServiceKey.CR_ACCOUNT_QUERY_RESP,TagName.CR_STATUS,TagFormat.TLV_INTEGER);
            }
        }
        /// <summary>
        /// 查看该玩家是否被激活
        /// </summary>
        /// <returns></returns>
        public Message CR_AccountActive_Query()
        {
            System.Data.DataSet result = null;
            int status = -1;
            string serverIP = "61.129.90.151";
            string account = null;
            string passwd = null;
            string number = null;
            try
            {
                //serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_ServerIP).m_bValueBuffer);
              //  account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_ACCOUNT).m_bValueBuffer);
                passwd = account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_Passord).m_bValueBuffer);
                number = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.CR_NUMBER).m_bValueBuffer);
                SqlHelper.log.WriteLog(lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_AccountInfoAPI_Account + account + lg.CR_AccountInfoAPI_ActiveState);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.CR_CR + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.CR_AccountInfoAPI_Account + account + lg.CR_AccountInfoAPI_ActiveState);
                result = CRAccountInfo.CR_AccountActive_Query(account,passwd,number);
                if (result !=null && result.Tables[0].Rows.Count>0)
                {
                    //密码错误
                    if (!result.Tables[0].Rows[0].ItemArray[1].Equals(passwd))
                    {
                        status = 2;
                        byte[] bgMsg_Status = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, status);
                        TLV_Structure Msg_Status = new TLV_Structure(TagName.CR_STATUS, (uint)bgMsg_Status.Length, bgMsg_Status);
                        byte[] baMsg_Pass = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, result.Tables[0].Rows[0].ItemArray[1]);
                        TLV_Structure Msg_Pass = new TLV_Structure(TagName.CR_Passord, (uint)baMsg_Pass.Length, baMsg_Pass);
                        Packet_Body body = new Packet_Body(new TLV_Structure[] { Msg_Status, Msg_Pass }, 2);
                        Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(), Msg_Category.CR_ADMIN,
                            ServiceKey.CR_ACCOUNTACTIVE_QUERY_RESP, body.m_uiBodyLen);
                        return new Message(new Packet(head, body));

                    }
                    //激活码未被使用过
                    else if (result.Tables[0].Rows[0].ItemArray[2].Equals("n"))
                    {
                        status = 3;
                        return Message.COMMON_MES_RESP(status, Msg_Category.CR_ADMIN, ServiceKey.CR_ACCOUNTACTIVE_QUERY, TagName.CR_STATUS, TagFormat.TLV_INTEGER);

                    }
                    // 激活码已被使用
                    else if (result.Tables[0].Rows[0].ItemArray[2].Equals("y"))
                    {
                        status = 4;
                        byte[] bgMsg_Status = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, status);
                        TLV_Structure Msg_Status = new TLV_Structure(TagName.CR_STATUS, (uint)bgMsg_Status.Length, bgMsg_Status);
                        byte[] baMsg_Account = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, result.Tables[0].Rows[0].ItemArray[3]);
                        TLV_Structure Msg_Account = new TLV_Structure(TagName.CR_ACCOUNT, (uint)baMsg_Account.Length, baMsg_Account);
                        Packet_Body body = new Packet_Body(new TLV_Structure[] { Msg_Status, Msg_Account }, 2);
                        Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(), Msg_Category.CR_ADMIN,
                            ServiceKey.CR_ACCOUNTACTIVE_QUERY_RESP, body.m_uiBodyLen);
                        return new Message(new Packet(head, body));
                    }
                    else
                    {
                        return Message.COMMON_MES_RESP(1, Msg_Category.CR_ADMIN, ServiceKey.CR_ACCOUNTACTIVE_QUERY, TagName.CR_STATUS, TagFormat.TLV_INTEGER);

                    }
                    /*// 查询帐号未被激活
                    else if (!result.Tables[0].Rows[0].ItemArray[3].Equals(account))
                    {
                        status = 5;
                    }
                    // 查询帐号已被激活
                    else if (result.Tables[0].Rows[0].ItemArray[3].Equals(account))
                    {
                        status = 6;
                    }*/
                }
                return Message.COMMON_MES_RESP(1, Msg_Category.CR_ADMIN, ServiceKey.CR_ACCOUNTACTIVE_QUERY, TagName.CR_STATUS, TagFormat.TLV_INTEGER);

            }
            catch (System.Exception)
            {
                return Message.COMMON_MES_RESP(1, Msg_Category.CR_ADMIN, ServiceKey.CR_ACCOUNTACTIVE_QUERY, TagName.CR_STATUS, TagFormat.TLV_INTEGER);
            }
        }


    }
}
