using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
namespace BAF.O2JAM2DataInfo
{
	/// <summary>
	/// ItemShopInfo ��ժҪ˵����
	/// </summary>
	public class ItemShopInfo
	{
		public ItemShopInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region �鿴���������Ʒ����
		/// <summary>
		/// �鿴������ϵ�����Ϣ
		/// </summary>
		/// <param name="serverIP">������IP</param>
		/// <param name="account">����ʺ�</param>
		/// <returns></returns>
		public static DataSet AvatarItemList_Query(string serverIP,string userID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=userID;
				result = SqlHelper.ExecSPDataSet("o2jam2_item_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region ɾ��������ϵ���
		/// <summary>
		/// ɾ�������Ʒ��Ϣ
		/// </summary>
		/// <param name="serverIP">������IP</param>
		/// <param name="account">����ʺ�</param>
		/// <returns></returns>
		public static int itemShop_Delete(int userByID,string serverIP,string userIndexID,int itemCode)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@O2JAM2_ItemCode",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=userIndexID;
				paramCode[3].Value=itemCode;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("o2jam2_item_delete",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region �鿴�������еĵ���
		/// <summary>
		/// �鿴�������еĵ���
		/// </summary>
		/// <param name="serverIP">������IP</param>
		/// <param name="account">����ʺ�</param>
		/// <returns></returns>
		public static DataSet O2JAM2GiftBox_Query(string serverIP,string userID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=userID;
				result = SqlHelper.ExecSPDataSet("O2jam2_Message_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region �鿴���ߵ�״̬
		/// <summary>
		/// �鿴���ߵ�״̬
		/// </summary>
		/// <param name="serverIP">������IP</param>
		/// <param name="itemCode">���ߴ���</param>
		/// <returns></returns>
		public static DataSet itemShop_Status_Query(string serverIP,int itemCode)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_Itemcode",SqlDbType.Int)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=itemCode;
				result = SqlHelper.ExecSPDataSet("O2JAM2_ItemShop_Query2",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region �鿴��Ϸ��������б�
		/// <summary>
		/// �鿴��Ϸ��������б�
		/// </summary>
		/// <param name="serverIP">������IP</param>
		/// <param name="bigType">���ߴ���</param>
		/// <param name="smallType">����С��</param>
		/// <returns>�������ݼ�</returns>
		public static DataSet itemShop_QueryAll(string serverIP,string itemName,string userID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
											       new SqlParameter("@O2JAM2_ItemName",SqlDbType.VarChar,30),
											       new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=itemName;
				paramCode[2].Value=userID;
				result = SqlHelper.ExecSPDataSet("o2jam2_item_QueryAll",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region ���������������Ʒ
		/// <summary>
		/// ���������������Ʒ
		/// </summary>
		/// <param name="serverIP">������IP</param>
		/// <param name="account">����ʺ�</param>
		/// <returns></returns>
		public static int giftBox_Insert(int userByID,string serverIP,string userID,int itemCode,string title,string context,int timesLimit,int dayLimit)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[9]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@O2JAM2_Title",SqlDbType.VarChar,40),
												   new SqlParameter("@O2JAM2_Content",SqlDbType.VarChar,400),
												   new SqlParameter("@O2JAM2_ItemCode",SqlDbType.Int),
												   new SqlParameter("@O2JAM2_TimesLimit",SqlDbType.Int),
												   new SqlParameter("@O2JAM2_dayslimit",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=userID;
				paramCode[3].Value=title;
				paramCode[4].Value=context;
				paramCode[5].Value=itemCode;
				paramCode[6].Value=timesLimit;
				paramCode[7].Value=dayLimit;
				paramCode[8].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("O2jam2_Message_Insert",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region ɾ�����������ϵ���
		/// <summary>
		/// ɾ�����������ϵ���
		/// </summary>
		/// <param name="serverIP">������IP</param>
		/// <param name="account">����ʺ�</param>
		/// <returns></returns>
		public static int giftBox_Delete(int userByID,string serverIP,string userID,int itemCode)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@O2JAM2_ItemCode",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=userID;
				paramCode[3].Value=itemCode;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("O2jam2_Message_delete",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region �����������߼�¼
		/// <summary>
		/// �����������߼�¼
		/// </summary>
		/// <param name="serverIP">������IP</param>
		/// <param name="account">����ʺ�</param>
		/// <returns></returns>
		public static DataSet userOnline_Query(string serverIP,string account)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=account;
				paramCode[2].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPDataSet("SDO_UserOnline_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region ���������Ѽ�¼
		/// <summary>
		/// ���������Ѽ�¼
		/// </summary>
		/// <param name="serverIP">������IP</param>
		/// <param name="account">����ʺ�</param>
		/// <returns></returns>
		public static DataSet userConsume_Query(string serverIP,string account,int moneyType,DateTime beginDate,DateTime endDate)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@O2JAM2_ConsumeType",SqlDbType.Int),
												   new SqlParameter("@O2JAM2_BeginDate",SqlDbType.DateTime),
												   new SqlParameter("@O2JAM2_EndDate",SqlDbType.DateTime)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=account;
				paramCode[2].Value=moneyType;
				paramCode[3].Value = beginDate;
				paramCode[4].Value = endDate;
				result = SqlHelper.ExecSPDataSet("o2jam2_ConsumeLog_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
        #region ���������Ѽ�¼�ϼ�
        /// <summary>
        /// ���������Ѽ�¼�ϼ�
        /// </summary>
        /// <param name="serverIP">������IP</param>
        /// <param name="account">����ʺ�</param>
        /// <returns></returns>
        public static DataSet userConsume_QuerySum(string serverIP, string account, int moneyType, DateTime beginDate, DateTime endDate)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[5]{
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@O2JAM2_ConsumeType",SqlDbType.Int),
												   new SqlParameter("@O2JAM2_BeginDate",SqlDbType.DateTime),
												   new SqlParameter("@O2JAM2_EndDate",SqlDbType.DateTime)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = account;
                paramCode[2].Value = moneyType;
                paramCode[3].Value = beginDate;
                paramCode[4].Value = endDate;
                result = SqlHelper.ExecSPDataSet("o2jam2_ConsumeLog_Sum_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion
		#region �����ҽ��׼�¼
		/// <summary>
		/// �����ҽ��׼�¼
		/// </summary>
		/// <param name="serverIP">������IP</param>
		/// <param name="account">����ʺ�</param>
		/// <returns></returns>
		public static DataSet userTrade_Query(string serverIP,string sendUserID,string recvUserID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_SenderUserID",SqlDbType.VarChar,20),
                                                   new SqlParameter("@O2JAM2_ReceiveUserID",SqlDbType.VarChar,20)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=sendUserID;
                paramCode[2].Value = recvUserID;
				result = SqlHelper.ExecSPDataSet("o2jam2_ConsumeLog_Sum_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
	}
}
