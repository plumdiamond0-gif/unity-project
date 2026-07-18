using UnityEngine;
using static WeaponPrefabTable;


    public interface IWeaponEffect
    {
    void Apply(GameObject target, float multiplier);
    }

