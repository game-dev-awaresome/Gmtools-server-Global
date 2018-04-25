using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
using Common.DataInfo;

namespace SDO.SDODataInfo
{
	/// <summary>
	/// ChallengeDataInfo 的摘要说明。
	/// </summary>
	public class ChallengeDataInfo
	{
		public ChallengeDataInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 查看游戏里面场景列表
		/// <summary>
		/// 查看游戏里面场景列表
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static DataSet TO2JAM_Challenge_Query(string serverIP)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30)};
				paramCode[0].Value=serverIP;
				result = SqlHelper.ExecSPDataSet("SDO_TO2JAM_Challenge_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看游戏里面音乐列表
		/// <summary>
		/// 查看游戏里面音乐列表
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static DataSet TO2JAM_MusicData_Query(string serverIP)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30)};
				paramCode[0].Value=serverIP;
				result = SqlHelper.ExecSPDataSet("SDO_TO2JAM_MusicData_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 根据ID查看游戏里面音乐列表
		/// <summary>
		/// 根据ID查看游戏里面音乐列表
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static DataSet TO2JAM_MusicData_Query(string serverIP,int musicID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_MusicID",SqlDbType.Int)};
				paramCode[0].Value =serverIP;
				paramCode[1].Value = musicID;
				result = SqlHelper.ExecSPDataSet("SDO_TO2JAM_MusicData_SingleQuery",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看游戏里面场景列表
		/// <summary>
		/// 查看游戏里面场景列表
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static DataSet TO2JAM_SceneListQuery()
		{
			DataSet result = null;
			try
			{
				result = SqlHelper.ExecSPDataSet("SDO_TO2JAM_Scene_Query");
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 添加游戏里面场景列表
		/// <summary>
		/// 添加游戏里面场景
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static int TO2JAM_ChallengeScene_Insert(int GMUserID,int sceneID,string sceneTag)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
													new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
													new SqlParameter("@SDO_SceneID",SqlDbType.Int),
													new SqlParameter("@SDO_SceneTag",SqlDbType.VarChar,400),
													new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=sceneID;
				paramCode[2].Value=sceneTag;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_TO2JAM_Scene_Insert",paramCode);
				if(GMUserID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(GMUserID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 更新游戏里面场景列表
		/// <summary>
		/// 更新游戏里面场景
		/// </summary>
		/// <returns>道具数据集</returns>
		public static int TO2JAM_ChallengeScene_Update(int GMUserID,int sceneID,string sceneTag)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_SceneID",SqlDbType.Int),
												   new SqlParameter("@SDO_SceneTag",SqlDbType.VarChar,400),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=sceneID;
				paramCode[2].Value=sceneTag;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_TO2JAM_Scene_Update",paramCode);
				if(GMUserID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(GMUserID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 删除游戏里面场景列表
		/// <summary>
		/// 删除游戏里面场景
		/// </summary>
		/// <returns>道具数据集</returns>
		public static int TO2JAM_ChallengeScene_Delete(int GMUserID,int sceneID)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_SceneID",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=sceneID;
				paramCode[2].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_TO2JAM_Scene_Delete",paramCode);
				if(GMUserID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(GMUserID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看场景中获得道具概率列表
		/// <summary>
		/// 查看场景中获得道具概率列表
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static DataSet TO2JAM_Medalitem_Query(string serverIP)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30)};
				paramCode[0].Value=serverIP;
				result = SqlHelper.ExecSPDataSet("SDO_TO2JAM_Medalitem_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 根据道具名查看场景中获得道具概率列表
		/// <summary>
		/// 根据道具名查看场景中获得道具概率列表
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static DataSet TO2JAM_Medalitem_Owner_Query(string serverIP,string itemName)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_Name",SqlDbType.VarChar,50)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=itemName;
				result = SqlHelper.ExecSPDataSet("SDO_TO2JAM_Medalitem_Own_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 添加在场景获得概率
		/// <summary>
		/// 添加在场景获得概率
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static int TO2JAM_Medalitem_Insert(int GMUserID,string serverIP,int itemcode,int percent,int timeslimit,int dayslimit)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[7]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_ItemCode",SqlDbType.Int),
												   new SqlParameter("@SDO_Percent",SqlDbType.Int),
												   new SqlParameter("@SDO_timeslimit",SqlDbType.Int),
												   new SqlParameter("@SDO_DaysLimit",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=itemcode;
				paramCode[3].Value=percent;
				paramCode[4].Value=timeslimit;
				paramCode[5].Value=dayslimit;
				paramCode[6].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_TO2JAM_Medalitem_Insert",paramCode);
				if(GMUserID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(GMUserID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 更新在场景获得概率
		/// <summary>
		/// 更新在场景获得概率
		/// </summary>
		/// <returns>道具数据集</returns>
		public static int TO2JAM_Medalitem_Update(int GMUserID,string serverIP,int itemcode,int percent,int timeslimit,int dayslimit)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[7]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_ItemCode",SqlDbType.Int),
												   new SqlParameter("@SDO_Percent",SqlDbType.Int),
												   new SqlParameter("@SDO_timeslimit",SqlDbType.Int),
												   new SqlParameter("@SDO_DaysLimit",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=itemcode;
				paramCode[3].Value=percent;
				paramCode[4].Value=timeslimit;
				paramCode[5].Value=dayslimit;
				paramCode[6].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_TO2JAM_Medalitem_Update",paramCode);
				if(GMUserID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(GMUserID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 删除在场景获得概率
		/// <summary>
		/// 删除在场景获得概率
		/// </summary>
		/// <returns>道具数据集</returns>
		public static int TO2JAM_Medalitem_Delete(int GMUserID,string serverIP,int itemcode)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_ItemCode",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=itemcode;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_TO2JAM_Medalitem_Delete",paramCode);
				if(GMUserID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(GMUserID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 添加游戏里面场景
		/// <summary>
		/// 添加游戏里面场景
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static int TO2JAM_Challenge_Insert(int GMUserID,string serverIP,int WeekDay,int MatPt_hr,int MatPt_min,int StPt_hr,int StPt_min,int EdPt_hr,int EdPt_min,int GCash,int MCash,int Scene,int musicID1,int lv1,int musicID2,int lv2,int musicID3,int lv3,int musicID4,int lv4,int musicID5,int lv5)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[23]{
													new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
													new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
													new SqlParameter("@SDO_WeekDay",SqlDbType.Int),
													new SqlParameter("@SDO_MatPtHR",SqlDbType.Int),
													new SqlParameter("@SDO_MatPtMin",SqlDbType.Int),
													new SqlParameter("@SDO_StPtHR",SqlDbType.Int),
													new SqlParameter("@SDO_StPtMin",SqlDbType.Int),
													new SqlParameter("@SDO_EdPtHR",SqlDbType.Int),
													new SqlParameter("@SDO_EdPtMin",SqlDbType.Int),
													new SqlParameter("@SDO_GCash",SqlDbType.Int),
													new SqlParameter("@SDO_MCash",SqlDbType.Int),
													new SqlParameter("@SDO_Sence",SqlDbType.Int),
													new SqlParameter("@SDO_MusicID1",SqlDbType.Int),
													new SqlParameter("@SDO_LV1",SqlDbType.Int),
													new SqlParameter("@SDO_MusicID2",SqlDbType.Int),
													new SqlParameter("@SDO_LV2",SqlDbType.Int),
													new SqlParameter("@SDO_MusicID3",SqlDbType.Int),
													new SqlParameter("@SDO_LV3",SqlDbType.Int),
													new SqlParameter("@SDO_MusicID4",SqlDbType.Int),
													new SqlParameter("@SDO_LV4",SqlDbType.Int),
													new SqlParameter("@SDO_MusicID5",SqlDbType.Int),
													new SqlParameter("@SDO_LV5",SqlDbType.Int),
				                                    new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=WeekDay;
				paramCode[3].Value=MatPt_hr;
				paramCode[4].Value=MatPt_min;
				paramCode[5].Value=StPt_hr;
				paramCode[6].Value=StPt_min;
				paramCode[7].Value=EdPt_hr;
				paramCode[8].Value=EdPt_min;
				paramCode[9].Value=GCash;
				paramCode[10].Value=MCash;
				paramCode[11].Value=Scene;
				paramCode[12].Value=musicID1;
				paramCode[13].Value=lv1;
				paramCode[14].Value=musicID2;
				paramCode[15].Value=lv2;
				paramCode[16].Value=musicID3;
				paramCode[17].Value=lv3;
				paramCode[18].Value=musicID4;
				paramCode[19].Value=lv4;
				paramCode[20].Value=musicID5;
				paramCode[21].Value=lv5;
				paramCode[22].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_TO2JAM_Challenge_Insert",paramCode);
				if(GMUserID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(GMUserID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 更新游戏里面场景
		/// <summary>
		/// 更新游戏里面场景
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static int TO2JAM_Challenge_Update(int GMUserID,string serverIP,int sceneID,int WeekDay,int MatPt_hr,int MatPt_min,int StPt_hr,int StPt_min,int EdPt_hr,int EdPt_min,int GCash,int MCash,int Scene,int musicID1,int lv1,int musicID2,int lv2,int musicID3,int lv3,int musicID4,int lv4,int musicID5,int lv5)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[24]{
													new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
													new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
													new SqlParameter("@SDO_SceneID",SqlDbType.Int),
													new SqlParameter("@SDO_WeekDay",SqlDbType.Int),
													new SqlParameter("@SDO_MatPtHR",SqlDbType.Int),
													new SqlParameter("@SDO_MatPtMin",SqlDbType.Int),
													new SqlParameter("@SDO_StPtHR",SqlDbType.Int),
													new SqlParameter("@SDO_StPtMin",SqlDbType.Int),
													new SqlParameter("@SDO_EdPtHR",SqlDbType.Int),
													new SqlParameter("@SDO_EdPtMin",SqlDbType.Int),
													new SqlParameter("@SDO_GCash",SqlDbType.Int),
													new SqlParameter("@SDO_MCash",SqlDbType.Int),
													new SqlParameter("@SDO_Sence",SqlDbType.Int),
													new SqlParameter("@SDO_MusicID1",SqlDbType.Int),
													new SqlParameter("@SDO_LV1",SqlDbType.Int),
													new SqlParameter("@SDO_MusicID2",SqlDbType.Int),
													new SqlParameter("@SDO_LV2",SqlDbType.Int),
													new SqlParameter("@SDO_MusicID3",SqlDbType.Int),
													new SqlParameter("@SDO_LV3",SqlDbType.Int),
													new SqlParameter("@SDO_MusicID4",SqlDbType.Int),
													new SqlParameter("@SDO_LV4",SqlDbType.Int),
													new SqlParameter("@SDO_MusicID5",SqlDbType.Int),
													new SqlParameter("@SDO_LV5",SqlDbType.Int),
													new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=sceneID;
				paramCode[3].Value=WeekDay;
				paramCode[4].Value=MatPt_hr;
				paramCode[5].Value=MatPt_min;
				paramCode[6].Value=StPt_hr;
				paramCode[7].Value=StPt_min;
				paramCode[8].Value=EdPt_hr;
				paramCode[9].Value=EdPt_min;
				paramCode[10].Value=GCash;
				paramCode[11].Value=MCash;
				paramCode[12].Value=Scene;
				paramCode[13].Value=musicID1;
				paramCode[14].Value=lv1;
				paramCode[15].Value=musicID2;
				paramCode[16].Value=lv2;
				paramCode[17].Value=musicID3;
				paramCode[18].Value=lv3;
				paramCode[19].Value=musicID4;
				paramCode[20].Value=lv4;
				paramCode[21].Value=musicID5;
				paramCode[22].Value=lv5;
				paramCode[23].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_TO2JAM_Challenge_Update",paramCode);
				if(GMUserID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(GMUserID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 删除游戏里面场景列表
		/// <summary>
		/// 删除游戏里面场景列表
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static int TO2JAM_Challenge_Del(string serverIP,int gmUserID,int sceneID)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_SceneID",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=gmUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=sceneID;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_TO2JAM_Challenge_Del",paramCode);
				if(gmUserID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(gmUserID);
				}
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
