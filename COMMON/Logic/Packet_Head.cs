using System;

namespace Common.Logic
{
	/// <summary>
	/// Packet_Head 的摘要说明。
	/// </summary>
	public enum Msg_Category:byte
	{
		COMMON = 0x80,//公用消息集
		USER_ADMIN = 0x81,//GM帐号操作消息集
		MODULE_ADMIN = 0x82,//模块操作消息集
		USER_MODULE_ADMIN = 0x83,//用户和模块操作消息集
		GAME_ADMIN = 0x84, //游戏模块操作消息集
		NOTES_ADMIN = 0x85,//NOTES模块操作消息集
		MJ_ADMIN = 0x86,//猛将GM工具操作消息集
		SDO_ADMIN = 0x87,//超级舞者操作消息集
        AU_ADMIN = 0x88,//劲舞团操作消息集
        CR_ADMIN = 0x89,//疯狂卡丁车操作消息集
        CARD_ADMIN = 0x90,//用户充值消费记录消息集
        AUSHOP_ADMIN = 0x91,//劲舞团商城记录消息集
		O2JAM_ADMIN = 0x92,//劲乐团记录消息集
		O2JAM2_ADMIN = 0x93,//劲乐团II记录消息集
		SOCCER_ADMIN = 0x94,//劲爆足球记录消息集
		ERROR = 0xFF
	}
	public enum	ServiceKey:ushort
	{
		/// <summary>
		/// 公共模块(0x80)
		/// </summary>
		CONNECT = 0x0001,
		CONNECT_RESP = 0x8001,
		DISCONNECT = 0x0002,
		DISCONNECT_RESP = 0x8002,
		ACCOUNT_AUTHOR = 0x0003,
		ACCOUNT_AUTHOR_RESP = 0x8003,
		SERVERINFO_IP_QUERY = 0x0004,
		SERVERINFO_IP_QUERY_RESP = 0x8004,
		GMTOOLS_OperateLog_Query = 0x0005,//查看工具操作记录
		GMTOOLS_OperateLog_Query_RESP = 0x8005,//查看工具操作记录响应
        SERVERINFO_IP_ALL_QUERY = 0x0006,//查看所有游戏服务器地址
        SERVERINFO_IP_ALL_QUERY_RESP = 0x8006,//查看所有游戏服务器地址响应
        LINK_SERVERIP_CREATE = 0x0007,//添加游戏链接数据库
        LINK_SERVERIP_CREATE_RESP = 0x8007,//添加游戏链接数据库响应
        CLIENT_PATCH_COMPARE = 0x0008,
        CLIENT_PATCH_COMPARE_RESP = 0x8008,
        CLIENT_PATCH_UPDATE = 0x0009,
        CLIENT_PATCH_UPDATE_RESP = 0x8009,
		LINK_SERVERIP_DELETE = 0x0010,
		LINK_SERVERIP_DELETE_RESP = 0x8010,
		GMTOOLS_BUGLIST_QUERY = 0x0011,
		GMTOOLS_BUGLIST_QUERY_RESP = 0x8011,
		GMTOOLS_BUGLIST_INSERT = 0x0012,
		GMTOOLS_BUGLIST_INSERT_RESP = 0x8012,
		GMTOOLS_BUGLIST_UPDATE = 0x0013,
		GMTOOLS_BUGLIST_UPDATE_RESP = 0x8013,
		GMTOOLS_UPDATELIST_QUERY = 0x0014,
		GMTOOLS_UPDATELIST_QUERY_RESP = 0x8014,

		/// <summary>
		///用户管理模块(0x81) 
		/// </summary>
		USER_CREATE = 0x0001,
		USER_CREATE_RESP = 0x8001,
		USER_UPDATE = 0x0002,
		USER_UPDATE_RESP = 0x8002,
		USER_DELETE = 0x0003,
		USER_DELETE_RESP = 0x8003,
        USER_QUERY = 0x0004,
        USER_QUERY_RESP = 0x8004,
		USER_PASSWD_MODIF = 0x0005,
		USER_PASSWD_MODIF_RESP = 0x8005,
		USER_QUERY_ALL = 0x0006,
		USER_QUERY_ALL_RESP = 0x8006,
		DEPART_QUERY = 0x0007,
		DEPART_QUERY_RESP = 0x8007,
        UPDATE_ACTIVEUSER = 0x0008,//更新在线用户状态
        UPDATE_ACTIVEUSER_RESP = 0x8008,//更新在线用户状态响应
		DEPARTMENT_CREATE = 0x0009,//部门创建
		DEPARTMENT_CREATE_RESP = 0x8009,//部门创建响应
		DEPARTMENT_UPDATE= 0x0010,//部门修改
		DEPARTMENT_UPDATE_RESP = 0x8010,//部门修改响应
		DEPARTMENT_DELETE= 0x0011,//部门删除
		DEPARTMENT_DELETE_RESP = 0x8011,//部门删除响应
		DEPARTMENT_ADMIN = 0x0012,//部门管理
		DEPARTMENT_ADMIN_RESP = 0x8012,//部门管理响应
		DEPARTMENT_RELATE_QUERY = 0x0013,//部门关联查询
		DEPARTMENT_RELATE_QUERY_RESP = 0x8013,//部门关联查询
		DEPART_RELATE_GAME_QUERY = 0x0014,
		DEPART_RELATE_GAME_QUERY_RESP = 0x8014,
		USER_SYSADMIN_QUERY = 0x0015,
		USER_SYSADMIN_QUERY_RESP = 0x8015,

		/// <summary>
		/// 模块管理(0x82)
		/// </summary>
		MODULE_CREATE = 0x0001,
		MODULE_CREATE_RESP = 0x8001,
		MODULE_UPDATE = 0x0002,
		MODULE_UPDATE_RESP = 0x8002,
		MODULE_DELETE = 0x0003,
		MODULE_DELETE_RESP = 0x8003,
		MODULE_QUERY = 0x0004,
		MODULE_QUERY_RESP = 0x8004,

		/// <summary>
		/// 用户与模块(0x83) 
		/// </summary>
		USER_MODULE_CREATE = 0x0001,
		USER_MODULE_CREATE_RESP = 0x8001,
		USER_MODULE_UPDATE = 0x0002,
		USER_MODULE_UPDATE_RESP = 0x8002,
		USER_MODULE_DELETE = 0x0003,
		USER_MODULE_DELETE_RESP = 0x8003,
		USER_MODULE_QUERY= 0x0004,
		USER_MODULE_QUERY_RESP = 0x8004,

