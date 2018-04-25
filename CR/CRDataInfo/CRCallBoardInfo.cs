using System;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;

namespace CR.CRDataInfo
{
    class CRCallBoardInfo
    {
        /// <summary>
        /// 查询频道列表
        /// </summary>
        /// <param name="serverIP"></param>
        /// <returns></returns>
        public static DataSet Channel_QueryAll(string serverIP)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[1]{
												 new SqlParameter("@CR_serverip",SqlDbType.VarChar,30)};
                paramCode[0].Value = serverIP;
                result = SqlHelper.ExecSPDataSet("CR_ChannelInfo_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;

        }
        /// <summary>
        /// 查询所有公告的列表
        /// </summary>
        /// <param name="account"></param>
        /// <param name="serverIP"></param>
        /// <returns></returns>
        public static DataSet CallBoardInfo_QueryAll(string serverIP)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[1]{
												 new SqlParameter("@CR_serverip",SqlDbType.VarChar,30)};
                paramCode[0].Value = serverIP;
                result = SqlHelper.ExecSPDataSet("CR_CallBoard_QueryAll", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;

        }
        /// <summary>
        /// 查询公告信息
        /// </summary>
        /// <param name="serverIP"></param>
        /// <param name="InvalidTime"></param>
        /// <param name="PublicID"></param>
        /// <param name="valid"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static DataSet CallBoardInfo_Query(string serverIP, DateTime InvalidTime, int PublicID,int valid,int action)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[5]{
												 new SqlParameter("@CR_serverip",SqlDbType.VarChar,30),
                                                 new SqlParameter("@CR_InvalidTime",SqlDbType.DateTime),
                                                 new SqlParameter("@CR_PublicID",SqlDbType.Int),
                                                 new SqlParameter("@CR_Invalid",SqlDbType.Int),
                                                 new SqlParameter("@CR_Action",SqlDbType.Int)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = InvalidTime;
                paramCode[2].Value = PublicID;
                paramCode[3].Value = valid;
                paramCode[4].Value = action;
                result = SqlHelper.ExecSPDataSet("CR_CallBoard_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;

        }
        /// <summary>
        /// 添加公告
        /// </summary>
        /// <param name="charIndex"></param>
        /// <returns></returns>
        public static string CallBoardInfo_ADD(int GM_UserID,string serverIP,string boardContext,string boardColor,int valid,DateTime efficTime,DateTime disabledTime,int publishID,int speed,int everyDay,int license,string channel)
        {
            int result = -1;
			string err_result = "0";
			string suc_result = "0";
            SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[11]{
													new SqlParameter("@GM_User",SqlDbType.Int),
													new SqlParameter("@CR_ServerIP",SqlDbType.VarChar,30),
													new SqlParameter("@CR_BoardContext",SqlDbType.VarChar,5000),
													new SqlParameter("@CR_BoardColor",SqlDbType.VarChar,6),
													new SqlParameter("@CR_EfficTime",SqlDbType.DateTime),
													new SqlParameter("@CR_DisabledTime",SqlDbType.DateTime),
													new SqlParameter("@CR_Valid",SqlDbType.TinyInt),
													new SqlParameter("@CR_PublishID",SqlDbType.Int),
													new SqlParameter("@CR_Speed",SqlDbType.Int),
													new SqlParameter("@CR_EveryDay",SqlDbType.Int),
													new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = GM_UserID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = boardContext;
				paramCode[3].Value = boardColor;
				paramCode[4].Value = efficTime;
				paramCode[5].Value = disabledTime;
				paramCode[6].Value = valid;
				paramCode[7].Value = publishID;
				paramCode[8].Value = speed;
				paramCode[9].Value = everyDay;
				paramCode[10].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("CR_InsertCallBoardInfo", paramCode);
				if (result == 1)
				{
					paramCode = new SqlParameter[1]{
													   new SqlParameter("@CR_ServerIP",SqlDbType.VarChar,30)};
					paramCode[0].Value = serverIP;

					DataSet ds = SqlHelper.ExecSPDataSet("CR_CallBoard_QueryAll", paramCode);
					int boardID = 0;
					if (ds != null && ds.Tables[0].Rows.Count > 0)
					{
						boardID = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]);
					}
					string[] channels = channel.Split(',');
					for (int i = 0;i < channels.Length-1;i++)
					{
						paramCode = new SqlParameter[5]{
														   new SqlParameter("@GM_User",SqlDbType.Int),
														   new SqlParameter("@CR_ServerIP",SqlDbType.VarChar,30),
														   new SqlParameter("@CR_BoardID",SqlDbType.Int),
														   new SqlParameter("@CR_ChannelID",SqlDbType.VarChar,80),
														   new SqlParameter("@result",SqlDbType.Int)};
						paramCode[0].Value = GM_UserID;
						paramCode[1].Value = serverIP;
						paramCode[2].Value = boardID;
						paramCode[3].Value = channels[i];
						paramCode[4].Direction = ParameterDirection.ReturnValue;
						result = SqlHelper.ExecSPCommand("CR_InsertChannelInfo", paramCode);
						if(result==0)
							err_result = err_result + "," + channels[i];
						else
							suc_result = suc_result + "," + channels[i];

					}
				}
			

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return  err_result;
        }
		/// <summary>
		/// 添加频道
		/// </summary>
		/// <param name="charIndex"></param>
		/// <returns></returns>
		public static string Channels_ADD(int GM_UserID,string serverIP,int boardID,string channel)
		{
			int result = -1;
			string err_result = "0";
			string suc_result = "0";
			SqlParameter[] paramCode;
			try
			{
				string[] channels = channel.Split(',');
				for (int i = 0;i < channels.Length-1;i++)
				{
					paramCode = new SqlParameter[5]{
														new SqlParameter("@GM_User",SqlDbType.Int),
														new SqlParameter("@CR_ServerIP",SqlDbType.VarChar,30),
														new SqlParameter("@CR_BoardID",SqlDbType.Int),
														new SqlParameter("@CR_ChannelID",SqlDbType.VarChar,80),
														new SqlParameter("@result",SqlDbType.Int)};
					paramCode[0].Value = GM_UserID;
					paramCode[1].Value = serverIP;
					paramCode[2].Value = boardID;
					paramCode[3].Value = channels[i];
					paramCode[4].Direction = ParameterDirection.ReturnValue;
					result = SqlHelper.ExecSPCommand("CR_InsertChannelInfo", paramCode);
					if(result==0)
						err_result = err_result + "," + channels[i];
					else
						suc_result = suc_result + "," + channels[i];

				}
			

			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return  err_result;
		}
        /// <summary>
        /// 修改公告
        /// </summary>
        /// <param name="charIndex"></param>
        /// <returns></returns>
        public static int CallBoardInfo_Update(int GM_UserID, string serverIP, int boardID,string boardContext, string boardColor, DateTime validTime, DateTime invalidTime,int everyDay)
        {
            int result = -1;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[9]{
                                                  new SqlParameter("@GM_User",SqlDbType.Int),
                                                  new SqlParameter("@CR_ServerIP",SqlDbType.VarChar,30),
                                                  new SqlParameter("@CR_BoardID",SqlDbType.Int),
                                                  new SqlParameter("@CR_BoardContext",SqlDbType.VarChar,5000),
                                                  new SqlParameter("@CR_BoardColor",SqlDbType.VarChar,6),
												  new SqlParameter("@CR_EfficTime",SqlDbType.DateTime),
                                                  new SqlParameter("@CR_DisabledTime",SqlDbType.DateTime),
                                                  new SqlParameter("@CR_EveryDay",SqlDbType.Int),
                                                  new SqlParameter("@result",SqlDbType.Int)};
                paramCode[0].Value = GM_UserID;
                paramCode[1].Value = serverIP;
                paramCode[2].Value = boardID;
                paramCode[3].Value = boardContext;
                paramCode[4].Value = boardColor;
                paramCode[5].Value = validTime;
                paramCode[6].Value = invalidTime;
                paramCode[7].Value = everyDay;
                paramCode[8].Direction = ParameterDirection.ReturnValue;
                result = SqlHelper.ExecSPCommand("CR_UpdateCallBoardInfo", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="charIndex"></param>
        /// <returns></returns>
        public static int CallBoardInfo_Delete(int GM_UserID, string serverIP,int charIndex)
        {
            int result = -1;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[4]{  new SqlParameter("@GM_User",SqlDbType.Int),
                                                  new SqlParameter("@CR_ServerIP",SqlDbType.VarChar,30),
                                                  new SqlParameter("@CR_CharIndex",SqlDbType.Int),
												  new SqlParameter("@result",SqlDbType.Int)};
                paramCode[0].Value = GM_UserID;
                paramCode[1].Value = serverIP;
                paramCode[2].Value = charIndex;
                paramCode[3].Direction = ParameterDirection.ReturnValue;
                result = SqlHelper.ExecSPCommand("CR_DeleteCallBoardInfo", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
