using System;
using System.Collections;
using System.Collections.Generic;
using CnControls;
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
    public float speedRotate = 10;
    [HideInInspector] public int countHeadPlayer;
    [HideInInspector] public bool isPlayerDied;
    [DisableInEditorMode][SerializeField] private Vector3 direction;
    
    public int tier = 0;
    [SerializeField] private int pointTier = 0;
    [SerializeField] private SpawnTrail spawnTrail;

    public ParticleSystem trail;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        GameManager.Instance.SetupPlayer(this);
        direction = Vector3.right;
    }

    public void Init()
    {
        isPlayerDied = false;
        countHeadPlayer = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameState == GameState.PLaying)
        {
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

            transform.position += direction * speed * Time.deltaTime;
       
            ClickTrail();
            Scale();

        }
    }
    private void SetDirection()
    {
        // float inputX = CnInputManager.GetAxis("Horizontal");         
        // float inputY = CnInputManager.GetAxis("Vertical");         
        // vectorDirectionMove = new Vector3(inputX, inputY, 0);        
        // vectorDirectionRotate = vectorDirectionMove;
        if (CnInputManager.GetAxis("Horizontal") != 0 && CnInputManager.GetAxis("Vertical") != 0)
        {
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
                    //spawnTrail.SpawnSnow(this.gameObject);
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
        if (other.gameObject.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            GameManager.Instance.point += 20;
            GameManager.Instance.energy += 0.3f;
            if (GameManager.Instance.energy > 1)
            {
                GameManager.Instance.energy = 1;
            }
            if (GameManager.Instance.energy < 0)
            {
                GameManager.Instance.energy = 0;
            }
        }
        if (other.gameObject.CompareTag("coin"))
        {
            Destroy(other.gameObject);
            GameManager.Instance.coin += 1;
        }
    }
    
}
