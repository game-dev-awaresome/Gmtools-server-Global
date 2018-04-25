using System;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;

namespace CR.CRDataInfo
{
    class CRCharacterInfo
    {
        #region ��ʱ�鿴����ҽ�ɫ��Ϣ
        /// <summary>
        /// ��ʱ�鿴����ҽ�ɫ��Ϣ
        /// </summary>
        /// <returns></returns>
        public static DataSet CR_CharacterInfo_Query(string serverIP, string account,string nickName,int action)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[4]{
												   new SqlParameter("@CR_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@CR_UserID",SqlDbType.VarChar,20),
                                                   new SqlParameter("@CR_NickName",SqlDbType.VarChar,20),
                                                   new SqlParameter("@CR_ActionType",SqlDbType.Int)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = account;
                paramCode[2].Value = nickName;
                paramCode[3].Value = action;
                result = SqlHelper.ExecSPDataSet("CR_CharacterInfo_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion
        #region �޸������������
        /// <summary>
        /// �޸��������
        /// </summary>
        /// <param name="userByID">����ԱID</param>
        /// <param name="serverIP">������IP</param>
        /// <param name="account">�ʺ�</param>
        /// <param name="level">�ȼ�</param>
        /// <param name="experience">����ֵ</param>
        /// <param name="battle">�ܾ���</param>
        /// <param name="win">ʤ��</param>
        /// <param name="draw">ƽ��</param>
        /// <param name="lose">����</param>
        /// <param name="MCash">M��</param>
        /// <param name="GCash">G��</param>
        /// <returns></returns>
        public static int characterInfo_Update(int userByID, string serverIP, int pstID,int experience, int license, int money,int rmb)
        {
            int result = -1;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[8]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@CR_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@CR_PstID",SqlDbType.VarChar,20),
												   new SqlParameter("@CR_Experience",SqlDbType.Int),
												   new SqlParameter("@CR_License",SqlDbType.Int),
												   new SqlParameter("@CR_Money",SqlDbType.Int),
												   new SqlParameter("@CR_RMB",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
                paramCode[0].Value = userByID;
                paramCode[1].Value = serverIP;
                paramCode[2].Value = pstID;
                paramCode[3].Value = experience;
                paramCode[4].Value = license;
                paramCode[5].Value = money;
				paramCode[6].Value = rmb;
                paramCode[7].Direction = ParameterDirection.ReturnValue;
                result = SqlHelper.ExecSPCommand("CR_CharacterInfo_Update", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;

        }
        #endregion
		#region �޸�����ǳ�
		/// <summary>
		/// �޸�����ǳ�
		/// </summary>
		/// <param name="userByID">����ԱID</param>
		/// <param name="serverIP">������IP</param>
		/// <param name="pstID">�ʺ�</param>
		/// <param name="nickname">�ǳ�</param>
		/// <returns></returns>
		public static int nickName_Update(int userByID, string serverIP, int pstID,string usernick)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@CR_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@CR_PstID",SqlDbType.VarChar,20),
												   new SqlParameter("@CR_NickName",SqlDbType.VarChar,40),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = pstID;
				paramCode[3].Value = usernick;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("CR_NickName_Update", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;

		}
		#endregion
		#region �鿴��������ߡ�������Ϣ
		/// <summary>
		/// �鿴��������ߡ�������Ϣ
		/// </summary>
		/// <returns></returns>
		public static DataSet CR_Login_Logout_Query(string serverIP, string userID,string nickName,int action)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@CR_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@CR_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@CR_NickName",SqlDbType.VarChar,20),
												   new SqlParameter("@CR_ActionType",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = userID;
				paramCode[2].Value = nickName;
				paramCode[3].Value = action;
				result = SqlHelper.ExecSPDataSet("CR_Login_Logout_Query", paramCode);
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
