using System.Collections;
using System.Collections.Generic;
// using Pancake;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : HCMonoBehaviour
{
    [ReadOnly] public StateItem stateItem;
    public GameObject bgUnSelect;
    public GameObject bgSelect;
    public Image icon;
    public GameObject btnBuy;
    public GameObject btnCannotBuy;
    public GameObject btnAds;
   // public GameObject btnDaily;
    public TextMeshProUGUI textCoin;
    public TextMeshProUGUI textCoinCannotBuy;

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
       // btnDaily.SetActive(false);
        icon.sprite = itemData.imageIcon;
        icon.SetNativeSize();
        textCoin.text = textCoinCannotBuy.text = itemData.Coin.ToString();
    }

    private void SetupStateItem()
    {
        if (itemData.typeItem == TypeItem.Skin && itemData.id == Database.CurrentIdModelSkin)
        {
            stateItem = StateItem.Select;
        }
        else if (itemData.typeItem == TypeItem.Sword && itemData.id == Database.CurrentIdHorn)
        {
            stateItem = StateItem.Select;
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
                // case TypeBuy.DailyReward:
                //     btnDaily.SetActive(true);
                //     break;
            }
        }
    }

    private bool CanBuyItem()
    {
        if (Gm.data.user.money >= itemData.Coin && !itemData.IsUnlock) return true;
        //if (Data.CurrencyTotal >= itemData.Coin && !itemData.IsUnlock) return true;
       // return false;
       return false;
    }

    public void OnClickSelect()
    {
        Debug.Log("click");
        
        if (itemData.IsUnlock && stateItem == StateItem.UnSelect)
        {
            Debug.Log("chon vao");
            switch (itemData.typeItem)
            {
                case TypeItem.Skin:
                    stateItem = StateItem.Select;
                    Database.CurrentIdModelSkin = itemData.id;
                    PopupShop.Instance.SetupState(PopupShop.Instance.currentShopState);
                    Debug.Log("skin dc chon " + itemData.id);
                    break;
                case TypeItem.Sword:
                    stateItem = StateItem.Select;
                    Database.CurrentIdHorn = itemData.id;
                    PopupShop.Instance.SetupState(PopupShop.Instance.currentShopState);
                    break;
            }
        }


        PopupShop.Instance.ViewSkin(itemData.id);
    }

    public void OnClickBuy()
    {
        Gm.data.user.money -= itemData.Coin;
        //Data.CurrencyTotal -= itemData.Coin;
        itemData.IsUnlock = true;
      //  SoundController.Instance.PlayFX(SoundType.CompletePurchase);
        SetupUI();
        OnClickSelect();
    }

    public void OnClickDaily()
    {
        // PopupShop.Hide();
        // PopupController.Instance.Show<PopupDailyReward>();
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
