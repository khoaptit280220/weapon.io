#region

using System;
using UnityEngine;

#endregion

public class AppOpenAdManager : Singleton<AppOpenAdManager>
{
    private static AppOpenAdManager _instance;

    public static bool shouldShowOpenAds = true;
    public static bool shouldShowResumeAds = true;
    public static bool showedFirstOpenAd;
    public static int tryGetAoaTime = -1;

    private bool _isShowingAoa;
    private DateTime _loadTime;

    private bool _isFirstLoad = true;
    
    private string AppOpenAdUnitId => GameManager.Instance.gameSetting.aoa;

    private bool IsAdAvailable => MaxSdk.IsAppOpenAdReady(AppOpenAdUnitId);

    public void Init()
    {
        MaxSdkCallbacks.AppOpen.OnAdDisplayedEvent += OnAppOpenDisplayedEvent;
        MaxSdkCallbacks.AppOpen.OnAdHiddenEvent += OnAppOpenDismissedEvent;
        MaxSdkCallbacks.AppOpen.OnAdLoadedEvent += OnAppOpenLoadedEvent;
        MaxSdkCallbacks.AppOpen.OnAdDisplayFailedEvent += OnAppOpenDisplayFailedEvent;
        MaxSdkCallbacks.AppOpen.OnAdLoadFailedEvent += OnAppOpenLoadFailedEvent;

        LoadAd();
    }
    
    public void ShowAdIfReady()
    {
        if (!shouldShowOpenAds ||GameManager.Instance && GameManager.Instance.gameInited && !GameManager.EnableAds ||
            AdManager.Instance.IsWatchingFullscreenAds || !MaxSdk.IsInitialized())
            return;

        if (IsAdAvailable && !_isShowingAoa)
        {
            MaxSdk.ShowAppOpenAd(AppOpenAdUnitId);
        }
        else
        {
            LoadAd();
        }
    }
    
    public void LoadAd() => MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
    
    private void OnAppOpenDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        _isShowingAoa = true;
        showedFirstOpenAd = true;
        Debug.Log("Show AOA!");
    }

    private void OnAppOpenDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        _isShowingAoa = false;
        LoadAd();
        Debug.Log("Hide AOA!");
    }

    private void OnAppOpenLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        Debug.Log("Aoa loaded successfully, ID: " + AppOpenAdUnitId);

        if (_isFirstLoad)
        {
            _isFirstLoad = false;
            ShowAdIfReady();
            AdManager.Instance.LoadAllOriginAds();
        }
    }

    private void OnAppOpenDisplayFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        LoadAd();
    }

    private void OnAppOpenLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        if (_isFirstLoad)
        {
            _isFirstLoad = false;
            AdManager.Instance.LoadAllOriginAds();
        }

        if (tryGetAoaTime > 0)
        {
            Invoke(nameof(LoadAd), tryGetAoaTime);
            Debug.Log("Load AOA Failed. Retry after " + tryGetAoaTime + " seconds");
        }
        else
        {
            Debug.Log("Load AOA Failed.");
        }
    }

    void ShowResumeAppAd()
    {
        if (!shouldShowResumeAds)
            return;
        
        ShowAdIfReady();
    }
    
    private void OnApplicationPause(bool pause)
    {
        if (!pause)
            ShowResumeAppAd();
    }
}