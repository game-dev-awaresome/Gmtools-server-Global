/*
 * Add by KeHuaQing 
 * 2006-09-14
 */
using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;

namespace Soccer.SOCCERDataInfo
{
	/// <summary>
	/// SOCCERCharacterInfo 的摘要说明。
	/// </summary>
	class SOCCERCharacterInfo
	{
		#region 查询用户信息
		/// <summary>
		/// 查询用户信息
		/// </summary>
		/// <param name="serverip">游戏服务器ip</param>
		/// <param name="type">查询类型：cID，cName,cIdx,全选</param>
		/// <param name="content">具体内容</param>
		/// <returns>返回的结果集</returns>
		public static DataSet Soccer_Characterinfo_Query (string serverip,string type,string content)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@Soccer_serverIP",SqlDbType.VarChar,30),
												   new SqlParameter("@srh_type",SqlDbType.VarChar,10),
												   new SqlParameter("@srh_string",SqlDbType.VarChar,80)};
				paramCode[0].Value = serverip;
				paramCode[1].Value = type;
				paramCode[2].Value = content;
				result = SqlHelper.ExecSPDataSet("SOCCER_CHARACTERINFO_QUERY", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion

		#region 删除用户状态查询
		/// <summary>
		/// 查询用户信息
		/// </summary>
		/// <param name="serverip">游戏服务器ip</param>
		/// <param name="type">查询类型：cID，cName,cIdx,全选</param>
		/// <param name="content">具体内容</param>
		/// <returns>返回的结果集</returns>
		public static DataSet Soccer_DeletedCharacterinfo_Query (string serverip,string type,string content)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@Soccer_serverIP",SqlDbType.VarChar,30),
												   new SqlParameter("@srh_type",SqlDbType.VarChar,10),
												   new SqlParameter("@srh_string",SqlDbType.VarChar,80)};
				paramCode[0].Value = serverip;
				paramCode[1].Value = type;
				paramCode[2].Value = content;
				result = SqlHelper.ExecSPDataSet("SOCCER_DELETEDCHARACTERINFO_QUERY", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion

		#region 修改用户G币
		/// <summary>
		/// 修改用户G币
		/// </summary>
		/// <param name="userByID">GMTOOLS操作员id</param>
		/// <param name="serverIP">游戏服务器ip</param>
		/// <param name="char_idx">角色编码</param>
		/// <param name="point">G币</param>
		/// <param name="admind">预留字段</param>
		/// <returns>返回处理结果，1失败，0成功</returns>
		public static int Gamepoint_Modify(int userByID, string serverIP, int char_idx,int point,string admind)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@Soccer_Serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@char_idx",SqlDbType.BigInt),
												   new SqlParameter("@point",SqlDbType.BigInt),
												   new SqlParameter("@admid",SqlDbType.VarChar,20),
											       new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = char_idx;
				paramCode[3].Value = point;
				paramCode[4].Value = admind;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SOCCER_CHARPOINT", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion

		#region 查询停封玩家信息
		/// <summary>
		/// 查询停封玩家信息
		/// </summary>
		/// <param name="serverip">游戏服务器ip</param>
		/// <param name="type">查询类型：1 未停封，0 已停封</param>
		/// <param name="content">loginId：为空 全部查询，不为空 具体某个玩家</param>
		/// <returns>返回的结果集</returns>
		public static DataSet Soccer_AccountState_Query (string serverip,string type,string content)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@Soccer_serverIP",SqlDbType.VarChar,30),
												   new SqlParameter("@srh_type",SqlDbType.VarChar,10),
												   new SqlParameter("@srh_string",SqlDbType.VarChar,80)};
				paramCode[0].Value = serverip;
				paramCode[1].Value = type;
				paramCode[2].Value = content;
				result = SqlHelper.ExecSPDataSet("SOCCER_ACCOUNTSTATE_QUERY", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion

		#region 查询停封角色信息
		/// <summary>
		/// 查询停封角色信息
		/// </summary>
		/// <param name="serverip">游戏服务器ip</param>
		/// <param name="ddtate">日期标识：为空 未停封，不为空 停封</param>
		/// <param name="type">查询类型：cID，cName,cIdx,全选</param>
		/// <param name="content">具体内容</param>
		/// <returns>返回的结果集</returns>
		public static DataSet Soccer_CharacterState_Query (string serverip,string ddtate,string type,string content)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Soccer_serverIP",SqlDbType.VarChar,30),
												   new SqlParameter("@str_ddate",SqlDbType.VarChar,10),
												   new SqlParameter("@srh_type",SqlDbType.VarChar,10),
												   new SqlParameter("@srh_string",SqlDbType.VarChar,80)};
				paramCode[0].Value = serverip;
				paramCode[1].Value = ddtate;
				paramCode[2].Value = type;
				paramCode[3].Value = content;
				result = SqlHelper.ExecSPDataSet("SOCCER_CHARACTERSTATE_QUERY", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion

		#region 修改玩家停封信息
		/// <summary>
		/// 修改玩家停封信息
		/// </summary>
		/// <param name="userByID">GMTOOLS操作员id</param>
		/// <param name="serverIP">游戏服务器ip</param>
		/// <param name="loginId">玩家登入名称</param>
		/// <param name="m_id">玩家编号</param>
		/// <param name="m_auth">玩家账号是否停封：0 停，1 起</param>
		/// <returns>返回处理结果，1失败，0成功</returns>
		public static int AccountState_Modify(int userByID, string serverIP, string loginId,int m_id,int m_auth)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@Soccer_Serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@loginId",SqlDbType.VarChar,10),
												   new SqlParameter("@m_id",SqlDbType.Int),
												   new SqlParameter("@m_auth",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = loginId;
				paramCode[3].Value = m_id;
				paramCode[4].Value = m_auth;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SOCCER_ACCOUNTSTATE", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion

		#region 修改角色停封信息
		/// <summary>
		/// 修改角色停封信息
		/// </summary>
		/// <param name="userByID">GMTOOLS操作员id</param>
		/// <param name="serverIP">游戏服务器ip</param>
		/// <param name="char_idx">角色编号</param>
		/// <param name="delete_date">玩家角色是否停封：为空 起，不为空 停</param>
		/// <returns>返回处理结果，1失败，0成功</returns>
		public static int CharacterState_Modify(int userByID, string serverIP, int char_idx,string delete_date)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@Soccer_Serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@char_idx",SqlDbType.BigInt),
												   new SqlParameter("@str_ddate",SqlDbType.VarChar,10),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = char_idx;
				paramCode[3].Value = delete_date;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SOCCER_CHARACTERSTATE", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		
		#region 恢复角色信息
		/// <summary>
		/// 恢复角色信息
		/// </summary>
		/// <param name="userByID">GMTOOLS操作员id</param>
		/// <param name="serverIP">游戏服务器ip</param>
		/// <param name="char_idx">角色编码</param>
		/// <returns></returns>
		public static int Soccer_CharItems_Recovery(int userByID, string serverIP, int char_idx)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@Soccer_Serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@char_idx",SqlDbType.BigInt),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = char_idx;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SOCCER_CHARITEMS_RECOVERY", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion

		#region 检查角色信息
		/// <summary>
		/// 检查角色信息
		/// </summary>
		/// <param name="serverip">游戏服务器ip</param>
		/// <param name="char_idx">角色编码</param>
		/// <param name="kind">检查方式:socket,name</param>
		/// <returns>返回的结果集</returns>
		public static DataSet Soccer_CharCheck(string serverip,int char_idx,string kind)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@Soccer_serverIP",SqlDbType.VarChar,30),
												   new SqlParameter("@char_idx",SqlDbType.BigInt),
												   new SqlParameter("@kind",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverip;
				paramCode[1].Value = char_idx;
				paramCode[2].Value = kind;
				result = SqlHelper.ExecSPDataSet("SOCCER_CHARCHECK", paramCode);
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
