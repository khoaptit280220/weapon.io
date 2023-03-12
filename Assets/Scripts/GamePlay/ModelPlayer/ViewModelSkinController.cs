using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Khoant;
using Sirenix.OdinInspector;
using UnityEngine;

public class ViewModelSkinController : MonoBehaviour
{
    [ReadOnly] public ModelSkinData currentModelSkinData;

    public List<SkinController> listModelSkins = new List<SkinController>();

    // public List<SkinController> listSkins => GetComponentsInChildren<SkinController>().ToList();
    [Button]
    private void LoadList()
    {
        listModelSkins = GetComponentsInChildren<SkinController>().ToList();
    }

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
        EventController.MainSkin += MainGame;
        EventController.Win += WinGame;
        EventController.Lose += LoseGame;
    }

    private void OnDestroy()
    {
        EventController.MainSkin -= MainGame;
        EventController.Win -= WinGame;
        EventController.Lose -= LoseGame;
    }

    public void MainGame()
    {
        SetupModelSkin();
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