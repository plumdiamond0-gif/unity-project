using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public enum StatType
{
    RedSlime,
    BlueSlime,
    Level
}

public class DataItem : ScriptableObject
{
    public StatType type;
    public ulong amount;

    Dictionary<StatType, Action<ulong>> itemActions
    = new()
{
    { StatType.RedSlime, (value) => GM.GetSaveManager().currentData.RedSlime += value },
    { StatType.BlueSlime, (value) => GM.GetSaveManager().currentData.BlueSlime += value },
    { StatType.Level, (value) => GM.GetSaveManager().currentData.level += value }
};

    public void GetItem()
    {
        itemActions[type](amount);
    }
}


