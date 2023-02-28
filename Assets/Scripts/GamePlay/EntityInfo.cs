using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInfo : MonoBehaviour, IComparable<EntityInfo>
{
    public int point;
    public string name;

    public int CompareTo(EntityInfo otherE)
    {
        return (int)(otherE.point - point);
    }
}