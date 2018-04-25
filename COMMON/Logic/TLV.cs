
using System;

namespace Common.Logic
{/// <summary>
	/// TLV 的摘要说明。
	/// </summary>
	public enum TagName:ushort
	{
		///////////////////////////////////////////////////////////////////////
		UserName = 0x0101, //Format:STRING 用户名
		PassWord = 0x0102, //Format:STRING 密码
		MAC = 0x0103, //Format:STRING  MAC码
		Limit = 0x0104,//Format:DateTime GM帐号使用时效
		User_Status = 0x0105,//Format:INT 状态信息
		UserByID = 0x0106,//Format:INT 操作员ID
		RealName = 0x0107,//Format:STRING 中文名
		DepartID = 0x0108,//Format:INT 部门ID
		DepartName = 0x0109,//Format:STRING 部门名称
		DepartRemark = 0x0110,//Format:STRING 部门描述
        OnlineActive = 0x0111,//Format:Integer 在线状态
        UpdateFileName = 0x0112,//Format:String 文件名
        UpdateFileVersion = 0x0113,//Format:String 文件版本
        UpdateFilePath = 0x0114,//Format:String 文件路径
        UpdateFileSize = 0x0115,//Format:Integer 文件大小
		SysAdmin = 0x0116,//Format:Integer 是否是系统管理员
		Bug_ID = 0x0117,//Format:Integer bugID
		Bug_Subject = 0x0118,//Format:String bug 标题
		Bug_Type = 0x0119,//Format:String 类型
		Bug_Context = 0x0120,//Format:String 内容
		Bug_Date = 0x0121,//Format:TimeStamp 日期
		Bug_Sender = 0x0122,//Format:Integer
		Bug_Process = 0x0123,//Format:Integer
		Bug_Result = 0x0124,//Format:String 
		Update_ID = 0x0125,//Format:Integer
		Update_Module = 0x0126,//Format:String
		Update_Context = 0x0127,//Format:String
		Update_Date = 0x0128,//Format:TimeStamp
		///////////////////////////////////////////////////////////////////////
		GameID = 0x0200, //Format:INTEGER 消息ID
		ModuleName = 0x0201, //Format:STRING 模块名称
		ModuleClass = 0x0202, //Format:STRING 模块分类
		ModuleContent = 0x0203, //Format:STRING 模块描述
		///////////////////////////////////////////////////////////////////////
		Module_ID = 0x0301, //Format:INTEGER 模块ID
		User_ID = 0x0302, //Format:INTEGER 用户ID
		ModuleList = 0x0303, //Format:String 模块列表
		///////////////////////////////////////////////////////////////////////
		Host_Addr = 0x0401, //Format:STRING
		Host_Port = 0x0402, //Format:STRING
		Host_Pat = 0x0403,  //Format:STRING
		Conn_Time = 0x0404, //Format:DateTime 请求和响应时间
		Connect_Msg = 0x0405,//Format:STRING 请求连接信息
		DisConnect_Msg = 0x0406,//Format:STRING	 请求端开信息
		Author_Msg = 0x0407, //Format:STRING 验证用户的信息
		Status = 0x0408,//Format:STRING 操作结果
		Index = 0x0409, //Format:Integer 记录集序号
		PageSize = 0x0410,//Format:Integer 记录页显示长度
		PageCount = 0x0411,//Format:Integer 显示总页数
		SP_Name = 0x0412,//Format:Integer 存储过程名
		Real_ACT = 0x0413,//Format:String 操作的内容
		ACT_Time = 0x0414,//Format:TimeStamp 操作时间
		BeginTime = 0x0415,//Format:Date 开始日期
		EndTime = 0x0416,//Format:Date 结束日期
		///////////////////////////////////////////////////////////////////////
		GameName = 0x0501, //Format:STRING 游戏名称
		GameContent = 0x0502, //Format:STRING 消息描述
		///////////////////////////////////////////////////////////////////////
		Letter_ID = 0x0601, //Format:Integer 
		Letter_Sender = 0x0602, //Format:String
		Letter_Receiver = 0x0603, //Format:String
		Letter_Subject = 0x0604, //Format:String
		Letter_Text = 0x0605, //Format:String
		Send_Date = 0x0606, //Format:Date
		Process_Man = 0x0607, //Format:Integer
		Process_Date = 0x0608, //Format:Date
		Transmit_Man = 0x0609, //Format:Integer
		Is_Process = 0x060A, //Format:Integer
		Process_Reason = 0x060B,//Format:String
		///////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 猛将定义
        /// </summary>
		MJ_Level = 0x0701, //Format:Integer 玩家等级
		MJ_Account = 0x0702, //Format:String 玩家帐号
		MJ_CharName = 0x0703, //Format:String 玩家呢称
		MJ_Exp = 0x0704, //Format:Integer 玩家当前经验
		MJ_Exp_Next_Level = 0x0705, //Format:Integer 玩家下次升级的经验 
		MJ_HP = 0x0706, //Format:Integer 玩家HP值
		MJ_HP_Max = 0x0707, //Format:Integer 玩家最大的HP值
		MJ_MP = 0x0708, //Format:Integer 玩家MP值
		MJ_MP_Max = 0x0709, //Format:Integer 玩家最大的MP值
		MJ_DP = 0x0710, //Format:Integer 玩家DP值
		MJ_DP_Increase_Ratio = 0x0711, //Format:Integer 玩家最大的DP值
		MJ_Exception_Dodge = 0x0712, //Format:Integer 异常状态回避
		MJ_Exception_Recovery = 0x0713, //Format:Integer 异常状态回复
		MJ_Physical_Ability_Max = 0x0714, //Format:Integer 物理能力最大值
		MJ_Physical_Ability_Min = 0x0715, //Format:Integer 物理能力最小值
	    MJ_Magic_Ability_Max = 0x0716, //Format:Integer 魔法能力最大值
		MJ_Magic_Ability_Min = 0x0717, //Format:Integer 魔法能力最小值
		MJ_Tao_Ability_Max = 0x0718, //Format:Integer 道术能力最大值
		MJ_Tao_Ability_Min = 0x0719, //Format:Integer 道术能力最小值
		MJ_Physical_Defend_Max = 0x0720, //Format:Integer 物防最大值
		MJ_Physical_Defend_Min = 0x0721, //Format:Integer 物防最小值
		MJ_Magic_Defend_Max = 0x0722, //Format:Integer 魔防最大值
		MJ_Magic_Defend_Min = 0x0723, //Format:Integer 魔防最小值
		MJ_Accuracy = 0x0724, //Format:Integer 命中率
		MJ_Phisical_Dodge = 0x0725, //Format:Integer 物理回避率
		MJ_Magic_Dodge = 0x0726, //Format:Integer 魔法回避率
		MJ_Move_Speed = 0x0727, //Format:Integer 移动速度
		MJ_Attack_speed = 0x0728, //Format:Integer 攻击速度
		MJ_Max_Beibao = 0x0729, //Format:Integer 背包上限
		MJ_Max_Wanli = 0x0730, //Format:Integer 腕力上限
		MJ_Max_Fuzhong = 0x0731, //Format:Integer 负重上限
		MJ_PASSWD = 0x0732,//Format:String 玩家密码
		MJ_ServerIP = 0x0733,//Format:String 玩家所在服务器
		MJ_TongID = 0x0734,//Format:Integer 帮会ID
		MJ_TongName  = 0x0735,//Format:String 帮会名称
		MJ_TongLevel = 0x0736,//Format:Integer 帮会等级
		MJ_TongMemberCount = 0x0737,//Format:Integer 帮会人数
		MJ_Money = 0x0738,//Format:Money 玩家金钱
		MJ_TypeID = 0x0739,//Format:Integer 玩家角色类型ID
		MJ_ActionType = 0x0740,//Format:Integer 玩家ID
		MJ_Time = 0x0741,//Format:TimeStamp  操作时间
		MJ_CharIndex = 0x0742,//玩家索引号
		MJ_CharName_Prefix = 0x0743,//玩家帮会名称
		MJ_Exploit_Value = 0x0744,//玩家功勋值
        MJ_Reason = 0x0745,//停封理由

