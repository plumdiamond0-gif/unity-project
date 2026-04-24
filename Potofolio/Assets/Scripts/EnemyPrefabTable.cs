using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/WeaponPrafabTable")]

public class EnemyPrefabTable : ScriptableObject
{
    [System.Serializable]
    public class EnemyPrefabTableData
    {
       
        public string EnemyName;
        public GameObject Enemy;
        public GameObject EnemyBullet;
        public float damage;
        public float Attackspeed;
        public List<ScriptableObject> effects;
        


    }
    public List<EnemyPrefabTableData> weaponPrafabTableDatas =
        new List<EnemyPrefabTableData>();
}
