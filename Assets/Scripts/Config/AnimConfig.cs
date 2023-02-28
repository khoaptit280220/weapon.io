using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Animancer;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimConfig", menuName = "Configs/AnimConfig")]
public class AnimConfig : ScriptableObject
{
    public List<AnimData> listAnim = new List<AnimData>();

    public AnimData GetAnimDataById(int _id)
    {
        return listAnim.First(data => data.Id == _id);
    }

    public AnimData GetAnimDataByTypeModel(TypeModelSkin _typeModelSkin)
    {
        return listAnim.First(data => data.typeModelSkin == _typeModelSkin);
    }
}

[Serializable]
public class AnimData
{
    public int Id;
    public TypeModelSkin typeModelSkin;

    public ClipTransition Idle;
    public ClipTransition Swin;
    public ClipTransition SwinTrail;
}