/*
 * Author:ImL1s
 *
 * Date:2016/02/11
 *
 * Email:ImL1s@outlook.com
 *
 * description:定義Prefab路徑、enum.
 */

using UnityEngine;
using System.Text;
using System;

public sealed class Define
{

	#region const(static & readonly) field 靜態字段(只讀)


	//public const string KanaTextPath = "Text/Kana";

	public static string KanaTextPath {
		get { return PathConfigProvider.Instance.GetContent ("KanaTextPath").Value; }
	}

	//public const string RomanTextPath = "Text/Roman";
	public static string RomanTextPath {
		get { return PathConfigProvider.Instance.GetContent ("RomanTextPath").Value; }
	}

	/// <summary>
	/// 單例所在的物體名稱
	/// </summary>
	public static readonly string SingletonName = "[Unity Singletion]";

	/// <summary>
	/// Resources下的音樂路徑
	/// </summary>
	public const string ResourcesAudioPath = "Audio/";

	/// <summary>
	/// 默認UI音效
	/// </summary>
	public const string ResourceDefaultAudioPath = "Audio/Button";

	#region unused

	/// <summary>
	/// 默認物品Prefab路徑.
	/// </summary>
	//public const string ItemContentPath = "UI/ItemContent";

	/// <summary>
	/// 默認Player物品Prefab路徑.
	/// </summary>
	//public const string PlayerItemContentPath = "UI/PlayerItemContent";

	/// <summary>
	/// 默認NPC物品Prefab路徑.
	/// </summary>
	//public const string NPCItemContentPath = "UI/NPCItemContent";

	/// <summary>
	/// 默認ItemSprite路徑.
	/// </summary>
	//public const string ItemSpritePath = "Sprite/Item/";

	/// <summary>
	/// 玩家主城市模型路徑.
	/// </summary>
	//public const string PlayerModelPath = "Model/Player_MainTown";


	/// <summary>
	/// CameraPrefab路徑.
	/// </summary>
	//public const string CameraModelPath = "Model/Main Camera";

	/// <summary>
	/// 使用物品特效Prefab.
	/// </summary>
	//public const string UseItemEffectPath = "Effect/UseItemEffect";

	//public const string AnimationClipPath = "AnimationClip/";

	#endregion

    #endregion

    #region enum

    #region UI、Module、Scene enum

    /// <summary>
    /// Moudle註冊方式.
    /// </summary>
    public enum RegisterMode
	{
		NotRegister,
		AutoRegister,
		AlereadyRegister
	}

	/// <summary>
	/// 角色職業.
	/// </summary>
	public enum Profession
	{
		/// <summary>
		/// 戰士.
		/// </summary>
		Warrior = 0,
		/// <summary>
		/// 法師.
		/// </summary>
		Enchanter = 1
	}

	/// <summary>
	/// UI面板類型.
	/// </summary>
	public enum UIType
	{
        UserPanel,

		MainPanel,

		LoadingPanel,

		None,
		LoginPanel
	}

	/// <summary>
	/// 角色(演員)類型.
	/// </summary>
	public enum ActorType
	{
		None = 0,
		/// <summary>
		/// 角色.
		/// </summary>
		Role,
		/// <summary>
		/// 敵人(怪物).
		/// </summary>
		Monster,
		/// <summary>
		/// NPC.
		/// </summary>
		NPC
	}

	/// <summary>
	/// UI狀態.
	/// </summary>
	public enum UIState
	{
		None,
		Initial,
		Loading,
		Readly,
		Closing,
		Closed
	}

	/// <summary>
	/// Module狀態.
	/// </summary>
	public enum ModuleState
	{
		None,
		Initial,
		Loading,
		Readly,
		Closing,
		Closed
	}

	/// <summary>
	/// 場景類型.
	/// </summary>
	public enum SceneType
	{
		None = 0,
		InitScene,

		/// <summary>
		/// 開始場景
		/// </summary>
		StartScene,

		CommonPlayScene,

		CommonTimerPlayScene,

