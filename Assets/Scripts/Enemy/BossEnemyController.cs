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
    public int pointEnemyBoss;

   
    
    private void Start()
    {
        isDied = false;
        firstStartRotate = true;
        firstStartPosition = false;
        startPosition =ModelEnemy.transform.localPosition;
        DOTween.Sequence().AppendInterval(.1f).AppendCallback(() => { firstStartRotate = true; });
        indexPoint = 0;
        pointEnemyBoss = 0;
       // WayPoints = WayPointController.GetListPosition();
       // WayPointController.SpawnWayPoint(WayPoints);
        MoveByPoint();
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

    
}