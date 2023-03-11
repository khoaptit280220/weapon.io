using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyController : MonoBehaviour
{
    public AnimSkinEnemyController animEnemy;

    public EntityInfo entityInfo;
    public GameObject ModelEnemy;
    public RandomModelEnemy ModelSkin;
    public RandomWeaponEnemy WeaponEnemy;
    public bool isDied;
    private List<Vector3> WayPoints;
    public float speedEnemy = 20;
    private float speedE;
    public WayPointController WayPointController;
    private TweenerCore<Vector3, Vector3, VectorOptions> t;
    private int indexPoint;
    private int NewIndex;
    private int OldIndex;
    private bool firstStartRotate;
    private bool firstStartPosition;
    private Vector3 startPosition;
    public int pointEnemy = 0;

    public ParticleSystem trail;

    public GameObject snowPrefab;
    public GameObject dartPrefab;

    public int countHeadEnemy;
    [SerializeField] private int tier = 0;
    [SerializeField] private int pointTier = 0;
    private float timeE = 0;

    public bool checkShieldEnemy;
    public GameObject shield;

    public string enemyName;
    public TMP_Text nameEnemyTxt;

    public List<string> names = new List<string>()
    {
        "EpicGame", "Human", "BestPlayer", "Nam", "Phuong", "David", "Hue", "Ha", "Jony", "Tomat", "Linda", "Quynh",
        "Hop", "Long", "Nguyen", "Tom", "Jerry", "Petter", "Duc", "Moon"
    };

    private void Start()
    {
        int indexName = Random.Range(0, names.Count);
        enemyName = names[indexName];
        nameEnemyTxt.text = enemyName;

        entityInfo.name = enemyName;

        checkShieldEnemy = false;
        isDied = false;
        firstStartRotate = true;
        firstStartPosition = false;
        startPosition = ModelEnemy.transform.localPosition;
        DOTween.Sequence().AppendInterval(.1f).AppendCallback(() => { firstStartRotate = true; });
        indexPoint = 0;
        pointEnemy = 0;
        SetupWaypoint();
    }

    private void Update()
    {
        entityInfo.point = pointEnemy;
        MoveByPoint();
        if (GameManager.Instance.GameState == GameState.Pause || GameManager.Instance.GameState == GameState.Win)
        {
            isDied = true;
        }
        else
        {
            isDied = false;
        }

        ModelEnemy.transform.localPosition =
            new Vector3(ModelEnemy.transform.position.x, ModelEnemy.transform.position.y, 0);

        Scale();
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
            timeE += Time.deltaTime;
            if (timeE > 0.2f)
            {
                timeE = 0f;
                GameObject snow = Instantiate(snowPrefab,
                    new Vector3(this.ModelEnemy.transform.position.x, this.ModelEnemy.transform.position.y, 0),
                    snowPrefab.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                snow.SetActive(false);
                DOTween.Sequence().SetDelay(0.15f).OnComplete(() => { snow.SetActive(true); });
                DOTween.Sequence().SetDelay(4).OnComplete(() => { snow.SetActive(false); });
            }
        }

        if (Database.CurrentIdMap == 4)
        {
            timeE += Time.deltaTime;
            if (timeE > 0.4f)
            {
                timeE = 0f;
                GameObject dart = Instantiate(dartPrefab,
                    new Vector3(this.ModelEnemy.transform.position.x, this.ModelEnemy.transform.position.y, 0),
                    dartPrefab.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                dart.SetActive(false);
                DOTween.Sequence().SetDelay(0.15f).OnComplete(() => { dart.SetActive(true); });
                DOTween.Sequence().SetDelay(4).OnComplete(() => { dart.SetActive(false); });
            }
        }
    }

    public void SetupWaypoint()
    {
        WayPoints = WayPointController.GetListPosition();
        WayPointController.SpawnWayPoint(WayPoints);
    }

    public void MoveByPoint()
    {
        if (!isDied)
        {
            if (indexPoint == 3 || indexPoint == 9 || indexPoint == 15)
            {
                checkShieldEnemy = true;
                shield.SetActive(true);
            }
            else
            {
                checkShieldEnemy = false;
                shield.SetActive(false);
            }

            if (indexPoint == 2 || indexPoint == 6 || indexPoint == 10 || indexPoint == 14 || indexPoint == 18)
            {
                animEnemy.PlaySwinTrail();
                animEnemy.eye.PlaySpeedEye();
                speedE = 50;
                trail.gameObject.SetActive(true);
                SpawnTrail();
            }
            else
            {
                animEnemy.PlaySwin();
                animEnemy.eye.OffSpeedEye();
                trail.gameObject.SetActive(false);
                speedE = speedEnemy;
            }

            NewIndex = indexPoint;
            if (firstStartRotate)
            {
                ModelEnemy.transform.DOLookAt(WayPoints[indexPoint], .2f);
            }

            ModelEnemy.transform.position = Vector3.MoveTowards(ModelEnemy.transform.position,
                WayPoints[indexPoint], speedE * Time.deltaTime);
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

    private void Scale()
    {
        if (pointEnemy > (pointTier - 1 + 4 * 50 * (tier + 1)) && tier < 7)
        {
            pointTier = pointEnemy;
            tier += 1;
            float x = 2 * Mathf.Pow(1.2f, tier);
            ModelEnemy.transform.localScale = new Vector3(x, x, x);
        }
    }
}