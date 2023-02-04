#region

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

#endregion

[Serializable]
public class GameData
{
    public SettingData setting = new SettingData();
    public UserData user = new UserData();

    [Serializable]
    public class UserData
    {
        public DateTime lastTimeLogOut = DateTime.Now;
        
        public int level = 1;

        //Progress Data
        public int money;
        public int score;
        public string name = "Player";
        public int luckyWheelProgress;
        public DateTime lastFreeSpinTime = DateTime.MinValue;
        public int dailyRewardClaimedCount;
        public DateTime lastDailyRewardClaimTime = DateTime.MinValue;

        //Purchase Data
        public bool purchasedNoAds;
        public bool rated;

        //Leaderboard
        public int totalScoreOffset;

        //Other Data
        public int sessionPlayed;
        private int _keyCount;

        public int KeyCount
        {
            get => _keyCount;
            set => _keyCount = Mathf.Clamp(value, 0, 3);
        }
    }

    [Serializable]
    public class SettingData
    {
        public Dictionary<PushNotificationType, int> androidPnIndexes = new Dictionary<PushNotificationType, int>();

        public bool enablePn;
        public bool requestedPn;

        public bool haptic = true;
        public float soundVolume = 1;
        public float musicVolume = 1;
        
        public int highPerformance = 1;

        public bool iOsTrackingRequested;
    }
}

public static partial class Database
{
    private static string dataKey = "GameData";

    public static void SaveData()
    {
        var dataString = JsonConvert.SerializeObject(GameManager.Instance.data);
        PlayerPrefs.SetString(dataKey, dataString);
        PlayerPrefs.Save();
    }

    public static GameData LoadData()
    {
        if (!PlayerPrefs.HasKey(dataKey))
            return null;

        return JsonConvert.DeserializeObject<GameData>(PlayerPrefs.GetString(dataKey));
    }

    public static bool IsMapUnlock
    {
        get => GetBool(IS_CHECK_UNLOCK_MAP,false);
        set => SetBool(IS_CHECK_UNLOCK_MAP, value);
    }

    public static int CurrentIdMap
    {
        get => GetInt(CURRENT_MAP_ID, 1);
        set => SetInt(CURRENT_MAP_ID, value);
    }

    #region KEY

    public static string IS_CHECK_UNLOCK_MAP = "";
    public static string CURRENT_MAP_ID = "CURRENT_MAP_ID";

    #endregion

}
public static partial class Database
{
    private static bool GetBool(string key, bool defaultValue = false) =>
        PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) > 0;

    private static void SetBool(string id, bool value) => PlayerPrefs.SetInt(id, value ? 1 : 0);

    private static int GetInt(string key, int defaultValue) => PlayerPrefs.GetInt(key, defaultValue);
    private static void SetInt(string id, int value) => PlayerPrefs.SetInt(id, value);

    private static string GetString(string key, string defaultValue) => PlayerPrefs.GetString(key, defaultValue);
    private static void SetString(string id, string value) => PlayerPrefs.SetString(id, value);
}