	   ///////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 超级舞者定义
        /// </summary>
	    SDO_ServerIP = 0x0801,//Format:String 大区IP
		SDO_UserIndexID = 0x0802,//Format:Integer 玩家用户ID
		SDO_Account = 0x0803,//Format:String 玩家的帐号
		SDO_Level = 0x0804,//Format:Integer 玩家的等级
		SDO_Exp = 0x0805,//Format:Integer 玩家的当前经验值
		SDO_GameTotal = 0x0806,//Format:Integer 总局数
		SDO_GameWin  = 0x0807,//Format:Integer 胜局数
		SDO_DogFall = 0x0808,//Format:Integer 平局数
		SDO_GameFall = 0x0809,//Format:Integer 负局数
		SDO_Reputation = 0x0810,//Format:Integer 声望值
        SDO_GCash = 0x0811,//Format:Integer G币
		SDO_MCash = 0x0812,//Format:Integer M币
		SDO_Address = 0x0813,//Format:Integer 地址
		SDO_Age = 0x0814,//Format:Integer 年龄
		SDO_ProductID = 0x0815,//Format:Integer 商品编号
		SDO_ProductName = 0x0816,//Format:String 商品名称
		SDO_ItemCode  = 0x0817,//Format:Integer 道具编号
		SDO_ItemName = 0x0818,//Format:String 道具名称
		SDO_TimesLimit = 0x0819,//Format:Integer 使用次数
		SDO_DateLimit = 0x0820,//Format:Integer 使用时效
		SDO_MoneyType = 0x0821,//Format:Integer 货币类型
		SDO_MoneyCost = 0x0822,//Format:Integer 道具的价格
		SDO_ShopTime = 0x0823,//Format:DateTime 消费时间
		SDO_MAINCH = 0x0824,//Format:Integer 服务器
		SDO_SUBCH = 0x0825,//Format:Integer 房间
		SDO_Online = 0x0826,//Format:Integer 是否在线
		SDO_LoginTime = 0x0827,//Format:DateTime 上线时间
		SDO_LogoutTime = 0x0828,//Format:DateTime 下线时间
		SDO_AREANAME = 0x0829,//Format:String 大区名字
		SDO_City = 0x0830,//Format:String 玩家所住城市
		SDO_Title = 0x0831,//Format:String 道具主题
		SDO_Context = 0x0832,//Format:String 道具描述
		SDO_MinLevel = 0x0833,//Format:Integer 所带道具的最小等级
		SDO_ActiveStatus = 0x0834,//Format:Integer 激活状态
		SDO_StopStatus = 0x0835,//Format:Integer 封停状态
		SDO_NickName = 0x0836,//Format:String 呢称
		SDO_9YouAccount = 0x0837,//Format:Integer 9you的帐号
		SDO_SEX = 0x0838,//Format:Integer 性别
		SDO_RegistDate =  0x0839,//Format:Date 注册日期
		SDO_FirstLogintime = 0x0840,//Format:Date 第一次登录时间
		SDO_LastLogintime  = 0x0841,//Format:Date 最后一次登录时间
        SDO_Ispad = 0x0842,//Format:Integer 是否已注册跳舞毯
		SDO_Desc = 0x0843,//Format:String 道具描述
		SDO_Postion = 0x0844,//Format:Integer 道具位置
		SDO_BeginTime = 0x0845,//Format:Date 消费记录开始时间
		SDO_EndTime = 0x0846,//Format:Date 消费记录结束时间
		SDO_SendTime = 0x0847,//Format:Date 道具送人日期
		SDO_SendIndexID = 0x0848,//Format:Integer 发送人的ID
		SDO_SendUserID = 0x0849,//Format:String 发送人帐号
		SDO_ReceiveNick = 0x0850,//Format:String 接受人呢称
		SDO_BigType = 0x0851,//Format:Integer 道具大类
		SDO_SmallType = 0x0852,//Format:Integer 道具小类
        SDO_REASON = 0x0853,//Format:String 停封理由
        SDO_StopTime = 0x0854,//Format:TimeStamp 停封时间
        SDO_DaysLimit  = 0x0855,//Format:Integer 使用天数
        SDO_Email = 0x0856,//Format:String 邮件
        SDO_ChargeSum = 0x0857,//Format:String 充值合计
		SDO_SenceID = 0x0858,
		SDO_WeekDay = 0x0859,
		SDO_MatPtHR = 0x0860,
		SDO_MatPtMin = 0x0861,
		SDO_StPtHR = 0x0862,
		SDO_StPtMin = 0x0863,
		SDO_EdPtHR = 0x0864,
		SDO_EdPtMin = 0x0865,
		SDO_Sence = 0x0868,
		SDO_MusicID1 = 0x0869,
		SDO_MusicName1 = 0x0870,
		SDO_LV1 = 0x0871,
		SDO_MusicID2 = 0x0872,
		SDO_MusicName2 = 0x0873,
		SDO_LV2 = 0x0874,
		SDO_MusicID3 = 0x0875,
		SDO_MusicName3 = 0x0876,
		SDO_LV3 = 0x0877,
		SDO_MusicID4 = 0x0878,
		SDO_MusicName4 = 0x0879,
		SDO_LV4 = 0x0880,
		SDO_MusicID5 = 0x0881,
		SDO_MusicName5 = 0x0882,
		SDO_LV5 = 0x0883,
		SDO_Precent = 0x0884,
		SDO_KeyID = 0x0885,
		SDO_KeyWord = 0x0886,
		SDO_MasterID = 0x0887,
		SDO_Master = 0x0888,
		SDO_SlaverID = 0x0889,
		SDO_Slaver = 0x0890,
		SDO_ChannelList = 0x0891,
		SDO_BoardMessage = 0x0892,
		SDO_wPlanetID = 0x0893,
		SDO_wChannelID = 0x0894,
		SDO_iLimitUser = 0x0895,
		SDO_iCurrentUser = 0x0896,
		SDO_ipaddr = 0x0897,
		SDO_Interval = 0x0898,
		SDO_TaskID = 0x0899,
		SDO_Status = 0x08100,
		SDO_Score = 0x08101,//积分
		SDO_FirstPadTime = 0x08102,//跳舞毯第一次使用时间
		SDO_BanDate = 0x08103,//停封多少天
        /// <summary>
        /// 游戏服务器列表定义
        /// </summary>
		ServerInfo_IP =  0x0901,//Format:String 服务器IP
		ServerInfo_City  =0x0902,//Format:String 城市
		ServerInfo_GameID = 0x0903,//Format:Integer 游戏ID
		ServerInfo_GameName = 0x0904,//Format:String 游戏名
		ServerInfo_GameDBID=  0x0905,//Format:Integer 游戏数据库类型
        ServerInfo_GameFlag = 0x0906,//Format:Integer 游戏服务器状态
        ServerInfo_Idx = 0x0907,

