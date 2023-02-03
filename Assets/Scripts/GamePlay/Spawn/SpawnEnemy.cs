using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    private float timeDelay = 1f;
    private float repeat = 8;
    public Transform parent;
    public GameObject enemy;
    public PlayerController player;
    
    public GameObject _boss;
    //test
    private float xRangeLeft = -130;
    private float xRangeRight = 135;
    private float yRangeTop = 70;
    private float yRangeDown = -75;
  
   // public List<EnemyController> enemyList;
   // public List<BossEnemyController> bossList;
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
                x = Random.Range(player.transform.position.x + 100, xRangeRight);
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
        //enemyList.Clear();
        for (int i = 0; i < 10; i++)
        {
            GameObject Enemy = Instantiate(enemy, new Vector3(0, 0, -3.8f), enemy.transform.rotation, parent);
            EnemyController enemyController = Enemy.GetComponent<EnemyController>();
            enemyController.ModelEnemy.transform.position = GetPosSpawnEnemy();
            enemyController.ModelEnemy.transform.localScale = player.transform.localScale;
          //  enemyList.Add(enemyController);
        }
       // Init();
    }

    public void SpawnBoss()
    {
        GameObject obj = Instantiate(_boss, new Vector3(0, 0, -3.8f), _boss.transform.rotation, parent);
        BossEnemyController bossenemy = obj.GetComponent<BossEnemyController>();
        bossenemy.ModelEnemy.transform.position = GetPosSpawnEnemy();
       // bossList.Add(bossenemy);
    }

    // private void Init()
    // {
    //     PlayScreen.Instance.InitDirectionIndicator(enemyList);
    // }
}
