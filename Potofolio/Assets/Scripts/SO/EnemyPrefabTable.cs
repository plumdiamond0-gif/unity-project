using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;



public enum EnemyType
{
    None,
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
    public string enemyName;
    public GameObject enemyPrefab;
    public GameObject enemyBullet;
    public int bulletCount;
    public List<ScriptableObject> effects;
    public EnemyDropItems dropItems;
    public float exp;
}
[CreateAssetMenu(menuName = "Data/EnemyPrafabTable")]
public class EnemyPrefabTable : ScriptableObject
{
    public List<EnemyPrefabData> EnemyPrefabDatas=
        new List<EnemyPrefabData>();
}
