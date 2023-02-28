using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = System.Random;

public class EnergyController : MonoBehaviour
{
    private bool _isSuck = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("RangeSuck"))
        {
            // Suck();
            if (!_isSuck) _isSuck = true;
        }
    }

    private void Suck()
    {
        //transform.DOKill();
        if (GameManager.Instance.GetPlayer.speed > 40)
        {
            transform.DOMove(GameManager.Instance.GetPlayer.transform.position, 0.05f);
        }
        else
        {
            transform.DOMove(GameManager.Instance.GetPlayer.transform.position, 0.4f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            // DOTween.Sequence().SetDelay(3).OnComplete(() => { this.gameObject.SetActive(true); });
            if (Database.CurrentIdModelSkin == 26)
            {
                GameManager.Instance.energy += 0.2f;
            }
            else
            {
                GameManager.Instance.energy += 0.1f;
            }

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
            // DOTween.Sequence().SetDelay(3).OnComplete(() => { this.gameObject.SetActive(true); });
        }
    }

    private void Update()
    {
        if (_isSuck)
        {
            // _isSuck = false;
            Suck();
        }
    }
}