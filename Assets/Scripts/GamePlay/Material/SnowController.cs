using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SnowController : MonoBehaviour
{
    //public EnemyController enemy;
    // public BossEnemyController BossEnemy;

    //private float speedenemy;
    // private float speedEnemyBoss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponentInParent<EnemyController>().speedEnemy = 8;
            DOTween.Sequence().SetDelay(3).OnComplete(() =>
            {
                other.gameObject.GetComponentInParent<EnemyController>().speedEnemy = 20;
            });
        }

        if (other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponentInParent<BossEnemyController>().speedBoss = 8;
            DOTween.Sequence().SetDelay(3).OnComplete(() =>
            {
                other.gameObject.GetComponentInParent<BossEnemyController>().speedBoss = 20;
            });
        }
    }
}