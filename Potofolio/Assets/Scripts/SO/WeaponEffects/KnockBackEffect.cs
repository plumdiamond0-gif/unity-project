using UnityEngine;
using UnityEngine.AI;
using static WeaponPrefabTable;

[CreateAssetMenu(menuName = "Weapon/Effects/KnockBack")]
public class KnockBackEffect : ScriptableObject, IWeaponEffect
{
    float charge;
    [SerializeField]float radius;       // 폭발 반경
    [SerializeField]float force ;       // 폭발 위력
    [SerializeField] float upModifier;   // 위로 띄우는 정도 (이게 있어야 시원하게 날아감)
    [SerializeField] float rangeDam;

    public void GetCharge(float charge)
    {
        this.charge = charge;
    }
    public void Apply(GameObject target, float multiplier)
    {
        IWeaponEffectReceiver receiver = target.GetComponent<IWeaponEffectReceiver>();
        if (receiver != null ) {
        float finalRadius = radius * (1+charge) * multiplier;
        float finalForce = force * (1+charge) * multiplier;
        float fianlUpModifier = upModifier * (1+charge) * multiplier;
        float finalRangeDam = rangeDam * (1 + charge) * multiplier;
        receiver.ApplyKnockBack(finalForce, fianlUpModifier, finalRadius, finalRangeDam);
        }
    }
}
