using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "Weapon/Effects/Fire")]


public class FireEffect : ScriptableObject, IWeaponEffect
{
    [SerializeField] float dotDamage;
    [SerializeField] float dotTime;
    //[SerializeField] private float _particleTime;
    //[SerializeField] private ParticleType _particleType;
    //public float particleTime => _particleTime;
    //public ParticleType particleType => _particleType;

    float charge;


    public void GetCharge(float charge)
    {
        this.charge = charge;
    }
    public void Apply(GameObject target, float multiplier)
    {
        IWeaponEffectReceiver receiver = target.GetComponent<IWeaponEffectReceiver>();
        if (receiver != null)
        {
            float finalDotDam = dotDamage * (charge + 1) * multiplier;
            float finalDotNum = dotTime * (charge + 1) * multiplier;
            receiver.ApplyFire(finalDotDam, finalDotNum);
        }

    }



}
