using System;
using System.Xml.Serialization;
using System.Collections;
using Common.DataInfo;
namespace Common.Logic
{
	public enum Message_Tag_ID:uint
	{
		/// <summary>
		/// ����ģ��(0x80)
		/// </summary>
		CONNECT = 0x800001,//��������
		CONNECT_RESP = 0x808001,//������Ӧ
		DISCONNECT = 0x800002,//�Ͽ�����
		DISCONNECT_RESP = 0x808002,//�Ͽ���Ӧ
		ACCOUNT_AUTHOR = 0x800003,//�û������֤����
		ACCOUNT_AUTHOR_RESP = 0x808003,//�û������֤��Ӧ
		SERVERINFO_IP_QUERY = 0x800004,
		SERVERINFO_IP_QUERY_RESP = 0x808004,
		GMTOOLS_OperateLog_Query = 0x800005,//�鿴���߲�����¼
		GMTOOLS_OperateLog_Query_RESP = 0x808005,//�鿴���߲�����¼��Ӧ
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
		/// �û�����ģ��(0x81)
		/// </summary>
		USER_CREATE = 0x810001,//����GM�ʺ�����
		USER_CREATE_RESP = 0x818001,//����GM�ʺ���Ӧ
		USER_UPDATE = 0x810002,//����GM�ʺ���Ϣ����
		USER_UPDATE_RESP = 0x818002,//����GM�ʺ���Ϣ��Ӧ
		USER_DELETE = 0x810003,//ɾ��GM�ʺ���Ϣ����
		USER_DELETE_RESP = 0x818003,//ɾ��GM�ʺ���Ϣ��Ӧ
		USER_QUERY = 0x810004,//��ѯGM�ʺ���Ϣ����
		USER_QUERY_RESP = 0x818004,//��ѯGM�ʺ���Ϣ��Ӧ
		USER_PASSWD_MODIF = 0x810005,//�����޸�����
		USER_PASSWD_MODIF_RESP = 0x818005, //�����޸���Ϣ��Ӧ
		USER_QUERY_ALL = 0x810006,//��ѯ����GM�ʺ���Ϣ
		USER_QUERY_ALL_RESP = 0x818006,//��ѯ����GM�ʺ���Ϣ��Ӧ
		DEPART_QUERY = 0x810007, //��ѯ�����б�
		DEPART_QUERY_RESP = 0x818007,//��ѯ�����б���Ӧ
        UPDATE_ACTIVEUSER = 0x810008,//���������û�״̬
        UPDATE_ACTIVEUSER_RESP = 0x818008,//���������û�״̬��Ӧ
		DEPARTMENT_CREATE = 0x810009,//���Ŵ���
		DEPARTMENT_CREATE_RESP = 0x818009,//���Ŵ�����Ӧ
		DEPARTMENT_UPDATE= 0x810010,//�����޸�
		DEPARTMENT_UPDATE_RESP = 0x818010,//�����޸���Ӧ
		DEPARTMENT_DELETE= 0x810011,//����ɾ��
		DEPARTMENT_DELETE_RESP = 0x818011,//����ɾ����Ӧ
		DEPARTMENT_ADMIN = 0x810012,//���Ź���
		DEPARTMENT_ADMIN_RESP = 0x818012,//���Ź�����Ӧ
		DEPARTMENT_RELATE_QUERY = 0x810013,//���Ź�����ѯ
		DEPARTMENT_RELATE_QUERY_RESP = 0x818013,//���Ź�����ѯ
		DEPART_RELATE_GAME_QUERY = 0x810014,
		DEPART_RELATE_GAME_QUERY_RESP = 0x818014,
		USER_SYSADMIN_QUERY = 0x810015,
		USER_SYSADMIN_QUERY_RESP = 0x818015,
		/// <summary>
		/// ģ�����(0x82)
		/// </summary>
		MODULE_CREATE = 0x820001,//����ģ����Ϣ����
		MDDULE_CREATE_RESP = 0x828001,//����ģ����Ϣ��Ӧ
		MODULE_UPDATE =0x820002,//����ģ����Ϣ����
		MODULE_UPDATE_RESP = 0x828002,//����ģ����Ϣ��Ӧ
		MODULE_DELETE = 0x820003,//ɾ��ģ������
		MODULE_DELETE_RESP = 0x828003,//ɾ��ģ����Ӧ
		MODULE_QUERY = 0x820004,//��ѯģ����Ϣ������
		MODULE_QUERY_RESP = 0x828004,//��ѯģ����Ϣ����Ӧ

		/// <summary>
		/// �û���ģ�����(0x83)
		/// </summary>
		USER_MODULE_CREATE = 0x830001,//�����û���ģ������
		USER_MODULE_CREATE_RESP = 0x838001,//�����û���ģ����Ӧ
		USER_MODULE_UPDATE = 0x830002,//�����û���ģ�������
		USER_MODULE_UPDATE_RESP = 0x838002,//�����û���ģ�����Ӧ
		USER_MODULE_DELETE = 0x830003,//ɾ���û���ģ������
		USER_MODULE_DELETE_RESP = 0x838003,//ɾ���û���ģ����Ӧ
		USER_MODULE_QUERY = 0x830004,//��ѯ�û�����Ӧģ������
		USER_MODULE_QUERY_RESP = 0x838004,//��ѯ�û�����Ӧģ����Ӧ

