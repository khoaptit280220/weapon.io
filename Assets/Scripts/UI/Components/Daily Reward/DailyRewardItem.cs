using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardItem : HCMonoBehaviour
{
    private ItemData currentItemData;
    public enum Status
    {
        NotClaimable,
        Claimable,
        Claimed
    }

    [SerializeField] private HCButton button;
    [SerializeField] private GameObject bgClaim, coin, gift, claimed, claimable;
    [SerializeField] private TMP_Text coinVal;

    [SerializeField] private Image IconGift;
    [SerializeField] private Sprite CoinIcon;

    [SerializeField] private RewardType type;
    // [SerializeField] private MoneyClaimFx moneyClaimFx;

    private Action _onClaim;
    private int _coinValue;

    private ItemData _itemData;

    public void InitCoin(int value, Action onClaim)
    {
        type = RewardType.Coin;
        _onClaim = onClaim;

        _coinValue = value;

        UpdateVisual();
    }

    public void InitGift(Action onClaim)
    {
        
        type = RewardType.Gift;
        _onClaim = onClaim;

        _itemData = ConfigManager.Instance.itemConfig.GetItemData(Gm.data.user.giftDailyType, Gm.data.user.giftDailyID);
        

        UpdateVisual();
    }

    void UpdateVisual()
    {
        coin.SetActive(type == RewardType.Coin);
        gift.SetActive(type == RewardType.Gift);

        coinVal.text = _coinValue.ToFormatString();
        if (_itemData != null)
        {
            IconGift.sprite = _itemData.imageIcon;
        }
        else
        {
            IconGift.sprite = CoinIcon;
        }
    }

    public void SetStatus(Status status)
    {
        button.interactable = status == Status.Claimable;
        claimable.SetActive(status == Status.Claimable);
        claimed.SetActive(status == Status.Claimed);

        if (status == Status.NotClaimable)
        {
            bgClaim.SetActive(true);
        }

        if (status == Status.Claimable || status == Status.Claimed)
        {
            bgClaim.SetActive(false);
        }
    }

    public void Claim()
    {
        PopupReward.Show();
        switch (type)
        {
            case RewardType.Coin:

                PopupReward.Instance.InitCoin(_coinValue);

                SetStatus(Status.Claimed);
                break;
            case RewardType.Gift:

                var giftDaily = ConfigManager.Instance.itemConfig.GetRandomItemDaily();
                Gm.data.user.giftDailyID = giftDaily.id;
                Gm.data.user.giftDailyType = giftDaily.typeItem;
                PopupReward.Instance.InitGift(_itemData);

                SetStatus(Status.Claimed);
                break;
        }

        Gm.data.user.dailyRewardClaimedCount++;
        Gm.data.user.lastDailyRewardClaimTime = DateTime.Now;
        _onClaim?.Invoke();
    }
}