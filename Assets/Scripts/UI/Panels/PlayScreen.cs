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
        
        // cam = Camera.main;
        // indicatorList = new List<GameObject>();
        // directionDic = new Dictionary<EnemyController, GameObject>();
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
                GameManager.Instance.checkBoss = false;
            });
        }
        
        //if (GameManager.Instance.GetMapController.isMap2 == true)
        if(checkActiveEventMap2 == true)
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

        //if (GameManager.Instance.GetMapController.isMap5 == true)
        if(checkActiveEventMap2 == true)
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

        //if (GameManager.Instance.GetMapController.isMap6 == true)
        if(checkActiveEventMap2 == true)
        {
            if (GameManager.Instance.time == 50 || GameManager.Instance.time == 25)
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

    
    // public void InitDirectionIndicator(List<EnemyController> enemyList)
    // {
    //     foreach(EnemyController enemychild in enemyList)
    //     {
    //         if(enemychild != null)
    //         {
    //             GameObject indicator = Instantiate(indicatorPrefab, new Vector3(0,0,0), indicatorPrefab.transform.rotation);
    //             indicatorList.Add(indicator);
    //             indicator.SetActive(true);
    //             directionDic.Add(enemychild, indicator);
    //         }
    //     }
    // }
    //
    // public void UpdateDirectionIndicator()
    // {
    //     foreach (KeyValuePair<EnemyController, GameObject> entry in directionDic)
    //     {
    //         if(directionDic.TryGetValue(entry.Key, out GameObject botIndicator))
    //         {
    //             Vector3 botCharacterPos = entry.Key.gameObject.transform.position;
    //
    //             Vector3 viewPos = cam.WorldToViewportPoint(botCharacterPos);
    //             if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
    //             {
    //                 // Your object is in the range of the camera, you can apply your behaviour
    //                 botIndicator.SetActive(false);
    //             }
    //             else
    //             {
    //                 botIndicator.SetActive(true);
    //             }
    //
    //             Vector3 vectorToPlayer = botCharacterPos - GameManager.Instance.GetPlayer.gameObject.transform.position;
    //             //HCDebug.Log("vector to player: " + vectorToPlayer);
    //
    //             float xAbs = Math.Abs(vectorToPlayer.x);
    //             float yAbs = Math.Abs(vectorToPlayer.y);
    //
    //             Vector2 uiVector = new Vector2();
    //
    //             if(yAbs <= xAbs)
    //             {
    //                 if (vectorToPlayer.x < 0) uiVector.x = -940;
    //                 else uiVector.x = 940;
    //
    //                 if (vectorToPlayer.y >= 25f)
    //                 {
    //                     uiVector.y = 520;
    //                 }
    //                 else if (vectorToPlayer.y <= -25f)
    //                 {
    //                     uiVector.y = -520;
    //                 }
    //                 else
    //                 {
    //                     uiVector.y = vectorToPlayer.y / 25f * 520;
    //                 }
    //             }
    //             else
    //             {
    //                 if (vectorToPlayer.y < 0) uiVector.y = -520;
    //                 else uiVector.y = 520;
    //
    //                 if (vectorToPlayer.x >= 40f)
    //                 {
    //                     uiVector.x = 940;
    //                 }
    //                 else if (vectorToPlayer.x <= -40)
    //                 {
    //                     uiVector.x = -940;
    //                 }
    //                 else
    //                 {
    //                     uiVector.x = vectorToPlayer.x / 40 * 940;
    //                 }
    //             }
    //             
    //             botIndicator.transform.localPosition = new Vector3(uiVector.x, uiVector.y, 0);
    //             float angle = Mathf.Atan2(uiVector.y, uiVector.x) * Mathf.Rad2Deg;
    //             botIndicator.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    //         }
    //     }
    // }
    // public void RemoveItemInIndicatorDic(EnemyController enemydelete)
    // {
    //     if (directionDic.TryGetValue(enemydelete, out GameObject botIndicator))
    //     {
    //         Destroy(botIndicator);
    //     }
    //     directionDic.Remove(enemydelete);
    // }

}