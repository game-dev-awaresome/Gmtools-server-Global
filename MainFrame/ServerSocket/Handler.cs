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
	/// Handler 的摘要说明。
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
		/// 单个客户端连接进入以后，服务器端接受和发送消息的处理事务
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
								//响应连接请求
								CommonAPI api = new CommonAPI(name,passwd,mac,connTime,"PASS");
								send(api.packConnectResp());
							}
							else if(msg.GetMessageID() ==Message_Tag_ID.ACCOUNT_AUTHOR)
							{
								//用户验证请求
								validate = new UserValidate(name,passwd,mac);
								send(validate.validateUser());
								userByID = validate.UserByID;
								status = validate.Status;
							}
							else if(msg.GetMessageID() == Message_Tag_ID.DISCONNECT)
							{
								//响应断开请求
								TLV_Structure stuct = new TLV_Structure(TagName.UserByID,mesBody.getTLVByTag(TagName.UserByID).m_uiValueLen,mesBody.getTLVByTag(TagName.UserByID).m_bValueBuffer);
								userByID = (int)stuct.toInteger();
								CommonAPI api = new CommonAPI(userByID,"PASS",bytes);
								UserInfoAPI userInfo = new UserInfoAPI();
								userInfo.GM_UpdateActiveUser(userByID,0);
								send(api.packConnectResp());
							}
						}
						//验证通过
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
									//客户端比较请求
								case Message_Tag_ID.CLIENT_PATCH_COMPARE:
								{
									send(patch.encodeMessage());
									break;
								}
									//客户端更新请求
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
									//得到部门信息
								case Message_Tag_ID.DEPART_QUERY:
									send(departInfo.GM_QueryDepartInfo());
									break;
								case Message_Tag_ID.DEPARTMENT_RELATE_QUERY:
									send(departInfo.GM_QueryDepartRelateInfo());
									break;
								case Message_Tag_ID.DEPART_RELATE_GAME_QUERY:
									send(departInfo.GM_QueryDepartRelateGameInfo());
									break;
									//添加部门信息
								case Message_Tag_ID.DEPARTMENT_CREATE:
									send(departInfo.GM_InsertDepartInfo());
									break;
									//修改部门信息
								case Message_Tag_ID.DEPARTMENT_UPDATE:
									send(departInfo.GM_UpdateDepartInfo());
									break;
									//删除部门信息
								case Message_Tag_ID.DEPARTMENT_DELETE:
									send(departInfo.GM_DelDepartInfo());
									break;
									//创建用户
								case Message_Tag_ID.USER_CREATE:
									send(userInfo.GM_InsertUserInfo());break;
									//修改密码
								case Message_Tag_ID.USER_UPDATE:
									send(userInfo.GM_UpdateUserInfo());break;
									//删除用户
								case Message_Tag_ID.USER_DELETE:
									send(userInfo.GM_DelUserInfo());break;
									//用户查询
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
									//修改在线用户状态
								case Message_Tag_ID.UPDATE_ACTIVEUSER:
								{
									TLV_Structure tvlUserID = new TLV_Structure(TagName.User_ID, mesBody.getTLVByTag(TagName.User_ID).m_uiValueLen, mesBody.getTLVByTag(TagName.User_ID).m_bValueBuffer);
									int userID = Convert.ToInt32(tvlUserID.toInteger());
									send(userInfo.GM_UpdateActiveUserPkg(userID,0));
									break;
								}
									//用户密码修改
								case Message_Tag_ID.USER_PASSWD_MODIF:
									send(userInfo.GM_ModifPassWd());
									break;
									//查询所有GM帐号信息
								case Message_Tag_ID.USER_QUERY_ALL:
									send(userInfo.GM_QueryAll(userByID));
									break;
									//模块查询
								case Message_Tag_ID.MODULE_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index,mesBody.getTLVByTag(TagName.Index).m_uiValueLen,mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize,mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen,mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(moduleInfo.GM_QueryAll(index-1,pageSize));
									break;
								}
									//用户模块分配
								case Message_Tag_ID.USER_MODULE_UPDATE:
									send(userModule.GM_UserModuleAdmin());
									break;
									//根据UserID查询模块
								case Message_Tag_ID.USER_MODULE_QUERY:
								{
									TLV_Structure tvlUserID = new TLV_Structure(TagName.User_ID,mesBody.getTLVByTag(TagName.User_ID).m_uiValueLen,mesBody.getTLVByTag(TagName.User_ID).m_bValueBuffer);
									int userID =Convert.ToInt32(tvlUserID.toInteger());
									send(userModule.GM_getModuleInfo(userID));
									break;
								}
									//添加模块
								case Message_Tag_ID.MODULE_CREATE:
									send(moduleInfo.GM_InsertModuleInfo());
									break;
									//修改模块
								case Message_Tag_ID.MODULE_UPDATE:
									send(moduleInfo.GM_UpdateModuleInfo());
									break;
									//删除模块
								case Message_Tag_ID.MODULE_DELETE:
									send(moduleInfo.GM_DelModuleInfo());
									break;
									//查询游戏
								case Message_Tag_ID.GAME_QUERY:
									send(gameInfo.GM_QueryAll());
									break;
									//创建游戏
								case Message_Tag_ID.GAME_CREATE:
									send(gameInfo.GM_InsertGameInfo());
									break;
									//修改游戏
								case Message_Tag_ID.GAME_UPDATE:
									send(gameInfo.GM_UpdateGameInfo());
									break;
									//删除游戏
								case Message_Tag_ID.GAME_DELETE:
									send(gameInfo.GM_DelGameInfo());
									break;
								case Message_Tag_ID.GAME_MODULE_QUERY:
									send(gameInfo.GM_QueryModuleInfo(1));
									break;
									//查询所有游戏IP
								case Message_Tag_ID.SERVERINFO_IP_ALL_QUERY:
									send(api.packServerInfoALLResp());
									break;
								case Message_Tag_ID.SERVERINFO_IP_QUERY:
								{
									send(api.packServerInfoResp());
									break;
								}
									//添加游戏服务器IP
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
									//取得NTES邮件列表
								case Message_Tag_ID.NOTES_LETTER_TRANSFER:
								{
									stuct = new TLV_Structure(TagName.Index,mesBody.getTLVByTag(TagName.Index).m_uiValueLen,mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize,mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen,mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(notesAPI.Notes_TransferInfo_Resp(index-1,pageSize));
									break;
								}
									//处理邮件NOTES
								case Message_Tag_ID.NOTES_LETTER_PROCESS:
									send(notesAPI.Notes_LetterProcess_Resp());
									break;
									//当前用户得到转发给他NOTES邮件
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
//									//猛将人物资料信息查询
//								case Message_Tag_ID.MJ_CHARACTERINFO_QUERY:
//								{
//									send(mjcharacterAPI.MJ_CharacterInfo_Query());
//									break;
//								}
//									//猛将帐号信息查询
//								case Message_Tag_ID.MJ_ACCOUNT_QUERY:
//								{
//									send(mjAccountAPI.MJAccount_Query());
//									break;
//								}
//
//									//保存猛将帐号
//								case Message_Tag_ID.MJ_ACCOUNT_LOCAL_CREATE:
//								{
//									send(mjAccountAPI.InsertLocalAccount());
//									break;
//								}
//									//删除猛将帐号
//								case Message_Tag_ID.MJ_ACCOUNT_REMOTE_DELETE:
//								{
//									send(mjAccountAPI.DelRemoteAccount());
//									break;
//								}
//									//解封帐号
//								case Message_Tag_ID.MJ_ACCOUNT_REMOTE_RESTORE:
//								{
//									send(mjAccountAPI.RestoreRemoteAccount());
//									break;
//								}
//									//查询保存本地猛将帐号
//								case Message_Tag_ID.MJ_ACCOUNT_LOCAL_QUERY:
//								{
//									send(mjAccountAPI.MJLocalAccount_Query());
//									break;
//								}
//									//密码保存
//								case Message_Tag_ID.MJ_ACCOUNTPASSWD_LOCAL_CREATE:
//								{
//									send(mjAccountAPI.AccountPwd_Insert());
//									break;
//								}
//									//密码修改
//								case Message_Tag_ID.MJ_ACCOUNTPASSWD_REMOTE_UPDATE:
//								{
//									send(mjAccountAPI.AccountPwd_Update());
//									break;
//								}
//									//密码恢复
//								case Message_Tag_ID.MJ_ACCOUNTPASSWD_REMOTE_RESTORE:
//								{
//									send(mjAccountAPI.AccountPwd_Restore());
//									break;
//								}
//									//根据金钱排行
//								case Message_Tag_ID.MJ_MONEYSORT_QUERY:
//								{
//									send(mjcharacterAPI.MJ_Sort_MoneyAll());
//									break;
//								}
//									//根据等级排行
//								case Message_Tag_ID.MJ_LEVELSORT_QUERY:
//								{
//									send(mjcharacterAPI.MJ_Sort_LevelAll());
//									break;
//								}
//									//根据不同职业金钱排序
//								case Message_Tag_ID.MJ_MONEYFIGHTERSORT_QUERY:
//								{
//									send(mjcharacterAPI.MJ_Sort_MoneyFighter());
//									break;
//								}
//									//查询战士等级前100名
//								case Message_Tag_ID.MJ_LEVELFIGHTERSORT_QUERY:
//								{
//									send(mjcharacterAPI.MJ_Sort_LevelFighter());
//									break;
//								}
//									//查询法师金钱前100名
//								case Message_Tag_ID.MJ_MONEYRABBISORT_QUERY:
//								{
//									send(mjcharacterAPI.MJ_Sort_MoneyRabbi());
//									break;
//								}
//									//查询法师等级前100名
//								case Message_Tag_ID.MJ_LEVELRABBISORT_QUERY:
//								{
//									send(mjcharacterAPI.MJ_Sort_LevelRabbi());
//									break;
//								}
//									//查询道士金钱前100名
//								case Message_Tag_ID.MJ_MONEYTAOISTSORT_QUERY:
//								{
//									send(mjcharacterAPI.MJ_Sort_MoneyTaois());
//									break;
//								}
//									//查询道士等级前100名
//								case Message_Tag_ID.MJ_LEVELTAOISTSORT_QUERY:
//								{
//									send(mjcharacterAPI.MJ_Sort_LevelTaois());
//									break;
//								}
//									//检查该用户交易记录
//								case Message_Tag_ID.MJ_ITEMLOG_QUERY:
//								{
//									send(mjlogAPI.MJ_ItemLog_Query());
//									break;
//								}
									//查看超级舞者玩家大区激活状态
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
									//查看超级舞者玩家人物资料
								case Message_Tag_ID.SDO_CHARACTERINFO_QUERY:
								{
									send(sdocharacterAPI.SDOCharInfo_Query());
									break;
								}
									//修改超级舞者玩家人物资料
								case Message_Tag_ID.SDO_CHARACTERINFO_UPDATE:
								{
									send(sdocharacterAPI.SDOCharacterInfo_Update());
									break;
								}
									//查看游戏里面的道具信息
								case Message_Tag_ID.SDO_ITEMSHOP_QUERY:
								{
									send(itemShopAPI.itemShop_QueryALL());
									break;
								}
									//查看玩家身上的道具
								case Message_Tag_ID.SDO_ITEMSHOP_BYOWNER_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(itemShopAPI.itemShop_Owner_Query(index-1,pageSize));
									break;
								}
									//删除玩家身上的道具
								case Message_Tag_ID.SDO_ITEMSHOP_DELETE:
								{
									send(itemShopAPI.ItemShop_Delete());
									break;
								}
									//查看玩家礼物盒道具
								case Message_Tag_ID.SDO_GIFTBOX_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(itemShopAPI.giftBox_Query(index-1,pageSize));
									break;
								}
									
									//添加玩家礼物盒上道具
								case Message_Tag_ID.SDO_GIFTBOX_CREATE:
								{
									send(itemShopAPI.GiftBox_MessageItem_Add());
									break;
								}
									//删除玩家礼物盒上道具
								case Message_Tag_ID.SDO_GIFTBOX_DELETE:
								{
									send(itemShopAPI.GiftBox_MessageItem_Delete());
									break;
								}
									//查看玩家登录状态
								case Message_Tag_ID.SDO_USERLOGIN_STATUS_QUERY:
								{
									send(accountAPI.SDO_login_Query());
									break;
								}
									//查看玩家上下线记录
								case Message_Tag_ID.SDO_USERONLINE_QUERY:
								{
									send(itemShopAPI.UserOnline_Query());
									break;
								}
									//查看玩家交易记录
								case Message_Tag_ID.SDO_ITEMSHOP_TRADE_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(itemShopAPI.UserTrade_Query(index-1,pageSize));
									break;
								}
									//查看玩家消费记录
								case Message_Tag_ID.SDO_CONSUME_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(itemShopAPI.UserConsume_Query(index-1,pageSize));
									break;
								}
									//查看玩家消费记录合计
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
									//查看玩家Ｇ币
								case Message_Tag_ID.SDO_USERGCASH_QUERY:
								{
									send(ItemlogAPI.userGCash_Query());
									break;
								}
									//查看所有被停封的帐号
								case Message_Tag_ID.SDO_MEMBERBANISHMENT_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(accountAPI.SDOMember_banishment_QueryAll(index-1,pageSize));
									break;
								}
									//查看本地被停封的帐号
								case Message_Tag_ID.SDO_MEMBERLOCAL_BANISHMENT:
								{
									send(accountAPI.SDOBanishmentLocal_Query());
									break;
								}
									//查看某个玩家帐号停封状态
								case Message_Tag_ID.SDO_MEMBERSTOPSTATUS_QUERY:
								{
									send(accountAPI.SDOMember_banishment_Query());
									break;
								}
									//封停帐号
								case Message_Tag_ID.SDO_ACCOUNT_CLOSE:
								{
									send(accountAPI.SDOMemberClose_Update());
									break;
								}
									//解封帐号
								case Message_Tag_ID.SDO_ACCOUNT_OPEN:
								{
									send(accountAPI.SDOMemberOpen_Update());
									break;
								}
									//充值明细
								case Message_Tag_ID.SDO_USERMCASH_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(ItemlogAPI.userChargeDetail_Query(index-1,pageSize));
									break;
								}
									//充值明细合计
								case Message_Tag_ID.SDO_USERCHARAGESUM_QUERY:
								{
									send(ItemlogAPI.userChargeSum_Query());
									break;
								}
									//补尝G币
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
									//一卡通和休闲卡查询
								case Message_Tag_ID.CARD_USERCHARGEDETAIL_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(cardDetailInfoAPI.CardDetailInfo_Query(index - 1, pageSize));
									break;
								}
									//一卡通和休闲卡消费查询
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
									//重置玩家身份证信息
								case Message_Tag_ID.CARD_USERINFO_QUERY:
								{
									send(cardDetailInfoAPI.UserInfo_Query());
									break;
								}
									//重置玩家安全码信息
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
									//查看玩家G币购买信息
								case Message_Tag_ID.AUSHOP_USERGPURCHASE_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(userCashPurchaseAPI.UserGCashPurchase_Query(index-1,pageSize));
									break;
								}
									//查看玩家G币购买记录合计
								case Message_Tag_ID.AUSHOP_USERGPURCHASE_SUM_QUERY:
								{
									send(userCashPurchaseAPI.UserGCashPurchase_QuerySum());
									break;
								}

									//查看玩家M币购买信息
								case Message_Tag_ID.AUSHOP_USERMPURCHASE_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(userCashPurchaseAPI.UserMCashPurchase_Query(index-1,pageSize));
									break;
								}
									//查看玩家M币购买记录合计
								case Message_Tag_ID.AUSHOP_USERMPURCHASE_SUM_QUERY:
								{
									send(userCashPurchaseAPI.UserMCashPurchase_QuerySum());
									break;
								}
									//查看玩家积分
								case Message_Tag_ID.AUSHOP_USERINTERGRAL_QUERY:
								{
									send(userCashPurchaseAPI.UserIntegral_Query());
									break;
								}
									//道具回收兑换记录
								case Message_Tag_ID.AUSHOP_AVATARECOVER_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(userCashPurchaseAPI.UserAvatarItemRev_Query(index-1,pageSize));
									break;
								}
									//道具回收兑换详细记录
								case Message_Tag_ID.AUSHOP_AVATARECOVER_DETAIL_QUERY:
								{
									send(userCashPurchaseAPI.UserAvatarItemRevDetail_Query());
									break;

								}
									//劲乐团玩家角色信息查询
								case Message_Tag_ID.O2JAM_CHARACTERINFO_QUERY:
								{
									send(o2jamCharacterAPI.O2JAMCharInfo_Query());
									break;
								}
									//劲乐团玩家角色信息更新
								case Message_Tag_ID.O2JAM_CHARACTERINFO_UPDATE:
								{
									send(o2jamCharacterAPI.O2JAMCharacterInfo_Update());
									break;
								}
									//劲乐团玩家消费信息
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
		/// 单线程用户发送消息
		/// </summary>
		/// <param name="msg">消息包</param>
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
		/// 单线程用户发送消息
		/// </summary>
		/// <param name="msg">消息包</param>
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
		/// /单线程用户接受消息
		/// </summary>
		/// <returns>接受到消息包</returns>
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
		/// 负责在单线程循环接受客户端发送的消息
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
