using System;
using System.Configuration;
using System.Collections;

namespace Common.API
{
	/// <summary>
	/// LanguageConfig 的摘要说明。
	/// </summary>
	public class LanguageAPI
	{
		public LanguageAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		/// <summary>
		/// 根据key值得到value
		/// </summary>
		/// <param name="game">游戏名称</param>
		/// <param name="key">关键字</param>
		/// <returns>value</returns>
		public static String GetValue(String game,String key)
		{
			System.Collections.IDictionary LanguageConfig = (System.Collections.IDictionary)
				System.Configuration.ConfigurationSettings.GetConfig(game);
			if (LanguageConfig == null)
				return game + " IS Not Exist In This GMTools";
			return LanguageConfig[key].ToString();
		}
		
		#region MainFrame
		public static String ServerSocket_Handler_User = GetValue("MainFrame","ServerSocket_Handler_User");
		public static String ServerSocket_Handler_UserLeft = GetValue("MainFrame","ServerSocket_Handler_UserLeft");
		public static String ServerSocket_ServerSocket_GMTools_Title = GetValue("MainFrame","ServerSocket_ServerSocket_GMTools_Title");
		public static String ServerSocket_ServerSocket_GMTools_Port = GetValue("MainFrame","ServerSocket_ServerSocket_GMTools_Port");
		public static String ServerSocket_ServerSocket_GMTools_Accept = GetValue("MainFrame","ServerSocket_ServerSocket_GMTools_Accept");
		public static String ServerSocket_ServerSocket_GMTools_Client = GetValue("MainFrame","ServerSocket_ServerSocket_GMTools_Client");
		public static String ServerSocket_ServerSocket_GMTools_Validate = GetValue("MainFrame","ServerSocket_ServerSocket_GMTools_Validate");
		public static String ServerSocket_Task_Continue = GetValue("MainFrame","ServerSocket_Task_Continue");
		public static String ServerSocket_Task_Query = GetValue("MainFrame","ServerSocket_Task_Query");
		public static String ServerSocket_UpdatePatch_Error = GetValue("MainFrame","ServerSocket_UpdatePatch_Error");
		#endregion 

