using System;
using CnControls;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayScreen : UIPanel
{
    
    [SerializeField] private TMP_Text timer;
    [SerializeField]private int timeGame = 60;
    [SerializeField] private Image energy;
    [SerializeField] private TMP_Text point;
    [SerializeField] private TMP_Text coin;
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

    void UpdateTimer()
    {
        if (timeGame > 0)
        {
            timeGame -= 1;
        }
        timer.text = timeGame.ToString();

        if (timeGame == 0)
        {
            Debug.Log("Victory");   
        }
        
        
    }

    private void Update()
    {
        point.text = "" + GameManager.Instance.point;
        coin.text = "" + GameManager.Instance.coin;
        //GameManager.Instance.energy = energy.fillAmount;
        if (CnInputManager.GetButton("Jump"))
        {
            if (energy.fillAmount > 0)
            {
                energy.fillAmount -= Time.deltaTime * 0.5f;
                GameManager.Instance.energy = energy.fillAmount;
            }
            // if (energy.fillAmount == 0)
            // {
            //     GameManager.Instance.energy = 0;
            // }
        }
        
        
        if (GameManager.Instance.energy > 0 && energy.fillAmount < GameManager.Instance.energy)
        {
            energy.fillAmount = GameManager.Instance.energy;
        }
    }
}