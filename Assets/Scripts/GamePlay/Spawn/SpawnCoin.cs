using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnCoin : MonoBehaviour
{
    public GameObject coin;
    public Transform parent;
    private float timeDelay = 1;

    private float repeat = 15;

    public GameObject torpedo;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnCoii", timeDelay, repeat);
    }

    // Update is called once per frame
    private void SpawnCoii()
    {
        //if (GameManager.Instance.GetMapController.isMap5 == false)
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(coin, new Vector3(Random.Range(-120, 120), 73, -3), coin.transform.rotation, parent);
            }
        }
      //else
        // {
        //     for (int i = 0; i < 20; i++)
        //     {
        //         Instantiate(coin, new Vector3(Random.Range(-120, 120), Random.Range(50, 73), -3), coin.transform.rotation, parent);
        //     }
        // }
    }

    private void SpawnTorpedo()
    {
        if (GameManager.Instance.GetMapController.isMap5 == true)
        {
            for (int i = 0; i < 20; i++)
            {
                Instantiate(torpedo, new Vector3(Random.Range(-110, 110), Random.Range(-60, 60), -1),
                    torpedo.transform.rotation, parent);
            }
        }
    }

    
}
