using System;

using System.Data;
using System.Data.SqlClient;
using Common.Logic;
namespace CR.CRDataInfo
{
    class CRAccountInfo
    {
        #region 即时查看该玩家激活状态
        /// <summary>
        /// 即时查看该玩家激活状态
        /// </summary>
        /// <returns></returns>
        public static DataSet CR_Account_Query(string serverIP, string account,string userNick,int action)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                         paramCode = new SqlParameter[4]{
												   new SqlParameter("@CR_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@CR_UserName",SqlDbType.VarChar,20),
                                                   new SqlParameter("@CR_UserNick",SqlDbType.VarChar,50),
                                                   new SqlParameter("@CR_ActionType",SqlDbType.Int)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = account;
                paramCode[2].Value = userNick;
                paramCode[3].Value = action;
                result = SqlHelper.ExecSPDataSet("CR_Account_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion
        #region 即时查看激活码是否被使用过
      /// <summary>
        /// 即时查看激活码是否被使用过
      /// </summary>
      /// <param name="serverIP"></param>
      /// <param name="password"></param>
      /// <param name="activeNum"></param>
      /// <returns></returns>
 
        public static DataSet CR_AccountActive_Query(string username,string password,string activeNum)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[3]{
												   new SqlParameter("@CR_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@CR_PASS",SqlDbType.VarChar,10),
                                                   new SqlParameter("@CR_NUM",SqlDbType.VarChar,10)};
                paramCode[0].Value = "61.129.90.151";
                paramCode[1].Value = password;
                paramCode[2].Value = activeNum;
                result = SqlHelper.ExecSPDataSet("CR_Account_ActiveNUM_Query", paramCode);

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
