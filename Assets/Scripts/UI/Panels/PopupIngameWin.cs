using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Khoant;
using UnityEngine;

public class PopupIngameWin : UIPanel
{
    public static PopupIngameWin Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupIngameWin;
    }

    public static void Show()
    {
        var newInstance = (PopupIngameWin)GUIManager.Instance.NewPanel(UiPanelType.PopupIngameWin);
        Instance = newInstance;
        newInstance.OnAppear();
    }

    public override void OnAppear()
    {
        if (isInited)
            return;
        base.OnAppear();
        Init();
    }


    private void Init()
    {
        GameManager.Instance.GameState = GameState.Lose;

        StartCoroutine((DelayShowScreenLose(() =>
        {
            Hide();
            ScreenWin.Show();
            GameManager.Instance.previewAfterGame.SetActive(true);
            EventController.AfterWeapon?.Invoke();
            EventController.Win?.Invoke();
        })));
    }


    IEnumerator DelayShowScreenLose(Action cb = null)
    {
        yield return new WaitForSeconds(3f);
        cb?.Invoke();
    }
}