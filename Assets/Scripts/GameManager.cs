#region

#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif
using System;
using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif

#endregion

public class GameManager : Singleton<GameManager>
{
#if UNITY_EDITOR
    private bool IsPlaying => EditorApplication.isPlaying;
    public TypeMap currentTypeMap;

    [ShowIf(nameof(IsPlaying))]
#endif
    public GameState GameState = GameState.Prepare;

    public GameData data;
    public bool gameInited;
    public HCGameSetting gameSetting;

    private int _secondToRemindComeback;

    public GameObject previewShop;
    public GameObject previewAfterGame;
    public GameObject previewMain;

    [HideInInspector] public float lastClaimOnlineGiftTime;

    [HideInInspector] public int rank = 0;
    [HideInInspector] public int point = 0;
    [HideInInspector] public int kill = 0;
    [HideInInspector] public int coin = 0;
    [HideInInspector] public float energy = 0;
    [HideInInspector] public bool checkBoss = false;
    [HideInInspector] public int time;
    private PlayerController _playerController;
    private LevelController _levelController;

    private MapController _mapController;
    private ViewModelWeponController _viewModelWeponController;

    public ViewModelWeponController ViewModelWeponController
    {
        get => _viewModelWeponController;
        set
        {
            if (_viewModelWeponController != null)
            {
                Destroy(_viewModelWeponController);
            }

            _viewModelWeponController = value;
        }
    }

    public static bool EnableAds
    {
        get
        {
            if (RemoteConfigManager.Instance != null && !RemoteConfigManager.Instance.shouldShowAds)
                return false;

            if (Instance != null && Instance.data != null && Instance.data.user.purchasedNoAds)
                return false;

            return true;
        }
    }

    public static bool NetworkAvailable => Application.internetReachability != NetworkReachability.NotReachable;

    public string GameVersion => string.Format("{0}.{1}.{2}", gameSetting.gameVersion, gameSetting.bundleVersion,
        gameSetting.buildVersion);


#if UNITY_EDITOR
    [Button]
    private void GetGameSetting()
    {
        gameSetting = HCTools.GetGameSetting();
    }
#endif

    protected override void Awake()
    {
        base.Awake();

        LoadGameData();

        SetupPushNotification();

        RequestTrackingForiOs();

        GUIManager.Instance.Init();

        Instance.gameInited = true;

        GameServices.Instance.Init();

        EventGlobalManager.Instance.OnGameInited.Dispatch();

        LoadingManager.Instance.LoadScene(SceneIndex.Gameplay, MainScreen.Show);


        lastClaimOnlineGiftTime = Time.time;
    }

    private void Start()
    {
        AdManager.Instance.Init();
        EventGlobalManager.Instance.OnUpdateSetting.Dispatch();

        previewShop.SetActive(false);

        previewAfterGame.SetActive(false);
        previewMain.SetActive(false);

        if (data.user.checkRanGift == false)
        {
            Random();
        }
    }

    private void Random()
    {
        data.user.checkRanGift = true;
        var giftLevel = ConfigManager.Instance.itemConfig.GetRandomItemLevel();
        data.user.giftLevelID = giftLevel.id;
        data.user.giftLevelType = giftLevel.typeItem;

        var giftMain = ConfigManager.Instance.itemConfig.GetRandomItemAds();
        data.user.giftMainID = giftMain.id;
        data.user.giftMainType = giftMain.typeItem;

        var giftDaily = ConfigManager.Instance.itemConfig.GetRandomItemDaily();
        data.user.giftDailyID = giftDaily.id;
        data.user.giftDailyType = giftDaily.typeItem;

        var giftLucky = ConfigManager.Instance.itemConfig.GetRandomItemAds();
        data.user.giftLuckyID = giftLucky.id;
        data.user.giftLuckyType = giftLucky.typeItem;
    }

    [Button]
    public void AddMoney(int value)
    {
        data.user.money += value;
        EventGlobalManager.Instance.OnMoneyChange.Dispatch(true);
    }

    [Button]
    public bool SpendMoney(int value)
    {
        if (data.user.money >= value)
        {
            data.user.money -= value;
            EventGlobalManager.Instance.OnMoneyChange.Dispatch(true);
            return true;
        }

        EventGlobalManager.Instance.OnMoneyChange.Dispatch(false);
        return false;
    }

