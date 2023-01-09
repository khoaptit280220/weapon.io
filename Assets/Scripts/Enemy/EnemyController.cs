using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public GameObject ModelEnemy;
    public bool isDied;
   private List<Vector3> WayPoints;
    public float speedEnemy = 15;
    public bool moveLoop;
    public WayPointController WayPointController;
    private TweenerCore<Vector3, Vector3, VectorOptions> t;
    private int indexPoint;
    private int NewIndex;
    private int OldIndex;
    private bool isTarget;
    private bool firstStartRotate;
    private bool firstStartPosition;
    private Vector3 startPosition;
    public int pointEnemy;

    [SerializeField] private int tier = 0;
    [SerializeField] private int pointTier = 0;
    private void Start()
    {
        isDied = false;
        firstStartRotate = true;
        firstStartPosition = false;
        startPosition =ModelEnemy.transform.localPosition;
        DOTween.Sequence().AppendInterval(.1f).AppendCallback(() => { firstStartRotate = true; });
        indexPoint = 0;
        pointEnemy = 0;
        SetupWaypoint();
        MoveByPoint();
    }

    private void Update()
    {
        Scale();
    }

    public void SetupWaypoint()
    {
        WayPoints = WayPointController.GetListPosition();
         WayPointController.SpawnWayPoint(WayPoints);
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
                               speedEnemy;
                }
                else
                { 
                     timeMove = Vector3.Distance(startPosition, WayPoints[NewIndex]) /
                              speedEnemy;
                     
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
                if (indexPoint == 2 || indexPoint == 7 || indexPoint == 12 || indexPoint == 17)
                {
                    speedEnemy = 60;
                }
                else
                {
                    speedEnemy = 25;
                }
                NewIndex = indexPoint;
                float timeMove = 0;
               if (firstStartPosition)
                {
                    timeMove = Vector3.Distance(WayPoints[OldIndex], WayPoints[NewIndex]) /
                               speedEnemy;
                }
                else
                { 
                    timeMove = Vector3.Distance(startPosition, WayPoints[NewIndex]) /
                             speedEnemy;
                    
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
    
    private void Scale()
    {
        if(pointEnemy > (pointTier - 1 + 4*50*(tier+1)) && tier < 7)
        {
            pointTier = pointEnemy;
            tier += 1;
            float x =  5 * Mathf.Pow(1.2f, tier);
            ModelEnemy.transform.localScale = new Vector3(x,x, x);
        }
    }

    
}
