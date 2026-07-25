using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Effects/RangeDotDam")]
public class RangeDotDamEffect : ScriptableObject, IWeaponEffect
{
    [SerializeField] float dotDamage;
    [SerializeField] float dotTime;
    [SerializeField] float range;
    public void GetCharge(float charge)
    {
    }
    public void Apply(GameObject target, float multiplier)
    {
        float finalRange = range * multiplier;
        Vector3 point = target.transform.position;

        Collider[] colliders = Physics.OverlapSphere(point, finalRange);
        foreach (Collider collider in colliders)
        {
            float dist = Vector3.Distance(point, collider.transform.position);
            float finalDotDam = (dotDamage * multiplier) /
                Mathf.Clamp(dist, 1f, 3f);
            IWeaponEffectReceiver receiver = collider.GetComponent<IWeaponEffectReceiver>();
            if (receiver != null)
                receiver.ApplyFire(finalDotDam, dotTime);
        }

    }

}