		/// <summary>
		/// ��Ϸ����(0x84)
		/// </summary>
		GAME_CREATE = 0x840001,//����GM�ʺ�����
		GAME_CREATE_RESP = 0x848001,//����GM�ʺ���Ӧ
		GAME_UPDATE = 0x840002,//����GM�ʺ���Ϣ����
		GAME_UPDATE_RESP = 0x848002,//����GM�ʺ���Ϣ��Ӧ
		GAME_DELETE = 0x840003,//ɾ��GM�ʺ���Ϣ����
		GAME_DELETE_RESP = 0x848003,//ɾ��GM�ʺ���Ϣ��Ӧ
		GAME_QUERY = 0x840004,//��ѯGM�ʺ���Ϣ����
		GAME_QUERY_RESP = 0x848004,//��ѯGM�ʺ���Ϣ��Ӧ
		GAME_MODULE_QUERY = 0x840005,//��ѯ��Ϸ��ģ���б�
		GAME_MODULE_QUERY_RESP = 0x848005,//��ѯ��Ϸ��ģ���б���Ӧ


		/// <summary>
		/// NOTES����(0x85)
		/// </summary>
		NOTES_LETTER_TRANSFER = 0x850001, //ȡ���ʼ��б�
		NOTES_LETTER_TRANSFER_RESP = 0x858001,//ȡ���ʼ��б����Ӧ
		NOTES_LETTER_PROCESS = 0x850002, //�ʼ�����
		NOTES_LETTER_PROCESS_RESP = 0x858002,//�ʼ�����
		NOTES_LETTER_TRANSMIT = 0x850003, //�ʼ�ת���б�
		NOTES_LETTER_TRANSMIT_RESP = 0x858003,//�ʼ�ת���б�

		/// <summary>
		/// �ͽ�GM����(0x86)
		/// </summary>
		MJ_CHARACTERINFO_QUERY = 0x860001,//������״̬
		MJ_CHARACTERINFO_QUERY_RESP = 0x868001,//������״̬��Ӧ
		MJ_CHARACTERINFO_UPDATE = 0x860002,//�޸����״̬
		MJ_CHARACTERINFO_UPDATE_RESP = 0x868002,//�޸����״̬��Ӧ
		MJ_LOGINTABLE_QUERY = 0x860003,//�������Ƿ�����
		MJ_LOGINTABLE_QUERY_RESP = 0x868003,//�������Ƿ�������Ӧ
		MJ_CHARACTERINFO_EXPLOIT_UPDATE = 0x860004,//�޸Ĺ�ѫֵ
		MJ_CHARACTERINFO_EXPLOIT_UPDATE_RESP = 0x868004,//�޸Ĺ�ѫֵ��Ӧ
		MJ_CHARACTERINFO_FRIEND_QUERY = 0x860005,//�г���������
		MJ_CHARACTERINFO_FRIEND_QUERY_RESP = 0x868005,//�г�����������Ӧ
		MJ_CHARACTERINFO_FRIEND_CREATE = 0x860006,//��Ӻ���
		MJ_CHARACTERINFO_FRIEND_CREATE_RESP = 0x868006,//��Ӻ�����Ӧ
		MJ_CHARACTERINFO_FRIEND_DELETE = 0x860007,//ɾ������
		MJ_CHARACTERINFO_FRIEND_DELETE_RESP = 0x868007,//ɾ��������Ӧ
		MJ_GUILDTABLE_UPDATE = 0x860008,//�޸ķ����������Ѵ��ڰ��
		MJ_GUILDTABLE_UPDATE_RESP = 0x868008,//�޸ķ����������Ѵ��ڰ����Ӧ
		MJ_ACCOUNT_LOCAL_CREATE = 0x860009,//���������ϵ�account����������Ϣ���浽���ط�������
		MJ_ACCOUNT_LOCAL_CREATE_RESP = 0x868009,//���������ϵ�account����������Ϣ���浽���ط���������Ӧ
		MJ_ACCOUNT_REMOTE_DELETE = 0x860010,//���÷�ͣ�ʺ�
		MJ_ACCOUNT_REMOTE_DELETE_RESP = 0x868010,//���÷�ͣ�ʺŵ���Ӧ
		MJ_ACCOUNT_REMOTE_RESTORE = 0x860011,//����ʺ�
		MJ_ACCOUNT_REMOTE_RESTORE_RESP = 0x868011,//����ʺ���Ӧ
		MJ_ACCOUNT_LIMIT_RESTORE = 0x860012,//��ʱ�޵ķ�ͣ
		MJ_ACCOUNT_LIMIT_RESTORE_RESP = 0x868012,//��ʱ�޵ķ�ͣ��Ӧ
		MJ_ACCOUNTPASSWD_LOCAL_CREATE = 0x860013,//����������뵽���� 
		MJ_ACCOUNTPASSWD_LOCAL_CREATE_RESP = 0x868013,//����������뵽����
		MJ_ACCOUNTPASSWD_REMOTE_UPDATE = 0x860014,//�޸�������� 
		MJ_ACCOUNTPASSWD_REMOTE_UPDATE_RESP = 0x868014,//�޸��������
		MJ_ACCOUNTPASSWD_REMOTE_RESTORE = 0x860015,//�ָ��������
		MJ_ACCOUNTPASSWD_REMOTE_RESTORE_RESP = 0x868015,//�ָ��������
		MJ_ITEMLOG_QUERY = 0x860016,//�����û����׼�¼
		MJ_ITEMLOG_QUERY_RESP = 0x868016,//�����û����׼�¼
		MJ_GMTOOLS_LOG_QUERY = 0x860017,//���ʹ���߲�����¼
		MJ_GMTOOLS_LOG_QUERY_RESP = 0x868017,//���ʹ���߲�����¼
		MJ_MONEYSORT_QUERY = 0x860018,//���ݽ�Ǯ����
		MJ_MONEYSORT_QUERY_RESP = 0x868018,//���ݽ�Ǯ�������Ӧ
		MJ_LEVELSORT_QUERY = 0x860019,//���ݵȼ�����
		MJ_LEVELSORT_QUERY_RESP = 0x868019,//���ݵȼ��������Ӧ
		MJ_MONEYFIGHTERSORT_QUERY = 0x860020,//���ݲ�ְͬҵ��Ǯ����
		MJ_MONEYFIGHTERSORT_QUERY_RESP = 0x868020,//���ݲ�ְͬҵ��Ǯ�������Ӧ
		MJ_LEVELFIGHTERSORT_QUERY = 0x860021,//���ݲ�ְͬҵ�ȼ�����
		MJ_LEVELFIGHTERSORT_QUERY_RESP = 0x868021,//���ݲ�ְͬҵ�ȼ��������Ӧ
		MJ_MONEYTAOISTSORT_QUERY = 0x860022,//���ݵ�ʿ��Ǯ����
		MJ_MONEYTAOISTSORT_QUERY_RESP = 0x868022,//���ݵ�ʿ��Ǯ�������Ӧ
		MJ_LEVELTAOISTSORT_QUERY = 0x860023,//���ݵ�ʿ�ȼ�����
		MJ_LEVELTAOISTSORT_QUERY_RESP = 0x868023,//���ݵ�ʿ�ȼ��������Ӧ
		MJ_MONEYRABBISORT_QUERY = 0x860024,//���ݷ�ʦ��Ǯ����
		MJ_MONEYRABBISORT_QUERY_RESP = 0x868024,//���ݷ�ʦ��Ǯ�������Ӧ
		MJ_LEVELRABBISORT_QUERY = 0x860025,//���ݷ�ʦ�ȼ�����
		MJ_LEVELRABBISORT_QUERY_RESP = 0x868025,//���ݷ�ʦ�ȼ��������Ӧ
		MJ_ACCOUNT_QUERY =  0x860026,//�ͽ��ʺŲ�ѯ
		MJ_ACCOUNT_QUERY_RESP = 0x868026,//�ͽ��ʺŲ�ѯ��Ӧ
        MJ_ACCOUNT_LOCAL_QUERY = 0x860027,//��ѯ�ͽ������ʺ�
        MJ_ACCOUNT_LOCAL_QUERY_RESP = 0x868027,//��ѯ�ͽ������ʺ���Ӧ

