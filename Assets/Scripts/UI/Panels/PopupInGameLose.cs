using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Khoant;
using NSubstitute.Exceptions;
using UnityEngine;

public class PopupInGameLose : UIPanel
{
    public static PopupInGameLose Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupInGameLose;
    }

    public static void Show()
    {
        var newInstance = (PopupInGameLose)GUIManager.Instance.NewPanel(UiPanelType.PopupInGameLose);
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
            GameManager.Instance.previewAfterGame.SetActive(true);
            Hide();
            ScreenLose.Show();
            EventController.AfterWeapon?.Invoke();
            EventController.Lose?.Invoke();
        })));
    }


    IEnumerator DelayShowScreenLose(Action cb = null)
    {
        yield return new WaitForSeconds(3f);
        cb?.Invoke();
    }
}