using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    private float xRangeLeft = -110;
    private float xRangeRight = 130;
    private float yRangeTop = 60;
    private float yRangeDown = -70;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
         Instantiate(player, Position(), player.transform.rotation);
           
    }

    private Vector3 Position()
    {
        float x = Random.Range(xRangeLeft, xRangeRight);
        float y = Random.Range(yRangeDown, yRangeTop);
        float z = -3.7f;
        return new Vector3(x, y, z);
    }
}