		/// <summary>
		/// 游戏管理(0x84) 
		/// </summary>
		GAME_CREATE = 0x0001,
		GAME_CREATE_RESP = 0x8001,
		GAME_UPDATE = 0x0002,
		GAME_UPDATE_RESP = 0x8002,
		GAME_DELETE = 0x0003,
		GAME_DELETE_RESP = 0x8003,
		GAME_QUERY = 0x0004,
		GAME_QUERY_RESP = 0x8004,
		GAME_MODULE_QUERY = 0x0005,
		GAME_MODULE_QUERY_RESP = 0x8005,

		/// <summary>
		/// NOTES管理(0x85) 
		/// </summary>
		NOTES_LETTER_TRANSFER = 0x0001,
		NOTES_LETTER_TRANSFER_RESP = 0x8001,
		NOTES_LETTER_PROCESS = 0x0002, //邮件处理
		NOTES_LETTER_PROCESS_RESP = 0x8002,//邮件处理
		NOTES_LETTER_TRANSMIT = 0x0003,//邮件转发
		NOTES_LETTER_TRANSMIT_RESP = 0x8003,//邮件转发响应
        
		/// <summary>
		/// 猛将GM工具管理(0x86)
		/// </summary>
		MJ_CHARACTERINFO_QUERY = 0x0001,//检查玩家状态
		MJ_CHARACTERINFO_QUERY_RESP = 0x8001,//检查玩家状态响应
		MJ_CHARACTERINFO_UPDATE = 0x0002,//修改玩家状态
		MJ_CHARACTERINFO_UPDATE_RESP = 0x8002,//修改玩家状态响应
		MJ_LOGINTABLE_QUERY = 0x0003,//检查玩家是否在线
		MJ_LOGINTABLE_QUERY_RESP = 0x8003,//检查玩家是否在线响应
		MJ_CHARACTERINFO_EXPLOIT_UPDATE = 0x0004,//修改功勋值
		MJ_CHARACTERINFO_EXPLOIT_UPDATE_RESP = 0x8004,//修改功勋值响应
		MJ_CHARACTERINFO_FRIEND_QUERY = 0x0005,//列出好友名单
		MJ_CHARACTERINFO_FRIEND_QUERY_RESP = 0x8005,//列出好有名单响应
		MJ_CHARACTERINFO_FRIEND_CREATE = 0x0006,//添加好友
		MJ_CHARACTERINFO_FRIEND_CREATE_RESP = 0x8006,//添加好友响应
		MJ_CHARACTERINFO_FRIEND_DELETE = 0x0007,//删除好友
		MJ_CHARACTERINFO_FRIEND_DELETE_RESP = 0x8007,//删除好友响应
		MJ_GUILDTABLE_UPDATE = 0x0008,//修改服务器所有已存在帮会
		MJ_GUILDTABLE_UPDATE_RESP = 0x8008,//修改服务器所有已存在帮会响应
		MJ_ACCOUNT_LOCAL_CREATE = 0x0009,//将服务器上的account表里的玩家信息保存到本地服务器的
		MJ_ACCOUNT_LOCAL_CREATE_RESP = 0x8009,//将服务器上的account表里的玩家信息保存到本地服务器的响应
		MJ_ACCOUNT_REMOTE_DELETE = 0x0010,//永久封停帐号
		MJ_ACCOUNT_REMOTE_DELETE_RESP = 0x8010,//永久封停帐号的响应
		MJ_ACCOUNT_REMOTE_RESTORE = 0x0011,//解封帐号
		MJ_ACCOUNT_REMOTE_RESTORE_RESP = 0x8011,//解封帐号响应
		MJ_ACCOUNT_LIMIT_RESTORE = 0x0012,//有时限的封停
		MJ_ACCOUNT_LIMIT_RESTORE_RESP = 0x8012,//有时限的封停响应
		MJ_ACCOUNTPASSWD_LOCAL_CREATE = 0x0013,//保存玩家密码到本地 
		MJ_ACCOUNTPASSWD_LOCAL_CREATE_RESP = 0x8013,//保存玩家密码到本地
		MJ_ACCOUNTPASSWD_REMOTE_UPDATE = 0x0014,//修改玩家密码 
		MJ_ACCOUNTPASSWD_REMOTE_UPDATE_RESP = 0x8014,//修改玩家密码
		MJ_ACCOUNTPASSWD_REMOTE_RESTORE = 0x0015,//恢复玩家密码
		MJ_ACCOUNTPASSWD_REMOTE_RESTORE_RESP = 0x8015,//恢复玩家密码
		MJ_ITEMLOG_QUERY = 0x0016,//检查该用户交易记录
		MJ_ITEMLOG_QUERY_RESP = 0x8016,//检查该用户交易记录
		MJ_GMTOOLS_LOG_QUERY = 0x0017,//检查使用者操作记录
		MJ_GMTOOLS_LOG_QUERY_RESP = 0x8017,//检查使用者操作记录
		MJ_MONEYSORT_QUERY = 0x0018,//根据金钱排序
		MJ_MONEYSORT_QUERY_RESP = 0x8018,//根据金钱排序的响应
		MJ_LEVELSORT_QUERY = 0x0019,//根据等级排序
		MJ_LEVELSORT_QUERY_RESP = 0x8019,//根据等级排序的响应
		MJ_MONEYFIGHTERSORT_QUERY = 0x0020,//根据不同职业金钱排序
		MJ_MONEYFIGHTERSORT_QUERY_RESP = 0x8020,//根据不同职业金钱排序的响应
		MJ_LEVELFIGHTERSORT_QUERY = 0x0021,//根据不同职业等级排序
		MJ_LEVELFIGHTERSORT_QUERY_RESP = 0x8021,//根据不同职业等级排序的响应
		MJ_MONEYTAOISTSORT_QUERY = 0x0022,//根据道士金钱排序
		MJ_MONEYTAOISTSORT_QUERY_RESP = 0x8022,//根据道士金钱排序的响应
		MJ_LEVELTAOISTSORT_QUERY = 0x0023,//根据道士等级排序
		MJ_LEVELTAOISTSORT_QUERY_RESP = 0x8023,//根据道士等级排序的响应
		MJ_MONEYRABBISORT_QUERY = 0x0024,//根据法师金钱排序
		MJ_MONEYRABBISORT_QUERY_RESP = 0x8024,//根据法师金钱排序的响应
		MJ_LEVELRABBISORT_QUERY = 0x0025,//根据法师等级排序
		MJ_LEVELRABBISORT_QUERY_RESP = 0x8025,//根据法师等级排序的响应
		MJ_ACCOUNT_QUERY =  0x0026,//猛将帐号查询
		MJ_ACCOUNT_QUERY_RESP = 0x8026,//猛将帐号查询响应
        MJ_ACCOUNT_LOCAL_QUERY = 0x0027,//查询猛将本地帐号
        MJ_ACCOUNT_LOCAL_QUERY_RESP = 0x8027,//查询猛将本地帐号响应

