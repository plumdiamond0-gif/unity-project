using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class CostData
{
    public OutItemType itemType;
    public Sprite CostSprite;
    public int amount;
}

[CreateAssetMenu(menuName = "Data/UpgradeCost")]
public class UpgradeCost : ScriptableObject
{
    public List<CostData> costs;
}
