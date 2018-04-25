using System;
using System.Xml.Serialization;
using System.Collections;
using Common.DataInfo;
namespace Common.Logic
{
	public enum Message_Tag_ID:uint
	{
		/// <summary>
		/// 公共模块(0x80)
		/// </summary>
		CONNECT = 0x800001,//连接请求
		CONNECT_RESP = 0x808001,//连接响应
		DISCONNECT = 0x800002,//断开请求
		DISCONNECT_RESP = 0x808002,//断开响应
		ACCOUNT_AUTHOR = 0x800003,//用户身份验证请求
		ACCOUNT_AUTHOR_RESP = 0x808003,//用户身份验证响应
		SERVERINFO_IP_QUERY = 0x800004,
		SERVERINFO_IP_QUERY_RESP = 0x808004,
		GMTOOLS_OperateLog_Query = 0x800005,//查看工具操作记录
		GMTOOLS_OperateLog_Query_RESP = 0x808005,//查看工具操作记录响应
        SERVERINFO_IP_ALL_QUERY = 0x800006,
        SERVERINFO_IP_ALL_QUERY_RESP = 0x808006,
        LINK_SERVERIP_CREATE = 0x800007,
        LINK_SERVERIP_CREATE_RESP = 0x808007,
        CLIENT_PATCH_COMPARE = 0x800008,
        CLIENT_PATCH_COMPARE_RESP = 0x808008,
        CLIENT_PATCH_UPDATE = 0x800009,
        CLIENT_PATCH_UPDATE_RESP = 0x808009,
		LINK_SERVERIP_DELETE = 0x800010,
		LINK_SERVERIP_DELETE_RESP = 0x808010,
		GMTOOLS_BUGLIST_QUERY = 0x800011,
		GMTOOLS_BUGLIST_QUERY_RESP = 0x808011,
		GMTOOLS_BUGLIST_INSERT = 0x800012,
		GMTOOLS_BUGLIST_INSERT_RESP = 0x808012,
		GMTOOLS_BUGLIST_UPDATE = 0x800013,
		GMTOOLS_BUGLIST_UPDATE_RESP = 0x808013,
		GMTOOLS_UPDATELIST_QUERY = 0x800014,
		GMTOOLS_UPDATELIST_QUERY_RESP = 0x808014,


		/// <summary>
		/// 用户管理模块(0x81)
		/// </summary>
		USER_CREATE = 0x810001,//创建GM帐号请求
		USER_CREATE_RESP = 0x818001,//创建GM帐号响应
		USER_UPDATE = 0x810002,//更新GM帐号信息请求
		USER_UPDATE_RESP = 0x818002,//更新GM帐号信息响应
		USER_DELETE = 0x810003,//删除GM帐号信息请求
		USER_DELETE_RESP = 0x818003,//删除GM帐号信息响应
		USER_QUERY = 0x810004,//查询GM帐号信息请求
		USER_QUERY_RESP = 0x818004,//查询GM帐号信息响应
		USER_PASSWD_MODIF = 0x810005,//密码修改请求
		USER_PASSWD_MODIF_RESP = 0x818005, //密码修改信息响应
		USER_QUERY_ALL = 0x810006,//查询所有GM帐号信息
		USER_QUERY_ALL_RESP = 0x818006,//查询所有GM帐号信息响应
		DEPART_QUERY = 0x810007, //查询部门列表
		DEPART_QUERY_RESP = 0x818007,//查询部门列表响应
        UPDATE_ACTIVEUSER = 0x810008,//更新在线用户状态
        UPDATE_ACTIVEUSER_RESP = 0x818008,//更新在线用户状态响应
		DEPARTMENT_CREATE = 0x810009,//部门创建
		DEPARTMENT_CREATE_RESP = 0x818009,//部门创建响应
		DEPARTMENT_UPDATE= 0x810010,//部门修改
		DEPARTMENT_UPDATE_RESP = 0x818010,//部门修改响应
		DEPARTMENT_DELETE= 0x810011,//部门删除
		DEPARTMENT_DELETE_RESP = 0x818011,//部门删除响应
		DEPARTMENT_ADMIN = 0x810012,//部门管理
		DEPARTMENT_ADMIN_RESP = 0x818012,//部门管理响应
		DEPARTMENT_RELATE_QUERY = 0x810013,//部门关联查询
		DEPARTMENT_RELATE_QUERY_RESP = 0x818013,//部门关联查询
		DEPART_RELATE_GAME_QUERY = 0x810014,
		DEPART_RELATE_GAME_QUERY_RESP = 0x818014,
		USER_SYSADMIN_QUERY = 0x810015,
		USER_SYSADMIN_QUERY_RESP = 0x818015,
		/// <summary>
		/// 模块管理(0x82)
		/// </summary>
		MODULE_CREATE = 0x820001,//创建模块信息请求
		MDDULE_CREATE_RESP = 0x828001,//创建模块信息响应
		MODULE_UPDATE =0x820002,//更新模块信息请求
		MODULE_UPDATE_RESP = 0x828002,//更新模块信息响应
		MODULE_DELETE = 0x820003,//删除模块请求
		MODULE_DELETE_RESP = 0x828003,//删除模块响应
		MODULE_QUERY = 0x820004,//查询模块信息的请求
		MODULE_QUERY_RESP = 0x828004,//查询模块信息的响应

		/// <summary>
		/// 用户与模块管理(0x83)
		/// </summary>
		USER_MODULE_CREATE = 0x830001,//创建用户与模块请求
		USER_MODULE_CREATE_RESP = 0x838001,//创建用户与模块响应
		USER_MODULE_UPDATE = 0x830002,//更新用户与模块的请求
		USER_MODULE_UPDATE_RESP = 0x838002,//更新用户与模块的响应
		USER_MODULE_DELETE = 0x830003,//删除用户与模块请求
		USER_MODULE_DELETE_RESP = 0x838003,//删除用户与模块响应
		USER_MODULE_QUERY = 0x830004,//查询用户所对应模块请求
		USER_MODULE_QUERY_RESP = 0x838004,//查询用户所对应模块响应

		/// <summary>
		/// 游戏管理(0x84)
		/// </summary>
		GAME_CREATE = 0x840001,//创建GM帐号请求
		GAME_CREATE_RESP = 0x848001,//创建GM帐号响应
		GAME_UPDATE = 0x840002,//更新GM帐号信息请求
		GAME_UPDATE_RESP = 0x848002,//更新GM帐号信息响应
		GAME_DELETE = 0x840003,//删除GM帐号信息请求
		GAME_DELETE_RESP = 0x848003,//删除GM帐号信息响应
		GAME_QUERY = 0x840004,//查询GM帐号信息请求
		GAME_QUERY_RESP = 0x848004,//查询GM帐号信息响应
		GAME_MODULE_QUERY = 0x840005,//查询游戏的模块列表
		GAME_MODULE_QUERY_RESP = 0x848005,//查询游戏的模块列表响应


		/// <summary>
		/// NOTES管理(0x85)
		/// </summary>
		NOTES_LETTER_TRANSFER = 0x850001, //取得邮件列表
		NOTES_LETTER_TRANSFER_RESP = 0x858001,//取得邮件列表的响应
		NOTES_LETTER_PROCESS = 0x850002, //邮件处理
		NOTES_LETTER_PROCESS_RESP = 0x858002,//邮件处理
		NOTES_LETTER_TRANSMIT = 0x850003, //邮件转发列表
		NOTES_LETTER_TRANSMIT_RESP = 0x858003,//邮件转发列表

