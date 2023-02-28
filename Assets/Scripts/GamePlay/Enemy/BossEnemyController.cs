using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;

public class BossEnemyController : MonoBehaviour
{
    public EntityInfo entityInfo;

    public GameObject ModelEnemy;
    public bool isDied;
    private List<Vector3> WayPoints = new List<Vector3>();
    public float speedBoss = 20;
    private float speedB;
    private TweenerCore<Vector3, Vector3, VectorOptions> t;
    private int indexPoint;
    private int NewIndex;
    private int OldIndex;
    private bool firstStartRotate;
    private bool firstStartPosition;
    private Vector3 startPosition;
    public int pointEnemyBoss;
    public WayPointController WayPointController;

    public int countHeadBoss;

    public ParticleSystem trail;

    public GameObject snowPrefab;
    public GameObject dartPrefab;

    private float timeB = 0;
    public bool checkShieldBoss;
    public GameObject shield;

    public string bossName;
    public TMP_Text nameBossTxt;

    private void Start()
    {
        bossName = "Boss";
        nameBossTxt.text = bossName;
        entityInfo.name = bossName;

        checkShieldBoss = false;
        timeB = 0;
        isDied = false;
        firstStartRotate = true;
        firstStartPosition = false;
        startPosition = ModelEnemy.transform.localPosition;
        DOTween.Sequence().AppendInterval(.1f).AppendCallback(() => { firstStartRotate = true; });
        indexPoint = 0;
        pointEnemyBoss = 0;
        countHeadBoss = 0;
        SetupWaypoint();
    }

    private void Update()
    {
        entityInfo.point = pointEnemyBoss;

        MoveByPoint();
        if (GameManager.Instance.GameState == GameState.Pause)
        {
            isDied = true;
        }
        else
        {
            isDied = false;
        }

        if (firstStartPosition)
        {
            if (WayPoints[NewIndex].x < WayPoints[OldIndex].x)
            {
                ModelEnemy.transform.localEulerAngles = new Vector3(ModelEnemy.transform.localEulerAngles.x,
                    ModelEnemy.transform.localEulerAngles.y, 180);
            }

            if (WayPoints[NewIndex].x > WayPoints[OldIndex].x)
            {
                ModelEnemy.transform.localEulerAngles = new Vector3(ModelEnemy.transform.localEulerAngles.x,
                    ModelEnemy.transform.localEulerAngles.y, 0);
            }
        }
        else
        {
            if (WayPoints[NewIndex].x < startPosition.x)
            {
                ModelEnemy.transform.eulerAngles = new Vector3(ModelEnemy.transform.eulerAngles.x,
                    ModelEnemy.transform.eulerAngles.y, 180);
            }

            if (WayPoints[NewIndex].x > startPosition.x)
            {
                ModelEnemy.transform.localEulerAngles = new Vector3(ModelEnemy.transform.localEulerAngles.x,
                    ModelEnemy.transform.localEulerAngles.y, 0);
            }
        }
    }

    private void SpawnTrail()
    {
        if (Database.CurrentIdMap == 3)
        {
            timeB += Time.deltaTime;
            if (timeB > 0.2f)
            {
                timeB = 0f;
                GameObject snow = Instantiate(snowPrefab,
                    new Vector3(transform.position.x, transform.position.y, 0),
                    snowPrefab.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                snow.SetActive(false);
                DOTween.Sequence().SetDelay(0.15f).OnComplete(() => { snow.SetActive(true); });
                DOTween.Sequence().SetDelay(4).OnComplete(() => { Destroy(snow); });
            }
        }

        if (Database.CurrentIdMap == 4)
        {
            timeB += Time.deltaTime;
            if (timeB > 0.3f)
            {
                timeB = 0f;
                GameObject dart = Instantiate(dartPrefab,
                    new Vector3(transform.position.x, transform.position.y, 0),
                    dartPrefab.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                dart.SetActive(false);
                DOTween.Sequence().SetDelay(0.15f).OnComplete(() => { dart.SetActive(true); });
                DOTween.Sequence().SetDelay(4).OnComplete(() => { Destroy(dart); });
            }
        }
    }

    public void MoveByPoint()
    {
        if (!isDied)
        {
            if (indexPoint == 3 || indexPoint == 9 || indexPoint == 15)
            {
                checkShieldBoss = true;
                shield.SetActive(true);
            }
            else
            {
                checkShieldBoss = false;
                shield.SetActive(false);
            }

            if (indexPoint == 2 || indexPoint == 6 || indexPoint == 10 || indexPoint == 14 || indexPoint == 18)
            {
                speedB = 50;
                trail.gameObject.SetActive(true);
                SpawnTrail();
            }
            else
            {
                speedB = speedBoss;
                trail.gameObject.SetActive(false);
            }

            NewIndex = indexPoint;

            if (firstStartRotate)
            {
                ModelEnemy.transform.DOLookAt(WayPoints[indexPoint], .2f);
            }

            ModelEnemy.transform.position = Vector3.MoveTowards(ModelEnemy.transform.position,
                WayPoints[indexPoint], speedB * Time.deltaTime);
            if (ModelEnemy.transform.position == WayPoints[indexPoint])
            {
                firstStartRotate = true;
                firstStartPosition = true;
                OldIndex = indexPoint;
                indexPoint++;
                if (indexPoint >= WayPoints.Count)
                {
                    indexPoint = 0;
                }
            }
        }
    }

    public void SetupWaypoint()
    {
        WayPoints = WayPointController.GetListPosition();
        WayPointController.SpawnWayPoint(WayPoints);
    }
}