using System;

namespace Common.Logic
{
	/// <summary>
	/// Packet_Head ��ժҪ˵����
	/// </summary>
	public enum Msg_Category:byte
	{
		COMMON = 0x80,//������Ϣ��
		USER_ADMIN = 0x81,//GM�ʺŲ�����Ϣ��
		MODULE_ADMIN = 0x82,//ģ�������Ϣ��
		USER_MODULE_ADMIN = 0x83,//�û���ģ�������Ϣ��
		GAME_ADMIN = 0x84, //��Ϸģ�������Ϣ��
		NOTES_ADMIN = 0x85,//NOTESģ�������Ϣ��
		MJ_ADMIN = 0x86,//�ͽ�GM���߲�����Ϣ��
		SDO_ADMIN = 0x87,//�������߲�����Ϣ��
        AU_ADMIN = 0x88,//�����Ų�����Ϣ��
        CR_ADMIN = 0x89,//��񿨶���������Ϣ��
        CARD_ADMIN = 0x90,//�û���ֵ���Ѽ�¼��Ϣ��
        AUSHOP_ADMIN = 0x91,//�������̳Ǽ�¼��Ϣ��
		O2JAM_ADMIN = 0x92,//�����ż�¼��Ϣ��
		O2JAM2_ADMIN = 0x93,//������II��¼��Ϣ��
		SOCCER_ADMIN = 0x94,//���������¼��Ϣ��
		ERROR = 0xFF
	}
	public enum	ServiceKey:ushort
	{
		/// <summary>
		/// ����ģ��(0x80)
		/// </summary>
		CONNECT = 0x0001,
		CONNECT_RESP = 0x8001,
		DISCONNECT = 0x0002,
		DISCONNECT_RESP = 0x8002,
		ACCOUNT_AUTHOR = 0x0003,
		ACCOUNT_AUTHOR_RESP = 0x8003,
		SERVERINFO_IP_QUERY = 0x0004,
		SERVERINFO_IP_QUERY_RESP = 0x8004,
		GMTOOLS_OperateLog_Query = 0x0005,//�鿴���߲�����¼
		GMTOOLS_OperateLog_Query_RESP = 0x8005,//�鿴���߲�����¼��Ӧ
        SERVERINFO_IP_ALL_QUERY = 0x0006,//�鿴������Ϸ��������ַ
        SERVERINFO_IP_ALL_QUERY_RESP = 0x8006,//�鿴������Ϸ��������ַ��Ӧ
        LINK_SERVERIP_CREATE = 0x0007,//�����Ϸ�������ݿ�
        LINK_SERVERIP_CREATE_RESP = 0x8007,//�����Ϸ�������ݿ���Ӧ
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
		///�û�����ģ��(0x81) 
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
        UPDATE_ACTIVEUSER = 0x0008,//���������û�״̬
        UPDATE_ACTIVEUSER_RESP = 0x8008,//���������û�״̬��Ӧ
		DEPARTMENT_CREATE = 0x0009,//���Ŵ���
		DEPARTMENT_CREATE_RESP = 0x8009,//���Ŵ�����Ӧ
		DEPARTMENT_UPDATE= 0x0010,//�����޸�
		DEPARTMENT_UPDATE_RESP = 0x8010,//�����޸���Ӧ
		DEPARTMENT_DELETE= 0x0011,//����ɾ��
		DEPARTMENT_DELETE_RESP = 0x8011,//����ɾ����Ӧ
		DEPARTMENT_ADMIN = 0x0012,//���Ź���
		DEPARTMENT_ADMIN_RESP = 0x8012,//���Ź�����Ӧ
		DEPARTMENT_RELATE_QUERY = 0x0013,//���Ź�����ѯ
		DEPARTMENT_RELATE_QUERY_RESP = 0x8013,//���Ź�����ѯ
		DEPART_RELATE_GAME_QUERY = 0x0014,
		DEPART_RELATE_GAME_QUERY_RESP = 0x8014,
		USER_SYSADMIN_QUERY = 0x0015,
		USER_SYSADMIN_QUERY_RESP = 0x8015,

		/// <summary>
		/// ģ�����(0x82)
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
		/// �û���ģ��(0x83) 
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
		/// ��Ϸ����(0x84) 
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
		/// NOTES����(0x85) 
		/// </summary>
		NOTES_LETTER_TRANSFER = 0x0001,
		NOTES_LETTER_TRANSFER_RESP = 0x8001,
		NOTES_LETTER_PROCESS = 0x0002, //�ʼ�����
		NOTES_LETTER_PROCESS_RESP = 0x8002,//�ʼ�����
		NOTES_LETTER_TRANSMIT = 0x0003,//�ʼ�ת��
		NOTES_LETTER_TRANSMIT_RESP = 0x8003,//�ʼ�ת����Ӧ
        
