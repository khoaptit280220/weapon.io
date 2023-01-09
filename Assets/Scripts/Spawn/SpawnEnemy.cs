using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    private float timeDelay = 0.1f;
    private float repeat = 12;
    
    public GameObject enemy;
    public PlayerController player;
    
    //test
    private float xRangeLeft = -140;
    private float xRangeRight = 140;
    private float yRangeTop = 70;
    private float yRangeDown = -70;
  
    void Start()
    {
        //Spawn();
        InvokeRepeating("Spawn", timeDelay, repeat);
    }
    public Vector3 GetPosSpawnEnemy()
    {
        float x=0, y=0;
        float z=0;
        if (player.transform.position.x >= 0)
        {
            y = Random.Range(yRangeDown, yRangeTop);
            if (y > player.transform.position.y - 55 && y < player.transform.position.y + 55)
            {
                x = Random.Range(xRangeLeft, player.transform.position.x - 100);
            }
            else
            {
                x = Random.Range(xRangeLeft, xRangeRight);
            }
        }
        else if(player.transform.position.x < 0)
        {
            y = Random.Range(yRangeDown, yRangeTop);
            if (y > player.transform.position.y - 55 && y < player.transform.position.y + 55)
            {
                x = Random.Range(player.transform.position.x + 100, 140);
            }
            else
            {
                x = Random.Range(xRangeLeft, xRangeRight);
            }
        }
        return new Vector3(x, y, z);
    }
    private void Spawn()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject Enemy = Instantiate(enemy, new Vector3(0, 0, -3.8f), enemy.transform.rotation);
            EnemyController enemyController = Enemy.GetComponent<EnemyController>();
            enemyController.ModelEnemy.transform.position = GetPosSpawnEnemy();
            enemyController.ModelEnemy.transform.localScale = player.transform.localScale;

        }
    }
}
