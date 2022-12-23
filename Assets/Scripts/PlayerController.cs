using System;
using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float speedRotate = 20;
    private float xRangeLeft = -113;
    private float xRangeRight = 135;
    private float yRangeTop = 67;
    private float yRangeDown = -75;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 vectorDirectionMove;
    private float inputX = 0;
    private float inputY = 0;
    private bool firstJoystick;

    public DynamicJoystick dynamicJoystick;

    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        firstJoystick = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (dynamicJoystick.Horizontal != 0 || dynamicJoystick.Vertical != 0)
        {
         
            direction = new Vector3(dynamicJoystick.Horizontal, 0.0f, dynamicJoystick.Vertical);
            Vector3 _rotation = new Vector3(0,0
                , -Mathf.Atan2(dynamicJoystick.Horizontal, dynamicJoystick.Vertical) * Mathf.Rad2Deg);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_rotation),
                speedRotate * Time.deltaTime);

          //  transform.position += direction * speed * Time.deltaTime;
        }
    }

    // private void FixedUpdate()
    // {
    //     float inputX = CnInputManager.GetAxis("Horizontal");         
    //     float inputY = CnInputManager.GetAxis("Vertical");    
    //     Vector3 _rotation = new Vector3(0,0
    //         , -Mathf.Atan2(inputX, inputY) * Mathf.Rad2Deg);
    //   
    //     transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_rotation),
    //         speedRotate * Time.deltaTime);
    //   //  rb.velocity = vectorDirectionMove.normalized * speed;
    // }

    /*public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            
        }
    }*/
}
