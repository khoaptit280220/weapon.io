using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinController : MonoBehaviour
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
        // transform.DOKill();
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
        if (other.gameObject.CompareTag("wallcoin"))
        {
            gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            if (Database.CurrentIdModelSkin == 24)
            {
                GameManager.Instance.coin += 2;
            }
            else
            {
                GameManager.Instance.coin += 1;
            }
        }

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss"))
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (_isSuck)
        {
            //  _isSuck = false;
            Suck();
        }
    }
}