		/// <summary>
		/// 猛将GM工具(0x86)
		/// </summary>
		MJ_CHARACTERINFO_QUERY = 0x860001,//检查玩家状态
		MJ_CHARACTERINFO_QUERY_RESP = 0x868001,//检查玩家状态响应
		MJ_CHARACTERINFO_UPDATE = 0x860002,//修改玩家状态
		MJ_CHARACTERINFO_UPDATE_RESP = 0x868002,//修改玩家状态响应
		MJ_LOGINTABLE_QUERY = 0x860003,//检查玩家是否在线
		MJ_LOGINTABLE_QUERY_RESP = 0x868003,//检查玩家是否在线响应
		MJ_CHARACTERINFO_EXPLOIT_UPDATE = 0x860004,//修改功勋值
		MJ_CHARACTERINFO_EXPLOIT_UPDATE_RESP = 0x868004,//修改功勋值响应
		MJ_CHARACTERINFO_FRIEND_QUERY = 0x860005,//列出好友名单
		MJ_CHARACTERINFO_FRIEND_QUERY_RESP = 0x868005,//列出好有名单响应
		MJ_CHARACTERINFO_FRIEND_CREATE = 0x860006,//添加好友
		MJ_CHARACTERINFO_FRIEND_CREATE_RESP = 0x868006,//添加好友响应
		MJ_CHARACTERINFO_FRIEND_DELETE = 0x860007,//删除好友
		MJ_CHARACTERINFO_FRIEND_DELETE_RESP = 0x868007,//删除好友响应
		MJ_GUILDTABLE_UPDATE = 0x860008,//修改服务器所有已存在帮会
		MJ_GUILDTABLE_UPDATE_RESP = 0x868008,//修改服务器所有已存在帮会响应
		MJ_ACCOUNT_LOCAL_CREATE = 0x860009,//将服务器上的account表里的玩家信息保存到本地服务器的
		MJ_ACCOUNT_LOCAL_CREATE_RESP = 0x868009,//将服务器上的account表里的玩家信息保存到本地服务器的响应
		MJ_ACCOUNT_REMOTE_DELETE = 0x860010,//永久封停帐号
		MJ_ACCOUNT_REMOTE_DELETE_RESP = 0x868010,//永久封停帐号的响应
		MJ_ACCOUNT_REMOTE_RESTORE = 0x860011,//解封帐号
		MJ_ACCOUNT_REMOTE_RESTORE_RESP = 0x868011,//解封帐号响应
		MJ_ACCOUNT_LIMIT_RESTORE = 0x860012,//有时限的封停
		MJ_ACCOUNT_LIMIT_RESTORE_RESP = 0x868012,//有时限的封停响应
		MJ_ACCOUNTPASSWD_LOCAL_CREATE = 0x860013,//保存玩家密码到本地 
		MJ_ACCOUNTPASSWD_LOCAL_CREATE_RESP = 0x868013,//保存玩家密码到本地
		MJ_ACCOUNTPASSWD_REMOTE_UPDATE = 0x860014,//修改玩家密码 
		MJ_ACCOUNTPASSWD_REMOTE_UPDATE_RESP = 0x868014,//修改玩家密码
		MJ_ACCOUNTPASSWD_REMOTE_RESTORE = 0x860015,//恢复玩家密码
		MJ_ACCOUNTPASSWD_REMOTE_RESTORE_RESP = 0x868015,//恢复玩家密码
		MJ_ITEMLOG_QUERY = 0x860016,//检查该用户交易记录
		MJ_ITEMLOG_QUERY_RESP = 0x868016,//检查该用户交易记录
		MJ_GMTOOLS_LOG_QUERY = 0x860017,//检查使用者操作记录
		MJ_GMTOOLS_LOG_QUERY_RESP = 0x868017,//检查使用者操作记录
		MJ_MONEYSORT_QUERY = 0x860018,//根据金钱排序
		MJ_MONEYSORT_QUERY_RESP = 0x868018,//根据金钱排序的响应
		MJ_LEVELSORT_QUERY = 0x860019,//根据等级排序
		MJ_LEVELSORT_QUERY_RESP = 0x868019,//根据等级排序的响应
		MJ_MONEYFIGHTERSORT_QUERY = 0x860020,//根据不同职业金钱排序
		MJ_MONEYFIGHTERSORT_QUERY_RESP = 0x868020,//根据不同职业金钱排序的响应
		MJ_LEVELFIGHTERSORT_QUERY = 0x860021,//根据不同职业等级排序
		MJ_LEVELFIGHTERSORT_QUERY_RESP = 0x868021,//根据不同职业等级排序的响应
		MJ_MONEYTAOISTSORT_QUERY = 0x860022,//根据道士金钱排序
		MJ_MONEYTAOISTSORT_QUERY_RESP = 0x868022,//根据道士金钱排序的响应
		MJ_LEVELTAOISTSORT_QUERY = 0x860023,//根据道士等级排序
		MJ_LEVELTAOISTSORT_QUERY_RESP = 0x868023,//根据道士等级排序的响应
		MJ_MONEYRABBISORT_QUERY = 0x860024,//根据法师金钱排序
		MJ_MONEYRABBISORT_QUERY_RESP = 0x868024,//根据法师金钱排序的响应
		MJ_LEVELRABBISORT_QUERY = 0x860025,//根据法师等级排序
		MJ_LEVELRABBISORT_QUERY_RESP = 0x868025,//根据法师等级排序的响应
		MJ_ACCOUNT_QUERY =  0x860026,//猛将帐号查询
		MJ_ACCOUNT_QUERY_RESP = 0x868026,//猛将帐号查询响应
        MJ_ACCOUNT_LOCAL_QUERY = 0x860027,//查询猛将本地帐号
        MJ_ACCOUNT_LOCAL_QUERY_RESP = 0x868027,//查询猛将本地帐号响应

