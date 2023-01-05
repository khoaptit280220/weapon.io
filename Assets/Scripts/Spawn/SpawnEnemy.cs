using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{

    private float timeDelay = 0.1f;
    private float repeat = 15;
    
    public GameObject enemy;
    public GameObject pointPrefabs;
    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("Spawn", timeDelay, repeat);
    }

    private void Spawn()
    {
        for (int i = 0; i < 5; i++)
        {


            Instantiate(enemy, new Vector3(0, 0, -3.8f), enemy.transform.rotation);
        }
    }
}
