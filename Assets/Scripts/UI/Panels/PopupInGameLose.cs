using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Khoant;
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

        DOTween.Sequence().SetDelay(3).OnComplete(() =>
        {
            GameManager.Instance.previewAfterGame.SetActive(true);
           
            Hide();
            ScreenLose.Show();
            EventController.Lose?.Invoke();
            
        });
    }
}