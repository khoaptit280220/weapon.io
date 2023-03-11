#region

using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Khoant;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class MainScreen : UIPanel
{
    public static MainScreen Instance { get; private set; }

    [SerializeField] public GameObject btnNoAds;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_Text coin;

    [SerializeField] private ItemGiftMain _itemGiftMain;

    public override UiPanelType GetId()
    {
        return UiPanelType.MainScreen;
    }

    public static void Show()
    {
        var newInstance = (MainScreen)GUIManager.Instance.NewPanel(UiPanelType.MainScreen);
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
        GameManager.Instance.previewMain.SetActive(true);
        UpdateText();
        EventController.MainSkin?.Invoke();
        EventController.MainWeapon?.Invoke();
        _itemGiftMain.InitItemData();
    }

    public void ClaimItemAds()
    {
        AdManager.Instance.ShowRewardedAds("AddGiftMain", () =>
        {
            _itemGiftMain.itemData.IsUnlock = true;

            _itemGiftMain.ShowReward();

            var giftMain = ConfigManager.Instance.itemConfig.GetRandomItemAds();
            Gm.data.user.giftMainID = giftMain.id;
            Gm.data.user.giftMainType = giftMain.typeItem;
            _itemGiftMain.InitItemData();
        });

        // }), () => { _itemGiftMain.itemData.IsUnlock = false; });
    }

    public void ShowSetting()
    {
        AudioAssistant.Shot(TypeSound.Button);
        PopupSetting.Show();
    }

    public void StartGame()
    {
        AudioAssistant.Shot(TypeSound.Button);

        if (!GameManager.NetworkAvailable)
        {
            PopupNoInternet.Show();
            return;
        }

        GameManager.Instance.previewMain.SetActive(false);
        //PlayScreen.Show();
        PopupMatch.Show();
    }

    public void ShowMap()
    {
        GameManager.Instance.previewMain.SetActive(false);
        AudioAssistant.Shot(TypeSound.Button);
        MapScreen.Show();
    }

    public void ShowShop()
    {
        GameManager.Instance.previewMain.SetActive(false);
        GameManager.Instance.previewShop.SetActive(true);
        AudioAssistant.Shot(TypeSound.Button);
        PopupShop.Show();
    }

    public void OnBuyNoAds()
    {
        AudioAssistant.Shot(TypeSound.Button);
        IAPManager.Instance.BuyProduct(IapProductName.NoAds);
    }

    public void ChangeName()
    {
        Gm.data.user.name = nameInput.text;
    }

    public void UpdateText()
    {
        nameInput.text = Gm.data.user.name;
        coin.text = "" + Gm.data.user.money;
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