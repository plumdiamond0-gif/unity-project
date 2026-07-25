using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public readonly Dictionary<WeaponState, ObjectPool> projectilePool = new();

    public void Init()
    {
        WeaponPrefabTable weaponPrefabTable = GM.GetPrefabManager().WeaponPrefabTable;
                foreach (var data in weaponPrefabTable.weaponPrafabTableDatas)
        {
            ObjectPool pool = new ObjectPool(data.WeaponBullet, data.bulletCount, transform);
            projectilePool.Add(data.weaponState, pool);
        }
    }
    public GameObject GetBullet(WeaponState type, Vector3 pos, Quaternion rot)
    {
        GameObject obj = projectilePool[type].Get();
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.transform.SetParent(GM.GetBulletManager().transform);
        return obj;
    }
}