        /// <summary>
        /// ��������GM����(0x87)
        /// </summary>
		SDO_ACCOUNT_QUERY = 0x870026,//�鿴��ҵ��ʺ���Ϣ
		SDO_ACCOUNT_QUERY_RESP = 0x878026,//�鿴��ҵ��ʺ���Ϣ��Ӧ
		SDO_CHARACTERINFO_QUERY = 0x870027,//�鿴�������ϵ���Ϣ
		SDO_CHARACTERINFO_QUERY_RESP = 0x878027,//�鿴�������ϵ���Ϣ��Ӧ
		SDO_ACCOUNT_CLOSE = 0x870028,//��ͣ�ʻ���Ȩ����Ϣ
		SDO_ACCOUNT_CLOSE_RESP = 0x878028,//��ͣ�ʻ���Ȩ����Ϣ��Ӧ
		SDO_ACCOUNT_OPEN = 0x870029,//����ʻ���Ȩ����Ϣ
		SDO_ACCOUNT_OPEN_RESP = 0x878029,//����ʻ���Ȩ����Ϣ��Ӧ
		SDO_PASSWORD_RECOVERY = 0x870030,//����һ�����
		SDO_PASSWORD_RECOVERY_RESP = 0x878030,//����һ�������Ӧ
		SDO_CONSUME_QUERY = 0x870031,//�鿴��ҵ����Ѽ�¼
		SDO_CONSUME_QUERY_RESP = 0x878031,//�鿴��ҵ����Ѽ�¼��Ӧ
		SDO_USERONLINE_QUERY = 0x870032,//�鿴���������״̬
		SDO_USERONLINE_QUERY_RESP = 0x878032,//�鿴���������״̬��Ӧ
		SDO_USERTRADE_QUERY = 0x870033,//�鿴��ҽ���״̬
		SDO_USERTRADE_QUERY_RESP = 0x878033,//�鿴��ҽ���״̬��Ӧ
		SDO_CHARACTERINFO_UPDATE = 0x870034,//�޸���ҵ��˺���Ϣ
		SDO_CHARACTERINFO_UPDATE_RESP = 0x878034,//�޸���ҵ��˺���Ϣ��Ӧ
		SDO_ITEMSHOP_QUERY = 0x870035,//�鿴��Ϸ�������е�����Ϣ
		SDO_ITEMSHOP_QUERY_RESP = 0x878035,//�鿴��Ϸ�������е�����Ϣ��Ӧ
		SDO_ITEMSHOP_DELETE = 0x870036,//ɾ����ҵ�����Ϣ
		SDO_ITEMSHOP_DELETE_RESP  = 0x878036,//ɾ����ҵ�����Ϣ��Ӧ
		SDO_GIFTBOX_CREATE  = 0x870037,//����������е�����Ϣ
		SDO_GIFTBOX_CREATE_RESP  = 0x878037,//����������е�����Ϣ��Ӧ
		SDO_GIFTBOX_QUERY = 0x870038,//�鿴�������еĵ���
		SDO_GIFTBOX_QUERY_RESP = 0x878038,//�鿴�������еĵ�����Ӧ
		SDO_GIFTBOX_DELETE = 0x870039,//ɾ���������еĵ���
		SDO_GIFTBOX_DELETE_RESP = 0x878039,//ɾ���������еĵ�����Ӧ
		SDO_USERLOGIN_STATUS_QUERY = 0x870040,//�鿴��ҵ�¼״̬
		SDO_USERLOGIN_STATUS_QUERY_RESP = 0x878040,//�鿴��ҵ�¼״̬��Ӧ
		SDO_ITEMSHOP_BYOWNER_QUERY = 0x870041,////�鿴������ϵ�����Ϣ
		SDO_ITEMSHOP_BYOWNER_QUERY_RESP = 0x878041,////�鿴������ϵ�����Ϣ
		SDO_ITEMSHOP_TRADE_QUERY = 0x870042,//�鿴��ҽ��׼�¼��Ϣ
		SDO_ITEMSHOP_TRADE_QUERY_RESP = 0x878042,//�鿴��ҽ��׼�¼��Ϣ����Ӧ
		SDO_MEMBERSTOPSTATUS_QUERY = 0x870043,//�鿴���ʺ�״̬
		SDO_MEMBERSTOPSTATUS_QUERY_RESP = 0x878043,///�鿴���ʺ�״̬����Ӧ
        SDO_MEMBERBANISHMENT_QUERY = 0x870044,//�鿴����ͣ����ʺ�
        SDO_MEMBERBANISHMENT_QUERY_RESP = 0x878044,//�鿴����ͣ����ʺ���Ӧ
        SDO_USERMCASH_QUERY = 0x870045,//��ҳ�ֵ��¼��ѯ
        SDO_USERMCASH_QUERY_RESP = 0x878045,//��ҳ�ֵ��¼��ѯ��Ӧ
        SDO_USERGCASH_UPDATE = 0x870046,//�������G��
        SDO_USERGCASH_UPDATE_RESP = 0x878046,//�������G�ҵ���Ӧ
        SDO_MEMBERLOCAL_BANISHMENT= 0x870047,//���ر���ͣ����Ϣ
        SDO_MEMBERLOCAL_BANISHMENT_RESP = 0x878047,//���ر���ͣ����Ϣ��Ӧ
        SDO_EMAIL_QUERY = 0x870048,//�õ���ҵ�EMAIL
        SDO_EMAIL_QUERY_RESP = 0x878048,//�õ���ҵ�EMAIL��Ӧ
        SDO_USERCHARAGESUM_QUERY = 0x870049,//�õ���ֵ��¼�ܺ�
        SDO_USERCHARAGESUM_QUERY_RESP = 0x878049,//�õ���ֵ��¼�ܺ���Ӧ
        SDO_USERCONSUMESUM_QUERY = 0x870050,//�õ����Ѽ�¼�ܺ�
        SDO_USERCONSUMESUM_QUERY_RESP = 0x878050,//�õ����Ѽ�¼�ܺ���Ӧ
        SDO_USERGCASH_QUERY = 0x870051,//��ңǱҼ�¼��ѯ
        SDO_USERGCASH_QUERY_RESP = 0x878051,//��ңǱҼ�¼��ѯ��Ӧ
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
		/// ������GM����(0x88)
		/// </summary>
		AU_ACCOUNT_QUERY = 0x880001,//����ʺ���Ϣ��ѯ
		AU_ACCOUNT_QUERY_RESP = 0x888001,//����ʺ���Ϣ��ѯ��Ӧ
		AU_ACCOUNTREMOTE_QUERY = 0x880002,//��Ϸ��������ͣ������ʺŲ�ѯ
		AU_ACCOUNTREMOTE_QUERY_RESP = 0x888002,//��Ϸ��������ͣ������ʺŲ�ѯ��Ӧ
		AU_ACCOUNTLOCAL_QUERY = 0x880003,//���ط�ͣ������ʺŲ�ѯ
		AU_ACCOUNTLOCAL_QUERY_RESP = 0x888003,//���ط�ͣ������ʺŲ�ѯ��Ӧ
		AU_ACCOUNT_CLOSE = 0x880004,//��ͣ������ʺ�
		AU_ACCOUNT_CLOSE_RESP = 0x888004,//��ͣ������ʺ���Ӧ
		AU_ACCOUNT_OPEN = 0x880005,//��������ʺ�
		AU_ACCOUNT_OPEN_RESP= 0x888005,//��������ʺ���Ӧ
		AU_ACCOUNT_BANISHMENT_QUERY = 0x880006,//��ҷ�ͣ�ʺŲ�ѯ
		AU_ACCOUNT_BANISHMENT_QUERY_RESP = 0x888006,//��ҷ�ͣ�ʺŲ�ѯ��Ӧ
		AU_CHARACTERINFO_QUERY = 0x880007,//��ѯ��ҵ��˺���Ϣ
		AU_CHARACTERINFO_QUERY_RESP = 0x888007,//��ѯ��ҵ��˺���Ϣ��Ӧ
		AU_CHARACTERINFO_UPDATE = 0x880008,//�޸���ҵ��˺���Ϣ
		AU_CHARACTERINFO_UPDATE_RESP = 0x888008,//�޸���ҵ��˺���Ϣ��Ӧ
		AU_ITEMSHOP_QUERY = 0x880009,//�鿴��Ϸ�������е�����Ϣ
		AU_ITEMSHOP_QUERY_RESP = 0x888009,//�鿴��Ϸ�������е�����Ϣ��Ӧ
		AU_ITEMSHOP_DELETE = 0x880010,//ɾ����ҵ�����Ϣ
		AU_ITEMSHOP_DELETE_RESP = 0x888010,//ɾ����ҵ�����Ϣ��Ӧ
		AU_ITEMSHOP_BYOWNER_QUERY = 0x880011,////�鿴������ϵ�����Ϣ
		AU_ITEMSHOP_BYOWNER_QUERY_RESP = 0x888011,////�鿴������ϵ�����Ϣ
		AU_ITEMSHOP_TRADE_QUERY = 0x880012,//�鿴��ҽ��׼�¼��Ϣ
		AU_ITEMSHOP_TRADE_QUERY_RESP = 0x888012,//�鿴��ҽ��׼�¼��Ϣ����Ӧ
		AU_ITEMSHOP_CREATE = 0x880013,//����������е�����Ϣ
		AU_ITEMSHOP_CREATE_RESP = 0x888013,//����������е�����Ϣ��Ӧ
		AU_LEVELEXP_QUERY = 0x880014,//�鿴��ҵȼ�����
		AU_LEVELEXP_QUERY_RESP = 0x888014,////�鿴��ҵȼ�������Ӧ
		AU_USERLOGIN_STATUS_QUERY = 0x880015,//�鿴��ҵ�¼״̬
		AU_USERLOGIN_STATUS_QUERY_RESP = 0x888015,//�鿴��ҵ�¼״̬��Ӧ
		AU_USERCHARAGESUM_QUERY = 0x880016,//�õ���ֵ��¼�ܺ�
		AU_USERCHARAGESUM_QUERY_RESP = 0x888016,//�õ���ֵ��¼�ܺ���Ӧ
		AU_CONSUME_QUERY = 0x880017,//�鿴��ҵ����Ѽ�¼
		AU_CONSUME_QUERY_RESP = 0x888017,//�鿴��ҵ����Ѽ�¼��Ӧ
		AU_USERCONSUMESUM_QUERY = 0x880018,//�õ����Ѽ�¼�ܺ�
		AU_USERCONSUMESUM_QUERY_RESP = 0x888018,//�õ����Ѽ�¼�ܺ���Ӧ
		AU_USERMCASH_QUERY = 0x880019,//��ҳ�ֵ��¼��ѯ
		AU_USERMCASH_QUERY_RESP = 0x888019,//��ҳ�ֵ��¼��ѯ��Ӧ
		AU_USERGCASH_QUERY = 0x880020,//��ңǱҼ�¼��ѯ
		AU_USERGCASH_QUERY_RESP = 0x888020,//��ңǱҼ�¼��ѯ��Ӧ
		AU_USERGCASH_UPDATE = 0x880021,//�������G��
		AU_USERGCASH_UPDATE_RESP = 0x888021,//�������G�ҵ���Ӧ
		AU_USERNICK_UPDATE = 0x880022,
		AU_USERNICK_UPDATE_RESP = 0x888022,

