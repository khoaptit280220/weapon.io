using System;
using CnControls;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayScreen : UIPanel
{
    
    [SerializeField] private TMP_Text timer;
    
    [SerializeField] private Image energy;
    [SerializeField] private TMP_Text point;
    [SerializeField] private TMP_Text coin;

    [SerializeField] private Animator AnimTextBossComingControler;
    
    public static PlayScreen Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PlayScreen;
    }

    public static void Show()
    {
        var newInstance = (PlayScreen) GUIManager.Instance.NewPanel(UiPanelType.PlayScreen);
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
        PlayAnimTextBossComing();
    }

    public override void OnDisappear()
    {
        base.OnDisappear();
        Instance = null;
    }

    public void Fast()
    {
        Debug.Log("aaa");
    }
    private void OnEnable()
    {
        EventGlobalManager.Instance.OnEverySecondTick.AddListener(UpdateTimer);
        UpdateTimer();
    }
    private void OnDisable()
    {
        if (EventGlobalManager.Instance)
            EventGlobalManager.Instance.OnEverySecondTick.AddListener(UpdateTimer);
    }
    public void ShowPause()
    {
        AudioAssistant.Shot(TypeSound.Button);
        PopupPauseGame.Show();
    }

    void UpdateTimer()
    {
        if (GameManager.Instance.GameState==GameState.PLaying)
        {
            if (GameManager.Instance.time > 0)
            {
                GameManager.Instance.time -= 1;
            }
            timer.text = GameManager.Instance.time.ToString();

            if (GameManager.Instance.time == 0)
            {
                GameManager.Instance.OnWinGame();
            }
        }
        else
        {
            timer.text = GameManager.Instance.time.ToString();
        }
    }

    private void Update()
    {
        point.text = "" + GameManager.Instance.point;
        coin.text = "" + GameManager.Instance.coin;
        if (CnInputManager.GetButton("Jump"))
        {
            if (energy.fillAmount > 0)
            {
                energy.fillAmount -= Time.deltaTime * 0.5f;
                GameManager.Instance.energy = energy.fillAmount;
            }
        }
        if (GameManager.Instance.energy > 0 && energy.fillAmount < GameManager.Instance.energy)
        {
            energy.fillAmount = GameManager.Instance.energy;
        }
        
    }

    private void PlayAnimTextBossComing()
    {
        AnimTextBossComingControler.Play("AnimTextBossComing");
    }
}