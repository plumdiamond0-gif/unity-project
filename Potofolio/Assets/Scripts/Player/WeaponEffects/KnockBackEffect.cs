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
        float fianlRadius = radius * (1+charge) * multiplier;
        float finalForce = force * (1+charge) * multiplier;
        float fianlUpModifier = upModifier * (1+charge) * multiplier;
        float finalrangeDam = rangeDam * (1 + charge) * multiplier;

        Vector3 explosionPoint = target.transform.position; 
        // 1. 반경 내 모든 물체 감지
        Collider[] colliders = Physics.OverlapSphere(explosionPoint, fianlRadius);

            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                EnemyMovement enemy = hit.GetComponent<EnemyMovement>();
                Health health = hit.GetComponent<Health>();
                if (enemy != null && health != null)
                {
                    enemy.ApplyKnockBack();
                    health.TakeDamage(finalrangeDam);
                    Debug.Log("넉백적용");

                }
                if (rb != null)
                {
                    explosionPoint = target.transform.position;//- Vector3.up * 1f;
                                                               // 3. 폭발 물리 적용 (중심점에서 밖으로 날려버림)
                    rb.AddExplosionForce(finalForce, explosionPoint, fianlRadius, fianlUpModifier, ForceMode.Impulse);
                    Debug.Log("넉백!!!");

                }
            }


        }
    }
}
