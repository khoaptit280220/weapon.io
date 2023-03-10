using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupSelectMap : UIPanel
{
    public static PopupSelectMap Instance { get; private set; }

    [SerializeField] private Button closeBg;

    [SerializeField] private Button closeButton;

    [SerializeField] private TMP_Text textMap;
    [SerializeField] private TMP_Text desMap;

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupSelectMap;
    }

    public static void Show()
    {
        var newInstance = (PopupSelectMap)GUIManager.Instance.NewPanel(UiPanelType.PopupSelectMap);
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
        textMap.text = MapScreen.Instance.textMap;
        desMap.text = MapScreen.Instance.desMap;
    }

    public override void OnDisappear()
    {
        base.OnDisappear();
        Instance = null;
    }

    public void StartGame()
    {
        GameManager.Instance.PrepareGame();
        Hide();
        GameManager.Instance.StartGame();
        AudioAssistant.Shot(TypeSound.Button);

        if (!GameManager.NetworkAvailable)
        {
            PopupNoInternet.Show();
            return;
        }

        PlayScreen.Show();

        //   GameManager.Instance.GetPlayer.isPlayerDied = false;
    }
}