using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
namespace BAF.O2JAM2DataInfo
{
	/// <summary>
	/// CharacterInfo 的摘要说明。
	/// </summary>
	public class CharacterInfo
	{
		public CharacterInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
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
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@O2JAM2_NickName",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = account;
				paramCode[2].Value = userNick;
				result = SqlHelper.ExecSPDataSet("O2JAM2_characterinfo_query",paramCode);
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
		public static int characterInfo_Update(int userByID,string serverIP,string account,int level,int experience,int battle,int win,int draw,int lose,int MCash,int GCash)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[12]{
													new SqlParameter("@Gm_UserID",SqlDbType.Int),
													new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
													new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20),
													new SqlParameter("@O2JAM2_Level",SqlDbType.Int),
													new SqlParameter("@O2JAM2_Experience",SqlDbType.Int),
													new SqlParameter("@O2JAM2_Total",SqlDbType.Int),
													new SqlParameter("@O2JAM2_Win",SqlDbType.Int),
													new SqlParameter("@O2JAM2_Draw",SqlDbType.Int),
													new SqlParameter("@O2JAM2_Lose",SqlDbType.Int),
													new SqlParameter("@O2JAM2_MCash",SqlDbType.Int),
													new SqlParameter("@O2JAM2_GCash",SqlDbType.Int),
													new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=account;
				paramCode[3].Value=level;
				paramCode[4].Value=experience;
				paramCode[5].Value=battle;
				paramCode[6].Value=win;
				paramCode[7].Value=draw;
				paramCode[8].Value=lose;
				paramCode[9].Value=MCash;
				paramCode[10].Value=GCash;
				paramCode[11].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("O2JAM2_characterInfo_Update",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;

		}
		#endregion
		#region 查看等级和经验的列表
		/// <summary>
		/// 查看等级和经验的列表
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns></returns>
		public static DataSet LevelInfo_Query()
		{
			DataSet result = null;
			try
			{
				result = SqlHelper.ExecSPDataSet("Baf_LevelInfo_Query");
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
