using System;
using System.Data;
using System.Text;
using SDO.SDODataInfo;
using Common.Logic;
using Common.DataInfo;
using System.IO;
using System.Collections;
using System.Configuration;
using lg = Common.API.LanguageAPI;
namespace SDO.SDOAPI
{
	/// <summary>
	/// ChallengeDataAPI 的摘要说明。
	/// </summary>
	public class SDOChallengeDataAPI
	{
		Message msg = null;
		private static string SongPath = ConfigurationSettings.AppSettings["SongPath"];//@"E:\SDO\song.txt";
		private static string ScenePath = ConfigurationSettings.AppSettings["ScenePath"];//@"E:\SDO\scene.txt";
		private static string DatPath = ConfigurationSettings.AppSettings["DatPath"];//@"E:\SDO\serverconfig.dat";		

		public SDOChallengeDataAPI(byte[] packet)
		{
			msg = new Message(packet,(uint)packet.Length);
		}
		/// <summary>
		/// 查看挑战场景
		/// </summary>
		/// <returns></returns>
		public Message SDO_Challenge_Query(int index,int pageSize)
		{
			string serverIP = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDOChallengeDataAPI_Challenge + lg.SDOAPI_SDOChallengeDataAPI_Scene + "!");
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDOChallengeDataAPI_Challenge + lg.SDOAPI_SDOChallengeDataAPI_Scene + "!");
				ds = ChallengeDataInfo.TO2JAM_Challenge_Query(serverIP);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					//总页数
					int pageCount = 0;
					pageCount = ds.Tables[0].Rows.Count % pageSize;
					if (pageCount > 0)
					{
						pageCount = ds.Tables[0].Rows.Count / pageSize + 1;
					}
					else
						pageCount = ds.Tables[0].Rows.Count / pageSize;
					if (index + pageSize > ds.Tables[0].Rows.Count)
					{
						pageSize = ds.Tables[0].Rows.Count - index;
					}
					Query_Structure[] structList = new Query_Structure[pageSize];
					for(int i=index;i<index+pageSize;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.SDO_SenceID,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.SDO_WeekDay,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.SDO_MatPtHR,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[3]);
						strut.AddTagKey(TagName.SDO_MatPtMin, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[4]);
						strut.AddTagKey(TagName.SDO_StPtHR,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[5]);
						strut.AddTagKey(TagName.SDO_StPtMin,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[6]);
						strut.AddTagKey(TagName.SDO_EdPtHR,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[7]);
						strut.AddTagKey(TagName.SDO_EdPtMin,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[8]);
						strut.AddTagKey(TagName.SDO_GCash,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[9]);
						strut.AddTagKey(TagName.SDO_MCash,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						//bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[10]);
						//strut.AddTagKey(TagName.SDO_SenceID,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[11]);
						strut.AddTagKey(TagName.SDO_Sence,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[12]);
						strut.AddTagKey(TagName.SDO_MusicID1,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[13]);
						strut.AddTagKey(TagName.SDO_LV1,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[14]);
						strut.AddTagKey(TagName.SDO_MusicID2,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[15]);
						strut.AddTagKey(TagName.SDO_LV2,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[16]);
						strut.AddTagKey(TagName.SDO_MusicID3,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[17]);
						strut.AddTagKey(TagName.SDO_LV3,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[18]);
						strut.AddTagKey(TagName.SDO_MusicID4,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[19]);
						strut.AddTagKey(TagName.SDO_LV4,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[20]);
						strut.AddTagKey(TagName.SDO_MusicID5,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[21]);
						strut.AddTagKey(TagName.SDO_LV5,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						//总页数
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
						structList[i - index] = strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_QUERY_RESP,22);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDOAPI_SDOChallengeDataAPI_NoSceneList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.SDOAPI_SDOChallengeDataAPI_NoSceneList, Msg_Category.SDO_ADMIN, ServiceKey.SDO_CHALLENGE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 查看游戏音乐数据列表
		/// </summary>
		/// <returns></returns>
		public Message SDO_MusicData_Query()
		{
			string serverIP = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDOChallengeDataAPI_GameMusicList);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDOChallengeDataAPI_GameMusicList);
				//读取全部歌曲名
				StreamReader objReader = new StreamReader(SongPath,Encoding.GetEncoding("GB2312"));
				string sLine="";
				ArrayList arrText = new ArrayList();
				while (sLine != null)
				{
					sLine = objReader.ReadLine();
					if (sLine != null)
						arrText.Add(sLine);
				}//while
				objReader.Close();

