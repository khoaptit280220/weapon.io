using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = System.Random;

public class EnergyController : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            DOTween.Sequence().SetDelay(3).OnComplete(() =>
            {
                this.gameObject.SetActive(true);
            });
            GameManager.Instance.energy += 0.1f;
            if (GameManager.Instance.energy > 1)
            {
                GameManager.Instance.energy = 1;
            }

            if (GameManager.Instance.energy < 0)
            {
                GameManager.Instance.energy = 0;
            }

        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            this.gameObject.SetActive(false);
            DOTween.Sequence().SetDelay(3).OnComplete(() =>
            {
                this.gameObject.SetActive(true);
            });
        }
    }
}