		/// <summary>
		/// ��񿨶���GM����(0x89)
		/// </summary>
		CR_ACCOUNT_QUERY = 0x890001,//����ʺ���Ϣ��ѯ
		CR_ACCOUNT_QUERY_RESP = 0x898001,//����ʺ���Ϣ��ѯ��Ӧ
		CR_ACCOUNTACTIVE_QUERY = 0x890002,//����ʺż�����Ϣ
		CR_ACCOUNTACTIVE_QUERY_RESP = 0x898002,//����ʺż�����Ӧ
		CR_CALLBOARD_QUERY = 0x890003,//������Ϣ��ѯ
		CR_CALLBOARD_QUERY_RESP = 0x898003,//������Ϣ��ѯ��Ӧ
		CR_CALLBOARD_CREATE = 0x890004,//��������
		CR_CALLBOARD_CREATE_RESP = 0x898004,//����������Ӧ
		CR_CALLBOARD_UPDATE = 0x890005,//���¹�����Ϣ
		CR_CALLBOARD_UPDATE_RESP = 0x898005,//���¹�����Ϣ����Ӧ
		CR_CALLBOARD_DELETE = 0x890006,//ɾ��������Ϣ
		CR_CALLBOARD_DELETE_RESP = 0x898006,//ɾ��������Ϣ����Ӧ
		CR_CHARACTERINFO_QUERY = 0x890007,//��ҽ�ɫ��Ϣ��ѯ
		CR_CHARACTERINFO_QUERY_RESP = 0x898007,//��ҽ�ɫ��Ϣ��ѯ����Ӧ
		CR_CHARACTERINFO_UPDATE = 0x890008,//��ҽ�ɫ��Ϣ��ѯ
		CR_CHARACTERINFO_UPDATE_RESP = 0x898008,//��ҽ�ɫ��Ϣ��ѯ����Ӧ
		CR_CHANNEL_QUERY = 0x890009,//����Ƶ����ѯ
		CR_CHANNEL_QUERY_RESP = 0x898009,//����Ƶ����ѯ����Ӧ
		CR_NICKNAME_QUERY = 0x890010,//����ǳƲ�ѯ
		CR_NICKNAME_QUERY_RESP = 0x898010,//����ǳƵ���Ӧ
		CR_LOGIN_LOGOUT_QUERY = 0x890011,//������ߡ�����ʱ���ѯ
		CR_LOGIN_LOGOUT_QUERY_RESP = 0x898011,//������ߡ�����ʱ���ѯ����Ӧ
		CR_ERRORCHANNEL_QUERY = 0x890012,//������󹫸�Ƶ����ѯ
		CR_ERRORCHANNEL_QUERY_RESP = 0x898012,//������󹫸�Ƶ����ѯ����Ӧ

