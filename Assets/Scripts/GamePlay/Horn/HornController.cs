using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class HornController : MonoBehaviour
{
    
    
    //public GameObject enemy;
    public GameObject objfood;
    private GameObject parentFood;
    private GameObject f1;
    private GameObject f2;
    private GameObject f3;
    
    float time = 0;
    private Vector3 stratPos;
    
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
        ListHead.ForEach(x=>x.SetActive(false));
    }

    public void SetupHeadPlayer()
    {
        playerController.countHeadPlayer++;
        if (playerController.countHeadPlayer < 10)
        {
            ListHead[playerController.countHeadPlayer-1].SetActive(true);
        }

        ResetHead();
    }
    public void SetupHeadEnemy()
    {
        enemy.countHeadEnemy++;
        if (enemy.countHeadEnemy < 10)
        {
            ListHead[enemy.countHeadEnemy-1].SetActive(true);
        }
        ResetHead();
    }
    public void SetupHeadBoss()
    {
        BossEnemy.countHeadBoss++;
        if (BossEnemy.countHeadBoss < 10)
        {
            ListHead[BossEnemy.countHeadBoss-1].SetActive(true);
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
                    ListHead.ForEach(x=>x.SetActive(false));
                    horn.Scale();
                    GameManager.Instance.checkBoss = true;
                }

                if (playerController.countHeadPlayer == 6 && indexHead == 1)
                {
                    playerController.countHeadPlayer = 0;
                    indexHead = 2;
                    ListHead.ForEach(x=>x.SetActive(false));
                    horn.Scale();
                    GameManager.Instance.checkBoss = true;
                }
                if (playerController.countHeadPlayer == 8 && indexHead == 2)
                {
                    playerController.countHeadPlayer = 0;
                    indexHead = 3;
                    ListHead.ForEach(x=>x.SetActive(false));
                    horn.Scale();
                    GameManager.Instance.checkBoss = true;
                }
                break;
            case TypeKiem.KiemEnemy:
                if (enemy.countHeadEnemy == 4 && countHead == 0)
                {
                    enemy.countHeadEnemy = 0;
                    countHead = 1;
                    ListHead.ForEach(x=>x.SetActive(false));
                    horn.Scale();
                }

                if (enemy.countHeadEnemy == 6 && countHead == 1)
                {
                    enemy.countHeadEnemy = 0;
                    countHead = 2;
                    ListHead.ForEach(x=>x.SetActive(false));
                    horn.Scale();
                }
                if (enemy.countHeadEnemy == 8 && countHead == 2)
                {
                    enemy.countHeadEnemy = 0;
                    countHead = 3;
                    ListHead.ForEach(x=>x.SetActive(false));
                    horn.Scale();
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
                if (other.gameObject.CompareTag("Torpedo"))
                {
                    this.transform.parent.gameObject.SetActive(false);
                    other.gameObject.SetActive(false);
                    GameManager.Instance.GetPlayer.isPlayerDied = true;
                    parentFood = this.gameObject;
                    SpamFood();
                    GameManager.Instance.OnLoseGame();
                }
                if (other.gameObject.CompareTag("Dart"))
                {
                    this.transform.parent.gameObject.SetActive(false);
                    GameManager.Instance.GetPlayer.isPlayerDied = true;
                    parentFood = this.gameObject;
                    SpamFood();
                    GameManager.Instance.OnLoseGame();
                }
                if (other.gameObject.CompareTag("Enemy"))
                {
                    SetupHeadPlayer();
                    other.transform.parent.gameObject.SetActive(false);
                    
                    parentFood = other.gameObject;
                    SpamFood();

                    GameManager.Instance.point += 50;
                }
                if (other.gameObject.CompareTag("Boss"))
                {
                    SetupHeadPlayer();
                    other.transform.parent.gameObject.SetActive(false);
                    
                    parentFood = other.gameObject;
                    SpamFood();
                    
                    GameManager.Instance.point += 50;
                }
                break;
            case TypeKiem.KiemEnemy:
                if (other.gameObject.CompareTag("Snow"))
                {
                    speedenemy = enemy.speedEnemy;
                    enemy.speedEnemy = 10;
                    DOTween.Sequence().SetDelay(3).OnComplete(() =>
                    {
                        enemy.speedEnemy = speedenemy;
                    });
                }
                if (other.gameObject.CompareTag("Dart"))
                {
                    this.transform.parent.gameObject.SetActive(false);
                    
                    parentFood = this.gameObject;
                    SpamFood();
                }
                if (other.gameObject.CompareTag("Torpedo"))
                {
                    this.transform.parent.gameObject.SetActive(false);
                    other.gameObject.SetActive(false);
                    parentFood = this.gameObject;
                    SpamFood();
                }
                if (other.gameObject.CompareTag("Enemy"))
                {
                    SetupHeadEnemy();
                    other.transform.parent.gameObject.SetActive(false);

                    this.enemy.pointEnemy += 50;
                    
                    parentFood = other.gameObject;
                    SpamFood();
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    SetupHeadEnemy();
                    GameManager.Instance.GetPlayer.isPlayerDied = true;
                    other.gameObject.SetActive(false);
                    this.enemy.pointEnemy += 50;
                    
                    parentFood = other.gameObject;
                    SpamFood();
                }
                break;
            case TypeKiem.KiemEnemyBoss:
                if (other.gameObject.CompareTag("Snow"))
                {
                    speedEnemyBoss = BossEnemy.speedBoss;
                    BossEnemy.speedBoss = 10;
                    DOTween.Sequence().SetDelay(3).OnComplete(() =>
                    {
                        BossEnemy.speedBoss = speedEnemyBoss;
                    });
                }
                if (other.gameObject.CompareTag("Dart"))
                {
                    this.transform.parent.gameObject.SetActive(false);
                    
                    parentFood = this.gameObject;
                    SpamFood();
                }
                if (other.gameObject.CompareTag("Enemy"))
                {
                    SetupHeadBoss();
                    other.transform.parent.gameObject.SetActive(false);

                    this.BossEnemy.pointEnemyBoss += 50;
                    
                    parentFood = other.gameObject;
                    SpamFood();
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    SetupHeadBoss();
                    GameManager.Instance.GetPlayer.isPlayerDied = true;
                    other.gameObject.SetActive(false);
                    this.BossEnemy.pointEnemyBoss += 50; 
                    GameManager.Instance.OnLoseGame();
                    
                    parentFood = other.gameObject;
                    SpamFood();
                } 
                break;
        }
    }
    
    private void SpamFood()
    {
        stratPos = new Vector3(parentFood.transform.position.x, parentFood.transform.position.y, -3);
        time = 0;
        
        f1 = Instantiate(objfood,
            new Vector3(parentFood.transform.position.x + 2, parentFood.transform.position.y, -3),
            objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
        f2 = Instantiate(objfood,
            new Vector3(parentFood.transform.position.x -2, parentFood.transform.position.y, -3),
            objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
        f3 = Instantiate(objfood,
            new Vector3(parentFood.transform.position.x, parentFood.transform.position.y + 2, -3),
            objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (f1 != null)
        {
            f1.transform.localPosition = 
                Vector3.Lerp(stratPos + new Vector3(2,0,0), stratPos + new Vector3(4,0,0), time);
        }
        if (f2 != null)
        {
            f2.transform.localPosition = 
                Vector3.Lerp(stratPos + new Vector3(-2,0,0), stratPos + new Vector3(-4,0,0), time);
        }
        if (f3 != null)
        {
            f3.transform.localPosition = 
                Vector3.Lerp(stratPos + new Vector3(0,2,0), stratPos + new Vector3(0,4,0), time);
        }
    }
}

public enum TypeKiem
{
    KiemEnemy,
    KiemPlayer,
    KiemEnemyBoss,
}



