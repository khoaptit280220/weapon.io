using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLose : UIPanel
{
    
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
        
    }

    

    public void ReMatch()
    {
        AudioAssistant.Shot(TypeSound.Button);
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