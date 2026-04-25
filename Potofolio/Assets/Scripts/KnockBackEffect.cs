using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Weapon/Effects/KnockBack")]
public class KnockBackEffect : ScriptableObject, IWeaponEffect
{
    float radius = 5.0f;       // 폭발 반경
    float force = 20.0f;       // 폭발 위력
    float upModifier = 3.0f;   // 위로 띄우는 정도 (이게 있어야 시원하게 날아감)

    public void Apply(GameObject target)
    {
        if (target == null)
            return;
        Vector3 explosionPoint = target.transform.position; 
        // 1. 반경 내 모든 물체 감지
        Collider[] colliders = Physics.OverlapSphere(explosionPoint, radius);
       
        foreach (Collider hit in colliders)
        {
            Debug.Log("fff");
            //NavMeshAgent agent = hit.GetComponent<NavMeshAgent>();
            //agent.enabled = false;
            // 2. 리지드바디가 있는 오브젝트만 골라냄
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // 3. 폭발 물리 적용 (중심점에서 밖으로 날려버림)
                rb.AddExplosionForce(force, explosionPoint, radius, upModifier, ForceMode.Impulse);

                Debug.Log("넉백");
            }
        }
    }
}