		/// <summary>
		/// 衣櫃場景
		/// </summary>
		ChestScene
	}

	/// <summary>
	/// 屬性類型.
	/// </summary>
	public enum PropertyType : int
	{
		/// <summary>
		/// 角色名稱.
		/// </summary>
		RoleName = 1,
		/// <summary>
		/// 性別.
		/// </summary>
		Gender,
		/// <summary>
		/// RoleID
		/// </summary>
		RoleID,
		/// <summary>
		/// 寶石.
		/// </summary>
		Gem,
		/// <summary>
		/// 錢幣.
		/// </summary>
		Coin,
		Level,
		Exp,

		AttackSpeed,
		HP,
		HPMax,
		Attack,
		WaterAbility,
		FireAbility
	}

	public enum ValueOperation
	{
		Plus,
		Substract,
		DirectChange,
		None
	}

	#endregion

	#region data enum

	/// <summary>
	/// 角色職業.
	/// </summary>
	public enum RoleProfession
	{
		//戰士
		Warrior = 0,
		//法師
		Mage,
		//職業數量
		Count

	}

	/// <summary>
	/// 角色種族.
	/// </summary>
	public enum RoleRace
	{
		//人類
		Human = 0,
		//精靈
		Spirit,
		//種族數量
		Count
	}

	/// <summary>
	/// 播放聲音類型.
	/// </summary>
	public enum AudioType
	{
		/// <summary>
		/// 背景音樂.
		/// </summary>
		BGM = 0,
		/// <summary>
		/// 音效.
		/// </summary>
		SE,
		Other
	}

	/// <summary>
	/// 物品類型.
	/// </summary>
	public enum ItemType
	{
		equipment,
		edicine,
		task
	}

	/// <summary>
	/// 監聽按鍵類型
	/// </summary>
	public enum EnumTouchEventType
	{
		OnClick,
		OnDoubleClick,
		OnDown,
		OnUp,
		OnEnter,
		OnExit,
		OnSelect,
		OnUpdateSelect,
		OnDeSelect,
		OnDrag,
		OnDragEnd,
		OnDrop,
		OnScroll,
		OnMove,
	}

	#endregion

	#region message enum

	public enum BuyItemResult
	{
		/// <summary>
		/// 成功
		/// </summary>
		Succsess,

		/// <summary>
		/// 櫻花不足
		/// </summary>
		Insufficient,

		/// <summary>
		/// 重複購買
		/// </summary>
		reapplied,

		/// <summary>
		/// 未知錯誤
		/// </summary>
		Error,


	}

	public enum ChangeDressResult
	{
		Success,
		Faild
	}

	#endregion


	#endregion

	#region delegate

	/// <summary>
	/// UI狀態改變委派.
	/// </summary>
	/// <param name="UIPanel"></param>
	/// <param name="previousState"></param>
	/// <param name="currentState"></param>
    public delegate void UIStateChangeHandler (AbsUIPanel UIPanel, UIState previousState, UIState currentState);

	/// <summary>
	/// Moudle狀態改變委派.
	/// </summary>
	/// <param name="moudle"></param>
	/// <param name="previousState"></param>
	/// <param name="currentState"></param>
    public delegate void MoudleStateChangeHandler (AbsModule moudle, ModuleState previousState, ModuleState currentState);

	/// <summary>
	/// 開啟協程委派.
	/// </summary>
	/// <param name="coroutineHandler"></param>
    public delegate void CoroutineStartedHandler (Coroutine coroutine);

	/// <summary>
	/// 消息中心委派.
	/// </summary>
	/// <param name="message"></param>
    public delegate void MessageEvent (Message message);

	/// <summary>
	/// 屬性改變委派.
	/// </summary>
	/// <param name="actor">屬性被改變的actor</param>
	/// <param name="id">屬性的ID</param>
	/// <param name="oldValue">屬性舊的值</param>
	/// <param name="newValue">屬性新的值</param>
    public delegate void PropertyChangedHandler (AbsActor actor, int id, object oldValue, object newValue);

