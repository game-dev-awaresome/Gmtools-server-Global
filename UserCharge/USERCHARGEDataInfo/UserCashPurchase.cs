using System;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
using Common.API;
namespace UserCharge.UserChargeInfo
{
    class UserCashPurchase
    {
        #region 查看G币购买记录
        /// <summary>
        /// 查看G币购买记录
        /// </summary>
        /// <param name="serverIP"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static DataSet UserGCashPurchase_Query(string serverIP, string userName,string getUserName, DateTime beginDate, DateTime endDate, string sex,int itemStyle,string presentBox,string present)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[9]{
                                                   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_UserID",SqlDbType.VarChar,20),
                                                   new SqlParameter("@AU_GetUserID",SqlDbType.VarChar,20),
												   new SqlParameter("@AU_BeginDate",SqlDbType.DateTime),
                                                   new SqlParameter("@AU_EndDate",SqlDbType.DateTime),
                                                   new SqlParameter("@AU_Sex",SqlDbType.VarChar,2),
                                                   new SqlParameter("@AU_ItemStyle",SqlDbType.Int),
                                                   new SqlParameter("@AU_PresentBox",SqlDbType.VarChar,2),
                                                   new SqlParameter("@AU_Present",SqlDbType.VarChar,2)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = userName;
                paramCode[2].Value = getUserName;
                paramCode[3].Value = beginDate;
                paramCode[4].Value = endDate;
                paramCode[5].Value = sex;
                paramCode[6].Value = itemStyle;
                paramCode[7].Value = presentBox;
                paramCode[8].Value = present;
                result = SqlHelper.ExecSPDataSet("AUShop_UserGCashPurchase_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion
        #region 查看G币购买记录合计
        /// <summary>
        /// 查看G币购买记录合计
        /// </summary>
        /// <param name="serverIP"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static DataSet UserGCashPurchaseSum_Query(string serverIP, string userName, string getUserName, DateTime beginDate, DateTime endDate, string sex, int itemStyle, string presentBox, string present)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[9]{
                                                   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_UserID",SqlDbType.VarChar,20),
                                                   new SqlParameter("@AU_GetUserID",SqlDbType.VarChar,20),
												   new SqlParameter("@AU_BeginDate",SqlDbType.DateTime),
                                                   new SqlParameter("@AU_EndDate",SqlDbType.DateTime),
                                                   new SqlParameter("@AU_Sex",SqlDbType.VarChar,4),
                                                   new SqlParameter("@AU_ItemStyle",SqlDbType.Int),
                                                   new SqlParameter("@AU_PresentBox",SqlDbType.VarChar,2),
                                                   new SqlParameter("@AU_Present",SqlDbType.VarChar,2)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = userName;
                paramCode[2].Value = getUserName;
                paramCode[3].Value = beginDate;
                paramCode[4].Value = endDate;
                paramCode[5].Value = sex;
                paramCode[6].Value = itemStyle;
                paramCode[7].Value = presentBox;
                paramCode[8].Value = present;
                result = SqlHelper.ExecSPDataSet("AUShop_UserGCashPurchaseSum_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
         #endregion
        #region 查看M币购买记录
        /// <summary>
        /// 查看M币购买记录
        /// </summary>
        /// <param name="serverIP"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static DataSet UserMCashPurchase_Query(string serverIP, string userName, string getUserName, DateTime beginDate, DateTime endDate, string sex,int itemStyle, string presentBox, string present)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[9]{
                                                   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_UserID",SqlDbType.VarChar,20),
                                                   new SqlParameter("@AU_GetUserID",SqlDbType.VarChar,20),
												   new SqlParameter("@AU_BeginDate",SqlDbType.DateTime),
                                                   new SqlParameter("@AU_EndDate",SqlDbType.DateTime),
                                                   new SqlParameter("@AU_Sex",SqlDbType.VarChar,4),
                                                   new SqlParameter("@AU_ItemStyle",SqlDbType.Int),
                                                   new SqlParameter("@AU_PresentBox",SqlDbType.VarChar,2),
                                                   new SqlParameter("@AU_Present",SqlDbType.VarChar,2)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = userName;
                paramCode[2].Value = getUserName;
                paramCode[3].Value = beginDate;
                paramCode[4].Value = endDate;
                paramCode[5].Value = sex;
                paramCode[6].Value = itemStyle;
                paramCode[7].Value = presentBox;
                paramCode[8].Value = present;
                result = SqlHelper.ExecSPDataSet("AUShop_UserMCashPurchase_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion
        #region 查看M币购买记录合计
        /// <summary>
        /// 查看M币购买记录合计
        /// </summary>
        /// <param name="serverIP"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static DataSet UserMCashPurchaseSum_Query(string serverIP, string userName, string getUserName, DateTime beginDate, DateTime endDate, string sex, int itemStyle, string presentBox, string present)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[9]{
                                                   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_UserID",SqlDbType.VarChar,20),
                                                   new SqlParameter("@AU_GetUserID",SqlDbType.VarChar,20),
												   new SqlParameter("@AU_BeginDate",SqlDbType.DateTime),
                                                   new SqlParameter("@AU_EndDate",SqlDbType.DateTime),
                                                   new SqlParameter("@AU_Sex",SqlDbType.VarChar,4),
                                                   new SqlParameter("@AU_ItemStyle",SqlDbType.Int),
                                                   new SqlParameter("@AU_PresentBox",SqlDbType.VarChar,2),
                                                   new SqlParameter("@AU_Present",SqlDbType.VarChar,2)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = userName;
                paramCode[2].Value = getUserName;
                paramCode[3].Value = beginDate;
                paramCode[4].Value = endDate;
                paramCode[5].Value = sex;
                paramCode[6].Value = itemStyle;
                paramCode[7].Value = presentBox;
                paramCode[8].Value = present;
                result = SqlHelper.ExecSPDataSet("AUShop_UserMCashPurchaseSum_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
         #endregion
        #region 查看玩家积分记录
        /// <summary>
        /// 查看玩家积分记录
        /// </summary>
        /// <param name="serverIP"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static DataSet UserIntegral_Query(string serverIP, string account)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[2]{
                                                   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_UserID",SqlDbType.VarChar,20),};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = account;
                result = SqlHelper.ExecSPDataSet("AUShop_UserIntegral_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
       #endregion
        #region 查看道具回收兑换记录
        /// <summary>
        /// 查看道具回收兑换记录
        /// </summary>
        /// <param name="serverIP"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static DataSet UserAvatarItemRev_Query(string serverIP, string account, DateTime beginDate, DateTime endDate)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[4]{
                                                   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@AU_BeginDate",SqlDbType.DateTime),
                                                   new SqlParameter("@AU_EndDate",SqlDbType.DateTime)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = account;
                paramCode[2].Value = beginDate;
                paramCode[3].Value = endDate;
                result = SqlHelper.ExecSPDataSet("AUShop_UserAvatarItemRev_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion
		#region 查看道具回收兑换详细记录
		/// <summary>
		/// 查看道具回收兑换详细记录
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet UserAvatarItemRevDetail_Query(string serverIP, string orderID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_OrderID",SqlDbType.VarChar,250)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = orderID;
				result = SqlHelper.ExecSPDataSet("AUShop_UserAvatarRev_Query", paramCode);
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
