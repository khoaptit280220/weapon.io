#region

using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;

#endif

#endregion

[ShowOdinSerializedPropertiesInInspector]
public class ConfigManager : Singleton<ConfigManager>, ISerializationCallbackReceiver, ISupportsPrefabSerialization
{
    public SoundConfig audioCfg;
    public GameConfig gameCfg;
    public UiConfig uiConfig;
    public IapConfig iapConfig;
    public MapConfig mapConfig;
    public ModelSkinConfig modelSkinConfig;
    public ModelHornConfig modelHornConfig;
    public ItemConfig itemConfig;
    public AnimConfig animConfig;
    public CountryConfig countryConfig;

    private void Start()
    {
        mapConfig.UnlockMapDefault();
        modelSkinConfig.UnlockModelSkinDefault();
        modelHornConfig.UnlockModelHornDefault();
        itemConfig.InitItem();
    }


    #region Odin

#if UNITY_EDITOR
    [Button]
    private void LoadConfigs()
    {
        audioCfg = HCTools.GetSoundConfig();
        gameCfg = HCTools.GetGameConfig();
        uiConfig = HCTools.GetUiConfig();
        iapConfig = HCTools.GetIapConfig();
        mapConfig = HCTools.GetMapConfig();
        modelSkinConfig = HCTools.GetConfig<ModelSkinConfig>("Assets/Configs/");
        modelHornConfig = HCTools.GetConfig<ModelHornConfig>("Assets/Configs/");
        itemConfig = HCTools.GetConfig<ItemConfig>("Assets/Configs/");
        animConfig = HCTools.GetConfig<AnimConfig>("Assets/Configs/");
        countryConfig = HCTools.GetConfig<CountryConfig>("Assets/Configs/");
    }
#endif

    [SerializeField] [HideInInspector] private SerializationData serializationData;

    SerializationData ISupportsPrefabSerialization.SerializationData
    {
        get => serializationData;
        set => serializationData = value;
    }

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        UnitySerializationUtility.DeserializeUnityObject(this, ref serializationData);
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        UnitySerializationUtility.SerializeUnityObject(this, ref serializationData);
    }

    #endregion
}