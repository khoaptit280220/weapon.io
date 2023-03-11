using System;
using System.Collections;
using System.Collections.Generic;
using Khoant;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemGiftLevel : HCMonoBehaviour
{
    private int coin = 1000;
    [SerializeField] private GameObject ads;
    [SerializeField] private Sprite coinIcon;
    [SerializeField] private Slider progressSlider;

    [SerializeField] private ItemGiftLevel progress;

    [SerializeField] private Image IconGift;

    [SerializeField] private TMP_Text txtkillAds, begin, end;

    [SerializeField] private MoneyClaimFx moneyClaimFx;

    private ItemData itemData;

    private RewardType type;


    public void Init()
    {
        ads.SetActive(true);
        progressSlider.maxValue = Gm.data.user.level * 15;

        Gm.data.user.killHead += GameManager.Instance.kill;
        var progress = Gm.data.user.killHead;
        progressSlider.value = progress;

        txtkillAds.text = "" + GameManager.Instance.kill;
        begin.text = "" + Gm.data.user.killHead;
        end.text = "" + progressSlider.maxValue;

        InitItem();

        UpdateProgress();
    }

    private void InitItem()
    {
        itemData = ConfigManager.Instance.itemConfig.GetItemData(Gm.data.user.giftLevelType, Gm.data.user.giftLevelID);
        if (itemData != null)
        {
            type = RewardType.Gift;
            IconGift.sprite = itemData.imageIcon;
        }
        else
        {
            type = RewardType.Coin;
            IconGift.sprite = coinIcon;
        }
    }

    public void OnClickAddHead()
    {
        AdManager.Instance.ShowRewardedAds("AddHeadKill", () =>
        {
            ads.SetActive(false);
            Gm.data.user.killHead += GameManager.Instance.kill;
            progress.UpdateProgress();
        });
    }

    public void UpdateProgress()
    {
        var progress = Gm.data.user.killHead;
        progressSlider.value = progress;


        begin.text = "" + Gm.data.user.killHead;
        end.text = "" + progressSlider.maxValue;

        SetupItem();

        //   progressTxt.text = $"Progress: {progress}/{progressSlider.maxValue}";
    }

    public void SetupItem()
    {
        if (Gm.data.user.killHead >= progressSlider.maxValue)
        {
            moneyClaimFx.ClaimMoney(100);
            PopupSelectGiftLevel.Show();
            switch (type)
            {
                case RewardType.Coin:

                    PopupSelectGiftLevel.Instance.InitCoin(coin);

                    break;
                case RewardType.Gift:
                    PopupSelectGiftLevel.Instance.InitGift(itemData);

                    break;
            }

            itemData.typeBuy = TypeBuy.Ads;
            Gm.data.user.killHead = 0;
            Gm.data.user.level++;

            var giftLevel = ConfigManager.Instance.itemConfig.GetRandomItemLevel();
            Gm.data.user.giftLevelID = giftLevel.id;
            Gm.data.user.giftLevelType = giftLevel.typeItem;

            Init();
        }
    }

    // private void OnEnable()
    // {
    //     EventController.GiftLevel += SetupItem;
    // }
    //
    // private void OnDisable()
    // {
    //     EventController.GiftLevel -= SetupItem;
    // }
}