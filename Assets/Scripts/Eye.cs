using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Eye : MonoBehaviour
{
    public TypeModelSkin TypeModelSkin;
    public SkinnedMeshRenderer _skinnedMeshRenderer => GetComponent<SkinnedMeshRenderer>();
    private float Value;
    private bool check;

    private void Start()
    {
        check = true;
    }

    public void ExplodeEye()
    {
        if (check)
        {
            Value = 0;
            check = false;
        }
        
        _skinnedMeshRenderer.SetBlendShapeWeight(0, Value);
    }
    public void PlaySpeedEye()
    {
        _skinnedMeshRenderer.SetBlendShapeWeight(1, 200);
    }

    public void OffSpeedEye()
    {
        _skinnedMeshRenderer.SetBlendShapeWeight(1, 0);
    }

    private void Update()
    {
        Value += 300 * Time.deltaTime;
    }
}