		/// <summary>
		/// �ͽ�GM���߹���(0x86)
		/// </summary>
		MJ_CHARACTERINFO_QUERY = 0x0001,//������״̬
		MJ_CHARACTERINFO_QUERY_RESP = 0x8001,//������״̬��Ӧ
		MJ_CHARACTERINFO_UPDATE = 0x0002,//�޸����״̬
		MJ_CHARACTERINFO_UPDATE_RESP = 0x8002,//�޸����״̬��Ӧ
		MJ_LOGINTABLE_QUERY = 0x0003,//�������Ƿ�����
		MJ_LOGINTABLE_QUERY_RESP = 0x8003,//�������Ƿ�������Ӧ
		MJ_CHARACTERINFO_EXPLOIT_UPDATE = 0x0004,//�޸Ĺ�ѫֵ
		MJ_CHARACTERINFO_EXPLOIT_UPDATE_RESP = 0x8004,//�޸Ĺ�ѫֵ��Ӧ
		MJ_CHARACTERINFO_FRIEND_QUERY = 0x0005,//�г���������
		MJ_CHARACTERINFO_FRIEND_QUERY_RESP = 0x8005,//�г�����������Ӧ
		MJ_CHARACTERINFO_FRIEND_CREATE = 0x0006,//��Ӻ���
		MJ_CHARACTERINFO_FRIEND_CREATE_RESP = 0x8006,//��Ӻ�����Ӧ
		MJ_CHARACTERINFO_FRIEND_DELETE = 0x0007,//ɾ������
		MJ_CHARACTERINFO_FRIEND_DELETE_RESP = 0x8007,//ɾ��������Ӧ
		MJ_GUILDTABLE_UPDATE = 0x0008,//�޸ķ����������Ѵ��ڰ��
		MJ_GUILDTABLE_UPDATE_RESP = 0x8008,//�޸ķ����������Ѵ��ڰ����Ӧ
		MJ_ACCOUNT_LOCAL_CREATE = 0x0009,//���������ϵ�account����������Ϣ���浽���ط�������
		MJ_ACCOUNT_LOCAL_CREATE_RESP = 0x8009,//���������ϵ�account����������Ϣ���浽���ط���������Ӧ
		MJ_ACCOUNT_REMOTE_DELETE = 0x0010,//���÷�ͣ�ʺ�
		MJ_ACCOUNT_REMOTE_DELETE_RESP = 0x8010,//���÷�ͣ�ʺŵ���Ӧ
		MJ_ACCOUNT_REMOTE_RESTORE = 0x0011,//����ʺ�
		MJ_ACCOUNT_REMOTE_RESTORE_RESP = 0x8011,//����ʺ���Ӧ
		MJ_ACCOUNT_LIMIT_RESTORE = 0x0012,//��ʱ�޵ķ�ͣ
		MJ_ACCOUNT_LIMIT_RESTORE_RESP = 0x8012,//��ʱ�޵ķ�ͣ��Ӧ
		MJ_ACCOUNTPASSWD_LOCAL_CREATE = 0x0013,//����������뵽���� 
		MJ_ACCOUNTPASSWD_LOCAL_CREATE_RESP = 0x8013,//����������뵽����
		MJ_ACCOUNTPASSWD_REMOTE_UPDATE = 0x0014,//�޸�������� 
		MJ_ACCOUNTPASSWD_REMOTE_UPDATE_RESP = 0x8014,//�޸��������
		MJ_ACCOUNTPASSWD_REMOTE_RESTORE = 0x0015,//�ָ��������
		MJ_ACCOUNTPASSWD_REMOTE_RESTORE_RESP = 0x8015,//�ָ��������
		MJ_ITEMLOG_QUERY = 0x0016,//�����û����׼�¼
		MJ_ITEMLOG_QUERY_RESP = 0x8016,//�����û����׼�¼
		MJ_GMTOOLS_LOG_QUERY = 0x0017,//���ʹ���߲�����¼
		MJ_GMTOOLS_LOG_QUERY_RESP = 0x8017,//���ʹ���߲�����¼
		MJ_MONEYSORT_QUERY = 0x0018,//���ݽ�Ǯ����
		MJ_MONEYSORT_QUERY_RESP = 0x8018,//���ݽ�Ǯ�������Ӧ
		MJ_LEVELSORT_QUERY = 0x0019,//���ݵȼ�����
		MJ_LEVELSORT_QUERY_RESP = 0x8019,//���ݵȼ��������Ӧ
		MJ_MONEYFIGHTERSORT_QUERY = 0x0020,//���ݲ�ְͬҵ��Ǯ����
		MJ_MONEYFIGHTERSORT_QUERY_RESP = 0x8020,//���ݲ�ְͬҵ��Ǯ�������Ӧ
		MJ_LEVELFIGHTERSORT_QUERY = 0x0021,//���ݲ�ְͬҵ�ȼ�����
		MJ_LEVELFIGHTERSORT_QUERY_RESP = 0x8021,//���ݲ�ְͬҵ�ȼ��������Ӧ
		MJ_MONEYTAOISTSORT_QUERY = 0x0022,//���ݵ�ʿ��Ǯ����
		MJ_MONEYTAOISTSORT_QUERY_RESP = 0x8022,//���ݵ�ʿ��Ǯ�������Ӧ
		MJ_LEVELTAOISTSORT_QUERY = 0x0023,//���ݵ�ʿ�ȼ�����
		MJ_LEVELTAOISTSORT_QUERY_RESP = 0x8023,//���ݵ�ʿ�ȼ��������Ӧ
		MJ_MONEYRABBISORT_QUERY = 0x0024,//���ݷ�ʦ��Ǯ����
		MJ_MONEYRABBISORT_QUERY_RESP = 0x8024,//���ݷ�ʦ��Ǯ�������Ӧ
		MJ_LEVELRABBISORT_QUERY = 0x0025,//���ݷ�ʦ�ȼ�����
		MJ_LEVELRABBISORT_QUERY_RESP = 0x8025,//���ݷ�ʦ�ȼ��������Ӧ
		MJ_ACCOUNT_QUERY =  0x0026,//�ͽ��ʺŲ�ѯ
		MJ_ACCOUNT_QUERY_RESP = 0x8026,//�ͽ��ʺŲ�ѯ��Ӧ
        MJ_ACCOUNT_LOCAL_QUERY = 0x0027,//��ѯ�ͽ������ʺ�
        MJ_ACCOUNT_LOCAL_QUERY_RESP = 0x8027,//��ѯ�ͽ������ʺ���Ӧ

