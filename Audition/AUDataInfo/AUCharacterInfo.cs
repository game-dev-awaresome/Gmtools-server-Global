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
	/// CharacterInfo 的摘要说明。
	/// </summary>
	public class AUCharacterInfo
	{
		public AUCharacterInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
        #region 查看等级和经验的列表
        /// <summary>
        /// 查看等级和经验的列表
        /// </summary>
        /// <param name="serverIP">服务器IP</param>
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
		#region 查看玩家资料
		/// <summary>
		/// 查看玩家资料
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
		#region 修改玩家人物资料
		/// <summary>
		/// 修改玩家资料
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <param name="level">等级</param>
		/// <param name="experience">经验值</param>
		/// <param name="battle">总局数</param>
		/// <param name="win">胜局</param>
		/// <param name="draw">平局</param>
		/// <param name="lose">负局</param>
		/// <param name="MCash">M币</param>
		/// <param name="GCash">G币</param>
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