		/// <summary>
		/// SDO_ADMIN 超级舞者工具操作消息集
		/// </summary>
		SDO_ACCOUNT_QUERY = 0x0026,//查看玩家的帐号信息
		SDO_ACCOUNT_QUERY_RESP = 0x8026,//查看玩家的帐号信息响应
		SDO_CHARACTERINFO_QUERY = 0x0027,//查看任务资料的信息
		SDO_CHARACTERINFO_QUERY_RESP = 0x8027,//查看人物资料的信息响应
		SDO_ACCOUNT_CLOSE = 0x0028,//封停帐户的权限信息
		SDO_ACCOUNT_CLOSE_RESP = 0x8028,//封停帐户的权限信息响应
		SDO_ACCOUNT_OPEN = 0x0029,//解封帐户的权限信息
		SDO_ACCOUNT_OPEN_RESP = 0x8029,//解封帐户的权限信息响应
		SDO_PASSWORD_RECOVERY = 0x0030,//玩家找回密码
		SDO_PASSWORD_RECOVERY_RESP = 0x8030,//玩家找回密码响应
		SDO_CONSUME_QUERY = 0x0031,//查看玩家的消费记录
		SDO_CONSUME_QUERY_RESP = 0x8031,//查看玩家的消费记录响应
		SDO_USERONLINE_QUERY = 0x0032,//查看玩家上下线状态
		SDO_USERONLINE_QUERY_RESP = 0x8032,//查看玩家上下线状态响应
		SDO_USERTRADE_QUERY = 0x0033,//查看玩家交易状态
		SDO_USERTRADE_QUERY_RESP = 0x8033,//查看玩家交易状态响应
		SDO_CHARACTERINFO_UPDATE = 0x0034,//修改玩家的账号信息
		SDO_CHARACTERINFO_UPDATE_RESP = 0x8034,//修改玩家的账号信息响应
		SDO_ITEMSHOP_QUERY = 0x0035,//查看游戏里面所有道具信息
		SDO_ITEMSHOP_QUERY_RESP = 0x8035,//查看游戏里面所有道具信息响应
		SDO_ITEMSHOP_DELETE = 0x0036,//删除玩家道具信息
		SDO_ITEMSHOP_DELETE_RESP  = 0x8036,//删除玩家道具信息响应
		SDO_GIFTBOX_CREATE  = 0x0037,//添加玩家礼物盒道具信息
		SDO_GIFTBOX_CREATE_RESP  = 0x8037,//添加玩家礼物盒道具信息响应
		SDO_GIFTBOX_QUERY = 0x0038,//查看玩家礼物盒的道具
		SDO_GIFTBOX_QUERY_RESP = 0x8038,//查看玩家礼物盒的道具响应
		SDO_GIFTBOX_DELETE = 0x0039,//删除玩家礼物盒的道具
		SDO_GIFTBOX_DELETE_RESP = 0x8039,//删除玩家礼物盒的道具响应
		SDO_USERLOGIN_STATUS_QUERY = 0x0040,//查看玩家登录状态
		SDO_USERLOGIN_STATUS_QUERY_RESP = 0x8040,//查看玩家登录状态响应
		SDO_ITEMSHOP_BYOWNER_QUERY = 0x0041,////查看玩家身上道具信息
		SDO_ITEMSHOP_BYOWNER_QUERY_RESP = 0x8041,////查看玩家身上道具信息
		SDO_ITEMSHOP_TRADE_QUERY = 0x0042,//查看玩家交易记录信息
		SDO_ITEMSHOP_TRADE_QUERY_RESP = 0x8042,//查看玩家交易记录信息的响应
		SDO_MEMBERSTOPSTATUS_QUERY = 0x0043,//查看该帐号状态
		SDO_MEMBERSTOPSTATUS_QUERY_RESP = 0x8043,///查看该帐号状态的响应
        SDO_MEMBERBANISHMENT_QUERY = 0x0044,//查看所有停封的帐号
        SDO_MEMBERBANISHMENT_QUERY_RESP = 0x8044,//查看所有停封的帐号响应
        SDO_USERMCASH_QUERY = 0x0045,//玩家充值记录查询
        SDO_USERMCASH_QUERY_RESP = 0x8045,//玩家充值记录查询响应
        SDO_USERGCASH_UPDATE = 0x0046,//补偿玩家G币
        SDO_USERGCASH_UPDATE_RESP = 0x8046,//补偿玩家G币的响应
        SDO_MEMBERLOCAL_BANISHMENT = 0x0047,//本地保存停封信息
        SDO_MEMBERLOCAL_BANISHMENT_RESP = 0x8047,//本地保存停封信息响应
        SDO_EMAIL_QUERY = 0x0048,//得到玩家的EMAIL
        SDO_EMAIL_QUERY_RESP = 0x8048,//得到玩家的EMAIL响应
        SDO_USERCHARAGESUM_QUERY = 0x0049,//得到充值记录总和
        SDO_USERCHARAGESUM_QUERY_RESP = 0x8049,//得到充值记录总和响应
        SDO_USERCONSUMESUM_QUERY = 0x0050,//得到消费记录总和
        SDO_USERCONSUMESUM_QUERY_RESP = 0x8050,//得到消费记录总和响应
        SDO_USERGCASH_QUERY = 0x0051,//玩家Ｇ币记录查询
        SDO_USERGCASH_QUERY_RESP = 0x8051,//玩家Ｇ币记录查询响应
		SDO_CHALLENGE_QUERY = 0x0052,
		SDO_CHALLENGE_QUERY_RESP = 0x8052,
		SDO_CHALLENGE_INSERT = 0x0053,
		SDO_CHALLENGE_INSERT_RESP = 0x8053,
		SDO_CHALLENGE_UPDATE = 0x0054,
		SDO_CHALLENGE_UPDATE_RESP = 0x8054,
		SDO_CHALLENGE_DELETE = 0x0055,
		SDO_CHALLENGE_DELETE_RESP = 0x8055,
		SDO_MUSICDATA_QUERY = 0x0056,
		SDO_MUSICDATA_QUERY_RESP = 0x8056,
		SDO_MUSICDATA_OWN_QUERY = 0x0057,
		SDO_MUSICDATA_OWN_QUERY_RESP = 0x8057,
		SDO_CHALLENGE_SCENE_QUERY = 0x0058,
		SDO_CHALLENGE_SCENE_QUERY_RESP = 0x8058,
		SDO_CHALLENGE_SCENE_CREATE = 0x0059,
		SDO_CHALLENGE_SCENE_CREATE_RESP = 0x8059,
		SDO_CHALLENGE_SCENE_UPDATE = 0x0060,
		SDO_CHALLENGE_SCENE_UPDATE_RESP = 0x8060,
		SDO_CHALLENGE_SCENE_DELETE = 0x0061,
		SDO_CHALLENGE_SCENE_DELETE_RESP = 0x8061,
		SDO_MEDALITEM_CREATE = 0x0062,
		SDO_MEDALITEM_CREATE_RESP = 0x8062,
		SDO_MEDALITEM_UPDATE = 0x0063,
		SDO_MEDALITEM_UPDATE_RESP = 0x8063,
		SDO_MEDALITEM_DELETE = 0x0064,
		SDO_MEDALITEM_DELETE_RESP = 0x8064,
		SDO_MEDALITEM_QUERY = 0x0065,
		SDO_MEDALITEM_QUERY_RESP = 0x8065,
		SDO_MEDALITEM_OWNER_QUERY = 0x0066,
		SDO_MEDALITEM_OWNER_QUERY_RESP = 0x8066,
		SDO_MEMBERDANCE_OPEN = 0x0067,
		SDO_MEMBERDANCE_OPEN_RESP = 0x8067,
		SDO_MEMBERDANCE_CLOSE = 0x0068,
		SDO_MEMBERDANCE_CLOSE_RESP = 0x8068,
		SDO_USERNICK_UPDATE =0x0069, 
		SDO_USERNICK_UPDATE_RESP =0x8069, 
		SDO_PADKEYWORD_QUERY = 0x0070,
		SDO_PADKEYWORD_QUERY_RESP = 0x8070,
		SDO_BOARDMESSAGE_REQ = 0x0071,
		SDO_BOARDMESSAGE_REQ_RESP = 0x8071,
		SDO_CHANNELLIST_QUERY =  0x0072,
		SDO_CHANNELLIST_QUERY_RESP = 0x8072,
		SDO_ALIVE_REQ = 0x0073,
		SDO_ALIVE_REQ_RESP = 0x8073,
		SDO_BOARDTASK_QUERY = 0x0074,
		SDO_BOARDTASK_QUERY_RESP = 0x8074,
		SDO_BOARDTASK_UPDATE = 0x0075,
		SDO_BOARDTASK_UPDATE_RESP = 0x8075,
		SDO_BOARDTASK_INSERT = 0x0076,
		SDO_BOARDTASK_INSERT_RESP = 0x8076,
		SDO_DAYSLIMIT_QUERY = 0x0077,
		SDO_DAYSLIMIT_QUERY_RESP = 0x8077,
		/// <summary>
		/// AU_ADMIN 劲舞团工具操作消息集
		/// </summary>
		AU_ACCOUNT_QUERY = 0x0001,//玩家帐号信息查询
		AU_ACCOUNT_QUERY_RESP = 0x8001,//玩家帐号信息查询响应
		AU_ACCOUNTREMOTE_QUERY = 0x0002,//游戏服务器封停的玩家帐号查询
		AU_ACCOUNTREMOTE_QUERY_RESP = 0x8002,//游戏服务器封停的玩家帐号查询响应
		AU_ACCOUNTLOCAL_QUERY = 0x0003,//本地封停的玩家帐号查询
		AU_ACCOUNTLOCAL_QUERY_RESP = 0x8003,//本地封停的玩家帐号查询响应
		AU_ACCOUNT_CLOSE = 0x0004,//封停的玩家帐号
		AU_ACCOUNT_CLOSE_RESP = 0x8004,//封停的玩家帐号响应
		AU_ACCOUNT_OPEN = 0x0005,//解封的玩家帐号
		AU_ACCOUNT_OPEN_RESP = 0x8005,//解封的玩家帐号响应
		AU_ACCOUNT_BANISHMENT_QUERY = 0x0006,//玩家封停帐号查询
		AU_ACCOUNT_BANISHMENT_QUERY_RESP = 0x8006,//玩家封停帐号查询响应
		AU_CHARACTERINFO_QUERY = 0x0007,//查询玩家的账号信息
		AU_CHARACTERINFO_QUERY_RESP = 0x8007,//查询玩家的账号信息响应
		AU_CHARACTERINFO_UPDATE = 0x0008,//修改玩家的账号信息
		AU_CHARACTERINFO_UPDATE_RESP = 0x8008,//修改玩家的账号信息响应
		AU_ITEMSHOP_QUERY = 0x0009,//查看游戏里面所有道具信息
		AU_ITEMSHOP_QUERY_RESP = 0x8009,//查看游戏里面所有道具信息响应
		AU_ITEMSHOP_DELETE = 0x0010,//删除玩家道具信息
		AU_ITEMSHOP_DELETE_RESP = 0x8010,//删除玩家道具信息响应
		AU_ITEMSHOP_BYOWNER_QUERY = 0x0011,////查看玩家身上道具信息
		AU_ITEMSHOP_BYOWNER_QUERY_RESP = 0x8011,////查看玩家身上道具信息
		AU_ITEMSHOP_TRADE_QUERY = 0x0012,//查看玩家交易记录信息
		AU_ITEMSHOP_TRADE_QUERY_RESP = 0x8012,//查看玩家交易记录信息的响应
		AU_ITEMSHOP_CREATE = 0x0013,//添加玩家礼物盒道具信息
		AU_ITEMSHOP_CREATE_RESP = 0x8013,//添加玩家礼物盒道具信息响应
		AU_LEVELEXP_QUERY = 0x0014,//查看玩家等级经验
		AU_LEVELEXP_QUERY_RESP = 0x8014,////查看玩家等级经验响应
		AU_USERLOGIN_STATUS_QUERY = 0x0015,//查看玩家登录状态
		AU_USERLOGIN_STATUS_QUERY_RESP = 0x8015,//查看玩家登录状态响应
		AU_USERCHARAGESUM_QUERY = 0x0016,//得到充值记录总和
		AU_USERCHARAGESUM_QUERY_RESP = 0x8016,//得到充值记录总和响应
		AU_CONSUME_QUERY = 0x0017,//查看玩家的消费记录
		AU_CONSUME_QUERY_RESP = 0x8017,//查看玩家的消费记录响应
		AU_USERCONSUMESUM_QUERY = 0x0018,//得到消费记录总和
		AU_USERCONSUMESUM_QUERY_RESP = 0x8018,//得到消费记录总和响应
		AU_USERMCASH_QUERY = 0x0019,//玩家充值记录查询
		AU_USERMCASH_QUERY_RESP = 0x8019,//玩家充值记录查询响应
		AU_USERGCASH_QUERY = 0x0020,//玩家Ｇ币记录查询
		AU_USERGCASH_QUERY_RESP = 0x8020,//玩家Ｇ币记录查询响应
		AU_USERGCASH_UPDATE = 0x0021,//补偿玩家G币
		AU_USERGCASH_UPDATE_RESP = 0x8021,//补偿玩家G币的响应
		AU_USERNICK_UPDATE = 0x0022,
		AU_USERNICK_UPDATE_RESP = 0x8022,

