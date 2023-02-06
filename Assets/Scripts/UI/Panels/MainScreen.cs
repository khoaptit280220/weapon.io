﻿#region

using TMPro;
using UnityEngine;

#endregion

public class MainScreen : UIPanel
{
   
    public static MainScreen Instance { get; private set; }

    [SerializeField]
    public GameObject btnNoAds;

    public override UiPanelType GetId()
    {
        return UiPanelType.MainScreen;
    }

    public static void Show()
    {
        var newInstance = (MainScreen) GUIManager.Instance.NewPanel(UiPanelType.MainScreen);
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
    }

    public void ShowSetting()
    {
        AudioAssistant.Shot(TypeSound.Button);
        PopupSetting.Show();
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
        AudioAssistant.Shot(TypeSound.Button);

        if (!GameManager.NetworkAvailable)
        {
            PopupNoInternet.Show();
            return;
        }
        
        PlayScreen.Show();

     //   GameManager.Instance.GetPlayer.isPlayerDied = false;
    }

    public void ShowMap()
    {
        AudioAssistant.Shot(TypeSound.Button);
        MapScreen.Show();
    }

    public void OnBuyNoAds()
    {
        AudioAssistant.Shot(TypeSound.Button);
        IAPManager.Instance.BuyProduct(IapProductName.NoAds);
    }

    protected override void RegisterEvent()
    {
        base.RegisterEvent();
    }

    protected override void UnregisterEvent()
    {
        base.UnregisterEvent();
    }

    public override void OnDisappear()
    {
        base.OnDisappear();
        Instance = null;
    }
}