        /// <summary>
        /// 超级舞者GM工具(0x87)
        /// </summary>
		SDO_ACCOUNT_QUERY = 0x870026,//查看玩家的帐号信息
		SDO_ACCOUNT_QUERY_RESP = 0x878026,//查看玩家的帐号信息响应
		SDO_CHARACTERINFO_QUERY = 0x870027,//查看任务资料的信息
		SDO_CHARACTERINFO_QUERY_RESP = 0x878027,//查看人物资料的信息响应
		SDO_ACCOUNT_CLOSE = 0x870028,//封停帐户的权限信息
		SDO_ACCOUNT_CLOSE_RESP = 0x878028,//封停帐户的权限信息响应
		SDO_ACCOUNT_OPEN = 0x870029,//解封帐户的权限信息
		SDO_ACCOUNT_OPEN_RESP = 0x878029,//解封帐户的权限信息响应
		SDO_PASSWORD_RECOVERY = 0x870030,//玩家找回密码
		SDO_PASSWORD_RECOVERY_RESP = 0x878030,//玩家找回密码响应
		SDO_CONSUME_QUERY = 0x870031,//查看玩家的消费记录
		SDO_CONSUME_QUERY_RESP = 0x878031,//查看玩家的消费记录响应
		SDO_USERONLINE_QUERY = 0x870032,//查看玩家上下线状态
		SDO_USERONLINE_QUERY_RESP = 0x878032,//查看玩家上下线状态响应
		SDO_USERTRADE_QUERY = 0x870033,//查看玩家交易状态
		SDO_USERTRADE_QUERY_RESP = 0x878033,//查看玩家交易状态响应
		SDO_CHARACTERINFO_UPDATE = 0x870034,//修改玩家的账号信息
		SDO_CHARACTERINFO_UPDATE_RESP = 0x878034,//修改玩家的账号信息响应
		SDO_ITEMSHOP_QUERY = 0x870035,//查看游戏里面所有道具信息
		SDO_ITEMSHOP_QUERY_RESP = 0x878035,//查看游戏里面所有道具信息响应
		SDO_ITEMSHOP_DELETE = 0x870036,//删除玩家道具信息
		SDO_ITEMSHOP_DELETE_RESP  = 0x878036,//删除玩家道具信息响应
		SDO_GIFTBOX_CREATE  = 0x870037,//添加玩家礼物盒道具信息
		SDO_GIFTBOX_CREATE_RESP  = 0x878037,//添加玩家礼物盒道具信息响应
		SDO_GIFTBOX_QUERY = 0x870038,//查看玩家礼物盒的道具
		SDO_GIFTBOX_QUERY_RESP = 0x878038,//查看玩家礼物盒的道具响应
		SDO_GIFTBOX_DELETE = 0x870039,//删除玩家礼物盒的道具
		SDO_GIFTBOX_DELETE_RESP = 0x878039,//删除玩家礼物盒的道具响应
		SDO_USERLOGIN_STATUS_QUERY = 0x870040,//查看玩家登录状态
		SDO_USERLOGIN_STATUS_QUERY_RESP = 0x878040,//查看玩家登录状态响应
		SDO_ITEMSHOP_BYOWNER_QUERY = 0x870041,////查看玩家身上道具信息
		SDO_ITEMSHOP_BYOWNER_QUERY_RESP = 0x878041,////查看玩家身上道具信息
		SDO_ITEMSHOP_TRADE_QUERY = 0x870042,//查看玩家交易记录信息
		SDO_ITEMSHOP_TRADE_QUERY_RESP = 0x878042,//查看玩家交易记录信息的响应
		SDO_MEMBERSTOPSTATUS_QUERY = 0x870043,//查看该帐号状态
		SDO_MEMBERSTOPSTATUS_QUERY_RESP = 0x878043,///查看该帐号状态的响应
        SDO_MEMBERBANISHMENT_QUERY = 0x870044,//查看所有停封的帐号
        SDO_MEMBERBANISHMENT_QUERY_RESP = 0x878044,//查看所有停封的帐号响应
        SDO_USERMCASH_QUERY = 0x870045,//玩家充值记录查询
        SDO_USERMCASH_QUERY_RESP = 0x878045,//玩家充值记录查询响应
        SDO_USERGCASH_UPDATE = 0x870046,//补偿玩家G币
        SDO_USERGCASH_UPDATE_RESP = 0x878046,//补偿玩家G币的响应
        SDO_MEMBERLOCAL_BANISHMENT= 0x870047,//本地保存停封信息
        SDO_MEMBERLOCAL_BANISHMENT_RESP = 0x878047,//本地保存停封信息响应
        SDO_EMAIL_QUERY = 0x870048,//得到玩家的EMAIL
        SDO_EMAIL_QUERY_RESP = 0x878048,//得到玩家的EMAIL响应
        SDO_USERCHARAGESUM_QUERY = 0x870049,//得到充值记录总和
        SDO_USERCHARAGESUM_QUERY_RESP = 0x878049,//得到充值记录总和响应
        SDO_USERCONSUMESUM_QUERY = 0x870050,//得到消费记录总和
        SDO_USERCONSUMESUM_QUERY_RESP = 0x878050,//得到消费记录总和响应
        SDO_USERGCASH_QUERY = 0x870051,//玩家Ｇ币记录查询
        SDO_USERGCASH_QUERY_RESP = 0x878051,//玩家Ｇ币记录查询响应
		SDO_CHALLENGE_QUERY = 0x870052,
		SDO_CHALLENGE_QUERY_RESP = 0x878052,
		SDO_CHALLENGE_INSERT = 0x870053,
		SDO_CHALLENGE_INSERT_RESP = 0x878053,
		SDO_CHALLENGE_UPDATE = 0x870054,
		SDO_CHALLENGE_UPDATE_RESP = 0x878054,
		SDO_CHALLENGE_DELETE = 0x870055,
		SDO_CHALLENGE_DELETE_RESP = 0x878055,
		SDO_MUSICDATA_QUERY = 0x870056,
		SDO_MUSICDATA_QUERY_RESP = 0x878056,
		SDO_MUSICDATA_OWN_QUERY = 0x870057,
		SDO_MUSICDATA_OWN_QUERY_RESP = 0x878057,
		SDO_CHALLENGE_SCENE_QUERY = 0x870058,
		SDO_CHALLENGE_SCENE_QUERY_RESP = 0x878058,
		SDO_CHALLENGE_SCENE_CREATE = 0x870059,
		SDO_CHALLENGE_SCENE_CREATE_RESP = 0x878059,
		SDO_CHALLENGE_SCENE_UPDATE = 0x870060,
		SDO_CHALLENGE_SCENE_UPDATE_RESP = 0x878060,
		SDO_CHALLENGE_SCENE_DELETE = 0x870061,
		SDO_CHALLENGE_SCENE_DELETE_RESP = 0x878061,
		SDO_MEDALITEM_CREATE = 0x870062,
		SDO_MEDALITEM_CREATE_RESP = 0x878062,
		SDO_MEDALITEM_UPDATE = 0x870063,
		SDO_MEDALITEM_UPDATE_RESP = 0x878063,
		SDO_MEDALITEM_DELETE = 0x870064,
		SDO_MEDALITEM_DELETE_RESP = 0x878064,
		SDO_MEDALITEM_QUERY = 0x870065,
		SDO_MEDALITEM_QUERY_RESP = 0x878065,
		SDO_MEDALITEM_OWNER_QUERY = 0x870066,
		SDO_MEDALITEM_OWNER_QUERY_RESP = 0x878066,
		SDO_MEMBERDANCE_OPEN = 0x870067,
		SDO_MEMBERDANCE_OPEN_RESP = 0x878067,
		SDO_MEMBERDANCE_CLOSE = 0x870068,
		SDO_MEMBERDANCE_CLOSE_RESP = 0x878068,
		SDO_USERNICK_UPDATE =0x870069, 
		SDO_USERNICK_UPDATE_RESP =0x878069, 
		SDO_PADKEYWORD_QUERY = 0x870070,
		SDO_PADKEYWORD_QUERY_RESP = 0x878070,
		SDO_BOARDMESSAGE_REQ = 0x870071,
		SDO_BOARDMESSAGE_REQ_RESP = 0x878071,
		SDO_CHANNELLIST_QUERY =  0x870072,
		SDO_CHANNELLIST_QUERY_RESP = 0x878072,
		SDO_ALIVE_REQ = 0x870073,
		SDO_ALIVE_REQ_RESP = 0x878073,
		SDO_BOARDTASK_QUERY = 0x870074,
		SDO_BOARDTASK_QUERY_RESP = 0x878074,
		SDO_BOARDTASK_UPDATE = 0x870075,
		SDO_BOARDTASK_UPDATE_RESP = 0x878075,
		SDO_BOARDTASK_INSERT = 0x870076,
		SDO_BOARDTASK_INSERT_RESP = 0x878076,
		SDO_DAYSLIMIT_QUERY = 0x870077,
		SDO_DAYSLIMIT_QUERY_RESP = 0x878077,

