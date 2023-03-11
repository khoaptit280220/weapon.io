using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DartController : MonoBehaviour
{
    public GameObject objfood;
    private GameObject parentFood;
    private GameObject f1;
    private GameObject f2;
    private GameObject f3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.GetPlayer.checkShield == false)
            {
                GameManager.Instance.GetPlayer.isPlayerDied = true;
                //SpawnEnemy.cells.RemoveAt(0);
                other.transform.gameObject.SetActive(false);
                parentFood = other.gameObject;
                SpamFood();
                GameManager.Instance.OnLoseGame();
            }
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponentInParent<EnemyController>().checkShieldEnemy == false)
            {
                // other.gameObject.GetComponentInParent<EnemyController>().pointEnemy = 0;
                other.transform.parent.gameObject.SetActive(false);
                parentFood = other.gameObject;
                SpamFood();
            }
        }

        if (other.gameObject.CompareTag("Boss"))
        {
            if (other.gameObject.GetComponentInParent<BossEnemyController>().checkShieldBoss == false)
            {
                other.gameObject.GetComponentInParent<BossEnemyController>().pointEnemyBoss = 0;
                other.transform.parent.gameObject.SetActive(false);
                parentFood = other.gameObject;
                SpamFood();
            }
        }
    }

    private void SpamFood()
    {
        Vector3 stratPos = new Vector3(parentFood.transform.position.x, parentFood.transform.position.y, 0);
        f1 = Instantiate(objfood,
            new Vector3(parentFood.transform.position.x + 1, parentFood.transform.position.y, 0),
            objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
        f1.transform.DOMove(stratPos + new Vector3(3, 0, 0), 0.5f);
        f2 = Instantiate(objfood,
            new Vector3(parentFood.transform.position.x - 1, parentFood.transform.position.y, 0),
            objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
        f2.transform.DOMove(stratPos + new Vector3(-3, 0, 0), 0.5f);
        f3 = Instantiate(objfood,
            new Vector3(parentFood.transform.position.x, parentFood.transform.position.y + 1, 0),
            objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
        f3.transform.DOMove(stratPos + new Vector3(0, 3, 0), 0.5f);
    }
}