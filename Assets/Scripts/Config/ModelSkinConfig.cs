using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ModelSkinConfig", menuName = "Configs/ModelSkinConfig")]
public class ModelSkinConfig : ScriptableObject
{
    public List<ModelSkinData> ListModelSkinDatas = new List<ModelSkinData>();

    //ham unlock model default
    public void UnlockModelSkinDefault()
    {
        GetModelSkinById(1).IsUnlock = true;
    }

    //ham get data by id
    public ModelSkinData GetModelSkinById(int idModelSkin)
    {
        return ListModelSkinDatas.First(data => data.idModelSkin == idModelSkin);
    }

    //ham get data by typemodel;
    public ModelSkinData GetModelSkinByTypeModel(TypeModelSkin _typeModelSkin)
    {
        return ListModelSkinDatas.First(data => data.typeModelSkin == _typeModelSkin);
    }
}

[Serializable]
public class ModelSkinData
{
    public int idModelSkin;
    public TypeModelSkin typeModelSkin;

    public bool IsUnlock
    {
        get
        {
            Database.IS_CHECK_UNLOCK_MODEL_SKIN = typeModelSkin + "_" + idModelSkin;
            return Database.IsModelSkinUnlock;
        }
        set
        {
            Database.IS_CHECK_UNLOCK_MODEL_SKIN = typeModelSkin + "_" + idModelSkin;
            Database.IsModelSkinUnlock = value;
        }
    }
}

public enum TypeModelSkin
{
    A,
    B,
    C,
    D,
    E,
    F,
    G,
    H,
    I,
    J,
    K,
    L,
    M,
    N,
    O,
    P,
    Q,
    R,
    S,
    T,
    U,
    V,
    W,
    X,
    Y,
    Z
}