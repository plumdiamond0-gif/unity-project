using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "Weapon/Effects/Stun")]


public class StunEffect : ScriptableObject, IWeaponEffect
{
    [SerializeField] float stunTime;
    public void GetCharge(float charge)
    {

    }
    public void Apply(GameObject target, float multiplier)
    {
        IWeaponEffectReceiver receiver = target.GetComponent<IWeaponEffectReceiver>();
        if (receiver != null)
        {
            float finalStunTime = stunTime * multiplier;
            receiver.ApplyStun(finalStunTime);
            Debug.Log("slow");
        }

    }



}
