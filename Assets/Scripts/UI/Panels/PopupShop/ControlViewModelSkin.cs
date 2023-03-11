using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ControlViewModelSkin : MonoBehaviour
{
    public AnimSkinShop animSkin;
    public List<SkinController> listSkin = new List<SkinController>();

    private void SetupViewSkinDefault()
    {
        listSkin.ForEach(item => item.gameObject.SetActive(false));
    }

    public void SetupModelSkin(int id)
    {
        SetupViewSkinDefault();
        foreach (var VARIABLE in listSkin)
        {
            if (VARIABLE.idSkin == id)
            {
                VARIABLE.gameObject.SetActive(true);
                animSkin.PlayIdle();
            }
        }
    }

    private void Update()
    {
        SetupModelSkin(PopupShop.Instance.idviewSkin);
    }
}