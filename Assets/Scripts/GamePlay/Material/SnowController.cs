using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SnowController : MonoBehaviour
{
    public EnemyController enemy;
    public BossEnemyController BossEnemy;
    
    private float speedenemy;
    private float speedEnemyBoss;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            speedenemy = enemy.speedEnemy;
            enemy.speedEnemy = 10;
            DOTween.Sequence().SetDelay(3).OnComplete(() =>
            {
                enemy.speedEnemy = speedenemy;
            });
        }
        if (other.gameObject.CompareTag("Boss"))
        {
            speedEnemyBoss = BossEnemy.speedBoss;
            BossEnemy.speedBoss = 10;
            DOTween.Sequence().SetDelay(3).OnComplete(() =>
            {
                BossEnemy.speedBoss = speedEnemyBoss;
            });
        }
    }
}
