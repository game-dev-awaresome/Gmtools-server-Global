using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
using System.Security.Cryptography;
namespace Common.DataInfo
{
	/// <summary>
	/// GMUserInfo ��ժҪ˵����
	/// </summary>
	public class GMUserInfo
	{
		/// <summary>
		/// �û���
		/// </summary>
		private string userName;
		/// <summary>
		/// ����
		/// </summary>
		private string passWord;
		/// <summary>
		/// MAC��
		/// </summary>
		private string mac;
		/// <summary>
		/// ����
		/// </summary>
		private string realName;
		/// <summary>
		/// ϵͳ����Ա
		/// </summary>
		private int sysAdmin;

		public GMUserInfo(byte[] packet)
		{
			Message msg = new Message(packet,(uint)packet.Length);
			msg.GetMessageID();
		}
		public GMUserInfo(string userName,string password)
		{
			this.UserName=userName;
			this.PassWord=password;
		}
		#region ��ѯ�����û�GM�ʺŵ���Ϣ
		/// <summary>
		/// ��ѯ�����û�GM�ʺŵ���Ϣ
		/// </summary>
		/// <returns></returns>
		public static DataSet SelectAll()
		{
			string strSQL = "";
			strSQL = "select a.userID,a.userName,a.user_Pwd,a.user_mac,a.realName,a.DepartID,b.departName,a.user_Status,a.limit,a.Onlineactive,a.SysAdmin from GMTOOLS_Users a,Department b where a.departID=b.departID";
			DataSet ds  = null;
			try
			{
				ds = SqlHelper.ExecuteDataset(strSQL);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return ds;
		}
		#endregion
		#region ��ѯ�����û�GM�ʺŵ���Ϣ
		/// <summary>
		/// ��ѯ�����û�GM�ʺŵ���Ϣ
		/// </summary>
		/// <returns></returns>
		public static DataSet SelectAll(int userID)
		{
			DataSet ds  = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{ 
												   new SqlParameter("@Gm_UserID",SqlDbType.Int)
											   };
				paramCode[0].Value =  userID;
				ds = SqlHelper.ExecSPDataSet("Gmtool_UserInfo_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
            return ds;
		}
		#endregion
		#region �������Ͽ��Ժ󣬸��������û�Ϊ�Ͽ�״̬
		/// <summary>
		/// �������Ͽ��Ժ󣬸��������û�Ϊ�Ͽ�״̬
		/// </summary>
		/// <param name="status"></param>
		/// <returns></returns>
		public static int updateActiveUser(int status)
		{
			int result  =-1;
			SqlParameter[] paramCode;
			string updateSql = "Update GMTOOLS_Users set onlineActive="+@status;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@status",SqlDbType.Int)
											   };
				paramCode[0].Value = status;
				result = SqlHelper.commitTrans(updateSql,paramCode);

			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;

		}
		#endregion
		#region �����߻������û�,�趨1��0
		/// <summary>
		/// �����߻������û�,�趨1��0
		/// </summary>
		/// <param name="userByID">���߻������û�</param>
		/// <param name="status">״̬</param>
		/// <returns></returns>
		public static int updateActiveUser(int userByID,int status)
		{
			int result  =-1;
				SqlParameter[] paramCode;
				string updateSql = "Update GMTOOLS_Users set onlineActive="+@status+" where userID='"+@userByID+"'";
				try
				{
					paramCode = new SqlParameter[2]{
													   new SqlParameter("@userByID",SqlDbType.Int),
													   new SqlParameter("@status",SqlDbType.Int)
												   };
					paramCode[0].Value = userByID;
					paramCode[1].Value = status;
					result = SqlHelper.commitTrans(updateSql,paramCode);

				}
				catch(SqlException ex)
				{
					Console.WriteLine(ex.Message);
				}
			return result;

		}
		#endregion
		#region ���û���MAC�浽���ݿ�����
		public static int insertMac(string userName,string userpwd,string mac)
		{
			int result  =-1;
			//�жϸ��û�MAC�Ƿ�Ϊ�գ��յû���д��ȥ
			if (getUserInfo(userName,userpwd)==0)
			{

				SqlParameter[] paramCode;
				string updateSql = "Update GMTOOLS_Users set User_Mac='"+@mac+"' where username='"+@userName+"'and user_pwd='"+convertToMD5(@userpwd)+"'";
				try
				{
					paramCode = new SqlParameter[3]{
													   new SqlParameter("@userName",SqlDbType.VarChar,20),
													   new SqlParameter("@userpwd",SqlDbType.VarChar,100,ParameterDirection.Input.ToString()),
													   new SqlParameter("@mac",SqlDbType.VarChar,20,ParameterDirection.Input.ToString())
												   };
					paramCode[0].Value = userName;
                    paramCode[1].Value = convertToMD5(userpwd);
					paramCode[2].Value = mac;
					result = SqlHelper.commitTrans(updateSql,paramCode);

				}
				catch(SqlException ex)
				{
					Console.WriteLine(ex.Message);
					result = 0;
				}
			}
            return result;

		}
		#endregion
		#region ���һ��GM�ʺ�
		/// <summary>
		/// ����һ���µ�GM�ʺ�
		/// </summary>
		/// <param name="operateUserID">����ԱID</param>
		/// <param name="userName">�û���</param>
		/// <param name="pwd">����</param>
		/// <param name="limit">ʹ��ʱЧ</param>
		/// <param name="status">״̬</param>
		/// <returns>�������</returns>
		public static int insertRow(int operateUserID,int departID,string userName,string pwd,string realName,DateTime limit,int status,int SysAdmin)
		{
			int result = -1;
			//string insertSql = "Insert into GMTOOLS_Users (name,pwd,MAC) values (@name,@pwd,@mac) ";
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[9]{
												new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												new SqlParameter("@Gm_UserName",SqlDbType.VarChar,50),
												new SqlParameter("@Gm_Password",SqlDbType.VarChar,50),
												new SqlParameter("@Gm_DepartID",SqlDbType.Int),
												new SqlParameter("@Gm_RealName",SqlDbType.VarChar,50),
												new SqlParameter("@Gm_LimitTime",SqlDbType.DateTime),
												new SqlParameter("@Gm_Status",SqlDbType.Int),
												new SqlParameter("@Gm_SysAdmin",SqlDbType.Int),
												new SqlParameter("@result",SqlDbType.Int)



											};
				paramCode[0].Value = operateUserID;
				paramCode[1].Value=userName.Trim();
                paramCode[2].Value = convertToMD5(pwd);
				paramCode[3].Value = departID;
				paramCode[4].Value = realName;
				paramCode[5].Value=limit;
				paramCode[6].Value =status;
				paramCode[7].Value = SysAdmin;
				paramCode[8].Direction=ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Gmtool_Gminfo_Add",paramCode);
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
		#region ɾ��һ��GM�ʺ�
		/// <summary>
		/// ɾ��һ��GM�ʺ�
		/// </summary>
		/// <param name="userID">��ɾ�����û�ID</param>
		/// <param name="userByID">ɾ���ò���ԱID</param>
		/// <returns></returns>
		public static int deleteRow(int userID,int operateUserID)
		{
			int result = -1;
			//string deleteSql = "delete from Tools_Users where ID="+@userID;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
											       new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userID;
				paramCode[1].Value=operateUserID;
				paramCode[2].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Gmtool_Gminfo_Del",paramCode);
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
		#region �޸��û���Ϣ
		/// <summary>
		/// ����GM�ʺ���Ϣ
		/// </summary>
		/// <param name="userbyID">����ԱID</param>
		/// <param name="userID">�û�ID</param>
		/// <param name="limitTime">ʹ��ʱЧ</param>
		/// <param name="status">״̬</param>
		/// <returns>�������</returns>
		public static int UpdateRow(int userbyID,int deptID,int userID,string realName,DateTime limitTime,int status,int onlineActive,int SysAdmin)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[9]{ new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@Gm_RealName",SqlDbType.VarChar,100),
												   new SqlParameter("@Gm_DeptID",SqlDbType.Int),
												   new SqlParameter("@Gm_LimitTime",SqlDbType.DateTime),
												   new SqlParameter("@Gm_Status",SqlDbType.Int),
                                                   new SqlParameter("@Gm_OnlineActive",SqlDbType.Int),
												   new SqlParameter("@Gm_SysAdmin",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userbyID;
				paramCode[1].Value=userID;
				paramCode[2].Value = realName;
				paramCode[3].Value = deptID;
				paramCode[4].Value = limitTime;
				paramCode[5].Value = status;
                paramCode[6].Value = onlineActive;
				paramCode[7].Value = SysAdmin;
				paramCode[8].Direction = ParameterDirection.ReturnValue;
                result = SqlHelper.ExecSPCommand("Gmtool_Gminfo_Edit",paramCode);
				if(userbyID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(userbyID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region �޸�����
		/// <summary>
		/// �޸�����
		/// </summary>
		/// <param name="userbyID">����ԱID</param>
		/// <param name="userID">�û�ID</param>
		/// <param name="passwd">����</param>
		/// <returns>�������</returns>
		public static int UpdateRow(int userbyID,int userID,string passwd)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{ new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@Gm_Password",SqlDbType.VarChar,50),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userbyID;
				paramCode[1].Value=userID;
                paramCode[2].Value = convertToMD5(passwd);
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Gmtool_PASSWD_Edit",paramCode);
				if(userbyID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(userbyID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
        #region �����û�ID�õ�һ���û���Ϣ
        /// <summary>
		/// �õ�һ���û���Ϣ
		/// </summary>
		/// <param name="userID">�û�ID</param>
		/// <returns></returns>
		public static DataSet getUserInfo(int userID)
		{
			string strSQL;
			strSQL = "select * from GMTOOLS_Users where User_status =1 and userID="+userID;

			System.Data.DataSet ds = SqlHelper.ExecuteDataset(strSQL);

			return ds;
		}
		#endregion
		#region �����û����������MAC�жϵ�ǰ�û��Ѿ���¼
		/// <summary>
		/// �����û����������MAC�жϵ�ǰ�û��Ѿ���¼
		/// </summary>
		/// <param name="userName">�û���</param>
		/// <param name="passWord">����</param>
		/// <returns>�����Ƿ����</returns>
		public static int getActiveUser(string userName,string passWord,string mac)
		{
			string querySQL = "select onlineActive,sysAdmin from GMTOOLS_Users where user_status=1 and userName='"+userName+"' and user_pwd='"+convertToMD5(passWord)+"' and user_mac='"+mac+"'";
			System.Data.DataSet result = SqlHelper.ExecuteDataset(querySQL);
			if(result.Tables[0].Rows.Count>0)
			{
				return Convert.ToInt32(result.Tables[0].Rows[0].ItemArray[0].ToString());
			}
			else
				return 0;
		}
		#endregion
		#region �����û�����������֤���ݴ���
		/// <summary>
		/// �����û�����������֤���ݴ���
		/// </summary>
		/// <param name="userName">�û���</param>
		/// <param name="passWord">����</param>
		/// <returns>�����Ƿ����</returns>
		public static int getUserInfo(string userName,string passWord)
		{
			string querySQL = "select user_mac from GMTOOLS_Users where user_status=1 and userName='"+userName+"' and user_pwd='"+convertToMD5(passWord)+"'";
			System.Data.DataSet result = SqlHelper.ExecuteDataset(querySQL);
			if(result.Tables[0].Rows.Count>0)
			{
				if(result.Tables[0].Rows[0].ItemArray[0].ToString().Length<=0 || result.Tables[0].Rows[0].IsNull(0)==true)
				{
					return 0;
				}
				else
					return 1;
			}
			else
				return 1;

		}
		#endregion
		#region �û������֤
		/// <summary>
		/// �����û�����������֤���ݴ���
		/// </summary>
		/// <param name="userName">�û���</param>
		/// <param name="passWord">����</param>
		/// <param name="MAC">MAC��</param>
		/// <returns>�����Ƿ����</returns>
		public static int getUserInfo(string userName,string passWord,string MAC)
		{
			string querySQL = "select * from GMTOOLS_Users where user_status = 1 and userName='"+userName+"' and user_pwd='"+convertToMD5(passWord)+"' and user_MAC='"+MAC+"' and datediff(d,limit,getdate())<0";
			System.Data.DataSet result = SqlHelper.ExecuteDataset(querySQL);
			if(result.Tables[0].Rows.Count>0)
			{
				return Convert.ToInt32(result.Tables[0].Rows[0].ItemArray[0].ToString());
			}
			else
				return Convert.ToInt32(result.Tables[0].Rows[0].ItemArray[0].ToString());

		}
		#endregion
		#region �û���
		public string  UserName
		{
			get
			{
				return this.userName;
			}
			set
			{
				this.userName =value;
			}

		}
		#endregion
		#region ����
		public string  PassWord
		{
			get
			{
				return this.passWord;
			}
			set
			{
				this.passWord =value;
			}

		}
		#endregion
		#region MAC
		public string MAC
		{
			get
			{
				return this.mac;
			}
			set
			{
				this.mac =value;
			}
		}
		#endregion
		#region ����
		public string RealName
		{
			get
			{
				return this.realName;
			}
			set
			{
				this.realName =value;
			}
		}

		#endregion
		#region ϵͳ����Ա
		public int SysAdmin
		{
			get
			{
				return this.sysAdmin;
			}
			set
			{
				this.sysAdmin =value;
			}
		}

		#endregion
        #region ���ܴ���
        public static string qswhMD5(string str)
        {
            byte[] b=System.Text.Encoding.Default.GetBytes(str);
            b=new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
            string ret="";
            for(int i=0;i<b.Length;i++)
                ret+=b[i].ToString ("x").PadLeft(2,'0');
            return ret;
        }
        #endregion
        #region ����ת����Կ
        public static string convertToMD5(string source)
        {
            byte[] md5 = System.Text.UTF8Encoding.UTF8.GetBytes(source.Trim());
            MD5CryptoServiceProvider objMD5 = new MD5CryptoServiceProvider();
            byte[] output = objMD5.ComputeHash(md5, 0, md5.Length);
            return BitConverter.ToString(output);  
        }
        #endregion

    }
}
