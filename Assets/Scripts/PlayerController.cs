using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    private float xRangeLeft = -113;
    private float xRangeRight = 135;
    private float yRangeTop = 67;
    private float yRangeDown = -75;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position + Vector3.up * (Time.deltaTime * speed));
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            rb.MoveRotation(transform.rotation);
            //Quaternion.LookRotation(Vector3.forward);
            // Vector3 rotationVector = new Vector3(0, 0, 90);
            // Quaternion rotation = Quaternion.Euler(rotationVector);
        }
    }
}
