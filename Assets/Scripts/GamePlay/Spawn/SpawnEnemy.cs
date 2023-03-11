using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    private float timeDelay = 0f;
    private float repeat = 9;
    public Transform parent;
    public GameObject enemy;
    public GameObject _boss;
    public PlayerController player;
    public EnemyController enemyController;
    public BossEnemyController bossEnemyController;

    private float xRangeLeft = -123;
    private float xRangeRight = 126;
    private float yRangeTop = 157;
    private float yRangeDown = -78;


    private bool addP;

    public static List<EntityInfo> cells = new List<EntityInfo>();

    void Start()
    {
        addP = true;
        InvokeRepeating("Spawn", timeDelay, repeat);
    }

    public Vector3 GetPosSpawnEnemy()
    {
        float x = 0, y = 0;
        float z = 0;
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
        else if (player.transform.position.x < 0)
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
        for (int i = 0; i < 15; i++)
        {
            GameObject Enemy = Instantiate(enemy, new Vector3(0, 0, 0), enemy.transform.rotation, parent);
            EnemyController enemyController = Enemy.GetComponent<EnemyController>();
            enemyController.ModelEnemy.transform.position = GetPosSpawnEnemy();
            enemyController.ModelEnemy.transform.localScale = player.transform.localScale;
            int id = Random.Range(1, 26);
            enemyController.ModelSkin.SetupModelSkin(id);
            enemyController.animEnemy.SetupAnim(id);
            cells.Add(enemyController.GetComponent<EnemyController>().entityInfo);
        }

        if (addP)
        {
            addP = false;
            cells.Add(GameManager.Instance.GetPlayer.entityInfo);
        }
    }

    public void SpawnBoss()
    {
        GameObject obj = Instantiate(_boss, new Vector3(0, 0, 0), _boss.transform.rotation, parent);
        BossEnemyController bossenemy = obj.GetComponent<BossEnemyController>();
        bossenemy.ModelEnemy.transform.position = GetPosSpawnEnemy();
        cells.Add(bossenemy.GetComponent<BossEnemyController>().entityInfo);
    }
}