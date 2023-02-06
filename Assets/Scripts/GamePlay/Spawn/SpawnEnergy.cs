using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnergy : MonoBehaviour
{
    private float xRangeLeft = -110;
    private float xRangeRight = 130;
    private float yRangeTop = 60;
    private float yRangeDown = -70;
    
    public int count = 50;
    public GameObject energy;
    public Transform parent;
    // Start is called before the first frame update
    void Start()
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
        float z = -3;
        return new Vector3(x, y, z);
    }
}