	/// <summary>
	/// 按鍵、觸摸觸發事件委託
	/// </summary>
	/// <param name="listener"></param>
	/// <param name="arg"></param>
	/// <param name="handleParams"></param>
    public delegate void OnTouchEventHandler (GameObject listener, object arg, object[] handleParams);

    /// <summary>
	/// 按鍵、觸摸觸發事件委託(無傳入參數)
	/// </summary>
    public delegate void OnTouchSingleEventHandler ();

	#endregion

	#region inner class

	/// <summary>
	/// UI相關定義.
	/// </summary>
	public class UI
	{
		/// <summary>
		/// Resources目錄下UI所在路徑
		/// </summary>
		public static string ResourceUIPath = "UI/";

		/// <summary>
		/// Resources目錄下SubUI所在路徑
		/// </summary>
		public static string ResourcesSubUIPath = ResourceUIPath + "SubUI/";

		/// <summary>
		/// Resources下 UI sprite 所在文件夾
		/// </summary>
		public static string ResourcesSpritePath = "Image/";

		/// <summary>
		/// 取得UI Prefab路徑.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string GetUIPath (UIType type)
		{
			StringBuilder builder = new StringBuilder ();
			builder.Append ("UI/");
			builder.Append (type.ToString ());

			return builder.ToString ();
		}

		/// <summary>
		/// 使用enum取得UI的Type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static Type GetUIType (UIType type)
		{
			//  將enum ToString()後，使用反射機制查詢相對應的類名，並將該類的Type return
			Type t = Type.GetType (type.ToString ());
			return t;

			#region 廢棄
			//switch (type)
			//{
			//    //case UIType.LoginPanel:
			//    //    return typeof(LoginPanel);
			//    case UIType.InitUI:
			//        return typeof(InitUI);

			//    case UIType.StartMenuUI:
			//        return typeof(StartMenuUI);

			//    case UIType.CommonPlayUI:
			//        return typeof(CommonPlayUI);

			//    case UIType.BillingUI:
			//        return typeof(BillingUI);

			//    case UIType.SettingUI:
			//        return typeof(SettingUI);

			//    case UIType.KanaUI:
			//        return typeof(KanaUI);

			//    case UIType.CommonPlayTimerUI:
			//        return typeof(CommonPlayTimerUI);

			//    case UIType.GirlUI:
			//        return typeof(GirlUI);

			//    case UIType.NewStartMenuUI:
			//        return typeof(NewStartMenuUI);

			//    case UIType.ChestUI:
			//        return typeof(ChestUI);

			//    default:
			//        return null;
			//}
			#endregion
		}
	}

	public class Scene
	{
		/// <summary>
		/// 使用enum取得Scene的Type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static Type GetSceneType (SceneType type)
		{
			return Type.GetType (type.ToString ());

			#region old getSceneType

			//switch (type)
			//{
			//    case SceneType.InitScene:
			//        return typeof(InitScene);

			//    case SceneType.StartScene:
			//        return typeof(StartScene);

			//    case SceneType.CommonPlayScene:
			//        return typeof(CommonPlayScene);

			//    case SceneType.CommonPlayTimerScene:
			//        return typeof(CommonTimerPlayScene);

			//    default:
			//        Debug.LogError
			//            ("Try get a inexistence scene!! Go to Scripts/YsUIFrameWork/Common/Define to Modify GetSceneType( ) ");
			//        return null;
			//}

			#endregion

		}

		public static string GetSceneName (SceneType type)
		{
			return type.ToString ();

			#region old getSceneName

			//switch (type)
			//{
			//    case SceneType.None:
			//        return null;

			//    case SceneType.InitScene:
			//        return "00_Init";

			//    case SceneType.StartScene:
			//        return "01_Start";

			//    case SceneType.CommonPlayScene:
			//        return "03_CommonPlay";

			//    case SceneType.CommonPlayTimerScene:
			//        return "04_CommonTimerPlay";

			//    default:
			//        return null;
			//}

			#endregion

		}
	}


