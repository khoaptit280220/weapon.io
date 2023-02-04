using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private MapController mapController;

    public void SetupMap()
    {
        mapController = GetComponentInChildren<MapController>();
        mapController.SetupMap();
    }
}