		/// <summary>
		/// SDO_ADMIN �������߹��߲�����Ϣ��
		/// </summary>
		SDO_ACCOUNT_QUERY = 0x0026,//�鿴��ҵ��ʺ���Ϣ
		SDO_ACCOUNT_QUERY_RESP = 0x8026,//�鿴��ҵ��ʺ���Ϣ��Ӧ
		SDO_CHARACTERINFO_QUERY = 0x0027,//�鿴�������ϵ���Ϣ
		SDO_CHARACTERINFO_QUERY_RESP = 0x8027,//�鿴�������ϵ���Ϣ��Ӧ
		SDO_ACCOUNT_CLOSE = 0x0028,//��ͣ�ʻ���Ȩ����Ϣ
		SDO_ACCOUNT_CLOSE_RESP = 0x8028,//��ͣ�ʻ���Ȩ����Ϣ��Ӧ
		SDO_ACCOUNT_OPEN = 0x0029,//����ʻ���Ȩ����Ϣ
		SDO_ACCOUNT_OPEN_RESP = 0x8029,//����ʻ���Ȩ����Ϣ��Ӧ
		SDO_PASSWORD_RECOVERY = 0x0030,//����һ�����
		SDO_PASSWORD_RECOVERY_RESP = 0x8030,//����һ�������Ӧ
		SDO_CONSUME_QUERY = 0x0031,//�鿴��ҵ����Ѽ�¼
		SDO_CONSUME_QUERY_RESP = 0x8031,//�鿴��ҵ����Ѽ�¼��Ӧ
		SDO_USERONLINE_QUERY = 0x0032,//�鿴���������״̬
		SDO_USERONLINE_QUERY_RESP = 0x8032,//�鿴���������״̬��Ӧ
		SDO_USERTRADE_QUERY = 0x0033,//�鿴��ҽ���״̬
		SDO_USERTRADE_QUERY_RESP = 0x8033,//�鿴��ҽ���״̬��Ӧ
		SDO_CHARACTERINFO_UPDATE = 0x0034,//�޸���ҵ��˺���Ϣ
		SDO_CHARACTERINFO_UPDATE_RESP = 0x8034,//�޸���ҵ��˺���Ϣ��Ӧ
		SDO_ITEMSHOP_QUERY = 0x0035,//�鿴��Ϸ�������е�����Ϣ
		SDO_ITEMSHOP_QUERY_RESP = 0x8035,//�鿴��Ϸ�������е�����Ϣ��Ӧ
		SDO_ITEMSHOP_DELETE = 0x0036,//ɾ����ҵ�����Ϣ
		SDO_ITEMSHOP_DELETE_RESP  = 0x8036,//ɾ����ҵ�����Ϣ��Ӧ
		SDO_GIFTBOX_CREATE  = 0x0037,//����������е�����Ϣ
		SDO_GIFTBOX_CREATE_RESP  = 0x8037,//����������е�����Ϣ��Ӧ
		SDO_GIFTBOX_QUERY = 0x0038,//�鿴�������еĵ���
		SDO_GIFTBOX_QUERY_RESP = 0x8038,//�鿴�������еĵ�����Ӧ
		SDO_GIFTBOX_DELETE = 0x0039,//ɾ���������еĵ���
		SDO_GIFTBOX_DELETE_RESP = 0x8039,//ɾ���������еĵ�����Ӧ
		SDO_USERLOGIN_STATUS_QUERY = 0x0040,//�鿴��ҵ�¼״̬
		SDO_USERLOGIN_STATUS_QUERY_RESP = 0x8040,//�鿴��ҵ�¼״̬��Ӧ
		SDO_ITEMSHOP_BYOWNER_QUERY = 0x0041,////�鿴������ϵ�����Ϣ
		SDO_ITEMSHOP_BYOWNER_QUERY_RESP = 0x8041,////�鿴������ϵ�����Ϣ
		SDO_ITEMSHOP_TRADE_QUERY = 0x0042,//�鿴��ҽ��׼�¼��Ϣ
		SDO_ITEMSHOP_TRADE_QUERY_RESP = 0x8042,//�鿴��ҽ��׼�¼��Ϣ����Ӧ
		SDO_MEMBERSTOPSTATUS_QUERY = 0x0043,//�鿴���ʺ�״̬
		SDO_MEMBERSTOPSTATUS_QUERY_RESP = 0x8043,///�鿴���ʺ�״̬����Ӧ
        SDO_MEMBERBANISHMENT_QUERY = 0x0044,//�鿴����ͣ����ʺ�
        SDO_MEMBERBANISHMENT_QUERY_RESP = 0x8044,//�鿴����ͣ����ʺ���Ӧ
        SDO_USERMCASH_QUERY = 0x0045,//��ҳ�ֵ��¼��ѯ
        SDO_USERMCASH_QUERY_RESP = 0x8045,//��ҳ�ֵ��¼��ѯ��Ӧ
        SDO_USERGCASH_UPDATE = 0x0046,//�������G��
        SDO_USERGCASH_UPDATE_RESP = 0x8046,//�������G�ҵ���Ӧ
        SDO_MEMBERLOCAL_BANISHMENT = 0x0047,//���ر���ͣ����Ϣ
        SDO_MEMBERLOCAL_BANISHMENT_RESP = 0x8047,//���ر���ͣ����Ϣ��Ӧ
        SDO_EMAIL_QUERY = 0x0048,//�õ���ҵ�EMAIL
        SDO_EMAIL_QUERY_RESP = 0x8048,//�õ���ҵ�EMAIL��Ӧ
        SDO_USERCHARAGESUM_QUERY = 0x0049,//�õ���ֵ��¼�ܺ�
        SDO_USERCHARAGESUM_QUERY_RESP = 0x8049,//�õ���ֵ��¼�ܺ���Ӧ
        SDO_USERCONSUMESUM_QUERY = 0x0050,//�õ����Ѽ�¼�ܺ�
        SDO_USERCONSUMESUM_QUERY_RESP = 0x8050,//�õ����Ѽ�¼�ܺ���Ӧ
        SDO_USERGCASH_QUERY = 0x0051,//��ңǱҼ�¼��ѯ
        SDO_USERGCASH_QUERY_RESP = 0x8051,//��ңǱҼ�¼��ѯ��Ӧ
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
		/// AU_ADMIN �����Ź��߲�����Ϣ��
		/// </summary>
		AU_ACCOUNT_QUERY = 0x0001,//����ʺ���Ϣ��ѯ
		AU_ACCOUNT_QUERY_RESP = 0x8001,//����ʺ���Ϣ��ѯ��Ӧ
		AU_ACCOUNTREMOTE_QUERY = 0x0002,//��Ϸ��������ͣ������ʺŲ�ѯ
		AU_ACCOUNTREMOTE_QUERY_RESP = 0x8002,//��Ϸ��������ͣ������ʺŲ�ѯ��Ӧ
		AU_ACCOUNTLOCAL_QUERY = 0x0003,//���ط�ͣ������ʺŲ�ѯ
		AU_ACCOUNTLOCAL_QUERY_RESP = 0x8003,//���ط�ͣ������ʺŲ�ѯ��Ӧ
		AU_ACCOUNT_CLOSE = 0x0004,//��ͣ������ʺ�
		AU_ACCOUNT_CLOSE_RESP = 0x8004,//��ͣ������ʺ���Ӧ
		AU_ACCOUNT_OPEN = 0x0005,//��������ʺ�
		AU_ACCOUNT_OPEN_RESP = 0x8005,//��������ʺ���Ӧ
		AU_ACCOUNT_BANISHMENT_QUERY = 0x0006,//��ҷ�ͣ�ʺŲ�ѯ
		AU_ACCOUNT_BANISHMENT_QUERY_RESP = 0x8006,//��ҷ�ͣ�ʺŲ�ѯ��Ӧ
		AU_CHARACTERINFO_QUERY = 0x0007,//��ѯ��ҵ��˺���Ϣ
		AU_CHARACTERINFO_QUERY_RESP = 0x8007,//��ѯ��ҵ��˺���Ϣ��Ӧ
		AU_CHARACTERINFO_UPDATE = 0x0008,//�޸���ҵ��˺���Ϣ
		AU_CHARACTERINFO_UPDATE_RESP = 0x8008,//�޸���ҵ��˺���Ϣ��Ӧ
		AU_ITEMSHOP_QUERY = 0x0009,//�鿴��Ϸ�������е�����Ϣ
		AU_ITEMSHOP_QUERY_RESP = 0x8009,//�鿴��Ϸ�������е�����Ϣ��Ӧ
		AU_ITEMSHOP_DELETE = 0x0010,//ɾ����ҵ�����Ϣ
		AU_ITEMSHOP_DELETE_RESP = 0x8010,//ɾ����ҵ�����Ϣ��Ӧ
		AU_ITEMSHOP_BYOWNER_QUERY = 0x0011,////�鿴������ϵ�����Ϣ
		AU_ITEMSHOP_BYOWNER_QUERY_RESP = 0x8011,////�鿴������ϵ�����Ϣ
		AU_ITEMSHOP_TRADE_QUERY = 0x0012,//�鿴��ҽ��׼�¼��Ϣ
		AU_ITEMSHOP_TRADE_QUERY_RESP = 0x8012,//�鿴��ҽ��׼�¼��Ϣ����Ӧ
		AU_ITEMSHOP_CREATE = 0x0013,//����������е�����Ϣ
		AU_ITEMSHOP_CREATE_RESP = 0x8013,//����������е�����Ϣ��Ӧ
		AU_LEVELEXP_QUERY = 0x0014,//�鿴��ҵȼ�����
		AU_LEVELEXP_QUERY_RESP = 0x8014,////�鿴��ҵȼ�������Ӧ
		AU_USERLOGIN_STATUS_QUERY = 0x0015,//�鿴��ҵ�¼״̬
		AU_USERLOGIN_STATUS_QUERY_RESP = 0x8015,//�鿴��ҵ�¼״̬��Ӧ
		AU_USERCHARAGESUM_QUERY = 0x0016,//�õ���ֵ��¼�ܺ�
		AU_USERCHARAGESUM_QUERY_RESP = 0x8016,//�õ���ֵ��¼�ܺ���Ӧ
		AU_CONSUME_QUERY = 0x0017,//�鿴��ҵ����Ѽ�¼
		AU_CONSUME_QUERY_RESP = 0x8017,//�鿴��ҵ����Ѽ�¼��Ӧ
		AU_USERCONSUMESUM_QUERY = 0x0018,//�õ����Ѽ�¼�ܺ�
		AU_USERCONSUMESUM_QUERY_RESP = 0x8018,//�õ����Ѽ�¼�ܺ���Ӧ
		AU_USERMCASH_QUERY = 0x0019,//��ҳ�ֵ��¼��ѯ
		AU_USERMCASH_QUERY_RESP = 0x8019,//��ҳ�ֵ��¼��ѯ��Ӧ
		AU_USERGCASH_QUERY = 0x0020,//��ңǱҼ�¼��ѯ
		AU_USERGCASH_QUERY_RESP = 0x8020,//��ңǱҼ�¼��ѯ��Ӧ
		AU_USERGCASH_UPDATE = 0x0021,//�������G��
		AU_USERGCASH_UPDATE_RESP = 0x8021,//�������G�ҵ���Ӧ
		AU_USERNICK_UPDATE = 0x0022,
		AU_USERNICK_UPDATE_RESP = 0x8022,

