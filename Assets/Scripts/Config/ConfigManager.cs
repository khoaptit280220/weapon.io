﻿#region

using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
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

    private void Start()
    {
        mapConfig.UnlockMapDefault();
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
    }
#endif

    [SerializeField]
    [HideInInspector]
    private SerializationData serializationData;

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