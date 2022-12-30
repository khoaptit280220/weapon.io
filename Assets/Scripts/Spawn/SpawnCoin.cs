using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnCoin : MonoBehaviour
{
    public GameObject coin;

    private float timeDelay = 1;

    private float repeat = 15;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnCoii", timeDelay, repeat);
    }

    // Update is called once per frame
    private void SpawnCoii()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(coin, new Vector3(Random.Range(-120, 120), 73, -1), coin.transform.rotation);

        }
    }

    
}
