using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/UnlockCost")]
public class UnlockCost : ScriptableObject
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
