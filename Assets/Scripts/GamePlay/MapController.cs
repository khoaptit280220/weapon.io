using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public TypeMap typeMap;

    public bool isMap1 = false;
    public bool isMap2 = false;
    public bool isMap3 = false;
    public bool isMap4 = false;
    public bool isMap5 = false;
    public bool isMap6 = false;
    public bool isMap7 = false;

    private void Start()
    {
        GameManager.Instance.SetupMapController(this);
        Map();
    }

    public void Map()
    {
        switch (typeMap)
        {
            case TypeMap.BlackPearlRuin:
                isMap1 = true;
                break;
            case TypeMap.AtlantisCity:
                isMap2 = true;
                break;
            case TypeMap.SnowWonderland:
                isMap3 = true;
                break;
            case TypeMap.LostSector:
                isMap4 = true;
                break;
            case TypeMap.TwilightShrine:
                isMap5 = true;
                break;
            case TypeMap.MidnightRift:
                isMap6 = true;
                break;
            case TypeMap.DreamTowers:
                isMap7 = true;
                break;
        }
    }
}
