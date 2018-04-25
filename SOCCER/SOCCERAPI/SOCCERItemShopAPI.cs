/*
 * Add by KeHuaQing 
 * 2006-11-21
 */
using System;
using System.Text;
using Common.Logic;
using Common.DataInfo;
using Soccer.SOCCERDataInfo;

namespace Soccer.SOCCERAPI
{
	/// <summary>
	/// SOCCERItemShopAPI ��ժҪ˵����
	/// </summary>
	public class SOCCERItemShopAPI
	{
		Message msg = null;
		public SOCCERItemShopAPI(byte[] packet)
		{
			msg = new Message(packet, (uint)packet.Length);
		}
        /// <summary>
        /// ��ѯ��ҹ����¼�����ͼ�¼
        /// </summary>
        /// <param name="index">�ڼ�ҳ<</param>
        /// <param name="pageSize">ÿҳ��¼��</param>
        /// <returns></returns>
		public Message Soccer_UserTrade_Query(int index, int pageSize)
		{
			System.Data.DataSet ds = null;
			string serverIP = null;
			string SenderUserName = null;
			string ReceiveUserName = null;
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_ServerIP).m_bValueBuffer);
				SenderUserName = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_SenderUserName).m_bValueBuffer);
				ReceiveUserName = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_ReceiveUserName).m_bValueBuffer);				

				SqlHelper.log.WriteLog("��������+>��������ַ" + CommonInfo.serverIP_Query(serverIP) + "���" + SenderUserName + "�����¼!");
				Console.WriteLine(DateTime.Now + " - ��������+>��������ַ" + CommonInfo.serverIP_Query(serverIP) + "���" + SenderUserName + "�����¼!");
				ds = SOCCERItemShopInfo.Soccer_UserTrade_Query(serverIP,SenderUserName,ReceiveUserName);
				if (ds !=null && ds.Tables[0].Rows.Count>0)
				{
					//��ҳ��
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
					for (int i=index;i<index+pageSize;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length + 1);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
						strut.AddTagKey(TagName.Soccer_account_idx, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[1]));
						strut.AddTagKey(TagName.Soccer_charname, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[2]));
						strut.AddTagKey(TagName.Soccer_i_name, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]));
						strut.AddTagKey(TagName.Soccer_item_type, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]));
						strut.AddTagKey(TagName.Soccer_item_equip, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[5]));
						strut.AddTagKey(TagName.Soccer_c_date, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);						
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[6]));
						strut.AddTagKey(TagName.Soccer_deleted_date, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[7]));
						strut.AddTagKey(TagName.Soccer_ReceiveUserName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[8]));
						strut.AddTagKey(TagName.Soccer_idx, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[9]));
						strut.AddTagKey(TagName.Soccer_charidx, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[10]));
						strut.AddTagKey(TagName.Soccer_charname, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
						structList[i - index] = strut;

					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_USERTRADE_QUERY_RESP, 12);
				}
				else
				{
					return Message.COMMON_MES_RESP("û�и���ҵĹ����¼!", Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_USERTRADE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
			}
			catch (System.Exception ex)
			{
				return Message.COMMON_MES_RESP("û�и���ҵĹ����¼!", Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_USERTRADE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// ɾ��������ϵ��ߡ�����
		/// </summary>
		/// <returns></returns>
		public Message Soccer_Item_Skill_Delete()
		{
			int operateUserID = 0;
			int result = -1;
			string serverIP = "";
			int char_idx = 0;
			int item_type = 0;
			int item_idx = 0;
			int item_equip = 0;
			string str_ddate = "";
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_ServerIP).m_bValueBuffer);
				strut = new TLV_Structure(TagName.Soccer_charidx, 4, msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_charidx).m_bValueBuffer);
				char_idx = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.Soccer_item_type, 4, msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_item_type).m_bValueBuffer);
				item_type = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.Soccer_idx, 4, msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_idx).m_bValueBuffer);
				item_idx = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.Soccer_item_equip, 4, msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_item_equip).m_bValueBuffer);
				item_equip = (int)strut.toInteger();
				str_ddate = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_deleted_date).m_bValueBuffer);

				result = SOCCERItemShopInfo.Soccer_Item_Skill_Delete(operateUserID,serverIP,char_idx,item_type,item_idx,item_equip,str_ddate);
				if (result == 0)
				{
					SqlHelper.log.WriteLog("��������+>��������ַ" + CommonInfo.serverIP_Query(serverIP) + "��ɫ" + char_idx + "�¹����ϵĵ��߻���ɾ���ɹ�!");
					Console.WriteLine(DateTime.Now + " - ��������+>��������ַ" + CommonInfo.serverIP_Query(serverIP) + "��ɫ" + char_idx + "�¹����ϵĵ��߻���ɾ���ɹ�!");
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_ITEM_SKILL_MODIFY);
				}
				else
				{
					SqlHelper.log.WriteLog("��������+>��������ַ" + CommonInfo.serverIP_Query(serverIP) + "��ɫ" + char_idx + "�¹����ϵĵ��߻���ɾ��ʧ��!");
					Console.WriteLine(DateTime.Now + " - ��������+>��������ַ" + CommonInfo.serverIP_Query(serverIP) + "��ɫ" + char_idx + "�¹����ϵĵ��߻���ɾ��ʧ��!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_ITEM_SKILL_MODIFY_RESP);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_ITEM_SKILL_MODIFY_RESP);
			}
		}

		/// <summary>
		/// ��ѯ������ϵĵ��ߡ�����
		/// </summary>
		/// <param name="index">�ڼ�ҳ</param>
		/// <param name="pageSize">ÿҳ��¼��</param>
		/// <returns></returns>
		public Message Soccer_Account_Itemskill_Query(int index, int pageSize)
		{
			System.Data.DataSet ds = null;
			string serverIP = null;
			string SenderUserName = null;
			int item_type = 0;
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_ServerIP).m_bValueBuffer);
				SenderUserName = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_SenderUserName).m_bValueBuffer);
				TLV_Structure strut1 = new TLV_Structure(TagName.Soccer_item_type,4,msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_item_type).m_bValueBuffer);
				item_type = (int)strut1.toInteger();				

				SqlHelper.log.WriteLog("��������+>��������ַ" + CommonInfo.serverIP_Query(serverIP) + "���" + SenderUserName + "���ϵ��ߺͼ���!");
				Console.WriteLine(DateTime.Now + " - ��������+>��������ַ" + CommonInfo.serverIP_Query(serverIP) + "���" + SenderUserName + "���ϵ��ߺͼ���!");
				ds = SOCCERItemShopInfo.Soccer_Account_Itemskill_Query(serverIP,SenderUserName,item_type);
				if (ds !=null && ds.Tables[0].Rows.Count>0)
				{
					//��ҳ��
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
					for (int i=index;i<index+pageSize;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length + 1);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
						strut.AddTagKey(TagName.Soccer_account_idx, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[1]));
						strut.AddTagKey(TagName.Soccer_charname, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[2]));
						strut.AddTagKey(TagName.Soccer_i_name, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]));
						strut.AddTagKey(TagName.Soccer_item_type, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]));
						strut.AddTagKey(TagName.Soccer_item_equip, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[5]));
						strut.AddTagKey(TagName.Soccer_c_date, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);						
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[6]));
						strut.AddTagKey(TagName.Soccer_deleted_date, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[7]));
						strut.AddTagKey(TagName.Soccer_ReceiveUserName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[8]));
						strut.AddTagKey(TagName.Soccer_idx, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[9]));
						strut.AddTagKey(TagName.Soccer_charidx, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[10]));
						strut.AddTagKey(TagName.Soccer_charname, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
						structList[i - index] = strut;

					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_ACCOUNT_ITEMSKILL_QUER_RESPY, 12);
				}
				else
				{
					return Message.COMMON_MES_RESP("�������û�е��߻���!", Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_ACCOUNT_ITEMSKILL_QUER_RESPY, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
			}
			catch (System.Exception ex)
			{
				return Message.COMMON_MES_RESP("�������û�е��߻���!", Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_ACCOUNT_ITEMSKILL_QUER_RESPY, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}

		/// <summary>
		/// ��������͵���
		/// </summary>
		/// <returns></returns>
		public Message Soccer_Itemshop_Insert()
		{
			int operateUserID = 0;
			int result = -1;
			string serverIP = "";
			string account_name = "";
			string char_name = "";
			string title = "";
			string content = "";
			int char_idx = 0;
			int item_type = 1;
			int item_idx = 0;
			int item_equip = 0;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_ServerIP).m_bValueBuffer);
				strut = new TLV_Structure(TagName.Soccer_charidx, 4, msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_charidx).m_bValueBuffer);
				char_idx = (int)strut.toInteger();