		/// <summary>
		/// CR_ADMIN ��񿨶������߲�����Ϣ��
		/// </summary>
		CR_ACCOUNT_QUERY = 0x0001,//����ʺ���Ϣ��ѯ
		CR_ACCOUNT_QUERY_RESP = 0x8001,//����ʺ���Ϣ��ѯ��Ӧ
		CR_ACCOUNTACTIVE_QUERY = 0x0002,//����ʺż�����Ϣ
		CR_ACCOUNTACTIVE_QUERY_RESP = 0x8002,//����ʺż�����Ӧ
		CR_CALLBOARD_QUERY = 0x0003,//������Ϣ��ѯ
		CR_CALLBOARD_QUERY_RESP = 0x8003,//������Ϣ��ѯ��Ӧ
		CR_CALLBOARD_CREATE = 0x0004,//��������
		CR_CALLBOARD_CREATE_RESP = 0x8004,//����������Ӧ
		CR_CALLBOARD_UPDATE = 0x0005,//���¹�����Ϣ
		CR_CALLBOARD_UPDATE_RESP = 0x8005,//���¹�����Ϣ����Ӧ
		CR_CALLBOARD_DELETE = 0x0006,//ɾ��������Ϣ
		CR_CALLBOARD_DELETE_RESP = 0x8006,//ɾ��������Ϣ����Ӧ
		CR_CHARACTERINFO_QUERY = 0x0007,//��ҽ�ɫ��Ϣ��ѯ
		CR_CHARACTERINFO_QUERY_RESP = 0x8007,//��ҽ�ɫ��Ϣ��ѯ����Ӧ
		CR_CHARACTERINFO_UPDATE = 0x0008,//��ҽ�ɫ��Ϣ��ѯ
		CR_CHARACTERINFO_UPDATE_RESP = 0x8008,//��ҽ�ɫ��Ϣ��ѯ����Ӧ
		CR_CHANNEL_QUERY = 0x0009,//����Ƶ����ѯ
		CR_CHANNEL_QUERY_RESP = 0x8009,//����Ƶ����ѯ����Ӧ
		CR_NICKNAME_QUERY = 0x0010,//����ǳƲ�ѯ
		CR_NICKNAME_QUERY_RESP = 0x8010,//����ǳƵ���Ӧ
		CR_LOGIN_LOGOUT_QUERY = 0x0011,//������ߡ�����ʱ���ѯ
		CR_LOGIN_LOGOUT_QUERY_RESP = 0x8011,//������ߡ�����ʱ���ѯ����Ӧ
		CR_ERRORCHANNEL_QUERY = 0x0012,//������󹫸�Ƶ����ѯ
		CR_ERRORCHANNEL_QUERY_RESP = 0x8012,//������󹫸�Ƶ����ѯ����Ӧ

