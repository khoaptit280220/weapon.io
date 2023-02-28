using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    public BossEnemyController boss;
    public EnemyController enemy;
    private bool checkfood;

    private void Start()
    {
        checkfood = true;
        DOVirtual.DelayedCall(.5f, () => { checkfood = false; });
    }

    private void OnTriggerStay(Collider other)
    {
        if (checkfood == false)
        {
            if (other.gameObject.CompareTag("RangeSuck"))
            {
                if (!_isSuck) _isSuck = true;
                DOTween.Sequence().SetDelay(0.5f).OnComplete(() => { this.gameObject.SetActive(false); });
            }
        }
    }

    private bool _isSuck = false;

    private void Suck()
    {
        transform.DOKill();
        if (GameManager.Instance.GetPlayer.speed > 40)
        {
            transform.DOMove(GameManager.Instance.GetPlayer.transform.position, 0.05f);
        }
        else
        {
            transform.DOMove(GameManager.Instance.GetPlayer.transform.position, 0.3f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (checkfood == false)
        {
            if (other.gameObject.CompareTag("Boss"))
            {
                Destroy(gameObject);
                other.gameObject.GetComponentInParent<BossEnemyController>().pointEnemyBoss += 20;
            }

            if (other.gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
                other.gameObject.GetComponentInParent<EnemyController>().pointEnemy += 20;
            }

            if (other.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
                if (Database.CurrentIdModelSkin == 25)
                {
                    GameManager.Instance.point += 40;
                }
                else
                {
                    GameManager.Instance.point += 20;
                }

                if (Database.CurrentIdModelSkin == 26)
                {
                    GameManager.Instance.energy += 0.6f;
                }
                else
                {
                    GameManager.Instance.energy += 0.3f;
                }

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

    private void Update()
    {
        if (_isSuck)
        {
            Suck();
        }
    }
}