		#region Common
		public static String Logic_Exception_Parameter = GetValue("Common","Logic_Exception_Parameter");
		public static String Logic_Exception_ExpectedType = GetValue("Common","Logic_Exception_ExpectedType");
		public static String Logic_Exception_RealType = GetValue("Common","Logic_Exception_RealType");
		public static String Logic_Exception_ExpectedValue = GetValue("Common","Logic_Exception_ExpectedValue");
		public static String Logic_Exception_RealValue = GetValue("Common","Logic_Exception_RealValue");
		public static String Logic_Exception_Error = GetValue("Common","Logic_Exception_Error");
		public static String Logic_TLV_Structure_NumLength = GetValue("Common","Logic_TLV_Structure_NumLength");
		public static String Logic_TLV_Structure_TagFormatType = GetValue("Common","Logic_TLV_Structure_TagFormatType");
		public static String Logic_UserValidate_User = GetValue("Common","Logic_UserValidate_User");
		public static String Logic_UserValidate_AcceptData = GetValue("Common","Logic_UserValidate_AcceptData");
		public static String Logic_UserValidate_ValidateFailue = GetValue("Common","Logic_UserValidate_ValidateFailue");
		public static String Logic_UserValidate_LoggingFailue = GetValue("Common","Logic_UserValidate_LoggingFailue");
		public static String API_Add = GetValue("Common","API_Add");
		public static String API_Delete = GetValue("Common","API_Delete");
		public static String API_Update = GetValue("Common","API_Update");
		public static String API_Success = GetValue("Common","API_Success");
		public static String API_Failure = GetValue("Common","API_Failure");
		public static String API_Display = GetValue("Common","API_Display");
		public static String API_Description = GetValue("Common","API_Description");
		public static String API_CommonAPI_NewServer = GetValue("Common","API_CommonAPI_NewServer");
		public static String API_CommonAPI_GameID = GetValue("Common","API_CommonAPI_GameID");
		public static String API_CommonAPI_ServerIP = GetValue("Common","API_CommonAPI_ServerIP");
		public static String API_CommonAPI_GameCity = GetValue("Common","API_CommonAPI_GameCity");
		public static String API_CommonAPI_GameListEmpty = GetValue("Common","API_CommonAPI_GameListEmpty");
		public static String API_CommonAPI_NoLog = GetValue("Common","API_CommonAPI_NoLog");
		public static String API_DepartmentAPI_DepInfo = GetValue("Common","API_DepartmentAPI_DepInfo");
		public static String API_DepartmentAPI_NoDepInfo = GetValue("Common","API_DepartmentAPI_NoDepInfo");
		public static String API_DepartmentAPI_OperatorID = GetValue("Common","API_DepartmentAPI_OperatorID");
		public static String API_DepartmentAPI_DepID = GetValue("Common","API_DepartmentAPI_DepID");
		public static String API_DepartmentAPI_DepTitle = GetValue("Common","API_DepartmentAPI_DepTitle");
		public static String API_DepartmentAPI_DepDesp = GetValue("Common","API_DepartmentAPI_DepDesp");
		public static String API_DepartmentAPI_GameList = GetValue("Common","API_DepartmentAPI_GameList");
		public static String API_DepartmentAPI_HoldGame = GetValue("Common","API_DepartmentAPI_HoldGame");
		public static String API_GameInfoAPI_GameList = GetValue("Common","API_GameInfoAPI_GameList");
		public static String API_GameInfoAPI_GameID = GetValue("Common","API_GameInfoAPI_GameID");
		public static String API_GameInfoAPI_GameTitle = GetValue("Common","API_GameInfoAPI_GameTitle");
		public static String API_GameInfoAPI_GameDesp = GetValue("Common","API_GameInfoAPI_GameDesp");
		public static String API_GameInfoAPI_GameInfo = GetValue("Common","API_GameInfoAPI_GameInfo");
		public static String API_GameInfoAPI_ModuleID = GetValue("Common","API_GameInfoAPI_ModuleID");
		public static String API_GameInfoAPI_ModuleTitle = GetValue("Common","API_GameInfoAPI_ModuleTitle");
		public static String API_GameInfoAPI_ModuleDesp = GetValue("Common","API_GameInfoAPI_ModuleDesp");
		public static String API_GameInfoAPI_ModuleClass = GetValue("Common","API_GameInfoAPI_ModuleClass");
		public static String API_GameInfoAPI_GameModuleList = GetValue("Common","API_GameInfoAPI_GameModuleList");
		public static String API_ModuleInfoAPI_ModuleInfo = GetValue("Common","API_ModuleInfoAPI_ModuleInfo");
		public static String API_ModuleInfoAPI_NoModuleInfo = GetValue("Common","API_ModuleInfoAPI_NoModuleInfo");
		public static String API_ModuleInfoAPI_ModuleList = GetValue("Common","API_ModuleInfoAPI_ModuleList");
		public static String API_ModuleInfoAPI_NoModuleList = GetValue("Common","API_ModuleInfoAPI_NoModuleList");
		public static String API_NotesInfoAPI_NotesEmailList = GetValue("Common","API_NotesInfoAPI_NotesEmailList");
		public static String API_NotesInfoAPI_NotesTransEmailList = GetValue("Common","API_NotesInfoAPI_NotesTransEmailList");
		public static String API_NotesInfoAPI_EmailID = GetValue("Common","API_NotesInfoAPI_EmailID");
		public static String API_NotesInfoAPI_EmailSubject = GetValue("Common","API_NotesInfoAPI_EmailSubject");
		public static String API_NotesInfoAPI_EmailContent = GetValue("Common","API_NotesInfoAPI_EmailContent");
		public static String API_NotesInfoAPI_EmailSender = GetValue("Common","API_NotesInfoAPI_EmailSender");
		public static String API_NotesInfoAPI_NoDealWithEmail = GetValue("Common","API_NotesInfoAPI_NoDealWithEmail");
		public static String API_NotesInfoAPI_NoTransDealWithEmail = GetValue("Common","API_NotesInfoAPI_NoTransDealWithEmail");
		public static String API_NotesInfoAPI_DealWithEmailFailure = GetValue("Common","API_NotesInfoAPI_DealWithEmailFailure");
		public static String API_UserInfoAPI_NoUserList = GetValue("Common","API_UserInfoAPI_NoUserList");
		public static String API_UserInfoAPI_AccountInfo = GetValue("Common","API_UserInfoAPI_AccountInfo");
		public static String API_UserInfoAPI_Password = GetValue("Common","API_UserInfoAPI_Password");
		public static String API_UserInfoAPI_NewPassword = GetValue("Common","API_UserInfoAPI_NewPassword");
		public static String API_UserInfoAPI_MAC = GetValue("Common","API_UserInfoAPI_MAC");
		public static String API_UserInfoAPI_NoAdmin = GetValue("Common","API_UserInfoAPI_NoAdmin");
		public static String API_UserInfoAPI_UserStatus = GetValue("Common","API_UserInfoAPI_UserStatus");
		public static String API_UserInfoAPI_LimitDay = GetValue("Common","API_UserInfoAPI_LimitDay");
		public static String API_UserInfoAPI_UserID = GetValue("Common","API_UserInfoAPI_UserID");
		public static String API_UserModuleAPI_NoRecord = GetValue("Common","API_UserModuleAPI_NoRecord");
		public static String API_UserModuleAPI_UserAuth = GetValue("Common","API_UserModuleAPI_UserAuth");
		public static String API_UserModuleAPI_UserModule = GetValue("Common","API_UserModuleAPI_UserModule");
		//Add by Andy 2006-11-2
		public static String API_CommonAPI_UpdateInfo = GetValue("Common","API_CommonAPI_UpdateInfo");
		public static String API_CommonAPI_NoUpdateInfo = GetValue("Common","API_CommonAPI_NoUpdateInfo");
		public static String API_CommonAPI_BugInfo = GetValue("Common","API_CommonAPI_BugInfo");
		public static String API_CommonAPI_NoBugInfo = GetValue("Common","API_CommonAPI_NoBugInfo");
		public static String API_Submit = GetValue("Common","API_Submit");
		public static String API_Handle = GetValue("Common","API_Handle");
		#endregion

