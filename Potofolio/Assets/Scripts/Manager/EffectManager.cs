using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private readonly Dictionary<ParticleType, ObjectPool> paticlePools = new();
    private void Start()
    {
        ParticlePrefabTable particlePrefabTable =
            GM.GetPrefabManager().ParticlePrefabTable;
        foreach (var data in particlePrefabTable.particleDatas)
        {
            ObjectPool pool = new ObjectPool(data.particlePrefab, data.particleCount, transform);
            paticlePools.Add(data.particleType, pool);
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
