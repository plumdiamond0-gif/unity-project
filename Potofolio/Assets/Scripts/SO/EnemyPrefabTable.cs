using System.Collections.Generic;
using UnityEngine;



public enum EnemyType
{
    Blink,
    ShellBlink,
    Chesty,
    Obsi,
    FlameNewt,
    Stingwing,
    Spike,
    Mush,
    Rocky,
    Starfy,
    Guard,
    PunchBunny,
    ShadeMage
}
[System.Serializable]

public class EnemyPrefabTableData
{
    public EnemyType enemyType;
    public string EnemyName;
    public GameObject Enemy;
    public GameObject EnemyBullet;
    public float Attackspeed;



}
[CreateAssetMenu(menuName = "Data/EnemyPrafabTable")]

public class EnemyPrefabTable : ScriptableObject
{
    public List<EnemyPrefabTableData> EnemyPrefabTableDatas=
        new List<EnemyPrefabTableData>();
}
