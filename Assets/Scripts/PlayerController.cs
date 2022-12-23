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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float inputX = CnInputManager.GetAxis("Horizontal");         
        float inputY = CnInputManager.GetAxis("Vertical");         
        vectorDirectionMove = new Vector3(inputX, inputY, 0f);
        rb.velocity = vectorDirectionMove.normalized * speed;
    }

    /*public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            
        }
    }*/
}
