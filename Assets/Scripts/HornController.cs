using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HornController : MonoBehaviour
{
    
    
    //public GameObject enemy;
    public GameObject objfood;
    
    public TypeKiem typeKiem;

    public Transform parent;
    public PlayerController player;
    public EnemyController enemy;
    public BossEnemyController BossEnemy;

    private void OnTriggerEnter(Collider other)
    {
        switch (typeKiem)
        { 
            case TypeKiem.KiemPlayer:
                if (other.gameObject.CompareTag("Enemy"))
                {
                    other.gameObject.SetActive(false);
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x, other.transform.position.y, -3.5f),
                        objfood.transform.rotation, parent);
                    GameManager.Instance.point += 50;
                    
                }
                if (other.gameObject.CompareTag("Boss"))
                {
                    other.gameObject.SetActive(false);
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x, other.transform.position.y, -3.5f),
                        objfood.transform.rotation, parent);
                    GameManager.Instance.point += 50;
                }
                break;
            case TypeKiem.KiemEnemy:
                if (other.gameObject.CompareTag("Enemy"))
                {
                    other.gameObject.SetActive(false);
                    this.enemy.pointEnemy += 50;
            
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x, other.transform.position.y, -3.5f),
                        objfood.transform.rotation, parent);
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    GameManager.Instance.GetPlayer.isPlayerDied = true;
                    other.gameObject.SetActive(false);
                    this.enemy.pointEnemy += 50;
            
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x, other.transform.position.y, -3.5f),
                        objfood.transform.rotation, parent);
                }
                break;
            case TypeKiem.KiemEnemyBoss:
                if (other.gameObject.CompareTag("Enemy"))
                {
                    other.gameObject.SetActive(false);
                    this.BossEnemy.pointEnemyBoss += 50;
            
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x, other.transform.position.y, -3.5f),
                        objfood.transform.rotation, parent);
                   
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    GameManager.Instance.GetPlayer.isPlayerDied = true;
                    other.gameObject.SetActive(false);
                    this.BossEnemy.pointEnemyBoss += 50; 
                    GameManager.Instance.OnLoseGame();
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x, other.transform.position.y, -3.5f),
                        objfood.transform.rotation, parent);
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



