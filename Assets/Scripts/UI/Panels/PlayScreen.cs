using System;
using System.Collections.Generic;
using CnControls;
using DG.Tweening;
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
    [SerializeField] private Animator AnimTextEventMap2;
    [SerializeField] private Animator AnimTextEventMap5;
    [SerializeField] private Animator AnimTextEventMap6;

    private bool checkActiveEventMap2 = false;
    // [SerializeField]
    // private GameObject indicatorPrefab;
    //
    // [SerializeField]
    // private GameObject indicatorGroup;
    //
    // private Camera cam;
    // private Dictionary<EnemyController, GameObject> directionDic;
    // private List<GameObject> indicatorList;
    //
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
        PlayAnimText();
        AnimTextBossComingControler.gameObject.SetActive(false);
        AnimTextEventMap2.gameObject.SetActive(false);
        AnimTextEventMap5.gameObject.SetActive(false);
        AnimTextEventMap6.gameObject.SetActive(false);
        
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
        //UpdateDirectionIndicator();
        point.text = "" + GameManager.Instance.point;
        coin.text = "" + GameManager.Instance.coin;
        if (CnInputManager.GetButton("Jump"))
        {
            if (energy.fillAmount > 0)
            {
                if (checkActiveEventMap2 == true)
                {
                    energy.fillAmount = GameManager.Instance.energy;
                }
                else
                {
                    energy.fillAmount -= Time.deltaTime * 0.5f;
                    GameManager.Instance.energy = energy.fillAmount;
                }
                
            }
        }
        if (GameManager.Instance.energy > 0 && energy.fillAmount < GameManager.Instance.energy)
        {
            energy.fillAmount = GameManager.Instance.energy;
        }

        if (GameManager.Instance.checkBoss == true)
        {
            AnimTextBossComingControler.gameObject.SetActive(true);
            DOTween.Sequence().SetDelay(3).OnComplete(() =>
            {
                AnimTextBossComingControler.gameObject.SetActive(false);
            });
            GameManager.Instance.checkBoss = false;
        }
        
        if(Database.CurrentIdMap == 2)
        {
            if (GameManager.Instance.time % 10 == 0 && GameManager.Instance.time != 0)
            {
                checkActiveEventMap2 = true;
                AnimTextEventMap2.gameObject.SetActive(true);
                DOTween.Sequence().SetDelay(5).OnComplete(() =>
                {
                    AnimTextEventMap2.gameObject.SetActive(false);
                    checkActiveEventMap2 = false;
                });
            }
        }

        if(Database.CurrentIdMap == 5)
        {
            
            if (GameManager.Instance.time == 30)
            {
                AnimTextEventMap5.gameObject.SetActive(true);
                DOTween.Sequence().SetDelay(5).OnComplete(() =>
                {
                    AnimTextEventMap5.gameObject.SetActive(false);
                });
            }
        }

        if(Database.CurrentIdMap == 6)
        {
            if (GameManager.Instance.time == 52 || GameManager.Instance.time == 28)
            {
                AnimTextEventMap6.gameObject.SetActive(true);
                DOTween.Sequence().SetDelay(15).OnComplete(() =>
                {
                    AnimTextEventMap6.gameObject.SetActive(false);
                });
            }
        }
    }

    private void PlayAnimText()
    {
        AnimTextBossComingControler.Play("AnimTextBossComing");
        AnimTextEventMap2.Play("AnimTextEventMap2");
        AnimTextEventMap5.Play("AnimTextEventMap5");
        AnimTextEventMap6.Play("AnimTextEventMap6");
    }

}