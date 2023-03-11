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

    public void ScalePlayer()
    {
        transform.localScale += new Vector3(0, 0.15f, 0);
    }

    public void ScaleEnemy()
    {
        transform.localScale += new Vector3(0, 0, 0.2f);
    }
}