using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PoolManager : MonoBehaviour
{
    public readonly Dictionary<ParticleType, ObjectPool> paticlePools = new();
    public readonly Dictionary<EnemyType, ObjectPool> enemyBulletPool = new();
    public readonly Dictionary<WeaponState, ObjectPool> bulletPool = new();



    public void Init()
    {
        ParticlePrefabTable particlePrefabTable =
            GM.GetPrefabManager().ParticlePrefabTable;
        foreach (var data in particlePrefabTable.particleDatas)
        {
            ObjectPool pool = new ObjectPool(data.particlePrefab, data.particleCount, transform);
            paticlePools.Add(data.particleType, pool);
        }

        WeaponPrefabTable weaponPrefabTable = 
            GM.GetPrefabManager().WeaponPrefabTable;
        foreach (var data in weaponPrefabTable.weaponPrafabTableDatas)
        {
            ObjectPool pool = new ObjectPool(data.WeaponBullet, data.bulletCount, transform);
            bulletPool.Add(data.weaponState, pool);
        }

        EnemyPrefabTable enemyPrefabTable = 
            GM.GetPrefabManager().EnemyPrefabTable;
        foreach (var data in enemyPrefabTable.EnemyPrefabDatas)
        {
            ObjectPool pool = new ObjectPool(data.enemyBullet, data.bulletCount, transform);
            enemyBulletPool.Add(data.enemyType, pool);
        }
    }

    public GameObject PlayParticle(ParticleType type,Vector3 pos, Quaternion rot, Vector3 size, Transform parent)
    {
        GameObject particle = paticlePools[type].Get();
        particle.transform.localPosition = pos;
        particle.transform.rotation = rot;  
        particle.transform.localScale = size;
        particle.transform.parent = parent;
        return particle;    
    }
    public GameObject GetBullet(WeaponState type, Vector3 pos, Vector3 size, Quaternion rot)
    {
        GameObject obj = bulletPool[type].Get();
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.transform.localScale = size;
        obj.transform.SetParent(transform);
        return obj;
    }

    public GameObject GetEnemyBullet(EnemyType type, Vector3 pos, Vector3 size, Quaternion rot)
    {
        GameObject obj = enemyBulletPool[type].Get();
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.transform.localScale = size;
        obj.transform.SetParent(transform);
        return obj;
    }
    public void StopParticle(GameObject particle)
    {
        particle.GetComponent<PoolObject>().parentPool.Return(particle);
    }
    public void PlayLoop()
    {

    }

    public void StopLoop()
    {

    }


}
