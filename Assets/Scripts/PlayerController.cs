using System;
using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;
public class PlayerController : MonoBehaviour
{
    public float speed = 20;
    private float speed2 = 15;
    public float speedRotate = 20;

    [SerializeField] private Rigidbody rb;
    [DisableInEditorMode][SerializeField] private Vector3 direction;
    //private DynamicJoystick dynamicJoystick => (PopupController.Instance.Get<PopupInGame>() as PopupInGame).DynamicJoystick;

    public int tier = 0;
    [SerializeField] private int pointTier = 0;

    public BossEnemyController _boss;
    public bool checkActiveBoss;
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        SetDirection();
        //rb.velocity = direction.normalized * speed;
        if (transform.position.y > 70)
        {
            transform.position = new Vector3(transform.position.x, 70, transform.position.z);
        }
        if (transform.position.y < -70)
        {
            transform.position = new Vector3(transform.position.x, -70, transform.position.z);
        }
        if (transform.position.x > 130)
        {
            transform.position = new Vector3(130, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -130)
        {
            transform.position = new Vector3(-130, transform.position.y, transform.position.z);
        }

        transform.position += direction * speed * Time.deltaTime;
       
        ClickTrail();
        Scale();
    }
    private void SetDirection()
    {
        if (CnInputManager.GetAxis("Horizontal") != 0 || CnInputManager.GetAxis("Vertical") != 0)
        {
            direction = new Vector3(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"),0.0f);
            Debug.Log(CnInputManager.GetAxis("Horizontal"));
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
                speed2 = speed;
                speed = 60;
            }
            else
            {
                speed = speed2;
            }
        }
        else if (CnInputManager.GetButton("Jump") == false)
        {
            speed = speed2;
        }
    }

    private void Scale()
    {
        if(GameManager.Instance.point > (pointTier - 1 + 4*50*(tier+1)) && tier < 7)
        {
            pointTier = GameManager.Instance.point;
            tier += 1;
            speed += 5;
            speed2 = speed;
            checkActiveBoss = true;
            float x =  5 * Mathf.Pow(1.2f, tier);
            this.transform.localScale = new Vector3(x,x, x);
            Instantiate(_boss, new Vector3(this.transform.position.x + 30, 0, _boss.transform.position.z), _boss.transform.rotation);
            
        }
        else
        {
            checkActiveBoss = false;
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
