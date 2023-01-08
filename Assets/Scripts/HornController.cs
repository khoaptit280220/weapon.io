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
                        objfood.transform.rotation);
                    GameManager.Instance.point += 50;
                    
                    DOTween.Sequence().SetDelay(10).OnComplete(() =>
                    {
                        if (other.transform.position.x < player.transform.position.x - 30 ||
                            other.transform.position.x > player.transform.position.x + 30)
                        {
                            other.gameObject.SetActive(true);
                        }
                        
                    });
                }

                if (other.gameObject.CompareTag("Boss"))
                {
                    other.gameObject.SetActive(false);
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x, other.transform.position.y, -3.5f),
                        objfood.transform.rotation);
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
                        objfood.transform.rotation);
                    DOTween.Sequence().SetDelay(10).OnComplete(() =>
                    {
                        if (other.transform.position.x < player.transform.position.x - 30 ||
                            other.transform.position.x > player.transform.position.x + 30)
                        {
                            other.gameObject.SetActive(true);
                        }
                    });
                    
                }

                if (other.gameObject.CompareTag("Boss"))
                {
                    other.gameObject.SetActive(false);
                    this.enemy.pointEnemy += 50;
            
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x, other.transform.position.y, -3.5f),
                        objfood.transform.rotation);
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    GameManager.Instance.isPlayerDie = true;
                    other.gameObject.SetActive(false);
                    this.enemy.pointEnemy += 50;
            
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x, other.transform.position.y, -3.5f),
                        objfood.transform.rotation);
                }
                break;
            case TypeKiem.KiemEnemyBoss:
                if (other.gameObject.CompareTag("Enemy"))
                {
                    other.gameObject.SetActive(false);
                    this.BossEnemy.pointEnemyBoss += 50;
            
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x, other.transform.position.y, -3.5f),
                        objfood.transform.rotation);
                    DOTween.Sequence().SetDelay(10).OnComplete(() =>
                    {
                        if (other.transform.position.x < player.transform.position.x - 30 ||
                            other.transform.position.x > player.transform.position.x + 30)
                        {
                            other.gameObject.SetActive(true);
                        }
                    });
                    
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    GameManager.Instance.isPlayerDie = true;
                    other.gameObject.SetActive(false);
                    this.BossEnemy.pointEnemyBoss += 50;
            
                    Instantiate(objfood,
                        new Vector3(other.transform.position.x, other.transform.position.y, -3.5f),
                        objfood.transform.rotation);
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



