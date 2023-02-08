using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    public BossEnemyController boss;
    public EnemyController enemy;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
            boss.pointEnemyBoss += 20;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            enemy.pointEnemy += 20;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameManager.Instance.point += 20;
            GameManager.Instance.energy += 0.3f;
            if (GameManager.Instance.energy > 1)
            {
                GameManager.Instance.energy = 1;
            }
            if (GameManager.Instance.energy < 0)
            {
                GameManager.Instance.energy = 0;
            }
        }
    }
    
}
