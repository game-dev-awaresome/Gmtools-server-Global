using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
namespace Common.DataInfo
{
	/// <summary>
	/// GMNotesInfo ��ժҪ˵����
	/// </summary>
	public class GMNotesInfo
	{
		/// <summary>
		/// �ʼ�ID
		/// </summary>
		int letterID = 0;
		/// <summary>
		/// ������
		/// </summary>
		string letterSender;
		/// <summary>
		/// �ռ���
		/// </summary>
		string letterReceiver; 
		/// <summary>
		/// �ʼ�����
		/// </summary>
		string letterSubject;
		/// <summary>
		/// �ʼ�����
		/// </summary>
		string letterText;
		/// <summary>
		/// ��������
		/// </summary>
		DateTime sendDate;
		/// <summary>
		/// ������
		/// </summary>
		string processMan; 
		/// <summary>
		/// ��������
		/// </summary>
		DateTime processDate;
		/// <summary>
		/// ת���ռ���
		/// </summary>
		string transmitMan; 
		/// <summary>
		/// �Ƿ���
		/// </summary>
		int isProcess =0;
		public GMNotesInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region �õ���ǰ�û���������ʼ�
		/// <summary>
		/// ȡ��NOTES�����ż��б�
		/// </summary>
		/// <returns>NOTES�����ż��б�</returns>
		public static DataSet SelectTransmitLetter(int userByID)
		{
			string strSQL="select b.*,a.realName from GMTOOLS_users a,letter_Info b where a.userID=b.processMan and isProcess=1 and transmitMan ="+userByID+" order by sendDate desc";
			DataSet ds =SqlHelper.ExecuteDataset(strSQL);
			return ds;
		}
		#endregion
		#region ȡ��NOTES�����ż��б�
		/// <summary>
		/// ȡ��NOTES�����ż��б�
		/// </summary>
		/// <returns>NOTES�����ż��б�</returns>
		public static DataSet SelectAll()
		{
			string strSQL="select * from letter_Info where isProcess=0";
			DataSet ds =SqlHelper.ExecuteDataset(strSQL);
			return ds;
		}
		#endregion
		#region �ʼ�����
		public static int updateRow(int userByID,int letterID,int processMan,DateTime processDate,int transmitMan,int isProcess,string reason)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[8]{   new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												   new SqlParameter("@Gm_LetterID",SqlDbType.Int),
												   new SqlParameter("@Gm_ProcessMan",SqlDbType.Int),
												   new SqlParameter("@GM_ProcessDate",SqlDbType.DateTime),
												   new SqlParameter("@GM_TransmitMan",SqlDbType.Int),
												   new SqlParameter("@GM_IsProcess",SqlDbType.Int),
												   new SqlParameter("@Gm_NotesReason",SqlDbType.VarChar,2000),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=letterID;
				paramCode[2].Value = processMan;
				paramCode[3].Value = processDate;
				paramCode[4].Value = transmitMan;
				paramCode[5].Value = isProcess;
				paramCode[6].Value = reason;
				paramCode[7].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Gmtool_GmNotes_Update",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		public int LetterID
		{
			get
			{
				return letterID;
			}
			set
			{
				letterID = value;
			}
		}
		public string LetterSender
		{
			get
			{
				return this.letterSender;
			}
			set
			{
				letterSender = value;
			}
		}
		public string LetterReceiver
		{
			get
			{
				return this.letterReceiver;
			}
			set
			{
				letterReceiver = value;
			}
		}
		public string LetterSubject
		{
			get
			{
				return this.letterSubject;
			}
			set
			{
				letterSubject = value;
			}
		}
		public string LetterText
		{
			get
			{
				return this.letterText;
			}
			set
			{
				letterText = value;
			}
		}
		public DateTime SendDate
		{
			get
			{
				return this.sendDate;
			}
			set
			{
				sendDate = value;
			}

		}
		public string ProcessMan
		{
			get
			{
				return this.processMan;
			}
			set
			{
				processMan = value;
			}
		}
		public DateTime ProcessDate
		{
			get
			{
				return this.processDate;
			}
			set
			{
				processDate = value;
			}

		}
		public string TransmitMan
		{
			get
			{
				return this.transmitMan;
			}
			set
			{
				transmitMan = value;
			}

		}
		public int IsProcess
		{
			get
			{
				return this.isProcess;
			}
			set
			{
				isProcess = value;
			}

		}
	}
}
