using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ItemShieldController : MonoBehaviour
{
    public EnemyController enemy;
    public BossEnemyController boss;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            GameManager.Instance.GetPlayer.checkShield = true;
            DOTween.Sequence().SetDelay(10).OnComplete(() =>
            {
                gameObject.SetActive(true);
                GameManager.Instance.GetPlayer.checkShield = false;
            });
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
