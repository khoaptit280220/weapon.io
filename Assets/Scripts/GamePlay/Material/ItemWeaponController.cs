using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ItemWeaponController : MonoBehaviour
{
    public EnemyController enemy;
    public BossEnemyController BossEnemy;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            GameManager.Instance.GetPlayer.GetComponentInChildren<Horn>().transform.localScale +=
                new Vector3(0, 0, 0.1f);
            GameManager.Instance.GetPlayer.GetComponentInChildren<Horn>().transform.localPosition +=
                new Vector3(0, 0, 0.1f);
            DOTween.Sequence().SetDelay(10).OnComplete(() =>
            {
                GameManager.Instance.GetPlayer.GetComponentInChildren<Horn>().transform.localScale -=
                    new Vector3(0, 0, 0.1f);
                GameManager.Instance.GetPlayer.GetComponentInChildren<Horn>().transform.localPosition -=
                    new Vector3(0, 0, 0.1f);
                gameObject.SetActive(true);
            });
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            enemy.GetComponentInChildren<Horn>().transform.localScale += new Vector3(0,0, 0.05f);
            enemy.GetComponentInChildren<Horn>().transform.localPosition += new Vector3(0, 0,0.05f);
            DOTween.Sequence().SetDelay(5).OnComplete(() =>
            {
                enemy.GetComponentInChildren<Horn>().transform.localScale -= new Vector3(0, 0,0.05f); 
                enemy.GetComponentInChildren<Horn>().transform.localPosition -= new Vector3(0, 0,0.05f);
                gameObject.SetActive(true);
            });
        }
        if (other.gameObject.CompareTag("Boss"))
        {
            gameObject.SetActive(false);
            DOTween.Sequence().SetDelay(5).OnComplete(() =>
            {
               gameObject.SetActive(true);
            });
        }
    }
}
