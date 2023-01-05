using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PointEatFood : MonoBehaviour
{
    public BossEnemyController boss;
    public EnemyController enemy;
    public TypeEnemy TypeEnemy;
    private void OnTriggerEnter(Collider other)
    {
        switch (TypeEnemy)
        {
            case TypeEnemy.EnemyBoss:
                if (other.gameObject.CompareTag("Food"))
                {
                    Destroy(other.gameObject);
                    this.boss.pointEnemyBoss += 20;
                }

                if (other.gameObject.CompareTag("coin"))
                {
                    Destroy(other.gameObject);
                }
                break;
            case TypeEnemy.EnemyNomal:
                if (other.gameObject.CompareTag("Food"))
                {
                    Destroy(other.gameObject);
                    this.enemy.pointEnemy += 20;
                }

                if (other.gameObject.CompareTag("coin"))
                {
                    Destroy(other.gameObject);
                }
                break;
        }
        
    }
    
}
public enum TypeEnemy
{
    EnemyNomal,
    EnemyBoss,
}