				//读取需要的歌曲ID
				FileStream fs = new FileStream(DatPath, FileMode.Open, FileAccess.Read);
				byte[] sss = new byte[fs.Length];			
				fs.Read(sss,0,sss.Length);
				int music_length = BitConverter.ToInt32(sss,0);
				int[] musicid = new int[music_length];
				int[] newmusicid = new int[music_length];
				for (int i=1; i<music_length+1; i++)
				{
					musicid[i-1]=BitConverter.ToInt32(sss,i*4);
					newmusicid[i-1]=BitConverter.ToInt32(sss,i*4) + 0x1000000;
				}

				if(0!=musicid.Length && 0!=arrText.Count)
				{
					Query_Structure[] structList = new Query_Structure[musicid.Length];
					for(int i=0;i<musicid.Length;i++)
					{
						Query_Structure strut = new Query_Structure((uint)2);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,newmusicid[i]);
						strut.AddTagKey(TagName.SDO_MusicID1,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,arrText[(int)musicid[i]]);
						strut.AddTagKey(TagName.SDO_MusicName1,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						structList[i]=strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_MUSICDATA_QUERY_RESP,2);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDOAPI_SDOChallengeDataAPI_NoGameMusicList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_MUSICDATA_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.SDOAPI_SDOChallengeDataAPI_NoGameMusicList, Msg_Category.SDO_ADMIN, ServiceKey.SDO_MUSICDATA_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 根据ID查看游戏音乐数据列表
		/// </summary>
		/// <returns></returns>
		public Message SDO_MusicData_SingleQuery()
		{
			string serverIP = null;
			int musicID = 0;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				TLV_Structure tlvStrut = new TLV_Structure(TagName.SDO_MusicID1,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_MusicID1).m_bValueBuffer);
				musicID =(int)tlvStrut.toInteger();
				SqlHelper.log.WriteLog(lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDOChallengeDataAPI_GameMusicList);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDOChallengeDataAPI_GameMusicList);
				//				if(serverIP == "61.151.249.172")
				//				{
				//读取全部歌曲名
				StreamReader objReader = new StreamReader(SongPath,Encoding.GetEncoding("GB2312"));
				string sLine="";
				ArrayList arrText = new ArrayList();
				while (sLine != null)
				{
					sLine = objReader.ReadLine();
					if (sLine != null)
						arrText.Add(sLine);
				}
				objReader.Close();

				//读取需要的歌曲ID
				FileStream fs = new FileStream(DatPath, FileMode.Open, FileAccess.Read);
				byte[] sss = new byte[fs.Length];			
				fs.Read(sss,0,sss.Length);
				int music_length = BitConverter.ToInt32(sss,0);
				int[] musicid = new int[music_length];
				for (int i=1; i<music_length+1; i++)
				{
					musicid[i-1]=BitConverter.ToInt32(sss,i*4);
				}