		#region SDO
		public static String SDOAPI_SDO = GetValue("SDO","SDOAPI_SDO");
		public static String SDOAPI_SDOChallengeDataAPI_Challenge = GetValue("SDO","SDOAPI_SDOChallengeDataAPI_Challenge");
		public static String SDOAPI_SDOChallengeDataAPI_Scene = GetValue("SDO","SDOAPI_SDOChallengeDataAPI_Scene");
		public static String SDOAPI_SDOChallengeDataAPI_SceneProbability = GetValue("SDO","SDOAPI_SDOChallengeDataAPI_SceneProbability");
		public static String SDOAPI_SDOChallengeDataAPI_GameMusicList = GetValue("SDO","SDOAPI_SDOChallengeDataAPI_GameMusicList");
		public static String SDOAPI_SDOChallengeDataAPI_ProbabilityList = GetValue("SDO","SDOAPI_SDOChallengeDataAPI_ProbabilityList");
		public static String SDOAPI_SDOChallengeDataAPI_NoChallengeScene = GetValue("SDO","SDOAPI_SDOChallengeDataAPI_NoChallengeScene");
		public static String SDOAPI_SDOChallengeDataAPI_NoGameMusicList = GetValue("SDO","SDOAPI_SDOChallengeDataAPI_NoGameMusicList");
		public static String SDOAPI_SDOChallengeDataAPI_NoSceneList = GetValue("SDO","SDOAPI_SDOChallengeDataAPI_NoSceneList");
		public static String SDOAPI_SDOChallengeDataAPI_NoProbabilityList = GetValue("SDO","SDOAPI_SDOChallengeDataAPI_NoProbabilityList");
		public static String SDOAPI_SDOCharacterInfoAPI_NoAccount = GetValue("SDO","SDOAPI_SDOCharacterInfoAPI_NoAccount");
		public static String SDOAPI_SDOCharacterInfoAPI_AccountInfo = GetValue("SDO","SDOAPI_SDOCharacterInfoAPI_AccountInfo");
		public static String SDOAPI_SDOCharacterInfoAPI_NoRelativeInfo = GetValue("SDO","SDOAPI_SDOCharacterInfoAPI_NoRelativeInfo");
		public static String SDOAPI_SDOItemLogInfoAPI_Account = GetValue("SDO","SDOAPI_SDOItemLogInfoAPI_Account");
		public static String SDOAPI_SDOItemLogInfoAPI_FillDetail = GetValue("SDO","SDOAPI_SDOItemLogInfoAPI_FillDetail");
		public static String SDOAPI_SDOItemLogInfoAPI_Sum = GetValue("SDO","SDOAPI_SDOItemLogInfoAPI_Sum");
		public static String SDOAPI_SDOItemLogInfoAPI_GCash = GetValue("SDO","SDOAPI_SDOItemLogInfoAPI_GCash");
		public static String SDOAPI_SDOItemLogInfoAPI_Compensate = GetValue("SDO","SDOAPI_SDOItemLogInfoAPI_Compensate");
		public static String SDOAPI_SDOItemLogInfoAPI_NoChargeRecord = GetValue("SDO","SDOAPI_SDOItemLogInfoAPI_NoChargeRecord");
		public static String SDOAPI_SDOItemLogInfoAPI_NoTotalValue = GetValue("SDO","SDOAPI_SDOItemLogInfoAPI_NoTotalValue");
		public static String SDOAPI_SDOItemShopAPI_GameItem = GetValue("SDO","SDOAPI_SDOItemShopAPI_GameItem");
		public static String SDOAPI_SDOItemShopAPI_PersonalItem = GetValue("SDO","SDOAPI_SDOItemShopAPI_PersonalItem");
		public static String SDOAPI_SDOItemShopAPI_GiftItem = GetValue("SDO","SDOAPI_SDOItemShopAPI_GiftItem");
		public static String SDOAPI_SDOItemShopAPI_OnlineStatus = GetValue("SDO","SDOAPI_SDOItemShopAPI_OnlineStatus");
		public static String SDOAPI_SDOItemShopAPI_ConsumeRecord = GetValue("SDO","SDOAPI_SDOItemShopAPI_ConsumeRecord");
		public static String SDOAPI_SDOItemShopAPI_TradeRecord = GetValue("SDO","SDOAPI_SDOItemShopAPI_TradeRecord");
		public static String SDOAPI_SDOItemShopAPI_NoItem = GetValue("SDO","SDOAPI_SDOItemShopAPI_NoItem");
		public static String SDOAPI_SDOItemShopAPI_NoItemOnPlayer = GetValue("SDO","SDOAPI_SDOItemShopAPI_NoItemOnPlayer");
		public static String SDOAPI_SDOItemShopAPI_NoItemOnGift = GetValue("SDO","SDOAPI_SDOItemShopAPI_NoItemOnGift");
		public static String SDOAPI_SDOItemShopAPI_NoOnlineStatus = GetValue("SDO","SDOAPI_SDOItemShopAPI_NoOnlineStatus");
		public static String SDOAPI_SDOItemShopAPI_NoItemLimit = GetValue("SDO","SDOAPI_SDOItemShopAPI_NoItemLimit");
		public static String SDOAPI_SDOItemShopAPI_NoChargeRecord = GetValue("SDO","SDOAPI_SDOItemShopAPI_NoChargeRecord");
		public static String SDOAPI_SDOItemShopAPI_NoTradeRecord = GetValue("SDO","SDOAPI_SDOItemShopAPI_NoTradeRecord");
		public static String SDOAPI_SDOMemberInfoAPI_ActiveState = GetValue("SDO","SDOAPI_SDOMemberInfoAPI_ActiveState");
		public static String SDOAPI_SDOMemberInfoAPI_NoActived = GetValue("SDO","SDOAPI_SDOMemberInfoAPI_NoActived");
		public static String SDOAPI_SDOMemberInfoAPI_AllBanAccount = GetValue("SDO","SDOAPI_SDOMemberInfoAPI_AllBanAccount");
		public static String SDOAPI_SDOMemberInfoAPI_NoBanAccount = GetValue("SDO","SDOAPI_SDOMemberInfoAPI_NoBanAccount");
		public static String SDOAPI_SDOMemberInfoAPI_BanInfo = GetValue("SDO","SDOAPI_SDOMemberInfoAPI_BanInfo");
		public static String SDOAPI_SDOMemberInfoAPI_NoBanInfo = GetValue("SDO","SDOAPI_SDOMemberInfoAPI_NoBanInfo");
		public static String SDOAPI_SDOMemberInfoAPI_AccountUnlock = GetValue("SDO","SDOAPI_SDOMemberInfoAPI_AccountUnlock");
		public static String SDOAPI_SDOMemberInfoAPI_AccountLock = GetValue("SDO","SDOAPI_SDOMemberInfoAPI_AccountLock");
		public static String SDOAPI_SDOMemberInfoAPI_NoCurrentInfo = GetValue("SDO","SDOAPI_SDOMemberInfoAPI_NoCurrentInfo");
		public static String SDOAPI_SDOMemberInfoAPI_CurrentState = GetValue("SDO","SDOAPI_SDOMemberInfoAPI_CurrentState");
		public static String SDOAPI_SDOMemberInfoAPI_PadActive = GetValue("SDO","SDOAPI_SDOMemberInfoAPI_PadActive");
		public static String SDOAPI_SDOMemberInfoAPI_PadBan = GetValue("SDO","SDOAPI_SDOMemberInfoAPI_PadBan");
		public static String SDOAPI_SDONoticeInfoAPI_SendNoticeInfo = GetValue("SDO","SDOAPI_SDONoticeInfoAPI_SendNoticeInfo");
		public static String SDOAPI_SDONoticeInfoAPI_SendNoticeList = GetValue("SDO","SDOAPI_SDONoticeInfoAPI_SendNoticeList");
		public static String SDOAPI_SDONoticeInfoAPI_NoNoticeList = GetValue("SDO","SDOAPI_SDONoticeInfoAPI_NoNoticeList");
		public static String SDOAPI_SDONoticeInfoAPI_ChannelList = GetValue("SDO","SDOAPI_SDONoticeInfoAPI_ChannelList");
		public static String SDOAPI_SDONoticeInfoAPI_NoChannelInfo = GetValue("SDO","SDOAPI_SDONoticeInfoAPI_NoChannelInfo");
		public static String SDOAPI_SDONoticeInfoAPI_LoginFailure = GetValue("SDO","SDOAPI_SDONoticeInfoAPI_LoginFailure");

