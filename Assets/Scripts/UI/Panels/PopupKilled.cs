using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PopupKilled : UIPanel
{
    [SerializeField] private TMP_Text txttime;
    private int time = 5;
    public GameObject btnNoThanks;
    private bool click;
    public static PopupKilled Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupKilled;
    }

    public static void Show()
    {
        var newInstance = (PopupKilled)GUIManager.Instance.NewPanel(UiPanelType.PopupKilled);
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
        time = 5;
        txttime.text = time.ToString();
        click = false;
        btnNoThanks.SetActive(false);
        if (!click) DOTween.Sequence().SetDelay(3).OnComplete((() => { btnNoThanks.SetActive(true); }));
    }

    private void OnEnable()
    {
        EventGlobalManager.Instance.OnEverySecondTick.AddListener(UpdateTimerKill);
        UpdateTimerKill();
    }

    private void OnDisable()
    {
        if (EventGlobalManager.Instance)
            EventGlobalManager.Instance.OnEverySecondTick.AddListener(UpdateTimerKill);
    }

    public void OnClickNothanks()
    {
        click = true;
        Close();
        Hide();
        PopupInGameLose.Show();
    }

    public void OnClickRevival()
    {
        click = true;
        AdManager.Instance.ShowRewardedAds("Revival", () =>
        {
            GameManager.Instance.GameState = GameState.PLaying;
            Close();
            Hide();
            GameManager.Instance.GetPlayer.Revival();
        });
    }

    void UpdateTimerKill()
    {
        if (!click)
        {
            if (time > 0)
            {
                time -= 1;
                txttime.text = time.ToString();
            }
            else
            {
                click = true;
                Close();
                Hide();
                PopupInGameLose.Show();
            }
        }
    }
}