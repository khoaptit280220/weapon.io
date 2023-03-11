using System.Collections;
using System.Collections.Generic;
using Khoant;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PopupSelectGiftLevel : UIPanel
{
    [SerializeField] private Sprite CoinIcon;
    [SerializeField] private Image iconGiftLevel;
    [SerializeField] private TMP_Text name;

    [SerializeField] private MoneyClaimFx moneyClaimFx;

    public static PopupSelectGiftLevel Instance { get; private set; }

    private ItemData _itemData;
    private int coin;

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupSelectGiftLevel;
    }

    public static void Show()
    {
        var newInstance = (PopupSelectGiftLevel)GUIManager.Instance.NewPanel(UiPanelType.PopupSelectGiftLevel);
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

    public void InitCoin(int val)
    {
        coin = val;
        iconGiftLevel.sprite = CoinIcon;
        name.text = "" + val;
    }

    public void InitGift(ItemData itemData)
    {
        _itemData = itemData;
        iconGiftLevel.sprite = itemData.imageIcon;
        if (itemData.typeItem == TypeItem.Skin)
        {
            name.text = "" + itemData.skinName;
        }

        if (itemData.typeItem == TypeItem.Sword)
        {
            name.text = "" + itemData.swordName;
        }

        if (itemData.typeItem == TypeItem.Trail)
        {
            name.text = "" + itemData.trailName;
        }
    }

    public void Claim()
    {
        AdManager.Instance.ShowRewardedAds("AddGiftLevel", () =>
        {
            if (_itemData.IsUnlock == false)
            {
                _itemData.IsUnlock = true;
                if (_itemData.typeItem == TypeItem.Skin)
                {
                    Database.CurrentIdModelSkin = _itemData.id;
                    EventController.MainSkin?.Invoke();
                }

                if (_itemData.typeItem == TypeItem.Sword)
                {
                    Database.CurrentIdHorn = _itemData.id;
                    EventController.MainWeapon?.Invoke();
                }

                if (_itemData.typeItem == TypeItem.Trail)
                {
                    Database.CurrentIdTrail = _itemData.id;
                }
            }
            else
            {
                moneyClaimFx.ClaimMoney(coin);
            }
        });
    }
}