		/// <summary>
		/// 劲舞团GM工具(0x88)
		/// </summary>
		AU_ACCOUNT_QUERY = 0x880001,//玩家帐号信息查询
		AU_ACCOUNT_QUERY_RESP = 0x888001,//玩家帐号信息查询响应
		AU_ACCOUNTREMOTE_QUERY = 0x880002,//游戏服务器封停的玩家帐号查询
		AU_ACCOUNTREMOTE_QUERY_RESP = 0x888002,//游戏服务器封停的玩家帐号查询响应
		AU_ACCOUNTLOCAL_QUERY = 0x880003,//本地封停的玩家帐号查询
		AU_ACCOUNTLOCAL_QUERY_RESP = 0x888003,//本地封停的玩家帐号查询响应
		AU_ACCOUNT_CLOSE = 0x880004,//封停的玩家帐号
		AU_ACCOUNT_CLOSE_RESP = 0x888004,//封停的玩家帐号响应
		AU_ACCOUNT_OPEN = 0x880005,//解封的玩家帐号
		AU_ACCOUNT_OPEN_RESP= 0x888005,//解封的玩家帐号响应
		AU_ACCOUNT_BANISHMENT_QUERY = 0x880006,//玩家封停帐号查询
		AU_ACCOUNT_BANISHMENT_QUERY_RESP = 0x888006,//玩家封停帐号查询响应
		AU_CHARACTERINFO_QUERY = 0x880007,//查询玩家的账号信息
		AU_CHARACTERINFO_QUERY_RESP = 0x888007,//查询玩家的账号信息响应
		AU_CHARACTERINFO_UPDATE = 0x880008,//修改玩家的账号信息
		AU_CHARACTERINFO_UPDATE_RESP = 0x888008,//修改玩家的账号信息响应
		AU_ITEMSHOP_QUERY = 0x880009,//查看游戏里面所有道具信息
		AU_ITEMSHOP_QUERY_RESP = 0x888009,//查看游戏里面所有道具信息响应
		AU_ITEMSHOP_DELETE = 0x880010,//删除玩家道具信息
		AU_ITEMSHOP_DELETE_RESP = 0x888010,//删除玩家道具信息响应
		AU_ITEMSHOP_BYOWNER_QUERY = 0x880011,////查看玩家身上道具信息
		AU_ITEMSHOP_BYOWNER_QUERY_RESP = 0x888011,////查看玩家身上道具信息
		AU_ITEMSHOP_TRADE_QUERY = 0x880012,//查看玩家交易记录信息
		AU_ITEMSHOP_TRADE_QUERY_RESP = 0x888012,//查看玩家交易记录信息的响应
		AU_ITEMSHOP_CREATE = 0x880013,//添加玩家礼物盒道具信息
		AU_ITEMSHOP_CREATE_RESP = 0x888013,//添加玩家礼物盒道具信息响应
		AU_LEVELEXP_QUERY = 0x880014,//查看玩家等级经验
		AU_LEVELEXP_QUERY_RESP = 0x888014,////查看玩家等级经验响应
		AU_USERLOGIN_STATUS_QUERY = 0x880015,//查看玩家登录状态
		AU_USERLOGIN_STATUS_QUERY_RESP = 0x888015,//查看玩家登录状态响应
		AU_USERCHARAGESUM_QUERY = 0x880016,//得到充值记录总和
		AU_USERCHARAGESUM_QUERY_RESP = 0x888016,//得到充值记录总和响应
		AU_CONSUME_QUERY = 0x880017,//查看玩家的消费记录
		AU_CONSUME_QUERY_RESP = 0x888017,//查看玩家的消费记录响应
		AU_USERCONSUMESUM_QUERY = 0x880018,//得到消费记录总和
		AU_USERCONSUMESUM_QUERY_RESP = 0x888018,//得到消费记录总和响应
		AU_USERMCASH_QUERY = 0x880019,//玩家充值记录查询
		AU_USERMCASH_QUERY_RESP = 0x888019,//玩家充值记录查询响应
		AU_USERGCASH_QUERY = 0x880020,//玩家Ｇ币记录查询
		AU_USERGCASH_QUERY_RESP = 0x888020,//玩家Ｇ币记录查询响应
		AU_USERGCASH_UPDATE = 0x880021,//补偿玩家G币
		AU_USERGCASH_UPDATE_RESP = 0x888021,//补偿玩家G币的响应
		AU_USERNICK_UPDATE = 0x880022,
		AU_USERNICK_UPDATE_RESP = 0x888022,

		/// <summary>
		/// 疯狂卡丁车GM工具(0x89)
		/// </summary>
		CR_ACCOUNT_QUERY = 0x890001,//玩家帐号信息查询
		CR_ACCOUNT_QUERY_RESP = 0x898001,//玩家帐号信息查询响应
		CR_ACCOUNTACTIVE_QUERY = 0x890002,//玩家帐号激活信息
		CR_ACCOUNTACTIVE_QUERY_RESP = 0x898002,//玩家帐号激活响应
		CR_CALLBOARD_QUERY = 0x890003,//公告信息查询
		CR_CALLBOARD_QUERY_RESP = 0x898003,//公告信息查询响应
		CR_CALLBOARD_CREATE = 0x890004,//发布公告
		CR_CALLBOARD_CREATE_RESP = 0x898004,//发布公告响应
		CR_CALLBOARD_UPDATE = 0x890005,//更新公告信息
		CR_CALLBOARD_UPDATE_RESP = 0x898005,//更新公告信息的响应
		CR_CALLBOARD_DELETE = 0x890006,//删除公告信息
		CR_CALLBOARD_DELETE_RESP = 0x898006,//删除公告信息的响应
		CR_CHARACTERINFO_QUERY = 0x890007,//玩家角色信息查询
		CR_CHARACTERINFO_QUERY_RESP = 0x898007,//玩家角色信息查询的响应
		CR_CHARACTERINFO_UPDATE = 0x890008,//玩家角色信息查询
		CR_CHARACTERINFO_UPDATE_RESP = 0x898008,//玩家角色信息查询的响应
		CR_CHANNEL_QUERY = 0x890009,//公告频道查询
		CR_CHANNEL_QUERY_RESP = 0x898009,//公告频道查询的响应
		CR_NICKNAME_QUERY = 0x890010,//玩家昵称查询
		CR_NICKNAME_QUERY_RESP = 0x898010,//玩家昵称的响应
		CR_LOGIN_LOGOUT_QUERY = 0x890011,//玩家上线、下线时间查询
		CR_LOGIN_LOGOUT_QUERY_RESP = 0x898011,//玩家上线、下线时间查询的响应
		CR_ERRORCHANNEL_QUERY = 0x890012,//补充错误公告频道查询
		CR_ERRORCHANNEL_QUERY_RESP = 0x898012,//补充错误公告频道查询的响应

		/// <summary>
		/// 充值消费GM工具(0x90)
		/// </summary>
		CARD_USERCHARGEDETAIL_QUERY = 0x900001,//一卡通查询
		CARD_USERCHARGEDETAIL_QUERY_RESP = 0x908001,//一卡通查询响应
		CARD_USERDETAIL_QUERY = 0x900002,//久之游用户注册信息查询
		CARD_USERDETAIL_QUERY_RESP = 0x908002,////久之游用户注册信息查询响应
		CARD_USERCONSUME_QUERY =0x900003,//休闲币消费查询
		CARD_USERCONSUME_QUERY_RESP = 0x908003,//休闲币消费查询响应
		CARD_VNETCHARGE_QUERY = 0x900004,//互联星空充值查询
		CARD_VNETCHARGE_QUERY_RESP = 0x908004,//互联星空充值查询响应
		CARD_USERDETAIL_SUM_QUERY = 0x900005,//充值合计查询
		CARD_USERDETAIL_SUM_QUERY_RESP = 0x908005,//充值合计查询响应
		CARD_USERCONSUME_SUM_QUERY = 0x900006,//消费合计查询
		CARD_USERCONSUME_SUM_QUERY_RESP = 0x908006,//消费合计响应
		CARD_USERINFO_QUERY = 0x900007,//玩家注册信息查询
		CARD_USERINFO_QUERY_RESP = 0x908007,//玩家注册信息查询响应
		CARD_USERINFO_CLEAR = 0x900008,//重置玩家身份证信息
		CARD_USERINFO_CLEAR_RESP = 0x908008,//重置玩家身份证信息响应
		CARD_USERSECURE_CLEAR = 0x900009,//重置玩家安全码信息
		CARD_USERSECURE_CLEAR_RESP = 0x908009,//重置玩家安全码信息响应
		CARD_USERNICK_QUERY = 0x900010,
		CARD_USERNICK_QUERY_RESP = 0x908010,
		CARD_USERLOCK_UPDATE = 0x900011,
		CARD_USERLOCK_UPDATE_RESP = 0x908011,


