using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemGiftMain : HCMonoBehaviour
{
    private int coin = 1000;
    [SerializeField] private Sprite coinIcon;
    public Image iconGift;
    [SerializeField] private TMP_Text nameGift;

    [HideInInspector] public ItemData itemData;

    private RewardType type;

    public void InitItemData()
    {
        itemData = ConfigManager.Instance.itemConfig.GetItemData(Gm.data.user.giftMainType, Gm.data.user.giftMainID);

        if (itemData != null)
        {
            InitGift(RewardType.Gift);
        }
        else
        {
            InitGift(RewardType.Coin);
        }
    }

    private void InitGift(RewardType rewardType)
    {
        switch (rewardType)
        {
            case RewardType.Coin:
                type = RewardType.Coin;
                iconGift.sprite = coinIcon;
                nameGift.text = "+ 1000";
                break;
            case RewardType.Gift:
                type = RewardType.Gift;

                iconGift.sprite = itemData.imageIcon;
                if (itemData.typeItem == TypeItem.Skin)
                {
                    nameGift.text = "" + itemData.skinName;
                }

                if (itemData.typeItem == TypeItem.Sword)
                {
                    nameGift.text = "" + itemData.swordName;
                }

                if (itemData.typeItem == TypeItem.Trail)
                {
                    nameGift.text = "" + itemData.trailName;
                }

                break;
        }
    }

    public void ShowReward()
    {
        PopupReward.Show();
        switch (type)
        {
            case RewardType.Coin:
                PopupReward.Instance.InitCoin(coin);

                break;
            case RewardType.Gift:
                PopupReward.Instance.InitGift(itemData);

                break;
        }
    }
}