using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
namespace BAF.O2JAM2DataInfo
{
	/// <summary>
	/// ItemShopInfo 的摘要说明。
	/// </summary>
	public class ItemShopInfo
	{
		public ItemShopInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 查看玩家身上物品道具
		/// <summary>
		/// 查看玩家身上道具信息
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet AvatarItemList_Query(string serverIP,string userID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=userID;
				result = SqlHelper.ExecSPDataSet("o2jam2_item_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 删除玩家身上道具
		/// <summary>
		/// 删除玩家物品信息
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int itemShop_Delete(int userByID,string serverIP,string userIndexID,int itemCode)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@O2JAM2_ItemCode",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=userIndexID;
				paramCode[3].Value=itemCode;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("o2jam2_item_delete",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家礼物盒的道具
		/// <summary>
		/// 查看玩家礼物盒的道具
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet O2JAM2GiftBox_Query(string serverIP,string userID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=userID;
				result = SqlHelper.ExecSPDataSet("O2jam2_Message_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看道具的状态
		/// <summary>
		/// 查看道具的状态
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="itemCode">道具代码</param>
		/// <returns></returns>
		public static DataSet itemShop_Status_Query(string serverIP,int itemCode)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_Itemcode",SqlDbType.Int)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=itemCode;
				result = SqlHelper.ExecSPDataSet("O2JAM2_ItemShop_Query2",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看游戏里面道具列表
		/// <summary>
		/// 查看游戏里面道具列表
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="bigType">道具大类</param>
		/// <param name="smallType">道具小类</param>
		/// <returns>道具数据集</returns>
		public static DataSet itemShop_QueryAll(string serverIP,string itemName,string userID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
											       new SqlParameter("@O2JAM2_ItemName",SqlDbType.VarChar,30),
											       new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=itemName;
				paramCode[2].Value=userID;
				result = SqlHelper.ExecSPDataSet("o2jam2_item_QueryAll",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 添加玩家礼物盒上物品
		/// <summary>
		/// 添加玩家礼物盒上物品
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int giftBox_Insert(int userByID,string serverIP,string userID,int itemCode,string title,string context,int timesLimit,int dayLimit)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[9]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@O2JAM2_Title",SqlDbType.VarChar,40),
												   new SqlParameter("@O2JAM2_Content",SqlDbType.VarChar,400),
												   new SqlParameter("@O2JAM2_ItemCode",SqlDbType.Int),
												   new SqlParameter("@O2JAM2_TimesLimit",SqlDbType.Int),
												   new SqlParameter("@O2JAM2_dayslimit",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=userID;
				paramCode[3].Value=title;
				paramCode[4].Value=context;
				paramCode[5].Value=itemCode;
				paramCode[6].Value=timesLimit;
				paramCode[7].Value=dayLimit;
				paramCode[8].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("O2jam2_Message_Insert",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 删除玩家礼物盒上道具
		/// <summary>
		/// 删除玩家礼物盒上道具
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int giftBox_Delete(int userByID,string serverIP,string userID,int itemCode)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@O2JAM2_ItemCode",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=userID;
				paramCode[3].Value=itemCode;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("O2jam2_Message_delete",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 检测玩家上下线记录
		/// <summary>
		/// 检测玩家上下线记录
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet userOnline_Query(string serverIP,string account)
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
				result = SqlHelper.ExecSPDataSet("SDO_UserOnline_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 检测玩家消费记录
		/// <summary>
		/// 检测玩家消费记录
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet userConsume_Query(string serverIP,string account,int moneyType,DateTime beginDate,DateTime endDate)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@O2JAM2_ConsumeType",SqlDbType.Int),
												   new SqlParameter("@O2JAM2_BeginDate",SqlDbType.DateTime),
												   new SqlParameter("@O2JAM2_EndDate",SqlDbType.DateTime)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=account;
				paramCode[2].Value=moneyType;
				paramCode[3].Value = beginDate;
				paramCode[4].Value = endDate;
				result = SqlHelper.ExecSPDataSet("o2jam2_ConsumeLog_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
        #region 检测玩家消费记录合计
        /// <summary>
        /// 检测玩家消费记录合计
        /// </summary>
        /// <param name="serverIP">服务器IP</param>
        /// <param name="account">玩家帐号</param>
        /// <returns></returns>
        public static DataSet userConsume_QuerySum(string serverIP, string account, int moneyType, DateTime beginDate, DateTime endDate)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[5]{
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@O2JAM2_ConsumeType",SqlDbType.Int),
												   new SqlParameter("@O2JAM2_BeginDate",SqlDbType.DateTime),
												   new SqlParameter("@O2JAM2_EndDate",SqlDbType.DateTime)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = account;
                paramCode[2].Value = moneyType;
                paramCode[3].Value = beginDate;
                paramCode[4].Value = endDate;
                result = SqlHelper.ExecSPDataSet("o2jam2_ConsumeLog_Sum_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion
		#region 检测玩家交易记录
		/// <summary>
		/// 检测玩家交易记录
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet userTrade_Query(string serverIP,string sendUserID,string recvUserID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@O2JAM2_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM2_SenderUserID",SqlDbType.VarChar,20),
                                                   new SqlParameter("@O2JAM2_ReceiveUserID",SqlDbType.VarChar,20)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=sendUserID;
                paramCode[2].Value = recvUserID;
				result = SqlHelper.ExecSPDataSet("o2jam2_ConsumeLog_Sum_Query",paramCode);
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
