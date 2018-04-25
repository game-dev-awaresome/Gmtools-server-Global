using System;
using System.Web;
using System.Net;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;
using System.Data.SqlClient;
using Common.Logic;
using Common.API;
namespace UserCharge.UserChargeInfo
{
    class CardDetailInfo
    {
        #region 查看玩家充值记录
        /// <summary>
        /// 查看玩家充值记录
        /// </summary>
        /// <param name="serverIP"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static DataSet CardDetailInfo_Query(string account, string CardID,string CardPwd,int actionType)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[4]{
												   new SqlParameter("@Card_ActionType",SqlDbType.Int),
												   new SqlParameter("@Card_User",SqlDbType.VarChar,50),
                                                   new SqlParameter("@Card_Card",SqlDbType.VarChar,25),
                                                   new SqlParameter("@Card_Pwd",SqlDbType.VarChar,25)};
                paramCode[0].Value = actionType;
                paramCode[1].Value = account;
                paramCode[2].Value = CardID;
                paramCode[3].Value = CardPwd;
                result = SqlHelper.ExecSPDataSet("Card_RecruitInfo_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion
        #region 检测玩家充值记录合计
        /// <summary>
        /// 检测玩家充值记录合计
        /// </summary>
        /// <param name="serverIP">服务器IP</param>
        /// <param name="account">玩家帐号</param>
        /// <returns></returns>
        public static DataSet CardDetailInfo_QuerySum(string account, string CardID, string CardPwd, int actionType)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[4]{
												   new SqlParameter("@Card_ActionType",SqlDbType.Int),
												   new SqlParameter("@Card_User",SqlDbType.VarChar,50),
                                                   new SqlParameter("@Card_Card",SqlDbType.VarChar,25),
                                                   new SqlParameter("@Card_Pwd",SqlDbType.VarChar,25)};
                paramCode[0].Value = actionType;
                paramCode[1].Value = account;
                paramCode[2].Value = CardID;
                paramCode[3].Value = CardPwd;
                result = SqlHelper.ExecSPDataSet("Card_RecruitInfo_Sum_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
         #endregion
        #region 查看玩家消费记录
        /// <summary>
        /// 查看玩家消费记录
        /// </summary>
        /// <param name="serverIP"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static DataSet CardUserConsumeInfo_Query(string account, int actionType)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[2]{
												   new SqlParameter("@Card_ActionType",SqlDbType.Int),
												   new SqlParameter("@Card_User",SqlDbType.VarChar,50)};
                paramCode[0].Value = actionType;
                paramCode[1].Value = account;
                result = SqlHelper.ExecSPDataSet("Card_ConsumeInfo_Query", paramCode);
            }
            catch (SqlException ex)
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
        public static DataSet CardUserConsumeInfo_QuerySum(string account,int actionType)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[2]{
												   new SqlParameter("@Card_ActionType",SqlDbType.Int),
												   new SqlParameter("@Card_User",SqlDbType.VarChar,50)};
                paramCode[0].Value = actionType;
                paramCode[1].Value = account;
                result = SqlHelper.ExecSPDataSet("Card_ConsumeInfo_Sum_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion
        #region 玩家注册用户名查询
        /// <summary>
        /// 玩家注册用户名查询
        /// </summary>
        /// <param name="serverIP">服务器IP</param>
        /// <param name="account">玩家帐号</param>
        /// <returns></returns>
        public static DataSet UserName_Query(string userName,string nickName)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[2]{
												   new SqlParameter("@Card_UserName",SqlDbType.VarChar,50),
												   new SqlParameter("@Card_UserNick",SqlDbType.VarChar,50)};
                paramCode[0].Value = userName;
			    paramCode[1].Value = nickName;
                result = SqlHelper.ExecSPDataSet("Card_UserID_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion
        #region 玩家注册信息查询
        /// <summary>
        /// 玩家注册信息查询
        /// </summary>
        /// <param name="serverIP">服务器IP</param>
        /// <param name="account">玩家帐号</param>
        /// <returns></returns>
        public static DataSet UserRegistInfo_Query(int userID, int actionType)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[2]{
												   new SqlParameter("@ActionType",SqlDbType.Int),
												   new SqlParameter("@Card_UserID",SqlDbType.Int)};
                paramCode[0].Value = actionType;
                paramCode[1].Value = userID;
                result = SqlHelper.ExecSPDataSet("Card_UserInfo_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion
		#region 重置玩家身份证信息
		/// <summary>
		/// 重置玩家身份证信息
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int UserRegistInfo_Clear(int userbyID, string userID)
		{
			try
			{
				XmlDocument xmlfile = new XmlDocument();
				string md5string = MD5EncryptAPI.MDString(userID+"HAOHAOXUEXI");
				string serverIP = null;
				System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from gmtools_serverInfo where gameid=999");
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					serverIP = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					string url = "http://"+serverIP+"/gmtools?req=resetIDCard&type=0&userid="+userID+"&s="+md5string;
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
		#endregion
		#region 重置玩家安全码信息
		/// <summary>
		/// 重置玩家安全码信息
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int UserSecureCodeInfo_Clear(int userByID,string userID)
		{
			try
			{
				XmlDocument xmlfile = new XmlDocument();
				string md5string = MD5EncryptAPI.MDString(userID+"HAOHAOXUEXI");
				string serverIP = null;
				System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from gmtools_serverInfo where gameid=999");
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					serverIP = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					string url = "http://"+serverIP+"/gmtools?req=resetSecureCode&type=0&userid="+userID+"&s="+md5string;
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
		#endregion
		#region 锁定久游用户
		/// <summary>
		/// 重置玩家安全码信息
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int MemberInfo_Lock(int userByID,string userID,long lockTime)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@Card_UserID",SqlDbType.VarChar,30),
												   new SqlParameter("@Card_LockTime",SqlDbType.BigInt),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = userID;
				paramCode[2].Value = lockTime;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Card_MemberInfo_Lock", paramCode);
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return 0;
			}
			return result;
		}
		#endregion
    }
}
