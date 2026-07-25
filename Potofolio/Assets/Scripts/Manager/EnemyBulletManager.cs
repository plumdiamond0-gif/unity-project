using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    public readonly Dictionary<EnemyType, ObjectPool> projectilePool = new();
    public void Init()
    {
        EnemyPrefabTable enemyPrefabTable = GM.GetPrefabManager().EnemyPrefabTable;
        foreach (var data in enemyPrefabTable.EnemyPrefabDatas)
        {
            ObjectPool pool = new ObjectPool(data.enemyBullet, data.bulletCount, transform);
            projectilePool.Add(data.enemyType, pool);
        }
    }
    public GameObject GetBullet(EnemyType type, Vector3 pos, Quaternion rot)
    {
        GameObject obj = projectilePool[type].Get();
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.transform.SetParent(GM.GetBulletManager().transform);
        return obj;
    }


}