		#endregion

		#region BAF
		public static String O2JAM2API_BAF = GetValue("BAF","O2JAM2API_BAF");
		public static String O2JAM2API_AccountInfoAPI_Account = GetValue("BAF","O2JAM2API_AccountInfoAPI_Account");
		public static String O2JAM2API_AccountInfoAPI_AccountInfo = GetValue("BAF","O2JAM2API_AccountInfoAPI_AccountInfo");
		public static String O2JAM2API_AccountInfoAPI_NoAccount = GetValue("BAF","O2JAM2API_AccountInfoAPI_NoAccount");
		public static String O2JAM2API_AccountInfoAPI_ActiveState = GetValue("BAF","O2JAM2API_AccountInfoAPI_ActiveState");
		public static String O2JAM2API_AccountInfoAPI_AllBanAccount = GetValue("BAF","O2JAM2API_AccountInfoAPI_AllBanAccount");
		public static String O2JAM2API_AccountInfoAPI_NoBanAccount = GetValue("BAF","O2JAM2API_AccountInfoAPI_NoBanAccount");
		public static String O2JAM2API_AccountInfoAPI_BanState = GetValue("BAF","O2JAM2API_AccountInfoAPI_BanState");
		public static String O2JAM2API_AccountInfoAPI_BanInfo = GetValue("BAF","O2JAM2API_AccountInfoAPI_BanInfo");
		public static String O2JAM2API_AccountInfoAPI_NoAccountBanInfo = GetValue("BAF","O2JAM2API_AccountInfoAPI_NoAccountBanInfo");
		public static String O2JAM2API_AccountInfoAPI_AccountUnlock = GetValue("BAF","O2JAM2API_AccountInfoAPI_AccountUnlock");
		public static String O2JAM2API_AccountInfoAPI_BanAccount = GetValue("BAF","O2JAM2API_AccountInfoAPI_BanAccount");
		public static String O2JAM2API_AccountInfoAPI_AccountKick = GetValue("BAF","O2JAM2API_AccountInfoAPI_AccountKick");
		public static String O2JAM2API_CharacterInfoAPI_CharacterInfo = GetValue("BAF","O2JAM2API_CharacterInfoAPI_CharacterInfo");
		public static String O2JAM2API_CharacterInfoAPI_NoCharacterInfo = GetValue("BAF","O2JAM2API_CharacterInfoAPI_NoCharacterInfo");
		public static String O2JAM2API_CharacterInfoAPI_CharacterLevelInfo = GetValue("BAF","O2JAM2API_CharacterInfoAPI_CharacterLevelInfo");
		public static String O2JAM2API_CharacterInfoAPI_NoCharacterLevelInfo = GetValue("BAF","O2JAM2API_CharacterInfoAPI_NoCharacterLevelInfo");
		public static String O2JAM2API_ItemShopAPI_AllGameItem = GetValue("BAF","O2JAM2API_ItemShopAPI_AllGameItem");
		public static String O2JAM2API_ItemShopAPI_NoGameItem = GetValue("BAF","O2JAM2API_ItemShopAPI_NoGameItem");
		public static String O2JAM2API_ItemShopAPI_AllBodyItem = GetValue("BAF","O2JAM2API_ItemShopAPI_AllBodyItem");
		public static String O2JAM2API_ItemShopAPI_NoBodyItem = GetValue("BAF","O2JAM2API_ItemShopAPI_NoBodyItem");
		public static String O2JAM2API_ItemShopAPI_GiftItem = GetValue("BAF","O2JAM2API_ItemShopAPI_GiftItem");
		public static String O2JAM2API_ItemShopAPI_NoGiftItem = GetValue("BAF","O2JAM2API_ItemShopAPI_NoGiftItem");
		public static String O2JAM2API_ItemShopAPI_OnlineStatus = GetValue("BAF","O2JAM2API_ItemShopAPI_OnlineStatus");
		public static String O2JAM2API_ItemShopAPI_NoOnlineStatus = GetValue("BAF","O2JAM2API_ItemShopAPI_NoOnlineStatus");
		public static String O2JAM2API_ItemShopAPI_ConsumeRecord = GetValue("BAF","O2JAM2API_ItemShopAPI_ConsumeRecord");
		public static String O2JAM2API_ItemShopAPI_NoConsumeRecord = GetValue("BAF","O2JAM2API_ItemShopAPI_NoConsumeRecord");
		public static String O2JAM2API_ItemShopAPI_SumConsumeRecord = GetValue("BAF","O2JAM2API_ItemShopAPI_SumConsumeRecord");
		public static String O2JAM2API_ItemShopAPI_NoSumConsumeRecord = GetValue("BAF","O2JAM2API_ItemShopAPI_NoSumConsumeRecord");
		public static String O2JAM2API_ItemShopAPI_TradeRecord = GetValue("BAF","O2JAM2API_ItemShopAPI_TradeRecord");
		public static String O2JAM2API_ItemShopAPI_NoTradeRecord = GetValue("BAF","O2JAM2API_ItemShopAPI_NoTradeRecord");
		#endregion

