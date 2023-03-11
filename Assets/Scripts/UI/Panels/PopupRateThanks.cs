#region

using HyperCatSdk;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class PopupRateThanks : UIPanel
{
    public override UiPanelType GetId()
    {
        return UiPanelType.PopupRateThanks;
    }

    public static void Show()
    {
        var newInstance = (PopupRateThanks)GUIManager.Instance.NewPanel(UiPanelType.PopupRateThanks);
        newInstance.OnAppear();
    }

    public override void OnAppear()
    {
        if (isInited)
            return;

        base.OnAppear();
    }
}