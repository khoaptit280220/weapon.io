using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Horn : MonoBehaviour
{
    public HornController hornController;

    private void OnTriggerEnter(Collider other)
    {
        hornController.SetTriggerHorn(other);
    }
    
    public void Scale()
    {
        transform.localScale += new Vector3(0, 0.2f, 0);
        transform.localPosition += new Vector3(0, 0.2f, 0);
        
    }
}
