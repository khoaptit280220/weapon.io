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
    private GameObject obj;
    private List<GameObject> listEnergy;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            obj = Instantiate(energy, Position(), energy.transform.rotation, parent);
   //         listEnergy.Add(obj);
        }
    }

    private Vector3 Position()
    {
        float x = Random.Range(xRangeLeft, xRangeRight);
        float y = Random.Range(yRangeDown, yRangeTop);
        float z = -1;
        return new Vector3(x, y, z);
    }
}
