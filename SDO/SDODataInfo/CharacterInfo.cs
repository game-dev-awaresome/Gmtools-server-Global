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
	/// CharacterInfo ��ժҪ˵����
	/// </summary>
	public class CharacterInfo
	{
		public CharacterInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region �鿴�������
		/// <summary>
		/// �鿴�������
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
		#region �޸������������
		/// <summary>
		/// �޸��������
		/// </summary>
		/// <param name="userByID">����ԱID</param>
		/// <param name="serverIP">������IP</param>
		/// <param name="account">�ʺ�</param>
		/// <param name="level">�ȼ�</param>
		/// <param name="experience">����ֵ</param>
		/// <param name="battle">�ܾ���</param>
		/// <param name="win">ʤ��</param>
		/// <param name="draw">ƽ��</param>
		/// <param name="lose">����</param>
		/// <param name="MCash">M��</param>
		/// <param name="GCash">G��</param>
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
		#region ��ѯ��ҵ��ʼ�
		/// <summary>
		/// ��ѯ��ҵ��ʼ�
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
		#region ������ҵ�����
		/// <summary>
		/// ������ҵ�����
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
					if(1==sendMail("haifeng_liu@staff.9you.com", email, account+"�������", password))
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
						//�жϷ���EMAIL�Ƿ�ɹ�
						if(1==sendMail("password@staff.9you.com", email, account, pwd))
						{
							result=1;                        
						}
						else
						{
							result =0;
						}
						//��¼��־
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
		#region �����ʼ�����
		public static int sendMail(string sender, string receiver, string subject, string body)
		{
			try
			{
				try
				{

					MailMessage Message = new MailMessage();
					Message.To = receiver;
					Message.From = sender;
					string message= "�װ���"+subject+"<br>";
					message += "&nbsp;&nbsp;&nbsp;&nbsp;��ӭ��ʹ�þ����������뱣������,����������"+body+"   ���μ���������.<br>";
					message += "&nbsp;&nbsp;&nbsp;&nbsp;Ϊ���������밲ȫ,�붨�ڵ�¼<a href='http://usercenter.9you.com/mmbh/' target='_blank'>���θ����û�����</a>,�޸���������.<br><br><br>";
					message += "�������Ȼ��������,����ϵ���οͻ���������<br>";
					message += "�������: 021-63601518<br>";
					message +="�����������������,���Ե�½kefu.9you.com,���ǽ��߳�Ϊ������!<br>";
					string headers = "From: ������ <password@staff.9you.com>\r\n";
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
