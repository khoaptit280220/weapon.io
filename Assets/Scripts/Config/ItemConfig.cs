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
        // foreach (var VARIABLE in ListSkinDatas)
        // {
        //     if (VARIABLE.typeBuy == TypeBuy.Default)
        //     {
        //         VARIABLE.IsUnlock = true;
        //     }
        // }
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
}

[Serializable]
public class ItemData
{
    public TypeItem typeItem;
    public int id;
    [ShowIf("typeItem", TypeItem.Skin)] public SkinName skinName;

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
    SkinDefaultA,
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
    SwordDefault,
}

public enum TrailName
{
    TrailDefault,
}