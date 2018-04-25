/*
 * Add by KeHuaQing 
 * 2006-09-14
 */
using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;

namespace Soccer.SOCCERDataInfo
{
	/// <summary>
	/// SOCCERCharacterInfo ��ժҪ˵����
	/// </summary>
	class SOCCERCharacterInfo
	{
		#region ��ѯ�û���Ϣ
		/// <summary>
		/// ��ѯ�û���Ϣ
		/// </summary>
		/// <param name="serverip">��Ϸ������ip</param>
		/// <param name="type">��ѯ���ͣ�cID��cName,cIdx,ȫѡ</param>
		/// <param name="content">��������</param>
		/// <returns>���صĽ����</returns>
		public static DataSet Soccer_Characterinfo_Query (string serverip,string type,string content)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@Soccer_serverIP",SqlDbType.VarChar,30),
												   new SqlParameter("@srh_type",SqlDbType.VarChar,10),
												   new SqlParameter("@srh_string",SqlDbType.VarChar,80)};
				paramCode[0].Value = serverip;
				paramCode[1].Value = type;
				paramCode[2].Value = content;
				result = SqlHelper.ExecSPDataSet("SOCCER_CHARACTERINFO_QUERY", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion

		#region ɾ���û�״̬��ѯ
		/// <summary>
		/// ��ѯ�û���Ϣ
		/// </summary>
		/// <param name="serverip">��Ϸ������ip</param>
		/// <param name="type">��ѯ���ͣ�cID��cName,cIdx,ȫѡ</param>
		/// <param name="content">��������</param>
		/// <returns>���صĽ����</returns>
		public static DataSet Soccer_DeletedCharacterinfo_Query (string serverip,string type,string content)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@Soccer_serverIP",SqlDbType.VarChar,30),
												   new SqlParameter("@srh_type",SqlDbType.VarChar,10),
												   new SqlParameter("@srh_string",SqlDbType.VarChar,80)};
				paramCode[0].Value = serverip;
				paramCode[1].Value = type;
				paramCode[2].Value = content;
				result = SqlHelper.ExecSPDataSet("SOCCER_DELETEDCHARACTERINFO_QUERY", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion

		#region �޸��û�G��
		/// <summary>
		/// �޸��û�G��
		/// </summary>
		/// <param name="userByID">GMTOOLS����Աid</param>
		/// <param name="serverIP">��Ϸ������ip</param>
		/// <param name="char_idx">��ɫ����</param>
		/// <param name="point">G��</param>
		/// <param name="admind">Ԥ���ֶ�</param>
		/// <returns>���ش�������1ʧ�ܣ�0�ɹ�</returns>
		public static int Gamepoint_Modify(int userByID, string serverIP, int char_idx,int point,string admind)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@Soccer_Serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@char_idx",SqlDbType.BigInt),
												   new SqlParameter("@point",SqlDbType.BigInt),
												   new SqlParameter("@admid",SqlDbType.VarChar,20),
											       new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = char_idx;
				paramCode[3].Value = point;
				paramCode[4].Value = admind;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SOCCER_CHARPOINT", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion

		#region ��ѯͣ�������Ϣ
		/// <summary>
		/// ��ѯͣ�������Ϣ
		/// </summary>
		/// <param name="serverip">��Ϸ������ip</param>
		/// <param name="type">��ѯ���ͣ�1 δͣ�⣬0 ��ͣ��</param>
		/// <param name="content">loginId��Ϊ�� ȫ����ѯ����Ϊ�� ����ĳ�����</param>
		/// <returns>���صĽ����</returns>
		public static DataSet Soccer_AccountState_Query (string serverip,string type,string content)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@Soccer_serverIP",SqlDbType.VarChar,30),
												   new SqlParameter("@srh_type",SqlDbType.VarChar,10),
												   new SqlParameter("@srh_string",SqlDbType.VarChar,80)};
				paramCode[0].Value = serverip;
				paramCode[1].Value = type;
				paramCode[2].Value = content;
				result = SqlHelper.ExecSPDataSet("SOCCER_ACCOUNTSTATE_QUERY", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion

		#region ��ѯͣ���ɫ��Ϣ
		/// <summary>
		/// ��ѯͣ���ɫ��Ϣ
		/// </summary>
		/// <param name="serverip">��Ϸ������ip</param>
		/// <param name="ddtate">���ڱ�ʶ��Ϊ�� δͣ�⣬��Ϊ�� ͣ��</param>
		/// <param name="type">��ѯ���ͣ�cID��cName,cIdx,ȫѡ</param>
		/// <param name="content">��������</param>
		/// <returns>���صĽ����</returns>
		public static DataSet Soccer_CharacterState_Query (string serverip,string ddtate,string type,string content)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Soccer_serverIP",SqlDbType.VarChar,30),
												   new SqlParameter("@str_ddate",SqlDbType.VarChar,10),
												   new SqlParameter("@srh_type",SqlDbType.VarChar,10),
												   new SqlParameter("@srh_string",SqlDbType.VarChar,80)};
				paramCode[0].Value = serverip;
				paramCode[1].Value = ddtate;
				paramCode[2].Value = type;
				paramCode[3].Value = content;
				result = SqlHelper.ExecSPDataSet("SOCCER_CHARACTERSTATE_QUERY", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion

		#region �޸����ͣ����Ϣ
		/// <summary>
		/// �޸����ͣ����Ϣ
		/// </summary>
		/// <param name="userByID">GMTOOLS����Աid</param>
		/// <param name="serverIP">��Ϸ������ip</param>
		/// <param name="loginId">��ҵ�������</param>
		/// <param name="m_id">��ұ��</param>
		/// <param name="m_auth">����˺��Ƿ�ͣ�⣺0 ͣ��1 ��</param>
		/// <returns>���ش�������1ʧ�ܣ�0�ɹ�</returns>
		public static int AccountState_Modify(int userByID, string serverIP, string loginId,int m_id,int m_auth)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@Soccer_Serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@loginId",SqlDbType.VarChar,10),
												   new SqlParameter("@m_id",SqlDbType.Int),
												   new SqlParameter("@m_auth",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = loginId;
				paramCode[3].Value = m_id;
				paramCode[4].Value = m_auth;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SOCCER_ACCOUNTSTATE", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion

		#region �޸Ľ�ɫͣ����Ϣ
		/// <summary>
		/// �޸Ľ�ɫͣ����Ϣ
		/// </summary>
		/// <param name="userByID">GMTOOLS����Աid</param>
		/// <param name="serverIP">��Ϸ������ip</param>
		/// <param name="char_idx">��ɫ���</param>
		/// <param name="delete_date">��ҽ�ɫ�Ƿ�ͣ�⣺Ϊ�� �𣬲�Ϊ�� ͣ</param>
		/// <returns>���ش�������1ʧ�ܣ�0�ɹ�</returns>
		public static int CharacterState_Modify(int userByID, string serverIP, int char_idx,string delete_date)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@Soccer_Serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@char_idx",SqlDbType.BigInt),
												   new SqlParameter("@str_ddate",SqlDbType.VarChar,10),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = char_idx;
				paramCode[3].Value = delete_date;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SOCCER_CHARACTERSTATE", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		
		#region �ָ���ɫ��Ϣ
		/// <summary>
		/// �ָ���ɫ��Ϣ
		/// </summary>
		/// <param name="userByID">GMTOOLS����Աid</param>
		/// <param name="serverIP">��Ϸ������ip</param>
		/// <param name="char_idx">��ɫ����</param>
		/// <returns></returns>
		public static int Soccer_CharItems_Recovery(int userByID, string serverIP, int char_idx)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@Soccer_Serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@char_idx",SqlDbType.BigInt),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = char_idx;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SOCCER_CHARITEMS_RECOVERY", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion

		#region ����ɫ��Ϣ
		/// <summary>
		/// ����ɫ��Ϣ
		/// </summary>
		/// <param name="serverip">��Ϸ������ip</param>
		/// <param name="char_idx">��ɫ����</param>
		/// <param name="kind">��鷽ʽ:socket,name</param>
		/// <returns>���صĽ����</returns>
		public static DataSet Soccer_CharCheck(string serverip,int char_idx,string kind)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@Soccer_serverIP",SqlDbType.VarChar,30),
												   new SqlParameter("@char_idx",SqlDbType.BigInt),
												   new SqlParameter("@kind",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverip;
				paramCode[1].Value = char_idx;
				paramCode[2].Value = kind;
				result = SqlHelper.ExecSPDataSet("SOCCER_CHARCHECK", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
	}
}
