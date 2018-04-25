using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
namespace BAF.O2JAM2DataInfo
{
    class AccountInfo
    {
        #region ��ʱ�鿴����Ҽ���״̬
        /// <summary>
        /// ��ʱ�鿴����Ҽ���״̬
        /// </summary>
        /// <returns></returns>
        public static DataSet O2JAM2_Account_Query(string serverIP, string account,string userNick,int action)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                         paramCode = new SqlParameter[4]{
												   new SqlParameter("@O2JAM2_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_UserName",SqlDbType.VarChar,20),
                                                   new SqlParameter("@O2JAM2_UserNick",SqlDbType.VarChar,50),
                                                   new SqlParameter("@O2JAM2_ActionType",SqlDbType.Int)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = account;
                paramCode[2].Value = userNick;
                paramCode[3].Value = action;
                result = SqlHelper.ExecSPDataSet("O2JAM2_Account_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion
        #region ��ʱ�鿴�������Ƿ�ʹ�ù�
      /// <summary>
        /// ��ʱ�鿴�������Ƿ�ʹ�ù�
      /// </summary>
      /// <param name="serverIP"></param>
      /// <param name="password"></param>
      /// <param name="activeNum"></param>
      /// <returns></returns>
 
        public static DataSet O2JAM2_AccountActive_Query(string username,string password,string activeNum)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[3]{
												   new SqlParameter("@O2JAM2_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_PASS",SqlDbType.VarChar,10),
                                                   new SqlParameter("@O2JAM2_NUM",SqlDbType.VarChar,10)};
                paramCode[0].Value = "61.129.90.151";
                paramCode[1].Value = password;
                paramCode[2].Value = activeNum;
                result = SqlHelper.ExecSPDataSet("O2JAM2_Account_ActiveNUM_Query", paramCode);

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion
		#region ��ʱ�鿴����������������ʺŷ�ͣ״̬
		/// <summary>
		/// ��ʱ�鿴����������������ʺŷ�ͣ״̬
		/// </summary>
		/// <returns></returns>
		public static DataSet O2JAM2_Banishment_QueryAll(string serverIP)
		{
			DataSet ds = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Direction = ParameterDirection.ReturnValue;
				ds = SqlHelper.ExecSPDataSet("o2jam2_banishment_Query", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return ds;
		}
		#endregion
		#region �鿴��������ʺŷ�ͣ״̬
		/// <summary>
		/// ��ʱ�鿴��������ʺŷ�ͣ״̬
		/// </summary>
		/// <returns></returns>
		public static DataSet O2JAM2_BanishmentLocal_Query(string serverIP,string account)
		{
			DataSet ds = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = account;
				paramCode[2].Direction = ParameterDirection.ReturnValue;
				ds = SqlHelper.ExecSPDataSet("O2JAM2_BanishmentLocal_Query", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return ds;
		}
		#endregion
		#region ��ʱ�鿴������ʺŷ�ͣ״̬
		/// <summary>
		/// ��ʱ�鿴������ʺŷ�ͣ״̬
		/// </summary>
		/// <returns></returns>
		public static int O2JAM2_Banishment_Query(string serverIP,string account)
		{
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=account;
				paramCode[2].Direction = ParameterDirection.ReturnValue;
				DataSet ds = SqlHelper.ExecSPDataSet("O2JAM2_Banishment_Query",paramCode);
				if(ds.Tables[0].Rows.Count>0)
				{
					return 1;
				}
				else
					return 0;
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
				return -1;
			}
		}
		#endregion
		#region ��ͣ�������ߵ��ʺ�
		/// <summary>
		/// ��ͣ�������ߵ��ʺ�
		/// </summary>
		/// <param name="userByID">����ԱID</param>
		/// <param name="serverIP">������IP</param>
		/// <param name="account">����ʺ�</param>
		/// <returns></returns>
		public static int O2JAM2_Banishment_Close(int userByID,string serverIP,string account,string reason)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@O2JAM2_Reason",SqlDbType.VarChar,500),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=account;
				paramCode[3].Value = reason;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("O2JAM2_Banishment_Close",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;

		}
		#endregion
		#region ��ⳬ�����ߵ��ʺ�
		/// <summary>
		/// ��ⳬ�����ߵ��ʺ�
		/// </summary>
		/// <param name="userByID">����ԱID</param>
		/// <param name="serverIP">������IP</param>
		/// <param name="account">����ʺ�</param>
		/// <returns></returns>
		public static int O2JAM2_Banishment_Open(int userByID,string serverIP,string account)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=account;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("O2JAM2_Banishment_Open",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;

		}
		#endregion
		#region �޳���ҵ�¼�ʺ�
		/// <summary>
		/// �޳���ҵ�¼�ʺ�
		/// </summary>
		/// <param name="userByID">����ԱID</param>
		/// <param name="serverIP">������IP</param>
		/// <param name="account">����ʺ�</param>
		/// <returns></returns>
		public static int O2JAM2_UserLogin_Delete(int userByID,string serverIP,string account)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=account;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("o2jam2_Login_delete",paramCode);
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