		/// <summary>
		/// ��ֵ����GM����(0x90)
		/// </summary>
		CARD_USERCHARGEDETAIL_QUERY = 0x900001,//һ��ͨ��ѯ
		CARD_USERCHARGEDETAIL_QUERY_RESP = 0x908001,//һ��ͨ��ѯ��Ӧ
		CARD_USERDETAIL_QUERY = 0x900002,//��֮���û�ע����Ϣ��ѯ
		CARD_USERDETAIL_QUERY_RESP = 0x908002,////��֮���û�ע����Ϣ��ѯ��Ӧ
		CARD_USERCONSUME_QUERY =0x900003,//���б����Ѳ�ѯ
		CARD_USERCONSUME_QUERY_RESP = 0x908003,//���б����Ѳ�ѯ��Ӧ
		CARD_VNETCHARGE_QUERY = 0x900004,//�����ǿճ�ֵ��ѯ
		CARD_VNETCHARGE_QUERY_RESP = 0x908004,//�����ǿճ�ֵ��ѯ��Ӧ
		CARD_USERDETAIL_SUM_QUERY = 0x900005,//��ֵ�ϼƲ�ѯ
		CARD_USERDETAIL_SUM_QUERY_RESP = 0x908005,//��ֵ�ϼƲ�ѯ��Ӧ
		CARD_USERCONSUME_SUM_QUERY = 0x900006,//���ѺϼƲ�ѯ
		CARD_USERCONSUME_SUM_QUERY_RESP = 0x908006,//���Ѻϼ���Ӧ
		CARD_USERINFO_QUERY = 0x900007,//���ע����Ϣ��ѯ
		CARD_USERINFO_QUERY_RESP = 0x908007,//���ע����Ϣ��ѯ��Ӧ
		CARD_USERINFO_CLEAR = 0x900008,//����������֤��Ϣ
		CARD_USERINFO_CLEAR_RESP = 0x908008,//����������֤��Ϣ��Ӧ
		CARD_USERSECURE_CLEAR = 0x900009,//������Ұ�ȫ����Ϣ
		CARD_USERSECURE_CLEAR_RESP = 0x908009,//������Ұ�ȫ����Ϣ��Ӧ
		CARD_USERNICK_QUERY = 0x900010,
		CARD_USERNICK_QUERY_RESP = 0x908010,
		CARD_USERLOCK_UPDATE = 0x900011,
		CARD_USERLOCK_UPDATE_RESP = 0x908011,


