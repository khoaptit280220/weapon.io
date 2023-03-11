using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Animancer;
using Khoant;
using Sirenix.OdinInspector;
using UnityEngine;

public class AnimSkinShop : MonoBehaviour
{
    public AnimancerComponent animancer;
    [ReadOnly] public ClipTransition idle;

    private AnimData animSkin;
    public List<SkinController> listSkin;

    [Button]
    private void LoadList()
    {
        listSkin = GetComponentsInChildren<SkinController>().ToList();
    }

    private void OnEnable()
    {
        EventController.OnChangeViewSkin += SetupAnim;
        //SetupAnim();
    }
    
    private void OnDestroy()
    {
        EventController.OnChangeViewSkin -= SetupAnim;
    }

    public void SetupAnim(int id)
    {
        animSkin = ConfigManager.Instance.animConfig.GetAnimDataById(id);
        SetupDefault();
    }

    private void SetupDefault()
    {
        idle = animSkin.Idle;

        animancer.Animator = listSkin.First(x => x.idSkin == animSkin.Id).animator;
    }

    public void PlayIdle()
    {
        if (!animancer.IsPlaying(idle))
        {
            animancer.Play(idle);
        }
    }
    
}