    private void LoadGameData()
    {
        data = Database.LoadData();
        if (data == null)
        {
            data = new GameData();

#if PROTOTYPE
            Data.User.PurchasedNoAds = true;
#endif
            Database.SaveData();
        }
    }

    private void SetupPushNotification()
    {
        _secondToRemindComeback = 60 * ConfigManager.Instance.gameCfg.maxOfflineRemindMinute + 30 * 60;

        if (data.setting.requestedPn)
            SetupRemindOfflinePushNotification();
        else
            PushNotificationManager.Instance.StartRequest();
    }

    private void RequestTrackingForiOs()
    {
#if UNITY_IOS
        if (!Data.Setting.iOSTrackingRequested)
        {
            ATTrackingStatusBinding.RequestAuthorizationTracking();
            Data.Setting.iOSTrackingRequested = true;
            Database.SaveData();
        }
#endif
    }

    private void UpdateGraphicSetting()
    {
        if (data.setting.highPerformance == 1)
        {
            Application.targetFrameRate = 60;
            Screen.SetResolution(Screen.width, Screen.height, true);
        }
        else
        {
            Application.targetFrameRate = 30;
            Screen.SetResolution(Screen.width / 2, Screen.height / 2, true);
        }
    }

    public override void OnApplicationQuit()
    {
        Logout();
        base.OnApplicationQuit();
    }

    public void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
            Logout();
    }

    public void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
            Logout();
    }

    private void Logout()
    {
        data.user.lastTimeLogOut = DateTime.Now;
        Database.SaveData();
    }

    private void OnEnable()
    {
        EventGlobalManager.Instance.OnUpdateSetting.AddListener(UpdateGraphicSetting);
    }

    private void OnDestroy()
    {
        if (!EventGlobalManager.Instance)
            return;

        EventGlobalManager.Instance.OnUpdateSetting.RemoveListener(UpdateGraphicSetting);
    }

    public void SetupRemindOfflinePushNotification()
    {
        PushNotificationManager.Instance.CancelNotification(PushNotificationType.RemindComeback);

        PushNotificationManager.Instance.ScheduleNotification(PushNotificationType.RemindComeback,
            "You have received a surprise gift. Claim it now!", _secondToRemindComeback);

        Database.SaveData();
    }

    public void SetupPlayer(PlayerController playerController)
    {
        if (_playerController != null)
        {
            Destroy(_playerController.gameObject);
        }

        this._playerController = playerController;
    }

    public PlayerController GetPlayer => _playerController;

    public void SetupLevelController(LevelController levelController)
    {
        this._levelController = levelController;
    }

    public LevelController GetLevelController => _levelController;

    public void SetupMapController(MapController mapController)
    {
        this._mapController = mapController;
    }

    public MapController GetMapController => _mapController;

    public void OnWinGame()
    {
        if (GameState != GameState.Lose)
        {
            PopupIngameWin.Show();
            GameState = GameState.Win;
            AddMoney(coin);

            //show popup win
        }
    }

    public void OnLoseGame()
    {
        if (GetPlayer.countDie == 0)
        {
            DOTween.Sequence().SetDelay(1).OnComplete(() => { PopupKilled.Show(); });
        }
        else
        {
            DOTween.Sequence().SetDelay(1).OnComplete(() => { PopupInGameLose.Show(); });
        }

        GameState = GameState.Lose;
        //show popup lose
    }

    public void BackHome()
    {
        PrepareGame();
    }

    public void PauseGame()
    {
        GameState = GameState.Pause;
        //pause play,....
    }

    public void PrepareGame()
    {
        GameState = GameState.Prepare;
        GetLevelController.GenerateLevel();
    }

    public void StartGame()
    {
        GameState = GameState.PLaying;
        GetLevelController.CurrentLevel.gameObject.SetActive(true);
        rank = 0;
        coin = 0;
        point = 0;
        kill = 0;
        time = 60;
        energy = 1;
    }
}

public enum GameState
{
    Prepare,
    PLaying,
    Win,
    Lose,
    Pause,
}