		#region AU
		public static String AU_AU = GetValue("Audition","AU_AU");
		public static String AU_AUAvatarListAPI_Account = GetValue("Audition","AU_AUAvatarListAPI_Account");
		public static String AU_AUAvatarListAPI_AllGameItem = GetValue("Audition","AU_AUAvatarListAPI_AllGameItem");
		public static String AU_AUAvatarListAPI_NoGameItem = GetValue("Audition","AU_AUAvatarListAPI_NoGameItem");
		public static String AU_AUAvatarListAPI_AllBodyItem = GetValue("Audition","AU_AUAvatarListAPI_AllBodyItem");
		public static String AU_AUAvatarListAPI_NoBodyItem = GetValue("Audition","AU_AUAvatarListAPI_NoBodyItem");
		public static String AU_AUAvatarListAPI_GiftItem = GetValue("Audition","AU_AUAvatarListAPI_GiftItem");
		public static String AU_AUAvatarListAPI_NoGiftItem = GetValue("Audition","AU_AUAvatarListAPI_NoGiftItem");
		public static String AU_AUAvatarListAPI_ConsumeRecord = GetValue("Audition","AU_AUAvatarListAPI_ConsumeRecord");
		public static String AU_AUAvatarListAPI_NoConsumeRecord = GetValue("Audition","AU_AUAvatarListAPI_NoConsumeRecord");
		public static String AU_AUAvatarListAPI_SumConsumeRecord = GetValue("Audition","AU_AUAvatarListAPI_SumConsumeRecord");
		public static String AU_AUAvatarListAPI_NoSumConsumeRecord = GetValue("Audition","AU_AUAvatarListAPI_NoSumConsumeRecord");
		public static String AU_AUAvatarListAPI_TradeRecord = GetValue("Audition","AU_AUAvatarListAPI_TradeRecord");
		public static String AU_AUAvatarListAPI_NoTradeRecord = GetValue("Audition","AU_AUAvatarListAPI_NoTradeRecord");
		public static String AU_AUAvatarListAPI_SumTradeRecord = GetValue("Audition","AU_AUAvatarListAPI_SumTradeRecord");
		public static String AU_AUAvatarListAPI_NoSumTradeRecord = GetValue("Audition","AU_AUAvatarListAPI_NoSumTradeRecord");
		public static String AU_AUCharacterInfoAPI_CharacterLevelInfo = GetValue("Audition","AU_AUCharacterInfoAPI_CharacterLevelInfo");
		public static String AU_AUCharacterInfoAPI_NoCharacterLevelInfo = GetValue("Audition","AU_AUCharacterInfoAPI_NoCharacterLevelInfo");
		public static String AU_AUCharacterInfoAPI_CharacterInfo = GetValue("Audition","AU_AUCharacterInfoAPI_CharacterInfo");
		public static String AU_AUCharacterInfoAPI_NoCharacterInfo = GetValue("Audition","AU_AUCharacterInfoAPI_NoCharacterInfo");
		public static String AU_AUCharacterInfoAPI_AccountInfo = GetValue("Audition","AU_AUCharacterInfoAPI_AccountInfo");
		public static String AU_AUCharacterInfoAPI_NoAccount = GetValue("Audition","AU_AUCharacterInfoAPI_NoAccount");
		public static String AU_AUMemberInfoAPI_AllBanAccount = GetValue("Audition","AU_AUMemberInfoAPI_AllBanAccount");
		public static String AU_AUMemberInfoAPI_NoBanAccount = GetValue("Audition","AU_AUMemberInfoAPI_NoBanAccount");
		public static String AU_AUMemberInfoAPI_BanInfo = GetValue("Audition","AU_AUMemberInfoAPI_BanInfo");
		public static String AU_AUMemberInfoAPI_NoAccountBanInfo = GetValue("Audition","AU_AUMemberInfoAPI_NoAccountBanInfo");
		public static String AU_AUMemberInfoAPI_AccountUnlock = GetValue("Audition","AU_AUMemberInfoAPI_AccountUnlock");
		public static String AU_AUMemberInfoAPI_BanAccount = GetValue("Audition","AU_AUMemberInfoAPI_BanAccount");
		public static String AU_AUMemberInfoAPI_NickName = GetValue("Audition","AU_AUMemberInfoAPI_NickName");
		public static String AU_AUMemberInfoAPI_BanState = GetValue("Audition","AU_AUMemberInfoAPI_BanState");
		public static String AU_AUMemberInfoAPI_AlreadyUnlock = GetValue("Audition","AU_AUMemberInfoAPI_AlreadyUnlock");
		public static String AU_AUMemberInfoAPI_AlreadyBan = GetValue("Audition","AU_AUMemberInfoAPI_AlreadyBan");
		#endregion

