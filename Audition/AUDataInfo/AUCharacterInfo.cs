using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using Common.Logic;
using Common.DataInfo;

namespace Audition.AUDataInfo
{
	/// <summary>
	/// CharacterInfo ��ժҪ˵����
	/// </summary>
	public class AUCharacterInfo
	{
		public AUCharacterInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
        #region �鿴�ȼ��;�����б�
        /// <summary>
        /// �鿴�ȼ��;�����б�
        /// </summary>
        /// <param name="serverIP">������IP</param>
        /// <returns></returns>
        public static DataSet LevelInfo_Query(string serverIP)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[1]{
												   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30)};
                paramCode[0].Value = serverIP;
                result = SqlHelper.ExecSPDataSet("Audition_LevelInfo_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion
		#region �鿴�������
		/// <summary>
		/// �鿴�������
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet characterInfo_Query(string serverIP,string account,string userNick)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
                                                   new SqlParameter("@AU_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@AU_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@AU_NickName",SqlDbType.VarChar,20)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = account;
                paramCode[2].Value = userNick;
                result = SqlHelper.ExecSPDataSet("Audition_CharacterInfo_Query", paramCode);
			}
			catch(SqlException ex)
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
        public static int characterInfo_Update(int userByID, string serverIP,int userSN,string account, int level, int experience, int MCash)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[8]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30),
                                                   new SqlParameter("@AU_UserSN",SqlDbType.Int),
												   new SqlParameter("@AU_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@AU_Level",SqlDbType.Int),
												   new SqlParameter("@AU_Exp",SqlDbType.Int),
                                                   new SqlParameter("@AU_Money",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
                paramCode[2].Value = userSN;
				paramCode[3].Value=account;
				paramCode[4].Value=level;
				paramCode[5].Value=experience;
                paramCode[6].Value = MCash;
				paramCode[7].Direction = ParameterDirection.ReturnValue;
                result = SqlHelper.ExecSPCommand("Audition_CharacterInfo_Update", paramCode);
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
