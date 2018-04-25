using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
namespace Common.DataInfo
{
	/// <summary>
	/// GMGameInfo ��ժҪ˵����
	/// </summary>
	public class GMGameInfo
	{
		/// <summary>
		/// ��ϷID
		/// </summary>
		private int gameID = 0;
		/// <summary>
		/// ��Ϸ����
		/// </summary>
		private string gameName = null;
		/// <summary>
		/// ��Ϸ����
		/// </summary>
		private string gameContent = null;
		public GMGameInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public GMGameInfo(int gameID_,string gameName_,string gameContent_)
		{
			gameID = gameID_;
			gameName = gameName_;
			gameContent = gameContent_;
			
		}
		#region �õ�������Ϸ��Ϣ
		/// <summary>
		/// �õ�ģ�����Ϣ
		/// </summary>
		/// <returns></returns>
		public static DataSet SelectAll()
		{
			string strSQL="select * from GAMELIST";
			try
			{
				return SqlHelper.ExecuteDataset(strSQL);

			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}
		#endregion
		#region �õ�һ����Ϸ��Ϣ��
		/// <summary>
		/// �õ�һ����Ϸ��Ϣ��
		/// </summary>
		/// <param name="gameID">��ϷID</param>
		/// <returns></returns>
		public static DataSet QueryModuleInfo(int gameID)
		{
			string strSQL;
			strSQL = "select * from GameList where ID="+gameID;

			System.Data.DataSet ds = SqlHelper.ExecuteDataset(strSQL);

			return ds;
		}
		#endregion
		#region ����һ����Ϸ��Ϣ
        /// <summary>
        /// ����һ����Ϸ��Ϣ
        /// </summary>
        /// <param name="Name">��Ϸ����</param>
        /// <param name="content">��Ϸ����</param>
        /// <returns>�������</returns>
		public static int insertRow(int userByID,string Name,string content)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												   new SqlParameter("@Gm_GameName",SqlDbType.VarChar,50),
												   new SqlParameter("@Gm_GameContext",SqlDbType.VarChar,400),
												   new SqlParameter("@result",SqlDbType.Int)
											   };
				paramCode[0].Value=userByID;
				paramCode[1].Value=Name.Trim();
				paramCode[2].Value=content.Trim();
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Gmtool_GAME_Add",paramCode);
				if(userByID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(userByID);
				}
                
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);

			}
			return result;
		}
		#endregion
		#region ����ĳ����Ϸ��Ϣ
		/// <summary>
		/// ����ĳ����Ϸ��Ϣ
		/// </summary>
		/// <param name="gameID">��ϷID</param>
		/// <param name="gameName">��Ϸ����</param>
		/// <param name="gameContent">��Ϸ����</param>
		public int updateRow(int userByID,int gameID,string gameName,string gameContent)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												   new SqlParameter("@Gm_GameID",SqlDbType.Int),
												   new SqlParameter("@Gm_GameName",SqlDbType.VarChar,50),
												   new SqlParameter("@Gm_GameContext",SqlDbType.VarChar,400),
												   new SqlParameter("@result",SqlDbType.Int)
											   };
				paramCode[0].Value=userByID;
				paramCode[1].Value=gameID;
				paramCode[2].Value=gameName.Trim();
				paramCode[3].Value=gameContent.Trim();
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Gmtool_GAME_Edit",paramCode);
				if(userByID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(userByID);
				}

			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);

			}
			return result;
		}
		#endregion
		#region ɾ����Ϸ��Ϣ
		/// <summary>
		/// ɾ����Ϸ��Ϣ
		/// </summary>
		/// <param name="gameID">��ϷID</param>
		public static int  deleteRow(int userByID,int gameID)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{ new SqlParameter("@Gm_GAMEID",SqlDbType.Int),
												   new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=gameID;
				paramCode[1].Value=userByID;
				paramCode[2].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Gmtool_GAME_Del",paramCode);
				if(userByID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(userByID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		public int GameID
		{
			get
			{
				return this.gameID;
			}
			set
			{
				this.gameID = value;
			}
		}
		public string GameName
		{
			get
			{
				return this.gameName;
			}
			set
			{
				this.gameName =value;
			}
		}
		public string GameContent
		{
			get
			{
				return this.gameContent;
			}
			set
			{
				this.gameContent =value;
			}
		}
	}
}