        /// <summary>
        /// �������̳�(0x91)
        /// </summary>
        AUSHOP_USERGPURCHASE_QUERY = 0x910001,//�û�G�ҹ����¼
        AUSHOP_USERGPURCHASE_QUERY_RESP = 0x918001,//�û�G�ҹ����¼
        AUSHOP_USERMPURCHASE_QUERY = 0x910002,//�û�M�ҹ����¼
        AUSHOP_USERMPURCHASE_QUERY_RESP = 0x918002,//�û�M�ҹ����¼
        AUSHOP_AVATARECOVER_QUERY = 0x910003,//���߻��նһ���
        AUSHOP_AVATARECOVER_QUERY_RESP = 0x918003,//���߻��նһ���
        AUSHOP_USERINTERGRAL_QUERY = 0x910004,//�û����ּ�¼
        AUSHOP_USERINTERGRAL_QUERY_RESP = 0x918004,//�û����ּ�¼
        AUSHOP_USERGPURCHASE_SUM_QUERY = 0x910005,//�û�G�ҹ����¼�ϼ�
        AUSHOP_USERGPURCHASE_SUM_QUERY_RESP = 0x918005,//�û�G�ҹ����¼�ϼ���Ӧ
        AUSHOP_USERMPURCHASE_SUM_QUERY = 0x910006,//�û�M�ҹ����¼�ϼ�
        AUSHOP_USERMPURCHASE_SUM_QUERY_RESP = 0x918006,//�û�M�ҹ����¼�ϼ���Ӧ
		AUSHOP_AVATARECOVER_DETAIL_QUERY = 0x910007,//���߻��նһ���ϸ��¼
		AUSHOP_AVATARECOVER_DETAIL_QUERY_RESP = 0x918007,//���߻��նһ���ϸ��¼

		/// <summary>
		/// �����Ź���(0x92)
		/// </summary>
		O2JAM_CHARACTERINFO_QUERY= 0x920001,//��ҽ�ɫ��Ϣ��ѯ
	    O2JAM_CHARACTERINFO_QUERY_RESP= 0x928001,//��ҽ�ɫ��Ϣ��ѯ
		O2JAM_CHARACTERINFO_UPDATE= 0x920002,//��ҽ�ɫ��Ϣ����
		O2JAM_CHARACTERINFO_UPDATE_RESP= 0x928002,//��ҽ�ɫ��Ϣ����
		O2JAM_ITEM_QUERY= 0x920003,//��ҵ�����Ϣ��ѯ
		O2JAM_ITEM_QUERY_RESP= 0x928003,//��ҽ�ɫ��Ϣ��ѯ
		O2JAM_ITEM_UPDATE= 0x920004,//��ҵ�����Ϣ����
		O2JAM_ITEM_UPDATE_RESP= 0x928004,//��ҵ�����Ϣ����
		O2JAM_CONSUME_QUERY= 0x920005,//���������Ϣ��ѯ
		O2JAM_CONSUME_QUERY_RESP= 0x928005,//���������Ϣ��ѯ
		O2JAM_ITEMDATA_QUERY= 0x920006,//�����б��ѯ
		O2JAM_ITEMDATA_QUERY_RESP= 0x928006,//�����б���Ϣ��ѯ
		O2JAM_GIFTBOX_QUERY = 0x920007,//�������в�ѯ
		O2JAM_GIFTBOX_QUERY_RESP = 0x928007,//�������в�ѯ
		O2JAM_USERGCASH_UPDATE = 0x920008,//�������G��
		O2JAM_USERGCASH_UPDATE_RESP = 0x928008,//�������G�ҵ���Ӧ
		O2JAM_CONSUME_SUM_QUERY= 0x920009,//���������Ϣ��ѯ
		O2JAM_CONSUME_SUM_QUERY_RESP= 0x928009,//���������Ϣ��ѯ
		O2JAM_GIFTBOX_CREATE= 0x920010,//����������е���
		O2JAM_GIFTBOX_CREATE_RESP= 0x928010,//����������е���
		O2JAM_ITEMNAME_QUERY = 0x920011,
		O2JAM_ITEMNAME_QUERY_RESP = 0x928011,
		O2JAM_GIFTBOX_DELETE = 0x920012,
		O2JAM_GIFTBOX_DELETE_RESP  =0x928012,

