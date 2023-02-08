using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnCoin : MonoBehaviour
{
    public GameObject coin;
    public Transform parent;
    private float timeDelay = 1;

    private float repeat = 15;

    public GameObject torpedo;

    public GameObject shield;
    public GameObject weapon;
    public GameObject shoe;

    private bool checkspawnTorpedo = true;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnCoii", timeDelay, repeat);
        SpawnItem();
    }

    // Update is called once per frame
    private void SpawnCoii()
    {
        if (Database.CurrentIdMap == 5 && GameManager.Instance.time < 30)
        {
            for (int i = 0; i < 30; i++)
            {
                Instantiate(coin, new Vector3(Random.Range(-120, 120), Random.Range(-50, 73), -3), coin.transform.rotation, parent);
            }
            if (checkspawnTorpedo == true)
            {
                checkspawnTorpedo = false;
                for (int i = 0; i < 15; i++)
                {
                    Instantiate(torpedo, new Vector3(Random.Range(-110, 110), Random.Range(-60, 60), -3),
                        torpedo.transform.rotation, parent);
                }
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            { 
                Instantiate(coin, new Vector3(Random.Range(-120, 120), 73, -3), coin.transform.rotation, parent);
            }
        }
    }

    private void SpawnItem()
    {
        if (Database.CurrentIdMap == 7)
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(shield, new Vector3(Random.Range(-120, 120), Random.Range(-60, 60), -3),
                    shield.transform.rotation, parent);
                Instantiate(weapon, new Vector3(Random.Range(-120, 120), Random.Range(-60, 60), -3),
                    weapon.transform.rotation, parent);
                Instantiate(shoe, new Vector3(Random.Range(-120, 120), Random.Range(-60, 60), -3),
                    shoe.transform.rotation, parent);
            }
        }
    }
}
