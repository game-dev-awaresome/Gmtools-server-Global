using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using STONE.HU.HELPER.UTIL;
namespace Common.Logic
{

	/**//// <summary>
	/// Summary description for SqlSvrHelper.
	/// </summary>
	public class SqlHelper
	{
		private string url = null;
		private string userID = null;
		private string pwd = null;
		public static Log log = null;
		public static string ConnectionString;
		public SqlHelper()
		{

		}
		public void init(string url,string dbName,string userID,string pwd)
		{
			string path= AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
			log = new Log(path+"\\log.txt", true, false, 0);	
			ConnectionString = "Data Source="+url+";Network Library=DBMSSOCN;Initial Catalog="+dbName+";User ID="+userID+";Password="+pwd+";Pooling=true;Min Pool Size=0;Max Pool Size=10;";
		}
		#region GetDataSet
		/// <summary>
		///  得到一个查询的数据集
		/// </summary>
		/// <param name="commandText">输入查询语句</param>
		/// <returns>查询的数据集</returns>
		public static DataSet ExecuteDataset(string commandText)
		{
			DataSet ds = null;
			try
			{
				if( ConnectionString == null || ConnectionString.Length == 0 ) throw new ArgumentNullException( "ConnectionString" );

				using (SqlConnection connection = new SqlConnection(ConnectionString))
				{
					connection.Open();
					// Create the DataAdapter & DataSet
					SqlCommand cmd = new SqlCommand(commandText,connection);
					cmd.CommandType = CommandType.Text;
					using( SqlDataAdapter da = new SqlDataAdapter(cmd) )
					{
						ds = new DataSet();
						da.Fill(ds);
						connection.Close();


					}

				}  
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return ds;
		}
		
		#endregion
 		#region ExecCommand
		/// <summary>
		/// 返回查询满足条件数据的结果
		/// </summary>
		/// <param name="sqlcom">输入查询语句</param>
		/// <returns>查询结果</returns>
		public static int ExecCommand(SqlCommand sqlcom)
		{
			SqlTransaction myTrans =null;
			SqlConnection conn=new SqlConnection(ConnectionString);
			sqlcom.Connection =conn;
			conn.Open();
			try
			{
				myTrans = conn.BeginTransaction();
				sqlcom.Transaction = myTrans;
				int rtn=sqlcom.ExecuteNonQuery();
				myTrans.Commit();
				return rtn;
			}
			catch(Exception ex) 
			{
				Console.WriteLine(ex.Message);
				myTrans.Rollback();
				return -1;
			}
			finally
			{
				conn.Close();

			}


		}
		public static int ExecCommand(string sql)
		{
			if (sql.EndsWith(",")) sql=sql.Substring(0,sql.Length-1);
        
			SqlCommand sqlcom=new SqlCommand(sql);
			return ExecCommand(sqlcom);                
		}
		#endregion       
		#region ExecuteScalar
		/// <summary>
		/// 查询满足条件的数据集的对象
		/// </summary>
		/// <param name="sql">输入查询语句</param>
		/// <returns>返回数据集的对象</returns>
		public static object ExecuteScalar(string sql)
		{
			SqlConnection conn=new SqlConnection(ConnectionString);
			SqlCommand sqlcom=new SqlCommand(sql,conn);
			conn.Open();
			try
			{
				object rtn=sqlcom.ExecuteScalar ();
				return rtn;
			}
			catch(Exception ex) 
			{
				throw ex;                
			}
			finally
			{
				conn.Close();
			}
		}
		#endregion
		#region ExecSPCommand
		/// <summary>
		/// 执行存储过程
		/// </summary>
		/// <param name="strProc">存储过程名</param>
		/// <param name="param">输入存储过程的参数集</param>
		/// <param name="count">参数个数</param>
		/// <returns>执行结果</returns>
		public static int ExecSPCommand(string strProc,ArrayList param,int count)
		{
			int returnValue =0;
			SqlConnection conn=new SqlConnection(ConnectionString);
			SqlCommand sqlcom=new SqlCommand(strProc,conn);
			sqlcom.CommandType= CommandType.StoredProcedure ;
			try
			{
				conn.Open();
				if(param!=null && count!=0)
				{
					foreach(System.Data.IDataParameter paramer in param)
					{
						sqlcom.Parameters.Add(paramer);
					}  
				}

				sqlcom.ExecuteNonQuery();
				if(count!=0)
				{

					if(!Convert.IsDBNull(sqlcom.Parameters["@result"].Value))
					{
						returnValue=Convert.ToInt32(sqlcom.Parameters["@result"].Value);
					}
				}
				sqlcom.Parameters.Clear();
				conn.Close();
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				conn.Close();
			}
			return returnValue;
			//string sourceIP=(string)param[1];


		}
		#endregion
		#region ExecSPCommand
		/// <summary>
		/// 调用存储过程
		/// </summary>
		/// <param name="strProc">存储过程名</param>
		/// <param name="paramers">输入参数集</param>
		public static int ExecSPCommand(string strProc,System.Data.IDataParameter[] paramers)
		{
			int returnValue = 0;
			SqlConnection conn=new SqlConnection(ConnectionString);
			SqlCommand sqlcom=new SqlCommand(strProc,conn);
			sqlcom.CommandType= CommandType.StoredProcedure ;

			foreach(System.Data.IDataParameter paramer in paramers)
			{
				sqlcom.Parameters.Add(paramer);
			}            
			conn.Open();
			try
			{
				sqlcom.ExecuteNonQuery();
				if(!Convert.IsDBNull(sqlcom.Parameters["@result"].Value))
				{
					returnValue=Convert.ToInt32(sqlcom.Parameters["@result"].Value);
				}
			}
			catch(Exception ex) 
			{
				string s=ex.Message ;
			}
			finally
			{
				conn.Close();
			}
			return returnValue;
		}
		#endregion
		#region ExecSPDataSet
		/// <summary>
		/// 得到存储过程返回数据集
		/// </summary>
		/// <param name="strProc">存储过程名</param>
		/// <param name="paramers">输入参数集</param>
		/// <returns>返回数据集</returns>
		public static DataSet ExecSPDataSet(string strProc,System.Data.IDataParameter[] paramers)
		{
			SqlConnection conn=new SqlConnection(ConnectionString);
			SqlCommand sqlcom=new SqlCommand(strProc,conn);
			sqlcom.CommandType= CommandType.StoredProcedure ;

			foreach(System.Data.IDataParameter paramer in paramers)
			{
				sqlcom.Parameters.Add(paramer);
			}            
			conn.Open();
            
			SqlDataAdapter da=new SqlDataAdapter();
			da.SelectCommand=sqlcom;
			DataSet ds=new DataSet();
			da.Fill(ds);
        
			conn.Close();
			return ds;
		}

		#endregion ExecSPDataSet
		#region ExecSPDataSet
		/// <summary>
		/// 得到存储过程返回数据集
		/// </summary>
		/// <param name="strProc">存储过程名</param>
		/// <param name="paramers">输入参数集</param>
		/// <returns>返回数据集</returns>
		public static DataSet ExecSPDataSet(string strProc)
		{
			SqlConnection conn=new SqlConnection(ConnectionString);
			SqlCommand sqlcom=new SqlCommand(strProc,conn);
			sqlcom.CommandType= CommandType.StoredProcedure ;
           
			conn.Open();
            
			SqlDataAdapter da=new SqlDataAdapter();
			da.SelectCommand=sqlcom;
			DataSet ds=new DataSet();
			da.Fill(ds);
        
			conn.Close();
			return ds;
		}

		#endregion DbType
		#region DbType
		private static System.Data.DbType GetDbType(Type type)
		{
			DbType result = DbType.String;
			if( type.Equals(typeof(int)) ||  type.IsEnum)
				result = DbType.Int32;
			else if( type.Equals(typeof(long)))
				result = DbType.Int32;
			else if( type.Equals(typeof(double)) || type.Equals( typeof(Double)))
				result = DbType.Decimal;
			else if( type.Equals(typeof(DateTime)))
				result = DbType.DateTime;
			else if( type.Equals(typeof(bool)))
				result = DbType.Boolean;
			else if( type.Equals(typeof(string) ) )
				result = DbType.String;
			else if( type.Equals(typeof(decimal)))
				result = DbType.Decimal;
			else if( type.Equals(typeof(byte[])))
				result = DbType.Binary;
			else if( type.Equals(typeof(Guid)))
				result = DbType.Guid;
        
			return result;
            
		}

		#endregion UpdateTable
		#region UpdateTable
		public static void UpdateTable(DataTable dt,string TableName,string KeyName)
		{
			foreach(DataRow dr in dt.Rows)
			{
				updateRow(dr,TableName,KeyName);
			}
		}
		#endregion InsertTable
		#region InsertTable
		//用于主键是数据库表名+ID类型的
		public static void InsertTable(DataTable dt)
		{
			string TableName="["+dt.TableName+"]";
			string KeyName=dt.TableName+"ID";
			foreach(DataRow dr in dt.Rows)
			{
				insertRow(dr,TableName,KeyName);
			}
		}
		//用于主键是任意类型的
		public static void InsertTable(DataTable dt,string KeyName)
		{
			string TableName="["+dt.TableName+"]";
			foreach(DataRow dr in dt.Rows)
			{
				insertRow(dr,TableName,KeyName);
			}
		}
		#endregion updateRow
		#region updateRow
		private static void  updateRow(DataRow dr,string TableName,string KeyName)
		{
			if (dr[KeyName]==DBNull.Value ) 
			{
				throw new Exception(KeyName +"的值不能为空");
			}
            
			if (dr.RowState ==DataRowState.Deleted)
			{
				deleteRow(dr,TableName,KeyName);
 
			}
			else if (dr.RowState ==DataRowState.Modified )
			{
				midifyRow(dr,TableName,KeyName);
			}
			else if (dr.RowState ==DataRowState.Added  )
			{
				insertRow(dr,TableName,KeyName);
			}
			else if (dr.RowState ==DataRowState.Unchanged )
			{
				midifyRow(dr,TableName,KeyName);
			}           
		}

		#endregion deleteRow
		#region deleteRow
		private static void  deleteRow(DataRow dr,string TableName,string KeyName)
		{
			string sql="Delete {0} where {1} =@{1}";
			DataTable dtb=dr.Table ;
			sql=string.Format(sql,TableName,KeyName);

			SqlCommand sqlcom=new SqlCommand(sql);
			System.Data.IDataParameter iparam=new  SqlParameter();
			iparam.ParameterName    = "@"+ KeyName;
			iparam.DbType            = GetDbType(dtb.Columns[KeyName].DataType);
			iparam.Value            = dr[KeyName];
			sqlcom.Parameters .Add(iparam);
            
			ExecCommand(sqlcom);
		}
		#endregion midifyRow
		#region midifyRow
		private static void  midifyRow(DataRow dr,string TableName,string KeyName)
		{
			string UpdateSql            = "Update {0} set {1} {2}";
			string setSql="{0}= @{0}";
			string wherSql=" Where {0}=@{0}";
			StringBuilder setSb    = new StringBuilder();

			SqlCommand sqlcom=new SqlCommand();
			DataTable dtb=dr.Table;
        
			for (int k=0; k<dr.Table.Columns.Count; ++k)
			{
				System.Data.IDataParameter iparam=new  SqlParameter();
				iparam.ParameterName    = "@"+ dtb.Columns[k].ColumnName;
				iparam.DbType            = GetDbType(dtb.Columns[k].DataType);
				iparam.Value            = dr[k];
				sqlcom.Parameters .Add(iparam);

				if (dtb.Columns[k].ColumnName==KeyName)
				{
					wherSql=string.Format(wherSql,KeyName);
				}
				else
				{
					setSb.Append(string.Format(setSql,dtb.Columns[k].ColumnName));    
					setSb.Append(",");
				}
                
			}
            
			string setStr=setSb.ToString();
			setStr=setStr.Substring(0,setStr.Length -1); //trim ,
            
			string sql = string.Format(UpdateSql, TableName, setStr,wherSql);
			sqlcom.CommandText =sql;    
			try
			{
				ExecCommand(sqlcom);
			}
			catch(Exception ex)
			{
				throw ex;            
			}
		}
		#endregion insertRow
		#region insertRow
		private static void  insertRow(DataRow dr,string TableName,string KeyName)
		{
			string InsertSql = "Insert into {0}({1}) values({2})";
			SqlCommand sqlcom=new SqlCommand();
			DataTable dtb=dr.Table ;
			StringBuilder insertValues    = new StringBuilder();
			StringBuilder cloumn_list    = new StringBuilder();
			for (int k=0; k<dr.Table.Columns.Count; ++k)
			{
				//just for genentae，
				if (dtb.Columns[k].ColumnName==KeyName) continue;
				System.Data.IDataParameter iparam=new  SqlParameter();
				iparam.ParameterName    = "@"+ dtb.Columns[k].ColumnName;
				iparam.DbType            = GetDbType(dtb.Columns[k].DataType);
				iparam.Value            = dr[k];
				sqlcom.Parameters .Add(iparam);

				cloumn_list.Append(dtb.Columns[k].ColumnName);
				insertValues.Append("@"+dtb.Columns[k].ColumnName);

				cloumn_list.Append(",");
				insertValues.Append(",");
			}
            
			string cols=cloumn_list.ToString();
			cols=cols.Substring(0,cols.Length -1);

			string values=insertValues.ToString();
			values=values.Substring(0,values.Length -1);
            
			string sql = string.Format(InsertSql, TableName,cols ,values);
			sqlcom.CommandText =sql;    
			try
			{
				ExecCommand(sqlcom);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		#endregion
		#region commitTrans
		public static int commitTrans(string strSQL,System.Data.IDataParameter[] paramers)
		{
			SqlConnection myConnection = new SqlConnection(SqlHelper.ConnectionString);
			myConnection.Open();
			// Start a local transaction.
			SqlTransaction myTrans = myConnection.BeginTransaction();

			// Enlist the command in the current transaction.
			SqlCommand myCommand = myConnection.CreateCommand();
			myCommand.Transaction = myTrans;
			myCommand.CommandType = CommandType.Text;

			foreach(System.Data.IDataParameter paramer in paramers)
			{
				myCommand.Parameters.Add(paramer);
			}   
			
			try
			{
				myCommand.CommandText = strSQL;
				myCommand.ExecuteNonQuery();
				myTrans.Commit();
				return 1;
			}
			catch(Exception e)
			{
				try
				{
					myTrans.Rollback();
					return 0;
				}
				catch (SqlException ex)
				{
					if (myTrans.Connection != null)
					{
						Console.WriteLine("An exception of type " + ex.GetType() +
							" was encountered while attempting to roll back the transaction.");
					}
				}

				Console.WriteLine("An exception of type " + e.GetType() +
					"was encountered while inserting the data.");
				Console.WriteLine("Neither record was written to database.");
			}
			finally
			{
				myConnection.Close();
			}
			return 1;

		}
		#endregion
		#region commitTrans
		public static void commitTrans(string strSQL)
		{
			SqlConnection myConnection = new SqlConnection(SqlHelper.ConnectionString);
			myConnection.Open();
			// Start a local transaction.
			SqlTransaction myTrans = myConnection.BeginTransaction();

			// Enlist the command in the current transaction.
			SqlCommand myCommand = myConnection.CreateCommand();
			myCommand.Transaction = myTrans;
			myCommand.CommandType = CommandType.Text;

			try
			{
				myCommand.CommandText = strSQL;
				myCommand.ExecuteNonQuery();
				myTrans.Commit();
				Console.WriteLine("Both records are written to database.");
			}
			catch(Exception e)
			{
				try
				{
					myTrans.Rollback();
				}
				catch (SqlException ex)
				{
					if (myTrans.Connection != null)
					{
						Console.WriteLine("An exception of type " + ex.GetType() +
							" was encountered while attempting to roll back the transaction.");
					}
				}

				Console.WriteLine("An exception of type " + e.GetType() +
					"was encountered while inserting the data.");
				Console.WriteLine("Neither record was written to database.");
			}
			finally
			{
				myConnection.Close();
			}


		}
		#endregion
		#region transcation
		public static void transcation(string procName)
		{
			SqlConnection myConnection = new SqlConnection("Data Source=192.168.9.124;Initial Catalog=Northwind;Integrated Security=SSPI;");
			myConnection.Open();

			// Start a local transaction.
			SqlTransaction myTrans = myConnection.BeginTransaction();

			// Enlist the command in the current transaction.
			SqlCommand myCommand = myConnection.CreateCommand();
			myCommand.Transaction = myTrans;
			myCommand.CommandType = CommandType.StoredProcedure;

			try
			{
				myCommand.CommandText = procName;
				myCommand.ExecuteNonQuery();
				myTrans.Commit();
				Console.WriteLine("Both records are written to database.");
			}
			catch(Exception e)
			{
				try
				{
					myTrans.Rollback();
				}
				catch (SqlException ex)
				{
					if (myTrans.Connection != null)
					{
						Console.WriteLine("An exception of type " + ex.GetType() +
							" was encountered while attempting to roll back the transaction.");
					}
				}

				Console.WriteLine("An exception of type " + e.GetType() +
					"was encountered while inserting the data.");
				Console.WriteLine("Neither record was written to database.");
			}
			finally
			{
				myConnection.Close();
			}

		}

        #endregion
		#region URL
		public  string URL
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}
		#endregion
		#region USERID
		public  string UserID
		{
			get
			{
				return this.userID;
			}
			set
			{
				this.userID = value;
			}
		}
		#endregion
		#region PWD
		public  string Pwd
		{
			get
			{
				return this.pwd;
			}
			set
			{
				this.pwd = value;
			}
		}
		#endregion
	}
}