		#region Soccer
		public static String Soccer_Soccer = GetValue("Soccer","Soccer_Soccer");
		public static String Soccer_CharacterInfoAPI_Account = GetValue("Soccer","Soccer_CharacterInfoAPI_Account");
		public static String Soccer_CharacterInfoAPI_Character = GetValue("Soccer","Soccer_CharacterInfoAPI_Character");
		public static String Soccer_CharacterInfoAPI_AccountInfo = GetValue("Soccer","Soccer_CharacterInfoAPI_AccountInfo");
		public static String Soccer_CharacterInfoAPI_NoAccount = GetValue("Soccer","Soccer_CharacterInfoAPI_NoAccount");
		public static String Soccer_CharacterInfoAPI_AlreadyUnlock = GetValue("Soccer","ASoccer_CharacterInfoAPI_AlreadyUnlock");
		public static String Soccer_CharacterInfoAPI_AlreadyBan = GetValue("Soccer","Soccer_CharacterInfoAPI_AlreadyBan");
		public static String Soccer_CharacterInfoAPI_NoBan = GetValue("Soccer","Soccer_CharacterInfoAPI_NoBan");
		public static String Soccer_CharacterInfoAPI_GCash = GetValue("Soccer","Soccer_CharacterInfoAPI_GCash");
		public static String Soccer_CharacterInfoAPI_BanInfo = GetValue("Soccer","Soccer_CharacterInfoAPI_BanInfo");
		public static String Soccer_CharacterInfoAPI_Recovery = GetValue("Soccer","Soccer_CharacterInfoAPI_Recovery");
		#endregion

		#region CR
		public static String CR_CR = GetValue("CR","CR_CR");
		public static String CR_AccountInfoAPI_Account = GetValue("CR","CR_AccountInfoAPI_Account");
		public static String CR_AccountInfoAPI_AccountInfo = GetValue("CR","CR_AccountInfoAPI_AccountInfo");
		public static String CR_AccountInfoAPI_NoAccount = GetValue("CR","CR_AccountInfoAPI_NoAccount");
		public static String CR_AccountInfoAPI_ActiveState = GetValue("CR","CR_AccountInfoAPI_ActiveState");
		public static String CR_CallBoardAPI_ChannelList = GetValue("CR","CR_CallBoardAPI_ChannelList");
		public static String CR_CallBoardAPI_NoChannelList = GetValue("CR","CR_CallBoardAPI_NoChannelList");
		public static String CR_CallBoardAPI_AllNoticeInfo = GetValue("CR","CR_CallBoardAPI_AllNoticeInfo");
		public static String CR_CallBoardAPI_NoNoticeInfo = GetValue("CR","CR_CallBoardAPI_NoNoticeInfo");
		public static String CR_CallBoardAPI_ChannelInfo = GetValue("CR","CR_CallBoardAPI_ChannelInfo");
		public static String CR_CharacterInfoAPI_CharacterInfo = GetValue("CR","CR_CharacterInfoAPI_CharacterInfo");
		public static String CR_CharacterInfoAPI_NickNameInfo = GetValue("CR","CR_CharacterInfoAPI_NickNameInfo");
		public static String CR_CharacterInfoAPI_OnlineStatus = GetValue("CR","CR_CharacterInfoAPI_OnlineStatus");
		#endregion

