using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum CostType
{
    RedSlime,
    BlueSlime,
}

[System.Serializable]
public class CostData
{
    public CostType itemType;
    public int amount;
}

[CreateAssetMenu(menuName = "Data/UpgradeCost")]
public class UpgradeCost : ScriptableObject
{
    public List<CostData> costs;
}
