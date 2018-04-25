/*
 * Add by KeHuaQing 
 * 2006-11-21
 */
using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;

namespace Soccer.SOCCERDataInfo
{
	/// <summary>
	/// SOCCERItemShopInfo ��ժҪ˵����
	/// </summary>
	public class SOCCERItemShopInfo
	{
		#region ��ѯ��ҹ����¼�����ͼ�¼
		/// <summary>
		/// ��ѯ��ҹ����¼�����ͼ�¼
		/// </summary>
		/// <param name="serverip">��Ϸ������ip</param>
		/// <param name="SenderUserName">�������ǳ�</param>
		/// <param name="ReceiveUserName">�������ǳ�</param>
		/// <returns>���صĽ����</returns>
		public static DataSet Soccer_UserTrade_Query (string serverip,string SenderUserName,string ReceiveUserName)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@Soccer_serverIP",SqlDbType.VarChar,30),
												   new SqlParameter("@SOCCER_SenderUserName",SqlDbType.VarChar,20),
												   new SqlParameter("@SOCCER_ReceiveUserName",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverip;
				paramCode[1].Value = SenderUserName;
				paramCode[2].Value = ReceiveUserName;
				result = SqlHelper.ExecSPDataSet("SOCCER_USERTRADE_QUERY", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion

		#region ɾ����ָ�������ϵ��ߡ�����
		/// <summary>
		/// ɾ����ָ�������ϵ��ߡ�����
		/// </summary>
		/// <param name="userByID">����Աid</param>
		/// <param name="serverIP">��Ϸ������ip</param>
		/// <param name="char_idx">��ɫidx</param>
		/// <param name="item_type">��������</param>
		/// <param name="item_idx">����idx</param>
		/// <param name="item_equip">�Ƿ�װ��</param>
		/// <param name="delete_date">ɾ������</param>
		/// <returns></returns>
		public static int Soccer_Item_Skill_Delete(int userByID, string serverIP, int char_idx,int item_type,int item_idx,int item_equip,string delete_date)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[8]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@Soccer_Serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@char_idx",SqlDbType.BigInt),
												   new SqlParameter("@item_type",SqlDbType.TinyInt),
												   new SqlParameter("@item_idx",SqlDbType.Int),
												   new SqlParameter("@item_equip",SqlDbType.TinyInt),
												   new SqlParameter("@str_ddate",SqlDbType.VarChar,10),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = char_idx;
				paramCode[3].Value = item_type;
				paramCode[4].Value = item_idx;
				paramCode[5].Value = item_equip;
				paramCode[6].Value = delete_date;
				paramCode[7].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SOCCER_ITEM_SKILL_MODIFY", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion

		#region ��ѯ������ϵ��߼���
		/// <summary>
		/// ��ѯ������ϵ��߼���
		/// </summary>
		/// <param name="serverip">��Ϸ������ip</param>
		/// <param name="SenderUserName">����˺�</param>
		/// <param name="item_type">���ͣ�1 ���� 0 ����</param>
		/// <returns></returns>
		public static DataSet Soccer_Account_Itemskill_Query (string serverip,string SenderUserName,int item_type)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@Soccer_serverIP",SqlDbType.VarChar,30),
												   new SqlParameter("@SOCCER_SenderUserName",SqlDbType.VarChar,20),
												   new SqlParameter("@Item_Type",SqlDbType.TinyInt)};
				paramCode[0].Value = serverip;
				paramCode[1].Value = SenderUserName;
				paramCode[2].Value = item_type;
				result = SqlHelper.ExecSPDataSet("SOCCER_ACCOUNT_ITEMSKILL_QUERY", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion

		#region ��������͵���
        /// <summary>
        /// ��������͵���
        /// </summary>
        /// <param name="userByID">����Աid</param>
        /// <param name="serverIP">��Ϸ������ip</param>
        /// <param name="Account_Name">����ǳ�</param>
        /// <param name="Char_idx">��ɫidx</param>
        /// <param name="Char_Name">��ɫ��</param>
        /// <param name="Title">����</param>
        /// <param name="Content">����</param>
        /// <param name="Item_idx">����idx</param>
        /// <param name="Item_type">��������</param>
        /// <param name="Item_equip">�Ƿ�װ��</param>
        /// <returns></returns>
		public static int Soccer_Itemshop_Insert(int userByID,string serverIP,string Account_Name,int Char_idx,string Char_Name,string Title,string Content,int Item_idx,int Item_type,int Item_equip)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[11]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@Account_name",SqlDbType.VarChar,20),
												   new SqlParameter("@char_idx",SqlDbType.BigInt),
												   new SqlParameter("@Char_name",SqlDbType.VarChar,20),
												   new SqlParameter("@Title",SqlDbType.VarChar,40),
												   new SqlParameter("@Content",SqlDbType.VarChar,400),												   
												   new SqlParameter("@item_idx",SqlDbType.Int),
												   new SqlParameter("@item_type",SqlDbType.TinyInt),
												   new SqlParameter("@item_equip",SqlDbType.TinyInt),												  
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = Account_Name;
				paramCode[3].Value = Char_idx;
				paramCode[4].Value = Char_Name;
				paramCode[5].Value = Title;
				paramCode[6].Value = Content;
				paramCode[7].Value = Item_idx;
				paramCode[8].Value = Item_type;
				paramCode[9].Value = Item_equip;
				paramCode[10].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SOCCER_ITEMSHOP_INSERT", paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
        
		#region ��ѯ���еļ��ܵ���
		/// <summary>
		/// ��ѯ���еļ��ܵ���
		/// </summary>
		/// <param name="item_type">����</param>
		/// <returns></returns>
		public static DataSet Soccer_Item_Skill_Query(int item_type,int Sex_type,string Body_Part)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@Item_Type",SqlDbType.TinyInt),
												   new SqlParameter("@Sex_Type",SqlDbType.Int),
												   new SqlParameter("@Body_Part",SqlDbType.VarChar,30)};
				paramCode[0].Value = item_type;
				paramCode[1].Value = Sex_type;
				paramCode[2].Value = Body_Part;
				result = SqlHelper.ExecSPDataSet("SOCCER_ITEM_SKILL_QUERY", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion

		#region ģ����ѯ���еļ��ܵ���
		/// <summary>
		/// ��ѯ���еļ��ܵ���
		/// </summary>
		/// <param name="item_type">����</param>
		/// <returns></returns>
		public static DataSet Soccer_Item_Skill_Blur_Query(int item_type,string Content)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@Item_Type",SqlDbType.TinyInt),
												   new SqlParameter("@Content",SqlDbType.VarChar,50)};
				paramCode[0].Value = item_type;
				paramCode[1].Value = Content;
				result = SqlHelper.ExecSPDataSet("SOCCER_ITEM_SKILL_BLUR_QUERY", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion

	}
}
