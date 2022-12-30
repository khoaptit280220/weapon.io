public class PopupPauseGame : UIPanel
{
    public static PopupPauseGame Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupPauseGame;
    }

    public static void Show()
    {
        var newInstance = (PopupPauseGame) GUIManager.Instance.NewPanel(UiPanelType.PopupPauseGame);
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