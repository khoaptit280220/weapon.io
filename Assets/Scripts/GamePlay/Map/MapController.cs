using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [ReadOnly] public MapData currentMapData;

    public List<Map> listMaps = new List<Map>();

    private void SetupMapDefault()
    {
        listMaps.ForEach(item=>item.gameObject.SetActive(false));
    }

    public void SetupMap()
    {
        currentMapData = ConfigManager.Instance.mapConfig.GetMapDataById(Database.CurrentIdMap);
        SetupMapDefault();
        foreach (var VARIABLE in listMaps)
        {
            if (VARIABLE.typeMap == currentMapData.typeMap)
            {
                VARIABLE.gameObject.SetActive(true);
            }
        }
    }
    
    // public void SetupLogicMap()
    // {
    //     switch (currentMapData.typeMap)
    //     {
    //         case TypeMap.BlackPearlRuin:
    //             
    //             break;
    //         case TypeMap.AtlantisCity:
    //            
    //             break;
    //         case TypeMap.SnowWonderland:
    //             
    //             break;
    //         case TypeMap.LostSector:
    //             
    //             break;
    //         case TypeMap.TwilightShrine:
    //            
    //             break;
    //         case TypeMap.MidnightRift:
    //           
    //             break;
    //         case TypeMap.DreamTowers:
    //             
    //             break;
    //     }
    // }
    
}
