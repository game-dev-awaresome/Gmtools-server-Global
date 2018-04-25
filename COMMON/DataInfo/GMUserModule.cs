using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
namespace Common.DataInfo
{
	/// <summary>
	/// GMUserModule ��ժҪ˵����
	/// </summary>
	public class GMUserModule
	{
		/// <summary>
		/// �û�ID
		/// </summary>
		private int userID = 0;
		/// <summary>
		/// ģ���б�
		/// </summary>
		private string moduleList;
		public GMUserModule()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
        }
        #region ��ѯ�����û�GM�ʺŵ���Ϣ
        /// <summary>
		/// ��ѯ�����û�GM�ʺŵ���Ϣ
		/// </summary>
		/// <returns></returns>
		public static DataSet SelectAll(int userID)
		{
			string strSQL="select * from GMTOOLS_Roles where [user]= "+userID;
			DataSet ds = new DataSet("GMTOOLSRoles");
			try
			{
				ds =SqlHelper.ExecuteDataset(strSQL);
			}
			catch(SqlException ex)
			{
				System.Console.WriteLine(ex.Message);
			}
			return ds;
		}
		#endregion
		#region �û���ģ��Ĺ���
		public static int UserModuleAdmin(int userID,string moduleList)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{ new SqlParameter("@GM_UserID",SqlDbType.Int),
												   new SqlParameter("@GM_ModuleList",SqlDbType.VarChar,1000),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userID;
				paramCode[1].Value = moduleList;
				paramCode[2].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("dbo.Gmtool_GmUserModule_Admin",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		public int UserID
		{
			get
			{
				return this.userID;
			}
			set
			{
				this.userID =value;
			}

		}
		public string ModuleList
		{
			get
			{
				return this.moduleList;
			}
			set
			{
				this.moduleList =value;
			}

		}
	}
}
