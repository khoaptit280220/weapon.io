#region

using System;
using AppsFlyerSDK;
using UnityEngine;

#endregion

public partial class MaxMediationController : MonoBehaviour, IMediationController
{
    private const string MaxSdkKey = "7PspscCcbGd6ohttmPcZTwGmZCihCW-Jwr7nSJN2a_9Mg0ERPs0tmGdKTK1gs__nr6XHQvK0vTNaTb1uR1mCIN";

    private string _bannerAdUnitId;
    private string _interstitialAdUnitId;
    private string _rewardedAdUnitId;

    public TypeAdsMax typeAdsUse;

    public void Init()
    {
        _interstitialAdUnitId = GameManager.Instance.gameSetting.interAd;
        _rewardedAdUnitId = GameManager.Instance.gameSetting.rewardedAd;
        _bannerAdUnitId = GameManager.Instance.gameSetting.bannerAd;

        MaxSdkCallbacks.OnSdkInitializedEvent += OnInitSuccess;

        MaxSdk.SetSdkKey(MaxSdkKey);
        MaxSdk.SetUserId(AppsFlyer.getAppsFlyerId());
        MaxSdk.InitializeSdk();
    }

    private void OnInitSuccess(MaxSdkBase.SdkConfiguration sdkConfiguration)
    {
        RegisterRevenuePaidCallback();

        Debug.Log("MAX Sdk Initialized Successfully!");

        AppOpenAdManager.Instance.Init();
        
        if (typeAdsUse.HasFlag(TypeAdsMax.Inter))
            InitInterstitialAds();

        if (typeAdsUse.HasFlag(TypeAdsMax.Reward))
            InitRewardedAds();

        if (typeAdsUse.HasFlag(TypeAdsMax.Banner))
            InitBannerAds();

        //MaxSdk.ShowMediationDebugger();
    }

    public void OnStartWatchFullscreenAds()
    {
        AdManager.Instance.IsWatchingFullscreenAds = true;
        if (AdManager.Instance.onStartFullscreenAds != null)
            AdManager.Instance.onStartFullscreenAds.Invoke();
        Debug.Log("MAX > START WATCHING ADS");
    }

    public void OnFinishWatchFullscreenAds()
    {
        AdManager.Instance.IsWatchingFullscreenAds = false;
        if (AdManager.Instance.onFinishFullscreenAds != null)
            AdManager.Instance.onFinishFullscreenAds.Invoke();
        Debug.Log("MAX > FINISH WATCHING ADS");
    }

    public void OnGetReward()
    {
        if (AdManager.Instance.onGetRewardedAds != null)
            AdManager.Instance.onGetRewardedAds.Invoke();
    }

    #region Interstitial Ad Methods

    private int _interstitialRetryAttempt;

    private void InitInterstitialAds()
    {
        // Attach callbacks
        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialFailedToDisplayEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialDismissedEvent;
        MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
        // Load the first interstitial
        //LoadInterstitial();
    }

    public void LoadInterstitial()
    {
        MaxSdk.LoadInterstitial(_interstitialAdUnitId);
    }

    public void ShowInterstitial(string placement)
    {
        if (IsInterstitialLoaded())
            MaxSdk.ShowInterstitial(_interstitialAdUnitId, AdManager.VerifyInterAdPlacement(placement));
    }

    public bool IsInterstitialLoaded()
    {
        return MaxSdk.IsInterstitialReady(_interstitialAdUnitId);
    }

    private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Reset retry attempt
        _interstitialRetryAttempt = 0;

