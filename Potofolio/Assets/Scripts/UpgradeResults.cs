using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/UpgradeResults")]

public class UpgradeResults: ScriptableObject
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [System.Serializable]
    public class UpgradeResultsData
    {
        public float DamageUp;
        public float SpeedUp; 
        public float BulletNumUp;

    }
    public List<UpgradeResultsData> UpgradeResultDatas=
        new List<UpgradeResultsData>();
}
