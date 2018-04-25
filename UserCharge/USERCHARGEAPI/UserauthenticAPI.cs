using System;
using System.Text;
using System.Collections;
using Common.API;
using Common.Logic;
using Common.DataInfo;
using UserCharge.UserChargeInfo;
using System.Configuration;

namespace UserCharge.UserChargeAPI
{
	/// <summary>
	/// UserauthenticAPI 的摘要说明。
	/// </summary>
	public class UserauthenticAPI
	{
		Message msg = null;
		public UserauthenticAPI(byte[] packet)
		{
			msg = new Message(packet,(uint)packet.Length);
		}
		/// <summary>
		/// 显示令牌的当前状态
		/// </summary>
		/// <returns></returns>
		public Message Token_Status()
		{
			string esn = null;
			string code = null;
			string description = null;
			string service = null;
			try
			{
				
				esn = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.TOKEN_esn).m_bValueBuffer);
				service = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.TOKEN_service).m_bValueBuffer);
				SqlHelper.log.WriteLog("显示令牌" + esn + "的当前状态!");
				Console.WriteLine(DateTime.Now+" - 显示令牌" + esn + "的当前状态!");
				ArrayList ds = UserauthenticInfo.Token_Status("asas",service,esn);
				if (ds!=null && ds.Count>0)
				{					
					for(int i = 0;i < ds.Count;i++)
					{
						ArrayList para = (ArrayList)ds[i];
						if(para[0].ToString() == "code")
							code = ConfigurationSettings.AppSettings[para[1].ToString()];
						if(para[0].ToString() == "description")
							description = para[1].ToString();
					}
					Query_Structure[] structList = new Query_Structure[1];
					Query_Structure strut = new Query_Structure(2);
					byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, code);
					strut.AddTagKey(TagName.TOKEN_code, TagFormat.TLV_STRING, (uint)bytes.Length,bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,description);
					strut.AddTagKey(TagName.TOKEN_description, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					structList[0] = strut;
					return Message.COMMON_MES_RESP(structList, Msg_Category.CARD_ADMIN, ServiceKey.TOKEN_TOKENSTATUS_QUERY_RESP,2);
				}
				else
				{
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.CARD_ADMIN, ServiceKey.TOKEN_TOKENSTATUS_QUERY_RESP);
				}
			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP("FAILURE", Msg_Category.CARD_ADMIN, ServiceKey.TOKEN_TOKENSTATUS_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 修改用户令牌
		/// </summary>
		/// <returns></returns>
		public Message Modify_User_Token()
		{
			string userName = null;
			string esn = null;
			string code = null;
			string description = null;
			string service = null;
			try
			{
				
				esn = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.TOKEN_esn).m_bValueBuffer);
				userName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.TOKEN_username).m_bValueBuffer);
				service = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.TOKEN_service).m_bValueBuffer);
				SqlHelper.log.WriteLog("修改用户" + userName + "的当前状态!");
				Console.WriteLine(DateTime.Now+" - 修改用户" + userName + "的当前状态!");
				ArrayList ds = UserauthenticInfo.Modify_User_Token("asas",service,userName,esn);
				if (ds!=null && ds.Count>0)
				{					
					for(int i = 0;i < ds.Count;i++)
					{
						ArrayList para = (ArrayList)ds[i];
						if(para[0].ToString() == "code")
							code = ConfigurationSettings.AppSettings[para[1].ToString()];
						if(para[0].ToString() == "description")
							description = para[1].ToString();
					}
					Query_Structure[] structList = new Query_Structure[1];
					Query_Structure strut = new Query_Structure(2);
					byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, code);
					strut.AddTagKey(TagName.TOKEN_code, TagFormat.TLV_STRING, (uint)bytes.Length,bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,description);
					strut.AddTagKey(TagName.TOKEN_description, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					structList[0] = strut;
					return Message.COMMON_MES_RESP(structList, Msg_Category.CARD_ADMIN, ServiceKey.TOKEN_MODIFYUSER_QUERY_RESP,2);
				}
				else
				{
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.CARD_ADMIN, ServiceKey.TOKEN_MODIFYUSER_QUERY_RESP);
				}
			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP("FAILURE", Msg_Category.CARD_ADMIN, ServiceKey.TOKEN_MODIFYUSER_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
	}
}