        ///////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 劲舞团定义
        /// </summary>
        AU_ACCOUNT = 0x1001,//玩家帐号 Format:String
        AU_UserNick = 0x1002,//玩家呢称 Format:String
        AU_Sex = 0x1003,//玩家性别 Format:Integer
        AU_State = 0x1004,//玩家状态 Format:Integer
        AU_STOPSTATUS = 0x1005,//劲舞者封停状态 Format:Integer
        AU_Reason = 0x1006,//封停理由 Format:String
        AU_BanDate = 0x1007,//封停日期 Format:TimeStamp
        AU_ServerIP = 0x1008,//劲舞团游戏服务器 Format:String
        AU_Id9you = 0x1009, //Format:Integer 9youID
        AU_UserSN = 0x1010, //Format:Integer 用户序列号
        AU_EquipState = 0x1011, //Format:String 
        AU_AvatarItem = 0x1012, //Format:Integer
        AU_BuyNick = 0x1013, //Format:String 购买呢称
        AU_BuyDate = 0x1014,//Format:Timestamp 购买日期
        AU_ExpireDate = 0x1015,//Format:TimesStamp  过期日期
        AU_BuyType = 0x1016, // Format:Integer 购买类型

        AU_PresentID = 0x1017, //Format:Integer 赠送ID
        AU_SendSN = 0x1018, //Format:Integer  赠送SN
        AU_SendNick = 0x1019, //Format:String 赠送呢称
        AU_RecvSN = 0x1020, //Format:String 接受人SN
        AU_RecvNick = 0x1021, //Format:String 接受人呢称
        AU_Kind = 0x1022, //Format:Integer 类型
        AU_ItemID = 0x1023, //Format:Integer 道具ID
        AU_Period = 0x1024, //Format:Integer 期间
        AU_BeforeCash = 0x1025, //Format:Integer 消费之前金额
        AU_AfterCash = 0x1026, //Format:Integer 消费之后金额
        AU_SendDate = 0x1027, //Format:TimeStamp 发送日期
        AU_RecvDate = 0x1028,//Format:TimeStamp 接受日期
        AU_Memo = 0x1029,//Format:String 备注
        AU_UserID = 0x1030, //Format:String 玩家ID
        AU_Exp = 0x1031, //Format:Integer 玩家经验
        AU_Point = 0x1032, //Format:Integer 玩家位置
        AU_Money = 0x1033, //Format:Integer 金钱
        AU_Cash = 0x1034, //Format:Integer 现金
        AU_Level = 0x1035, //Format:Integer 等级
        AU_Ranking = 0x1036, //Format:Integer 银行
        AU_IsAllowMsg = 0x1037, //Format:Integer 允许发消息
        AU_IsAllowInvite = 0x1038, //Format:Integer 允许邀请
        AU_LastLoginTime = 0x1039, //Format:TimeStamp 最后登录时间
        AU_Password = 0x1040, //Format:String 密码
        AU_UserName = 0x1041, //Format:String 用户名
        AU_UserGender = 0x1042, //Format:String 
        AU_UserPower = 0x1043, //Format:Integer
        AU_UserRegion = 0x1044, //Format:String 
        AU_UserEMail = 0x1045, //Format:String 用户电子邮件
        AU_RegistedTime = 0x1046, //Format:TimeStamp 注册时间
        AU_ItemName = 0x1047,//道具名
        AU_ItemStyle = 0x1048,//道具类型
        AU_Demo = 0x1049,//描述 
        AU_BeginTime = 0x1050,//开始时间
        AU_EndTime = 0x1051,//结束时间
        AU_SendUserID = 0x1052,//发送人帐号
        AU_RecvUserID = 0x1053,//接受人帐号 
        AU_SexIndex = 0x1054,//性别
        ///////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 疯狂卡丁车定义
        /// </summary>
        CR_ServerIP = 0x1101,//服务器IP
        CR_ACCOUNT = 0x1102,//玩家帐号 Format:String
        CR_Passord = 0x1103,//玩家密码 Format:String
        CR_NUMBER = 0x1104,//激活码 Format:String
        CR_ISUSE = 0x1105,//是否被使用
        CR_STATUS = 0x1106,//玩家状态 Format:Integer
        CR_ActiveIP = 0x1107,//激活服务器IP Format:String
        CR_ActiveDate = 0x1108,//激活日期 Format:TimeStamp
        CR_BoardID = 0x1109,//公告ID Format:Integer
        CR_BoardContext = 0x1110,//公告内容 Format:String
        CR_BoardColor = 0x1111,//公告颜色 Format:String
        CR_ValidTime = 0x1112,//生效时间 Format:TimeStamp
        CR_InValidTime = 0x1113,//失效时间 Format:TimeStamp
        CR_Valid = 0x1114,//是否有效 Format:Integer
        CR_PublishID = 0x1115,//发布人ID Format:Integer
        CR_DayLoop = 0x1116,//每天播放 Format:Integer
        CR_PSTID = 0x1117,//注册号 Format:Integer
        CR_SEX = 0x1118,//性别 Format:Integer
        CR_LEVEL = 0x1119,//等级 Format:Integer
        CR_EXP = 0x1120,//经验 Format:Integer
        CR_License = 0x1121,//驾照Format:Integer
        CR_Money = 0x1122,//金钱Format:Integer
        CR_RMB = 0x1123,//人民币Format:Integer
        CR_RaceTotal = 0x1124,//比赛总数Format:Integer
        CR_RaceWon = 0x1125,//胜利场数Format:Integer
        CR_ExpOrder = 0x1126,//经验排名Format:Integer
        CR_WinRateOrder = 0x1127,//胜率排名Format:Integer
        CR_WinNumOrder = 0x1128,//胜利场数排名Format:Integer
        CR_SPEED = 0x1129,//播放速度Format:Integer
        CR_Mode= 0x1130,//播放方式 Format:Integer
        CR_ACTION = 0x1131,//查询动作　Format:Integer
        CR_NickName = 0x1132,//呢称 Format:String
        CR_Channel = 0x1133,//频道ID
        CR_UserID = 0x1134,//用户ID
        CR_BoardContext1 = 0x1135,//内容1
        CR_BoardContext2 = 0x1136,//内容2
        CR_Expire = 0x1137,//生效格式
        CR_ChannelID = 0x1138,//频道ID
        CR_ChannelName = 0x1139,//频道名称
		CR_Last_Login = 0x1140,//上次登入时间
		CR_Last_Logout = 0x1141,//上次登出时间
		CR_Last_Playing_Time = 0x1142,//上次游戏时长
		CR_Total_Time = 0x1143,//总的游戏时长
		CR_UserName = 0x1144,//玩家姓名
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 一卡通定义
		/// </summary>
        CARD_PDID = 0x1202,
        CARD_PDkey = 0x1203,
        CARD_PDCardType = 0x1204,
        CARD_PDFrom = 0x1205,
        CARD_PDCardNO = 0x1206,
        CARD_PDCardPASS = 0x1207,
        CARD_PDCardPrice = 0x1208,
        CARD_PDaction = 0x1209,
        CARD_PDuserid = 0x1210,
        CARD_PDusername = 0x1211,
        CARD_PDgetuserid = 0x1212,
        CARD_PDgetusername = 0x1213,
        CARD_PDdate = 0x1214,
        CARD_PDip = 0x1215,
        CARD_PDstatus = 0x1216,
        CARD_UDID = 0x1217,
        CARD_UDkey = 0x1218,
        CARD_UDusedo = 0x1219,
        CARD_UDdirect = 0x1220,
        CARD_UDuserid = 0x1221,
        CARD_UDusername = 0x1222,
        CARD_UDgetuserid = 0x1223,
        CARD_UDgetusername = 0x1224,
        CARD_UDcoins = 0x1225,
        CARD_UDtype = 0x1226,
        CARD_UDtargetvalue = 0x1227,
        CARD_UDzone1 = 0x1228,
        CARD_UDzone2 = 0x1229,
        CARD_UDdate = 0x1230,
        CARD_UDip = 0x1231,
        CARD_UDstatus = 0x1232,
        CARD_cardnum = 0x1233,
        CARD_cardpass = 0x1234,
        CARD_serial = 0x1235,
        CARD_draft = 0x1236,
        CARD_type1 = 0x1237,
        CARD_type2 = 0x1238,
        CARD_type3 = 0x1239,
        CARD_type4 = 0x1240,
        CARD_price = 0x1241,
        CARD_valid_date = 0x1242,
        CARD_use_status = 0x1243,
        CARD_cardsent = 0x1244,
        CARD_create_date = 0x1245,
        CARD_use_userid = 0x1246,
        CARD_use_username = 0x1247,
        CARD_partner = 0x1248,
        CARD_skey = 0x1249,
        CARD_ActionType = 0x1250,
        CARD_id = 0x1251 ,//TLV_STRING 久之游注册卡号
        CARD_username = 0x1252,//TLV_STRING 久之游注册用户名
        CARD_nickname = 0x1253,//TLV_STRING 久之游注册呢称
        CARD_password = 0x1254,//TLV_STRING 久之游注册密码
        CARD_sex = 0x1255,//TLV_STRING 久之游注册性别
        CARD_rdate = 0x1256,//TLV_Date 久之游注册日期
        CARD_rtime = 0x1257,//TLV_Time 久之游注册时间
        CARD_securecode = 0x1258,//TLV_STRING 安全码
        CARD_vis = 0x1259,//TLV_INTEGER
        CARD_logdate = 0x1260,//TLV_TimeStamp 日期
        CARD_realname = 0x1263,//TLV_STRING 真实姓名
        CARD_birthday = 0x1264,//TLV_Date 出生日期
        CARD_cardtype = 0x1265,//TLV_STRING
        CARD_email = 0x1267,//TLV_STRING 邮件
        CARD_occupation = 0x1268,//TLV_STRING 职业
        CARD_education = 0x1269,//TLV_STRING 教育程度
        CARD_marriage = 0x1270,//TLV_STRING 婚否
        CARD_constellation = 0x1271,//TLV_STRING 星座
        CARD_shx = 0x1272,//TLV_STRING 生肖
        CARD_city = 0x1273,//TLV_STRING 城市
        CARD_address = 0x1274,//TLV_STRING 联系地址
        CARD_phone = 0x1275,//TLV_STRING 联系电话
        CARD_qq = 0x1276,//TLV_STRING QQ
        CARD_intro = 0x1277,//TLV_STRING 介绍
        CARD_msn = 0x1278,//TLV_STRING MSN
        CARD_mobilephone = 0x1279,//TLV_STRING 移动电话
        CARD_SumTotal = 0x1280,//TLV_INTEGER 合计
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 劲舞团商城
		/// </summary>
        AuShop_orderid = 0x1301, //TLV_INTEGER
        AuShop_udmark = 0x1302,
        AuShop_bkey = 0x1303,
        AuShop_pkey = 0x1304,//TLV_INTEGER
        AuShop_userid = 0x1305,
        AuShop_username = 0x1306,//TLV_STRING
        AuShop_getuserid = 0x1307,
        AuShop_getusername = 0x1308,//TLV_STRING
        AuShop_pcategory = 0x1309,
        AuShop_pisgift = 0x1310,
        AuShop_islover = 0x1311,
        AuShop_ispresent = 0x1312,
        AuShop_isbuysong = 0x1313,
        AuShop_prule = 0x1314,
        AuShop_psex = 0x1315,
        AuShop_pbuytimes = 0x1316,
        AuShop_allprice = 0x1317,
        AuShop_allaup = 0x1318,
        AuShop_buytime = 0x1319,
        AuShop_buytime2 = 0x1320,
        AuShop_buyip = 0x1321,
        AuShop_zone = 0x1322,
        AuShop_status = 0x1323,
        AuShop_pid = 0x1324,
        AuShop_pname = 0x1326,
        AuShop_pgift = 0x1328,
        AuShop_pscash = 0x1330,
        AuShop_pgamecode = 0x1331,
        AuShop_pnew = 0x1332,
        AuShop_phot = 0x1333,
        AuShop_pcheap = 0x1334,
        AuShop_pchstarttime = 0x1335,
        AuShop_pchstoptime = 0x1336,
        AuShop_pstorage = 0x1337,
        AuShop_pautoprice = 0x1339,
        AuShop_price = 0x1340,
        AuShop_chprice = 0x1341,
        AuShop_aup = 0x1342,
        AuShop_chaup = 0x1343,
        AuShop_ptimeitem = 0x1344,
        AuShop_pricedetail = 0x1345,
        AuShop_pdesc = 0x1347,
        AuShop_pbuys = 0x1348,
        AuShop_pfocus = 0x1349,
        AuShop_pmark1 = 0x1350,
        AuShop_pmark2 = 0x1351,
        AuShop_pmark3 = 0x1352,
        AuShop_pinttime = 0x1353,
        AuShop_pdate = 0x1354,
        AuShop_pisuse = 0x1355,
        AuShop_ppic = 0x1356,
        AuShop_ppic1 = 0x1357,
        AuShop_usefeesum = 0x1358,
        AuShop_useaupsum = 0x1359,
        AuShop_buyitemsum = 0x1360,
        AuShop_BeginDate = 0x1361,
        AuShop_EndDate = 0x1362,
        AuShop_GCashSum = 0x1363,
        AuShop_MCashSum = 0x1364,
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 劲乐团
		/// </summary>
		o2jam_ServerIP= 0x1401,//Format:TLV_STRING IP
		o2jam_UserID = 0x1402,//Format:TLV_STRING 用户帐号
		o2jam_UserNick = 0x1403,//Format:TLV_STRING 用户呢称
		o2jam_Sex = 0x1404,//Format:TLV_BOOLEAN 性别
		o2jam_Level = 0x1405,//Format:TLV_INTEGER 等级
		o2jam_Win = 0x1406,//Format:TLV_INTEGER 胜
		o2jam_Draw = 0x1407,//Format:TLV_INTEGER 平
		o2jam_Lose = 0x1408,//Format:TLV_INTEGER 负
		o2jam_SenderID = 0x1409,				//varchar
		o2jam_SenderIndexID = 0x1410,				//int
		o2jam_SenderNickName = 0x1411,				//varchar
		o2jam_ReceiverID = 0x1412,				//varchar
		o2jam_ReceiverIndexID = 0x1413,				//int
		o2jam_ReceiverNickName = 0x1414,				//varchar
		o2jam_Title = 0x1415,				//varchar
		o2jam_Content = 0x1416,				//varchar
		o2jam_WriteDate = 0x1417,				//datetime
		o2jam_ReadDate = 0x1418,				//datetime
		o2jam_ReadFlag = 0x1419,				//char
		o2jam_TypeFlag = 0x1420,				//char
		o2jam_Ban_Date = 0x1421,				//datetime
		o2jam_GEM = 0x1422,				//int
		o2jam_MCASH = 0x1423,				//int
		o2jam_O2CASH = 0x1424,				//int
		o2jam_MUSICCASH = 0x1425,				//int
		o2jam_ITEMCASH = 0x1426,				//int
		o2jam_USER_INDEX_ID = 0x1427,				//int
		o2jam_ITEM_INDEX_ID = 0x1428,				//int
		o2jam_USED_COUNT = 0x1429,				//int
		o2jam_REG_DATE = 0x1430,				//datetime
		o2jam_OLD_USED_COUNT = 0x1431,				//int
		o2jam_CURRENT_CASH = 0x1433,				//int
		o2jam_CHARGED_CASH = 0x1434,				//int
		o2jam_KIND_CASH = 0x1435,				//char
		o2jam_NAME = 0x1437,				//varchar
		o2jam_KIND = 0x1438,				//int
		o2jam_PLANET = 0x1439,				//int
		o2jam_VAL = 0x1440,				//int
		o2jam_EFFECT = 0x1441,				//int
		o2jam_JUSTICE = 0x1442,				//int
		o2jam_LIFE = 0x1443,				//int
		o2jam_PRICE_KIND = 0x1444,				//int
		o2jam_Exp = 0x1445, //Int
		o2jam_Battle = 0x1446,//Int
		o2jam_POSITION = 0x1448,				//int
		o2jam_COMPANY_ID = 0x1449,				//int
		o2jam_DESCRIBE = 0x1450,				//varchar
		o2jam_UPDATE_TIME = 0x1451,				//datetime
		o2jam_ITEM_NAME = 0x1453,				//varchar
		o2jam_ITEM_USE_COUNT = 0x1454,				//int
		o2jam_ITEM_ATTR_KIND = 0x1455,				//int
		o2jam_USER_ID = 0x1457,				//varchar
		o2jam_USER_NICKNAME = 0x1458,				//varchar
		o2jam_CREATE_TIME = 0x1460,				//datetime
		o2jam_BeginDate = 0x1461,
		o2jam_EndDate = 0x1462,
		O2JAM_EQUIP1 = 0x1463,		//TLV_STRING,
		O2JAM_EQUIP2 = 0x1464,	//TLV_STRING,
		O2JAM_EQUIP3 = 0x1465,	//TLV_STRING,
		O2JAM_EQUIP4 = 0x1466,	//TLV_STRING,
		O2JAM_EQUIP5 = 0x1467,	//TLV_STRING,
		O2JAM_EQUIP6 = 0x1468,	//TLV_STRING,
		O2JAM_EQUIP7 = 0x1469,	//TLV_STRING,
		O2JAM_EQUIP8 = 0x1470,	//TLV_STRING,
		O2JAM_EQUIP9 = 0x1471,	//TLV_STRING,
		O2JAM_EQUIP10 = 0x1472,		//TLV_STRING,
		O2JAM_EQUIP11 = 0x1473,	//TLV_STRING,
		O2JAM_EQUIP12 = 0x1474,	//TLV_STRING,
		O2JAM_EQUIP13 = 0x1475,	//TLV_STRING,
		O2JAM_EQUIP14 = 0x1476,	//TLV_STRING,
		O2JAM_EQUIP15 = 0x1477,	//TLV_STRING,
		O2JAM_EQUIP16 = 0x1478,		//TLV_STRING,
		O2JAM_BAG1 = 0x1479,		//TLV_STRING,
		O2JAM_BAG2 = 0x1480,	//TLV_STRING,
		O2JAM_BAG3 = 0x1481,	//TLV_STRING,
		O2JAM_BAG4 = 0x1482,	//TLV_STRING,
		O2JAM_BAG5 = 0x1483,		//TLV_STRING,
		O2JAM_BAG6 = 0x1484,	//TLV_STRING,
		O2JAM_BAG7 = 0x1485,	//TLV_STRING,
		O2JAM_BAG8 = 0x1486,	//TLV_STRING,
		O2JAM_BAG9 = 0x1487,	//TLV_STRING,
		O2JAM_BAG10	= 0x1488,	//TLV_STRING,
		O2JAM_BAG11	= 0x1489,	//TLV_STRING,
		O2JAM_BAG12	= 0x1490,	//TLV_STRING,
		O2JAM_BAG13	= 0x1491,	//TLV_STRING,
		O2JAM_BAG14	= 0x1492,	//TLV_STRING,
		O2JAM_BAG15	= 0x1493,	//TLV_STRING,
		O2JAM_BAG16	= 0x1494,	//TLV_STRING,
		O2JAM_BAG17	= 0x1495,	//TLV_STRING,
		O2JAM_BAG18	= 0x1496,	//TLV_STRING,
		O2JAM_BAG19	= 0x1497,	//TLV_STRING,
		O2JAM_BAG20	= 0x1498,	//TLV_STRING,
		O2JAM_BAG21	= 0x1499,	//TLV_STRING,
		O2JAM_BAG22	= 0x1500,	//TLV_STRING,
		O2JAM_BAG23	= 0x1501,	//TLV_STRING,
		O2JAM_BAG24	= 0x1502,	//TLV_STRING,
		O2JAM_BAG25	= 0x1503,	//TLV_STRING,
		O2JAM_BAG26	= 0x1504,	//TLV_STRING,
		O2JAM_BAG27	= 0x1505,	//TLV_STRING,
		O2JAM_BAG28	= 0x1506,	//TLV_STRING,
		O2JAM_BAG29	= 0x1507,	//TLV_STRING,
		O2JAM_BAG30	= 0x1508,	//TLV_STRING
		O2JAM_BuyType = 0x1509,//TLV_INTEGER
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 劲乐团II
		/// </summary>
		O2JAM2_ServerIP = 0x1601,//TLV_STRING
		O2JAM2_UserID = 0x1602,//TLV_STRING
		O2JAM2_UserName = 0x1603,//TLV_STRING
		O2JAM2_Id1 = 0x1604,//TLV_Integer
		O2JAM2_Id2 = 0x1605,//TLV_Integer
		O2JAM2_Rdate = 0x1606,//TLV_Date
		O2JAM2_IsUse = 0x1607,//TLV_Integer
		O2JAM2_Status = 0x1608,//TLV_Integer
		O2JAM2_UserIndexID = 0x1609,//TLV_Integer
		O2JAM2_UserNick = 0x1610,//Format:TLV_STRING 用户呢称
		O2JAM2_Sex = 0x1611,//Format:TLV_BOOLEAN 性别
		O2JAM2_Level = 0x1612,//Format:TLV_INTEGER 等级
		O2JAM2_Win = 0x1613,//Format:TLV_INTEGER 胜
		O2JAM2_Draw = 0x1614,//Format:TLV_INTEGER 平
		O2JAM2_Lose = 0x1615,//Format:TLV_INTEGER 负
		O2JAM2_Exp = 0x1616,//Format:TLV_INTEGER 经验
		O2JAM2_TOTAL =0x1617,//Format:TLV_INTEGER 总局数
		O2JAM2_GCash = 0x1618,//Format:TLV_INTEGER G币
		O2JAM2_MCash =0x1619,//Format:TLV_INTEGER M币
		O2JAM2_ItemCode = 0x1620,//Format:TLV_INTEGER
		O2JAM2_ItemName = 0x1621,//Format:TLV_String
		O2JAM2_Timeslimt = 0x1622,
		O2JAM2_DateLimit = 0x1623,
		O2JAM2_ItemSource = 0x1624,
		O2JAM2_Position = 0x1625,
		O2JAM2_BeginDate = 0x1626,
		O2JAM2_ENDDate = 0x1627,
		O2JAM2_MoneyType = 0x1628,
		O2JAM2_Title = 0x1629,
		O2JAM2_Context = 0x1630,
		O2JAM2_ComsumeType  =0x1631,
		O2JAM2_ComsumeCode = 0x1632,
		O2JAM2_DayLimit = 0x1633,
		O2JAM2_StopTime = 0x1634,
		O2JAM2_StopStatus = 0x1635,
		O2JAM2_REASON = 0x1636,

		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 劲爆足球 Add by KeHuaQing
		/// </summary>
		Soccer_ServerIP = 0x1701,
		Soccer_loginId = 0x1702,//Login ID
		Soccer_charsex = 0x1703,//男/女, 体型(瘦/稍瘦/普通/稍胖/胖) 区别code
		Soccer_charidx = 0x1704,//角色序列号 (ExSoccer.dbo.t_character[idx])
		Soccer_charexp = 0x1705,//经验值
		Soccer_charlevel = 0x1706,//等级
		Soccer_charpoint = 0x1707,//点数 
		Soccer_match = 0x1708,//比赛数
		Soccer_win = 0x1709,//胜利
		Soccer_lose = 0x1710,//失败
		Soccer_draw = 0x1711,//平局
		Soccer_drop = 0x1712,//平局		
		Soccer_charname = 0x1713,//角色名
		Soccer_charpos = 0x1714,//位置
		Soccer_Type = 0x1715,//查询类型
		Soccer_String = 0x1716,//查询值
		Soccer_admid = 0x1717,//管理者ID
		Soccer_deleted_date = 0x1718,//删除日期
		Soccer_status = 0x1719,//状态
		Soccer_m_id = 0x1720,//玩家序列号 int
		Soccer_m_auth = 0x1721,//玩家是否被停封 int
		Soccer_regDate = 0x1722,//玩家注册日期 string 
		Soccer_c_date = 0x1723,//角色创建日期 string