		/// <summary>
		/// ��ֵ����GM����(0x90)
		/// </summary>
		CARD_USERCHARGEDETAIL_QUERY = 0x0001,//һ��ͨ��ѯ
		CARD_USERCHARGEDETAIL_QUERY_RESP = 0x8001,//һ��ͨ��ѯ��Ӧ
		CARD_USERDETAIL_QUERY = 0x0002,//��֮���û�ע����Ϣ��ѯ
		CARD_USERDETAIL_QUERY_RESP = 0x8002,//��֮���û�ע����Ϣ��ѯ��Ӧ
		CARD_USERCONSUME_QUERY = 0x0003,//���б����Ѳ�ѯ
		CARD_USERCONSUME_QUERY_RESP = 0x8003,//���б����Ѳ�ѯ��Ӧ
		CARD_VNETCHARGE_QUERY = 0x0004,//�����ǿճ�ֵ��ѯ
		CARD_VNETCHARGE_QUERY_RESP = 0x8004,//�����ǿճ�ֵ��ѯ��Ӧ
		CARD_USERDETAIL_SUM_QUERY = 0x0005,//��ֵ�ϼƲ�ѯ
		CARD_USERDETAIL_SUM_QUERY_RESP = 0x8005,//��ֵ�ϼƲ�ѯ��Ӧ
		CARD_USERCONSUME_SUM_QUERY = 0x0006,//���ѺϼƲ�ѯ
		CARD_USERCONSUME_SUM_QUERY_RESP = 0x8006,//���Ѻϼ���Ӧ
		CARD_USERINFO_QUERY = 0x0007,//���ע����Ϣ��ѯ
		CARD_USERINFO_QUERY_RESP = 0x8007,//���ע����Ϣ��ѯ��Ӧ
		CARD_USERINFO_CLEAR = 0x0008,
		CARD_USERINFO_CLEAR_RESP = 0x8008,
		CARD_USERSECURE_CLEAR = 0x0009,//������Ұ�ȫ����Ϣ
		CARD_USERSECURE_CLEAR_RESP = 0x8009,//������Ұ�ȫ����Ϣ��Ӧ
		CARD_USERNICK_QUERY = 0x0010,
		CARD_USERNICK_QUERY_RESP = 0x8010,
		CARD_USERLOCK_UPDATE = 0x0011,
		CARD_USERLOCK_UPDATE_RESP = 0x8011,

