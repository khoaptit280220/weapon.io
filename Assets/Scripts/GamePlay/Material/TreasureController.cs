using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TreasureController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            GameManager.Instance.coin += 10;
            DOTween.Sequence().SetDelay(15).OnComplete(() =>
            {
                gameObject.SetActive(true);
            });
        }
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss"))
        {
            gameObject.SetActive(false);
            DOTween.Sequence().SetDelay(15).OnComplete(() =>
            {
                gameObject.SetActive(true);
            });
        }
    }
}
