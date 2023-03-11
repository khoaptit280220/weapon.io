using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "Configs/ItemConfig")]
public class ItemConfig : ScriptableObject
{
    public List<ItemData> ListSkinDatas;
    public List<ItemData> ListSwordDatas;
    public List<ItemData> ListTrailDatas;
    public List<ItemData> ListPackDatas;

    public void InitItem()
    {
        UnlockItemDefaul();
    }

    public void UnlockItemDefaul()
    {
        ListSkinDatas.First(dt => dt.typeBuy == TypeBuy.Default).IsUnlock = true;
        ListSwordDatas.First(dt => dt.typeBuy == TypeBuy.Default).IsUnlock = true;
//        ListTrailDatas.First(dt => dt.typeBuy == TypeBuy.Default).IsUnlock = true;
        //  ListPackDatas.First(dt => dt.typeBuy == TypeBuy.Default).IsUnlock = true;
    }

    #region Skin

    public ItemData GetSkinDataById(int _id)
    {
        return ListSkinDatas.First(dt => dt.id == _id);
    }

    #endregion

    public ItemData GetItemData(TypeItem typeItem, int _id)
    {
        switch (typeItem)
        {
            case TypeItem.Skin:
                return ListSkinDatas.First(dt => dt.id == _id);
                break;
            case TypeItem.Sword:
                return ListSwordDatas.First(dt => dt.id == _id);
                break;
            case TypeItem.Trail:
                return ListSwordDatas.First(dt => dt.id == _id);
                break;
        }

        return null;
    }

    public ItemData GetRandomItemDaily()
    {
        List<ItemData> _listTemp = new List<ItemData>();

        foreach (var VARIABLE in ListSkinDatas)
        {
            if (!VARIABLE.IsUnlock && VARIABLE.typeBuy == TypeBuy.DailyReward)
            {
                _listTemp.Add(VARIABLE);
            }
        }

        foreach (var VARIABLE in ListSwordDatas)
        {
            if (!VARIABLE.IsUnlock && VARIABLE.typeBuy == TypeBuy.DailyReward)
            {
                _listTemp.Add(VARIABLE);
            }
        }

        foreach (var VARIABLE in ListTrailDatas)
        {
            if (!VARIABLE.IsUnlock && VARIABLE.typeBuy == TypeBuy.DailyReward)
            {
                _listTemp.Add(VARIABLE);
            }
        }

        _listTemp.Shuffle();
        if (_listTemp.Count > 0)
        {
            return _listTemp[0];
        }
        else
        {
            return null;
        }
    }

    public ItemData GetRandomItemAds()
    {
        List<ItemData> _listTemp = new List<ItemData>();

        foreach (var VARIABLE in ListSkinDatas)
        {
            if (!VARIABLE.IsUnlock && VARIABLE.typeBuy == TypeBuy.Ads)
            {
                _listTemp.Add(VARIABLE);
            }
        }

        foreach (var VARIABLE in ListSwordDatas)
        {
            if (!VARIABLE.IsUnlock && VARIABLE.typeBuy == TypeBuy.Ads)
            {
                _listTemp.Add(VARIABLE);
            }
        }

        foreach (var VARIABLE in ListTrailDatas)
        {
            if (!VARIABLE.IsUnlock && VARIABLE.typeBuy == TypeBuy.Ads)
            {
                _listTemp.Add(VARIABLE);
            }
        }

        _listTemp.Shuffle();
        if (_listTemp.Count > 0)
        {
            return _listTemp[0];
        }
        else
        {
            return null;
        }
    }

    public ItemData GetRandomItemLevel()
    {
        List<ItemData> _listTemp = new List<ItemData>();

        foreach (var VARIABLE in ListSkinDatas)
        {
            if (!VARIABLE.IsUnlock && VARIABLE.typeBuy == TypeBuy.Coin)
            {
                _listTemp.Add(VARIABLE);
            }
        }

        foreach (var VARIABLE in ListSwordDatas)
        {
            if (!VARIABLE.IsUnlock && VARIABLE.typeBuy == TypeBuy.Coin)
            {
                _listTemp.Add(VARIABLE);
            }
        }

        foreach (var VARIABLE in ListTrailDatas)
        {
            if (!VARIABLE.IsUnlock && VARIABLE.typeBuy == TypeBuy.Coin)
            {
                _listTemp.Add(VARIABLE);
            }
        }

        _listTemp.Shuffle();
        if (_listTemp.Count > 0)
        {
            return _listTemp[0];
        }
        else
        {
            return null;
        }
    }
}

[Serializable]
public class ItemData
{
    public TypeItem typeItem;
    public int id;
    [ShowIf("typeItem", TypeItem.Skin)] public SkinName skinName;
    [ShowIf("typeItem", TypeItem.Skin)] public string descripSkin;

    [ShowIf("typeItem", TypeItem.Sword)] public SwordName swordName;

    [ShowIf("typeItem", TypeItem.Trail)] public TrailName trailName;

    //[ShowIf("typeItem", TypeItem.Pack)] public PackName packName;

    public Sprite imageIcon;
    public TypeBuy typeBuy;

    [ShowIf("typeBuy", global::TypeBuy.Coin)]
    public int Coin;

    public bool IsUnlock
    {
        get
        {
            Database.KeyItemCheckUnlocked = typeItem + "_" + id;
            return Database.IsItemUnlocked;
        }
        set
        {
            Database.KeyItemCheckUnlocked = typeItem + "_" + id;
            Database.IsItemUnlocked = value;
        }
    }
}

public enum TypeItem
{
    Skin,
    Sword,
    Trail,
    Pack
}

public enum TypeBuy
{
    Default,
    Coin,
    Ads,
    DailyReward,
}

public enum SkinName
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
    Z,
}

public enum SwordName
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

public enum TrailName
{
    TrailDefault,
}