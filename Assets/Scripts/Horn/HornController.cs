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
                }

                if (playerController.countHeadPlayer == 6 && indexHead == 1)
                {
                    playerController.countHeadPlayer = 0;
                    indexHead = 2;
                    ListHead.ForEach(x=>x.SetActive(false));
                    horn.Scale();
                }
                if (playerController.countHeadPlayer == 8 && indexHead == 2)
                {
                    playerController.countHeadPlayer = 0;
                    indexHead = 3;
                    ListHead.ForEach(x=>x.SetActive(false));
                    horn.Scale();
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
                
                if (other.gameObject.CompareTag("Enemy"))
                {
                    SetupHeadPlayer();
                    other.gameObject.SetActive(false);
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x + 3, other.transform.position.y, -3.5f),
                        objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x - 3, other.transform.position.y, -3.5f),
                        objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x, other.transform.position.y + 3, -3.5f),
                        objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                    GameManager.Instance.point += 50;
                    
                }
                if (other.gameObject.CompareTag("Boss"))
                {
                    SetupHeadPlayer();
                    other.gameObject.SetActive(false);
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x + 3, other.transform.position.y, -3.5f),
                        objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x - 3, other.transform.position.y, -3.5f),
                        objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x, other.transform.position.y + 3, -3.5f),
                        objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                    GameManager.Instance.point += 50;
                    
                }
                break;
            case TypeKiem.KiemEnemy:
                
                if (other.gameObject.CompareTag("Enemy"))
                {
                    SetupHeadEnemy();
                    other.gameObject.SetActive(false);
                    this.enemy.pointEnemy += 50;
            
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x + 3, other.transform.position.y, -3.5f),
                        objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x - 3, other.transform.position.y, -3.5f),
                        objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x, other.transform.position.y + 3, -3.5f),
                        objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                    
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    SetupHeadEnemy();
                    GameManager.Instance.GetPlayer.isPlayerDied = true;
                    other.gameObject.SetActive(false);
                    this.enemy.pointEnemy += 50;
            
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x + 3, other.transform.position.y, -3.5f),
                        objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x - 3, other.transform.position.y, -3.5f),
                        objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x, other.transform.position.y + 3, -3.5f),
                        objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                    
                }
                break;
            case TypeKiem.KiemEnemyBoss:
                
                if (other.gameObject.CompareTag("Enemy"))
                {
                    SetupHeadBoss();
                    other.gameObject.SetActive(false);
                    this.BossEnemy.pointEnemyBoss += 50;
            
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x + 3, other.transform.position.y, -3.5f),
                        objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x - 3, other.transform.position.y, -3.5f),
                        objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x, other.transform.position.y + 3, -3.5f),
                        objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                    
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    SetupHeadBoss();
                    GameManager.Instance.GetPlayer.isPlayerDied = true;
                    other.gameObject.SetActive(false);
                    this.BossEnemy.pointEnemyBoss += 50; 
                    GameManager.Instance.OnLoseGame();
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x + 3, other.transform.position.y, -3.5f),
                        objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x - 3, other.transform.position.y, -3.5f),
                        objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x, other.transform.position.y + 3, -3.5f),
                        objfood.transform.rotation, GameManager.Instance.GetLevelController.CurrentLevel.transform);
                    
                } 
                break;
        }
    }
}

public enum TypeKiem
{
    KiemEnemy,
    KiemPlayer,
    KiemEnemyBoss,
}