		/// <summary>
		/// CR_ADMIN 疯狂卡丁车工具操作消息集
		/// </summary>
		CR_ACCOUNT_QUERY = 0x0001,//玩家帐号信息查询
		CR_ACCOUNT_QUERY_RESP = 0x8001,//玩家帐号信息查询响应
		CR_ACCOUNTACTIVE_QUERY = 0x0002,//玩家帐号激活信息
		CR_ACCOUNTACTIVE_QUERY_RESP = 0x8002,//玩家帐号激活响应
		CR_CALLBOARD_QUERY = 0x0003,//公告信息查询
		CR_CALLBOARD_QUERY_RESP = 0x8003,//公告信息查询响应
		CR_CALLBOARD_CREATE = 0x0004,//发布公告
		CR_CALLBOARD_CREATE_RESP = 0x8004,//发布公告响应
		CR_CALLBOARD_UPDATE = 0x0005,//更新公告信息
		CR_CALLBOARD_UPDATE_RESP = 0x8005,//更新公告信息的响应
		CR_CALLBOARD_DELETE = 0x0006,//删除公告信息
		CR_CALLBOARD_DELETE_RESP = 0x8006,//删除公告信息的响应
		CR_CHARACTERINFO_QUERY = 0x0007,//玩家角色信息查询
		CR_CHARACTERINFO_QUERY_RESP = 0x8007,//玩家角色信息查询的响应
		CR_CHARACTERINFO_UPDATE = 0x0008,//玩家角色信息查询
		CR_CHARACTERINFO_UPDATE_RESP = 0x8008,//玩家角色信息查询的响应
		CR_CHANNEL_QUERY = 0x0009,//公告频道查询
		CR_CHANNEL_QUERY_RESP = 0x8009,//公告频道查询的响应
		CR_NICKNAME_QUERY = 0x0010,//玩家昵称查询
		CR_NICKNAME_QUERY_RESP = 0x8010,//玩家昵称的响应
		CR_LOGIN_LOGOUT_QUERY = 0x0011,//玩家上线、下线时间查询
		CR_LOGIN_LOGOUT_QUERY_RESP = 0x8011,//玩家上线、下线时间查询的响应
		CR_ERRORCHANNEL_QUERY = 0x0012,//补充错误公告频道查询
		CR_ERRORCHANNEL_QUERY_RESP = 0x8012,//补充错误公告频道查询的响应

