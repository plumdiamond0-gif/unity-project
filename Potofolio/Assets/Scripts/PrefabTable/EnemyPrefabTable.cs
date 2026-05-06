using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyPrafabTable")]

public class EnemyPrefabTable : ScriptableObject
{
    [System.Serializable]
    public class EnemyPrefabTableData
    {
       
        public string EnemyName;
        public GameObject Enemy;
        public GameObject EnemyBullet;
        public float Attackspeed;
        public float CoinNum;
        


    }
    public List<EnemyPrefabTableData> weaponPrafabTableDatas =
        new List<EnemyPrefabTableData>();
}
