using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/UpgradeCost")]

public class UpgradeCost : ScriptableObject
{
    [System.Serializable]
    public class UpgradeCostData
    {
        public int  ResourceID;
        public int ResourceNum; 
    }
    public List<UpgradeCostData> UpgradeCostDatas =
        new List<UpgradeCostData>();
}
