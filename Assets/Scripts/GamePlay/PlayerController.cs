using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CnControls;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;
using TMPro;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : HCMonoBehaviour
{
    public AnimSkinController anim;
    public EntityInfo entityInfo;
    private float Top = 165;
    private float Down = -80;
    private float Left = -125;
    private float Right = 125;

    public float speed = 20;
    private float speed2 = 20;
    private float speedTrail = 60;
    private float speedRotate = 20;
    private float speedTemp;

    float time = 0f;
    [HideInInspector] public bool checkShield;
    public GameObject shield;
    public GameObject rangeSuck;

    [HideInInspector] public int countHeadPlayer;
    [HideInInspector] public bool isPlayerDied;
    [HideInInspector] public float timeShield;
    [DisableInEditorMode] [SerializeField] private Vector3 direction;

    public int tier = 0;
    [SerializeField] private int pointTier = 0;
    [SerializeField] private SpawnTrail spawnTrail;
    [SerializeField] private SpawnEnemy spawnEnemy;
    private List<Eye> listEyes = new List<Eye>();
    public ParticleSystem trail;
    private Transform t;

    public string playerName;

    public TMP_Text nameTxt;

    // Start is called before the first frame update
    private void Awake()
    {
        t = GetComponent<PlayerController>().transform;
    }

    void Start()
    {
        Init();
        GameManager.Instance.SetupPlayer(this);
    }

    public void Init()
    {
        timeShield = 5;
        isPlayerDied = false;
        countHeadPlayer = 0;
        SetSpeed();
        SetHorn();
        OnRangeSuck();
        speedTemp = speed2;
        playerName = Gm.data.user.name;
        entityInfo.name = playerName;
        nameTxt.text = playerName;
        listEyes = GetComponentsInChildren<Eye>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerDied == true) anim.eye.ExplodeEye();
        entityInfo.point = GameManager.Instance.point;
        if (GameManager.Instance.GameState == GameState.PLaying)
        {
            OnShield();
            SetDirection();
            t.position += t.right * speed * Time.deltaTime;
            if (t.position.y > Top)
            {
                t.position = new Vector3(t.position.x, Top, t.position.z);
            }

            if (t.position.y < Down)
            {
                t.position = new Vector3(t.position.x, Down, t.position.z);
            }

            if (t.position.x > Right)
            {
                t.position = new Vector3(Right, t.position.y, t.position.z);
            }

            if (t.position.x < Left)
            {
                t.position = new Vector3(Left, t.position.y, t.position.z);
            }

            ClickTrail();
            Scale();
            if (GameManager.Instance.checkBoss == true)
            {
                spawnEnemy.SpawnBoss();
            }
        }
    }

    private void SetDirection()
    {
        if (CnInputManager.GetAxis("Horizontal") != 0 || CnInputManager.GetAxis("Vertical") != 0)
        {
            direction = new Vector3(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"), 0.0f);
            Vector3 _rotation = new Vector3(0, 0
                , Mathf.Atan2(CnInputManager.GetAxis("Vertical"), CnInputManager.GetAxis("Horizontal")) * Mathf.Rad2Deg);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_rotation),
                speedRotate * Time.deltaTime);
        }

        // if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        // {
        //     skin.transform.localEulerAngles = new Vector3(0, 135, 180);
        //     skin.transform.localPosition = new Vector3(0,1.9f, 0);
        // }
        // if (transform.eulerAngles.z > 270 || transform.eulerAngles.z < 90)
        // {
        //     skin.transform.localEulerAngles = new Vector3(0, 135, 0);
        //     skin.transform.localPosition = new Vector3(0,0,0);
        // }
    }

    private void ClickTrail()
    {
        if (CnInputManager.GetButton("Jump"))
        {
            if (GameManager.Instance.energy > 0)
            {
                anim.PlaySwinTrail();
                anim.eye.PlaySpeedEye();
                speed = speedTrail;
                trail.gameObject.SetActive(true);
                if (Database.CurrentIdMap == 3)
                {
                    time += Time.deltaTime;
                    if (time > 0.15f)
                    {
                        time = 0f;
                        spawnTrail.SpawnSnow(this.gameObject);
                    }
                }

                if (Database.CurrentIdMap == 4)
                {
                    time += Time.deltaTime;
                    if (time > 0.2f)
                    {
                        time = 0f;
                        spawnTrail.SpawnDart(this.gameObject);
                    }
                }

                if (Database.CurrentIdMap != 3 && Database.CurrentIdModelSkin == 6)
                {
                    time += Time.deltaTime;
                    if (time > 0.15f)
                    {
                        time = 0f;
                        spawnTrail.SpawnSnow(this.gameObject);
                    }
                }

                if (Database.CurrentIdMap != 4 && Database.CurrentIdModelSkin == 9)
                {
                    time += Time.deltaTime;
                    if (time > 0.2f)
                    {
                        time = 0f;
                        spawnTrail.SpawnDart(this.gameObject);
                    }
                }

                if (Database.CurrentIdModelSkin == 12)
                {
                    time += Time.deltaTime;
                    if (time > 0.25f)
                    {
                        time = 0f;
                        spawnTrail.SpawnTorpedo(this.gameObject);
                    }
                }
            }
            else
            {
                anim.PlaySwin();
                anim.eye.OffSpeedEye();
                speed = speed2;
                trail.gameObject.SetActive(false);
            }
        }
        else if (CnInputManager.GetButton("Jump") == false)
        {
            anim.PlaySwin();
            anim.eye.OffSpeedEye();
            speed = speed2;
            trail.gameObject.SetActive(false);
        }
    }

    private void Scale()
    {
        if (GameManager.Instance.point > (pointTier - 1 + 4 * 50 * (tier + 1)) && tier < 7)
        {
            pointTier = GameManager.Instance.point;
            tier += 1;
            float x = 3 * Mathf.Pow(1.2f, tier);
            this.transform.localScale = new Vector3(x, x, x);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Snow"))
        {
            speed2 = 8;
            DOTween.Sequence().SetDelay(3).OnComplete(() => { speed2 = speedTemp; });
        }

        if (other.gameObject.CompareTag("Shoe"))
        {
            speed2 = 35;
            DOTween.Sequence().SetDelay(3).OnComplete(() => { speedTemp = speed2; });
        }
    }

    private void OnShield()
    {
        timeShield -= Time.deltaTime;
        if (timeShield > 0) checkShield = true;

        if (timeShield < 0) timeShield = 0;

        if (timeShield == 0) checkShield = false;

        if (checkShield == true) shield.SetActive(true);
        else shield.SetActive(false);

        if (Database.CurrentIdModelSkin == 8)
        {
            if (GameManager.Instance.time == 45 || GameManager.Instance.time == 30 || GameManager.Instance.time == 15)
            {
                timeShield = 5;
            }
        }

        if (Database.CurrentIdModelSkin == 15)
        {
            if (GameManager.Instance.time == 45 || GameManager.Instance.time == 25)
            {
                timeShield = 10;
            }
        }

        if (Database.CurrentIdModelSkin == 19)
        {
            if (GameManager.Instance.time == 50 || GameManager.Instance.time == 35 || GameManager.Instance.time == 20)
            {
                timeShield = 10;
            }
        }
    }

    private void OnRangeSuck()
    {
        if (Database.CurrentIdModelSkin == 23)
        {
            rangeSuck.SetActive(true);
        }
        else
        {
            rangeSuck.SetActive(false);
        }
    }

    private void SetSpeed()
    {
        if (Database.CurrentIdModelSkin == 10)
        {
            speed2 = 20 * 1.05f;
        }

        if (Database.CurrentIdModelSkin == 14)
        {
            speed2 = 20 * 1.1f;
        }

        if (Database.CurrentIdModelSkin == 18)
        {
            speed2 = 20 * 1.15f;
        }

        if (Database.CurrentIdModelSkin == 22)
        {
            speed2 = 20 * 1.2f;
        }
    }

    private void SetHorn()
    {
        if (Database.CurrentIdModelSkin == 11)
        {
            gameObject.GetComponentInChildren<Horn>().transform.localScale += new Vector3(0, 0, 0.05f);
        }

        if (Database.CurrentIdModelSkin == 16)
        {
            gameObject.GetComponentInChildren<Horn>().transform.localScale += new Vector3(0, 0, 0.1f);
        }

        if (Database.CurrentIdModelSkin == 20)
        {
            gameObject.GetComponentInChildren<Horn>().transform.localScale += new Vector3(0, 0, 0.15f);
        }
    }
}