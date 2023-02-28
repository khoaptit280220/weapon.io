using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class HornController : MonoBehaviour
{
    public GameObject objfood;
    private GameObject parentFood;
    private GameObject f1;
    private GameObject f2;
    private GameObject f3;
    public TypeKiem typeKiem;

    [ShowIf("typeKiem", TypeKiem.KiemPlayer)]
    public PlayerController playerController;

    [ShowIf("typeKiem", TypeKiem.KiemEnemy)]
    public EnemyController enemy;

    [ShowIf("typeKiem", TypeKiem.KiemEnemyBoss)]
    public BossEnemyController BossEnemy;

    public Horn horn;
    public List<GameObject> ListHead;
    private int countHead;
    private int indexHead = 0;

    private float speedPlayer;
    private float speedenemy;
    private float speedEnemyBoss;

    private void Start()
    {
        ResetHead();
        ListHead.ForEach(x => x.SetActive(false));
    }

    public void SetupHeadPlayer()
    {
        playerController.countHeadPlayer++;
        if (playerController.countHeadPlayer < 10)
        {
            ListHead[playerController.countHeadPlayer - 1].SetActive(true);
        }

        ResetHead();
    }

    public void SetupHeadEnemy()
    {
        enemy.countHeadEnemy++;
        if (enemy.countHeadEnemy < 10)
        {
            ListHead[enemy.countHeadEnemy - 1].SetActive(true);
        }

        ResetHead();
    }

    public void SetupHeadBoss()
    {
        BossEnemy.countHeadBoss++;
        if (BossEnemy.countHeadBoss < 10)
        {
            ListHead[BossEnemy.countHeadBoss - 1].SetActive(true);
        }
    }

    private void ResetHead()
    {
        switch (typeKiem)
        {
            case TypeKiem.KiemPlayer:
                if (playerController.countHeadPlayer == 4 && indexHead == 0)
                {
                    playerController.countHeadPlayer = 0;
                    indexHead = 1;
                    ListHead.ForEach(x => x.SetActive(false));
                    horn.ScalePlayer();
                    GameManager.Instance.checkBoss = true;
                }

                if (playerController.countHeadPlayer == 6 && indexHead == 1)
                {
                    playerController.countHeadPlayer = 0;
                    indexHead = 2;
                    ListHead.ForEach(x => x.SetActive(false));
                    horn.ScalePlayer();
                    GameManager.Instance.checkBoss = true;
                }

                if (playerController.countHeadPlayer == 8 && indexHead == 2)
                {
                    playerController.countHeadPlayer = 0;
                    indexHead = 3;
                    ListHead.ForEach(x => x.SetActive(false));
                    horn.ScalePlayer();
                    GameManager.Instance.checkBoss = true;
                }

                break;
            case TypeKiem.KiemEnemy:
                if (enemy.countHeadEnemy == 4 && countHead == 0)
                {
                    enemy.countHeadEnemy = 0;
                    countHead = 1;
                    ListHead.ForEach(x => x.SetActive(false));
                    horn.ScaleEnemy();
                }

                if (enemy.countHeadEnemy == 6 && countHead == 1)
                {
                    enemy.countHeadEnemy = 0;
                    countHead = 2;
                    ListHead.ForEach(x => x.SetActive(false));
                    horn.ScaleEnemy();
                }

                if (enemy.countHeadEnemy == 8 && countHead == 2)
                {
                    enemy.countHeadEnemy = 0;
                    countHead = 3;
                    ListHead.ForEach(x => x.SetActive(false));
                    horn.ScaleEnemy();
                }

                break;
            case TypeKiem.KiemEnemyBoss:
                break;
        }
    }

    public void SetTriggerHorn(Collider other)
    {
        switch (typeKiem)
        {
            case TypeKiem.KiemPlayer:
                if (other.gameObject.CompareTag("Enemy"))
                {
                    if (other.gameObject.GetComponentInParent<EnemyController>().checkShieldEnemy == false)
                    {
                        SetupHeadPlayer();
                        GameManager.Instance.kill += 1;
                        other.gameObject.GetComponentInParent<EnemyController>().pointEnemy = 0;
                        other.transform.parent.gameObject.SetActive(false);
                        parentFood = other.gameObject;
                        SpamFood();
                        GameManager.Instance.point += 50;
                    }
                }

                if (other.gameObject.CompareTag("Boss"))
                {
                    if (other.gameObject.GetComponentInParent<BossEnemyController>().checkShieldBoss == false)
                    {
                        SetupHeadPlayer();
                        other.gameObject.GetComponentInParent<BossEnemyController>().pointEnemyBoss = 0;
                        other.transform.parent.gameObject.SetActive(false);
                        parentFood = other.gameObject;
                        SpamFood();
                        GameManager.Instance.point += 50;
                        GameManager.Instance.kill += 1;
                    }
                }

                break;
            case TypeKiem.KiemEnemy:
                if (other.gameObject.CompareTag("Enemy"))
                {
                    if (other.gameObject.GetComponentInParent<EnemyController>().checkShieldEnemy == false)
                    {
                        SetupHeadEnemy();
                        other.gameObject.GetComponentInParent<EnemyController>().pointEnemy = 0;
                        other.transform.parent.gameObject.SetActive(false);
                        this.enemy.pointEnemy += 50;
                        parentFood = other.gameObject;
                        SpamFood();
                    }
                }

                if (other.gameObject.CompareTag("Player"))
                {
                    if (GameManager.Instance.GetPlayer.checkShield == false)
                    {
                        SetupHeadEnemy();
                        GameManager.Instance.GetPlayer.isPlayerDied = true;
                        SpawnEnemy.cells.RemoveAt(0);
                        DOTween.Sequence().SetDelay(0.8f).OnComplete(() => { other.gameObject.SetActive(false); });
                        GameManager.Instance.OnLoseGame();
                        this.enemy.pointEnemy += 50;
                        parentFood = other.gameObject;
                        SpamFood();
                    }
                }

                break;
            case TypeKiem.KiemEnemyBoss:
                if (other.gameObject.CompareTag("Enemy"))
                {
                    if (other.gameObject.GetComponentInParent<EnemyController>().checkShieldEnemy == false)
                    {
                        SetupHeadBoss();
                        other.gameObject.GetComponentInParent<EnemyController>().pointEnemy = 0;
                        other.transform.parent.gameObject.SetActive(false);
                        this.BossEnemy.pointEnemyBoss += 50;
                        parentFood = other.gameObject;
                        SpamFood();
                    }
                }

                if (other.gameObject.CompareTag("Player"))
                {
                    if (GameManager.Instance.GetPlayer.checkShield == false)
                    {
                        SetupHeadBoss();
                        GameManager.Instance.GetPlayer.isPlayerDied = true;
                        SpawnEnemy.cells.RemoveAt(0);
                        DOTween.Sequence().SetDelay(0.8f).OnComplete(() => { other.gameObject.SetActive(false); });
                        this.BossEnemy.pointEnemyBoss += 50;
                        GameManager.Instance.OnLoseGame();
                        parentFood = other.gameObject;
                        SpamFood();
                    }
                }

                break;
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

public enum TypeKiem
{
    KiemEnemy,
    KiemPlayer,
    KiemEnemyBoss,
}