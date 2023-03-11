using System.Collections;
using System.Collections.Generic;
using Khoant;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenLose : UIPanel
{
    [SerializeField] private TMP_Text txtRank, txtkill, txtCoin;

    [SerializeField] private ItemGiftLevel _itemGiftLevel;
    public static ScreenLose Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.ScreenLose;
    }

    public static void Show()
    {
        var newInstance = (ScreenLose)GUIManager.Instance.NewPanel(UiPanelType.ScreenLose);
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
        txtRank.text = "" + GameManager.Instance.rank;
        txtkill.text = "" + GameManager.Instance.kill;
        txtCoin.text = "" + GameManager.Instance.coin;

        _itemGiftLevel.Init();

        //GameManager.Instance.ViewModelWeponController.SetupModelWeapon();
    }

    public void ReMatch()
    {
        AudioAssistant.Shot(TypeSound.Button);
        Hide();
        GameManager.Instance.BackHome();
        GameManager.Instance.previewAfterGame.SetActive(false);
        GameManager.Instance.previewMain.SetActive(true);
        MainScreen.Show();
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