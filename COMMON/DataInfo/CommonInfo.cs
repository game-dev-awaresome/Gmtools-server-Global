using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;

namespace Common.DataInfo
{
	/// <summary>
	/// CommonInfo 的摘要说明。
	/// </summary>
	public class CommonInfo
	{
		public CommonInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
        public static int LinkServerIP_Create(int userByID,string gameIP,string usr,string pwd,string city,int gameID,int gamedbID,int gameFlag)
        {
            int result = -1;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[9]{
                                                   new SqlParameter("@GM_UserID",SqlDbType.Int),
												   new SqlParameter("@Game_IP",SqlDbType.VarChar,30),
												   new SqlParameter("@Game_Usr",SqlDbType.VarChar,50),
												   new SqlParameter("@Game_PWD",SqlDbType.VarChar,50),
                                                   new SqlParameter("@Game_City",SqlDbType.VarChar,50),
                                                   new SqlParameter("@Game_ID",SqlDbType.Int),
                                                   new SqlParameter("@Game_DBID",SqlDbType.Int),
                                                   new SqlParameter("@Game_Flag",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)
											   };
                paramCode[0].Value = userByID;
                paramCode[1].Value = gameIP.Trim();
                paramCode[2].Value = usr.Trim();
                paramCode[3].Value = pwd.Trim();
                paramCode[4].Value = city.Trim();
                paramCode[5].Value = gameID;
                paramCode[6].Value = gamedbID;
                paramCode[7].Value = gameFlag;
                paramCode[8].Direction = ParameterDirection.ReturnValue;
                result = SqlHelper.ExecSPCommand("sp_linkGameIP", paramCode);
				if(userByID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(userByID);
				}
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);

            }
            return result;
        }
		public static int LinkServerIP_Delete(int userByID,int idx,string gameIP)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@GM_UserID",SqlDbType.Int),
												   new SqlParameter("@idx",SqlDbType.Int),
												   new SqlParameter("@ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@Result",SqlDbType.Int)
											   };
				paramCode[0].Value = userByID;
				paramCode[1].Value = idx;
				paramCode[2].Value = gameIP.Trim();
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("sp_deleteLinkDown", paramCode);
				if(userByID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(userByID);
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);

			}
			return result;
		}
        public static DataSet serverIP_QueryAll()
        {
            try
            {
                return SqlHelper.ExecuteDataset("ServerInfo_Query_All");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public static string serverIP_Query(string serverIP)
        {
            string serverName = null;
			SqlParameter[] paramCode;
            try
            {
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@ServerIP",SqlDbType.VarChar,30)};
			    paramCode[0].Value = serverIP;
                DataSet ds = SqlHelper.ExecSPDataSet("ServerName_Query",paramCode);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    serverName = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return serverName;
        }
		public static DataSet serverIP_Query(int gameID,int gameDBID)
		{
            DataSet ds = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[2]{
												   new SqlParameter("@GM_gameID",SqlDbType.Int),
												   new SqlParameter("@GM_gameDBID",SqlDbType.Int)};
                paramCode[0].Value = gameID;
                paramCode[1].Value = gameDBID;
                ds = SqlHelper.ExecSPDataSet("ServerInfo_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return ds;
		}
		#region 查看工具操作记录
		/// <summary>
		/// 查看工具操作记录
		/// </summary>
		/// <param name="userID">用户ID</param>
		/// <param name="beginDate">开始日期</param>
		/// <param name="endDate">结束日期</param>
		/// <returns></returns>
		public static DataSet OperateLog_Query(int userID,DateTime beginDate,DateTime endDate)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@BeginDate",SqlDbType.DateTime),
												   new SqlParameter("@EndDate",SqlDbType.DateTime)};
				paramCode[0].Value=userID;
				paramCode[1].Value=beginDate;
				paramCode[2].Value=endDate;
				result = SqlHelper.ExecSPDataSet("GMTools_Log_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 删除管理员操作日志
		/// <summary>
		/// 删除管理员操作日志
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <returns></returns>
		public static int SDO_OperatorLogDel(int userByID)
		{
			int result = -1;
			try
			{
				result = SqlHelper.ExecCommand("delete from  GMTools_Log where UserID = "+userByID);
				result = SqlHelper.ExecCommand("delete from  GMTools_Log_UpdateAgo where UserID = "+userByID);
				result = SqlHelper.ExecCommand("delete from  GMTools_LogTime where OperateUserID = "+userByID);
			}
			catch(System.Data.SqlClient.SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;

		}
		#endregion
	    #region 查看bug信息
		/// <summary>
		/// 查看bug信息
		/// </summary>
		public static DataSet BugList_Query()
		{
			DataSet result = null;
			try
			{
				result = SqlHelper.ExecSPDataSet("GMTools_BugList_Query");
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看更新内容信息
		/// <summary>
		/// 查看bug信息
		/// </summary>
		public static DataSet UpdateList_Query()
		{
			DataSet result = null;
			try
			{
				result = SqlHelper.ExecSPDataSet("gmtools_UpatchList_Query");
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 提交新BUG信息
		/// <summary>
		/// 提交新BUG信息
		/// </summary>
		public static int BugList_Insert(int userbyID,string bugSubject,string bugContext,int gameID,int bugType)
		{
			int status = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@GM_UserID",SqlDbType.Int),
												   new SqlParameter("@GM_bugSubject",SqlDbType.VarChar,30),
												   new SqlParameter("@GM_GameID",SqlDbType.Int),
												   new SqlParameter("@GM_bugType",SqlDbType.Int),
												   new SqlParameter("@GM_bugContext",SqlDbType.VarChar,300),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userbyID;
				paramCode[1].Value = bugSubject;
				paramCode[2].Value = gameID;
				paramCode[3].Value = bugType;
				paramCode[4].Value = bugContext;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				status = SqlHelper.ExecSPCommand("gmtools_BugList_Insert",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return status;
		}
		#endregion
		#region 处理BUG信息
		/// <summary>
		/// 处理BUG信息
		/// </summary>
		public static int BugList_Update(int userbyID,int bugID,int gameID,int bugType,string subject,string context,string result)
		{
			int status = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[8]{
												   new SqlParameter("@GM_UserID",SqlDbType.Int),
												   new SqlParameter("@GM_BugID",SqlDbType.Int),
												   new SqlParameter("@GM_gameID",SqlDbType.Int),
												   new SqlParameter("@GM_bugType",SqlDbType.Int),
												   new SqlParameter("@GM_BugSubject",SqlDbType.VarChar,30),
												   new SqlParameter("@GM_BugContext",SqlDbType.VarChar,500),
												   new SqlParameter("@GM_BugResult",SqlDbType.VarChar,300),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userbyID;
				paramCode[1].Value = bugID;
				paramCode[2].Value = gameID;
				paramCode[3].Value = bugType;
				paramCode[4].Value = subject;
				paramCode[5].Value = context;
				paramCode[6].Value = result;
				paramCode[7].Direction = ParameterDirection.ReturnValue;
				status = SqlHelper.ExecSPCommand("GMTools_BugList_Update",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return status;
		}
		#endregion
	}
}
