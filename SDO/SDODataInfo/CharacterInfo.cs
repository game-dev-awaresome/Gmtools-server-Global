using System;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Web.Mail;
using System.Web;
using System.IO;
using System.Xml;
using Common.Logic;
using Common.API;

namespace SDO.SDODataInfo
{
	/// <summary>
	/// CharacterInfo 的摘要说明。
	/// </summary>
	public class CharacterInfo
	{
		public CharacterInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 查看玩家资料
		/// <summary>
		/// 查看玩家资料
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet characterInfo_Query(string serverIP,string account,string userNick)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_NickName",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = account;
				paramCode[2].Value = userNick;
				result = SqlHelper.ExecSPDataSet("sdo_charinfo_query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 修改玩家人物资料
		/// <summary>
		/// 修改玩家资料
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <param name="level">等级</param>
		/// <param name="experience">经验值</param>
		/// <param name="battle">总局数</param>
		/// <param name="win">胜局</param>
		/// <param name="draw">平局</param>
		/// <param name="lose">负局</param>
		/// <param name="MCash">M币</param>
		/// <param name="GCash">G币</param>
		/// <returns></returns>
		public static int characterInfo_Update(int userByID,string serverIP,string account,int level,int experience,int battle,int win,int draw,int lose,int MCash,int GCash)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[12]{
													new SqlParameter("@Gm_UserID",SqlDbType.Int),
													new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
													new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20),
													new SqlParameter("@SDO_Level",SqlDbType.Int),
													new SqlParameter("@SDO_Experience",SqlDbType.Int),
													new SqlParameter("@SDO_Total",SqlDbType.Int),
													new SqlParameter("@SDO_Win",SqlDbType.Int),
													new SqlParameter("@SDO_Draw",SqlDbType.Int),
													new SqlParameter("@SDO_Lose",SqlDbType.Int),
													new SqlParameter("@SDO_MCash",SqlDbType.Int),
													new SqlParameter("@SDO_GCash",SqlDbType.Int),
													new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=account;
				paramCode[3].Value=level;
				paramCode[4].Value=experience;
				paramCode[5].Value=battle;
				paramCode[6].Value=win;
				paramCode[7].Value=draw;
				paramCode[8].Value=lose;
				paramCode[9].Value=MCash;
				paramCode[10].Value=GCash;
				paramCode[11].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("sdo_charinfo_Edit",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;

		}
		#endregion
		#region 查询玩家的邮件
		/// <summary>
		/// 查询玩家的邮件
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static string SDOEmail_Query(string account)
		{
			XmlDocument xmlfile= new XmlDocument();

			/*string email = null;
			DataSet ds = null;
			SqlParameter[] paramCode;
			string sql = "select b.email from user a,userinfo b where a.username='" + account + "' and a.id=b.id";
			try
			{

				paramCode = new SqlParameter[6]{
												   new SqlParameter("@SDO_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_DataBase",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_UserName",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_UserPwd",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_Sql",SqlDbType.VarChar,100),
												   new SqlParameter("@Err",SqlDbType.VarChar,20,ParameterDirection.Output.ToString())};
				paramCode[0].Value = "61.129.66.164";
				paramCode[1].Value = "member";
				paramCode[2].Value = "db_oper";
				paramCode[3].Value = "db_oper@9you";
				paramCode[4].Value = sql;
				paramCode[5].Direction = ParameterDirection.Output;
				ds = SqlHelper.ExecSPDataSet("master..SelectInfo", paramCode);

				if (ds.Tables[0].Rows.Count > 0)
				{
					email = (string)ds.Tables[0].Rows[0].ItemArray[0];
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return email;*/
			string md5string = MD5EncryptAPI.MDString(account+"HAOHAOXUEXI");
			System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from gmtools_serverInfo where gameid=999");
			if(ds!=null && ds.Tables[0].Rows.Count>0)
			{
				string serverIP = ds.Tables[0].Rows[0].ItemArray[0].ToString();
				string url = "http://"+serverIP+"/gmtools?req=queryUser&type=0&userid="+account+"&s="+md5string;
				HttpWebRequest  request  = (HttpWebRequest)
					WebRequest.Create(url);
				//request.ContentType="GB2312";
				request.KeepAlive=false;
				WebResponse resp = request.GetResponse();
				StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
				xmlfile.Load(sr);
				XmlNode email = xmlfile.SelectSingleNode("you9/user/email");
				sr.Close();
				return email.InnerText;
			}
			return null;
		}
		#endregion
		#region 反馈玩家的密码
		/// <summary>
		/// 反馈玩家的密码
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static int sendEmailPasswd(int userByID,string account,string email,string password)
		{
			XmlDocument xmlfile= new XmlDocument();
			string pwd = null;
			int result = -1;
			
			SqlParameter[] paramCode;/*
            try
            {
            	pwd = MD5EncryptAPI.MDString(password);
                paramCode = new SqlParameter[4]{ 
                                                   new SqlParameter("@GM_UserID",SqlDbType.Int),
                                                   new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@9YOU_PWD",SqlDbType.VarChar,32),
											       new SqlParameter("@result",SqlDbType.Int)};
                paramCode[0].Value = userByID;
                paramCode[1].Value = account;
			    paramCode[2].Value = pwd;
				paramCode[3].Direction = ParameterDirection.ReturnValue; 
                result = SqlHelper.ExecSPCommand("SDO_EmailPWD_Update", paramCode);
				if (result==0)
				{
					if(1==sendMail("haifeng_liu@staff.9you.com", email, account+"玩家密码", password))
					  return 1;
				}
				else
					return 0;
            }*/
			try
			{
				pwd = MD5EncryptAPI.nextID(6);
				string md5string = MD5EncryptAPI.MDString(account+"HAOHAOXUEXI");
				string serverIP = "";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from gmtools_serverInfo where gameid=999");
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					serverIP = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					string url = "http://"+serverIP+"/gmtools?req=updatePassword&type=0&userid="+account+"&newPassword="+pwd+"&s="+md5string;
					HttpWebRequest  request  = (HttpWebRequest)
						WebRequest.Create(url);
					//request.Method="Post";
					request.KeepAlive=false;   
					WebResponse resp = request.GetResponse();
					StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
					xmlfile.Load(sr);
					XmlNode state = xmlfile.SelectSingleNode("you9/state");
					sr.Close();
					if(Convert.ToInt32(state.InnerText.ToString())==0)
					{
						//判断发送EMAIL是否成功
						if(1==sendMail("password@staff.9you.com", email, account, pwd))
						{
							result=1;                        
						}
						else
						{
							result =0;
						}
						//记录日志
						paramCode = new SqlParameter[4]{ 
														   new SqlParameter("@GM_UserID",SqlDbType.Int),
														   new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20),
														   new SqlParameter("@9YOU_PWD",SqlDbType.VarChar,32),
														   new SqlParameter("@result",SqlDbType.Int)};
						paramCode[0].Value = userByID;
						paramCode[1].Value = account;
						paramCode[2].Value = pwd;
						paramCode[3].Value = result;
						SqlHelper.ExecSPCommand("SDO_EmailPWD_Update", paramCode);
						return result;
					}
					else
					{
						return 0;
					}
				}
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return 0;
			}
			return 0;
		}
		#endregion
		#region 发送邮件函数
		public static int sendMail(string sender, string receiver, string subject, string body)
		{
			try
			{
				try
				{

					MailMessage Message = new MailMessage();
					Message.To = receiver;
					Message.From = sender;
					string message= "亲爱的"+subject+"<br>";
					message += "&nbsp;&nbsp;&nbsp;&nbsp;欢迎您使用久游网的密码保护功能,您的密码是"+body+"   请牢记您的密码.<br>";
					message += "&nbsp;&nbsp;&nbsp;&nbsp;为了您的密码安全,请定期登录<a href='http://usercenter.9you.com/mmbh/' target='_blank'>久游个人用户中心</a>,修改您的密码.<br><br><br>";
					message += "如果您仍然存在问题,请联系久游客户服务中心<br>";
					message += "传真号码: 021-63601518<br>";
					message +="如果您还有其他问题,可以登陆kefu.9you.com,我们将竭诚为您服务!<br>";
					string headers = "From: 久游网 <password@staff.9you.com>\r\n";
					Message.Priority = MailPriority.High;
					Message.BodyFormat = MailFormat.Html;
					Message.Subject = headers;
					Message.Body = message;
					try
					{
						SmtpMail.SmtpServer = "localhost";
						SmtpMail.Send(Message);
						return 1;

					}
					catch (System.Web.HttpException ehttp)
					{
						Console.WriteLine("{0}", ehttp.Message);
						Console.WriteLine("Here is the full error message output");
						Console.Write("{0}", ehttp.ToString());
						return 0;
					}
				}
				catch (IndexOutOfRangeException)
				{
					return 0;
                   
				}
			}
			catch (System.Exception e)
			{
				Console.WriteLine("Unknown Exception occurred {0}", e.Message);
				Console.WriteLine("Here is the Full Message output");
				Console.WriteLine("{0}", e.ToString());
				return 0;
			}

		}
		#endregion
	}
}