	public class RoleInfo
	{
		//角色id
		public int Uid;
		//角色名稱
		public string Name;
		//角色等級
		public int Lv;
		//角色職業
		public RoleProfession Pro;
		//角色種族
		public RoleRace Race;

	}

	public class Audio
	{
		private string path;

		/// <summary>
		/// 音頻名稱
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 音頻源
		/// </summary>
		public AudioClip Source { get; set; }

		/// <summary>
		/// 是否是默認的路徑
		/// </summary>
		public bool IsDefaultPath { get; protected set; }

		/// <summary>
		/// 音頻路徑
		/// </summary>
		public string Path {
			get {
				if (IsDefaultPath)
					return Define.ResourcesAudioPath;
				else
					return path;
			}

			protected set {
				path = value;
			}
		}

		/// <summary>
		/// Resources下的音效路徑.
		/// </summary>
		public static string AudioPath { get { return Define.ResourcesAudioPath; } }

		/// <summary>
		/// 按鈕聲路徑.
		/// </summary>
		public const string BtnSoundPath = "Audio/button";

		public Audio ()
		{
		}

		public Audio (string name, AudioClip source)
		{
			this.Name = name;
			this.Source = source;
			this.IsDefaultPath = true;
			this.Path = null;
		}

		public Audio (string name, AudioClip source, string path)
		{
			this.Name = name;
			this.Source = source;
			this.IsDefaultPath = false;
			this.Path = path;
		}

		/// <summary>
		/// 取得各個場景的音效.
		/// </summary>
		/// <param name="sceneType"></param>
		/// <returns></returns>
		public static string GetSceneAudioName (Define.SceneType sceneType)
		{
			switch (sceneType) {
			case SceneType.None:
				return null;

			case SceneType.InitScene:
				return null;

			case SceneType.StartScene:
				return "BGM01";

			case SceneType.CommonPlayScene:
				return "BGM02";

			case SceneType.CommonTimerPlayScene:
				return "BGM02";

			default:
				return null;
			}
		}

		public static void Play2D (Audio audio)
		{
			AudioSource.PlayClipAtPoint (audio.Source, Vector3.zero);
		}

		public static void Play3DAtPoint (GameObject gameObject, Audio audio)
		{
			AudioSource.PlayClipAtPoint (audio.Source, gameObject.transform.position);
		}
	}

	/// <summary>
	/// Audio名稱
	/// </summary>
	public class AudioName
	{
		public const string ButtonClick = "buttonClick";
		public const string BGM01 = "BGM01";
		public const string BGM02 = "BGM02";
		public const string Currect = "correct";
		public const string Error = "error";
		public const string Tick = "tick";
		public const string ChangePage = "changePage";
	}

	/// <summary>
	/// 定義Message名稱.
	/// </summary>
	public class MessageName
	{

        public const string UI_LoadingPanelClosed = "UI_LoadingPanelClosed";

        public const string OpenUserUI = "OpenUserUI";

        public const string OnLogin = "OnLogin";

        public const string Logout = "Logout";

        public const string Net_LogoutCallback = "Net_LogoutCallback";

        public const string Net_GetUserData = "Net_GetUserData";

        public const string Net_GetUserDataCallback = "Net_GetUserDataCallback";

        public const string Data_LoadingCompleted = "Data_LoadingCompleted";

		/// <summary>
		/// 更新玩家UI的值
		/// </summary>
		public const string RefreshPlayerUI = "RefreshPlayerUI";

		/// <summary>
		/// 玩家的資料數值改變消息
		/// </summary>
		public const string OnPlayerDataChanged = "OnPlayerValueChanged";

		/// <summary>
		/// 改變櫻花
		/// </summary>
		public const string ChangeSakura = "ChangeSakura";

		/// <summary>
		/// 開頭動畫撥放完畢
		/// </summary>
		public const string OnTitlePlayed = "OnTitlePlayed";

		/// <summary>
		/// 決定遊玩一般模式
		/// </summary>
		public const string OnDecideCommonPlay = "OnDecideCommonPlay";

