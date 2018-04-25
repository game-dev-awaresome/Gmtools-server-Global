
using System;

namespace Common.Logic
{/// <summary>
	/// TLV ��ժҪ˵����
	/// </summary>
	public enum TagName:ushort
	{
		///////////////////////////////////////////////////////////////////////
		UserName = 0x0101, //Format:STRING �û���
		PassWord = 0x0102, //Format:STRING ����
		MAC = 0x0103, //Format:STRING  MAC��
		Limit = 0x0104,//Format:DateTime GM�ʺ�ʹ��ʱЧ
		User_Status = 0x0105,//Format:INT ״̬��Ϣ
		UserByID = 0x0106,//Format:INT ����ԱID
		RealName = 0x0107,//Format:STRING ������
		DepartID = 0x0108,//Format:INT ����ID
		DepartName = 0x0109,//Format:STRING ��������
		DepartRemark = 0x0110,//Format:STRING ��������
        OnlineActive = 0x0111,//Format:Integer ����״̬
        UpdateFileName = 0x0112,//Format:String �ļ���
        UpdateFileVersion = 0x0113,//Format:String �ļ��汾
        UpdateFilePath = 0x0114,//Format:String �ļ�·��
        UpdateFileSize = 0x0115,//Format:Integer �ļ���С
		SysAdmin = 0x0116,//Format:Integer �Ƿ���ϵͳ����Ա
		Bug_ID = 0x0117,//Format:Integer bugID
		Bug_Subject = 0x0118,//Format:String bug ����
		Bug_Type = 0x0119,//Format:String ����
		Bug_Context = 0x0120,//Format:String ����
		Bug_Date = 0x0121,//Format:TimeStamp ����
		Bug_Sender = 0x0122,//Format:Integer
		Bug_Process = 0x0123,//Format:Integer
		Bug_Result = 0x0124,//Format:String 
		Update_ID = 0x0125,//Format:Integer
		Update_Module = 0x0126,//Format:String
		Update_Context = 0x0127,//Format:String
		Update_Date = 0x0128,//Format:TimeStamp
		///////////////////////////////////////////////////////////////////////
		GameID = 0x0200, //Format:INTEGER ��ϢID
		ModuleName = 0x0201, //Format:STRING ģ������
		ModuleClass = 0x0202, //Format:STRING ģ�����
		ModuleContent = 0x0203, //Format:STRING ģ������
		///////////////////////////////////////////////////////////////////////
		Module_ID = 0x0301, //Format:INTEGER ģ��ID
		User_ID = 0x0302, //Format:INTEGER �û�ID
		ModuleList = 0x0303, //Format:String ģ���б�
		///////////////////////////////////////////////////////////////////////
		Host_Addr = 0x0401, //Format:STRING
		Host_Port = 0x0402, //Format:STRING
		Host_Pat = 0x0403,  //Format:STRING
		Conn_Time = 0x0404, //Format:DateTime �������Ӧʱ��
		Connect_Msg = 0x0405,//Format:STRING ����������Ϣ
		DisConnect_Msg = 0x0406,//Format:STRING	 ����˿���Ϣ
		Author_Msg = 0x0407, //Format:STRING ��֤�û�����Ϣ
		Status = 0x0408,//Format:STRING �������
		Index = 0x0409, //Format:Integer ��¼�����
		PageSize = 0x0410,//Format:Integer ��¼ҳ��ʾ����
		PageCount = 0x0411,//Format:Integer ��ʾ��ҳ��
		SP_Name = 0x0412,//Format:Integer �洢������
		Real_ACT = 0x0413,//Format:String ����������
		ACT_Time = 0x0414,//Format:TimeStamp ����ʱ��
		BeginTime = 0x0415,//Format:Date ��ʼ����
		EndTime = 0x0416,//Format:Date ��������
		///////////////////////////////////////////////////////////////////////
		GameName = 0x0501, //Format:STRING ��Ϸ����
		GameContent = 0x0502, //Format:STRING ��Ϣ����
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
        /// �ͽ�����
        /// </summary>
		MJ_Level = 0x0701, //Format:Integer ��ҵȼ�
		MJ_Account = 0x0702, //Format:String ����ʺ�
		MJ_CharName = 0x0703, //Format:String ����س�
		MJ_Exp = 0x0704, //Format:Integer ��ҵ�ǰ����
		MJ_Exp_Next_Level = 0x0705, //Format:Integer ����´������ľ��� 
		MJ_HP = 0x0706, //Format:Integer ���HPֵ
		MJ_HP_Max = 0x0707, //Format:Integer �������HPֵ
		MJ_MP = 0x0708, //Format:Integer ���MPֵ
		MJ_MP_Max = 0x0709, //Format:Integer �������MPֵ
		MJ_DP = 0x0710, //Format:Integer ���DPֵ
		MJ_DP_Increase_Ratio = 0x0711, //Format:Integer �������DPֵ
		MJ_Exception_Dodge = 0x0712, //Format:Integer �쳣״̬�ر�
		MJ_Exception_Recovery = 0x0713, //Format:Integer �쳣״̬�ظ�
		MJ_Physical_Ability_Max = 0x0714, //Format:Integer �����������ֵ
		MJ_Physical_Ability_Min = 0x0715, //Format:Integer ����������Сֵ
	    MJ_Magic_Ability_Max = 0x0716, //Format:Integer ħ���������ֵ
		MJ_Magic_Ability_Min = 0x0717, //Format:Integer ħ��������Сֵ
		MJ_Tao_Ability_Max = 0x0718, //Format:Integer �����������ֵ
		MJ_Tao_Ability_Min = 0x0719, //Format:Integer ����������Сֵ
		MJ_Physical_Defend_Max = 0x0720, //Format:Integer ������ֵ
		MJ_Physical_Defend_Min = 0x0721, //Format:Integer �����Сֵ
		MJ_Magic_Defend_Max = 0x0722, //Format:Integer ħ�����ֵ
		MJ_Magic_Defend_Min = 0x0723, //Format:Integer ħ����Сֵ
		MJ_Accuracy = 0x0724, //Format:Integer ������
		MJ_Phisical_Dodge = 0x0725, //Format:Integer ����ر���
		MJ_Magic_Dodge = 0x0726, //Format:Integer ħ���ر���
		MJ_Move_Speed = 0x0727, //Format:Integer �ƶ��ٶ�
		MJ_Attack_speed = 0x0728, //Format:Integer �����ٶ�
		MJ_Max_Beibao = 0x0729, //Format:Integer ��������
		MJ_Max_Wanli = 0x0730, //Format:Integer ��������
		MJ_Max_Fuzhong = 0x0731, //Format:Integer ��������
		MJ_PASSWD = 0x0732,//Format:String �������
		MJ_ServerIP = 0x0733,//Format:String ������ڷ�����
		MJ_TongID = 0x0734,//Format:Integer ���ID
		MJ_TongName  = 0x0735,//Format:String �������
		MJ_TongLevel = 0x0736,//Format:Integer ���ȼ�
		MJ_TongMemberCount = 0x0737,//Format:Integer �������
		MJ_Money = 0x0738,//Format:Money ��ҽ�Ǯ
		MJ_TypeID = 0x0739,//Format:Integer ��ҽ�ɫ����ID
		MJ_ActionType = 0x0740,//Format:Integer ���ID
		MJ_Time = 0x0741,//Format:TimeStamp  ����ʱ��
		MJ_CharIndex = 0x0742,//���������
		MJ_CharName_Prefix = 0x0743,//��Ұ������
		MJ_Exploit_Value = 0x0744,//��ҹ�ѫֵ
        MJ_Reason = 0x0745,//ͣ������

