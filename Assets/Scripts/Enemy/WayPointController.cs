using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WayPointController : MonoBehaviour
{
    private float xRangeLeft = -140;
    private float xRangeRight = 140;
    private float yRangeTop = 70;
    private float yRangeDown = -70;
    public GameObject pointPrefabs;
    List<Vector3> listPos = new List<Vector3>();

   

    private Vector3 Position()
    {
        float x = Random.Range(xRangeLeft, xRangeRight);
        float y = Random.Range(yRangeDown, yRangeTop);
        float z = 0;
        return new Vector3(x, y, z);
    }
    public List<Vector3> GetListPosition()
    {
      
        for (int i = 0; i < 4; i++)
        {
            listPos.Add(Position());
        }

        return listPos;
    }

    public void SpawnWayPoint(List<Vector3> _listPos)
    {
       for(int i = 0; i<_listPos.Count; i++)
       {
           Instantiate(pointPrefabs, _listPos[i], pointPrefabs.transform.rotation , gameObject.transform);
       }
    }
    
}
