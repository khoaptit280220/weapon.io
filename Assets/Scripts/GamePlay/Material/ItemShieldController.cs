using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ItemShieldController : MonoBehaviour
{
    public EnemyController enemy;
    public BossEnemyController boss;
    private bool va;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            GameManager.Instance.GetPlayer.checkShield = true;
            if (va)
            {
                GameManager.Instance.GetPlayer.timeShield += 8;
                va = false;
            }

            DOTween.Sequence().SetDelay(10).OnComplete(() => { gameObject.SetActive(true); });
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            enemy.checkShieldEnemy = true;
            DOTween.Sequence().SetDelay(5).OnComplete(() =>
            {
                gameObject.SetActive(true);
                enemy.checkShieldEnemy = false;
            });
        }

        if (other.gameObject.CompareTag("Boss"))
        {
            gameObject.SetActive(false);
            // boss.checkShieldBoss = true;
            DOTween.Sequence().SetDelay(8).OnComplete(() =>
            {
                gameObject.SetActive(true);
                //     boss.checkShieldBoss = false;
            });
        }
    }
}