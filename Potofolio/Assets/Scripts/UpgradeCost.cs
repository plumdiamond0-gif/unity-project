using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/UpgradeCost")]
public class UpgradeCost : ScriptableObject
{
    [System.Serializable]

    public class CostData
    {
        public OutItemType itemType;
        public Sprite CostSprite;
        public int amount;
    }

    public List<CostData> costs;


}