		#region O2JAM
		public static String O2JAM_O2JAM = GetValue("O2JAM","O2JAM_O2JAM");
		public static String O2JAM_CharacterInfoAPI_Account = GetValue("O2JAM","O2JAM_CharacterInfoAPI_Account");
		public static String O2JAM_CharacterInfoAPI_CharacterInfo = GetValue("O2JAM","O2JAM_CharacterInfoAPI_CharacterInfo");
		public static String O2JAM_CharacterInfoAPI_NoCharacterInfo = GetValue("O2JAM","O2JAM_CharacterInfoAPI_NoCharacterInfo");
		public static String O2JAM_CharacterInfoAPI_NoAccount = GetValue("O2JAM","O2JAM_CharacterInfoAPI_NoAccount");
		public static String O2JAM_ItemLogInfoAPI_FillDetail = GetValue("O2JAM","O2JAM_ItemLogInfoAPI_FillDetail");
		public static String O2JAM_ItemLogInfoAPI_NoChargeRecord = GetValue("O2JAM","O2JAM_ItemLogInfoAPI_NoChargeRecord");
		public static String O2JAM_ItemLogInfoAPI_SumFillDetail = GetValue("O2JAM","O2JAM_ItemLogInfoAPI_SumFillDetail");
		public static String O2JAM_ItemLogInfoAPI_NoSumChargeRecord = GetValue("O2JAM","O2JAM_ItemLogInfoAPI_NoSumChargeRecord");
		public static String O2JAM_ItemLogInfoAPI_GCash = GetValue("O2JAM","O2JAM_ItemLogInfoAPI_GCash");
		public static String O2JAM_ItemLogInfoAPI_Sum = GetValue("O2JAM","O2JAM_ItemLogInfoAPI_Sum");
		public static String O2JAM_ItemLogInfoAPI_Compensate = GetValue("O2JAM","O2JAM_ItemLogInfoAPI_Compensate");
		public static String O2JAM_ItemShopInfoAPI_GameItem = GetValue("O2JAM","O2JAM_ItemShopInfoAPI_GameItem");
		public static String O2JAM_ItemShopInfoAPI_PersonalItem = GetValue("O2JAM","O2JAM_ItemShopInfoAPI_PersonalItem");
		public static String O2JAM_ItemShopInfoAPI_GiftItem = GetValue("O2JAM","O2JAM_ItemShopInfoAPI_GiftItem");
		public static String O2JAM_ItemShopInfoAPI_NoItem = GetValue("O2JAM","O2JAM_ItemShopInfoAPI_NoItem");
		public static String O2JAM_ItemShopInfoAPI_NoItemOnPlayer = GetValue("O2JAM","O2JAM_ItemShopInfoAPI_NoItemOnPlayer");
		public static String O2JAM_ItemShopInfoAPI_NoItemOnGift = GetValue("O2JAM","O2JAM_ItemShopInfoAPI_NoItemOnGift");
		public static String O2JAM_ItemShopInfoAPI_ConsumeRecord = GetValue("O2JAM","O2JAM_ItemShopInfoAPI_ConsumeRecord");
		public static String O2JAM_ItemShopInfoAPI_NoConsumeRecord = GetValue("O2JAM","O2JAM_ItemShopInfoAPI_NoConsumeRecord");
		public static String O2JAM_ItemShopInfoAPI_SumConsumeRecord = GetValue("O2JAM","O2JAM_ItemShopInfoAPI_SumConsumeRecord");
		public static String O2JAM_ItemShopInfoAPI_NoSumConsumeRecord = GetValue("O2JAM","O2JAM_ItemShopInfoAPI_NoSumConsumeRecord");
		public static String O2JAM_ItemShopInfoAPI_ItemName = GetValue("O2JAM","O2JAM_ItemShopInfoAPI_ItemName");
		public static String O2JAM_ItemShopInfoAPI_NoItemName = GetValue("O2JAM","O2JAM_ItemShopInfoAPI_NoItemName");
		public static String O2JAM_MemberInfoAPI_ActiveState = GetValue("O2JAM","O2JAM_MemberInfoAPI_ActiveState");
		public static String O2JAM_MemberInfoAPI_NoActived = GetValue("O2JAM","O2JAM_MemberInfoAPI_NoActived");
		public static String O2JAM_MemberInfoAPI_AllBanAccount = GetValue("O2JAM","O2JAM_MemberInfoAPI_AllBanAccount");
		public static String O2JAM_MemberInfoAPI_NoBanAccount = GetValue("O2JAM","O2JAM_MemberInfoAPI_NoBanAccount");
		public static String O2JAM_MemberInfoAPII_BanState = GetValue("O2JAM","O2JAM_MemberInfoAPII_BanState");
		public static String O2JAM_MemberInfoAPI_BanInfo = GetValue("O2JAM","O2JAM_MemberInfoAPI_BanInfo");
		public static String O2JAM_MemberInfoAPI_NoAccountBanInfo = GetValue("O2JAM","O2JAM_MemberInfoAPI_NoAccountBanInfo");
		public static String O2JAM_MemberInfoAPI_AccountUnlock = GetValue("O2JAM","O2JAM_MemberInfoAPI_AccountUnlock");
		public static String O2JAM_MemberInfoAPI_BanAccount = GetValue("O2JAM","O2JAM_MemberInfoAPI_BanAccount");
		public static String O2JAM_MemberInfoAPI_CurrentStatus = GetValue("O2JAM","O2JAM_MemberInfoAPI_CurrentStatus");
		public static String O2JAM_MemberInfoAPI_NoCurrentStatus = GetValue("O2JAM","O2JAM_MemberInfoAPI_NoCurrentStatus");
		#endregion