		/// <summary>
		/// 充值消费GM工具(0x90)
		/// </summary>
		CARD_USERCHARGEDETAIL_QUERY = 0x0001,//一卡通查询
		CARD_USERCHARGEDETAIL_QUERY_RESP = 0x8001,//一卡通查询响应
		CARD_USERDETAIL_QUERY = 0x0002,//久之游用户注册信息查询
		CARD_USERDETAIL_QUERY_RESP = 0x8002,//久之游用户注册信息查询响应
		CARD_USERCONSUME_QUERY = 0x0003,//休闲币消费查询
		CARD_USERCONSUME_QUERY_RESP = 0x8003,//休闲币消费查询响应
		CARD_VNETCHARGE_QUERY = 0x0004,//互联星空充值查询
		CARD_VNETCHARGE_QUERY_RESP = 0x8004,//互联星空充值查询响应
		CARD_USERDETAIL_SUM_QUERY = 0x0005,//充值合计查询
		CARD_USERDETAIL_SUM_QUERY_RESP = 0x8005,//充值合计查询响应
		CARD_USERCONSUME_SUM_QUERY = 0x0006,//消费合计查询
		CARD_USERCONSUME_SUM_QUERY_RESP = 0x8006,//消费合计响应
		CARD_USERINFO_QUERY = 0x0007,//玩家注册信息查询
		CARD_USERINFO_QUERY_RESP = 0x8007,//玩家注册信息查询响应
		CARD_USERINFO_CLEAR = 0x0008,
		CARD_USERINFO_CLEAR_RESP = 0x8008,
		CARD_USERSECURE_CLEAR = 0x0009,//重置玩家安全码信息
		CARD_USERSECURE_CLEAR_RESP = 0x8009,//重置玩家安全码信息响应
		CARD_USERNICK_QUERY = 0x0010,
		CARD_USERNICK_QUERY_RESP = 0x8010,
		CARD_USERLOCK_UPDATE = 0x0011,
		CARD_USERLOCK_UPDATE_RESP = 0x8011,

