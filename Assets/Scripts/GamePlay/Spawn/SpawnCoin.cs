using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnCoin : MonoBehaviour
{
    public GameObject coin;
    public GameObject treasure;
    public Transform parent;
    private float timeDelay = 1;

    private float repeat = 15;

    public GameObject torpedo;

    public GameObject shield;
    public GameObject weapon;
    public GameObject shoe;

    private bool checkspawnTorpedo = true;

    void Start()
    {
        InvokeRepeating("SpawnCoii", timeDelay, repeat);
        SpawnItem();
        SpawnTreasure();
    }

    private void SpawnCoii()
    {
        if (Database.CurrentIdMap == 5 && GameManager.Instance.time < 30)
        {
            for (int i = 0; i < 60; i++)
            {
                Instantiate(coin, new Vector3(Random.Range(-120, 120), Random.Range(-50, 140), 0),
                    coin.transform.rotation, parent);
            }

            if (checkspawnTorpedo == true)
            {
                checkspawnTorpedo = false;
                for (int i = 0; i < 15; i++)
                {
                    Instantiate(torpedo, new Vector3(Random.Range(-110, 110), Random.Range(-60, 140), 0),
                        torpedo.transform.rotation, parent);
                }
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(coin, new Vector3(Random.Range(-120, 120), 165, 0), coin.transform.rotation, parent);
            }
        }
    }

    private void SpawnTreasure()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(treasure, new Vector3(Random.Range(-110, 110), -78, 0), treasure.transform.rotation, parent);
        }
    }

    private void SpawnItem()
    {
        if (Database.CurrentIdMap == 7)
        {
            DOTween.Sequence().SetDelay(2).OnComplete(() =>
            {
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(shield, new Vector3(Random.Range(-120, 120), Random.Range(-70, 150), 0),
                        shield.transform.rotation, parent);
                    Instantiate(weapon, new Vector3(Random.Range(-120, 120), Random.Range(-70, 150), 0),
                        weapon.transform.rotation, parent);
                    Instantiate(shoe, new Vector3(Random.Range(-120, 120), Random.Range(-70, 150), 0),
                        shoe.transform.rotation, parent);
                }
            });
        }
    }
}