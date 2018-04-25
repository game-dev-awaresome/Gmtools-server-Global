using System;
using System.Web;
using System.Net;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;
using System.Web.Mail;
using System.Collections;
using System.Data.SqlClient;
using Common.Logic;
using Common.API;

namespace UserCharge.UserChargeInfo
{
	/// <summary>
	/// UserauthenticInfo 的摘要说明。
	/// </summary>
	public class UserauthenticInfo
	{
		/// <summary>
		/// 显示令牌的当前状态
		/// </summary>
		/// <param name="provide">服务提供商</param>
		/// <param name="service">请求服务名称</param>
		/// <param name="esn">令牌号码</param>
		/// <returns>XML数据集</returns>
		public static ArrayList Token_Status(string provide,string service,string esn)
		{
			string certificate =  MD5EncryptAPI.MDString(service+"e10adc3949");
			string Xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
				"<you9>" +
				"<request>" +
				"<provide>"+provide+"</provide>" +
				"<service>"+service+"</service>" +
				"<certificate>"+certificate+"</certificate>" +	
				"<parameter>" +
				"<key>esn</key>" +
				"<value>"+esn+"</value>" +
				"</parameter>" +
				"</request>" +
				"</you9>" ;
			string url = "http://61.152.148.130:8080/authentic/authServlet";
			try
			{
				HttpWebRequest  request  = (HttpWebRequest)WebRequest.Create(url);
				request.ContentType="text/xml";
				request.KeepAlive=false; 
				request.Method="POST";
				ASCIIEncoding encodedData=new ASCIIEncoding();
				byte[]  byteArray=encodedData.GetBytes(Xml);
				request.ContentLength=byteArray.Length;
				Stream newStream=request.GetRequestStream();
				newStream.Write(byteArray,0,byteArray.Length);
				newStream.Close();
				WebResponse
					resp = request.GetResponse();
				StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
				XmlDocument xmlfile = new XmlDocument();
				xmlfile.Load(sr);
				XmlNode nodes=xmlfile.SelectSingleNode("you9/response");
				System.Collections.ArrayList rowList = new System.Collections.ArrayList();
				foreach(XmlNode xmlnodes in nodes.ChildNodes)
				{
				
					System.Collections.ArrayList colList = new System.Collections.ArrayList();
					foreach(XmlNode chilenodes in xmlnodes.ChildNodes)
					{
						colList.Add(chilenodes.InnerText);
					}
					rowList.Add(colList);				
				}
				sr.Close();
				return rowList;
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return null;
		}

		/// <summary>
		/// 修改用户令牌，锁定-解锁-停用
		/// </summary>
		/// <param name="provide">服务提供商</param>
		/// <param name="service">请求服务名称</param>
		/// <param name="userName">用户名</param>
		/// <param name="esn">令牌号码</param>
		/// <returns>XML数据集</returns>
		public static ArrayList Modify_User_Token(string provide,string service,string userName,string esn)
		{
			string certificate =  MD5EncryptAPI.MDString(service+"e10adc3949");
			string Xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
				"<you9>" +
				"<request>" +
				"<provide>"+provide+"</provide>" +
				"<service>"+service+"</service>" +
				"<certificate>"+certificate+"</certificate>" +	
				"<parameter>" + 
				"<key>userName</key>" + 
				"<value>" + userName + "</value>" + 
				"</parameter>" + 
				"<parameter>" +
				"<key>esn</key>" +
				"<value>"+esn+"</value>" +
				"</parameter>" +
				"</request>" +
				"</you9>" ;
			string url = "http://61.152.148.130:8080/authentic/authServlet";
			try
			{
				HttpWebRequest  request  = (HttpWebRequest)WebRequest.Create(url);
				request.ContentType="text/xml";
				request.KeepAlive=false; 
				request.Method="POST";
				ASCIIEncoding encodedData=new ASCIIEncoding();
				byte[]  byteArray=encodedData.GetBytes(Xml);
				request.ContentLength=byteArray.Length;
				Stream newStream=request.GetRequestStream();
				newStream.Write(byteArray,0,byteArray.Length);
				newStream.Close();
				WebResponse
					resp = request.GetResponse();
				StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
				XmlDocument xmlfile = new XmlDocument();
				xmlfile.Load(sr);
				XmlNode nodes=xmlfile.SelectSingleNode("you9/response");
				System.Collections.ArrayList rowList = new System.Collections.ArrayList();
				foreach(XmlNode xmlnodes in nodes.ChildNodes)
				{
				
					System.Collections.ArrayList colList = new System.Collections.ArrayList();
					foreach(XmlNode chilenodes in xmlnodes.ChildNodes)
					{
						colList.Add(chilenodes.InnerText);
					}
					rowList.Add(colList);				
				}
				sr.Close();
				return rowList;
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return null;
		}
		#region 查看用户密码历史记录
		/// <summary>
		/// 查看用户密码历史记录
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static ArrayList User_Token_Query(string type,string strValue,string beginDate,string endDate)
		{
			XmlDocument xmlfile = new XmlDocument();
			string md5string = MD5EncryptAPI.MDString(strValue+"QUXUEXIBA");
			try
			{
				System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from gmtools_serverInfo where gameid=999");
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					string serverIP = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					string url = "http://"+serverIP+"/gmtools?req=viewToken&type="+type+"&start="+beginDate+"&end="+endDate+"&v="+strValue+"&s="+md5string+"";
					HttpWebRequest  request  = (HttpWebRequest)
						WebRequest.Create(url);
					request.KeepAlive=false;   
					WebResponse resp = request.GetResponse();
					StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
					xmlfile.Load(sr);
					//int i=1;
					XmlNodeList nodeList =xmlfile.SelectNodes("you9/password/history9you");
					System.Collections.ArrayList rowList = new System.Collections.ArrayList();
					foreach(XmlElement tmp1 in nodeList)
					{
						System.Collections.ArrayList colList = new System.Collections.ArrayList();
						foreach(XmlElement tmp2 in tmp1.ChildNodes)
						{
							colList.Add(tmp2.InnerText);
						}
						rowList.Add(colList);
					}
					sr.Close();
					return rowList;

				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return null;
		}
		#endregion
		public static int User_Token_Open(int userByID,string userID)
		{
			try
			{
				XmlDocument xmlfile = new XmlDocument();
				string md5string = MD5EncryptAPI.MDString(userID+"QUXUEXIBA");
				string serverIP = null;
				System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from gmtools_serverInfo where gameid=999");
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					serverIP = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					string url = "http://"+serverIP+"/gmtools?req=doEsn&type=authUnlock&userid="+userID+"&s="+md5string;
					HttpWebRequest  request  = (HttpWebRequest)
						WebRequest.Create(url);
					request.KeepAlive=false;   
					WebResponse resp = request.GetResponse();
					StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
					xmlfile.Load(sr);
					XmlNode state = xmlfile.SelectSingleNode("you9/state");
					sr.Close();
					if(Convert.ToInt32(state.InnerText.ToString())==0)
					{
						//记录日志
						SqlParameter[] paramCode = new SqlParameter[3]{ 
																		  new SqlParameter("@GM_UserID",SqlDbType.Int),
																		  new SqlParameter("@Card_UserID",SqlDbType.VarChar,20),
																		  new SqlParameter("@result",SqlDbType.Int)};
						paramCode[0].Value = userByID;
						paramCode[1].Value = userID;
						paramCode[2].Value = Convert.ToInt32(state.InnerText.ToString());
						SqlHelper.ExecSPCommand("Card_SecureCodeInfo_Clear", paramCode);
						return 1;                    
					}
				}
				return 0;
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return 0;
			}
            
		}
		public static int User_Token_Close(int userByID,string userID)
		{
			try
			{
				XmlDocument xmlfile = new XmlDocument();
				string md5string = MD5EncryptAPI.MDString(userID+"QUXUEXIBA");
				string serverIP = null;
				System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from gmtools_serverInfo where gameid=999");
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					serverIP = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					string url = "http://"+serverIP+"/gmtools?req=doEsn&type=authLock&userid="+userID+"&s="+md5string;
					HttpWebRequest  request  = (HttpWebRequest)
						WebRequest.Create(url);
					request.KeepAlive=false;   
					WebResponse resp = request.GetResponse();
					StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
					xmlfile.Load(sr);
					XmlNode state = xmlfile.SelectSingleNode("you9/state");
					sr.Close();
					if(Convert.ToInt32(state.InnerText.ToString())==0)
					{
						//记录日志
						SqlParameter[] paramCode = new SqlParameter[3]{ 
																		  new SqlParameter("@GM_UserID",SqlDbType.Int),
																		  new SqlParameter("@Card_UserID",SqlDbType.VarChar,20),
																		  new SqlParameter("@result",SqlDbType.Int)};
						paramCode[0].Value = userByID;
						paramCode[1].Value = userID;
						paramCode[2].Value = Convert.ToInt32(state.InnerText.ToString());
						SqlHelper.ExecSPCommand("Card_SecureCodeInfo_Clear", paramCode);
						return 1;                    
					}
				}
				return 0;
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return 0;
			}
            
		}

	}
}