        Debug.Log("MAX > Interstitial Ad ready.");
    }

    private void OnInterstitialFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo error)
    {
        // Interstitial ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).
        _interstitialRetryAttempt++;
        var retryDelay = 2 * Math.Min(6, _interstitialRetryAttempt);

        Invoke(nameof(LoadInterstitial), retryDelay);

        Debug.Log("MAX > Interstitial ad failed to load with error code: " + error.Code);
    }

    private void OnInterstitialFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad failed to display. We recommend loading the next ad
        LoadInterstitial();
        Debug.Log("MAX > Interstitial failed to display with error code: " + errorInfo.Message);
    }

    private void OnInterstitialDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is hidden. Pre-load the next ad
        LoadInterstitial();
        OnFinishWatchFullscreenAds();
    }

    private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        AnalyticManager.AppflyerLogAdsClicked(AdsType.Interstitial, adInfo.Placement);
    }

    private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        AnalyticManager.AppflyerLogAdsImpression(AdsType.Interstitial, adInfo.Placement);
    }

    #endregion

    #region Rewarded Ad Methods

    private int _rewardedRetryAttempt;

    private void InitRewardedAds()
    {
        // Attach callbacks
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdDismissedEvent;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

        // Load the first RewardedAd
        //LoadRewardedAd();
    }

    public void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(_rewardedAdUnitId);
    }

    public bool IsRewardedAdLoaded()
    {
        return MaxSdk.IsRewardedAdReady(_rewardedAdUnitId);
    }

    public void ShowRewardedAd(string placement)
    {
        if (IsRewardedAdLoaded())
            MaxSdk.ShowRewardedAd(_rewardedAdUnitId, AdManager.VerifyRewardAdPlacement(placement));
    }

    private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is ready to be shown. MaxSdk.IsRewardedAdReady(rewardedAdUnitId) will now return 'true'

        // Reset retry attempt
        _rewardedRetryAttempt = 0;

        Debug.Log("MAX > Rewarded Ad ready.");
    }

    private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Rewarded ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).
        _rewardedRetryAttempt++;
        var retryDelay = 2 * Math.Min(6, _rewardedRetryAttempt);

        Invoke(nameof(LoadRewardedAd), retryDelay);

        Debug.Log("MAX > Rewarded ad failed to load with error code: " + errorInfo.Code);
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad failed to display. We recommend loading the next ad
        LoadRewardedAd();

        Debug.Log("MAX > Rewarded ad failed to display with error code: " + errorInfo.Code);
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        AnalyticManager.AppflyerLogAdsImpression(AdsType.Rewarded, adInfo.Placement);
    }

    private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        AnalyticManager.AppflyerLogAdsClicked(AdsType.Rewarded, adInfo.Placement);
    }

    private void OnRewardedAdDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        LoadRewardedAd();
        OnFinishWatchFullscreenAds();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdkBase.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad was displayed and user should receive the reward
        OnGetReward();
    }

    #endregion

    #region Banner Ad Methods

    private bool _isBannerLoaded = true;

    private void InitBannerAds()
    {
        // Banners are automatically sized to 320x50 on phones and 728x90 on tablets.
        // You may use the utility method `MaxSdkUtils.isTablet()` to help with view sizing adjustments.
        MaxSdk.CreateBanner(_bannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);
        MaxSdk.SetBannerExtraParameter(_bannerAdUnitId, "adaptive_banner", "true");
        MaxSdk.SetBannerPlacement(_bannerAdUnitId, "Banner");
        // Set background or background color for banners to be fully functional.
        MaxSdk.SetBannerBackgroundColor(_bannerAdUnitId, Color.clear);

        AdManager.Instance.ShowBanner();
    }

    public bool IsBannerAdLoaded()
    {
        return _isBannerLoaded;
    }

    private void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        Debug.Log("MAX > Banner Ad ready.");
    }

    private void OnBannerAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        Debug.Log("MAX > Banner ad failed to load with error code: " + errorInfo.Code);
    }

    public void ShowBanner()
    {
        MaxSdk.ShowBanner(_bannerAdUnitId);
    }

    public void HideBanner()
    {
        MaxSdk.HideBanner(_bannerAdUnitId);
    }

    #endregion
}

[Flags]
public enum TypeAdsMax
{
    Inter = 1 << 0,
    Banner = 1 << 1,
    Reward = 1 << 2
}