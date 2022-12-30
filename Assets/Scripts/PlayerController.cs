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
    public float speedRotate = 20;
    
    [SerializeField] private Rigidbody rb;
    [DisableInEditorMode][SerializeField] private Vector3 direction;
    
    [SerializeField] private int tier = 0;
    [SerializeField] private int pointTier = 0;
    //public int point = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        SetDirection();
        rb.velocity = direction.normalized * speed;
        ClickTrail();
        
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
        Scale();
    }
    private void ClickTrail()
    {
        if (CnInputManager.GetButton("Jump") )
        {
            if (GameManager.Instance.energy > 0)
            {
                speed = 60;
            }
            else
            {
                speed = 15;
            }
            
        }
        else if (CnInputManager.GetButton("Jump") == false)
        {
            speed = 15;
        }
    }

    private void Scale()
    {
        if(GameManager.Instance.point > (pointTier - 1 + 4*50*(tier+1)) && tier < 7)
        {
            pointTier = GameManager.Instance.point;
            tier += 1;
            float x =  5 * Mathf.Pow(1.2f, tier);
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