		/// <summary>
		/// ������IIGM����(0x93)
		/// </summary>
		O2JAM2_ACCOUNT_QUERY = 0x930001,//����ʺ���Ϣ��ѯ
		O2JAM2_ACCOUNT_QUERY_RESP = 0x938001,//����ʺ���Ϣ��ѯ��Ӧ
		O2JAM2_ACCOUNTACTIVE_QUERY = 0x930002,//����ʺż�����Ϣ
		O2JAM2_ACCOUNTACTIVE_QUERY_RESP = 0x938002,//����ʺż�����Ӧ
		O2JAM2_CHARACTERINFO_QUERY = 0x930003,//�û���Ϣ��ѯ
		O2JAM2_CHARACTERINFO_QUERY_RESP = 0x938003,
		O2JAM2_CHARACTERINFO_UPDATE = 0x930004,//�û���Ϣ�޸�
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
		O2JAM2_ACCOUNT_CLOSE = 0x930015,//��ͣ�ʻ���Ȩ����Ϣ
		O2JAM2_ACCOUNT_CLOSE_RESP = 0x938015,//��ͣ�ʻ���Ȩ����Ϣ��Ӧ
		O2JAM2_ACCOUNT_OPEN = 0x930016,//����ʻ���Ȩ����Ϣ
		O2JAM2_ACCOUNT_OPEN_RESP = 0x938016,//����ʻ���Ȩ����Ϣ��Ӧ
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
		/// �������� Add by KeHuaQing 2006-09-14
		/// </summary>
		SOCCER_CHARACTERINFO_QUERY = 0x940001,//�û���Ϣ��ѯ
		SOCCER_CHARACTERINFO_QUERY_RESP = 0x948001,
		SOCCER_CHARCHECK_QUERY = 0x940002,//�û�NameCheck,SocketCheck
		SOCCER_CHARCHECK_QUERY_RESP = 0x948002,
		SOCCER_CHARITEMS_RECOVERY_QUERY = 0x940003,//�ָ���ɫ��Ϣ
		SOCCER_CHARITEMS_RECOVERY_QUERY_RESP = 0x948003,
		SOCCER_CHARPOINT_QUERY = 0x940004,//�û�G���޸�
		SOCCER_CHARPOINT_QUERY_RESP = 0x948004,
		SOCCER_DELETEDCHARACTERINFO_QUERY = 0x940005,//ɾ���û���ѯ
		SOCCER_DELETEDCHARACTERINFO_QUERY_RESP = 0x948005,
		SOCCER_CHARACTERSTATE_MODIFY = 0x940006,//ͣ���ɫ
		SOCCER_CHARACTERSTATE_MODIFY_RESP = 0x948006,
		SOCCER_ACCOUNTSTATE_MODIFY = 0x940007,//ͣ�����
		SOCCER_ACCOUNTSTATE_MODIFY_RESP = 0x948007,
		SOCCER_CHARACTERSTATE_QUERY = 0x940008,//ͣ���ɫ��ѯ
		SOCCER_CHARACTERSTATE_QUERY_RESP = 0x948008,
		SOCCER_ACCOUNTSTATE_QUERY = 0x940009,//ͣ����Ҳ�ѯ
		SOCCER_ACCOUNTSTATE_QUERY_RESP = 0x948009,

		NOTDEFINED = 0x0,
		ERROR = 0xFFFFFF
	}
	/// <summary>
	/// Message ��ժҪ˵����
	/// </summary>
	public class Message
	{
		/// <summary>
		/// ��ϢByte����
		/// </summary>
		public byte[] m_bMessageBuffer;
		/// <summary>
		/// ��Ϣbyte����
		/// </summary>
		public uint m_uiMessageLen;
		/// <summary>
		/// ��Ϣ��
		/// </summary>
		public Packet m_packet;
		/// <summary>
		/// �Ƿ��ǺϷ���Ϣ
		/// </summary>
		public bool IsValidMessage = false;
		
		public Message()
		{
		}
		/// <summary>
		/// ������Ϣ������Ϣ�塢��Ϣͷ�������ɶ�����Ϣ
		/// </summary>
		/// <param name="buffer">������</param>
		/// <param name="len">����������</param>
		public Message(byte[] buffer , uint len)
		{
			if (buffer == null || buffer.Length != len)
				return;
			this.m_bMessageBuffer = buffer;
			this.m_uiMessageLen = len;
			this.IsValidMessage = this.DecodeMessage();
		}
		/// <summary>
		/// ��Ϣ����
		/// ����Ϣ��ʼFE����Ϣ��βEFȥ��
		/// ���ҽ���Ϣ���м����FE��0xFD��0x01,EF��0xFD��0xF2ȥ��
		/// </summary>
		/// <returns>���</returns>
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
        /// ������Ϣ��������Ϣ����װ
        /// </summary>
        /// <param name="packet">��Ϣ��</param>
		public Message(Packet packet)
		{
			if (packet == null || !packet.IsValidPacket)
				return;
			this.m_packet = packet;
			this.IsValidMessage = this.EncodeMessage();
		}
        /// <summary>
        /// ��װ��Ϣ������Ϣ��ͷ����FE��Ϣβ����EF��
        /// �����Ϣ���м����FE��0xFD��0x01,EF��0xFD��0xF2
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
		/// �õ���Ϣ����ID
		/// </summary>
		/// <returns>��Ϣ����ID</returns>
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
        /// �õ���Ϣ�Ľ����
        /// </summary>
        /// <param name="buffer">������Ϣ</param>
        /// <param name="len">����</param>
        /// <param name="start">��ʼλ��</param>
        /// <param name="end">��β</param>
        /// <returns>��Ϣ�����</returns>
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
		/// ��message��ṹ��д��XML�ļ�����
		/// </summary>
		/// <param name="filename">XML�ļ�</param>
		/// <param name="message">Message��Ϣ��</param>
		/// <param name="append">�Ƿ������Ϣ��</param>
		/// <returns>�������</returns>
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
		/// ��XML�ļ���������ת����Message��ṹ
		/// </summary>
		/// <param name="filename">�ļ���</param>
		/// <returns>Message��Ϣ��</returns>
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
		/// CLIENT�˷������ӵ���Ӧ
		/// </summary>
		/// <param name="status">������Ӧ����Ϣ</param>
		/// <returns>��Ϣ��</returns>
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
		/// CLIENT�˷��������Ͽ���Ӧ
		/// </summary>
		/// <param name="userByID">�û���¼ID</param>
		/// <param name="msg">�Ͽ�����Ӧ��Ϣ</param>
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
		/// ��Ӧ�û���֤����Ϣ
		/// </summary>
		/// <param name="userName">�û���</param>
		/// <param name="password">����</param>
		/// <param name="mac">MAC��</param>
		/// <returns>����������Ϣ����Ϣ��</returns>
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
		/// ��Ӧ�������û���Ϣ
		/// </summary>
		/// <returns>��Ϣ��</returns>
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
		/// ��Ӧ�û��޸��������Ϣ��
		/// </summary>
		/// <param name=" status">�޸�״̬</param>
		/// <returns>��Ϣ��</returns>
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
		/// ��Ӧɾ��GM�ʺŵ���Ϣ��
		/// </summary>
		/// <param name="userID">�û�ID</param>
		/// <param name="msg">������Ϣ</param>
		/// <returns>ɾ��GM�ʺŵ���Ϣ��</returns>
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
		///  ��Ӧ����ģ�����Ϣ
		/// </summary>

