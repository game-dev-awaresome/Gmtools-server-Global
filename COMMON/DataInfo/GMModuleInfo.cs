using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
namespace Common.DataInfo
{
	/// <summary>
	/// GMModuleInfo ��ժҪ˵����
	/// </summary>
	public class GMModuleInfo
	{
		/// <summary>
		/// ��ϷID
		/// </summary>
		int gameID = 0;
		/// <summary>
		/// ģ��ID
		/// </summary>
		private int moduleID;
		/// <summary>
		/// ģ������
		/// </summary>
		private string moduleName;
		/// <summary>
		/// ģ����
		/// </summary>
		private string moduleClass;
		/// <summary>
		/// ģ������
		/// </summary>
		private string moduleContent;
		public GMModuleInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public GMModuleInfo(int layer,int moduleID,string moduleName,string moduleClass,string moduleContent)
		{
			this.GameID = layer;
			this.ModuleID =moduleID;
			this.ModuleName=moduleName;
			this.ModuleClass=moduleClass;
			this.ModuleContent=moduleContent;
		}
		#region �õ�����ģ�����Ϣ
		/// <summary>
		/// �õ�ģ�����Ϣ
		/// </summary>
		/// <returns></returns>
		public static DataSet SelectAll()
		{
			string strSQL="select a.game_ID,b.module_ID,a.game_Name,b.module_name,b.module_class,b.module_content from GAMELIST a ,GMTOOLS_Modules b where a.game_ID=b.game_id order by a.game_ID";
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
		#region ���һ��ģ����Ϣ
		/// <summary>
		/// ����һ��ģ����Ϣ
		/// </summary>
		/// <param name="operateUserID">����ԱID</param>
		/// <param name="gameID">��ϷID</param>
		/// <param name="Name">ģ������</param>
		/// <param name="Class">ģ������</param>
		/// <param name="content">ģ������</param>
		public static int insertRow(int operateUserID,int gameID,string Name,string Class,string content)
		{
			int result = -1;
			//string insertSql = "Insert into GMTOOLS_Modules (layer,Name,class,content) values (@layer,@name,@class,@content) ";
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												   new SqlParameter("@GM_GameID",SqlDbType.Int),
												   new SqlParameter("@GM_Name",SqlDbType.VarChar,50),
												   new SqlParameter("@GM_Class",SqlDbType.VarChar,50),
												   new SqlParameter("@GM_Content",SqlDbType.VarChar,400),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = operateUserID;
				paramCode[1].Value=gameID;
				paramCode[2].Value=Name;
				paramCode[3].Value=Class;
				paramCode[4].Value=content;
				paramCode[5].Direction=ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Gmtool_GmModule_Insert",paramCode);
				if(operateUserID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(operateUserID);
				}
                
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);

			}
			return result;
		}
		#endregion
		#region �õ�һ��ģ����Ϣ
		public static DataSet QueryModuleInfo(int moduleID)
		{
			string strSQL;
			strSQL = "select * from GMTOOLS_Modules where ID="+moduleID;

			System.Data.DataSet ds = SqlHelper.ExecuteDataset(strSQL);

			return ds;
		}
		#endregion
		#region �õ��û�����Ӧ��ģ����Ϣ
		public static DataSet getModuleInfo(int userID)
		{
			System.Data.DataSet ds;
			string strSQL;
			try
			{
				strSQL = "select a.game_ID,b.module_ID,a.game_Name,b.module_name,b.module_class,b.module_content from GAMELIST a ,GMTOOLS_Modules b,GMTOOLS_Users c,GMTOOLS_Roles d where a.game_ID=d.game_id and b.module_ID=d.module_ID and c.userID=d.userID and c.userID="+userID;
				ds = SqlHelper.ExecuteDataset(strSQL);
				return ds;
                
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}

		}
		#endregion
		#region ����һ��ģ���¼
		public int updateRow(int operateUserID,int moduleID)
		{
			int result = -1;
			//string updateSql ="update GMTOOLS_Modules set moduleName=@moduleName,moduleClass=@moduleClass,moduleContent=@moduleContent where ID="+moduleID;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[7]{
												   new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												   new SqlParameter("@GM_ModuleID",SqlDbType.Int),
												   new SqlParameter("@GM_GameID",SqlDbType.Int),
												   new SqlParameter("@GM_Name",SqlDbType.VarChar,50),
												   new SqlParameter("@GM_Class",SqlDbType.VarChar,50),
												   new SqlParameter("@GM_Content",SqlDbType.VarChar,400),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = operateUserID;
				paramCode[1].Value=ModuleID;
				paramCode[2].Value=GameID;
				paramCode[3].Value=ModuleName;
				paramCode[4].Value=ModuleClass;
				paramCode[5].Value=ModuleContent;
				paramCode[6].Direction = ParameterDirection.ReturnValue;
				result  = SqlHelper.ExecSPCommand("Gmtool_GmModule_Update",paramCode);
				if(operateUserID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(operateUserID);
				}

			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);

			}
			return result;
		}
        #endregion
		#region ɾ��һ��ģ���¼
        /// <summary>
        /// ɾ��ģ����Ϣ
        /// </summary>
        /// <param name="userByID">����ԱID</param>
        /// <param name="moduleID">����ԱID</param>
		public static int deleteRow(int userByID,int moduleID)
		{
			SqlParameter[] paramCode;
			int result= -1;
			//string deleteSql = "delete from GMTOOLS_Modules where ID="+moduleID;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												   new SqlParameter("@GM_ModuleID",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = moduleID;
				paramCode[2].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Gmtool_GmModule_Delete",paramCode);
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
		#region ��ϷID
		public int GameID
		{
			get
			{
				return this.gameID;
			}
			set
			{
				this.gameID=value;
			}
		}
		#endregion
		#region ģ��ID
		public int ModuleID
		{
			get
			{
				return this.moduleID;
			}
			set
			{
				this.moduleID = value;
			}
		}
		#endregion
		#region ģ������
		public string ModuleName
		{
			get
			{
				return this.moduleName;
			}
			set
			{
				this.moduleName=value;
			}
		}
		#endregion
		#region ģ������
		public string ModuleClass
		{

			get
			{
				return this.moduleClass;
			}
			set
			{
				this.moduleClass=value;
			}
		}
		#endregion
		#region ģ������
		public string ModuleContent
		{
			get
			{
				return this.moduleContent;
			}
			set
			{
				this.moduleContent=value;
			}
		}
        #endregion
	}
}
