using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PopupMatch : UIPanel
{
    public static PopupMatch Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupMatch;
    }

    public static void Show()
    {
        var newInstance = (PopupMatch)GUIManager.Instance.NewPanel(UiPanelType.PopupMatch);
        Instance = newInstance;
        newInstance.OnAppear();
    }

    public override void OnAppear()
    {
        base.OnAppear();
        Init();
    }

    private void Init()
    {
        DOVirtual.DelayedCall(3f, () =>
        {
            Close();
            GameManager.Instance.StartGame();
            PlayScreen.Show();
        });
    }

    public override void OnDisappear()
    {
        base.OnDisappear();
        Instance = null;
    }
}