        /// <summary>
        /// 劲舞团商城(0x91)
        /// </summary>
        AUSHOP_USERGPURCHASE_QUERY = 0x910001,//用户G币购买记录
        AUSHOP_USERGPURCHASE_QUERY_RESP = 0x918001,//用户G币购买记录
        AUSHOP_USERMPURCHASE_QUERY = 0x910002,//用户M币购买记录
        AUSHOP_USERMPURCHASE_QUERY_RESP = 0x918002,//用户M币购买记录
        AUSHOP_AVATARECOVER_QUERY = 0x910003,//道具回收兑换记
        AUSHOP_AVATARECOVER_QUERY_RESP = 0x918003,//道具回收兑换记
        AUSHOP_USERINTERGRAL_QUERY = 0x910004,//用户积分记录
        AUSHOP_USERINTERGRAL_QUERY_RESP = 0x918004,//用户积分记录
        AUSHOP_USERGPURCHASE_SUM_QUERY = 0x910005,//用户G币购买记录合计
        AUSHOP_USERGPURCHASE_SUM_QUERY_RESP = 0x918005,//用户G币购买记录合计响应
        AUSHOP_USERMPURCHASE_SUM_QUERY = 0x910006,//用户M币购买记录合计
        AUSHOP_USERMPURCHASE_SUM_QUERY_RESP = 0x918006,//用户M币购买记录合计响应
		AUSHOP_AVATARECOVER_DETAIL_QUERY = 0x910007,//道具回收兑换详细记录
		AUSHOP_AVATARECOVER_DETAIL_QUERY_RESP = 0x918007,//道具回收兑换详细记录

		/// <summary>
		/// 劲乐团工具(0x92)
		/// </summary>
		O2JAM_CHARACTERINFO_QUERY= 0x920001,//玩家角色信息查询
	    O2JAM_CHARACTERINFO_QUERY_RESP= 0x928001,//玩家角色信息查询
		O2JAM_CHARACTERINFO_UPDATE= 0x920002,//玩家角色信息更新
		O2JAM_CHARACTERINFO_UPDATE_RESP= 0x928002,//玩家角色信息更新
		O2JAM_ITEM_QUERY= 0x920003,//玩家道具信息查询
		O2JAM_ITEM_QUERY_RESP= 0x928003,//玩家角色信息查询
		O2JAM_ITEM_UPDATE= 0x920004,//玩家道具信息更新
		O2JAM_ITEM_UPDATE_RESP= 0x928004,//玩家道具信息更新
		O2JAM_CONSUME_QUERY= 0x920005,//玩家消费信息查询
		O2JAM_CONSUME_QUERY_RESP= 0x928005,//玩家消费信息查询
		O2JAM_ITEMDATA_QUERY= 0x920006,//道具列表查询
		O2JAM_ITEMDATA_QUERY_RESP= 0x928006,//道具列表信息查询
		O2JAM_GIFTBOX_QUERY = 0x920007,//玩家礼物盒查询
		O2JAM_GIFTBOX_QUERY_RESP = 0x928007,//玩家礼物盒查询
		O2JAM_USERGCASH_UPDATE = 0x920008,//补偿玩家G币
		O2JAM_USERGCASH_UPDATE_RESP = 0x928008,//补偿玩家G币的响应
		O2JAM_CONSUME_SUM_QUERY= 0x920009,//玩家消费信息查询
		O2JAM_CONSUME_SUM_QUERY_RESP= 0x928009,//玩家消费信息查询
		O2JAM_GIFTBOX_CREATE= 0x920010,//添加玩家礼物盒道具
		O2JAM_GIFTBOX_CREATE_RESP= 0x928010,//添加玩家礼物盒道具
		O2JAM_ITEMNAME_QUERY = 0x920011,
		O2JAM_ITEMNAME_QUERY_RESP = 0x928011,
		O2JAM_GIFTBOX_DELETE = 0x920012,
		O2JAM_GIFTBOX_DELETE_RESP  =0x928012,

		/// <summary>
		/// 劲乐团IIGM工具(0x93)
		/// </summary>
		O2JAM2_ACCOUNT_QUERY = 0x930001,//玩家帐号信息查询
		O2JAM2_ACCOUNT_QUERY_RESP = 0x938001,//玩家帐号信息查询响应
		O2JAM2_ACCOUNTACTIVE_QUERY = 0x930002,//玩家帐号激活信息
		O2JAM2_ACCOUNTACTIVE_QUERY_RESP = 0x938002,//玩家帐号激活响应
		O2JAM2_CHARACTERINFO_QUERY = 0x930003,//用户信息查询
		O2JAM2_CHARACTERINFO_QUERY_RESP = 0x938003,
		O2JAM2_CHARACTERINFO_UPDATE = 0x930004,//用户信息修改
		O2JAM2_CHARACTERINFO_UPDATE_RESP = 0x938004,
		O2JAM2_ITEMSHOP_QUERY = 0x930005,
		O2JAM2_ITEMSHOP_QUERY_RESP = 0x938005,
		O2JAM2_ITEMSHOP_DELETE = 0x930006,
		O2JAM2_ITEMSHOP_DELETE_RESP = 0x938006,
		O2JAM2_MESSAGE_QUERY = 0x930007,
		O2JAM2_MESSAGE_QUERY_RESP = 0x938007,
		O2JAM2_MESSAGE_CREATE = 0x930008,
		O2JAM2_MESSAGE_CREATE_RESP = 0x938008,
		O2JAM2_MESSAGE_DELETE = 0x930009,
		O2JAM2_MESSAGE_DELETE_RESP = 0x938009,
		O2JAM2_CONSUME_QUERY = 0x930010,
		O2JAM2_CONUMSE_QUERY_RESP = 0x938010,
		O2JAM2_CONSUME_SUM_QUERY = 0x930011,
		O2JAM2_CONUMSE_QUERY_SUM_RESP = 0x938011,
		O2JAM2_TRADE_QUERY = 0x930012,
		O2JAM2_TRADE_QUERY_RESP = 0x938012,
		O2JAM2_TRADE_SUM_QUERY = 0x930013,
		O2JAM2_TRADE_QUERY_SUM_RESP = 0x938013,
		O2JAM2_AVATORLIST_QUERY = 0x930014,
		O2JAM2_AVATORLIST_QUERY_RESP = 0x938014,
		O2JAM2_ACCOUNT_CLOSE = 0x930015,//封停帐户的权限信息
		O2JAM2_ACCOUNT_CLOSE_RESP = 0x938015,//封停帐户的权限信息响应
		O2JAM2_ACCOUNT_OPEN = 0x930016,//解封帐户的权限信息
		O2JAM2_ACCOUNT_OPEN_RESP = 0x938016,//解封帐户的权限信息响应
		O2JAM2_MEMBERBANISHMENT_QUERY = 0x930017,
		O2JAM2_MEMBERBANISHMENT_QUERY_RESP = 0x938017,
		O2JAM2_MEMBERSTOPSTATUS_QUERY = 0x930018,
		O2JAM2_MEMBERSTOPSTATUS_QUERY_RESP = 0x938018,
		O2JAM2_MEMBERLOCAL_BANISHMENT = 0x930019,
		O2JAM2_MEMBERLOCAL_BANISHMENT_RESP = 0x938019,
		O2JAM2_USERLOGIN_DELETE = 0x930020,
		O2JAM2_USERLOGIN_DELETE_RESP = 0x938020,
		O2JAM2_LEVELEXP_QUERY = 0x930021,
		O2JAM2_LEVELEXP_QUERY_RESP =  0x938021,

		/// <summary>
		/// 劲爆足球 Add by KeHuaQing 2006-09-14
		/// </summary>
		SOCCER_CHARACTERINFO_QUERY = 0x940001,//用户信息查询
		SOCCER_CHARACTERINFO_QUERY_RESP = 0x948001,
		SOCCER_CHARCHECK_QUERY = 0x940002,//用户NameCheck,SocketCheck
		SOCCER_CHARCHECK_QUERY_RESP = 0x948002,
		SOCCER_CHARITEMS_RECOVERY_QUERY = 0x940003,//恢复角色信息
		SOCCER_CHARITEMS_RECOVERY_QUERY_RESP = 0x948003,
		SOCCER_CHARPOINT_QUERY = 0x940004,//用户G币修改
		SOCCER_CHARPOINT_QUERY_RESP = 0x948004,
		SOCCER_DELETEDCHARACTERINFO_QUERY = 0x940005,//删除用户查询
		SOCCER_DELETEDCHARACTERINFO_QUERY_RESP = 0x948005,
		SOCCER_CHARACTERSTATE_MODIFY = 0x940006,//停封角色
		SOCCER_CHARACTERSTATE_MODIFY_RESP = 0x948006,
		SOCCER_ACCOUNTSTATE_MODIFY = 0x940007,//停封玩家
		SOCCER_ACCOUNTSTATE_MODIFY_RESP = 0x948007,
		SOCCER_CHARACTERSTATE_QUERY = 0x940008,//停封角色查询
		SOCCER_CHARACTERSTATE_QUERY_RESP = 0x948008,
		SOCCER_ACCOUNTSTATE_QUERY = 0x940009,//停封玩家查询
		SOCCER_ACCOUNTSTATE_QUERY_RESP = 0x948009,

