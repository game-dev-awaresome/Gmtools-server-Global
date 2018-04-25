using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
using Common.DataInfo;

namespace SDO.SDODataInfo
{
	/// <summary>
	/// MemberInfo ��ժҪ˵����
	/// </summary>
	public class MemberInfo
	{
		public MemberInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region �鿴��ҵ��ʺ���Ϣ���Լ��ʺŵĴ�������״̬
		/// <summary>
		/// �鿴��ҵ��ʺ���Ϣ���Լ��ʺŵĴ�������״̬
		/// </summary>
		/// <returns></returns>
		public static DataSet memberInfo_Query(string serverIP,string account)
		{
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
				DataSet ds = SqlHelper.ExecSPDataSet("SDO_Member_Query",paramCode);
				if(ds.Tables[0].Rows.Count>0)
				{
					return ds;
				}
				else
					return null;

			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
            
		}
		#endregion
		#region ��ʱ�鿴��ҵ�ǰ״̬(������/����/����״̬)
		/// <summary>
		/// ��ʱ�鿴��ҵ�ǰ״̬(������/����/����״̬)
		/// </summary>
		/// <returns></returns>
		public static DataSet T_o2jam_login_Query(string serverIP,string account)
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
				result = SqlHelper.ExecSPDataSet("SDO_Login_Status",paramCode);
			}
			catch(SqlException ex)
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
        public static DataSet SDO_Banishment_QueryAll(string serverIP)
        {
            DataSet ds = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[2]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@result",SqlDbType.Int)};
                paramCode[0].Value = serverIP;
                paramCode[1].Direction = ParameterDirection.ReturnValue;
                ds = SqlHelper.ExecSPDataSet("SDO_Banishment_QueryAll", paramCode);
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
        public static DataSet SDO_BanishmentLocal_Query(string serverIP,string account)
        {
            DataSet ds = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[3]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
                                                   new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@result",SqlDbType.Int)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = account;
                paramCode[2].Direction = ParameterDirection.ReturnValue;
                ds = SqlHelper.ExecSPDataSet("SDO_AccountTemp_Query", paramCode);
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
		public static int SDO_Banishment_Query(string serverIP,string account)
		{
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=account;
				paramCode[2].Direction = ParameterDirection.ReturnValue;
				DataSet ds = SqlHelper.ExecSPDataSet("SDO_Banishment_Query",paramCode);
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
		public static int SDO_Banishment_Close(int userByID,string serverIP,string account,string reason)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20),
                                                   new SqlParameter("@SDO_Reason",SqlDbType.VarChar,500),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=account;
                paramCode[3].Value = reason;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_Banishment_Close",paramCode);
				if(userByID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(userByID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;

		}
		#endregion
		#region �������������̺״̬
		/// <summary>
		/// �������������̺״̬
		/// </summary>
		/// <returns></returns>
		public static int memberDanceInfo_Close(int userByID,string serverIP,string account)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=account;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_MemberDance_Close",paramCode);

			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region �����������̺״̬
		/// <summary>
		/// �����������̺״̬
		/// </summary>
		/// <returns></returns>
		public static int memberDanceInfo_Active(int userByID,string serverIP,string account)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=account;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_MemberDance_Open",paramCode);

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
		public static int SDO_Banishment_Open(int userByID,string serverIP,string account)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=account;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_Banishment_Open",paramCode);
				if(userByID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(userByID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;

		}
		#endregion
		#region ������ҵ��س�
		/// <summary>
		/// ������ҵ��س�
		/// </summary>
		/// <returns></returns>
		public static int UserNick_Update(int userByID,string serverIP,string account,string nickName)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_UserNick",SqlDbType.VarChar,20),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=account;
				paramCode[3].Value =nickName; 
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_UserNick_Update",paramCode);

			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion  
		#region ��ʱ�鿴����̺
		/// <summary>
		/// ��ʱ�鿴����̺
		/// </summary>
		/// <returns></returns>
		public static DataSet SDO_PADKeyWord_Query(string keyID,string keyWord)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@Card_ID",SqlDbType.VarChar,50),
												   new SqlParameter("@Card_Keyword",SqlDbType.VarChar,100)
											   };
				paramCode[0].Value=keyID.Trim();
				paramCode[1].Value=keyWord.Trim();
				result = SqlHelper.ExecSPDataSet("Card_PadKeyInfo_Query",paramCode);
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