		#region UserCharge
		public static String NineYou = GetValue("UserCharge","NineYou");
		public static String CardDetail_Account = GetValue("UserCharge","CardDetail_Account");
		public static String CardDetail_RegisterInfo = GetValue("UserCharge","CardDetail_RegisterInfo");
		public static String CardDetail_NoRegisterInfo = GetValue("UserCharge","CardDetail_NoRegisterInfo");
		public static String CardDetail_OneCard = GetValue("UserCharge","CardDetail_OneCard");
		public static String CardDetail_LeisureCard = GetValue("UserCharge","CardDetail_LeisureCard");
		public static String CardDetail_FillDetail = GetValue("UserCharge","CardDetail_FillDetail");
		public static String CardDetail_NoChargeRecord = GetValue("UserCharge","CardDetail_NoChargeRecord");
		public static String CardDetail_SumFillDetail = GetValue("UserCharge","CardDetail_SumFillDetail");
		public static String CardDetail_NoSumChargeRecord = GetValue("UserCharge","CardDetail_NoSumChargeRecord");
		public static String CardDetail_ConsumeRecord = GetValue("UserCharge","CardDetail_ConsumeRecord");
		public static String CardDetail_NoConsumeRecord = GetValue("UserCharge","CardDetail_NoConsumeRecord");
		public static String CardDetail_SumConsumeRecord = GetValue("UserCharge","CardDetail_SumConsumeRecord");
		public static String CardDetail_NoSumConsumeRecord = GetValue("UserCharge","CardDetail_NoSumConsumeRecord");
		public static String CardDetail_Reset = GetValue("UserCharge","CardDetail_Reset");
		public static String CardDetail_IDInfo = GetValue("UserCharge","CardDetail_IDInfo");
		public static String CardDetail_SecurityInfo = GetValue("UserCharge","CardDetail_SecurityInfo");
		public static String CardDetail_Lock = GetValue("UserCharge","CardDetail_Lock");
		public static String CardDetail_Locked = GetValue("UserCharge","CardDetail_Locked");
		public static String CardDetail_UserInfo = GetValue("UserCharge","CardDetail_UserInfo");
		public static String UserCashPurchase_GCash = GetValue("UserCharge","UserCashPurchase_GCash");
		public static String UserCashPurchase_NoGCash = GetValue("UserCharge","UserCashPurchase_NoGCash");
		public static String UserCashPurchase_SumGCash = GetValue("UserCharge","UserCashPurchase_SumGCash");
		public static String UserCashPurchase_NoSumGCash = GetValue("UserCharge","UserCashPurchase_NoSumGCash");
		public static String UserCashPurchase_MCash = GetValue("UserCharge","UserCashPurchase_MCash");
		public static String UserCashPurchase_NoMCash = GetValue("UserCharge","UserCashPurchase_NoMCash");
		public static String UserCashPurchase_SumMCash = GetValue("UserCharge","UserCashPurchase_SumMCash");
		public static String UserCashPurchase_NoSumMCash = GetValue("UserCharge","UserCashPurchase_NoSumMCash");
		public static String UserCashPurchase_Integral = GetValue("UserCharge","UserCashPurchase_Integral");
		public static String UserCashPurchase_NoIntegral = GetValue("UserCharge","UserCashPurchase_NoIntegral");
		public static String UserCashPurchase_ItemChange = GetValue("UserCharge","UserCashPurchase_ItemChange");
		public static String UserCashPurchase_NoItemChange = GetValue("UserCharge","UserCashPurchase_NoItemChange");
		public static String UserCashPurchase_ItemChangeDetails = GetValue("UserCharge","UserCashPurchase_ItemChangeDetails");
		public static String UserCashPurchase_NoItemChangeDetails = GetValue("UserCharge","UserCashPurchase_NoItemChangeDetails");
		public static String UserConsumeDetail_ConsumeInfo = GetValue("UserCharge","UserConsumeDetail_ConsumeInfo");
		public static String UserConsumeDetail_NoConsumeInfo = GetValue("UserCharge","UserConsumeDetail_NoConsumeInfo");
		#endregion

	}
}
