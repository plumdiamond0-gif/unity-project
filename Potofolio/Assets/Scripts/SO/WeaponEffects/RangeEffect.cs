using System;
using System.Net.NetworkInformation;
using UnityEngine;


[CreateAssetMenu(menuName = "Weapon/Effects/Range")]
public class RangeEffect : ScriptableObject, IWeaponEffect
{
    [SerializeField] float range;
    [SerializeField] float rangeDam;
    float charge;

    public void GetCharge(float charge)
    {
        this.charge = charge;
    }
    public void Apply(GameObject target, float multiplier)
    {
        float finalRange = range * (charge + 1) * multiplier;
        IWeaponEffectReceiver receiver = target.GetComponent<IWeaponEffectReceiver>();
        if (receiver != null)
            receiver.ApplyRangeDam(rangeDam, finalRange);
    
           

    }
}