		/// <param name="status">״ֵ̬</param>
		/// <returns>����ģ�����Ϣ��װ</returns>
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
		///  ��Ӧ�޸�ģ�����Ϣ
		/// </summary>
		/// <param name="status">״ֵ̬</param>
		/// <param name="moduleContent">ģ������</param>
		/// <returns>�޸�ģ�����Ϣ��װ</returns>
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
		///  ��Ӧɾ��ģ�����Ϣ
		/// </summary>
		/// <param name="moduleID">ģ��ID</param>
		/// <param name="msg">�������</param>
		/// <returns>ɾ��ģ�����Ϣ��װ</returns>
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
		/// ����ʧ����Ϣ����Ӧ
		/// </summary>
		/// <param name="UserID">�û���</param>
		/// <param name="errorMsg">������Ϣ</param>
		/// <returns>������Ϣ</returns>
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
		/// ��װ���ݼ�����Ϣ��
		/// </summary>
		/// <param name="queryList">���������б�</param>
		/// <param name="colCnt">��������</param>
		/// <returns>�������ݼ�����Ϣ��</returns>
		public static Message COMMON_MES_RESP(Query_Structure[] queryList,Msg_Category category,ServiceKey service,int colCnt)
		{
			uint iPos = 0;
			TLV_Structure[] tlv = new TLV_Structure[queryList.Length*colCnt];
			int pos = 0;

			for(int i=0;i<queryList.Length;i++)
			{
				for(int j=0;j<colCnt;j++)
				{
					//��ϢԪ�ظ�ʽ
					TagFormat format_ = queryList[i].m_tagList[j].format;
					//��ϢԪ������
					TagName key_ = queryList[i].m_tagList[j].tag;
					//��ϢԪ�ص�ֵ
                    byte[] bgMsg_Value = queryList[i].m_tagList[j].tag_buf;
					//��Ϣ�Ľṹ��
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
			//��װ��Ϣ��
			Packet_Body body = new Packet_Body(tlv,(uint)tlv.Length,(uint)colCnt);
			//��װ��Ϣͷ
			Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(),category,
				service,body.m_uiBodyLen);
			return new Message(new Packet(head,body));
		}
		/// <summary>
		/// ��װ���ݼ�����Ϣ��
		/// </summary>
		/// <param name="queryList">���������б�</param>
		/// <param name="colCnt">��������</param>
		/// <returns>�������ݼ�����Ϣ��</returns>
		public static ArrayList QUERY_MES_RESP(Query_Structure[] queryList,Msg_Category category,ServiceKey service,int colCnt)
		{
			ArrayList list = new ArrayList();
			for(int i=0;i<queryList.Length;i++)
			{
				int pos =0;
				TLV_Structure[] tlv = new TLV_Structure[colCnt];
				for(int j=0;j<colCnt;j++)
				{
					//��ϢԪ�ظ�ʽ
					TagFormat format_ = queryList[i].m_tagList[j].format;
					//��ϢԪ������
					TagName key_ = queryList[i].m_tagList[j].tag;
					//��ϢԪ�ص�ֵ
					byte[] bgMsg_Value = queryList[i].m_tagList[j].tag_buf;
					//��Ϣ�Ľṹ��
					TLV_Structure Msg_Value = new TLV_Structure(key_,format_,(uint)bgMsg_Value.Length,bgMsg_Value);
					tlv[pos++]=Msg_Value;
				}
				//��װ��Ϣ��
				Packet_Body body = new Packet_Body(tlv,(uint)tlv.Length,(uint)colCnt);
				//��װ��Ϣͷ
				Packet_Head head = new Packet_Head(SeqID_Generator.Instance().GetNewSeqID(),category,
					service,body.m_uiBodyLen);
				list.Add(new Message(new Packet(head,body)));

			}
			return list;
		}
		/// <summary>
		/// ���������б���Ϣ����
		/// </summary>
		/// <param name="status">״ֵ̬</param>
		/// <returns>��Ϣ�ṹ</returns>
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
		/// ���������б���Ϣ����
		/// </summary>
		/// <param name="status">״ֵ̬</param>
		/// <returns>��Ϣ�ṹ</returns>
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
		/// ���������б���Ϣ����
		/// </summary>
		/// <param name="list">�����б�</param>
		/// <returns>��Ϣ�ṹ</returns>
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
	    /// ��ѯ�û������б���Ϣ����
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
