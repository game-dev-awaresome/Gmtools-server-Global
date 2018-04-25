using System;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
using Common.API;
namespace UserCharge.UserChargeInfo
{
    class UserConsumeDetailInfo
    {
        #region 查看玩家资料
        /// <summary>
        /// 查看玩家资料
        /// </summary>
        /// <param name="serverIP"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static DataSet UserConsumeDetailInfo_Query(string serverIP, string account, string CardID, string CardPwd, int actionType)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[5]{
                                                   new SqlParameter("@AU_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@AU_ActionType",SqlDbType.Int),
												   new SqlParameter("@AU_User",SqlDbType.VarChar,20),
                                                   new SqlParameter("@AU_Card",SqlDbType.VarChar,20),
                                                   new SqlParameter("@AU_Pwd",SqlDbType.VarChar,20)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = actionType;
                paramCode[2].Value = account;
                paramCode[3].Value = CardID;
                paramCode[4].Value = CardPwd;
                result = SqlHelper.ExecSPDataSet("Audition_RecruitInfo_Query", paramCode);
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
