using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Khoant;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupReward : UIPanel
{
    // [SerializeField] private Transform popup;
    [SerializeField] private GameObject coin, skin;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text coinVal;
    [SerializeField] private MoneyClaimFx moneyClaimFx;

    // private CanvasGroup _canvasGroup;

    private int _coinValue;

    private ItemData itemData;
    public static PopupReward Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupReward;
    }

    public static void Show()
    {
        var newInstance = (PopupReward)GUIManager.Instance.NewPanel(UiPanelType.PopupReward);
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
        // _canvasGroup = GetComponent<CanvasGroup>();
        //  _canvasGroup.alpha = 0;
        // popup.localScale = Vector3.zero;
        coin.SetActive(false);
        skin.SetActive(false);
        //_coinValue = 0;
        // gameObject.SetActive(false);
    }


    // private TypeModelSkin _unlockedSkin;

    // Start is called before the first frame update

    public void InitCoin(int val)
    {
        coin.SetActive(true);
        _coinValue = val;
        coinVal.text = val.ToFormatString();
    }

    public void InitGift(ItemData _itemData)
    {
        itemData = _itemData;
        skin.SetActive(true);
        itemIcon.sprite = itemData.imageIcon;
    }

    // public void Show(float animInTime)
    // {
    //     gameObject.SetActive(true);
    //     _canvasGroup.alpha = 0;
    //     _canvasGroup.DOFade(1, animInTime * .7f);
    //     popup.localScale = Vector3.zero;
    //     popup.DOScale(new Vector3(.9f, 1.1f, 1), animInTime * .4f).OnComplete(() =>
    //     {
    //         popup.DOScale(new Vector3(1.1f, .9f, 1), animInTime * .2f).OnComplete(() =>
    //         {
    //             popup.DOScale(new Vector3(.95f, 1.05f, 1), animInTime * .2f).OnComplete(() =>
    //             {
    //                 popup.DOScale(Vector3.one, animInTime * .2f);
    //             });
    //         });
    //     });
    // }
    //
    // void Close(float animOutTime)
    // {
    //     _canvasGroup.DOFade(0, animOutTime * .7f);
    //     popup.DOScale(Vector3.one * 1.1f, animOutTime * .33f).OnComplete(() =>
    //     {
    //         popup.DOScale(Vector3.zero, animOutTime * .67f).OnComplete(() =>
    //         {
    //             coin.SetActive(false);
    //             skin.SetActive(false);
    //             _coinValue = 0;
    //             gameObject.SetActive(false);
    //         });
    //     });
    // }

    public void Claim()
    {
        if (coin.activeInHierarchy)
        {
            moneyClaimFx.ClaimMoney(_coinValue);
        }

        if (skin.activeInHierarchy)
        {
            if (itemData.IsUnlock == false)
            {
                if (itemData.typeItem == TypeItem.Skin)
                {
                    Database.CurrentIdModelSkin = itemData.id;
                    EventController.MainSkin?.Invoke();
                }

                if (itemData.typeItem == TypeItem.Sword)
                {
                    Database.CurrentIdHorn = itemData.id;
                    EventController.MainWeapon?.Invoke();
                }

                if (itemData.typeItem == TypeItem.Trail)
                {
                    Database.CurrentIdTrail = itemData.id;
                }
            }

            if (itemData.IsUnlock == true)
            {
                moneyClaimFx.ClaimMoney(1000);
            }
        }

        DOTween.Sequence().SetDelay(1).OnComplete(() => { Close(); });

        // Close(.35f);
    }
}