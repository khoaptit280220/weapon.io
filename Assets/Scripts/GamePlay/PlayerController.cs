using System;
using System.Collections;
using System.Collections.Generic;
using CnControls;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;
public class PlayerController : MonoBehaviour
{
    private float Top = 70;
    private float Down = -75;
    private float Left = -135;
    private float Right = 135;
    
    public float speed = 20;
    private float speed2 = 20;
    private float speedRotate = 20;

    private bool checkFisrtJoy = false;
    
    float time = 0f;
    public bool checkShield;
    public GameObject shield;

    [HideInInspector] public int countHeadPlayer;
    [HideInInspector] public bool isPlayerDied;
    [DisableInEditorMode][SerializeField] private Vector3 direction;
    
    public int tier = 0;
    [SerializeField] private int pointTier = 0;
    [SerializeField] private SpawnTrail spawnTrail;
    [SerializeField] private SpawnEnemy spawnEnemy;
    public ParticleSystem trail;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        GameManager.Instance.SetupPlayer(this);
    }

    public void Init()
    {
        checkShield = true;
        isPlayerDied = false;
        countHeadPlayer = 0;
        checkFisrtJoy = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameState == GameState.PLaying)
        {
            if (checkShield == true)
            {
                shield.SetActive(true);
            }
            else
            {
                shield.SetActive(false);
            }
            if (GameManager.Instance.time == 55)
            {
                checkShield = false;
            }
            SetDirection();
            if (transform.position.y > Top)
            {
                transform.position = new Vector3(transform.position.x, Top, transform.position.z);
            }
            if (transform.position.y < Down)
            {
                transform.position = new Vector3(transform.position.x, Down, transform.position.z);
            }
            if (transform.position.x > Right)
            {
                transform.position = new Vector3(Right, transform.position.y, transform.position.z);
            }
            if (transform.position.x < Left)
            {
                transform.position = new Vector3(Left, transform.position.y, transform.position.z);
            }

            if (checkFisrtJoy == false)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }

            if (checkFisrtJoy == true)
            {
                transform.position += direction * speed * Time.deltaTime;
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
        if (CnInputManager.GetAxis("Horizontal") != 0 && CnInputManager.GetAxis("Vertical") != 0)
        {
            checkFisrtJoy = true;
            direction = new Vector3(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"),0.0f);
            Vector3 _rotation = new Vector3(0,0
                , -Mathf.Atan2(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical")) * Mathf.Rad2Deg);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_rotation),
                speedRotate * Time.deltaTime);
        }
    }
    private void ClickTrail()
    {
        if (CnInputManager.GetButton("Jump") )
        {
            if (GameManager.Instance.energy > 0)
            {
                speed = 60;
                trail.gameObject.SetActive(true);
                if (Database.CurrentIdMap == 3)
                {
                    time += Time.deltaTime;
                    if (time > 0.3f)
                    {
                        time = 0f;
                        spawnTrail.SpawnSnow(this.gameObject);   
                    }
                }
                if (Database.CurrentIdMap == 4)
                {
                    time += Time.deltaTime;
                    if (time > 0.3f)
                    {
                        time = 0f;
                        spawnTrail.SpawnDart(this.gameObject);   
                    }
                }
            }
            else
            {
                speed = speed2;
                trail.gameObject.SetActive(false);
            }
        }
        else if (CnInputManager.GetButton("Jump") == false)
        {
            speed = speed2;
            trail.gameObject.SetActive(false);
        }
    }
    private void Scale()
    {
        if(GameManager.Instance.point > (pointTier - 1 + 4*50*(tier+1)) && tier < 7)
        {
            pointTier = GameManager.Instance.point;
            tier += 1;
            float x =  4 * Mathf.Pow(1.2f, tier);
            this.transform.localScale = new Vector3(x,x, x);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Snow"))
        {
            speed = 10;
            speed2 = 10;
            DOTween.Sequence().SetDelay(3).OnComplete(() =>
            {
                speed2 = 20;
            });
        }

        if (other.gameObject.CompareTag("Shoe"))
        {
            speed = 35;
            speed2 = 35;
            DOTween.Sequence().SetDelay(3).OnComplete(() =>
            {
                speed2 = 20;
            });
        }
    }
    
}
