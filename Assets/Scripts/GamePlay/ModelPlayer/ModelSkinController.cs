using System;
using System.Collections;
using System.Collections.Generic;
using Khoant;
using Sirenix.OdinInspector;
using UnityEngine;

public class ModelSkinController : MonoBehaviour
{
    [ReadOnly] public ModelSkinData currentModelSkinData;


    public List<SkinController> listModelSkins = new List<SkinController>();

    private void SetupModelSkinDefault()
    {
        listModelSkins.ForEach(item => item.gameObject.SetActive(false));
    }

    public void SetupModelSkin()
    {
        currentModelSkinData = ConfigManager.Instance.modelSkinConfig.GetModelSkinById(Database.CurrentIdModelSkin);
        SetupModelSkinDefault();
        foreach (var VARIABLE in listModelSkins)
        {
            if (VARIABLE.idSkin == currentModelSkinData.idModelSkin)
            {
                VARIABLE.gameObject.SetActive(true);
            }
        }
    }

    private void OnEnable()
    {
        EventController.Win += WinGame;
        EventController.Lose += LoseGame;
    }

    private void OnDestroy()
    {
        EventController.Win -= WinGame;
        EventController.Lose -= LoseGame;
    }

    public void WinGame()
    {
        SetupModelSkin();
    }

    public void LoseGame()
    {
        SetupModelSkin();
    }
}