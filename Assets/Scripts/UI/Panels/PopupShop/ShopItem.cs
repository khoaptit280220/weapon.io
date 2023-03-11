using System.Collections;
using System.Collections.Generic;
using Khoant;
// using Pancake;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : HCMonoBehaviour
{
    //private ControlViewModelSkin viewModelSkin;

    [ReadOnly] public StateItem stateItem;
    public GameObject bgUnSelect;
    public GameObject bgSelect;
    public Image icon;
    public GameObject btnBuy;
    public GameObject btnCannotBuy;

    public GameObject btnAds;

    public GameObject btnDaily;
    public TextMeshProUGUI textCoin;
    public TextMeshProUGUI textCoinCannotBuy;

    public TextMeshProUGUI textName;

    //public string descripSkin;

    private ItemData itemData;

    // public PopupShop PopupShop => (PopupShop)GUIManager.Instance.NewPanel(UiPanelType.PopupShop) as PopupShop;
    public void InitItemData(ItemData _itemData)
    {
        itemData = _itemData;
        SetupStateItem();
        SetupUI();
    }

    private void SetupDefaultUI()
    {
        bgSelect.SetActive(false);
        bgUnSelect.SetActive(false);
        btnBuy.SetActive(false);
        btnCannotBuy.SetActive(false);
        btnAds.SetActive(false);
        btnDaily.SetActive(false);
        if (itemData.typeItem == TypeItem.Skin)
        {
            textName.text = "" + itemData.skinName;
        }

        if (itemData.typeItem == TypeItem.Sword)
        {
            textName.text = "" + itemData.swordName;
        }

        if (itemData.typeItem == TypeItem.Trail)
        {
            textName.text = "" + itemData.trailName;
        }

        icon.sprite = itemData.imageIcon;
        textCoin.text = textCoinCannotBuy.text = itemData.Coin.ToString();
    }

    private void SetupStateItem()
    {
        if (itemData.typeItem == TypeItem.Skin && itemData.id == Database.CurrentIdModelSkin)
        {
            stateItem = StateItem.Select;
            PopupShop.Instance.name.text = "" + itemData.skinName;
            PopupShop.Instance.desSkin.text = "" + itemData.descripSkin;

            EventController.OnChangeViewSkin?.Invoke(itemData.id);

            PopupShop.Instance.idviewSkin = itemData.id;
        }
        else if (itemData.typeItem == TypeItem.Sword && itemData.id == Database.CurrentIdHorn)
        {
            stateItem = StateItem.Select;
            PopupShop.Instance.name.text = "" + itemData.swordName;
            PopupShop.Instance.desSkin.text = "";
        }
        else if (itemData.typeItem == TypeItem.Trail && itemData.id == Database.CurrentIdTrail)
        {
            stateItem = StateItem.Select;
        }
        else
        {
            stateItem = StateItem.UnSelect;
        }
    }

    private void SetupUI()
    {
        SetupDefaultUI();

        if (itemData.IsUnlock)
        {
            if (stateItem == StateItem.Select)
            {
                bgSelect.SetActive(true);
            }
            else
            {
                bgUnSelect.SetActive(true);
            }
        }
        else
        {
            bgUnSelect.SetActive(true);
            switch (itemData.typeBuy)
            {
                case TypeBuy.Default:
                    break;
                case TypeBuy.Coin:
                    if (CanBuyItem())
                    {
                        btnBuy.SetActive(true);
                    }
                    else
                    {
                        btnCannotBuy.SetActive(true);
                    }

                    break;
                case TypeBuy.Ads:
                    btnAds.SetActive(true);
                    break;
                case TypeBuy.DailyReward:
                    btnDaily.SetActive(true);
                    break;
            }
        }
    }

    private bool CanBuyItem()
    {
        if (Gm.data.user.money >= itemData.Coin && !itemData.IsUnlock)
        {
            return true;
        }

        //if (Data.CurrencyTotal >= itemData.Coin && !itemData.IsUnlock) return true;
        // return false;
        return false;
    }

    public void OnClickSelect()
    {
        switch (itemData.typeItem)
        {
            case TypeItem.Skin:
                PopupShop.Instance.name.text = "" + itemData.skinName;
                PopupShop.Instance.desSkin.text = "" + itemData.descripSkin;

                EventController.OnChangeViewSkin?.Invoke(itemData.id);

                PopupShop.Instance.idviewSkin = itemData.id;
                break;
            case TypeItem.Sword:
                PopupShop.Instance.name.text = "" + itemData.swordName;
                PopupShop.Instance.desSkin.text = "";
                break;
        }


        if (itemData.IsUnlock && stateItem == StateItem.UnSelect)
        {
            switch (itemData.typeItem)
            {
                case TypeItem.Skin:
                    stateItem = StateItem.Select;
                    Database.CurrentIdModelSkin = itemData.id;
                    PopupShop.Instance.SetupState(PopupShop.Instance.currentShopState);
                    break;
                case TypeItem.Sword:
                    stateItem = StateItem.Select;
                    Database.CurrentIdHorn = itemData.id;
                    PopupShop.Instance.SetupState(PopupShop.Instance.currentShopState);
                    break;
            }
        }

        //PopupShop.Instance.ViewSkin(itemData.id);
    }

    public void OnClickBuy()
    {
        Gm.data.user.money -= itemData.Coin;
        //Data.CurrencyTotal -= itemData.Coin;
        itemData.IsUnlock = true;
        ConfigManager.Instance.modelSkinConfig.GetModelSkinById(itemData.id).IsUnlock = true;
        //  SoundController.Instance.PlayFX(SoundType.CompletePurchase);
        SetupUI();
        OnClickSelect();
    }

    public void OnClickWatchAds()
    {
        // if (Data.IsTesting)
        // {
        //     itemData.IsUnlock = true;
        //     //SoundController.Instance.PlayFX(SoundType.CompletePurchase);
        //     SetupUI();
        //     OnClickSelect();
        // }
        // else
        // {
        //     AdsManager.ShowRewardAds((() =>
        //     {
        //         itemData.IsUnlock = true;
        //         //SoundController.Instance.PlayFX(SoundType.CompletePurchase);
        //         SetupUI();
        //         OnClickSelect();
        //     }), () => { itemData.IsUnlock = false; });
        // }
    }
}

public enum StateItem
{
    Select,
    UnSelect
}