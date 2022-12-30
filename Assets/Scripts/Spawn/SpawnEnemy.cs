using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    private float xRangeLeft = -110;
    private float xRangeRight = 130;
    private float yRangeTop = 60;
    private float yRangeDown = -70;
    
    public int count = 20;

    public GameObject enemy;
    private GameObject obj;
    public List<GameObject> listEnergy;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            obj = Instantiate(enemy, Position(), enemy.transform.rotation);
            listEnergy.Add(obj);
        }
    }

    private Vector3 Position()
    {
        float x = Random.Range(xRangeLeft, xRangeRight);
        float y = Random.Range(yRangeDown, yRangeTop);
        float z = -3.5f;
        return new Vector3(x, y, z);
    }
}
