using System;

using System.Text;
using System.Data.SqlClient;
using System.Data;
using Common.Logic;

namespace Audition.AUDataInfo
{
    class AUMemberInfo
    {
        #region ��ʱ�鿴����������������ʺŷ�ͣ״̬
        /// <summary>
        /// ��ʱ�鿴����������������ʺŷ�ͣ״̬
        /// </summary>
        /// <returns></returns>
        public static DataSet Audition_Banishment_QueryAll(string serverIP)
        {
            DataSet ds = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[1]{
												   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30)
                };
                paramCode[0].Value = serverIP;
                ds = SqlHelper.ExecSPDataSet("Audition_AccountEnvelop_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }
        #endregion
        #region �鿴�����ű�������ʺŷ�ͣ״̬
        /// <summary>
        /// ��ʱ�鿴�����ű�������ʺŷ�ͣ״̬
        /// </summary>
        /// <returns></returns>
        public static DataSet Audition_BanishmentLocal_Query(string serverIP, string account)
        {
            DataSet ds = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[2]{
												   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30),
                                                   new SqlParameter("@AU_UserID",SqlDbType.VarChar,20)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = account;
                ds = SqlHelper.ExecSPDataSet("Audition_AccountLocal_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }
        #endregion
        #region ��֤9you�ľ���������ʺ�
        /// <summary>
        /// ��֤9you������ʺ�
        /// </summary>
        /// <returns></returns>
        public static string Audition_Identity9you_Query(string serverIP, string account)
        {
            string userID = null;
            int nineyouID = 0;
            DataSet ds = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[1]{
												   new SqlParameter("@AU_UserID",SqlDbType.VarChar,30)};
                paramCode[0].Value = account;
                ds = SqlHelper.ExecSPDataSet("Audition_9youAccount_Query", paramCode);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    nineyouID = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0].ToString());

                    paramCode = new SqlParameter[2]{
												   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_9YouID",SqlDbType.Int)};
                    paramCode[0].Value = serverIP;
                    paramCode[1].Value = nineyouID;
                    ds = SqlHelper.ExecSPDataSet("Audition_UserID_Query", paramCode);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        userID = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                    }
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return userID;
        }
        #endregion
        #region ��ʱ�鿴������ʺ���Ϣ
        /// <summary>
        /// ��ʱ�鿴������ʺ���Ϣ
        /// </summary>
        /// <returns></returns>
        public static DataSet Audition_Account_Query(string serverIP, string account)
        {
            DataSet ds = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[2]{
												   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_UserID",SqlDbType.VarChar,30)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = account;
                ds = SqlHelper.ExecSPDataSet("Audition_Account_Query", paramCode);
                
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
        public static int Audition_BanishmentAccount_Query(string serverIP, string userNick)
        {
            int result = -1;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[3]{
												   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_UserNick",SqlDbType.VarChar,20),
                                                   new SqlParameter("@result",SqlDbType.Int)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = userNick;
                paramCode[2].Direction = ParameterDirection.ReturnValue;
                result = SqlHelper.ExecSPCommand("Audition_AccountClose_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            return result;
        }
         #endregion
        #region ��ͣ�����ŵ��ʺ�
        /// <summary>
        /// ��ͣ�����ŵ��ʺ�
        /// </summary>
        /// <param name="userByID">����ԱID</param>
        /// <param name="serverIP">������IP</param>
        /// <param name="account">����ʺ�</param>
        /// <returns></returns>
        public static int Audition_Banishment_Close(int userByID, string serverIP, string account,string nickName,string reason)
        {
            int result = -1;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@AU_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@AU_UserID",SqlDbType.VarChar,20),
                                                   new SqlParameter("@AU_UserNick",SqlDbType.VarChar,50),
                                                   new SqlParameter("@AU_Reason",SqlDbType.VarChar,500),
												   new SqlParameter("@result",SqlDbType.Int)};
                paramCode[0].Value = userByID;
                paramCode[1].Value = serverIP;
                paramCode[2].Value = account;
                paramCode[3].Value = nickName;
                paramCode[4].Value = reason;
                paramCode[5].Direction = ParameterDirection.ReturnValue;
                result = SqlHelper.ExecSPCommand("Audition_Account_Close", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;

        }
        #endregion
        #region ��⾢���ŵ��ʺ�
        /// <summary>
        /// ��⾢���ŵ��ʺ�
        /// </summary>
        /// <param name="userByID">����ԱID</param>
        /// <param name="serverIP">������IP</param>
        /// <param name="account">����ʺ�</param>
        /// <returns></returns>
        public static int Audition_Banishment_Open(int userByID, string serverIP, string account,string nickName)
        {
            int result = -1;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_UserID",SqlDbType.VarChar,20),
                                                   new SqlParameter("@AU_UserNick",SqlDbType.VarChar,50),
												   new SqlParameter("@result",SqlDbType.Int)};
                paramCode[0].Value = userByID;
                paramCode[1].Value = serverIP;
                paramCode[2].Value = account;
                paramCode[3].Value = nickName;
                paramCode[4].Direction = ParameterDirection.ReturnValue;
                result = SqlHelper.ExecSPCommand("Audition_Account_Open", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;

        }
        #endregion
		#region ���¾������س�
		/// <summary>
		/// ���¾������س�
		/// </summary>
		/// <param name="userByID">����ԱID</param>
		/// <param name="serverIP">������IP</param>
		/// <param name="account">����ʺ�</param>
		/// <returns></returns>
		public static int Audition_UserNick_Update(int userByID, string serverIP, string account,string nickName)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@AU_UserNick",SqlDbType.VarChar,50),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = account;
				paramCode[3].Value = nickName;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Audition_NickName_Update", paramCode);
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
