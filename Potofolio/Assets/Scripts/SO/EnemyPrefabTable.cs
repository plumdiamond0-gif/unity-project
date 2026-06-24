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

public class EnemyPrefabData
{
    public EnemyType enemyType;
    public string EnemyName;
    public GameObject Enemy;
    public GameObject EnemyBullet;

}
[CreateAssetMenu(menuName = "Data/EnemyPrafabTable")]
public class EnemyPrefabTable : ScriptableObject
{
    public List<EnemyPrefabData> EnemyPrefabDatas=
        new List<EnemyPrefabData>();
}
