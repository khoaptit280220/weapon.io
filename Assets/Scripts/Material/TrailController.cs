using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    public GameObject parent;

    private void Update()
    {
        this.transform.position = parent.transform.position - new Vector3(0, 2, 0);
    }
}
