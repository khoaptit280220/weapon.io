using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnergy : MonoBehaviour
{
    private float xRangeLeft = -120;
    private float xRangeRight = 120;
    private float yRangeTop = 150;
    private float yRangeDown = -70;

    public int count = 20;
    public GameObject energy;
    public Transform parent;

    private void Start()
    {
        InvokeRepeating("SapwnEnergy", 0.1f, 15);
    }

    void SapwnEnergy()
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(energy, Position(), energy.transform.rotation, parent);
        }
    }

    private Vector3 Position()
    {
        float x = Random.Range(xRangeLeft, xRangeRight);
        float y = Random.Range(yRangeDown, yRangeTop);
        float z = 0;
        return new Vector3(x, y, z);
    }
}