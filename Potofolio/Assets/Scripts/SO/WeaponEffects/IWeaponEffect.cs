using UnityEngine;
using static WeaponPrefabTable;


    public interface IWeaponEffect
    {
    //public float particleTime { get; }
    //public ParticleType particleType { get; }
    void GetCharge(float charge);
    void Apply(GameObject target, float multiplier);
    }

