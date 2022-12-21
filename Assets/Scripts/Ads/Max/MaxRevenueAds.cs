#region

using UnityEngine;

#endregion

public partial class MaxMediationController 
{
    private void RegisterRevenuePaidCallback()
    {
        if (typeAdsUse.HasFlag(TypeAdsMax.Inter))
            MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnInterstitialAdRevenuePaidEvent;
        if (typeAdsUse.HasFlag(TypeAdsMax.Reward))
            MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
        if (typeAdsUse.HasFlag(TypeAdsMax.Banner))
            MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnBannerAdRevenuePaidEvent;
    }

    private ImpressionData GetImpressionData(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        var revenue = adInfo.Revenue;

        var countryCode = MaxSdk.GetSdkConfiguration().CountryCode;
        var networkName = adInfo.NetworkName;
        var adUnitIdentifier = adInfo.AdUnitIdentifier;
        var placement = adInfo.Placement;

        var data = new ImpressionData();
        data.AdUnitIdentifier = adUnitIdentifier;
        data.CountryCode = countryCode;
        data.NetworkName = networkName;
        data.Placement = placement;
        data.Revenue = revenue;

        return data;
    }

    private void OnInterstitialAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        var data = GetImpressionData(adUnitId, adInfo);
        data.AdFormat = "interstitial";

        AnalyticsRevenueAds.SendEvent(data, AdFormat.interstitial);
        Debug.Log("MAX > Inter Ad Revenue Paid");
    }

    private void OnBannerAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        var data = GetImpressionData(adUnitId, adInfo);
        data.AdFormat = "banner";

        AnalyticsRevenueAds.SendEvent(data, AdFormat.banner);
        Debug.Log("MAX > Banner Ad Revenue Paid");
    }

    private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        var data = GetImpressionData(adUnitId, adInfo);
        data.AdFormat = "video_reward";

        AnalyticsRevenueAds.SendEvent(data, AdFormat.video_rewarded);
        Debug.Log("MAX > Rewarded Ad Revenue Paid");
    }
}