using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class BossEnemyController : MonoBehaviour
{
    public GameObject ModelEnemy;
    public bool isDied;
    private List<Vector3> WayPoints = new List<Vector3>();
    public float speedBoss = 20;
    public bool moveLoop;
    public List<GameObject> Points;
    private TweenerCore<Vector3, Vector3, VectorOptions> t;
    private int indexPoint;
    private int NewIndex;
    private int OldIndex;
    private bool isTarget;
    private bool firstStartRotate;
    private bool firstStartPosition;
    private Vector3 startPosition;
    public int pointEnemyBoss;
    public WayPointController WayPointController;
    
    public int countHeadBoss;
    
    public ParticleSystem trail;
 //   public Transform parent;
    public GameObject snowPrefab;
    public GameObject dartPrefab;

    private float timeB = 0;
    public bool checkShieldBoss;
    public GameObject shield;
    private void Start()
    {
        checkShieldBoss = false;
        timeB = 0;
        isDied = false;
        firstStartRotate = true;
        firstStartPosition = false;
        startPosition =ModelEnemy.transform.localPosition;
        DOTween.Sequence().AppendInterval(.1f).AppendCallback(() => { firstStartRotate = true; });
        indexPoint = 0;
        pointEnemyBoss = 0;
        countHeadBoss = 0;
        SetupWaypoint();
        
        MoveByPoint();
    }

    private void Update()
    {
        if (checkShieldBoss == true)
        {
            shield.SetActive(true);
        }
        else
        {
            shield.SetActive(false);
        }
        
        if (speedBoss > 30)
        {
            SpawnTrail();
        }
    }

    private void SpawnTrail()
    {
        if (Database.CurrentIdMap == 3)
        {
            Debug.Log("boss spawn snow");
            timeB += Time.deltaTime;
            if (timeB > 0.4f)
            {
                timeB = 0f;
                GameObject snow = Instantiate(snowPrefab,
                    new Vector3(transform.position.x, transform.position.y, -2),
                    snowPrefab.transform.rotation);
                snow.SetActive(false);
                DOTween.Sequence().SetDelay(0.15f).OnComplete(() => { snow.SetActive(true); });
                DOTween.Sequence().SetDelay(4).OnComplete(() => { Destroy(snow); });
            }
        }

        if (Database.CurrentIdMap == 4)
        {
            timeB += Time.deltaTime;
            if (timeB > 0.4f)
            {
                timeB = 0f;
                GameObject dart = Instantiate(dartPrefab,
                    new Vector3(transform.position.x, transform.position.y, -2),
                    dartPrefab.transform.rotation);
                dart.SetActive(false);
                DOTween.Sequence().SetDelay(0.15f).OnComplete(() => { dart.SetActive(true); });
                DOTween.Sequence().SetDelay(4).OnComplete(() => { Destroy(dart); });
            }
        }
    }
    public void MoveByPoint()
    {
        if (!moveLoop)
        {
            if (!isDied) 
            {
                NewIndex = indexPoint;
                float timeMove = 0;
                if (firstStartPosition)
                {
                    timeMove = Vector3.Distance(WayPoints[OldIndex], WayPoints[NewIndex]) /
                               speedBoss;
                }
                else
                { 
                    timeMove = Vector3.Distance(startPosition, WayPoints[NewIndex]) /
                             speedBoss;
                    
                }
                if (firstStartRotate)
                {
                    ModelEnemy.transform.DOLookAt(WayPoints[indexPoint], .2f);
                }
            
                t = ModelEnemy.transform.DOLocalMove(WayPoints[indexPoint], timeMove).SetEase(Ease.Linear).OnComplete((() =>
                {
                    firstStartPosition = true;
                    if (indexPoint >= WayPoints.Count - 1)
                    {
                        isTarget = false;
                    }
            
                    if (indexPoint <= 0)
                    {
                        isTarget = true;
                    }
            
                    if (isTarget)
                    {
                        OldIndex = indexPoint;
                        indexPoint++;
                    }
                    else
                    {
                        OldIndex = indexPoint;
                        indexPoint--;
                    }
            
                    // if (isDied)
                    // {
                    //     return;
                    // }
            
                    MoveByPoint();
                }));
            }
        }
        else
        {
            if (!isDied)
            {
                if (indexPoint == 3 || indexPoint == 9 || indexPoint == 15)
                {
                    checkShieldBoss = true;
                }
                else
                {
                    checkShieldBoss = false;
                }
                if (indexPoint == 2 || indexPoint == 6 || indexPoint == 10 || indexPoint == 14 || indexPoint == 18)
                {
                    speedBoss = 50;
                    trail.gameObject.SetActive(true);
                }
                else
                {
                    speedBoss = 20;
                    trail.gameObject.SetActive(false);
                }
                NewIndex = indexPoint;
                float timeMove = 0;
               if (firstStartPosition)
                {
                    timeMove = Vector3.Distance(WayPoints[OldIndex], WayPoints[NewIndex]) /
                               speedBoss;
                }
                else
                { 
                    timeMove = Vector3.Distance(startPosition, WayPoints[NewIndex]) /
                             speedBoss;
                    
                }
                if (firstStartRotate)
                {
                    ModelEnemy.transform.DOLookAt(WayPoints[indexPoint], .2f);
                }

                t = ModelEnemy.transform.DOLocalMove(WayPoints[indexPoint], timeMove).SetEase(Ease.Linear)
                    .OnComplete((() =>
                    {
                        firstStartPosition = true;
                    OldIndex = indexPoint;
                    indexPoint++;
                    if (indexPoint >= WayPoints.Count)
                    {
                        indexPoint = 0;
                    }

                    // if ( isDied)
                    // {
                    //     return;
                    // }

                    MoveByPoint();
                }));
            }
        }
        
    }

    public void SetupWaypoint()
    {
        WayPoints = WayPointController.GetListPosition();
        WayPointController.SpawnWayPoint(WayPoints);
    }
}
