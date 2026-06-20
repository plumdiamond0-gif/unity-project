using UnityEngine;
using UnityEngine.AI;
using static WeaponPrefabTable;

[CreateAssetMenu(menuName = "Weapon/Effects/KnockBack")]
public class KnockBackEffect : ScriptableObject, IWeaponEffect
{
    float charge;
    float radius;       // 폭발 반경
    float force ;       // 폭발 위력
    float upModifier;   // 위로 띄우는 정도 (이게 있어야 시원하게 날아감)

    public void GetCharge(float charge)
    {
        this.charge = charge;
    }


    public void Apply(GameObject target, float level)
    {

        if (target == null)
            return;
        float fianlRadius = radius * (1+charge) * Mathf.Pow(level, 1.15f);
        float finalForce = force * (1+charge) * Mathf.Pow(level, 1.15f);
        float fianlUpModifier = upModifier * (1+charge) * Mathf.Pow(level, 1.15f);

        Vector3 explosionPoint = target.transform.position; 
        // 1. 반경 내 모든 물체 감지
        Collider[] colliders = Physics.OverlapSphere(explosionPoint, fianlRadius);
       
        foreach (Collider hit in colliders)
        {
            //if (!hit.gameObject.CompareTag("Enemy"))
            //{
            //    return;
            //}
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            //NavMeshAgent agent = hit.GetComponent<NavMeshAgent>();
            //agent.enabled = false;
            EnemyMovement enemy = hit.GetComponent<EnemyMovement>();
            if (enemy != null)
            {
                enemy.ApplyKnockBack();
            }
            if (rb != null)
            {
                explosionPoint = target.transform.position;//- Vector3.up * 1f;
                // 3. 폭발 물리 적용 (중심점에서 밖으로 날려버림)
                rb.AddExplosionForce(finalForce, explosionPoint, fianlRadius, fianlUpModifier, ForceMode.Impulse);

                Debug.Log("넉백");
            }
           


        }
    }
}
