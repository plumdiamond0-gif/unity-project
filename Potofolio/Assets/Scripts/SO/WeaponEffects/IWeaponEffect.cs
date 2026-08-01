using UnityEngine;
using static WeaponPrefabTable;


    public interface IWeaponEffect
    {
    void GetCharge(float charge);
    void Apply(GameObject target, float multiplier);
    }

