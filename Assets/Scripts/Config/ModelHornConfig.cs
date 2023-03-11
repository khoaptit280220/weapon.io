using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ModelHornConfig", menuName = "Configs/ModelHornConfig")]
public class ModelHornConfig : ScriptableObject
{
    public List<ModelHornData> ListModelHornDatas = new List<ModelHornData>();

    //ham unlock model default
    public void UnlockModelHornDefault()
    {
        GetModelHornById(1).IsUnlock = true;
    }

    //ham get data by id
    public ModelHornData GetModelHornById(int idModelHorn)
    {
        return ListModelHornDatas.First(data => data.idModelHorn == idModelHorn);
    }

    //ham get data by typemodel;
    public ModelHornData GetModelHornByTypeModel(TypeModelHorn _typeModelHorn)
    {
        return ListModelHornDatas.First(data => data.typeModelHorn == _typeModelHorn);
    }
}

[Serializable]
public class ModelHornData
{
    public int idModelHorn;
    public TypeModelHorn typeModelHorn;

    public bool IsUnlock
    {
        get
        {
            Database.IS_CHECK_UNLOCK_HORN = typeModelHorn + "_" + idModelHorn;
            return Database.isModelHornUnlock;
        }
        set
        {
            Database.IS_CHECK_UNLOCK_HORN = typeModelHorn + "_" + idModelHorn;
            Database.isModelHornUnlock = value;
        }
    }
}

public enum TypeModelHorn
{
    Laser,
    AmericanFlag,
    Mace,
    Saw,
    Ice,
    Flame,
    Thunder,
    Hammer,
    Poison,
    Bouquet,
    Cannon,
    Broom,
    Reaper,
    Kunai,
    Cowbar,
    PirateSword,
    Trumpet,
    KrakenSlayer,
    EngineSword,
    TrafficLamp,
    Poseidon,
    Arm,
    Candle,
    Bell,
    Cinder,
    Frostbite,
    Enchanted,
    MoonSickle,
    Sickle,
    Saron,
    Spear,
    DragonBrade,
    Hook,
    Demon,
    Angelic,
    Runic,
    FrostAxe,
    Satan,
    RapierSword,
    PowerPole,
}