		NOTDEFINED = 0x0,
		ERROR = 0xFFFFFF
	}
	/// <summary>
	/// Message 的摘要说明。
	/// </summary>
	public class Message
	{
		/// <summary>
		/// 消息Byte数组
		/// </summary>
		public byte[] m_bMessageBuffer;
		/// <summary>
		/// 消息byte长度
		/// </summary>
		public uint m_uiMessageLen;
		/// <summary>
		/// 消息包
		/// </summary>
		public Packet m_packet;
		/// <summary>
		/// 是否是合法消息
		/// </summary>
		public bool IsValidMessage = false;
		
		public Message()
		{
		}
		/// <summary>
		/// 构造消息包、消息体、消息头，解析可读的消息
		/// </summary>
		/// <param name="buffer">数据流</param>
		/// <param name="len">数据流长度</param>
		public Message(byte[] buffer , uint len)
		{
			if (buffer == null || buffer.Length != len)
				return;
			this.m_bMessageBuffer = buffer;
			this.m_uiMessageLen = len;
			this.IsValidMessage = this.DecodeMessage();
		}
		/// <summary>
		/// 消息解码
		/// 将消息开始FE和消息结尾EF去除
		/// 并且将消息体中间出现FE用0xFD和0x01,EF用0xFD和0xF2去除
		/// </summary>
		/// <returns>结果</returns>
		private bool DecodeMessage() 
		{			
			int head = 0 , tail = 0;
			for ( ; head < m_uiMessageLen ; head ++ )
				if ( m_bMessageBuffer[ head ] == 0xFE )
					break;
			for ( tail = head ; tail < m_uiMessageLen ; tail ++ )
				if ( m_bMessageBuffer[ tail ] == 0xEF )
					break;
			// tail extends the packet size , the packet corrupts.
			if ( tail >= m_uiMessageLen )
				return false;

			System.Collections.ArrayList dest = new System.Collections.ArrayList();
			for ( int i = head + 1 ; i < tail ; i ++ ) 
			{
				if ( m_bMessageBuffer[ i ] == 0xFD ) 
				{
					switch ( m_bMessageBuffer[ ++i ] ) 
					{
						case 0x01: 
							dest.Add((byte)0xFE);
							break;
						case 0x00: 
							dest.Add((byte)0xFD);
							break;
						case 0xF2: 
							dest.Add((byte)0xEF);
							break;
						default: 
							return false;
					}
				}
				else					
					dest.Add(m_bMessageBuffer[ i ]);
			}
			int size = dest.Count;
			byte[] buffer = new byte[size];
			for (int i = 0;i < size;i ++)
				buffer[i] = (byte)dest[i];
			this.m_packet = new Packet(buffer,(uint)size);
			if (this.m_packet.IsValidPacket)
				return true;
			else
				return false;
		}
        /// <summary>
        /// 构造消息包，将消息包封装
        /// </summary>
        /// <param name="packet">消息包</param>
		public Message(Packet packet)
		{
			if (packet == null || !packet.IsValidPacket)
				return;
			this.m_packet = packet;
			this.IsValidMessage = this.EncodeMessage();
		}
        /// <summary>
        /// 封装消息，在消息包头加上FE消息尾加上EF，
        /// 如果消息体中间出现FE用0xFD和0x01,EF用0xFD和0xF2
        /// </summary>
        /// <returns></returns>
		private bool EncodeMessage() 
		{
			if (this.m_packet == null || !this.m_packet.IsValidPacket)
				return false;
			byte[] source = this.m_packet.ToByteArray();

			System.Collections.ArrayList dest = new System.Collections.ArrayList();
			dest.Add((byte)0xFE);
			for (int i = 0;i < source.Length;i++)
			{
				switch ( source[ i ] ) 
				{
					case 0xFE: 
						dest.Add((byte)0xFD);
						dest.Add((byte)0x01);
						break;
					case 0xEF: 
						dest.Add((byte)0xFD);
						dest.Add((byte)0xF2);
						break;
					case 0xFD: 
						dest.Add((byte)0xFD);
						dest.Add((byte)0x00);
						break;
					default :
						dest.Add((byte)source[i]);
						break;
				}
			}
			dest.Add((byte)0xEF);
			int size = dest.Count;
			this.m_uiMessageLen = (uint)size;
			this.m_bMessageBuffer = new byte[size];
			for (int i = 0;i < size;i ++)
				this.m_bMessageBuffer[i] = (byte)dest[i];
			return true;
		}

		/// <summary>
		/// 得到消息类型ID
		/// </summary>
		/// <returns>消息类型ID</returns>
		public Message_Tag_ID GetMessageID()
		{
			if (!this.IsValidMessage)
				return Message_Tag_ID.ERROR;
			uint uiID;
			byte bCategory = (byte)this.m_packet.m_Head.m_mcCategory;
			ushort bServicekey = (ushort)this.m_packet.m_Head.m_skServiceKey;
			uiID = (uint)(bCategory << 16) + (uint)bServicekey;
			return (Message_Tag_ID)uiID;
		}
        
