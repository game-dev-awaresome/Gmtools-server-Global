using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Common.DataInfo;
using Common.Logic;
using System.Text;
namespace Common.API
{
	/// <summary>
	/// UserInfoAPI ��ժҪ˵����
	/// </summary>
	public class UserInfoAPI
	{
		GMLogAPI logAPI = new GMLogAPI();
		private static uint iSequenceID = 0;
		Message msg ;
		//ReadXMLFile log;
		public UserInfoAPI()
		{
		}
		public UserInfoAPI(byte[] packet)
		{
			//log = new ReadXMLFile();
			iSequenceID = 0;
			msg= new Message(packet,(uint)packet.Length); 

		}
		public void switchResult(Message_Tag_ID tagID,byte[] packet)
		{
			msg= new Message(packet,(uint)packet.Length); 
			switch(tagID)
			{
				case Message_Tag_ID.USER_CREATE ://user_create
					GM_InsertUserInfo();
					break;
				case Message_Tag_ID.USER_UPDATE://user_update
					GM_UpdateUserInfo();
					break;
				case Message_Tag_ID.USER_DELETE://user_delete
					GM_DelUserInfo();
					break;

			}
		} 
		/// <summary>
		/// �õ�����GM�ʺ���Ϣ
		/// </summary>
		/// <returns>����GM�ʺ���Ϣ����Ϣ��</returns>
		public Message GM_QueryList(int index,int pageSize,int userID)
		{
            GMLogAPI logAPI = new GMLogAPI();
            ArrayList rowList = new ArrayList();
            DataSet ds = null;
            try
            {
				if (userID == 0)
				{
					ds = GMUserInfo.SelectAll(); 
				}
				else
				{
					ds = GMUserInfo.SelectAll(userID);
				}
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    return Message.COMMON_MES_RESP(LanguageAPI.API_UserInfoAPI_NoUserList, Msg_Category.USER_ADMIN, ServiceKey.USER_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
                }
                else
                {
                    //��ҳ��
                    int pageCount = 0;
                    pageCount = ds.Tables[0].Rows.Count % pageSize;
                    if (pageCount > 0)
                    {
                        pageCount = ds.Tables[0].Rows.Count / pageSize + 1;
                    }
                    else
                        pageCount = ds.Tables[0].Rows.Count / pageSize;
                    //��ҳ��ʾ
                    if (index + pageSize > ds.Tables[0].Rows.Count)
                    {
                        pageSize = ds.Tables[0].Rows.Count - index;
                    }
                    logAPI.writeTitle(LanguageAPI.API_UserInfoAPI_AccountInfo,LanguageAPI.API_UserInfoAPI_AccountInfo +  LanguageAPI.API_Success + " !");
                    Query_Structure[] structList = new Query_Structure[pageSize];
                    logAPI.writeContent(LanguageAPI.Logic_UserValidate_User,LanguageAPI.API_UserInfoAPI_Password,LanguageAPI.API_UserInfoAPI_MAC);
                    for (int i = index; i < index + pageSize; i++)
                    {
                        Query_Structure strut = new Query_Structure(12);
                        strut.AddTagKey(TagName.User_ID, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[0]));
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[1]);
                        strut.AddTagKey(TagName.UserName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
                        strut.AddTagKey(TagName.PassWord, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        string mac;
                        if (ds.Tables[0].Rows[i].IsNull(3) == false)
                            mac = ds.Tables[0].Rows[i].ItemArray[3].ToString();
                        else
                            mac = "";
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, mac);
                        strut.AddTagKey(TagName.MAC, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        //ʹ��ʱЧ
                        object limit;
                        if (ds.Tables[0].Rows[i].IsNull(8) == false)
                            limit = ds.Tables[0].Rows[i].ItemArray[8];
                        else
                            limit = "";
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_DATE, limit);
                        strut.AddTagKey(TagName.Limit, TagFormat.TLV_DATE, (uint)bytes.Length, bytes);

                        //��ʵ����
                        object realName;
                        if (ds.Tables[0].Rows[i].IsNull(4) == false)
                            realName = ds.Tables[0].Rows[i].ItemArray[4];
                        else
                            realName = "";
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, realName);
                        strut.AddTagKey(TagName.RealName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						//����ID                    
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[5]);
						strut.AddTagKey(TagName.DepartID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        //��������                     
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[6]);
						strut.AddTagKey(TagName.DepartName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);                        //״̬
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[7]);
                        strut.AddTagKey(TagName.User_Status, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        //��ҳ��
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
                        //����״̬
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[9]);
                        strut.AddTagKey(TagName.OnlineActive, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[10]);
						strut.AddTagKey(TagName.SysAdmin, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        structList[i - index] = strut;
                        logAPI.writeData(ds.Tables[0].Rows[i].ItemArray[1].ToString().Trim(), ds.Tables[0].Rows[i].ItemArray[2].ToString().Trim(), ds.Tables[0].Rows[i].ItemArray[3].ToString().Trim());
                    }
                    Console.WriteLine(logAPI.Buffer.ToString());
                    return Message.COMMON_MES_RESP(structList, Msg_Category.USER_ADMIN, ServiceKey.USER_QUERY_RESP, 12);
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(LanguageAPI.API_UserInfoAPI_NoUserList, Msg_Category.USER_ADMIN, ServiceKey.USER_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
            }
		}
		/// <summary>
		/// �õ�����GM�ʺ���Ϣ
		/// </summary>
		/// <returns>����GM�ʺ���Ϣ����Ϣ��</returns>
		public Message GM_QueryAll(int userID)
		{
			GMLogAPI logAPI = new GMLogAPI();
			ArrayList rowList = new ArrayList();
			DataSet ds = null;
			try
			{
				ds = GMUserInfo.SelectAll(userID);
				if(ds.Tables[0].Rows.Count<=0)
				{
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.USER_ADMIN,ServiceKey.USER_QUERY_ALL);
				}
				else
				{
					logAPI.writeTitle(LanguageAPI.API_Display + LanguageAPI.API_UserInfoAPI_AccountInfo,LanguageAPI.API_Display + LanguageAPI.API_UserInfoAPI_AccountInfo + LanguageAPI.API_Success + " !");
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					logAPI.writeContent(LanguageAPI.Logic_UserValidate_User,LanguageAPI.API_UserInfoAPI_Password,LanguageAPI.API_UserInfoAPI_MAC);
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						strut.AddTagKey(TagName.User_ID,TagFormat.TLV_INTEGER,4,TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]));
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.UserName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.PassWord,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						string mac;
						if(ds.Tables[0].Rows[i].IsNull(3)==false)
							mac = ds.Tables[0].Rows[i].ItemArray[3].ToString();
						else
							mac = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,mac);
						strut.AddTagKey(TagName.MAC,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	

						//ʹ��ʱЧ
						object limit;
						if(ds.Tables[0].Rows[i].IsNull(8)==false)
							limit = ds.Tables[0].Rows[i].ItemArray[8];
						else
							limit = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_DATE,limit);
						strut.AddTagKey(TagName.Limit,TagFormat.TLV_DATE,(uint)bytes.Length,bytes);

