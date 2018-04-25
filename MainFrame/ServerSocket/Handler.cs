using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.IO;
using Common.Logic;
using Common.API;
using Common.DataInfo;
using SDO.SDOAPI;
using Audition.AUAPI;
using CR.CRAPI;
using UserCharge.UserChargeAPI;
using O2JAM.O2JAMAPI;
using BAF.O2JAM2API;
using Soccer.SOCCERAPI;
using lg = Common.API.LanguageAPI;
namespace GMSERVER.ServerSocket
{
	/// <summary>
	/// Handler ��ժҪ˵����
	/// </summary>
	public class Handler
	{
		private TcpClient svrSocket ;
		private NetworkStream networkStream ;
		private Mutex mut = new Mutex();
		bool ContinueProcess = false ;
		private byte[] bytes; 		// Data buffer for incoming data.
		private StringBuilder sb =  new StringBuilder(); // Received data string.
		private string data = null; // Incoming data from the client.
		Handler[] handler_ = new Handler[2];
		int handlerType = 0;

		public Handler (TcpClient acceptClientSocket) 
		{
			this.svrSocket = acceptClientSocket ;
			networkStream = acceptClientSocket.GetStream();
			bytes = new byte[acceptClientSocket.ReceiveBufferSize];
			handler_[handlerType]=this;
			//ContinueProcess = true ;
		}
		public Handler getHandler(int type)
		{
			return this.handler_[type];
		}
		/// <summary>
		/// �����ͻ������ӽ����Ժ󣬷������˽��ܺͷ�����Ϣ�Ĵ�������
		/// </summary>
		public void Process() 
		{
			int index = 0;
			int pageSize = 0;
			int userByID = 0;
			string name = null;
			string passwd = null;
			string mac = null;
			DateTime connTime;
			int status = 0;
			mut.WaitOne();
			try
			{
				while ( true ) 
				{

					UserValidate validate = null;
					int BytesRead =  networkStream.Read(bytes, 0, (int) bytes.Length) ;
					Message msg = new Message(bytes,(uint)bytes.Length);
					Packet packet = msg.m_packet;
					if( msg.IsValidMessage==true)
					{
						Packet_Body mesBody = new Packet_Body(packet.m_Body.m_bBodyBuffer,packet.m_Body.m_uiBodyLen);
						if (msg.GetMessageID() ==Message_Tag_ID.CONNECT 
							|| msg.GetMessageID() ==Message_Tag_ID.ACCOUNT_AUTHOR
							|| msg.GetMessageID() == Message_Tag_ID.DISCONNECT)
						{
							name = System.Text.Encoding.Default.GetString(mesBody.getTLVByTag(TagName.UserName).m_bValueBuffer);
							data = name;
							passwd = System.Text.Encoding.Default.GetString(mesBody.getTLVByTag(TagName.PassWord).m_bValueBuffer);
							mac = System.Text.Encoding.Default.GetString(mesBody.getTLVByTag(TagName.MAC).m_bValueBuffer);
							if(msg.GetMessageID()==Message_Tag_ID.CONNECT )
							{
								//TLV_Structure tvlLimit = new TLV_Structure(TagName.Conn_Time,mesBody.getTLVByTag(TagName.Conn_Time).m_uiValueLen,mesBody.getTLVByTag(TagName.Conn_Time).m_bValueBuffer);
								connTime = DateTime.Now; 
								//��Ӧ��������
								CommonAPI api = new CommonAPI(name,passwd,mac,connTime,"PASS");
								send(api.packConnectResp());
							}
							else if(msg.GetMessageID() ==Message_Tag_ID.ACCOUNT_AUTHOR)
							{
								//�û���֤����
								validate = new UserValidate(name,passwd,mac);
								send(validate.validateUser());
								userByID = validate.UserByID;
								status = validate.Status;
							}
							else if(msg.GetMessageID() == Message_Tag_ID.DISCONNECT)
							{
								//��Ӧ�Ͽ�����
								TLV_Structure stuct = new TLV_Structure(TagName.UserByID,mesBody.getTLVByTag(TagName.UserByID).m_uiValueLen,mesBody.getTLVByTag(TagName.UserByID).m_bValueBuffer);
								userByID = (int)stuct.toInteger();
								CommonAPI api = new CommonAPI(userByID,"PASS",bytes);
								UserInfoAPI userInfo = new UserInfoAPI();
								userInfo.GM_UpdateActiveUser(userByID,0);
								send(api.packConnectResp());
							}
						}
						//��֤ͨ��
						if(status==1)
						{
							TLV_Structure stuct;
							UpdatePatch patch = new UpdatePatch(bytes);
							DepartmentAPI departInfo = new DepartmentAPI(bytes);
							UserInfoAPI userInfo = new UserInfoAPI(bytes);
							ModuleInfoAPI moduleInfo = new ModuleInfoAPI(bytes);
							GameInfoAPI gameInfo = new GameInfoAPI(bytes);
							UserModuleAPI userModule = new UserModuleAPI(bytes);
							NotesInfoAPI notesAPI = new NotesInfoAPI(bytes);
							O2JAMCharacterInfoAPI o2jamCharacterAPI = new O2JAMCharacterInfoAPI(bytes);
							O2JAMItemShopAPI o2itemShop = new O2JAMItemShopAPI(bytes);
							SDOMemberInfoAPI accountAPI = new SDOMemberInfoAPI(bytes);
							SDOCharacterInfoAPI sdocharacterAPI = new SDOCharacterInfoAPI(bytes);
							SDOItemShopAPI itemShopAPI = new SDOItemShopAPI(bytes);
							SDONoticeInfoAPI noticeInfo = new SDONoticeInfoAPI(bytes);
							CommonAPI api = new CommonAPI(userByID,"PASS",bytes);
							SDOItemLogInfoAPI ItemlogAPI = new SDOItemLogInfoAPI(bytes);
							AUMemberInfoAPI auMemberAPI = new AUMemberInfoAPI(bytes);
							CRAccountInfoAPI crAccountAPI = new CRAccountInfoAPI(bytes);
							CRCallBoardAPI crCallBoardAPI = new CRCallBoardAPI(bytes);
							CRCharacterInfoAPI crCharacterInfoAPI = new CRCharacterInfoAPI(bytes);
							AUAvatarListAPI avatarListAPI = new AUAvatarListAPI(bytes);
							AUCharacterInfoAPI characterInfoAPI = new AUCharacterInfoAPI(bytes);
							CardDetailInfoAPI cardDetailInfoAPI = new CardDetailInfoAPI(bytes);
							UserCashPurchaseAPI userCashPurchaseAPI = new UserCashPurchaseAPI(bytes);
							AccountInfoAPI o2jam2Account =  new AccountInfoAPI(bytes);
							CharacterInfoAPI o2jam2CharacterInfo = new CharacterInfoAPI(bytes);
							ItemShopAPI o2jam2ItemShopAPI = new ItemShopAPI(bytes);
							SDOChallengeDataAPI challenge = new SDOChallengeDataAPI(bytes);
							SOCCERCharacterInfoAPI soccercharacterAPI = new SOCCERCharacterInfoAPI(bytes);
							switch(msg.GetMessageID())
							{
									//�ͻ��˱Ƚ�����
								case Message_Tag_ID.CLIENT_PATCH_COMPARE:
								{
									send(patch.encodeMessage());
									break;
								}
									//�ͻ��˸�������
								case Message_Tag_ID.CLIENT_PATCH_UPDATE:
								{
									send(patch.transferPatchFile());
									break;
								}
								case Message_Tag_ID.GMTOOLS_UPDATELIST_QUERY:
								{
									send(api.UpdateList_Query());
									break;
								}
									//�õ�������Ϣ
								case Message_Tag_ID.DEPART_QUERY:
									send(departInfo.GM_QueryDepartInfo());
									break;
								case Message_Tag_ID.DEPARTMENT_RELATE_QUERY:
									send(departInfo.GM_QueryDepartRelateInfo());
									break;
								case Message_Tag_ID.DEPART_RELATE_GAME_QUERY:
									send(departInfo.GM_QueryDepartRelateGameInfo());
									break;
									//��Ӳ�����Ϣ
								case Message_Tag_ID.DEPARTMENT_CREATE:
									send(departInfo.GM_InsertDepartInfo());
									break;
									//�޸Ĳ�����Ϣ
								case Message_Tag_ID.DEPARTMENT_UPDATE:
									send(departInfo.GM_UpdateDepartInfo());
									break;
									//ɾ��������Ϣ
								case Message_Tag_ID.DEPARTMENT_DELETE:
									send(departInfo.GM_DelDepartInfo());
									break;
									//�����û�
								case Message_Tag_ID.USER_CREATE:
									send(userInfo.GM_InsertUserInfo());break;
									//�޸�����
								case Message_Tag_ID.USER_UPDATE:
									send(userInfo.GM_UpdateUserInfo());break;
									//ɾ���û�
								case Message_Tag_ID.USER_DELETE:
									send(userInfo.GM_DelUserInfo());break;
									//�û���ѯ
								case Message_Tag_ID.USER_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index,mesBody.getTLVByTag(TagName.Index).m_uiValueLen,mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize,mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen,mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(userInfo.GM_QueryList(index-1,pageSize,userByID));
									break;
								}
								case Message_Tag_ID.USER_SYSADMIN_QUERY:
								{
									TLV_Structure tvlUserID = new TLV_Structure(TagName.UserByID, mesBody.getTLVByTag(TagName.UserByID).m_uiValueLen, mesBody.getTLVByTag(TagName.UserByID).m_bValueBuffer);
									int userID = Convert.ToInt32(tvlUserID.toInteger());
									send(userInfo.GM_QuerySysAdminInfo(userID));
									break;
								}
									//�޸������û�״̬
								case Message_Tag_ID.UPDATE_ACTIVEUSER:
								{
									TLV_Structure tvlUserID = new TLV_Structure(TagName.User_ID, mesBody.getTLVByTag(TagName.User_ID).m_uiValueLen, mesBody.getTLVByTag(TagName.User_ID).m_bValueBuffer);
									int userID = Convert.ToInt32(tvlUserID.toInteger());
									send(userInfo.GM_UpdateActiveUserPkg(userID,0));
									break;
								}
									//�û������޸�
								case Message_Tag_ID.USER_PASSWD_MODIF:
									send(userInfo.GM_ModifPassWd());
									break;
									//��ѯ����GM�ʺ���Ϣ
								case Message_Tag_ID.USER_QUERY_ALL:
									send(userInfo.GM_QueryAll(userByID));
									break;
									//ģ���ѯ
								case Message_Tag_ID.MODULE_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index,mesBody.getTLVByTag(TagName.Index).m_uiValueLen,mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize,mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen,mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(moduleInfo.GM_QueryAll(index-1,pageSize));
									break;
								}
									//�û�ģ�����
								case Message_Tag_ID.USER_MODULE_UPDATE:
									send(userModule.GM_UserModuleAdmin());
									break;
									//����UserID��ѯģ��
								case Message_Tag_ID.USER_MODULE_QUERY:
								{
									TLV_Structure tvlUserID = new TLV_Structure(TagName.User_ID,mesBody.getTLVByTag(TagName.User_ID).m_uiValueLen,mesBody.getTLVByTag(TagName.User_ID).m_bValueBuffer);
									int userID =Convert.ToInt32(tvlUserID.toInteger());
									send(userModule.GM_getModuleInfo(userID));
									break;
								}
									//���ģ��
								case Message_Tag_ID.MODULE_CREATE:
									send(moduleInfo.GM_InsertModuleInfo());
									break;
									//�޸�ģ��
								case Message_Tag_ID.MODULE_UPDATE:
									send(moduleInfo.GM_UpdateModuleInfo());
									break;
									//ɾ��ģ��
								case Message_Tag_ID.MODULE_DELETE:
									send(moduleInfo.GM_DelModuleInfo());
									break;
									//��ѯ��Ϸ
								case Message_Tag_ID.GAME_QUERY:
									send(gameInfo.GM_QueryAll());
									break;
									//������Ϸ
								case Message_Tag_ID.GAME_CREATE:
									send(gameInfo.GM_InsertGameInfo());
									break;
									//�޸���Ϸ
								case Message_Tag_ID.GAME_UPDATE:
									send(gameInfo.GM_UpdateGameInfo());
									break;
									//ɾ����Ϸ
								case Message_Tag_ID.GAME_DELETE:
									send(gameInfo.GM_DelGameInfo());
									break;
								case Message_Tag_ID.GAME_MODULE_QUERY:
									send(gameInfo.GM_QueryModuleInfo(1));
									break;
									//��ѯ������ϷIP
								case Message_Tag_ID.SERVERINFO_IP_ALL_QUERY:
									send(api.packServerInfoALLResp());
									break;
								case Message_Tag_ID.SERVERINFO_IP_QUERY:
								{
									send(api.packServerInfoResp());
									break;
								}
									//�����Ϸ������IP
								case Message_Tag_ID.LINK_SERVERIP_CREATE:
								{
									send(api.packCreateServerInfoResp());
									break;
								}
								case Message_Tag_ID.LINK_SERVERIP_DELETE:
								{
									send(api.packDelServerInfoResp());
									break;
								}
								case Message_Tag_ID.GMTOOLS_OperateLog_Query:
								{
									stuct = new TLV_Structure(TagName.Index,mesBody.getTLVByTag(TagName.Index).m_uiValueLen,mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize,mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen,mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(api.UserOperateLog_Query(index-1,pageSize));
									break;
								}
									//ȡ��NTES�ʼ��б�
								case Message_Tag_ID.NOTES_LETTER_TRANSFER:
								{
									stuct = new TLV_Structure(TagName.Index,mesBody.getTLVByTag(TagName.Index).m_uiValueLen,mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize,mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen,mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(notesAPI.Notes_TransferInfo_Resp(index-1,pageSize));
									break;
								}
									//�����ʼ�NOTES
								case Message_Tag_ID.NOTES_LETTER_PROCESS:
									send(notesAPI.Notes_LetterProcess_Resp());
									break;
									//��ǰ�û��õ�ת������NOTES�ʼ�
								case Message_Tag_ID.NOTES_LETTER_TRANSMIT:
								{
									stuct = new TLV_Structure(TagName.UserByID,mesBody.getTLVByTag(TagName.UserByID).m_uiValueLen,mesBody.getTLVByTag(TagName.UserByID).m_bValueBuffer);
									int userbyID =Convert.ToInt32(stuct.toInteger());
									stuct = new TLV_Structure(TagName.Index,mesBody.getTLVByTag(TagName.Index).m_uiValueLen,mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize,mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen,mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(notesAPI.Notes_TransmitInfo_Resp(userbyID,index-1,pageSize));
									break;
								}
//									//�ͽ�����������Ϣ��ѯ
//								case Message_Tag_ID.MJ_CHARACTERINFO_QUERY:
//								{
//									send(mjcharacterAPI.MJ_CharacterInfo_Query());
//									break;
//								}
//									//�ͽ��ʺ���Ϣ��ѯ
//								case Message_Tag_ID.MJ_ACCOUNT_QUERY:
//								{
//									send(mjAccountAPI.MJAccount_Query());
//									break;
//								}
//
//									//�����ͽ��ʺ�
//								case Message_Tag_ID.MJ_ACCOUNT_LOCAL_CREATE:
//								{
//									send(mjAccountAPI.InsertLocalAccount());
//									break;
//								}
//									//ɾ���ͽ��ʺ�
//								case Message_Tag_ID.MJ_ACCOUNT_REMOTE_DELETE:
//								{
//									send(mjAccountAPI.DelRemoteAccount());
//									break;
//								}
//									//����ʺ�
//								case Message_Tag_ID.MJ_ACCOUNT_REMOTE_RESTORE:
//								{
//									send(mjAccountAPI.RestoreRemoteAccount());
//									break;
//								}
//									//��ѯ���汾���ͽ��ʺ�
//								case Message_Tag_ID.MJ_ACCOUNT_LOCAL_QUERY:
//								{
//									send(mjAccountAPI.MJLocalAccount_Query());
//									break;
//								}
//									//���뱣��
//								case Message_Tag_ID.MJ_ACCOUNTPASSWD_LOCAL_CREATE:
//								{
//									send(mjAccountAPI.AccountPwd_Insert());
//									break;
//								}
//									//�����޸�
//								case Message_Tag_ID.MJ_ACCOUNTPASSWD_REMOTE_UPDATE:
//								{
//									send(mjAccountAPI.AccountPwd_Update());
//									break;
//								}
//									//����ָ�
//								case Message_Tag_ID.MJ_ACCOUNTPASSWD_REMOTE_RESTORE:
//								{
//									send(mjAccountAPI.AccountPwd_Restore());
//									break;
//								}
//									//���ݽ�Ǯ����
//								case Message_Tag_ID.MJ_MONEYSORT_QUERY:
//								{
//									send(mjcharacterAPI.MJ_Sort_MoneyAll());
//									break;
//								}
//									//���ݵȼ�����
//								case Message_Tag_ID.MJ_LEVELSORT_QUERY:
//								{
//									send(mjcharacterAPI.MJ_Sort_LevelAll());
//									break;
//								}
//									//���ݲ�ְͬҵ��Ǯ����
//								case Message_Tag_ID.MJ_MONEYFIGHTERSORT_QUERY:
//								{
//									send(mjcharacterAPI.MJ_Sort_MoneyFighter());
//									break;
//								}
//									//��ѯսʿ�ȼ�ǰ100��
//								case Message_Tag_ID.MJ_LEVELFIGHTERSORT_QUERY:
//								{
//									send(mjcharacterAPI.MJ_Sort_LevelFighter());
//									break;
//								}
//									//��ѯ��ʦ��Ǯǰ100��
//								case Message_Tag_ID.MJ_MONEYRABBISORT_QUERY:
//								{
//									send(mjcharacterAPI.MJ_Sort_MoneyRabbi());
//									break;
//								}
//									//��ѯ��ʦ�ȼ�ǰ100��
//								case Message_Tag_ID.MJ_LEVELRABBISORT_QUERY:
//								{
//									send(mjcharacterAPI.MJ_Sort_LevelRabbi());
//									break;
//								}
//									//��ѯ��ʿ��Ǯǰ100��
//								case Message_Tag_ID.MJ_MONEYTAOISTSORT_QUERY:
//								{
//									send(mjcharacterAPI.MJ_Sort_MoneyTaois());
//									break;
//								}
//									//��ѯ��ʿ�ȼ�ǰ100��
//								case Message_Tag_ID.MJ_LEVELTAOISTSORT_QUERY:
//								{
//									send(mjcharacterAPI.MJ_Sort_LevelTaois());
//									break;
//								}
//									//�����û����׼�¼
//								case Message_Tag_ID.MJ_ITEMLOG_QUERY:
//								{
//									send(mjlogAPI.MJ_ItemLog_Query());
//									break;
//								}
									//�鿴����������Ҵ�������״̬
								case Message_Tag_ID.SDO_ACCOUNT_QUERY:
								{
									send(accountAPI.SDOMemberInfo_Query());
									break;
								}
								case Message_Tag_ID.SDO_EMAIL_QUERY:
								{
									send(sdocharacterAPI.SDOEmailQuery());
									break;
								}
								case Message_Tag_ID.SDO_PASSWORD_RECOVERY:
								{
									send(sdocharacterAPI.sendEmailPasswdMsg());
									break;
								}
									//�鿴�������������������
								case Message_Tag_ID.SDO_CHARACTERINFO_QUERY:
								{
									send(sdocharacterAPI.SDOCharInfo_Query());
									break;
								}
									//�޸ĳ������������������
								case Message_Tag_ID.SDO_CHARACTERINFO_UPDATE:
								{
									send(sdocharacterAPI.SDOCharacterInfo_Update());
									break;
								}
									//�鿴��Ϸ����ĵ�����Ϣ
								case Message_Tag_ID.SDO_ITEMSHOP_QUERY:
								{
									send(itemShopAPI.itemShop_QueryALL());
									break;
								}
									//�鿴������ϵĵ���
								case Message_Tag_ID.SDO_ITEMSHOP_BYOWNER_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(itemShopAPI.itemShop_Owner_Query(index-1,pageSize));
									break;
								}
									//ɾ��������ϵĵ���
								case Message_Tag_ID.SDO_ITEMSHOP_DELETE:
								{
									send(itemShopAPI.ItemShop_Delete());
									break;
								}
									//�鿴�������е���
								case Message_Tag_ID.SDO_GIFTBOX_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(itemShopAPI.giftBox_Query(index-1,pageSize));
									break;
								}
									
									//������������ϵ���
								case Message_Tag_ID.SDO_GIFTBOX_CREATE:
								{
									send(itemShopAPI.GiftBox_MessageItem_Add());
									break;
								}
									//ɾ�����������ϵ���
								case Message_Tag_ID.SDO_GIFTBOX_DELETE:
								{
									send(itemShopAPI.GiftBox_MessageItem_Delete());
									break;
								}
									//�鿴��ҵ�¼״̬
								case Message_Tag_ID.SDO_USERLOGIN_STATUS_QUERY:
								{
									send(accountAPI.SDO_login_Query());
									break;
								}
									//�鿴��������߼�¼
								case Message_Tag_ID.SDO_USERONLINE_QUERY:
								{
									send(itemShopAPI.UserOnline_Query());
									break;
								}
									//�鿴��ҽ��׼�¼
								case Message_Tag_ID.SDO_ITEMSHOP_TRADE_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(itemShopAPI.UserTrade_Query(index-1,pageSize));
									break;
								}
									//�鿴������Ѽ�¼
								case Message_Tag_ID.SDO_CONSUME_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(itemShopAPI.UserConsume_Query(index-1,pageSize));
									break;
								}
									//�鿴������Ѽ�¼�ϼ�
								case Message_Tag_ID.SDO_USERCONSUMESUM_QUERY:
								{
									send(itemShopAPI.UserConsume_QuerySum());
									break;
								}
								case Message_Tag_ID.SDO_DAYSLIMIT_QUERY:
								{
									send(itemShopAPI.ItemLimit_Query());
									break;
								}
									//�鿴��ңǱ�
								case Message_Tag_ID.SDO_USERGCASH_QUERY:
								{
									send(ItemlogAPI.userGCash_Query());
									break;
								}
									//�鿴���б�ͣ����ʺ�
								case Message_Tag_ID.SDO_MEMBERBANISHMENT_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(accountAPI.SDOMember_banishment_QueryAll(index-1,pageSize));
									break;
								}
									//�鿴���ر�ͣ����ʺ�
								case Message_Tag_ID.SDO_MEMBERLOCAL_BANISHMENT:
								{
									send(accountAPI.SDOBanishmentLocal_Query());
									break;
								}
									//�鿴ĳ������ʺ�ͣ��״̬
								case Message_Tag_ID.SDO_MEMBERSTOPSTATUS_QUERY:
								{
									send(accountAPI.SDOMember_banishment_Query());
									break;
								}
									//��ͣ�ʺ�
								case Message_Tag_ID.SDO_ACCOUNT_CLOSE:
								{
									send(accountAPI.SDOMemberClose_Update());
									break;
								}
									//����ʺ�
								case Message_Tag_ID.SDO_ACCOUNT_OPEN:
								{
									send(accountAPI.SDOMemberOpen_Update());
									break;
								}
									//��ֵ��ϸ
								case Message_Tag_ID.SDO_USERMCASH_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(ItemlogAPI.userChargeDetail_Query(index-1,pageSize));
									break;
								}
									//��ֵ��ϸ�ϼ�
								case Message_Tag_ID.SDO_USERCHARAGESUM_QUERY:
								{
									send(ItemlogAPI.userChargeSum_Query());
									break;
								}
									//����G��
								case Message_Tag_ID.SDO_USERGCASH_UPDATE:
								{
									send(ItemlogAPI.SDO_GCash_Update());
									break;
								}
								case Message_Tag_ID.SDO_CHALLENGE_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(challenge.SDO_Challenge_Query(index-1,pageSize));
									break;
								}
								case Message_Tag_ID.SDO_MUSICDATA_QUERY:
								{
									send(challenge.SDO_MusicData_Query());
									break;
								}
								case Message_Tag_ID.SDO_MUSICDATA_OWN_QUERY:
								{
									send(challenge.SDO_MusicData_SingleQuery());
									break;
								}
								case Message_Tag_ID.SDO_CHALLENGE_INSERT:
								{
									send(challenge.SDOChallengeInfo_Insert());
									break;
								}
								case Message_Tag_ID.SDO_CHALLENGE_UPDATE:
								{
									send(challenge.SDOChallengeInfo_Update());
									break;
								}
								case Message_Tag_ID.SDO_CHALLENGE_DELETE:
								{
									send(challenge.Challenge_Delete());
									break;
								}
								case Message_Tag_ID.SDO_CHALLENGE_SCENE_QUERY:
								{
									send(challenge.SDO_SceneList_Query());
									break;
								}
								case Message_Tag_ID.SDO_CHALLENGE_SCENE_CREATE:
								{
									send(challenge.SDOChallengeSceneInfo_Insert());
									break;
								}
								case Message_Tag_ID.SDO_CHALLENGE_SCENE_UPDATE:
								{
									send(challenge.SDOChallengeSceneInfo_Update());
									break;
								}
								case Message_Tag_ID.SDO_CHALLENGE_SCENE_DELETE:
								{
									send(challenge.SDOChallengeSceneInfo_Delete());
									break;
								}
								case Message_Tag_ID.SDO_MEDALITEM_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(challenge.SDO_Medalite_Query(index-1,pageSize));
									break;
								}
								case Message_Tag_ID.SDO_MEDALITEM_OWNER_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(challenge.SDO_Medalite_Owner_Query(index-1,pageSize));
									break;
								}
								case Message_Tag_ID.SDO_MEDALITEM_CREATE:
								{
									send(challenge.SDOMedalitem_Insert());
									break;
								}
								case Message_Tag_ID.SDO_MEDALITEM_UPDATE:
								{
									send(challenge.SDOMedalitemInfo_Update());
									break;
								}
								case Message_Tag_ID.SDO_MEDALITEM_DELETE:
								{
									send(challenge.SDOMedalitemInfo_Delete());
									break;
								}
								case Message_Tag_ID.SDO_MEMBERDANCE_CLOSE:
								{
									send(accountAPI.SDOMemberDanceClose_Update());
									break;
								}
								case Message_Tag_ID.SDO_MEMBERDANCE_OPEN:
								{
									send(accountAPI.SDOMemberDanceOpen_Update());
									break;
								}
								case Message_Tag_ID.SDO_USERNICK_UPDATE:
								{
									send(accountAPI.SDOUserNick_Update());
									break;
								}
								case Message_Tag_ID.SDO_PADKEYWORD_QUERY:
								{
									send(accountAPI.SDO_PADKeyWord_Query());
									break;
								}
								case Message_Tag_ID.SDO_CHANNELLIST_QUERY:
								{
									send(noticeInfo.ChannelList_Query());
									break;
								}
								case Message_Tag_ID.SDO_ALIVE_REQ:
								{
									send(noticeInfo.sendAlive_Req());
									break;
								}
								case Message_Tag_ID.SDO_BOARDMESSAGE_REQ:
								{
									send(noticeInfo.TaskList_OnwerQuery());
									break;
								}
								case Message_Tag_ID.SDO_BOARDTASK_INSERT:
								{
									send(noticeInfo.TaskList_Insert());
									break;
								}
								case Message_Tag_ID.SDO_BOARDTASK_UPDATE:
								{
									send(noticeInfo.TaskList_Update());
									break;
								}
								case Message_Tag_ID.SDO_BOARDTASK_QUERY:
								{
									send(noticeInfo.TaskList_Query());
									break;
								}
								case Message_Tag_ID.AU_ACCOUNT_QUERY:
								{
									send(auMemberAPI.Audition_Account_Query());
									break;
								}
								case Message_Tag_ID.AU_ACCOUNTLOCAL_QUERY:
								{
									send(auMemberAPI.Audition_BanishmentLocal_Query());
									break;
								}
								case Message_Tag_ID.AU_ACCOUNTREMOTE_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(auMemberAPI.Audition_banishment_QueryAll(index-1,pageSize));
									break;
								}
								case Message_Tag_ID.AU_ACCOUNT_BANISHMENT_QUERY:
								{
									send(auMemberAPI.Audition_banishment_Query());
									break;
								}
								case Message_Tag_ID.AU_ACCOUNT_CLOSE:
								{
									send(auMemberAPI.Audition_AccountClose_Update());
									break;
								}
								case Message_Tag_ID.AU_ACCOUNT_OPEN:
								{
									send(auMemberAPI.Audition_AccountOpen_Update());
									break;
								}
								case Message_Tag_ID.CR_ACCOUNTACTIVE_QUERY:
								{
									send(crAccountAPI.CR_AccountActive_Query());
									break;
								}
								case Message_Tag_ID.CR_ACCOUNT_QUERY:
								{
									send(crAccountAPI.CR_Account_Query());
									break;
								}
								case Message_Tag_ID.CR_CALLBOARD_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(crCallBoardAPI.CR_CallBoard_Query(index-1,pageSize));
									break;
								}
								case Message_Tag_ID.CR_CALLBOARD_CREATE:
								{
									send(crCallBoardAPI.CR_CallBoardInfo_Insert());
									break;
								}
								case Message_Tag_ID.CR_ERRORCHANNEL_QUERY:
								{
									send(crCallBoardAPI.CR_ErrorChannel_Insert());
									break;
								}
								case Message_Tag_ID.CR_CALLBOARD_UPDATE:
								{
									send(crCallBoardAPI.CR_CallBoardInfo_Update());
									break;
								}
								case Message_Tag_ID.CR_CALLBOARD_DELETE:
								{
									send(crCallBoardAPI.CR_CallBoardInfo_Delete());
									break;
								}
								case Message_Tag_ID.CR_CHARACTERINFO_QUERY:
								{
									send(crCharacterInfoAPI.CR_CharacterInfo_Query());
									break;
								}
								case Message_Tag_ID.CR_CHARACTERINFO_UPDATE:
								{
									send(crCharacterInfoAPI.CR_CharacterInfo_Update());
									break;
								}
								case Message_Tag_ID.CR_CHANNEL_QUERY:
								{
									send(crCallBoardAPI.CR_Channel_QueryAll());
									break;
								}
								case Message_Tag_ID.CR_NICKNAME_QUERY:
								{
									send(crCharacterInfoAPI.CR_NickName_Update());
									break;
								}
								case Message_Tag_ID.CR_LOGIN_LOGOUT_QUERY:
								{
									send(crCharacterInfoAPI.CR_Login_Logout_Query());
									break;
								}
								case Message_Tag_ID.AU_ITEMSHOP_TRADE_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(avatarListAPI.UserTrade_Query(index-1,pageSize));
									break;
								}
								case Message_Tag_ID.AU_CONSUME_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(avatarListAPI.UserConsume_Query(index - 1, pageSize));
									break;
								}
								case Message_Tag_ID.AU_USERCONSUMESUM_QUERY:
								{
									send(avatarListAPI.UserConsume_QuerySum());
									break;
								}
								case Message_Tag_ID.AU_USERCHARAGESUM_QUERY:
								{
									send(avatarListAPI.UserTrade_QuerySum());
									break;
								}

								case Message_Tag_ID.AU_CHARACTERINFO_QUERY:
								{
									send(characterInfoAPI.AuditionCharInfo_Query());
									break;
								}
								case Message_Tag_ID.AU_LEVELEXP_QUERY:
								{
									send(characterInfoAPI.AuditionLevelExp_Query());
									break;
								}

								case Message_Tag_ID.AU_CHARACTERINFO_UPDATE:
								{
									send(characterInfoAPI.AuditionCharacterInfo_Update());
									break;
								}
								case Message_Tag_ID.AU_ITEMSHOP_BYOWNER_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(avatarListAPI.AvatarList_Owner_Query(index-1,pageSize));
									break;
								}
								case Message_Tag_ID.AU_ITEMSHOP_QUERY:
								{
									send(avatarListAPI.AvatarList_QueryALL());
									break;
								}
								case Message_Tag_ID.AU_ITEMSHOP_CREATE:
								{
									send(avatarListAPI.AvatarList_BatchInsert());
									break;
								}
								case Message_Tag_ID.AU_ITEMSHOP_DELETE:
								{
									send(avatarListAPI.AvatarList_Delete());
									break;
								}
									//һ��ͨ�����п���ѯ
								case Message_Tag_ID.CARD_USERCHARGEDETAIL_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(cardDetailInfoAPI.CardDetailInfo_Query(index - 1, pageSize));
									break;
								}
									//һ��ͨ�����п����Ѳ�ѯ
								case Message_Tag_ID.CARD_USERCONSUME_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(cardDetailInfoAPI.CardUserConsume_Query(index - 1, pageSize));
									break;
								}
								case Message_Tag_ID.CARD_USERDETAIL_SUM_QUERY:
								{
									send(cardDetailInfoAPI.CardDetailInfo_QuerySum());
									break;
								}
								case Message_Tag_ID.CARD_USERCONSUME_SUM_QUERY:
								{
									send(cardDetailInfoAPI.CardUserConsumeInfo_QuerySum());
									break;
								}
								case Message_Tag_ID.CARD_USERNICK_QUERY:
								{
									send(cardDetailInfoAPI.UserNick_Query());
									break;
								}
								case Message_Tag_ID.AU_USERNICK_UPDATE:
								{
									send(auMemberAPI.Audition_NickName_Update());
									break;
								}
									//����������֤��Ϣ
								case Message_Tag_ID.CARD_USERINFO_QUERY:
								{
									send(cardDetailInfoAPI.UserInfo_Query());
									break;
								}
									//������Ұ�ȫ����Ϣ
								case Message_Tag_ID.CARD_USERINFO_CLEAR:
								{
									send(cardDetailInfoAPI.CardUserInfo_Clear());
									break;
								}
								case Message_Tag_ID.CARD_USERSECURE_CLEAR:
								{
									send(cardDetailInfoAPI.CardUserSecure_Clear());
									break;
								}
								case Message_Tag_ID.CARD_USERLOCK_UPDATE:
								{
									send(cardDetailInfoAPI.MemberInfo_Lock());
									break;
								}
									//�鿴���G�ҹ�����Ϣ
								case Message_Tag_ID.AUSHOP_USERGPURCHASE_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(userCashPurchaseAPI.UserGCashPurchase_Query(index-1,pageSize));
									break;
								}
									//�鿴���G�ҹ����¼�ϼ�
								case Message_Tag_ID.AUSHOP_USERGPURCHASE_SUM_QUERY:
								{
									send(userCashPurchaseAPI.UserGCashPurchase_QuerySum());
									break;
								}

									//�鿴���M�ҹ�����Ϣ
								case Message_Tag_ID.AUSHOP_USERMPURCHASE_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(userCashPurchaseAPI.UserMCashPurchase_Query(index-1,pageSize));
									break;
								}
									//�鿴���M�ҹ����¼�ϼ�
								case Message_Tag_ID.AUSHOP_USERMPURCHASE_SUM_QUERY:
								{
									send(userCashPurchaseAPI.UserMCashPurchase_QuerySum());
									break;
								}
									//�鿴��һ���
								case Message_Tag_ID.AUSHOP_USERINTERGRAL_QUERY:
								{
									send(userCashPurchaseAPI.UserIntegral_Query());
									break;
								}
									//���߻��նһ���¼
								case Message_Tag_ID.AUSHOP_AVATARECOVER_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(userCashPurchaseAPI.UserAvatarItemRev_Query(index-1,pageSize));
									break;
								}
									//���߻��նһ���ϸ��¼
								case Message_Tag_ID.AUSHOP_AVATARECOVER_DETAIL_QUERY:
								{
									send(userCashPurchaseAPI.UserAvatarItemRevDetail_Query());
									break;

								}
									//��������ҽ�ɫ��Ϣ��ѯ
								case Message_Tag_ID.O2JAM_CHARACTERINFO_QUERY:
								{
									send(o2jamCharacterAPI.O2JAMCharInfo_Query());
									break;
								}
									//��������ҽ�ɫ��Ϣ����
								case Message_Tag_ID.O2JAM_CHARACTERINFO_UPDATE:
								{
									send(o2jamCharacterAPI.O2JAMCharacterInfo_Update());
									break;
								}
									//���������������Ϣ
								case Message_Tag_ID.O2JAM_CONSUME_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(o2itemShop.UserConsume_Query(index-1,pageSize));
									break;
								}
								case Message_Tag_ID.O2JAM_CONSUME_SUM_QUERY:
								{
									send(o2itemShop.UserConsume_QuerySum());
									break;
								}
								case Message_Tag_ID.O2JAM_GIFTBOX_QUERY:
								{
									send(o2itemShop.giftBox_Query());
									break;
								}
								case Message_Tag_ID.O2JAM_ITEM_QUERY:
								{
									send(o2itemShop.itemShop_Owner_Query());
									break;
								}
								case Message_Tag_ID.O2JAM_ITEMNAME_QUERY:
								{
									send(o2itemShop.ItemName_Query());
									break;
								}
								case Message_Tag_ID.O2JAM_ITEM_UPDATE:
								{
									send(o2itemShop.ItemShop_Delete());
									break;
								}
								case Message_Tag_ID.O2JAM_ITEMDATA_QUERY:
								{
									send(o2itemShop.itemShop_QueryALL());
									break;
								}
								case Message_Tag_ID.O2JAM_GIFTBOX_CREATE:
								{
									send(o2itemShop.GiftBox_MessageItem_Add());
									break;
								}
								case Message_Tag_ID.O2JAM_GIFTBOX_DELETE:
								{
									send(o2itemShop.GiftBox_MessageItem_Delete());
									break;
								}
								case Message_Tag_ID.O2JAM2_ACCOUNTACTIVE_QUERY:
								{
									send(o2jam2Account.O2JAM2_AccountActive_Query());
									break;
								}
								case Message_Tag_ID.O2JAM2_CHARACTERINFO_QUERY:
								{
									send(o2jam2CharacterInfo.O2JAM2CharInfo_Query());
									break;
								}
								case Message_Tag_ID.O2JAM2_CHARACTERINFO_UPDATE:
								{
									send(o2jam2CharacterInfo.O2JAM2CharacterInfo_Update());
									break;
								}
								case Message_Tag_ID.O2JAM2_LEVELEXP_QUERY:
								{
									send(o2jam2CharacterInfo.Baf_LevelExp_Query());
									break;
								}
								case Message_Tag_ID.O2JAM2_AVATORLIST_QUERY:
								{
									send(o2jam2ItemShopAPI.itemShop_Owner_Query());
									break;
								}
								case Message_Tag_ID.O2JAM2_ITEMSHOP_DELETE:
								{
									send(o2jam2ItemShopAPI.ItemShop_Delete());
									break;
								}
								case Message_Tag_ID.O2JAM2_CONSUME_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(o2jam2ItemShopAPI.UserConsume_Query(index-1,pageSize));
									break;
								}
								case Message_Tag_ID.O2JAM2_CONSUME_SUM_QUERY:
								{
									send(o2jam2ItemShopAPI.UserConsume_QuerySum());
									break;
								}
								case Message_Tag_ID.O2JAM2_MEMBERBANISHMENT_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(o2jam2Account.O2JAM2Member_banishment_QueryAll(index-1,pageSize));
									break;
								}
								case Message_Tag_ID.O2JAM2_ACCOUNT_CLOSE:
								{
									send(o2jam2Account.O2JAM2MemberClose_Update());
									break;
								}
								case Message_Tag_ID.O2JAM2_MEMBERLOCAL_BANISHMENT:
								{
									send(o2jam2Account.O2JAM2BanishmentLocal_Query());
									break;
								}
								case Message_Tag_ID.O2JAM2_ACCOUNT_OPEN:
								{
									send(o2jam2Account.O2JAM2MemberOpen_Update());
									break;
								}
								case Message_Tag_ID.O2JAM2_ITEMSHOP_QUERY:
								{
									send(o2jam2ItemShopAPI.itemShop_QueryALL());
									break;
								}
								case Message_Tag_ID.O2JAM2_MESSAGE_CREATE:
								{
									send(o2jam2ItemShopAPI.GiftBox_MessageItem_Add());
									break;
								}
								case Message_Tag_ID.O2JAM2_MESSAGE_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(o2jam2ItemShopAPI.giftBox_Query(index-1,pageSize));
									break;
								}
								case Message_Tag_ID.O2JAM2_MESSAGE_DELETE:
								{
									send(o2jam2ItemShopAPI.GiftBox_MessageItem_Delete());
									break;
								}
								case Message_Tag_ID.O2JAM2_USERLOGIN_DELETE:
								{
									send(o2jam2Account.O2JAM2UserLogin_Delete());
									break;
								}
								case Message_Tag_ID.SOCCER_CHARACTERINFO_QUERY:
								{
									send(soccercharacterAPI.Soccer_Characterinfo_Query());
									break;
								}
								case Message_Tag_ID.SOCCER_CHARPOINT_QUERY:
								{
									send(soccercharacterAPI.Soccer_Gamepoint_Modify());
									break;
								}
								case Message_Tag_ID.SOCCER_ACCOUNTSTATE_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(soccercharacterAPI.Soccer_AccountState_Query(index-1,pageSize));
									break;
								}
								case Message_Tag_ID.SOCCER_CHARACTERSTATE_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(soccercharacterAPI.Soccer_CharacterState_Query(index-1,pageSize));
									break;
								}
								case Message_Tag_ID.SOCCER_ACCOUNTSTATE_MODIFY:
								{
									send(soccercharacterAPI.AccountState_Modify());
									break;
								}
								case Message_Tag_ID.SOCCER_CHARACTERSTATE_MODIFY:
								{
									send(soccercharacterAPI.CharacterState_Modify());
									break;
								}
								case Message_Tag_ID.SOCCER_DELETEDCHARACTERINFO_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(soccercharacterAPI.Soccer_DeletedCharacterinfo_Query(index-1,pageSize));
									break;
								}
								case Message_Tag_ID.SOCCER_CHARCHECK_QUERY:
								{
									send(soccercharacterAPI.Soccer_CharCheck());
									break;
								}
								case Message_Tag_ID.SOCCER_CHARITEMS_RECOVERY_QUERY:
								{
									send(soccercharacterAPI.Soccer_CharItems_Recovery());
									break;
								}
                                   
							}
						}
					}
					Thread.Sleep(1000);
				}
			}
			catch  (IOException ) 
			{
				if(ContinueProcess==false)
				{
					UserInfoAPI userAPI = new UserInfoAPI();
					userAPI.GM_UpdateActiveUser(userByID,0);
					ContinueProcess=true;
					if(userByID>0)
					{
						if(userByID!=99999)
						{
							Console.Write(lg.ServerSocket_Handler_User+name+lg.ServerSocket_Handler_UserLeft+"\n");
						}
					}
				}
			}	  
			catch  ( SocketException se ) 
			{
				Console.Write("Client Disconnected.");
				if(se.ErrorCode == 10054)
				{
					Console.WriteLine("Client Disconnected..");
				}
				else
				{
					Console.WriteLine(se.Message );
				}
				networkStream.Close() ;
				svrSocket.Close();			
				ContinueProcess = false ; 
				Console.WriteLine( "Conection is broken!");
			}
			mut.ReleaseMutex();

		}

		/// <summary>
		/// ���߳��û�������Ϣ
		/// </summary>
		/// <param name="msg">��Ϣ��</param>
		public void send(Message msg)
		{
			byte[] sendBytes;
			sendBytes=msg.m_bMessageBuffer;
			try
			{
                if (networkStream.CanWrite)
                {
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                }
			}
			catch(SocketException ex)
			{
				Console.WriteLine(ex.Message);
				networkStream.Close();
			}

		}
		/// <summary>
		/// ���߳��û�������Ϣ
		/// </summary>
		/// <param name="msg">��Ϣ��</param>
		public void send(byte[] msg)
		{
			try
			{
                if (networkStream.CanWrite)
                {
                    networkStream.Write(msg, 0, msg.Length);
                }
			}
			catch(SocketException ex)
			{
				Console.WriteLine(ex.Message);
				networkStream.Close();
			}

		}
		/// <summary>
		/// /���߳��û�������Ϣ
		/// </summary>
		/// <returns>���ܵ���Ϣ��</returns>
		public byte[] receive()
		{
			byte[] recvBytes = new byte[128];
			try
			{
				networkStream.Read(recvBytes,0,recvBytes.Length);
			}
			catch(SocketException ex)
			{
				Console.WriteLine(ex.Message);
				networkStream.Close();
			}
			return recvBytes;
            
		}
		/// <summary>
		/// �����ڵ��߳�ѭ�����ܿͻ��˷��͵���Ϣ
		/// </summary>
		public void WorkerThread()
		{
			while(networkStream.CanRead)
			{
				byte[] BytesRead ;
				BytesRead=receive();
				Console.WriteLine(Encoding.Default.GetString(BytesRead));


			}

		}
		public void Close() 
		{
			networkStream.Close() ;
			svrSocket.Close();        
		}
        
		public  bool Alive 
		{
			get 
			{
				return  ContinueProcess ;
			}
		}
		public int HandlerType
		{
			get{return this.handlerType ;}
			set{this.handlerType=value;}
		}
	}
}
