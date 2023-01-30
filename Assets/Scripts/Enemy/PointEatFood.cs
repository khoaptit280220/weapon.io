using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class PointEatFood : MonoBehaviour
{
    public TypeEnemy TypeEnemy;
    [ShowIf("TypeEnemy", global::TypeEnemy.EnemyBoss)]
    public BossEnemyController boss;
    [ShowIf("TypeEnemy", global::TypeEnemy.EnemyNomal)]
    public EnemyController enemy;
    
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
