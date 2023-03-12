using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PopupMatch : UIPanel
{
    public static PopupMatch Instance { get; private set; }
    public ItemMatching ItemMatchingPrefabs;
    public Transform container;
    private List<CountryData> listCountryDatas;

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupMatch;
    }

    public static void Show()
    {
        var newInstance = (PopupMatch)GUIManager.Instance.NewPanel(UiPanelType.PopupMatch);
        Instance = newInstance;
        newInstance.OnAppear();
    }

    public override void OnAppear()
    {
        base.OnAppear();
        Init();
    }

    private void Init()
    { 
        SetupUI();
        DOVirtual.DelayedCall(3f, () =>
        {
            Close();
            GameManager.Instance.StartGame();
            PlayScreen.Show();
        });
    }

    public override void OnDisappear()
    {
        base.OnDisappear();
        Instance = null;
    }

    private void SetupUI()
    {
        Utility.Clear(container);
        listCountryDatas = ConfigManager.Instance.countryConfig.GetRandomTenValue();
        foreach (var VARIABLE in listCountryDatas)
        {
            ItemMatching itemMatching = Instantiate(ItemMatchingPrefabs, container);
            itemMatching.Init(VARIABLE);
        }
    }
}