        /// <summary>
        /// 劲舞团商城(0x91)
        /// </summary>
        AUSHOP_USERGPURCHASE_QUERY = 0x0001,//用户G币购买记录
        AUSHOP_USERGPURCHASE_QUERY_RESP = 0x8001,//用户G币购买记录
        AUSHOP_USERMPURCHASE_QUERY = 0x0002,//用户M币购买记录
        AUSHOP_USERMPURCHASE_QUERY_RESP = 0x8002,//用户M币购买记录
        AUSHOP_AVATARECOVER_QUERY = 0x0003,//道具回收兑换记
        AUSHOP_AVATARECOVER_QUERY_RESP = 0x8003,//道具回收兑换记
        AUSHOP_USERINTERGRAL_QUERY = 0x0004,//用户积分记录
        AUSHOP_USERINTERGRAL_QUERY_RESP = 0x8004,//用户积分记录
        AUSHOP_USERGPURCHASE_SUM_QUERY = 0x0005,//用户G币购买记录合计
        AUSHOP_USERGPURCHASE_SUM_QUERY_RESP = 0x8005,//用户G币购买记录合计响应
        AUSHOP_USERMPURCHASE_SUM_QUERY = 0x0006,//用户M币购买记录合计
        AUSHOP_USERMPURCHASE_SUM_QUERY_RESP = 0x8006,//用户M币购买记录合计响应
		AUSHOP_AVATARECOVER_DETAIL_QUERY = 0x0007,//道具回收兑换详细记录
		AUSHOP_AVATARECOVER_DETAIL_QUERY_RESP = 0x8007,//道具回收兑换详细记录

		/// <summary>
		/// 劲乐团工具(0x92)
		/// </summary>
		O2JAM_CHARACTERINFO_QUERY= 0x0001,//玩家角色信息查询
		O2JAM_CHARACTERINFO_QUERY_RESP= 0x8001,//玩家角色信息查询
		O2JAM_CHARACTERINFO_UPDATE= 0x0002,//玩家角色信息更新
		O2JAM_CHARACTERINFO_UPDATE_RESP= 0x8002,//玩家角色信息更新
		O2JAM_ITEM_QUERY= 0x0003,//玩家道具信息查询
		O2JAM_ITEM_QUERY_RESP= 0x8003,//玩家角色信息查询
		O2JAM_ITEM_UPDATE= 0x0004,//玩家道具信息更新
		O2JAM_ITEM_UPDATE_RESP= 0x8004,//玩家道具信息更新
		O2JAM_CONSUME_QUERY= 0x0005,//玩家消费信息查询
		O2JAM_CONSUME_QUERY_RESP= 0x8005,//玩家消费信息查询
		O2JAM_ITEMDATA_QUERY= 0x0006,//道具列表查询
		O2JAM_ITEMDATA_QUERY_RESP= 0x8006,//道具列表信息查询
		O2JAM_GIFTBOX_QUERY = 0x0007,//玩家礼物盒查询
		O2JAM_GIFTBOX_QUERY_RESP = 0x8007,//玩家礼物盒查询
		O2JAM_USERGCASH_UPDATE = 0x0008,//补偿玩家G币
		O2JAM_USERGCASH_UPDATE_RESP = 0x8008,//补偿玩家G币的响应
		O2JAM_CONSUME_SUM_QUERY= 0x0009,//玩家消费信息查询
		O2JAM_CONSUME_SUM_QUERY_RESP= 0x8009,//玩家消费信息查询
		O2JAM_GIFTBOX_CREATE= 0x0010,//添加玩家礼物盒道具
		O2JAM_GIFTBOX_CREATE_RESP= 0x8010,//添加玩家礼物盒道具
		O2JAM_ITEMNAME_QUERY = 0x0011,
		O2JAM_ITEMNAME_QUERY_RESP = 0x8011,
		O2JAM_GIFTBOX_DELETE = 0x0012,
		O2JAM_GIFTBOX_DELETE_RESP  =0x8012,

		/// <summary>
		/// 劲乐团IIGM工具(0x93)
		/// </summary>
		O2JAM2_ACCOUNT_QUERY = 0x0001,//玩家帐号信息查询
		O2JAM2_ACCOUNT_QUERY_RESP = 0x8001,//玩家帐号信息查询响应
		O2JAM2_ACCOUNTACTIVE_QUERY = 0x0002,//玩家帐号激活信息
		O2JAM2_ACCOUNTACTIVE_QUERY_RESP = 0x8002,//玩家帐号激活响应
		O2JAM2_CHARACTERINFO_QUERY = 0x0003,//用户信息查询
		O2JAM2_CHARACTERINFO_QUERY_RESP = 0x8003,
		O2JAM2_CHARACTERINFO_UPDATE = 0x0004,//用户信息修改
		O2JAM2_CHARACTERINFO_UPDATE_RESP = 0x8004,
		O2JAM2_ITEMSHOP_QUERY = 0x0005,
		O2JAM2_ITEMSHOP_QUERY_RESP = 0x8005,
		O2JAM2_ITEMSHOP_DELETE = 0x0006,
		O2JAM2_ITEMSHOP_DELETE_RESP = 0x8006,
		O2JAM2_MESSAGE_QUERY = 0x0007,
		O2JAM2_MESSAGE_QUERY_RESP = 0x8007,
		O2JAM2_MESSAGE_CREATE = 0x0008,
		O2JAM2_MESSAGE_CREATE_RESP = 0x8008,
		O2JAM2_MESSAGE_DELETE = 0x0009,
		O2JAM2_MESSAGE_DELETE_RESP = 0x8009,
		O2JAM2_CONSUME_QUERY = 0x0010,
		O2JAM2_CONUMSE_QUERY_RESP = 0x8010,
		O2JAM2_CONSUME_SUM_QUERY = 0x0011,
		O2JAM2_CONUMSE_QUERY_SUM_RESP = 0x8011,
		O2JAM2_TRADE_QUERY = 0x0012,
		O2JAM2_TRADE_QUERY_RESP = 0x8012,
		O2JAM2_TRADE_SUM_QUERY = 0x0013,
		O2JAM2_TRADE_QUERY_SUM_RESP = 0x8013,
		O2JAM2_AVATORLIST_QUERY = 0x0014,
		O2JAM2_AVATORLIST_QUERY_RESP = 0x8014,
		O2JAM2_ACCOUNT_CLOSE = 0x0015,//封停帐户的权限信息
		O2JAM2_ACCOUNT_CLOSE_RESP = 0x8015,//封停帐户的权限信息响应
		O2JAM2_ACCOUNT_OPEN = 0x0016,//解封帐户的权限信息
		O2JAM2_ACCOUNT_OPEN_RESP = 0x8016,//解封帐户的权限信息响应
		O2JAM2_MEMBERBANISHMENT_QUERY = 0x0017,
		O2JAM2_MEMBERBANISHMENT_QUERY_RESP = 0x8017,
		O2JAM2_MEMBERSTOPSTATUS_QUERY = 0x0018,
		O2JAM2_MEMBERSTOPSTATUS_QUERY_RESP = 0x8018,
		O2JAM2_MEMBERLOCAL_BANISHMENT = 0x0019,
		O2JAM2_MEMBERLOCAL_BANISHMENT_RESP = 0x8019,
		O2JAM2_USERLOGIN_DELETE = 0x0020,
		O2JAM2_USERLOGIN_DELETE_RESP = 0x8020,
		O2JAM2_LEVELEXP_QUERY = 0x0021,
		O2JAM2_LEVELEXP_QUERY_RESP =  0x8021,

