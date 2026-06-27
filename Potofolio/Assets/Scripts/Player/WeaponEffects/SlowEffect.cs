using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "Weapon/Effects/Slow")]


public class SlowEffect : ScriptableObject, IWeaponEffect
{
    [SerializeField] float slowTime;
    [SerializeField] float slowAmount;

    public void Apply(GameObject target, float multiplier)
    {
        IWeaponEffectReceiver receiver = target.GetComponent<IWeaponEffectReceiver>();
        if (receiver != null)
        {
            float fianlSlowTime = slowTime * multiplier;
            float finalSlowAmount = slowAmount * multiplier;
            receiver.ApplySlow(fianlSlowTime, finalSlowAmount);
            Debug.Log("slow");
        }
    }


   
}