				if(0!=musicid.Length && 0!=arrText.Count&&0!=musicID)
				{
					Query_Structure[] structList = new Query_Structure[1];
					Query_Structure strut = new Query_Structure((uint)1);
					byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,arrText[musicID - 0x1000000]);
					strut.AddTagKey(TagName.SDO_MusicName1,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
					structList[0]=strut;
						
					return Message.COMMON_MES_RESP(structList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_MUSICDATA_OWN_QUERY_RESP,1);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDOAPI_SDOChallengeDataAPI_NoGameMusicList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_MUSICDATA_OWN_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.SDOAPI_SDOChallengeDataAPI_NoGameMusicList, Msg_Category.SDO_ADMIN, ServiceKey.SDO_MUSICDATA_OWN_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 查看游戏场景数据列表
		/// </summary>
		/// <returns></returns>
		public Message SDO_SceneList_Query()
		{
			DataSet ds = null;
			try
			{	
				SqlHelper.log.WriteLog(lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.SDOAPI_SDOChallengeDataAPI_Scene + "!");
				Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.SDOAPI_SDOChallengeDataAPI_Scene + "!");
//				//读取全部场景
//				StreamReader objReader = new StreamReader(ScenePath,Encoding.GetEncoding("GB2312"));
//				string sLine="";
//				ArrayList arrText = new ArrayList();
//				while (sLine != null)
//				{
//					sLine = objReader.ReadLine();
//					if (sLine != null)
//						arrText.Add(sLine);
//				}
//				objReader.Close();
//
//				//读取需要的场景ID
//				FileStream fs = new FileStream(DatPath, FileMode.Open, FileAccess.Read);
//				byte[] sss = new byte[fs.Length];			
//				fs.Read(sss,0,sss.Length);
//				int music_length = BitConverter.ToInt32(sss,0);
//				int[] musicid = new int[music_length];
//				for (int i=1; i<music_length+1; i++)
//				{
//					musicid[i-1]=BitConverter.ToInt32(sss,i*4);
//				}
//				int scene_length = BitConverter.ToInt32(sss,(music_length+1)*4);
//				int[] sceneid = new int[scene_length];
//				for (int j =1;j < scene_length + 1; j++)
//				{
//					sceneid[j-1] = BitConverter.ToInt32(sss,j*4+(music_length+1)*4);
//				}
//
//				if(0!=sceneid.Length && 0!=arrText.Count)
//				{
//					Query_Structure[] structList = new Query_Structure[sceneid.Length];
//					for(int i=0;i<sceneid.Length;i++)
//					{
//						Query_Structure strut = new Query_Structure((uint)2);
//						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,sceneid[i]);
//						strut.AddTagKey(TagName.SDO_MusicID1,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
//						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,arrText[(int)sceneid[i]]);
//						strut.AddTagKey(TagName.SDO_MusicName1,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
//						structList[i]=strut;
//					}
//					return Message.COMMON_MES_RESP(structList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_SCENE_QUERY_RESP,2);
//				}
//				else
//				{
//					return Message.COMMON_MES_RESP(lg.SDOAPI_SDOChallengeDataAPI_NoSceneList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_SCENE_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
//				}
				ds = ChallengeDataInfo.TO2JAM_SceneListQuery();	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.SDO_MusicID1,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.SDO_MusicName1,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						structList[i]=strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_SCENE_QUERY_RESP,2);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDOAPI_SDOChallengeDataAPI_NoSceneList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_SCENE_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.SDOAPI_SDOChallengeDataAPI_NoSceneList, Msg_Category.SDO_ADMIN, ServiceKey.SDO_CHALLENGE_SCENE_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 添加游戏场景资料信息
		/// </summary>
		/// <returns></returns>
		public Message SDOChallengeSceneInfo_Insert()
		{
			int result = -1;
			int operateUserID = 0;
			int sceneID = 0;
			string sceneTag = null;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_SenceID,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_SenceID).m_bValueBuffer);
				sceneID =(int)strut.toInteger();
				sceneTag = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Sence).m_bValueBuffer);
				result =ChallengeDataInfo.TO2JAM_ChallengeScene_Insert(operateUserID,sceneID,sceneTag);
				if(result==1)
				{
					SqlHelper.log.WriteLog(lg.SDOAPI_SDO + "+>" + lg.API_Add + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.SDOAPI_SDO + "+>" + lg.API_Add + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_SCENE_CREATE_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.SDOAPI_SDO + "+>" + lg.API_Add + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now+" - "+ lg.SDOAPI_SDO + "+>" + lg.API_Add + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_SCENE_CREATE_RESP);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_SCENE_CREATE_RESP);
			}
		}
		/// <summary>
		/// 更新游戏场景资料信息
		/// </summary>
		/// <returns></returns>
		public Message SDOChallengeSceneInfo_Update()
		{
			int result = -1;
			int operateUserID = 0;
			int sceneID = 0;
			string sceneTag = null;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_SenceID,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_SenceID).m_bValueBuffer);
				sceneID =(int)strut.toInteger();
				sceneTag = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Sence).m_bValueBuffer);
				result =ChallengeDataInfo.TO2JAM_ChallengeScene_Update(operateUserID,sceneID,sceneTag);
				if(result==1)
				{
					SqlHelper.log.WriteLog(lg.SDOAPI_SDO + "+>" + lg.API_Update + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.SDOAPI_SDO + "+>" + lg.API_Update + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_SCENE_UPDATE_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.SDOAPI_SDO + "+>" + lg.API_Update + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.SDOAPI_SDO + "+>" + lg.API_Update + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_SCENE_UPDATE_RESP);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_SCENE_UPDATE_RESP);
			}
		}
		/// <summary>
		/// 删除游戏场景资料信息
		/// </summary>
		/// <returns></returns>
		public Message SDOChallengeSceneInfo_Delete()
		{
			int result = -1;
			int operateUserID = 0;
			int sceneID = 0;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_SenceID,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_SenceID).m_bValueBuffer);
				sceneID =(int)strut.toInteger();
				result =ChallengeDataInfo.TO2JAM_ChallengeScene_Delete(operateUserID,sceneID);
				if(result==1)
				{
					SqlHelper.log.WriteLog(lg.SDOAPI_SDO + "+>" + lg.API_Delete + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.SDOAPI_SDO + "+>" + lg.API_Delete + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_SCENE_DELETE_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.SDOAPI_SDO + "+>" + lg.API_Delete + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.SDOAPI_SDO + "+>" + lg.API_Delete + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_SCENE_DELETE_RESP);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_SCENE_DELETE_RESP);
			}
		}
		/// <summary>
		/// 根据游戏场景中获得概率的列表
		/// </summary>
		/// <returns></returns>
		public Message SDO_Medalite_Query(int index,int pageSize)
		{
			string serverIP = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.SDOAPI_SDOChallengeDataAPI_ProbabilityList);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.SDOAPI_SDOChallengeDataAPI_ProbabilityList);
				ds = ChallengeDataInfo.TO2JAM_Medalitem_Query(serverIP);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					//总页数
					int pageCount = 0;
					pageCount = ds.Tables[0].Rows.Count % pageSize;
					if (pageCount > 0)
					{
						pageCount = ds.Tables[0].Rows.Count / pageSize + 1;
					}
					else
						pageCount = ds.Tables[0].Rows.Count / pageSize;
					if (index + pageSize > ds.Tables[0].Rows.Count)
					{
						pageSize = ds.Tables[0].Rows.Count - index;
					}
					Query_Structure[] structList = new Query_Structure[pageSize];
					for(int i=index;i<index+pageSize;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length+1);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.SDO_ItemCode,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.SDO_ItemName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.SDO_Precent,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						//道具使用次数
						int timelimits = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]);
						if (timelimits == -1)
							timelimits = 0;
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, timelimits);
						strut.AddTagKey(TagName.SDO_TimesLimit, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						//使用时限
						//道具使用次数
						int dayslimits = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]);
						if (dayslimits == -1)
							dayslimits = 0;
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, dayslimits);
						strut.AddTagKey(TagName.SDO_DaysLimit, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						strut.AddTagKey(TagName.PageCount,TagFormat.TLV_INTEGER,(uint)bytes.Length,TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
						structList[i-index]=strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_MEDALITEM_QUERY_RESP,6);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDOAPI_SDOChallengeDataAPI_NoProbabilityList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_MEDALITEM_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.SDOAPI_SDOChallengeDataAPI_NoProbabilityList, Msg_Category.SDO_ADMIN, ServiceKey.SDO_MEDALITEM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 根据游戏场景中获得概率的列表
		/// </summary>
		/// <returns></returns>
		public Message SDO_Medalite_Owner_Query(int index,int pageSize)
		{
			string serverIP = null;
			string itemName = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				itemName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ItemName).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.SDOAPI_SDOChallengeDataAPI_ProbabilityList);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.SDOAPI_SDOChallengeDataAPI_ProbabilityList);
				ds = ChallengeDataInfo.TO2JAM_Medalitem_Owner_Query(serverIP,itemName);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					//总页数
					int pageCount = 0;
					pageCount = ds.Tables[0].Rows.Count % pageSize;
					if (pageCount > 0)
					{
						pageCount = ds.Tables[0].Rows.Count / pageSize + 1;
					}
					else
						pageCount = ds.Tables[0].Rows.Count / pageSize;
					if (index + pageSize > ds.Tables[0].Rows.Count)
					{
						pageSize = ds.Tables[0].Rows.Count - index;
					}
					Query_Structure[] structList = new Query_Structure[pageSize];
					for(int i=index;i<index+pageSize;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length+1);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.SDO_ItemCode,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.SDO_ItemName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.SDO_Precent,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						//道具使用次数
						int timelimits = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]);
						if (timelimits == -1)
							timelimits = 0;
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, timelimits);
						strut.AddTagKey(TagName.SDO_TimesLimit, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						//使用时限
						//道具使用次数
						int dayslimits = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]);
						if (dayslimits == -1)
							dayslimits = 0;
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, dayslimits);
						strut.AddTagKey(TagName.SDO_DaysLimit, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						strut.AddTagKey(TagName.PageCount,TagFormat.TLV_INTEGER,(uint)bytes.Length,TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
						structList[i-index]=strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_MEDALITEM_OWNER_QUERY_RESP,6);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDOAPI_SDOChallengeDataAPI_NoProbabilityList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_MEDALITEM_OWNER_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.SDOAPI_SDOChallengeDataAPI_NoProbabilityList, Msg_Category.SDO_ADMIN, ServiceKey.SDO_MEDALITEM_OWNER_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 添加游戏场景概率信息
		/// </summary>
		/// <returns></returns>
		public Message SDOMedalitem_Insert()
		{
			int result = -1;
			string serverIP = null;
			int operateUserID = 0;
			int itemCode = 0;
			int precent = 0 ;
			int timeslimit = 0;
			int dayslimit = 0;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_ItemCode,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ItemCode).m_bValueBuffer);
				itemCode =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_Precent,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Precent).m_bValueBuffer);
				precent =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_TimesLimit,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_TimesLimit).m_bValueBuffer);
				timeslimit =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_DaysLimit,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_DaysLimit).m_bValueBuffer);
				dayslimit =(int)strut.toInteger();
				result =ChallengeDataInfo.TO2JAM_Medalitem_Insert(operateUserID,serverIP,itemCode,precent,timeslimit,dayslimit);
				if(result==1)
				{
					SqlHelper.log.WriteLog(lg.SDOAPI_SDO + "+>" + lg.API_Add + lg.SDOAPI_SDOChallengeDataAPI_SceneProbability + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.SDOAPI_SDO + "+>" + lg.API_Add + lg.SDOAPI_SDOChallengeDataAPI_SceneProbability + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_MEDALITEM_CREATE_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.SDOAPI_SDO + "+>" + lg.API_Add + lg.SDOAPI_SDOChallengeDataAPI_SceneProbability + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.SDOAPI_SDO + "+>" + lg.API_Add + lg.SDOAPI_SDOChallengeDataAPI_SceneProbability + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_MEDALITEM_CREATE_RESP);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.SDO_ADMIN,ServiceKey.SDO_MEDALITEM_CREATE_RESP);
			}
		}
		/// <summary>
		/// 更新游戏场景概率信息
		/// </summary>
		/// <returns></returns>
		public Message SDOMedalitemInfo_Update()
		{
			int result = -1;
			string serverIP = null;
			int operateUserID = 0;
			int itemCode = 0;
			int percent = 0;
			int timeslimit = 0;
			int dayslimit = 0;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_ItemCode,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ItemCode).m_bValueBuffer);
				itemCode =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_Precent,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Precent).m_bValueBuffer);
				percent =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_TimesLimit,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_TimesLimit).m_bValueBuffer);
				timeslimit =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_DaysLimit,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_DaysLimit).m_bValueBuffer);
				dayslimit =(int)strut.toInteger();
				result =ChallengeDataInfo.TO2JAM_Medalitem_Update(operateUserID,serverIP,itemCode,percent,timeslimit,dayslimit);
				if(result==1)
				{
					SqlHelper.log.WriteLog(lg.SDOAPI_SDO + "+>" + lg.API_Update + lg.SDOAPI_SDOChallengeDataAPI_SceneProbability + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.SDOAPI_SDO + "+>" + lg.API_Update + lg.SDOAPI_SDOChallengeDataAPI_SceneProbability + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_MEDALITEM_UPDATE_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.SDOAPI_SDO + "+>" + lg.API_Update + lg.SDOAPI_SDOChallengeDataAPI_SceneProbability + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.SDOAPI_SDO + "+>" + lg.API_Update + lg.SDOAPI_SDOChallengeDataAPI_SceneProbability + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_MEDALITEM_UPDATE_RESP);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_SCENE_UPDATE_RESP);
			}
		}
		/// <summary>
		/// 删除游戏场景概率信息
		/// </summary>
		/// <returns></returns>
		public Message SDOMedalitemInfo_Delete()
		{
			int result = -1;
			string serverIP = null;
			int operateUserID = 0;
			int itemcode = 0;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_ItemCode,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ItemCode).m_bValueBuffer);
				itemcode =(int)strut.toInteger();
				result = ChallengeDataInfo.TO2JAM_Medalitem_Delete(operateUserID,serverIP,itemcode);
				if(result==1)
				{
					SqlHelper.log.WriteLog(lg.SDOAPI_SDO + "+>" + lg.API_Delete + lg.SDOAPI_SDOChallengeDataAPI_SceneProbability + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.SDOAPI_SDO + "+>" + lg.API_Delete + lg.SDOAPI_SDOChallengeDataAPI_SceneProbability + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_MEDALITEM_DELETE_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.SDOAPI_SDO + "+>" + lg.API_Delete + lg.SDOAPI_SDOChallengeDataAPI_SceneProbability + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now+" - " + lg.SDOAPI_SDO + "+>" + lg.API_Delete + lg.SDOAPI_SDOChallengeDataAPI_SceneProbability + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_MEDALITEM_DELETE_RESP);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.SDO_ADMIN,ServiceKey.SDO_MEDALITEM_DELETE_RESP);
			}
		}
		/// <summary>
		/// 添加玩家资料信息
		/// </summary>
		/// <returns></returns>
		public Message SDOChallengeInfo_Insert()
		{
			int result = -1;
			int operateUserID  = 0;
			string serverIP = null;
			int weekDay = 0;
			int matPtHR = 0;
			int matPtMin = 0;
			int stPtHR = 0;
			int stPtMin = 0;
			int edPtHR = 0;
			int edPtMin = 0;
			int MCash = 0;
			int GCash = 0;
			int scence = 0;
			int musicID1 = 0;
			int lv1 = 0;
			int musicID2 = 0;
			int lv2 = 0;
			int musicID3 = 0;
			int lv3 = 0;
			int musicID4 = 0;
			int lv4 = 0;
			int musicID5 = 0;
			int lv5 = 0;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				//strut = new TLV_Structure(TagName.SDO_SenceID,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_SenceID).m_bValueBuffer);
				//sceneID =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_WeekDay,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_WeekDay).m_bValueBuffer);
				weekDay =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_MatPtHR,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_MatPtHR).m_bValueBuffer);
				matPtHR  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_MatPtMin,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_MatPtMin).m_bValueBuffer);
				matPtMin  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_StPtHR,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_StPtHR).m_bValueBuffer);
				stPtHR  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_StPtMin,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_StPtMin).m_bValueBuffer);
				stPtMin  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_EdPtHR,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_EdPtHR).m_bValueBuffer);
				edPtHR  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_EdPtMin,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_EdPtMin).m_bValueBuffer);
				edPtMin  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_MCash, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_MCash).m_bValueBuffer);
				MCash = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_GCash, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_GCash).m_bValueBuffer);
				GCash = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_Sence, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Sence).m_bValueBuffer);
				scence = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_MusicID1, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_MusicID1).m_bValueBuffer);
				musicID1 = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_LV1, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_LV1).m_bValueBuffer);
				lv1 = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_MusicID2, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_MusicID2).m_bValueBuffer);
				musicID2 = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_LV2, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_LV2).m_bValueBuffer);
				lv2 = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_MusicID3, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_MusicID3).m_bValueBuffer);
				musicID3 = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_LV3, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_LV3).m_bValueBuffer);
				lv3 = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_MusicID4, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_MusicID4).m_bValueBuffer);
				musicID4 = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_LV4, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_LV4).m_bValueBuffer);
				lv4 = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_MusicID5, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_MusicID5).m_bValueBuffer);
				musicID5 = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_LV5, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_LV5).m_bValueBuffer);
				lv5 = (int)strut.toInteger();
				result =ChallengeDataInfo.TO2JAM_Challenge_Insert(operateUserID,serverIP,weekDay,matPtHR,matPtMin,stPtHR,stPtMin,edPtHR,edPtMin,GCash,MCash,scence,musicID1,lv1,musicID2,lv2,musicID3,lv3,musicID4,lv4,musicID5,lv5);
				if(result==1)
				{
					SqlHelper.log.WriteLog(lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.API_Add + lg.SDOAPI_SDOChallengeDataAPI_Challenge + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.API_Add + lg.SDOAPI_SDOChallengeDataAPI_Challenge + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_INSERT_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.API_Add + lg.SDOAPI_SDOChallengeDataAPI_Challenge + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.API_Add + lg.SDOAPI_SDOChallengeDataAPI_Challenge + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_INSERT_RESP);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_INSERT_RESP);
			}

		}
		/// <summary>
		/// 玩家资料信息修改
		/// </summary>
		/// <returns></returns>
		public Message SDOChallengeInfo_Update()
		{
			int result = -1;
			int operateUserID  = 0;
			string serverIP = null;
			int sceneID = 0;
			int weekDay = 0;
			int matPtHR = 0;
			int matPtMin = 0;
			int stPtHR = 0;
			int stPtMin = 0;
			int edPtHR = 0;
			int edPtMin = 0;
			int MCash = 0;
			int GCash = 0;
			int scence = 0;
			int musicID1 = 0;
			int lv1 = 0;
			int musicID2 = 0;
			int lv2 = 0;
			int musicID3 = 0;
			int lv3 = 0;
			int musicID4 = 0;
			int lv4 = 0;
			int musicID5 = 0;
			int lv5 = 0;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				strut = new TLV_Structure(TagName.SDO_SenceID,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_SenceID).m_bValueBuffer);
				sceneID =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_WeekDay,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_WeekDay).m_bValueBuffer);
				weekDay =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_MatPtHR,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_MatPtHR).m_bValueBuffer);
				matPtHR  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_MatPtMin,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_MatPtMin).m_bValueBuffer);
				matPtMin  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_StPtHR,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_StPtHR).m_bValueBuffer);
				stPtHR  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_StPtMin,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_StPtMin).m_bValueBuffer);
				stPtMin  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_EdPtHR,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_EdPtHR).m_bValueBuffer);
				edPtHR  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_EdPtMin,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_EdPtMin).m_bValueBuffer);
				edPtMin  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_MCash, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_MCash).m_bValueBuffer);
				MCash = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_GCash, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_GCash).m_bValueBuffer);
				GCash = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_Sence, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Sence).m_bValueBuffer);
				scence = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_MusicID1, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_MusicID1).m_bValueBuffer);
				musicID1 = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_LV1, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_LV1).m_bValueBuffer);
				lv1 = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_MusicID2, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_MusicID2).m_bValueBuffer);
				musicID2 = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_LV2, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_LV2).m_bValueBuffer);
				lv2 = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_MusicID3, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_MusicID3).m_bValueBuffer);
				musicID3 = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_LV3, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_LV3).m_bValueBuffer);
				lv3 = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_MusicID4, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_MusicID4).m_bValueBuffer);
				musicID4 = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_LV4, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_LV4).m_bValueBuffer);
				lv4 = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_MusicID5, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_MusicID5).m_bValueBuffer);
				musicID5 = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_LV5, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_LV5).m_bValueBuffer);
				lv5 = (int)strut.toInteger();
				result =ChallengeDataInfo.TO2JAM_Challenge_Update(operateUserID,serverIP,sceneID,weekDay,matPtHR,matPtMin,stPtHR,stPtMin,edPtHR,edPtMin,GCash,MCash,scence,musicID1,lv1,musicID2,lv2,musicID3,lv3,musicID4,lv4,musicID5,lv5);
				if(result==1)
				{
					SqlHelper.log.WriteLog(lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.API_Update + lg.SDOAPI_SDOChallengeDataAPI_Challenge + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now + " - "+ lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.API_Update + lg.SDOAPI_SDOChallengeDataAPI_Challenge + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_UPDATE_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.API_Update + lg.SDOAPI_SDOChallengeDataAPI_Challenge + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.API_Update + lg.SDOAPI_SDOChallengeDataAPI_Challenge + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_UPDATE_RESP);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_UPDATE_RESP);
			}

		}
		/// <summary>
		/// 删除挑战信息
		/// </summary>
		/// <returns></returns>
		public Message Challenge_Delete()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			int scenceID = -1;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				strut = new TLV_Structure(TagName.SDO_SenceID,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_SenceID).m_bValueBuffer);
				scenceID =(int)strut.toInteger();
				result = ChallengeDataInfo.TO2JAM_Challenge_Del(serverIP,operateUserID,scenceID);
				if(result == 1)
				{
					SqlHelper.log.WriteLog(lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.API_Delete + lg.SDOAPI_SDOChallengeDataAPI_Challenge + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.API_Delete + lg.SDOAPI_SDOChallengeDataAPI_Challenge + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_DELETE_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.API_Delete + lg.SDOAPI_SDOChallengeDataAPI_Challenge + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.API_Delete + lg.SDOAPI_SDOChallengeDataAPI_Challenge + lg.SDOAPI_SDOChallengeDataAPI_Scene + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_DELETE_RESP);
				}
			}
			catch(System.Exception e)
			{
				return Message.COMMON_MES_RESP(e.Message,Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHALLENGE_DELETE_RESP);
			}
		}
	}
}
