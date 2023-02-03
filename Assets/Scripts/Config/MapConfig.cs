using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MapConfig", menuName = "Configs/MapConfig")]
public class MapConfig : ScriptableObject
{
    public List<MapData> ListMapDatas = new List<MapData>();

    // public MapData GetMapDataByTypeMap(TypeMap typeMap)
    // {
    //     
    // }
}

public enum TypeMap
{
    BlackPearlRuin,
    AtlantisCity,
    SnowWonderland,
    LostSector,
    TwilightShrine,
    MidnightRift,
    DreamTowers,
}
[Serializable]
public class MapData
{
    public TypeMap typeMap;
    public Sprite imageBackground;

    public bool IsUnlock
    {
        get
        {
            Database.IS_CHECK_UNLOCK_MAP = typeMap.ToString();
            return Database.IsMapUnlock;
        }
        set
        {
            Database.IS_CHECK_UNLOCK_MAP = typeMap.ToString();
            Database.IsMapUnlock = value;
        }
    }
}