        /// <summary>
        /// �������̳�(0x91)
        /// </summary>
        AUSHOP_USERGPURCHASE_QUERY = 0x0001,//�û�G�ҹ����¼
        AUSHOP_USERGPURCHASE_QUERY_RESP = 0x8001,//�û�G�ҹ����¼
        AUSHOP_USERMPURCHASE_QUERY = 0x0002,//�û�M�ҹ����¼
        AUSHOP_USERMPURCHASE_QUERY_RESP = 0x8002,//�û�M�ҹ����¼
        AUSHOP_AVATARECOVER_QUERY = 0x0003,//���߻��նһ���
        AUSHOP_AVATARECOVER_QUERY_RESP = 0x8003,//���߻��նһ���
        AUSHOP_USERINTERGRAL_QUERY = 0x0004,//�û����ּ�¼
        AUSHOP_USERINTERGRAL_QUERY_RESP = 0x8004,//�û����ּ�¼
        AUSHOP_USERGPURCHASE_SUM_QUERY = 0x0005,//�û�G�ҹ����¼�ϼ�
        AUSHOP_USERGPURCHASE_SUM_QUERY_RESP = 0x8005,//�û�G�ҹ����¼�ϼ���Ӧ
        AUSHOP_USERMPURCHASE_SUM_QUERY = 0x0006,//�û�M�ҹ����¼�ϼ�
        AUSHOP_USERMPURCHASE_SUM_QUERY_RESP = 0x8006,//�û�M�ҹ����¼�ϼ���Ӧ
		AUSHOP_AVATARECOVER_DETAIL_QUERY = 0x0007,//���߻��նһ���ϸ��¼
		AUSHOP_AVATARECOVER_DETAIL_QUERY_RESP = 0x8007,//���߻��նһ���ϸ��¼

