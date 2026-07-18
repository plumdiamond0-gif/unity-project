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
            float fimalRange = range * (charge + 1) * multiplier;
            Vector3 point = target.transform.position;

            Collider[] colliders = Physics.OverlapSphere(point, fimalRange);
            foreach (Collider collider in colliders)
            {
            float dist = Vector3.Distance(point, collider.transform.position);
            float finalRangeDam = (rangeDam * (charge + 1) * multiplier) / 
                Mathf.Clamp(dist, 1f, 3f);
            IWeaponEffectReceiver receiver = collider.GetComponent<IWeaponEffectReceiver>();
            if (receiver != null)
                receiver.ApplyRangeDam(finalRangeDam);
            }

    }
}