		/// <summary>
		/// 劲爆足球 Add by KeHuaQing 2006-09-14
		/// </summary>
		SOCCER_CHARACTERINFO_QUERY = 0x0001,//用户信息查询
		SOCCER_CHARACTERINFO_QUERY_RESP = 0x8001,
		SOCCER_CHARCHECK_QUERY = 0x0002,//用户NameCheck,SocketCheck
		SOCCER_CHARCHECK_QUERY_RESP = 0x8002,
		SOCCER_CHARITEMS_RECOVERY_QUERY = 0x0003,//用户启用
		SOCCER_CHARITEMS_RECOVERY_QUERY_RESP = 0x8003,
		SOCCER_CHARPOINT_QUERY = 0x0004,//用户G币修改
		SOCCER_CHARPOINT_QUERY_RESP = 0x8004,
		SOCCER_DELETEDCHARACTERINFO_QUERY = 0x0005,//删除用户查询
		SOCCER_DELETEDCHARACTERINFO_QUERY_RESP = 0x8005,
		SOCCER_CHARACTERSTATE_MODIFY = 0x0006,//停封角色
		SOCCER_CHARACTERSTATE_MODIFY_RESP = 0x8006,
		SOCCER_ACCOUNTSTATE_MODIFY = 0x0007,//停封玩家
		SOCCER_ACCOUNTSTATE_MODIFY_RESP = 0x8007,
		SOCCER_CHARACTERSTATE_QUERY = 0x0008,//停封角色查询
		SOCCER_CHARACTERSTATE_QUERY_RESP = 0x8008,
		SOCCER_ACCOUNTSTATE_QUERY = 0x0009,//停封玩家查询
		SOCCER_ACCOUNTSTATE_QUERY_RESP = 0x8009,

		ERROR = 0xFFFF
	}

	/// <summary>
	/// Packet_Head 的摘要说明。
	/// </summary>
	public class Packet_Head
	{
		/// <summary>
		/// 消息体最大长度
		/// </summary>
		public const uint HEAD_LENGTH = 16;
		/// <summary>
		/// 消息体byte数组
		/// </summary>
		public byte[] m_bHeadBuffer;
		/// <summary>
		/// 消息体byte数组长度
		/// </summary>
		public uint m_uiHeadBufferLen = HEAD_LENGTH;
		/// <summary>
		/// 消息序列号ID
		/// </summary>
		public uint m_uiSeqID;
		/// <summary>
		/// 消息分类
		/// </summary>
		public Msg_Category m_mcCategory;
		/// <summary>
		/// 消息服务健
		/// </summary>
		public ServiceKey m_skServiceKey;
		/// <summary>
		/// 消息体里面日期
		/// </summary>
		public DateTime m_dtMsgDateTime;
		/// <summary>
		/// 消息体长度
		/// </summary>
		public uint m_uiBodyLen;
		/// <summary>
		/// 是否是合法消息头
		/// </summary>
		public bool IsValidHead = false;

		public Packet_Head()
		{
		}
		/// <summary>
		/// 构造消息头
		/// </summary>
		/// <param name="uiSeqID">消息序列号</param>
		/// <param name="mcCategory">消息分类</param>
		/// <param name="skServiceKey">消息服务健</param>
		/// <param name="uiBodyLen">消息体长度</param>
		public Packet_Head(uint uiSeqID, Msg_Category mcCategory,ServiceKey skServiceKey, uint uiBodyLen ) 
		{
			this.m_uiSeqID = uiSeqID;
			this.m_mcCategory = mcCategory;
			this.m_skServiceKey = skServiceKey;
			this.m_dtMsgDateTime = System.DateTime.Now;
			this.m_uiBodyLen = uiBodyLen;

			this.IsValidHead = this.PutToBuffer();
		}
		/// <summary>
		/// 封装消息头，消息头包括序列号、消息分类(CateGory)、消息服务健(ServiceKey)、日期
		/// </summary>
		/// <returns></returns>
		private bool PutToBuffer()
		{
			this.m_bHeadBuffer = new byte[HEAD_LENGTH];
			// sequence id
			for ( int i = 3;i >= 0;i-- ) 
			{
				m_bHeadBuffer[ i ] = (byte) (m_uiSeqID >> ( 8 * i )) ;
			}
			// msg category
			m_bHeadBuffer[ 4 ] = ( byte ) m_mcCategory;
			// reserved
			m_bHeadBuffer[ 5 ] = 0;
			// service key
			for ( int i = 1;i >= 0;i-- ) 
			{
				m_bHeadBuffer[ 6 + i ] = ( byte ) ( ((ushort)m_skServiceKey) >> ( 8 * i ) );
			}			
			m_bHeadBuffer[ 8 ] = ( byte ) ( m_dtMsgDateTime.Year - 1900);
			m_bHeadBuffer[ 9 ] = ( byte ) ( m_dtMsgDateTime.Month );
			m_bHeadBuffer[ 10 ] = ( byte ) ( m_dtMsgDateTime.Day );
			m_bHeadBuffer[ 11 ] = ( byte ) ( m_dtMsgDateTime.Hour );
			m_bHeadBuffer[ 12 ] = ( byte ) ( m_dtMsgDateTime.Minute );
			m_bHeadBuffer[ 13 ] = ( byte ) ( m_dtMsgDateTime.Second );
			// Body len
			for ( int i = 1;i >= 0;i-- ) 
			{
				m_bHeadBuffer[ 14 + i ] = ( byte ) ( m_uiBodyLen >> ( 8 * i ) );
			}
			return true;
		}
        /// <summary>
        /// 构造消息头
        /// </summary>
        /// <param name="bHeadBuffer">消息头</param>
        /// <param name="uiHeadBufferLen">消息头长度</param>
		public Packet_Head(byte[] bHeadBuffer, uint uiHeadBufferLen )
		{
			if (bHeadBuffer == null || bHeadBuffer.Length < HEAD_LENGTH)
				return ;
			//this.m_uiHeadBufferLen = uiHeadBufferLen;
			this.m_bHeadBuffer = new byte[this.m_uiHeadBufferLen];
			System.Array.Copy(bHeadBuffer,0,m_bHeadBuffer,0,m_uiHeadBufferLen);

			this.IsValidHead = this.Init();
		}
        /// <summary>
        /// 初始化消息头
        /// </summary>
        /// <returns></returns>
		