		/// <summary>
		/// �����Ź���(0x92)
		/// </summary>
		O2JAM_CHARACTERINFO_QUERY= 0x0001,//��ҽ�ɫ��Ϣ��ѯ
		O2JAM_CHARACTERINFO_QUERY_RESP= 0x8001,//��ҽ�ɫ��Ϣ��ѯ
		O2JAM_CHARACTERINFO_UPDATE= 0x0002,//��ҽ�ɫ��Ϣ����
		O2JAM_CHARACTERINFO_UPDATE_RESP= 0x8002,//��ҽ�ɫ��Ϣ����
		O2JAM_ITEM_QUERY= 0x0003,//��ҵ�����Ϣ��ѯ
		O2JAM_ITEM_QUERY_RESP= 0x8003,//��ҽ�ɫ��Ϣ��ѯ
		O2JAM_ITEM_UPDATE= 0x0004,//��ҵ�����Ϣ����
		O2JAM_ITEM_UPDATE_RESP= 0x8004,//��ҵ�����Ϣ����
		O2JAM_CONSUME_QUERY= 0x0005,//���������Ϣ��ѯ
		O2JAM_CONSUME_QUERY_RESP= 0x8005,//���������Ϣ��ѯ
		O2JAM_ITEMDATA_QUERY= 0x0006,//�����б��ѯ
		O2JAM_ITEMDATA_QUERY_RESP= 0x8006,//�����б���Ϣ��ѯ
		O2JAM_GIFTBOX_QUERY = 0x0007,//�������в�ѯ
		O2JAM_GIFTBOX_QUERY_RESP = 0x8007,//�������в�ѯ
		O2JAM_USERGCASH_UPDATE = 0x0008,//�������G��
		O2JAM_USERGCASH_UPDATE_RESP = 0x8008,//�������G�ҵ���Ӧ
		O2JAM_CONSUME_SUM_QUERY= 0x0009,//���������Ϣ��ѯ
		O2JAM_CONSUME_SUM_QUERY_RESP= 0x8009,//���������Ϣ��ѯ
		O2JAM_GIFTBOX_CREATE= 0x0010,//����������е���
		O2JAM_GIFTBOX_CREATE_RESP= 0x8010,//����������е���
		O2JAM_ITEMNAME_QUERY = 0x0011,
		O2JAM_ITEMNAME_QUERY_RESP = 0x8011,
		O2JAM_GIFTBOX_DELETE = 0x0012,
		O2JAM_GIFTBOX_DELETE_RESP  =0x8012,

