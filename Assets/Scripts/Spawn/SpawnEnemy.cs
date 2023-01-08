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
    private float repeat = 15;
    
    public GameObject enemy;
    public PlayerController player;
    
    //test
    private float xRangeLeft = -140;
    private float xRangeRight = 140;
    private float yRangeTop = 70;
    private float yRangeDown = -70;
  
    void Start()
    {
        InvokeRepeating("Spawn", timeDelay, repeat);
    }
    public Vector3 GetPosSpawnEnemy()
    {
        float x = 0;
        float y = 0;
        float z = 0;
        
        return new Vector3(x, y, z);
    }
    private void Spawn()
    {
       
        
            for (int i = 0; i < 10; i++)
            {
               GameObject Enemy = Instantiate(enemy, new Vector3(0, 0, -3.8f), enemy.transform.rotation);
               EnemyController enemyController = Enemy.GetComponent<EnemyController>();
               enemyController.ModelEnemy.transform.position = GetPosSpawnEnemy();

            }

        
    }
}
