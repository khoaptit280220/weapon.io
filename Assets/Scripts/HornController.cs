using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornController : MonoBehaviour
{
    public int pointEnemy = 0;
    public GameObject enemy;
    public GameObject objfood;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            GameManager.Instance.point += 50;
            Instantiate(objfood,
                new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z),
                objfood.transform.rotation);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            pointEnemy += 50;
        }
    }
}