		public byte[] ToByteArray()
		{
			if (!this.IsValidMessage)
				return new byte[0];
			return this.m_bMessageBuffer;
		}
		public override string ToString()
		{
			if (!this.IsValidMessage)
				return "Invalid Message\r\n";
			return this.m_packet.ToString();
		}
        /// <summary>
        /// 得到消息的结果集
        /// </summary>
        /// <param name="buffer">单个消息</param>
        /// <param name="len">长度</param>
        /// <param name="start">开始位置</param>
        /// <param name="end">结尾</param>
        /// <returns>消息结果集</returns>
		public static System.Collections.ArrayList GetMessage(byte[] buffer,uint len,ref int start,ref int end)
		{
			System.Collections.ArrayList al = new System.Collections.ArrayList();
			if (buffer == null|| buffer.Length != len)
				return al;
			int head = -1 , tail = -1;
			for (int i = 0;i < len; i++)
			{
				if (buffer[i] == 0xFE)
					head = i;
				else if (buffer[i] == 0xEF)
				{
					if (head == -1) continue;
					tail = i;
					int size = tail - head + 1;
					byte[] tmp = new byte[size];
					System.Array.Copy(buffer,head,tmp,0,size);
					al.Add(new Message(tmp,(uint)size));
					head = -1;
				}
			}
			start = head;
			end = tail;
			return al;
		}
		/// <summary>
		/// 将message类结构体写到XML文件里面
		/// </summary>
		/// <param name="filename">XML文件</param>
		/// <param name="message">Message消息类</param>
		/// <param name="append">是否加入消息集</param>
		/// <returns>操作结果</returns>
		public static bool WriteToXmlFile(string filename,Message message,bool append)
		{
			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(Message));
				System.IO.TextWriter writer = new System.IO.StreamWriter(filename,append);
				serializer.Serialize(writer,message);
				writer.Close();
				return true;
			}
			catch(System.Exception ex)
			{
				throw ex;
			}
		}
		/// <summary>
		/// 从XML文件读出数据转化成Message类结构
		/// </summary>
		/// <param name="filename">文件名</param>
		/// <returns>Message消息类</returns>
		public static Message GetFromXmlFile(string filename)
		{
			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(Message));			
				System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open);
				return (Message) serializer.Deserialize(fs);
			}
			catch(System.Exception ex)
			{
				throw ex;
			}
		}
		/// <summary>
		/// CLIENT端发送连接的响应
		/// </summary>
		/// <param name="status">连接响应的信息</param>
		/// <returns>消息包</returns>
		public static Message Common_CONNECT_RESP(string status)
		{
			try
			{
				byte[] baMsg_Msg = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,status);
				TLV_Structure Msg_msg= new TLV_Structure(TagName.Connect_Msg,(uint)baMsg_Msg.Length,baMsg_Msg);
				Packet_Body body = new Packet_Body(new TLV_Structure[]{Msg_msg},1);
				Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(),Msg_Category.COMMON,
					ServiceKey.CONNECT_RESP,body.m_uiBodyLen);
				return new Message(new Packet(head,body));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		/// <summary>
		/// CLIENT端发送主动断开响应
		/// </summary>
		/// <param name="userByID">用户登录ID</param>
		/// <param name="msg">断开的响应信息</param>
		/// <returns></returns>
		public static Message Common_DISCONNECT_RESP(int userByID,string msg)
		{
			try
			{
				byte[] bgMsg_UserByID = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,userByID);
				TLV_Structure Msg_UserID = new TLV_Structure(TagName.UserByID,(uint)bgMsg_UserByID.Length,bgMsg_UserByID);
				byte[] baMsg_Msg = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,msg);
				TLV_Structure Msg_msg= new TLV_Structure(TagName.DisConnect_Msg,(uint)baMsg_Msg.Length,baMsg_Msg);
				Packet_Body body = new Packet_Body(new TLV_Structure[]{Msg_UserID,Msg_msg},2);
				Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(),Msg_Category.COMMON,
					ServiceKey.DISCONNECT_RESP,body.m_uiBodyLen);
				return new Message(new Packet(head,body));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		/// <summary>
		/// 响应用户验证的消息
		/// </summary>
		/// <param name="userName">用户名</param>
		/// <param name="password">密码</param>
		/// <param name="mac">MAC码</param>
		/// <returns>包括以上信息的消息包</returns>
		public static Message Common_ACCOUNT_AUTHOR_RESP(int userByID,string status)
		{
			try
			{
				byte[] bgMsg_UserID = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,userByID);
				TLV_Structure Msg_UserID = new TLV_Structure(TagName.UserByID,(uint)bgMsg_UserID.Length,bgMsg_UserID);

				byte[] baMsg_Msg = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,status);
				TLV_Structure Msg_Msg = new TLV_Structure(TagName.Status,(uint)baMsg_Msg.Length,baMsg_Msg);
				Packet_Body body = new Packet_Body(new TLV_Structure[]{Msg_UserID,Msg_Msg},2);
				Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(),Msg_Category.COMMON,
					ServiceKey.ACCOUNT_AUTHOR_RESP,body.m_uiBodyLen);
				return new Message(new Packet(head,body));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		/// <summary>
		/// 响应创建新用户消息
		/// </summary>
		/// <returns>消息包</returns>
		public static Message Common_USER_CREATE_RESP(string status)
		{
			try
			{
				byte[] baMsg_Status = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,status);
				TLV_Structure Msg_Status = new TLV_Structure(TagName.Status,(uint)baMsg_Status.Length,baMsg_Status);
				Packet_Body body = new Packet_Body(new TLV_Structure[]{Msg_Status},1);
				Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(),Msg_Category.USER_ADMIN,
					ServiceKey.USER_CREATE_RESP,body.m_uiBodyLen);
				return new Message(new Packet(head,body));
			}
			catch (Exception ex)
			{
				throw ex;
			
			}
		}
		/// <summary>
		/// 响应用户修改密码的消息包
		/// </summary>
		/// <param name=" status">修改状态</param>
		/// <returns>消息包</returns>
		public static Message Common_USER_UPDATE_RESP(string status)
		{
			try
			{
				byte[] baMsg_Status = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,status);
				TLV_Structure Msg_Status = new TLV_Structure(TagName.Status,(uint)baMsg_Status.Length,baMsg_Status);
				Packet_Body body = new Packet_Body(new TLV_Structure[]{Msg_Status},1);
				Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(),Msg_Category.USER_ADMIN,
					ServiceKey.USER_CREATE_RESP,body.m_uiBodyLen);
				return new Message(new Packet(head,body));
			}
			catch (Exception ex)
			{
				throw ex;
			
			}
		}
		/// <summary>
		/// 响应删除GM帐号的消息包
		/// </summary>
		/// <param name="userID">用户ID</param>
		/// <param name="msg">操作信息</param>
		/// <returns>删除GM帐号的消息包</returns>
		public static Message Common_USER_DELETE_RESP(string status)
		{
			try
			{
				byte[] baMsg_Status = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,status);
				TLV_Structure Msg_Status = new TLV_Structure(TagName.Status,(uint)baMsg_Status.Length,baMsg_Status);
				Packet_Body body = new Packet_Body(new TLV_Structure[]{Msg_Status},1);
				Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(),Msg_Category.USER_ADMIN,
					ServiceKey.USER_DELETE_RESP,body.m_uiBodyLen);
				return new Message(new Packet(head,body));
			}
			catch (Exception ex)
			{
				throw ex;
			
			}
		}
		/// <summary>
		///  响应创建模块的消息
		/// </summary>

		/// <param name="status">状态值</param>
		/// <returns>创建模块的消息封装</returns>
		public static Message Common_MODULE_CREATE_RESP(string status)
		{
			try
			{
				byte[] baMsg_Status = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,status);
				TLV_Structure Msg_Status = new TLV_Structure(TagName.Status,(uint)baMsg_Status.Length,baMsg_Status);
				Packet_Body body = new Packet_Body(new TLV_Structure[]{Msg_Status},1);
				Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(),Msg_Category.MODULE_ADMIN,
					ServiceKey.MODULE_CREATE_RESP,body.m_uiBodyLen);
				return new Message(new Packet(head,body));
			}
			catch (Exception ex)
			{
				throw ex;
			
			}
		}
		/// <summary>
		///  响应修改模块的消息
		/// </summary>
		/// <param name="status">状态值</param>
		/// <param name="moduleContent">模块描述</param>
		/// <returns>修改模块的消息封装</returns>
		public static Message Common_MODULE_UPDATE_RESP(string status)
		{
			try
			{
				byte[] baMsg_Status = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,status);
				TLV_Structure Msg_Status = new TLV_Structure(TagName.Status,(uint)baMsg_Status.Length,baMsg_Status);
				Packet_Body body = new Packet_Body(new TLV_Structure[]{Msg_Status},1);
				Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(),Msg_Category.MODULE_ADMIN,
					ServiceKey.MODULE_UPDATE_RESP,body.m_uiBodyLen);
				return new Message(new Packet(head,body));
			}
			catch (Exception ex)
			{
				throw ex;
			
			}
		}
		/// <summary>
		///  响应删除模块的消息
		/// </summary>
		/// <param name="moduleID">模块ID</param>
		/// <param name="msg">操作结果</param>
		/// <returns>删除模块的消息封装</returns>
		public static Message Common_MODULE_DELETE_RESP(int moduleID,string msg)
		{
			try
			{
				byte[] bgMsg_ModuleID = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,moduleID);
				TLV_Structure Msg_ModuleID = new TLV_Structure(TagName.Module_ID,(uint)bgMsg_ModuleID.Length,bgMsg_ModuleID);
				byte[] baMsg_Status = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,msg);
				TLV_Structure Msg_Status = new TLV_Structure(TagName.Status,(uint)baMsg_Status.Length,baMsg_Status);
				Packet_Body body = new Packet_Body(new TLV_Structure[]{Msg_ModuleID,Msg_Status},2);
				Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(),Msg_Category.MODULE_ADMIN,
					ServiceKey.USER_DELETE_RESP,body.m_uiBodyLen);
				return new Message(new Packet(head,body));
			}
			catch (Exception ex)
			{
				throw ex;
			
			}
		}
		/// <summary>
		/// 操作失败消息的响应
		/// </summary>
		/// <param name="UserID">用户名</param>
		/// <param name="errorMsg">出错信息</param>
		/// <returns>出错消息</returns>
		public static Message ERROR_MES_RESP(int UserByID,string errorMsg)
		{
			try
			{
				byte[] bgMsg_UserNM = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,UserByID);
				TLV_Structure Msg_UserID = new TLV_Structure(TagName.UserByID,(uint)bgMsg_UserNM.Length,bgMsg_UserNM);
				byte[] baMsg_Error = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,errorMsg);
				TLV_Structure Msg_Error = new TLV_Structure(TagName.Status,(uint)baMsg_Error.Length,baMsg_Error);
				Packet_Body body = new Packet_Body(new TLV_Structure[]{Msg_UserID,Msg_Error},2);
				Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(),Msg_Category.ERROR,
					ServiceKey.ERROR,body.m_uiBodyLen);
				return new Message(new Packet(head,body));
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		/// <summary>
		/// 封装数据集的消息包
		/// </summary>
		/// <param name="queryList">输入数据列表</param>
		/// <param name="colCnt">输入列数</param>
		/// <returns>返回数据集的消息包</returns>
		public static Message COMMON_MES_RESP(Query_Structure[] queryList,Msg_Category category,ServiceKey service,int colCnt)
		{
			uint iPos = 0;
			TLV_Structure[] tlv = new TLV_Structure[queryList.Length*colCnt];
			int pos = 0;

			for(int i=0;i<queryList.Length;i++)
			{
				for(int j=0;j<colCnt;j++)
				{
					//消息元素格式
					TagFormat format_ = queryList[i].m_tagList[j].format;
					//消息元素名称
					TagName key_ = queryList[i].m_tagList[j].tag;
					//消息元素的值
                    byte[] bgMsg_Value = queryList[i].m_tagList[j].tag_buf;
					//消息的结构体
					TLV_Structure Msg_Value = new TLV_Structure(key_,format_,(uint)bgMsg_Value.Length,bgMsg_Value);
					tlv[pos++]=Msg_Value;
					iPos +=Msg_Value.m_uiValueLen;
				}
				/*if(iPos+20>=7192)
				{
					pos = i;
					break;
				}*/
			}
			//封装消息体
			Packet_Body body = new Packet_Body(tlv,(uint)tlv.Length,(uint)colCnt);
			//封装消息头
			Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(),category,
				service,body.m_uiBodyLen);
			return new Message(new Packet(head,body));
		}
		/// <summary>
		/// 封装数据集的消息包
		/// </summary>
		/// <param name="queryList">输入数据列表</param>
		/// <param name="colCnt">输入列数</param>
		/// <returns>返回数据集的消息包</returns>
		public static ArrayList QUERY_MES_RESP(Query_Structure[] queryList,Msg_Category category,ServiceKey service,int colCnt)
		{
			ArrayList list = new ArrayList();
			for(int i=0;i<queryList.Length;i++)
			{
				int pos =0;
				TLV_Structure[] tlv = new TLV_Structure[colCnt];
				for(int j=0;j<colCnt;j++)
				{
					//消息元素格式
					TagFormat format_ = queryList[i].m_tagList[j].format;
					//消息元素名称
					TagName key_ = queryList[i].m_tagList[j].tag;
					//消息元素的值
					byte[] bgMsg_Value = queryList[i].m_tagList[j].tag_buf;
					//消息的结构体
					TLV_Structure Msg_Value = new TLV_Structure(key_,format_,(uint)bgMsg_Value.Length,bgMsg_Value);
					tlv[pos++]=Msg_Value;
				}
				//封装消息体
				Packet_Body body = new Packet_Body(tlv,(uint)tlv.Length,(uint)colCnt);
				//封装消息头
				Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(),category,
					service,body.m_uiBodyLen);
				list.Add(new Message(new Packet(head,body)));

			}
			return list;
		}
		/// <summary>
		/// 返回数据列表消息定义
		/// </summary>
		/// <param name="status">状态值</param>
		/// <returns>消息结构</returns>
		public static Message COMMON_MES_RESP(string status,Msg_Category cateGory,ServiceKey serviceKey)
		{
				byte[] bgMsg_status = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,status);
				TLV_Structure Msg_status = new TLV_Structure(TagName.Status,(uint)bgMsg_status.Length,bgMsg_status);
			Packet_Body body = new Packet_Body(new TLV_Structure[]{Msg_status},1);
			Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(),cateGory,
				serviceKey,body.m_uiBodyLen);
			return new Message(new Packet(head,body));
				
		}
		/// <summary>
		/// 返回数据列表消息定义
		/// </summary>
		/// <param name="status">状态值</param>
		/// <returns>消息结构</returns>
		public static Message COMMON_MES_RESP(object status,Msg_Category cateGory,ServiceKey serviceKey,TagName tag,TagFormat format)
		{
			byte[] bgMsg_status = TLV_Structure.ValueToByteArray(format,status);
			TLV_Structure Msg_status = new TLV_Structure(tag,(uint)bgMsg_status.Length,bgMsg_status);
			Packet_Body body = new Packet_Body(new TLV_Structure[]{Msg_status},1);
			Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(),cateGory,
				serviceKey,body.m_uiBodyLen);
			return new Message(new Packet(head,body));	
		}
		/// <summary>
		/// 返回数据列表消息定义
		/// </summary>
		/// <param name="list">数据列表</param>
		/// <returns>消息结构</returns>
		public static Message COMMON_MES_RESP(ArrayList list,Msg_Category cateGory,ServiceKey serviceKey)
		{
			TLV_Structure[] tlv = new TLV_Structure[list.Count];
			for(int i=0;i<list.Count;i++)
			{
				byte[] bgMsg_list = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,list[0].ToString());
				TLV_Structure Msg_List = new TLV_Structure(TagName.Status,(uint)bgMsg_list.Length,bgMsg_list);
				tlv[i] = Msg_List;
			}
			Packet_Body body = new Packet_Body(tlv,(uint)tlv.Length);
			Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(),cateGory,
				serviceKey,body.m_uiBodyLen);
			return new Message(new Packet(head,body));
				
		}
	    /// <summary>
	    /// 查询用户数据列表消息定义
	    /// </summary>
	    /// <param name="list"></param>
	    /// <returns></returns>
		public static Message Common_QUERY_USER_RESP(ArrayList list)
		{
			try
			{
				int j=0;
				TLV_Structure[] TLV = new TLV_Structure[list.Count*3];
				for(int i=0;i<list.Count;i++)
				{
                    ArrayList colList = (ArrayList)list[i];
					byte[] bgMsg_UserNM = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,colList[0].ToString());
					TLV_Structure Msg_UserNM = new TLV_Structure(TagName.UserName,(uint)bgMsg_UserNM.Length,bgMsg_UserNM);
					byte[] baMsg_Passwd = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,colList[1].ToString());
					TLV_Structure Msg_Passwd = new TLV_Structure(TagName.PassWord,(uint)baMsg_Passwd.Length,baMsg_Passwd);
					byte[] baMsg_Mac = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,colList[2].ToString());
					TLV_Structure Msg_MAC = new TLV_Structure(TagName.MAC,(uint)baMsg_Mac.Length,baMsg_Mac);
					TLV[j++] = Msg_UserNM;
					TLV[j++] = Msg_Passwd;
					TLV[j++] = Msg_MAC;
				}

				Packet_Body body = new Packet_Body(new TLV_Structure[]{TLV[0],TLV[1],TLV[2],TLV[3],TLV[6]},5);
				Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(),Msg_Category.USER_ADMIN,
					ServiceKey.USER_QUERY_RESP,body.m_uiBodyLen);
				return new Message(new Packet(head,body));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
