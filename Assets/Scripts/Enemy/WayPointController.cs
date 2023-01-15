using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WayPointController : MonoBehaviour
{
    private float xRangeLeft = -130;
    private float xRangeRight = 135;
    private float yRangeTop = 70;
    private float yRangeDown = -75;
    public GameObject pointPrefabs;
    List<Vector3> listPos = new List<Vector3>();
    
    
    
    public Vector3 Position()
    {
        float x = Random.Range(xRangeLeft, xRangeRight);
        float y = Random.Range(yRangeDown, yRangeTop);
        float z = 0;
        return new Vector3(x, y, z);
    }
    public List<Vector3> GetListPosition()
    {
      
        for (int i = 0; i < 20; i++)
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
