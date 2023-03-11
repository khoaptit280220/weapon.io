using System;
using System.Collections.Generic;
using System.Linq;
using Animancer;
using Cysharp.Threading.Tasks;
using Khoant;
using Sirenix.OdinInspector;
using UnityEngine;

public class AnimSkinController : MonoBehaviour
{
    public AnimancerComponent animancer;
    [ReadOnly] public ClipTransition idle;
    [ReadOnly] public ClipTransition swim;
    [ReadOnly] public ClipTransition swimTrail;
    [ReadOnly] public Eye eye;
    private AnimData currentAnimData;
    public List<SkinController> listSkin;
    public List<Eye> listEye;

    [Button]
    private void LoadList()
    {
        listSkin = GetComponentsInChildren<SkinController>().ToList();
        listEye = GetComponentsInChildren<Eye>().ToList();
    }

    private void OnEnable()
    {
        EventController.OnChangeModelSkin += SetupAnim;
        SetupAnim();
    }

    private void OnDestroy()
    {
        EventController.OnChangeModelSkin -= SetupAnim;
    }

    public void SetupAnim()
    {
        currentAnimData = ConfigManager.Instance.animConfig.GetAnimDataById(Database.CurrentIdModelSkin);
        SetupDefault();
    }

    private void SetupDefault()
    {
        idle = currentAnimData.Idle;
        swim = currentAnimData.Swin;
        swimTrail = currentAnimData.SwinTrail;

        animancer.Animator = listSkin.First(x => x.idSkin == currentAnimData.Id).animator;
        eye = listEye.First(eye => eye.TypeModelSkin == currentAnimData.typeModelSkin);
    }

    public void PlayIdle()
    {
        if (!animancer.IsPlaying(idle))
        {
            animancer.Play(idle);
        }
    }
    
    public void PlaySwin()
    {
        if (!animancer.IsPlaying(swim))
        {
            animancer.Play(swim);
        }
    }
    
    public void PlaySwinTrail()
    {
        if (!animancer.IsPlaying(swimTrail))
        {
            animancer.Play(swimTrail);
        }
    }
}