	   ///////////////////////////////////////////////////////////////////////
        /// <summary>
        /// �������߶���
        /// </summary>
	    SDO_ServerIP = 0x0801,//Format:String ����IP
		SDO_UserIndexID = 0x0802,//Format:Integer ����û�ID
		SDO_Account = 0x0803,//Format:String ��ҵ��ʺ�
		SDO_Level = 0x0804,//Format:Integer ��ҵĵȼ�
		SDO_Exp = 0x0805,//Format:Integer ��ҵĵ�ǰ����ֵ
		SDO_GameTotal = 0x0806,//Format:Integer �ܾ���
		SDO_GameWin  = 0x0807,//Format:Integer ʤ����
		SDO_DogFall = 0x0808,//Format:Integer ƽ����
		SDO_GameFall = 0x0809,//Format:Integer ������
		SDO_Reputation = 0x0810,//Format:Integer ����ֵ
        SDO_GCash = 0x0811,//Format:Integer G��
		SDO_MCash = 0x0812,//Format:Integer M��
		SDO_Address = 0x0813,//Format:Integer ��ַ
		SDO_Age = 0x0814,//Format:Integer ����
		SDO_ProductID = 0x0815,//Format:Integer ��Ʒ���
		SDO_ProductName = 0x0816,//Format:String ��Ʒ����
		SDO_ItemCode  = 0x0817,//Format:Integer ���߱��
		SDO_ItemName = 0x0818,//Format:String ��������
		SDO_TimesLimit = 0x0819,//Format:Integer ʹ�ô���
		SDO_DateLimit = 0x0820,//Format:Integer ʹ��ʱЧ
		SDO_MoneyType = 0x0821,//Format:Integer ��������
		SDO_MoneyCost = 0x0822,//Format:Integer ���ߵļ۸�
		SDO_ShopTime = 0x0823,//Format:DateTime ����ʱ��
		SDO_MAINCH = 0x0824,//Format:Integer ������
		SDO_SUBCH = 0x0825,//Format:Integer ����
		SDO_Online = 0x0826,//Format:Integer �Ƿ�����
		SDO_LoginTime = 0x0827,//Format:DateTime ����ʱ��
		SDO_LogoutTime = 0x0828,//Format:DateTime ����ʱ��
		SDO_AREANAME = 0x0829,//Format:String ��������
		SDO_City = 0x0830,//Format:String �����ס����
		SDO_Title = 0x0831,//Format:String ��������
		SDO_Context = 0x0832,//Format:String ��������
		SDO_MinLevel = 0x0833,//Format:Integer �������ߵ���С�ȼ�
		SDO_ActiveStatus = 0x0834,//Format:Integer ����״̬
		SDO_StopStatus = 0x0835,//Format:Integer ��ͣ״̬
		SDO_NickName = 0x0836,//Format:String �س�
		SDO_9YouAccount = 0x0837,//Format:Integer 9you���ʺ�
		SDO_SEX = 0x0838,//Format:Integer �Ա�
		SDO_RegistDate =  0x0839,//Format:Date ע������
		SDO_FirstLogintime = 0x0840,//Format:Date ��һ�ε�¼ʱ��
		SDO_LastLogintime  = 0x0841,//Format:Date ���һ�ε�¼ʱ��
        SDO_Ispad = 0x0842,//Format:Integer �Ƿ���ע������̺
		SDO_Desc = 0x0843,//Format:String ��������
		SDO_Postion = 0x0844,//Format:Integer ����λ��
		SDO_BeginTime = 0x0845,//Format:Date ���Ѽ�¼��ʼʱ��
		SDO_EndTime = 0x0846,//Format:Date ���Ѽ�¼����ʱ��
		SDO_SendTime = 0x0847,//Format:Date ������������
		SDO_SendIndexID = 0x0848,//Format:Integer �����˵�ID
		SDO_SendUserID = 0x0849,//Format:String �������ʺ�
		SDO_ReceiveNick = 0x0850,//Format:String �������س�
		SDO_BigType = 0x0851,//Format:Integer ���ߴ���
		SDO_SmallType = 0x0852,//Format:Integer ����С��
        SDO_REASON = 0x0853,//Format:String ͣ������
        SDO_StopTime = 0x0854,//Format:TimeStamp ͣ��ʱ��
        SDO_DaysLimit  = 0x0855,//Format:Integer ʹ������
        SDO_Email = 0x0856,//Format:String �ʼ�
        SDO_ChargeSum = 0x0857,//Format:String ��ֵ�ϼ�
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
		SDO_Score = 0x08101,//����
		SDO_FirstPadTime = 0x08102,//����̺��һ��ʹ��ʱ��
		SDO_BanDate = 0x08103,//ͣ�������
        /// <summary>
        /// ��Ϸ�������б���
        /// </summary>
		ServerInfo_IP =  0x0901,//Format:String ������IP
		ServerInfo_City  =0x0902,//Format:String ����
		ServerInfo_GameID = 0x0903,//Format:Integer ��ϷID
		ServerInfo_GameName = 0x0904,//Format:String ��Ϸ��
		ServerInfo_GameDBID=  0x0905,//Format:Integer ��Ϸ���ݿ�����
        ServerInfo_GameFlag = 0x0906,//Format:Integer ��Ϸ������״̬
        ServerInfo_Idx = 0x0907,

