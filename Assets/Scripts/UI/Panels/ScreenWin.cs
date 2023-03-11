using System.Collections;
using System.Collections.Generic;
using Khoant;
using TMPro;
using UnityEngine;

public class ScreenWin : UIPanel
{
    [SerializeField] private TMP_Text txtRank, txtkill, txtCoin, txtCoinGif, txtCoinGifX2;

    [SerializeField] private MoneyClaimFx moneyClaimFx;


    [SerializeField] private ItemGiftLevel _itemGiftLevel;
    public static ScreenWin Instance { get; private set; }

    private int coin;

    public override UiPanelType GetId()
    {
        return UiPanelType.ScreenWin;
    }

    public static void Show()
    {
        var newInstance = (ScreenWin)GUIManager.Instance.NewPanel(UiPanelType.ScreenWin);
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
        Gm.AddMoney(GameManager.Instance.coin);

        if (GameManager.Instance.kill == 0)
        {
            coin = 50;
        }
        else if (GameManager.Instance.kill > 10)
        {
            coin = 500;
        }
        else
        {
            coin = GameManager.Instance.kill * 50;
        }

        txtCoinGif.text = "+ " + coin;
        txtCoinGifX2.text = "+ " + coin * 2;
        txtRank.text = "" + GameManager.Instance.rank;
        txtkill.text = "" + GameManager.Instance.kill;
        txtCoin.text = "" + GameManager.Instance.coin;

        _itemGiftLevel.Init();
    }


    public void Continue()
    {
        moneyClaimFx.ClaimMoney(coin);
        AudioAssistant.Shot(TypeSound.Button);
        Hide();
        GameManager.Instance.BackHome();
        GameManager.Instance.previewAfterGame.SetActive(false);
        GameManager.Instance.previewMain.SetActive(true);
        MainScreen.Show();
    }

    public void Ads()
    {
        AdManager.Instance.ShowRewardedAds("AdsX2CoinWin", () =>
        {
            coin = coin * 2;
            Continue();
        });
    }


    public override void OnDisappear()
    {
        base.OnDisappear();
        Instance = null;
    }
}