						//״̬
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[7]);
						strut.AddTagKey(TagName.User_Status,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						string realName;
						if(ds.Tables[0].Rows[i].IsNull(4)==false)
							realName = ds.Tables[0].Rows[i].ItemArray[4].ToString();
						else
							realName = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,realName);
						strut.AddTagKey(TagName.RealName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[10]);
						strut.AddTagKey(TagName.SysAdmin, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						structList[i]=strut;					
						logAPI.writeData(ds.Tables[0].Rows[i].ItemArray[1].ToString().Trim(),ds.Tables[0].Rows[i].ItemArray[2].ToString().Trim(),ds.Tables[0].Rows[i].ItemArray[3].ToString().Trim());
					}
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.COMMON_MES_RESP(structList,Msg_Category.USER_ADMIN,ServiceKey.USER_QUERY_ALL,7);
				}

			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(LanguageAPI.API_UserInfoAPI_NoUserList,Msg_Category.USER_ADMIN,ServiceKey.USER_QUERY_ALL,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
			catch(System.Exception ex1)
			{
				Console.WriteLine(ex1.Message);
				return Message.COMMON_MES_RESP(LanguageAPI.API_UserInfoAPI_NoUserList,Msg_Category.USER_ADMIN,ServiceKey.USER_QUERY_ALL,TagName.ERROR_Msg,TagFormat.TLV_STRING);

			}
		}
		/// <summary>
		/// �õ�GM�ʺ���Ϣ��
		/// </summary>
		/// <param name="userID">�û�ID</param>
		/// <returns>GM�ʺ���</returns>
		public Message GM_QuerySysAdminInfo(int userID)
		{
			//int sysAdmin = -1;
			System.Data.DataSet ds = null;

			try
			{
				//��GM�ʺ���Ϣ����DATASET
				ds = GMUserInfo.getUserInfo(userID);
				Query_Structure[] structList = new Query_Structure[1];
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[0].ItemArray.Length);
					strut.AddTagKey(TagName.SysAdmin,TagFormat.TLV_INTEGER,4,TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[9]));
					byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[0].ItemArray[5]);
					strut.AddTagKey(TagName.DepartID,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
					structList[0] = strut;
					return Message.COMMON_MES_RESP(structList,Msg_Category.USER_ADMIN,ServiceKey.USER_SYSADMIN_QUERY_RESP,2);
				}
				else
				{
					return Message.COMMON_MES_RESP(LanguageAPI.API_UserInfoAPI_NoAdmin,Msg_Category.USER_ADMIN,ServiceKey.USER_SYSADMIN_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP(LanguageAPI.API_UserInfoAPI_NoAdmin,Msg_Category.USER_ADMIN,ServiceKey.USER_SYSADMIN_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
			
		}
		/// <summary>
		/// �õ�GM�ʺ���Ϣ��
		/// </summary>
		/// <param name="userID">�û�ID</param>
		/// <returns>GM�ʺ���</returns>
		public GMUserInfo GM_QueryUserInfo(int userID)
		{
			System.Data.DataSet ds = null;
			GMUserInfo userInfo = null;

			try
			{
				//��GM�ʺ���Ϣ����DATASET
				ds = GMUserInfo.getUserInfo(userID);
				//����һ���û������������Ϣ��
				userInfo = new GMUserInfo(ds.Tables[0].Rows[0].ItemArray[0].ToString(),ds.Tables[0].Rows[0].ItemArray[1].ToString());
			}

			catch(System.Exception ex)
			{
                Console.WriteLine(ex.Message);		
				
			}

			return userInfo;
			
		}
		/// <summary>
		/// ��֤GM�ʺ���Ч��
		/// </summary>
		/// <param name="userName">�û���</param>
		/// <param name="password">����</param>
		public int GM_ValidateUser(string userName,string password,string MAC)
		{
			int result = -1;
			try
			{
               result =  GMUserInfo.getUserInfo(userName,password,MAC);
			}
			catch(System.Exception)
			{		
				
			}
			  return result;
		}
		/// <summary>
		/// �жϸ��ʺ��Ƿ��Ѿ�����
		/// </summary>
		/// <param name="userName">�û���</param>
		/// <param name="password">����</param>
		public int GM_ActiveUser(string userName,string password,string MAC)
		{
			int result = 1;
			try
			{
				result =  GMUserInfo.getActiveUser(userName,password,MAC);
			}
			catch(System.Exception)
			{		
				
			}
			return result;
		}
		/// <summary>
		/// ��¼MAC����Ϣ
		/// </summary>
		/// <returns>��¼MAC��Ϣ</returns>
		public void GM_UpdateMACInfo()
		{
			int result = -1;
			string userName = null;
			string passWd = null;
			string mac= null;
			try
			{
				userName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.UserName).m_bValueBuffer);
				passWd =Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.PassWord).m_bValueBuffer);
				mac = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.MAC).m_bValueBuffer);
				result = GMUserInfo.insertMac(userName,passWd,mac);
			}
			catch(Common.Logic.Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

		}
		/// <summary>
		/// ��¼�����û�
		/// </summary>
		/// <returns>��¼�����û�</returns>
		public void GM_UpdateActiveUser(int userByID,int status)
		{
			int result = -1;
			try
			{
				result = GMUserInfo.updateActiveUser(userByID,status);
			}
			catch(Common.Logic.Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

		}
        /// <summary>
        /// ��¼�����û�
        /// </summary>
        /// <returns>��¼�����û�</returns>
        public Message GM_UpdateActiveUserPkg(int userByID, int status)
        {
            int result = -1;
            try
            {
                result = GMUserInfo.updateActiveUser(userByID, status);
                if(result==1)
				{
					Console.WriteLine(DateTime.Now+" - "+LanguageAPI.API_Update+userByID+LanguageAPI.API_UserInfoAPI_UserStatus+LanguageAPI.API_Success+"!");
                    return Message.COMMON_MES_RESP("SUCESS", Msg_Category.USER_ADMIN,ServiceKey.UPDATE_ACTIVEUSER_RESP);
				}
				else
				{
					Console.WriteLine(DateTime.Now+" - "+LanguageAPI.API_Update+userByID+LanguageAPI.API_UserInfoAPI_UserStatus+LanguageAPI.API_Success+"!");
                    return Message.COMMON_MES_RESP("FAILURE", Msg_Category.USER_ADMIN, ServiceKey.UPDATE_ACTIVEUSER_RESP);
				}
            }
            catch (Common.Logic.Exception ex)
            {
                return Message.COMMON_MES_RESP("FAILURE",Msg_Category.USER_ADMIN,ServiceKey.UPDATE_ACTIVEUSER_RESP);
            }

        }
		/// <summary>
		/// �������Ͽ��Ժ󣬸��������û�Ϊ�Ͽ�״̬
		/// </summary>
		/// <returns>��¼�����û�</returns>
		public void GM_UpdateActiveUser(int status)
		{
			int result = -1;
			try
			{
				result = GMUserInfo.updateActiveUser(status);
			}
			catch(Common.Logic.Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

		}
		/// <summary>
		/// ����һ���µ�GM�ʺ�
		/// </summary>
		public Message GM_InsertUserInfo()
		{
			int result = -1;
			int operateUserID = 0;
			int departID = 0;
			string userName = null;
			string passWd = null;
			string realName = null;
			DateTime limit = DateTime.Now;
			int status= 0;
			try
			{
				//����ԱID
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				//����ID
				strut = new TLV_Structure(TagName.DepartID,4,msg.m_packet.m_Body.getTLVByTag(TagName.DepartID).m_bValueBuffer);
				departID = (int)strut.toInteger();
                //�û���
				userName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.UserName).m_bValueBuffer);
				//����
				passWd =Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.PassWord).m_bValueBuffer);
				//������
				realName =Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RealName).m_bValueBuffer);
				//״ֵ̬
				TLV_Structure tlvStruct = new TLV_Structure(TagName.User_Status,4,msg.m_packet.m_Body.getTLVByTag(TagName.User_Status).m_bValueBuffer);
				status =(int)tlvStruct.toInteger();
				//ʹ��ʱ��
				tlvStruct = new TLV_Structure(TagName.Limit,3,msg.m_packet.m_Body.getTLVByTag(TagName.Limit).m_bValueBuffer);
				limit =tlvStruct.toDate();
				//�Ƿ����Ա
				tlvStruct = new TLV_Structure(TagName.SysAdmin,4,msg.m_packet.m_Body.getTLVByTag(TagName.SysAdmin).m_bValueBuffer);
				int sysAdmin =(int)tlvStruct.toInteger();

				result = GMUserInfo.insertRow(operateUserID,departID,userName,passWd,realName,limit,status,sysAdmin);
				if(result==1)
				{
					logAPI.writeTitle(LanguageAPI.API_Add + LanguageAPI.API_UserInfoAPI_AccountInfo,LanguageAPI.API_Add + LanguageAPI.API_UserInfoAPI_AccountInfo + LanguageAPI.API_Success + "!");
					logAPI.writeContent(LanguageAPI.Logic_UserValidate_User,LanguageAPI.API_UserInfoAPI_Password,LanguageAPI.API_UserInfoAPI_LimitDay);
					logAPI.writeData(userName,passWd,limit.ToString());
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.Common_USER_CREATE_RESP("SUCESS");
				}
				else
				{
					logAPI.writeTitle(LanguageAPI.API_Add + LanguageAPI.API_UserInfoAPI_AccountInfo,LanguageAPI.API_Add + LanguageAPI.API_UserInfoAPI_AccountInfo + LanguageAPI.API_Failure + "!");
					logAPI.writeContent(LanguageAPI.Logic_UserValidate_User,LanguageAPI.API_UserInfoAPI_Password,LanguageAPI.API_UserInfoAPI_LimitDay);
					logAPI.writeData(userName,passWd,limit.ToString());
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.Common_USER_CREATE_RESP("FAILURE");
				}
			}
			catch(Common.Logic.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.Common_USER_CREATE_RESP(ex.Message);
			}

		}
		/// <summary>
		/// ɾ��һ���û���Ϣ
		/// </summary>
		public Message GM_DelUserInfo()
		{
			int result = -1;
			int userID = 0;
			int userByID = 0;
			try
			{
				TLV_Structure strut1 = new TLV_Structure(TagName.User_ID,4,msg.m_packet.m_Body.getTLVByTag(TagName.User_ID).m_bValueBuffer);
				userID =(int)strut1.toInteger();
				TLV_Structure strut2 = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userByID =(int)strut2.toInteger();
				result = GMUserInfo.deleteRow(userID,userByID);
				if(result==1)
				{
					logAPI.writeTitle(LanguageAPI.API_Delete + LanguageAPI.API_UserInfoAPI_AccountInfo,LanguageAPI.API_Delete + LanguageAPI.API_UserInfoAPI_AccountInfo + LanguageAPI.API_Success + "!");
					logAPI.writeContent(LanguageAPI.API_DepartmentAPI_OperatorID,LanguageAPI.API_UserInfoAPI_UserID,LanguageAPI.API_UserInfoAPI_LimitDay);
					logAPI.writeData(Convert.ToString(userByID),Convert.ToString(userID),LanguageAPI.API_Delete);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.Common_USER_DELETE_RESP("SUCESS");
				}
				else
				{
					logAPI.writeTitle(LanguageAPI.API_Delete + LanguageAPI.API_UserInfoAPI_AccountInfo,LanguageAPI.API_Delete + LanguageAPI.API_UserInfoAPI_AccountInfo + LanguageAPI.API_Failure + "!");
					logAPI.writeContent(LanguageAPI.API_DepartmentAPI_OperatorID,LanguageAPI.API_UserInfoAPI_UserID,LanguageAPI.API_UserInfoAPI_LimitDay);
					logAPI.writeData(Convert.ToString(userID),Convert.ToString(userByID),LanguageAPI.API_Delete);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.Common_USER_DELETE_RESP("FAILURE");
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
				return Message.Common_USER_DELETE_RESP("FAILURE");
			}

		}
		/// <summary>
		/// �޸��û���Ϣ
		/// </summary>
		public Message GM_UpdateUserInfo()
		{
			int result = -1;
			int deptID = 0;
			int userID = 0;
			int userByID = 0;
			string realName = null;
			int status = 0;
            int onlineActive = 0;
			int sysAdmin = 0;
			DateTime limitTime;
			try
			{
				TLV_Structure tlv = new TLV_Structure(TagName.User_ID,4,msg.m_packet.m_Body.getTLVByTag(TagName.User_ID).m_bValueBuffer);
				userID =(int)tlv.toInteger();
				tlv = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userByID  =(int)tlv.toInteger();
				tlv = new TLV_Structure(TagName.DepartID,4,msg.m_packet.m_Body.getTLVByTag(TagName.DepartID).m_bValueBuffer);
				deptID =(int)tlv.toInteger();
				realName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RealName).m_bValueBuffer);
				tlv = new TLV_Structure(TagName.Limit,3,msg.m_packet.m_Body.getTLVByTag(TagName.Limit).m_bValueBuffer);
				limitTime =tlv.toDate();
				tlv = new TLV_Structure(TagName.User_Status,4,msg.m_packet.m_Body.getTLVByTag(TagName.User_Status).m_bValueBuffer);
				status =(int)tlv.toInteger();
                tlv = new TLV_Structure(TagName.OnlineActive, 4, msg.m_packet.m_Body.getTLVByTag(TagName.OnlineActive).m_bValueBuffer);
                onlineActive = (int)tlv.toInteger();
				tlv = new TLV_Structure(TagName.SysAdmin, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SysAdmin).m_bValueBuffer);
				sysAdmin = (int)tlv.toInteger();
                result = GMUserInfo.UpdateRow(userByID, deptID, userID, realName, limitTime, status, onlineActive,sysAdmin);
				if(result==1)
				{
					logAPI.writeTitle(LanguageAPI.API_Update + LanguageAPI.API_UserInfoAPI_AccountInfo,LanguageAPI.API_Update + LanguageAPI.API_UserInfoAPI_AccountInfo + LanguageAPI.API_Success + "!");
					logAPI.writeContent(LanguageAPI.API_DepartmentAPI_OperatorID,LanguageAPI.API_UserInfoAPI_UserID,LanguageAPI.API_UserInfoAPI_LimitDay);
					logAPI.writeData(Convert.ToString(userByID),Convert.ToString(userID),Convert.ToString(limitTime));
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.Common_USER_UPDATE_RESP("SUCESS");
				}
				else
				{
					logAPI.writeTitle(LanguageAPI.API_Update + LanguageAPI.API_UserInfoAPI_AccountInfo,LanguageAPI.API_Update + LanguageAPI.API_UserInfoAPI_AccountInfo + LanguageAPI.API_Failure + "!");
					logAPI.writeContent(LanguageAPI.API_DepartmentAPI_OperatorID,LanguageAPI.API_UserInfoAPI_UserID,LanguageAPI.API_UserInfoAPI_LimitDay);
					logAPI.writeData(Convert.ToString(userByID),Convert.ToString(userID),Convert.ToString(limitTime));
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.Common_USER_UPDATE_RESP("FAILURE");
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
				return Message.Common_USER_UPDATE_RESP(ex.Message);
			}

		}
		/// <summary>
		/// �޸�������Ϣ
		/// </summary>
		public Message GM_ModifPassWd()
		{
			GMLogAPI logAPI = new GMLogAPI();
			int result = -1;
			int userID = 0;
			int userByID = 0;
			string passWd = null;
			try
			{
				TLV_Structure strut1 = new TLV_Structure(TagName.User_ID,4,msg.m_packet.m_Body.getTLVByTag(TagName.User_ID).m_bValueBuffer);
				userID =(int)strut1.toInteger();
				TLV_Structure strut2 = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userByID  =(int)strut2.toInteger();
				passWd =Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.PassWord).m_bValueBuffer);
				result = GMUserInfo.UpdateRow(userByID,userID,passWd);
				if(result==1)
				{
					logAPI.writeTitle(LanguageAPI.API_Update + LanguageAPI.API_UserInfoAPI_Password,LanguageAPI.API_Update + LanguageAPI.API_UserInfoAPI_Password + LanguageAPI.API_Success + "!");
					logAPI.writeContent(LanguageAPI.API_DepartmentAPI_OperatorID,LanguageAPI.API_UserInfoAPI_UserID,LanguageAPI.API_UserInfoAPI_NewPassword);
					logAPI.writeData(Convert.ToString(userByID),Convert.ToString(userID),passWd);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.USER_ADMIN,ServiceKey.USER_PASSWD_MODIF_RESP);

				}
				else
				{
					logAPI.writeTitle(LanguageAPI.API_Update + LanguageAPI.API_UserInfoAPI_Password,LanguageAPI.API_Update + LanguageAPI.API_UserInfoAPI_Password + LanguageAPI.API_Failure + "!");
					logAPI.writeContent(LanguageAPI.API_DepartmentAPI_OperatorID,LanguageAPI.API_UserInfoAPI_UserID,LanguageAPI.API_UserInfoAPI_NewPassword);
					logAPI.writeData(Convert.ToString(userByID),Convert.ToString(userID),passWd);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.USER_ADMIN,ServiceKey.USER_PASSWD_MODIF_RESP);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.USER_ADMIN,ServiceKey.USER_PASSWD_MODIF_RESP);
			}

		}
		public void parseUserInfoMsg(byte[] packet)
		{
			int iCurPos = 0;
			uint sequence = 0;
            Packet_Head head = new Packet_Head();
            sequence=head.charArrayToInt(packet,4);
            iCurPos+=4;

		}
		/// <summary>
		/// ��װ�û��б����Ϣ��
		/// </summary>
		/// <returns>��������Ϣ��</returns>
		public static byte[] PackUserListMsg(int userID)
		{
			string[] userName ;
			string[] passWd;
			string[] MAC;
			int iCurrPos = 0;
			int iDetailLen = 0;
			byte[] bDetail = new byte[128];

			//����UserInfo��ĺ��������û������ݶ���������DATASET����
			DataSet ds = GMUserInfo.SelectAll(userID);
            userName = new string[ds.Tables[0].Rows.Count];
			passWd = new string[ds.Tables[0].Rows.Count];
			MAC = new string[ds.Tables[0].Rows.Count];

			//�����û����������ݣ����û��������롢MAC����Byte��������
			//iCurrPos��¼Byte�����λ��
			for(int i=0;i<ds.Tables[0].Rows.Count;i++)
			{
				userName[i] = ds.Tables[0].Rows[i][1].ToString();
				passWd[i] = ds.Tables[0].Rows[i][2].ToString();
				MAC[i] = ds.Tables[0].Rows[i][3].ToString();
				byte[] bTmpUserName = System.Text.Encoding.Default.GetBytes(userName[i]);
				Array.Copy(bTmpUserName,0,bDetail,iCurrPos,bTmpUserName.Length);
                iCurrPos+=bTmpUserName.Length;

				byte[] bTmpPassWord = System.Text.Encoding.Default.GetBytes(passWd[i]);
				Array.Copy(bTmpPassWord,0,bDetail,iCurrPos,bTmpPassWord.Length);
				iCurrPos+=bTmpPassWord.Length;

				byte[] bTmpMac = System.Text.Encoding.Default.GetBytes(MAC[i]);
				Array.Copy(bTmpMac,0,bDetail,iCurrPos,bTmpMac.Length);
				iCurrPos+=bTmpMac.Length;

			}
			//�õ���Ϣ���ĳ���
			iDetailLen = iCurrPos;
			//��ȡ����Byte�ռ�
			byte[] detailMsg = Packet_Body.getPacketBody(bDetail);
			byte[] bHead = new byte[16+ds.Tables[0].Rows.Count*3+iDetailLen];
			iCurrPos = 0;
			//�������к�ID
			bHead[iCurrPos++]=System.Convert.ToByte(iSequenceID&255);
			bHead[iCurrPos++]=System.Convert.ToByte((iSequenceID&65280)>>8);
			bHead[iCurrPos++]=System.Convert.ToByte((iSequenceID&16711680)>>16);
			bHead[iCurrPos++]=System.Convert.ToByte((iSequenceID&4278190080)>>32);
			//��Ϣ����
			bHead[iCurrPos++]=0x82;

			//������ַ
			bHead[iCurrPos++]=0x80;

			//������REQUEST����RESP
			bHead[iCurrPos++]=0x01;
			bHead[iCurrPos++]=0x00;

            //������Ϣ�����ݵĳ��ȣ�����������Ϣͷ���棬��������Ƕ��еĻ���ѭ���洢����
			for(int i=0;i<ds.Tables[0].Rows.Count;i++)
			{
				bHead[iCurrPos++]=Convert.ToByte(userName[i].Length);
				bHead[iCurrPos++]=Convert.ToByte(passWd[i].Length);
				bHead[iCurrPos++]=Convert.ToByte(MAC[i].Length);
			}

			//��Ϣ��������
			System.DateTime dt = System.DateTime.Now;
			bHead[iCurrPos++]=System.Convert.ToByte(dt.Year-1900);
			bHead[iCurrPos++]=System.Convert.ToByte(dt.Month);
			bHead[iCurrPos++]=System.Convert.ToByte(dt.Day);
			bHead[iCurrPos++]=System.Convert.ToByte(dt.Hour);
			bHead[iCurrPos++]=System.Convert.ToByte(dt.Minute);
			bHead[iCurrPos++]=System.Convert.ToByte(dt.Second);
			iSequenceID++;
			bHead[iCurrPos++]=System.Convert.ToByte(detailMsg.Length&255);
			bHead[iCurrPos++]=System.Convert.ToByte((detailMsg.Length&65280)>>8);

            //����Ϣͷ����Ϣ�����һ����Ϣ��
			Array.Copy(bDetail,0,bHead,iCurrPos,detailMsg.Length);
			
			return bHead;
		}
		/// <summary>
		/// ��װһ���½�GM�ʺ���Ϣ��
		/// </summary>
		/// <param name="userName">�û���</param>
		/// <param name="passWord">����</param>
		/// <param name="MAC">MAC��</param>
		/// <returns></returns>
		public static byte[] PackUserInfoMsg(string userName,string passWord,string MAC)
		{
			int iCurrPos = 0;
			int iDetailLen = 0;

			iDetailLen = System.Text.Encoding.ASCII.GetBytes(userName).Length;
			iDetailLen += System.Text.Encoding.ASCII.GetBytes(passWord).Length;
			iDetailLen += System.Text.Encoding.ASCII.GetBytes(MAC).Length;
			byte[] bDetail = new byte[3*3+iDetailLen];

			bDetail[iCurrPos++]=0x01;
			bDetail[iCurrPos++]=0x01;
			bDetail[iCurrPos++]=0x01;
            //�û���
			byte[] bTmpUserName = System.Text.Encoding.ASCII.GetBytes(userName);
			bDetail[iCurrPos++]=Convert.ToByte(bTmpUserName.Length);
			Array.Copy(bTmpUserName,0,bDetail,iCurrPos,bTmpUserName.Length);
			iCurrPos+=bTmpUserName.Length;

			bDetail[iCurrPos++]=0x02;
			bDetail[iCurrPos++]=0x01;
			bDetail[iCurrPos++]=0x01;
			//����
			byte[] bTmpPassWord = System.Text.Encoding.ASCII.GetBytes(passWord);
			bDetail[iCurrPos++]=Convert.ToByte(bTmpPassWord.Length);
			Array.Copy(bTmpPassWord,0,bDetail,iCurrPos,bTmpPassWord.Length);
			iCurrPos+=bTmpPassWord.Length;

			bDetail[iCurrPos++]=0x03;
			bDetail[iCurrPos++]=0x01;
			bDetail[iCurrPos++]=0x01;
			//MAC��
			byte[] bTmpMac = System.Text.Encoding.ASCII.GetBytes(MAC);
			bDetail[iCurrPos++]=Convert.ToByte(bTmpMac.Length);
			Array.Copy(bTmpMac,0,bDetail,iCurrPos,bTmpMac.Length);
			iCurrPos+=bTmpMac.Length;

			byte[] bHead = new byte[16+iDetailLen];
			iCurrPos = 0;
			//Sequence_id
			bHead[iCurrPos++]=System.Convert.ToByte(iSequenceID&255);
			bHead[iCurrPos++]=System.Convert.ToByte((iSequenceID&65280)>>8);
			bHead[iCurrPos++]=System.Convert.ToByte((iSequenceID&16711680)>>16);
			bHead[iCurrPos++]=System.Convert.ToByte((iSequenceID&4278190080)>>32);
			//Msg_Category
			bHead[iCurrPos++]=0x81;

			//Reserved
			bHead[iCurrPos++]=0x80;

			//Service_Key
			bHead[iCurrPos++]=0x01;
			bHead[iCurrPos++]=0x00;

			//Msg_Date
			System.DateTime dt = System.DateTime.Now;
			bHead[iCurrPos++]=System.Convert.ToByte(dt.Year-1900);
			bHead[iCurrPos++]=System.Convert.ToByte(dt.Month);
			bHead[iCurrPos++]=System.Convert.ToByte(dt.Day);
			bHead[iCurrPos++]=System.Convert.ToByte(dt.Hour);
			bHead[iCurrPos++]=System.Convert.ToByte(dt.Minute);
			bHead[iCurrPos++]=System.Convert.ToByte(dt.Second);
			iSequenceID++;
			bHead[iCurrPos++]=System.Convert.ToByte(bDetail.Length&255);
			bHead[iCurrPos++]=System.Convert.ToByte((bDetail.Length&65280)>>8);

			Array.Copy(bDetail,0,bHead,iCurrPos,bDetail.Length);
			
			return bHead;

		}
		private static byte[] PackMsg(byte[] bDetail)
		{
			byte[] bPacket = new byte[bDetail.Length + 2];
			bPacket[0]  = 0xFE;
			Array.Copy(bDetail,0,bPacket,1,bDetail.Length);
			bPacket[bDetail.Length+1]=0xEF;

			return bPacket;
		}
	}
}