		private bool Init()
		{
			if (!this.IsValidHeadBuffer())
				return false;
			if (!this.getSequenceID(ref this.m_uiSeqID))
				return false;
			if (!this.getMsgCategory(ref this.m_mcCategory))
				return false;
			if (!this.getSeriveKey(ref this.m_skServiceKey))
				return false;
			if (!this.getMsgDateTime(ref this.m_dtMsgDateTime))
				return false;
			if (!this.getPacketBodyLen(ref this.m_uiBodyLen))
				return false;
			return true;
		}
		private bool IsValidHeadBuffer()
		{
			if ( this.m_bHeadBuffer == null || this.m_bHeadBuffer.Length != HEAD_LENGTH || this.m_uiHeadBufferLen != HEAD_LENGTH)
				return false;
			return true;
		}
        /// <summary>
        /// 得到消息序列号
        /// </summary>
        /// <param name="seqid">序列号</param>
        /// <returns></returns>
		private bool getSequenceID(ref uint seqid) //ByteArrayToUInt()
		{
			if (!this.IsValidHeadBuffer())
				return false;

			byte[] temp = new byte[ 4 ];
			System.Array.Copy(this.m_bHeadBuffer,0,temp,0,4);
			seqid = charArrayToInt( temp, 4 );
			return true;
		}
		/// <summary>
		/// 得到消息分类
		/// </summary>
		/// <param name="mc">消息分类</param>
		/// <returns></returns>
		private bool getMsgCategory(ref Msg_Category mc) 
		{
			if (!this.IsValidHeadBuffer())
				return false;
			try
			{
				mc = (Msg_Category) this.m_bHeadBuffer[4];
				return true;
			}
			catch(System.Exception)
			{
				mc = Msg_Category.ERROR;
				return false;
			}
		}
        /// <summary>
        /// 消息服务健
        /// </summary>
        /// <param name="sk">服务健</param>
        /// <returns></returns>
		private bool getSeriveKey(ref ServiceKey sk)//test(ServiceKey_FeeRequest)(temp[0] + temp[1] << 8) 
		{
			if (!this.IsValidHeadBuffer())
				return false;

			byte[] temp = new byte[ 2 ];
			System.Array.Copy(this.m_bHeadBuffer,6,temp,0,2);
			try
			{
				sk = (ServiceKey)((ushort)temp[0] + (ushort)(temp[1] << 8));
				return true;
			}
			catch(System.Exception)
			{
				sk = ServiceKey.ERROR;
				return false;
			}
		}
		/// <summary>
		/// 得到消息日期
		/// </summary>
		/// <param name="dt">传入日期</param>
		/// <returns></returns>
		private bool getMsgDateTime(ref DateTime dt ) 
		{
			if (!this.IsValidHeadBuffer())
				return false;

			uint year = ( uint ) this.m_bHeadBuffer[ 8 ] + 1900;
			uint month = ( uint ) this.m_bHeadBuffer[ 9 ];
			uint day = ( uint ) this.m_bHeadBuffer[ 10 ];
			uint hour = (uint)this.m_bHeadBuffer[11];
			uint minute = (uint)this.m_bHeadBuffer[12];
			uint second = (uint)this.m_bHeadBuffer[13];

			try
			{
				dt = new DateTime((int)year,(int)month,(int)day,(int)hour,(int)minute,(int)second);
				return true;
			}
			catch(System.Exception)
			{
				dt = new DateTime();
				return false;
			}			
		}
        /// <summary>
        /// 得到消息体长度
        /// </summary>
        /// <param name="len">消息体长度</param>
        /// <returns></returns>
		private bool getPacketBodyLen(ref uint len) 
		{
			if (!this.IsValidHeadBuffer())
				return false;

			byte[] temp = new byte[ 2 ];
			System.Array.Copy(this.m_bHeadBuffer,14,temp,0,2);
			len = charArrayToInt( temp, 2 );
			return true;
		}
        /// <summary>
        /// 将Byte转换INT
        /// </summary>
        /// <param name="b">BYTE类型</param>
        /// <param name="size">长度</param>
        /// <returns></returns>
		public uint charArrayToInt( byte[] b , int size ) 
		{
			uint value = 0;
			for ( int i = 0;i < size;i++ ) 
			{
				value += (uint)(b[ i ] << ( i * 8 )) ;
			}
			return value;
		}
		
		
		public override string ToString() 
		{
			if (!this.IsValidHead)
				return "Invalid Packet Head\r\n";
			return "Sequece Id  :" + this.m_uiSeqID.ToString("X")
				+"\r\nMsg Category:" + this.m_mcCategory.ToString("X")
				+"\r\nService Key :" + this.m_skServiceKey.ToString("X")
				+"\r\nDateTime       :" + this.m_dtMsgDateTime.ToString("yyyy-MM-dd hh:mm:ss")
				+"\r\nBody Length :" + this.m_uiBodyLen.ToString("X")
				+"\r\n";
		}
		public byte[] ToByteArray()
		{
			if (this.IsValidHead)
				return this.m_bHeadBuffer;
			else
				return new byte[0];
		}

	}
}
