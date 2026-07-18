using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "Weapon/Effects/Dotdam")]


public class DotdamEffect : ScriptableObject, IWeaponEffect
{
    [SerializeField] float dotDamage;
    [SerializeField] float dotTime;

    float charge;
    public void GetCharge(float charge)
    {
        this.charge = charge;
    }
    public void Apply(GameObject target, float multiplier)
    {
        IWeaponEffectReceiver receiver = target.GetComponent<IWeaponEffectReceiver>();
        if (receiver != null) { 
            float finalDotDam = dotDamage * (charge+1) * multiplier;
            float finalDotNum= dotTime * (charge + 1) * multiplier;
            receiver.ApplyDotDam(finalDotDam, finalDotNum);
        }

    }



}