		Soccer_char_max = 0x1724,//tinyint
		Soccer_char_cnt = 0x1725,//int
		Soccer_ret = 0x1726,//int
		Soccer_kind = 0x1727,//socket,name string

		ERROR_Msg = 0xFFFF //Format:STRING  验证用户的信息

	}
	public enum TagFormat:byte
	{
		TLV_STRING = 0,
		TLV_MONEY = 1,
		TLV_DATE = 2,
		TLV_INTEGER = 3,
		TLV_EXTEND = 4,
		TLV_NUMBER = 5,
		TLV_TIME = 6,
		TLV_TIMESTAMP = 7,
		TLV_BOOLEAN = 8
	}
	public class TLV
	{
		/// <summary>
		/// TAG最大长度
		/// </summary>
		public const int MAX_TAG_LENGTH = 30;
		public class TagStruct
		{
			/// <summary>
			/// 构造TLV结构
			/// </summary>
			/// <param name="_tag">标签</param>
			/// <param name="_format">类型</param>
			/// <param name="_len">长度</param>
			/// <param name="_tag_buf">值</param>
			public TagStruct(TagName _tag,TagFormat _format,uint _len,string _tag_buf)
			{
				tag = _tag;
				format = _format;
				len = _len;
				tag_buf= _tag_buf;
			}
			public TagName tag;
			public TagFormat format;
			public uint len;
			public string tag_buf;
		}
		public static TagStruct[] Tags =
			{
				new TagStruct(TagName.UserName , TagFormat.TLV_STRING, 6,"UserName"),
				new TagStruct(TagName.PassWord , TagFormat.TLV_STRING, 6,"PassWord" ),
				new TagStruct(TagName.MAC , TagFormat.TLV_NUMBER, 0xffffffff,"Random Number" ),
				new TagStruct(TagName.ModuleName , TagFormat.TLV_STRING, 0xffffffff,"Module Name" ),
				new TagStruct(TagName.ModuleClass , TagFormat.TLV_STRING, 0xffffffff,"Module Class" ),
				new TagStruct(TagName.ModuleContent , TagFormat.TLV_STRING, 0xffffffff,"Module Content" ),
				new TagStruct(TagName.User_ID , TagFormat.TLV_INTEGER,4, "User ID" ),
				new TagStruct(TagName.Module_ID , TagFormat.TLV_INTEGER,4, "Module ID" ),
			};
	}
}