        ///////////////////////////////////////////////////////////////////////
        /// <summary>
        /// �����Ŷ���
        /// </summary>
        AU_ACCOUNT = 0x1001,//����ʺ� Format:String
        AU_UserNick = 0x1002,//����س� Format:String
        AU_Sex = 0x1003,//����Ա� Format:Integer
        AU_State = 0x1004,//���״̬ Format:Integer
        AU_STOPSTATUS = 0x1005,//�����߷�ͣ״̬ Format:Integer
        AU_Reason = 0x1006,//��ͣ���� Format:String
        AU_BanDate = 0x1007,//��ͣ���� Format:TimeStamp
        AU_ServerIP = 0x1008,//��������Ϸ������ Format:String
        AU_Id9you = 0x1009, //Format:Integer 9youID
        AU_UserSN = 0x1010, //Format:Integer �û����к�
        AU_EquipState = 0x1011, //Format:String 
        AU_AvatarItem = 0x1012, //Format:Integer
        AU_BuyNick = 0x1013, //Format:String �����س�
        AU_BuyDate = 0x1014,//Format:Timestamp ��������
        AU_ExpireDate = 0x1015,//Format:TimesStamp  ��������
        AU_BuyType = 0x1016, // Format:Integer ��������

        AU_PresentID = 0x1017, //Format:Integer ����ID
        AU_SendSN = 0x1018, //Format:Integer  ����SN
        AU_SendNick = 0x1019, //Format:String �����س�
        AU_RecvSN = 0x1020, //Format:String ������SN
        AU_RecvNick = 0x1021, //Format:String �������س�
        AU_Kind = 0x1022, //Format:Integer ����
        AU_ItemID = 0x1023, //Format:Integer ����ID
        AU_Period = 0x1024, //Format:Integer �ڼ�
        AU_BeforeCash = 0x1025, //Format:Integer ����֮ǰ���
        AU_AfterCash = 0x1026, //Format:Integer ����֮����
        AU_SendDate = 0x1027, //Format:TimeStamp ��������
        AU_RecvDate = 0x1028,//Format:TimeStamp ��������
        AU_Memo = 0x1029,//Format:String ��ע
        AU_UserID = 0x1030, //Format:String ���ID
        AU_Exp = 0x1031, //Format:Integer ��Ҿ���
        AU_Point = 0x1032, //Format:Integer ���λ��
        AU_Money = 0x1033, //Format:Integer ��Ǯ
        AU_Cash = 0x1034, //Format:Integer �ֽ�
        AU_Level = 0x1035, //Format:Integer �ȼ�
        AU_Ranking = 0x1036, //Format:Integer ����
        AU_IsAllowMsg = 0x1037, //Format:Integer ������Ϣ
        AU_IsAllowInvite = 0x1038, //Format:Integer ��������
        AU_LastLoginTime = 0x1039, //Format:TimeStamp ����¼ʱ��
        AU_Password = 0x1040, //Format:String ����
        AU_UserName = 0x1041, //Format:String �û���
        AU_UserGender = 0x1042, //Format:String 
        AU_UserPower = 0x1043, //Format:Integer
        AU_UserRegion = 0x1044, //Format:String 
        AU_UserEMail = 0x1045, //Format:String �û������ʼ�
        AU_RegistedTime = 0x1046, //Format:TimeStamp ע��ʱ��
        AU_ItemName = 0x1047,//������
        AU_ItemStyle = 0x1048,//��������
        AU_Demo = 0x1049,//���� 
        AU_BeginTime = 0x1050,//��ʼʱ��
        AU_EndTime = 0x1051,//����ʱ��
        AU_SendUserID = 0x1052,//�������ʺ�
        AU_RecvUserID = 0x1053,//�������ʺ� 
        AU_SexIndex = 0x1054,//�Ա�
        ///////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ��񿨶�������
        /// </summary>
        CR_ServerIP = 0x1101,//������IP
        CR_ACCOUNT = 0x1102,//����ʺ� Format:String
        CR_Passord = 0x1103,//������� Format:String
        CR_NUMBER = 0x1104,//������ Format:String
        CR_ISUSE = 0x1105,//�Ƿ�ʹ��
        CR_STATUS = 0x1106,//���״̬ Format:Integer
        CR_ActiveIP = 0x1107,//���������IP Format:String
        CR_ActiveDate = 0x1108,//�������� Format:TimeStamp
        CR_BoardID = 0x1109,//����ID Format:Integer
        CR_BoardContext = 0x1110,//�������� Format:String
        CR_BoardColor = 0x1111,//������ɫ Format:String
        CR_ValidTime = 0x1112,//��Чʱ�� Format:TimeStamp
        CR_InValidTime = 0x1113,//ʧЧʱ�� Format:TimeStamp
        CR_Valid = 0x1114,//�Ƿ���Ч Format:Integer
        CR_PublishID = 0x1115,//������ID Format:Integer
        CR_DayLoop = 0x1116,//ÿ�첥�� Format:Integer
        CR_PSTID = 0x1117,//ע��� Format:Integer
        CR_SEX = 0x1118,//�Ա� Format:Integer
        CR_LEVEL = 0x1119,//�ȼ� Format:Integer
        CR_EXP = 0x1120,//���� Format:Integer
        CR_License = 0x1121,//����Format:Integer
        CR_Money = 0x1122,//��ǮFormat:Integer
        CR_RMB = 0x1123,//�����Format:Integer
        CR_RaceTotal = 0x1124,//��������Format:Integer
        CR_RaceWon = 0x1125,//ʤ������Format:Integer
        CR_ExpOrder = 0x1126,//��������Format:Integer
        CR_WinRateOrder = 0x1127,//ʤ������Format:Integer
        CR_WinNumOrder = 0x1128,//ʤ����������Format:Integer
        CR_SPEED = 0x1129,//�����ٶ�Format:Integer
        CR_Mode= 0x1130,//���ŷ�ʽ Format:Integer
        CR_ACTION = 0x1131,//��ѯ������Format:Integer
        CR_NickName = 0x1132,//�س� Format:String
        CR_Channel = 0x1133,//Ƶ��ID
        CR_UserID = 0x1134,//�û�ID
        CR_BoardContext1 = 0x1135,//����1
        CR_BoardContext2 = 0x1136,//����2
        CR_Expire = 0x1137,//��Ч��ʽ
        CR_ChannelID = 0x1138,//Ƶ��ID
        CR_ChannelName = 0x1139,//Ƶ������
		CR_Last_Login = 0x1140,//�ϴε���ʱ��
		CR_Last_Logout = 0x1141,//�ϴεǳ�ʱ��
		CR_Last_Playing_Time = 0x1142,//�ϴ���Ϸʱ��
		CR_Total_Time = 0x1143,//�ܵ���Ϸʱ��
		CR_UserName = 0x1144,//�������
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// һ��ͨ����
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
        CARD_id = 0x1251 ,//TLV_STRING ��֮��ע�Ῠ��
        CARD_username = 0x1252,//TLV_STRING ��֮��ע���û���
        CARD_nickname = 0x1253,//TLV_STRING ��֮��ע���س�
        CARD_password = 0x1254,//TLV_STRING ��֮��ע������
        CARD_sex = 0x1255,//TLV_STRING ��֮��ע���Ա�
        CARD_rdate = 0x1256,//TLV_Date ��֮��ע������
        CARD_rtime = 0x1257,//TLV_Time ��֮��ע��ʱ��
        CARD_securecode = 0x1258,//TLV_STRING ��ȫ��
        CARD_vis = 0x1259,//TLV_INTEGER
        CARD_logdate = 0x1260,//TLV_TimeStamp ����
        CARD_realname = 0x1263,//TLV_STRING ��ʵ����
        CARD_birthday = 0x1264,//TLV_Date ��������
        CARD_cardtype = 0x1265,//TLV_STRING
        CARD_email = 0x1267,//TLV_STRING �ʼ�
        CARD_occupation = 0x1268,//TLV_STRING ְҵ
        CARD_education = 0x1269,//TLV_STRING �����̶�
        CARD_marriage = 0x1270,//TLV_STRING ���
        CARD_constellation = 0x1271,//TLV_STRING ����
        CARD_shx = 0x1272,//TLV_STRING ��Ф
        CARD_city = 0x1273,//TLV_STRING ����
        CARD_address = 0x1274,//TLV_STRING ��ϵ��ַ
        CARD_phone = 0x1275,//TLV_STRING ��ϵ�绰
        CARD_qq = 0x1276,//TLV_STRING QQ
        CARD_intro = 0x1277,//TLV_STRING ����
        CARD_msn = 0x1278,//TLV_STRING MSN
        CARD_mobilephone = 0x1279,//TLV_STRING �ƶ��绰
        CARD_SumTotal = 0x1280,//TLV_INTEGER �ϼ�
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// �������̳�
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
		/// ������
		/// </summary>
		o2jam_ServerIP= 0x1401,//Format:TLV_STRING IP
		o2jam_UserID = 0x1402,//Format:TLV_STRING �û��ʺ�
		o2jam_UserNick = 0x1403,//Format:TLV_STRING �û��س�
		o2jam_Sex = 0x1404,//Format:TLV_BOOLEAN �Ա�
		o2jam_Level = 0x1405,//Format:TLV_INTEGER �ȼ�
		o2jam_Win = 0x1406,//Format:TLV_INTEGER ʤ
		o2jam_Draw = 0x1407,//Format:TLV_INTEGER ƽ
		o2jam_Lose = 0x1408,//Format:TLV_INTEGER ��
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
		/// ������II
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
		O2JAM2_UserNick = 0x1610,//Format:TLV_STRING �û��س�
		O2JAM2_Sex = 0x1611,//Format:TLV_BOOLEAN �Ա�
		O2JAM2_Level = 0x1612,//Format:TLV_INTEGER �ȼ�
		O2JAM2_Win = 0x1613,//Format:TLV_INTEGER ʤ
		O2JAM2_Draw = 0x1614,//Format:TLV_INTEGER ƽ
		O2JAM2_Lose = 0x1615,//Format:TLV_INTEGER ��
		O2JAM2_Exp = 0x1616,//Format:TLV_INTEGER ����
		O2JAM2_TOTAL =0x1617,//Format:TLV_INTEGER �ܾ���
		O2JAM2_GCash = 0x1618,//Format:TLV_INTEGER G��
		O2JAM2_MCash =0x1619,//Format:TLV_INTEGER M��
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
		/// �������� Add by KeHuaQing
		/// </summary>
		Soccer_ServerIP = 0x1701,
		Soccer_loginId = 0x1702,//Login ID
		Soccer_charsex = 0x1703,//��/Ů, ����(��/����/��ͨ/����/��) ����code
		Soccer_charidx = 0x1704,//��ɫ���к� (ExSoccer.dbo.t_character[idx])
		Soccer_charexp = 0x1705,//����ֵ
		Soccer_charlevel = 0x1706,//�ȼ�
		Soccer_charpoint = 0x1707,//���� 
		Soccer_match = 0x1708,//������
		Soccer_win = 0x1709,//ʤ��
		Soccer_lose = 0x1710,//ʧ��
		Soccer_draw = 0x1711,//ƽ��
		Soccer_drop = 0x1712,//ƽ��		
		Soccer_charname = 0x1713,//��ɫ��
		Soccer_charpos = 0x1714,//λ��
		Soccer_Type = 0x1715,//��ѯ����
		Soccer_String = 0x1716,//��ѯֵ
		Soccer_admid = 0x1717,//������ID
		Soccer_deleted_date = 0x1718,//ɾ������
		Soccer_status = 0x1719,//״̬
		Soccer_m_id = 0x1720,//������к� int
		Soccer_m_auth = 0x1721,//����Ƿ�ͣ�� int
		Soccer_regDate = 0x1722,//���ע������ string 
		Soccer_c_date = 0x1723,//��ɫ�������� string

		Soccer_char_max = 0x1724,//tinyint
		Soccer_char_cnt = 0x1725,//int
		Soccer_ret = 0x1726,//int
		Soccer_kind = 0x1727,//socket,name string

		ERROR_Msg = 0xFFFF //Format:STRING  ��֤�û�����Ϣ

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
		/// TAG��󳤶�
		/// </summary>
		public const int MAX_TAG_LENGTH = 30;
		public class TagStruct
		{
			/// <summary>
			/// ����TLV�ṹ
			/// </summary>
			/// <param name="_tag">��ǩ</param>
			/// <param name="_format">����</param>
			/// <param name="_len">����</param>
			/// <param name="_tag_buf">ֵ</param>
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
