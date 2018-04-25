using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;

namespace Audition.AUDataInfo
{
	/// <summary>
	/// ItemShopInfo ��ժҪ˵����
	/// </summary>
	public class AUAvatarListInfo
	{
		public AUAvatarListInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

        #region �鿴���������Ʒ����
        /// <summary>
		/// �鿴������ϵ�����Ϣ
		/// </summary>
		/// <param name="serverIP">������IP</param>
		/// <param name="account">����ʺ�</param>
		/// <returns></returns>
        public static DataSet AvatarList_Query(string serverIP, int userIndexID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_UserIndexID",SqlDbType.Int)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=userIndexID;
                result = SqlHelper.ExecSPDataSet("Audition_AvatarList_Query", paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
        #region ���������ҵ���
        /// <summary>
        /// ���������ҵ���
        /// </summary>
        /// <param name="serverIP">������IP</param>
        /// <param name="account">����ʺ�</param>
        /// <returns></returns>
        public static int AvatarList_BatchInsert(int userByID, string serverIP, string itemID,int period, string demo)
        {
            int result = -1;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[7]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@AU_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_SendSN",SqlDbType.Int),
												   new SqlParameter("@AU_ItemID",SqlDbType.VarChar,500),
												   new SqlParameter("@AU_Period",SqlDbType.Int),
												   new SqlParameter("@AU_Demo",SqlDbType.VarChar,500),
												   new SqlParameter("@result",SqlDbType.Int)};
                paramCode[0].Value = userByID;
                paramCode[1].Value = serverIP;
                paramCode[2].Value = 1;
                paramCode[3].Value = itemID;
                paramCode[4].Value = period;
                paramCode[5].Value = demo;
                paramCode[6].Direction = ParameterDirection.ReturnValue;
                result = SqlHelper.ExecSPCommand("Audition_AvatarList_BatchInsert", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion
        #region �����ҵ���
        /// <summary>
        /// �����ҵ���
        /// </summary>
        /// <param name="serverIP">������IP</param>
        /// <param name="account">����ʺ�</param>
        /// <returns></returns>
        public static int AvatarList_Insert(int userByID, string serverIP, int SendSN, string SendNick, int itemID, int RecvSN, string RecvNick, DateTime SendDate, string demo)
        {
            int result = -1;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[10]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@AUserverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_SendSN",SqlDbType.Int),
												   new SqlParameter("@AU_SendNick",SqlDbType.VarChar,40),
												   new SqlParameter("@AU_ItemID",SqlDbType.Int),
												   new SqlParameter("@AU_RecvSN",SqlDbType.Int),
                                                   new SqlParameter("@AU_RecvNick",SqlDbType.VarChar,40),
												   new SqlParameter("@AU_SendDate",SqlDbType.DateTime),
												   new SqlParameter("@AU_Demo",SqlDbType.VarChar,500),
												   new SqlParameter("@result",SqlDbType.Int)};
                paramCode[0].Value = userByID;
                paramCode[1].Value = serverIP;
                paramCode[2].Value = SendSN;
                paramCode[3].Value = SendNick;
                paramCode[4].Value = itemID;
                paramCode[5].Value = RecvSN;
                paramCode[6].Value = RecvNick;
                paramCode[7].Value = SendDate;
                paramCode[8].Value = demo;
                paramCode[9].Direction = ParameterDirection.ReturnValue;
                result = SqlHelper.ExecSPCommand("Audition_AvatarList_Insert", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
         #endregion
		#region ɾ��������ϵ���
		/// <summary>
		/// ɾ�������Ʒ��Ϣ
		/// </summary>
		/// <param name="serverIP">������IP</param>
		/// <param name="account">����ʺ�</param>
		/// <returns></returns>
        public static int AvatarList_Delete(int userByID, string serverIP, int userIndexID, int itemCode)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_UserIndexID",SqlDbType.Int),
												   new SqlParameter("@AU_ItemID",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=userIndexID;
				paramCode[3].Value=itemCode;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
                result = SqlHelper.ExecSPCommand("Audition_AvatarList_del", paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region �鿴��Ϸ��������б�
		/// <summary>
		/// �鿴��Ϸ��������б�
		/// </summary>
		/// <param name="serverIP">������IP</param>
		/// <param name="bigType">���߷���</param>
		/// <param name="smallType">�Ա�</param>
		/// <returns>�������ݼ�</returns>
        public static DataSet AvatarList_QueryAll(string serverIP, string itemName)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30),
											       new SqlParameter("@AU_ItemName",SqlDbType.VarChar,30)};
				paramCode[0].Value=serverIP;
                paramCode[1].Value = itemName;
                result = SqlHelper.ExecSPDataSet("Audition_AvatarItemList_Query", paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region ���������Ѽ�¼
		/// <summary>
		/// ���������Ѽ�¼
		/// </summary>
		/// <param name="serverIP">������IP</param>
		/// <param name="account">����ʺ�</param>
		/// <returns></returns>
		public static DataSet userConsume_Query(string serverIP,string sendUserID,string sendUserNick,DateTime beginDate,DateTime endDate)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_SenderUserID",SqlDbType.VarChar,20),
                                                   new SqlParameter("@AU_SendNick",SqlDbType.VarChar,20),
												   new SqlParameter("@AU_BeginDate",SqlDbType.DateTime),
												   new SqlParameter("@AU_EndDate",SqlDbType.DateTime)};
				paramCode[0].Value=serverIP;
                paramCode[1].Value = sendUserID;
                paramCode[2].Value = sendUserNick;
				paramCode[3].Value = beginDate;
				paramCode[4].Value = endDate;
				result = SqlHelper.ExecSPDataSet("Audition_UserConsume_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
        #region ���������Ѽ�¼�ϼ�
        /// <summary>
        /// ���������Ѽ�¼�ϼ�
        /// </summary>
        /// <param name="serverIP">������IP</param>
        /// <param name="account">����ʺ�</param>
        /// <returns></returns>
        public static DataSet userConsume_QuerySum(string serverIP, string sendUserID,string sendUserNick, DateTime beginDate, DateTime endDate)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_SenderUserID",SqlDbType.VarChar,20),
												   new SqlParameter("@AU_SendNick",SqlDbType.VarChar,20),
												   new SqlParameter("@AU_BeginDate",SqlDbType.DateTime),
												   new SqlParameter("@AU_EndDate",SqlDbType.DateTime)};
				paramCode[0].Value=serverIP;
                paramCode[1].Value = sendUserID;
                paramCode[2].Value = sendUserNick;
				paramCode[3].Value = beginDate;
				paramCode[4].Value = endDate;
                result = SqlHelper.ExecSPDataSet("Audition_UserConsumeSum_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion
		#region �����ҽ��׼�¼
		/// <summary>
		/// �����ҽ��׼�¼
		/// </summary>
		/// <param name="serverIP">������IP</param>
		/// <param name="account">����ʺ�</param>
		/// <returns></returns>
		public static DataSet userTrade_Query(string serverIP,string sendUserID,string recvUserID,string sendUserNick,string recvUserNick,DateTime beginDate,DateTime endDate)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[7]{
												   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_SenderUserID",SqlDbType.VarChar,20),
                                                   new SqlParameter("@AU_ReceiveUserID",SqlDbType.VarChar,20),
                                                   new SqlParameter("@AU_SendNick",SqlDbType.VarChar,30),
                                                   new SqlParameter("@AU_RecvNick",SqlDbType.VarChar,30),
                                                   new SqlParameter("@AU_BeginDate",SqlDbType.VarChar,30),
                                                   new SqlParameter("@AU_EndDate",SqlDbType.VarChar,30),};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=sendUserID;
                paramCode[2].Value = recvUserID;
                paramCode[3].Value = sendUserNick;
                paramCode[4].Value = recvUserNick;
                paramCode[5].Value = beginDate;
                paramCode[6].Value = endDate;
				result = SqlHelper.ExecSPDataSet("Audition_UserTrade_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region �����ҽ��׺ϼƼ�¼
		/// <summary>
		/// �����ҽ��׺ϼƼ�¼
		/// </summary>
		/// <param name="serverIP">������IP</param>
		/// <param name="account">����ʺ�</param>
		/// <returns></returns>
		public static DataSet userTradeSum_Query(string serverIP,string sendUserID,string recvUserID,string sendUserNick,string recvUserNick,DateTime beginDate,DateTime endDate)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[7]{
												   new SqlParameter("@AU_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_SenderUserID",SqlDbType.VarChar,20),
												   new SqlParameter("@AU_ReceiveUserID",SqlDbType.VarChar,20),
												   new SqlParameter("@AU_SendNick",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_RecvNick",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_BeginDate",SqlDbType.VarChar,30),
												   new SqlParameter("@AU_EndDate",SqlDbType.VarChar,30),};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=sendUserID;
				paramCode[2].Value = recvUserID;
				paramCode[3].Value = sendUserNick;
				paramCode[4].Value = recvUserNick;
				paramCode[5].Value = beginDate;
				paramCode[6].Value = endDate;
				result = SqlHelper.ExecSPDataSet("Audition_UserTradeSum_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
	}
}