		/// <summary>
		/// ������IIGM����(0x93)
		/// </summary>
		O2JAM2_ACCOUNT_QUERY = 0x0001,//����ʺ���Ϣ��ѯ
		O2JAM2_ACCOUNT_QUERY_RESP = 0x8001,//����ʺ���Ϣ��ѯ��Ӧ
		O2JAM2_ACCOUNTACTIVE_QUERY = 0x0002,//����ʺż�����Ϣ
		O2JAM2_ACCOUNTACTIVE_QUERY_RESP = 0x8002,//����ʺż�����Ӧ
		O2JAM2_CHARACTERINFO_QUERY = 0x0003,//�û���Ϣ��ѯ
		O2JAM2_CHARACTERINFO_QUERY_RESP = 0x8003,
		O2JAM2_CHARACTERINFO_UPDATE = 0x0004,//�û���Ϣ�޸�
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
		O2JAM2_ACCOUNT_CLOSE = 0x0015,//��ͣ�ʻ���Ȩ����Ϣ
		O2JAM2_ACCOUNT_CLOSE_RESP = 0x8015,//��ͣ�ʻ���Ȩ����Ϣ��Ӧ
		O2JAM2_ACCOUNT_OPEN = 0x0016,//����ʻ���Ȩ����Ϣ
		O2JAM2_ACCOUNT_OPEN_RESP = 0x8016,//����ʻ���Ȩ����Ϣ��Ӧ
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
		/// �������� Add by KeHuaQing 2006-09-14
		/// </summary>
		SOCCER_CHARACTERINFO_QUERY = 0x0001,//�û���Ϣ��ѯ
		SOCCER_CHARACTERINFO_QUERY_RESP = 0x8001,
		SOCCER_CHARCHECK_QUERY = 0x0002,//�û�NameCheck,SocketCheck
		SOCCER_CHARCHECK_QUERY_RESP = 0x8002,
		SOCCER_CHARITEMS_RECOVERY_QUERY = 0x0003,//�û�����
		SOCCER_CHARITEMS_RECOVERY_QUERY_RESP = 0x8003,
		SOCCER_CHARPOINT_QUERY = 0x0004,//�û�G���޸�
		SOCCER_CHARPOINT_QUERY_RESP = 0x8004,
		SOCCER_DELETEDCHARACTERINFO_QUERY = 0x0005,//ɾ���û���ѯ
		SOCCER_DELETEDCHARACTERINFO_QUERY_RESP = 0x8005,
		SOCCER_CHARACTERSTATE_MODIFY = 0x0006,//ͣ���ɫ
		SOCCER_CHARACTERSTATE_MODIFY_RESP = 0x8006,
		SOCCER_ACCOUNTSTATE_MODIFY = 0x0007,//ͣ�����
		SOCCER_ACCOUNTSTATE_MODIFY_RESP = 0x8007,
		SOCCER_CHARACTERSTATE_QUERY = 0x0008,//ͣ���ɫ��ѯ
		SOCCER_CHARACTERSTATE_QUERY_RESP = 0x8008,
		SOCCER_ACCOUNTSTATE_QUERY = 0x0009,//ͣ����Ҳ�ѯ
		SOCCER_ACCOUNTSTATE_QUERY_RESP = 0x8009,

		ERROR = 0xFFFF
	}

	/// <summary>
	/// Packet_Head ��ժҪ˵����
	/// </summary>
	public class Packet_Head
	{
		/// <summary>
		/// ��Ϣ����󳤶�
		/// </summary>
		public const uint HEAD_LENGTH = 16;
		/// <summary>
		/// ��Ϣ��byte����
		/// </summary>
		public byte[] m_bHeadBuffer;
		/// <summary>
		/// ��Ϣ��byte���鳤��
		/// </summary>
		public uint m_uiHeadBufferLen = HEAD_LENGTH;
		/// <summary>
		/// ��Ϣ���к�ID
		/// </summary>
		public uint m_uiSeqID;
		/// <summary>
		/// ��Ϣ����
		/// </summary>
		public Msg_Category m_mcCategory;
		/// <summary>
		/// ��Ϣ����
		/// </summary>
		public ServiceKey m_skServiceKey;
		/// <summary>
		/// ��Ϣ����������
		/// </summary>
		public DateTime m_dtMsgDateTime;
		/// <summary>
		/// ��Ϣ�峤��
		/// </summary>
		public uint m_uiBodyLen;
		/// <summary>
		/// �Ƿ��ǺϷ���Ϣͷ
		/// </summary>
		public bool IsValidHead = false;

		public Packet_Head()
		{
		}
		/// <summary>
		/// ������Ϣͷ
		/// </summary>
		/// <param name="uiSeqID">��Ϣ���к�</param>
		/// <param name="mcCategory">��Ϣ����</param>
		/// <param name="skServiceKey">��Ϣ����</param>
		/// <param name="uiBodyLen">��Ϣ�峤��</param>
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
		/// ��װ��Ϣͷ����Ϣͷ�������кš���Ϣ����(CateGory)����Ϣ����(ServiceKey)������
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
        /// ������Ϣͷ
        /// </summary>
        /// <param name="bHeadBuffer">��Ϣͷ</param>
        /// <param name="uiHeadBufferLen">��Ϣͷ����</param>
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
        /// ��ʼ����Ϣͷ
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
        /// �õ���Ϣ���к�
        /// </summary>
        /// <param name="seqid">���к�</param>
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
		/// �õ���Ϣ����
		/// </summary>
		/// <param name="mc">��Ϣ����</param>
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
        /// ��Ϣ����
        /// </summary>
        /// <param name="sk">����</param>
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
		/// �õ���Ϣ����
		/// </summary>
		/// <param name="dt">��������</param>
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
        /// �õ���Ϣ�峤��
        /// </summary>
        /// <param name="len">��Ϣ�峤��</param>
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
        /// ��Byteת��INT
        /// </summary>
        /// <param name="b">BYTE����</param>
        /// <param name="size">����</param>
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
