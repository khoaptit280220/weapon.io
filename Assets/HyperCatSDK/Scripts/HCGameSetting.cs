using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "HcGameSetting", menuName = "HyperCat/Game Setting")]
public class HCGameSetting : ScriptableObject
{
    [BoxGroup("Identifier")]
    public string gameName = "Prototype";

    [BoxGroup("Identifier")]
    public string packageName = "com.hypercat.prototype";

    [HorizontalGroup("A", LabelWidth = 100), PropertySpace(10, 10)]
    public string gameVersion = "1.0";

    [HorizontalGroup("A"), PropertySpace(10, 10)]
    public int bundleVersion = 1;

    [HorizontalGroup("A"), PropertySpace(10, 10)]
    public int buildVersion = 0;

    [PropertySpace(10, 20), Header("Appstore ID (iOS Only)")]
    public string appstoreId;

    [BoxGroup("Ads IDs")]
    public string admobAndroidId;

    [BoxGroup("Ads IDs")]
    public string admobIosId;

#if UNITY_EDITOR
    [Button(ButtonSizes.Large, ButtonStyle.Box)]
    [BoxGroup("Ads IDs"), PropertySpace(10, 0), LabelText("Verify MAX adapters"), GUIColor(0.4f, 0.8f, 1)]
    public static void VerifyRequiredAdapter()
    {
        HCTools.CheckRequiredNetworks();
    }
#endif

    [Space]
    [BoxGroup("Ads IDs")]
    public string interAd;

    [BoxGroup("Ads IDs")]
    public string rewardedAd;

    [BoxGroup("Ads IDs")]
    public string bannerAd;
    
    [BoxGroup("Ads IDs"), Space]
    public string aoa;

#if UNITY_EDITOR
    [Button(ButtonSizes.Large, ButtonStyle.Box)]
    [BoxGroup("Ads IDs"), PropertySpace(10, 0), LabelText("Sync ids from HyperCat server"), GUIColor(0.4f, 0.8f, 1)]
    public static void SyncIdFromHyperCatServer()
    {
        HCTools.GetGameIdFromServer();
    }
#endif

    [Space, Header("Analytic")]
    public string firebaseEventPrefix = "hcPrototype";

    [Space, Header("Screen Orientation")]
    public ScreenOrientation orientation = ScreenOrientation.Portrait;

#if UNITY_EDITOR
    [Button]
    public static void SaveGameSetting()
    {
        HCTools.SaveGameSetting();
    }
#endif
}