//				strut = new TLV_Structure(TagName.Soccer_item_type, 4, msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_item_type).m_bValueBuffer);
//				item_type = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.Soccer_idx, 4, msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_idx).m_bValueBuffer);
				item_idx = (int)strut.toInteger();
//				strut = new TLV_Structure(TagName.Soccer_item_equip, 4, msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_item_equip).m_bValueBuffer);
//				item_equip = (int)strut.toInteger();
				char_name = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_charname).m_bValueBuffer);
				account_name = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_loginId).m_bValueBuffer);
				title = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_title).m_bValueBuffer);
				content = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_content).m_bValueBuffer);

				result = SOCCERItemShopInfo.Soccer_Itemshop_Insert(operateUserID,serverIP,account_name,char_idx,char_name,title,content,item_idx,item_type,item_equip);
				if (result == 0)
				{
					SqlHelper.log.WriteLog("��������+>��������ַ" + CommonInfo.serverIP_Query(serverIP) + "��ɫ" + char_name + "���͵��߻��ܳɹ�!");
					Console.WriteLine(DateTime.Now + " - ��������+>��������ַ" + CommonInfo.serverIP_Query(serverIP) + "��ɫ" + char_name + "���͵��߻��ܳɹ�!");
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_ITEMSHOP_INSERT_RESPY);
				}
				else
				{
					SqlHelper.log.WriteLog("��������+>��������ַ" + CommonInfo.serverIP_Query(serverIP) + "��ɫ" + char_name + "���͵��߻���ʧ��!");
					Console.WriteLine(DateTime.Now + " - ��������+>��������ַ" + CommonInfo.serverIP_Query(serverIP) + "��ɫ" + char_name + "���͵��߻���ʧ��!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_ITEMSHOP_INSERT_RESPY);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_ITEMSHOP_INSERT_RESPY);
			}
		}

		/// <summary>
		/// ��ѯ���еļ��ܵ���
		/// </summary>
		/// <returns></returns>
		public Message Soccer_Item_Skill_Query()
		{
			System.Data.DataSet ds = null;
			int item_type = 1;
			int sex_type = 7;
			string body_part = "hair";
			try
			{
				SqlHelper.log.WriteLog("��������+>" + "��ѯ���е���!");
				Console.WriteLine(DateTime.Now + " - ��������+>��������ַ" + "��ѯ���е���!");
				TLV_Structure strut = new TLV_Structure(TagName.Soccer_item_type,4,msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_item_type).m_bValueBuffer);
				sex_type = (int)strut.toInteger();
				body_part = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_body_part).m_bValueBuffer);
				ds = SOCCERItemShopInfo.Soccer_Item_Skill_Query(item_type,sex_type,body_part);
				if (ds !=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for (int i = 0;i < ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut1 = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
						strut1.AddTagKey(TagName.Soccer_idx, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[1]));
						strut1.AddTagKey(TagName.Soccer_i_name, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);						
						structList[i] = strut1;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_ITEM_SKILL_QUERY_RESP, 2);
				}
				else
				{
					return Message.COMMON_MES_RESP("û�е����б�!", Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_ITEM_SKILL_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP("û�е����б�!", Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_ITEM_SKILL_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}


		/// <summary>
		/// ��ѯ���еļ��ܵ���
		/// </summary>
		/// <returns></returns>
		public Message Soccer_Item_Skill_Blur_Query()
		{
			System.Data.DataSet ds = null;
			int item_type = 1;
			string Content = null;
			try
			{
				SqlHelper.log.WriteLog("��������+>" + "��ѯ���е���!");
				Console.WriteLine(DateTime.Now + " - ��������+>��������ַ" + "��ѯ���е���!");
				Content = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Soccer_content).m_bValueBuffer);
				ds = SOCCERItemShopInfo.Soccer_Item_Skill_Blur_Query(item_type,Content);
				if (ds !=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for (int i = 0;i < ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut1 = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
						strut1.AddTagKey(TagName.Soccer_idx, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[1]));
						strut1.AddTagKey(TagName.Soccer_i_name, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[2]));
						strut1.AddTagKey(TagName.Soccer_body_part, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]));
						strut1.AddTagKey(TagName.Soccer_item_type, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						structList[i] = strut1;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_ITEM_SKILL_BLUR_QUERY_RESP, 4);
				}
				else
				{
					return Message.COMMON_MES_RESP("û�е����б�!", Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_ITEM_SKILL_BLUR_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP("û�е����б�!", Msg_Category.SOCCER_ADMIN, ServiceKey.SOCCER_ITEM_SKILL_BLUR_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}

	}
}
