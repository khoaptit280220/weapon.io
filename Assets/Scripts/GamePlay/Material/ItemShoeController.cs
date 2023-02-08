using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ItemShoeController : MonoBehaviour
{
    public EnemyController enemy;
    public BossEnemyController BossEnemy;
    
    private float speedenemy;
    private float speedEnemyBoss;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            DOTween.Sequence().SetDelay(10).OnComplete(() =>
            {
                gameObject.SetActive(true);
            });
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            speedenemy = enemy.speedEnemy;
            enemy.speedEnemy = 30;
            DOTween.Sequence().SetDelay(5).OnComplete(() =>
            {
                enemy.speedEnemy = speedenemy;
                gameObject.SetActive(true);
            });
        }
        if (other.gameObject.CompareTag("Boss"))
        {
            gameObject.SetActive(false);
            speedEnemyBoss = BossEnemy.speedBoss;
            BossEnemy.speedBoss = 30;
            DOTween.Sequence().SetDelay(5).OnComplete(() =>
            {
                BossEnemy.speedBoss = speedEnemyBoss;
                gameObject.SetActive(true);
            });
            
        }
    }
}
