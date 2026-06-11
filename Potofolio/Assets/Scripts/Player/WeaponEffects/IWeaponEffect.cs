using UnityEngine;
using static WeaponPrefabTable;


    public interface IWeaponEffect
    {
        void Apply(GameObject target, float level)
    {
        Debug.Log("이펙트적용");
    }
    }

