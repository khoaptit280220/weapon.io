using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimMain : MonoBehaviour
{
    [SerializeField] public AnimSkinController anim;

    private void Update()
    {
        anim.PlaySwin();
    }
}