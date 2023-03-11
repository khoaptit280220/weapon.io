using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Eye : MonoBehaviour
{
    public TypeModelSkin TypeModelSkin;
    public SkinnedMeshRenderer _skinnedMeshRenderer => GetComponent<SkinnedMeshRenderer>();

    public void ExplodeEye()
    {
        _skinnedMeshRenderer.SetBlendShapeWeight(0, 150);
    }

    [Button]
    public void PlaySpeedEye()
    {
        _skinnedMeshRenderer.SetBlendShapeWeight(1, 99);
    }

    public void OffSpeedEye()
    {
        _skinnedMeshRenderer.SetBlendShapeWeight(1, 0);
    }
}