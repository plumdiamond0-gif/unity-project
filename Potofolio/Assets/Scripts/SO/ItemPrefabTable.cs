using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;



public enum ItemType
{
    InGameItem,
    OutGameItem,
}
public enum InItemType
{
    None,
    HealItem,
    DamageBuffItem,
    AttackSpeedPlus,
    MoveSpeedPlus,
    JumpPowerBuff,
    MaxHpPlus,

    Coin
}
public enum OutItemType
{
    None,
    MonsterCore,
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
[System.Serializable]

public class ItemData
{
    public Sprite itemSprite;


    //public float MinHealAmount;
    //public float MaxHealAmount;

    //public float MinDamageBuffAmount;
    //public float MaxDamageBuffAmount;

    //public float MinAttackSpeed;
    //public float MaxAttackSpeed;

    //public float MinMoveSpeed;
    //public float MaxMoveSpeed;

    //public float MaxHpBuffAmount;
    //public float MinHpBuffAmount;

    public float HealAmount;
    public float DamageBuffAmount;
    public float AttackSpeedBuffAmount;
    public float MoveSpeedBuffAmount;
    public float MaxHpBuffAmount;
    public float JumpPowerBuffAmount;


    public InItemType inItemType;
    public OutItemType outItemType;
    public ItemType itemType;
    public string ItemName;
    public GameObject ItemPrefab;


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


[CreateAssetMenu(menuName = "Data/ItemPrefabTable")]
public class ItemPrefabTable : ScriptableObject
{
    public List<ItemData> InItemDatas =
        new List<ItemData>();
    public List<ItemData> OutItemDatas =
        new List<ItemData>();

}