		public const string OnDecideCommonTimerPlay = "OnDecideCommonTimerPlay";

		public const string OnExitPlayScene = "OnExitPlayScene";
		/// <summary>
		/// 選擇角色結果(伺服器下發).
		/// </summary>
		public const string SelectRoleResult = "SelectRoleResponse";

		public const string GetRoleInfoResult = "GetRoleInfoListResquest";

		/// <summary>
		/// 更換服裝消息
		/// </summary>
		public const string ChangeDress = "ChangeDress";

		/// <summary>
		/// 更換完畢服裝消息
		/// </summary>
		public const string OnChangeDress = "OnChangeDress";

		/// <summary>
		/// 成功購買物品消息.
		/// </summary>
		public const string BuyItem = "BuyItem";

		/// <summary>
		/// 購買物品結果
		/// </summary>
		public const string BuyItemResult = "BuyItemResult";

		/// <summary>
		/// 更新玩家資源、資產消息.
		/// </summary>
		public const string RefreshAsset = "RefreshAsset";

		/// <summary>
		/// 使用物品.
		/// </summary>
		public const string UseItem = "UseItem";

		/// <summary>
		/// 更新玩家狀態.
		/// </summary>
		public const string RefreshPlayerState = "RefreshPlayerState";

		/// <summary>
		/// 玩家死亡.
		/// </summary>
		public const string PlayerDie = "PlayerDie";

		/// <summary>
		/// 玩家受傷.
		/// </summary>
		public const string PlayerDamage = "PlayerDamage";

		/// <summary>
		/// 玩家治癒.
		/// </summary>
		public const string PlayerCure = "PlayerCure";

		/// <summary>
		/// 遊戲結束
		/// </summary>
		public const string GameOver = "GameOver";

		/// <summary>
		/// 增加親密度
		/// </summary>
		public const string IncreaseIntimacy = "IncreaseIntimacy";
	}

	public class MessageDicName
	{
		public const string ValueOperation = "ValueOperation";
	}

	/// <summary>
	/// Actor名稱.
	/// </summary>
	public class ActorName
	{
		public const string PlayerMainTown = "Player_MainTown";
	}

	/// <summary>
	/// 傳送至MessageBox的參數.
	/// </summary>
	public class MessageBoxPara
	{
		public string Message { get; set; }

		/// <summary>
		/// 是否可以點擊按鈕.
		/// </summary>
		public bool CanClick { get; set; }

		public Action<bool> CallBack { get; set; }

		public MessageBoxPara ()
		{
		}

		public MessageBoxPara (string message, Action<bool> callBack, bool canClick = true)
		{
			Message = message;
			CallBack = callBack;
			CanClick = canClick;
		}
	}


	/// <summary>
	/// Tip面板初始化時傳入的參數.
	/// </summary>
	public class TipPanelPara
	{

		public TipPanelPara ()
		{
		}

		public TipPanelPara (string tip, Vector3 position, int price)
		{
			this.Text = tip;
			Position = position;
			this.Price = price;
		}

		/// <summary>
		/// 顯示的文字.
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// 顯示的位置.
		/// </summary>
		public Vector2 Position { get; set; }

		/// <summary>
		/// 價格.
		/// </summary>
		public int Price { get; set; }
	}

	public struct Setting
	{
		public static readonly Vector2 Camera_Offset = new Vector2 (0, 20);
	}

	public class LoadingPanelParameter
	{
		public string EventName{ get; set; }

		public UIType OpenUIType{ get; set; }

        public Func<Message,bool> OnFinishAction{ get; set;}
	}

	#region Extra class/struct

	public class PlayerValue
	{
		public int SakuraCount { get; set; }
	}

	public class LocalRecordName
	{
		public const string GamePlayTime = "GamePlayTime";
	}

	/// <summary>
	/// 假名的羅馬拼音念法
	/// </summary>
	public class KanaRomanPronunciation
	{
        
	}

	#endregion

	#endregion

}
