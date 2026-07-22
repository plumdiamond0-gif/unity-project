using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleType
{
    Fire,
    Toxic,
    Energy,
    Slime
}

[System.Serializable]
public class ParticleData
{
    public GameObject particlePrefab;
    public ParticleType particleType;
    public int particleCount;
}

[CreateAssetMenu(menuName = "Data/ParticlePrefabTable")]
public class ParticlePrefabTable : ScriptableObject
{
    public List<ParticleData> particleDatas = new List<ParticleData>();
}
