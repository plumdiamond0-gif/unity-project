using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;



public enum DataType
{
    InGameItem,
    OutGameItem,
}
public enum InItemType
{
    None,
    HealItem,
    DamageBuffItem,

}
public enum OutItemType
{
    None,
    RedJelly,
    SlimeShell,
    HardenedFang,
    WatchersEye,
    FlameCrystal,
    GaleCrystal,
    ToxicThornFragment,
    SporeSac,
    EmeraldShard,
    KnightsEmblem,
    ManaCrystal,
}

[CreateAssetMenu(menuName = "Data/ItemData")]

public class ItemData : ScriptableObject
{
    public Sprite itemSprite;
    public float healAmount;
    public float damageBuffAmount;
    public InItemType inItemType;
    public OutItemType outItemType;
    public DataType dataType;



    //public ulong amount;

    //    Dictionary<ItemType, Action<ulong>> itemActions
    //    = new()
    //{
    //    { ItemType.RedSlime, (value) => GM.GetSaveManager().currentData.RedSlime += value },
    //    { ItemType.BlueSlime, (value) => GM.GetSaveManager().currentData.BlueSlime += value },
    //    { ItemType.Level, (value) => GM.GetSaveManager().currentData.level += value }
    //};

    //public void GetItem()
    //{
    //    itemActions[type](amount);
    //}
}


