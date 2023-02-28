using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWin : UIPanel
{
    
    public static ScreenWin Instance { get; private set; }

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
        
    }

    

    public void Continue()
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