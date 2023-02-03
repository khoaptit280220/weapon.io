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
    public float speedEnemy = 20;
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
   
    public ParticleSystem trail;
    
    public int countHeadEnemy;
    [SerializeField] private int tier = 0;
    [SerializeField] private int pointTier = 0;

   // public GameObject Indicator;
    //public GameObject target;

   // private Renderer rd;
    
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

      //  rd = GetComponent<Renderer>();
    }

    private void Update()
    {
        Scale();
        // Debug.Log(rd.isVisible);
        // if (rd.isVisible == false)
        // {
        //     Debug.Log("isvisible = false");
        //     if (Indicator.activeSelf == false)
        //     {
        //          Indicator.SetActive(true);
        //     }
        //
        //     Vector3 vectorToPlayer = target.transform.position - transform.position;
        //     Vector2 thisEnemy = new Vector2(transform.position.x, transform.position.y);
        //     Vector2 direction = new Vector2(vectorToPlayer.x, vectorToPlayer.y);
        //     RaycastHit2D ray = Physics2D.Raycast(thisEnemy, direction);
        //
        //     if (ray.collider != null)
        //     {
        //         Indicator.transform.position = ray.point;
        //     }
        // }
        // else
        // {
        //     if (Indicator.activeSelf == true)
        //     {
        //         Indicator.SetActive(false);
        //     }
        // }
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
                if (indexPoint == 2 || indexPoint == 6 || indexPoint == 10 || indexPoint == 14 || indexPoint == 18)
                {
                    speedEnemy = 55;
                    trail.gameObject.SetActive(true);
                }
                else
                {
                    speedEnemy = 20;
                    trail.gameObject.SetActive(false);
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
            float x =  4 * Mathf.Pow(1.2f, tier);
            ModelEnemy.transform.localScale = new Vector3(x,x, x);
        }
    }

    
}
