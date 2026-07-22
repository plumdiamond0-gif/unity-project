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

    public void Play(ParticleType type,Vector3 pos, Quaternion rot, Transform parent)
    {
        GameObject particle = paticlePools[type].Get();
        particle.transform.position = pos;
        particle.transform.rotation = rot;  
        particle.transform.parent = parent;
    }

    public void PlayLoop()
    {

    }

    public void StopLoop()
    {

    }


}
