using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private MapController mapController;
    private ModelSkinController modelSkinController;
    private ModelHornController modelHornController;

    public void SetupMap()
    {
        mapController = GetComponentInChildren<MapController>();
        mapController.SetupMap();
    }

    public void SetupSkin()
    {
        modelSkinController = GetComponentInChildren<ModelSkinController>();
        modelSkinController.SetupModelSkin();
    }

    public void SetupHorn()
    {
        modelHornController = GetComponentInChildren<ModelHornController>();
        modelHornController.SetupModelHorn();
    }
}