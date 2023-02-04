using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MapConfig", menuName = "Configs/MapConfig")]
public class MapConfig : ScriptableObject
{
    public List<MapData> ListMapDatas = new List<MapData>();

    public void UnlockMapDefault()
    {
        GetMapDataByTypeMap(TypeMap.BlackPearlRuin).IsUnlock = true;
    }
    public MapData GetMapDataByTypeMap(TypeMap _typeMap)
    {
        foreach (var VARIABLE in ListMapDatas)
        {
            if (VARIABLE.typeMap == _typeMap)
            {
                return VARIABLE;
            }
        }

        return null;
    }
    public MapData GetMapDataById(int idmap)
    {
        foreach (var VARIABLE in ListMapDatas)
        {
            if (VARIABLE.idMap == idmap)
            {
                return VARIABLE;
            }
        }

        return null;
    }
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
    public int idMap;
    public TypeMap typeMap;
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
