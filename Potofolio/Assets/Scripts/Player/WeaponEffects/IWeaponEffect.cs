using UnityEngine;


    public interface IWeaponEffect
    {
        void Apply(GameObject target)
    {
        Debug.Log("이펙트적용");
    }
    }

