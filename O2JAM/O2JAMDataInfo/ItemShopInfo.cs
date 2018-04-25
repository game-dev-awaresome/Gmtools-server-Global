using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;

namespace O2JAM.O2JAMDataInfo
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
		public static DataSet itemShop_Query(string serverIP,string userID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@O2JAM_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM_UserID",SqlDbType.VarChar,20)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=userID;
				result = SqlHelper.ExecSPDataSet("O2JAM_Item_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 添加玩家身上道具
		/// <summary>
		/// 添加玩家身上道具
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int GiftBox_Insert(int userByID,string serverIP,int userIndexID,int itemCode,int gem,int mCash,int expireDate)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[8]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@O2JAM_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM_UserIndexID",SqlDbType.Int),
												   new SqlParameter("@O2JAM_Itemcode",SqlDbType.Int),
												   new SqlParameter("@O2JAM_GemCash",SqlDbType.Int),
												   new SqlParameter("@O2JAM_MCash",SqlDbType.Int),
												   new SqlParameter("@O2JAM_Expire",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=userIndexID;
				paramCode[3].Value=itemCode;
				paramCode[4].Value=gem;
				paramCode[5].Value=mCash;
				paramCode[6].Value=expireDate;
				paramCode[7].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("O2JAM_GiftBox_Insert",paramCode);
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
		public static DataSet O2JAMGiftBox_Query(string serverIP,int userIndexID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@O2JAM_ServerIP",SqlDbType.VarChar,20),
												   new SqlParameter("@O2JAM_UserIndexID",SqlDbType.Int)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=userIndexID;
				result = SqlHelper.ExecSPDataSet("O2JAM_GiftBox_Query",paramCode);
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
		public static DataSet itemShop_Name_Query(string serverIP,int itemCode)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@O2JAM_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM_ID",SqlDbType.Int)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=itemCode;
				result = SqlHelper.ExecSPDataSet("O2JAM_ItemData_Query",paramCode);
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
		public static DataSet itemShop_QueryAll(string serverIP,string itemName,int itemSex)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@O2JAM_ServerIP",SqlDbType.VarChar,30),
											       new SqlParameter("@O2JAM_Name",SqlDbType.VarChar,30),
											       new SqlParameter("@O2JAM_Sex",SqlDbType.Int)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=itemName;
				paramCode[2].Value=itemSex;
				result = SqlHelper.ExecSPDataSet("O2JAM_ItemData_QueryAll",paramCode);
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
		public static int itemShop_Delete(int userByID,string serverIP,int userIndexID,int pos,int flag)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@O2JAM_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM_UserIndexID",SqlDbType.Int),
												   new SqlParameter("@O2JAM_Pos",SqlDbType.Int),
												   new SqlParameter("@O2JAM_modiType",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=userIndexID;
				paramCode[3].Value=pos;
				paramCode[4].Value=flag;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("O2JAM_Item_update",paramCode);
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
		public static int giftBox_Delete(int userByID,string serverIP,int userIndexID,int itemCode)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@O2JAM_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM_UserIndexID",SqlDbType.Int),
												   new SqlParameter("@O2JAM_Itemcode",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=userIndexID;
				paramCode[3].Value=itemCode;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("O2JAM_GiftBox_del",paramCode);
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
		public static DataSet userConsume_Query(string serverIP,string account,int kind,int buyType,DateTime beginDate,DateTime endDate)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@O2JAM_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM_Account",SqlDbType.VarChar,20),
												   new SqlParameter("@O2JAM_Flag",SqlDbType.Int),
												   new SqlParameter("@O2JAM_BuyType",SqlDbType.Int),
												   new SqlParameter("@O2JAM_BeginTime",SqlDbType.DateTime),
												   new SqlParameter("@O2JAM_EndTime",SqlDbType.DateTime)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=account;
			    paramCode[2].Value=kind;
				paramCode[3].Value=buyType;
				paramCode[4].Value = beginDate;
				paramCode[5].Value = endDate;
				result = SqlHelper.ExecSPDataSet("O2JAM_LOGLIST_Query",paramCode);
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
        public static DataSet userConsume_QuerySum(string serverIP, string account, int kind,int buyType, DateTime beginDate, DateTime endDate)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[6]{
												   new SqlParameter("@O2JAM_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM_Account",SqlDbType.VarChar,20),
												   new SqlParameter("@O2JAM_Flag",SqlDbType.Int),
												   new SqlParameter("@O2JAM_BuyType",SqlDbType.Int),
												   new SqlParameter("@O2JAM_BeginTime",SqlDbType.DateTime),
												   new SqlParameter("@O2JAM_EndTime",SqlDbType.DateTime)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = account;
				paramCode[2].Value=kind;
				paramCode[3].Value=buyType;
                paramCode[4].Value = beginDate;
                paramCode[5].Value = endDate;
                result = SqlHelper.ExecSPDataSet("O2JAM_LOGLIST_QuerySum", paramCode);
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
		public static DataSet userTrade_Query(string serverIP,string userID,int kind,DateTime beginDate,DateTime endDate)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@O2JAM_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@O2JAM_Account",SqlDbType.VarChar,20),
                                                   new SqlParameter("@O2JAM_Flag",SqlDbType.Int),
												   new SqlParameter("@O2JAM_BeginTime",SqlDbType.DateTime),
												   new SqlParameter("@O2JAM_EndTime",SqlDbType.DateTime),
				};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=userID;
				paramCode[2].Value = kind;
				paramCode[3].Value = beginDate;
				paramCode[4].Value = endDate;
				result = SqlHelper.ExecSPDataSet("O2JAM_LOGLIST_Query",paramCode);
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
