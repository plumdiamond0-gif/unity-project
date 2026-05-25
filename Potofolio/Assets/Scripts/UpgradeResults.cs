using System.Collections.Generic;
using UnityEngine;

public enum ResultType
{
    None,
    Damage,
    Speed,
    BulletNum
}
[CreateAssetMenu(menuName = "Data/UpgradeResults")]

public class UpgradeResults: ScriptableObject
{
    
    [System.Serializable]
    public class UpgradeResultsData
    {
        public ResultType type;
        public float amount;
        public Sprite sprite;
    }
    public List<UpgradeResultsData> results = new();
}
