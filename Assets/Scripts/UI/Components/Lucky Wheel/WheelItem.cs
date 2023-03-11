using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WheelItem : HCMonoBehaviour
{
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject gift;
    [SerializeField] private TMP_Text valueText;
    [SerializeField] private Image giftIcon;

    [SerializeField] private Sprite CoinIcon;

    [SerializeField] private RewardType type;
    //   [SerializeField] private MoneyClaimFx moneyClaimFx;

    private int _value;
    private ItemData _itemData;

    public void InitCoin(int val)
    {
        type = RewardType.Coin;
        _value = val;
        coin.SetActive(true);
        gift.SetActive(false);
        valueText.text = val.ToFormatString();
    }

    public void InitGift()
    {
        type = RewardType.Gift;

        _itemData = ConfigManager.Instance.itemConfig.GetItemData(Gm.data.user.giftLuckyType, Gm.data.user.giftLuckyID);


        // TODO: Init gift reward

        coin.SetActive(false);
        gift.SetActive(true);
        if (_itemData != null)
        {
            giftIcon.sprite = _itemData.imageIcon;
        }
        else
        {
            giftIcon.sprite = CoinIcon;
        }
    }

    public void Claim()
    {
        PopupReward.Show();
        switch (type)
        {
            case RewardType.Coin:

                PopupReward.Instance.InitCoin(_value);
                break;
            case RewardType.Gift:

                var giftLuck = ConfigManager.Instance.itemConfig.GetRandomItemDaily();
                Gm.data.user.giftLuckyID = giftLuck.id;
                Gm.data.user.giftLuckyType = giftLuck.typeItem;
                PopupReward.Instance.InitGift(_itemData);

                break;
        }
    }
}