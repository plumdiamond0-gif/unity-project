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
    WeaponPrefabData data;

    public void GetKnockVal(float charge, WeaponPrefabData data)
    {
        this.data = data;
        this.charge = charge;
    }


    public void Apply(GameObject target, WeaponState weaponType)
    {

        if (target == null)
            return;
        radius = data.radius * (1+charge);
        force = data.force * (1+charge);
        upModifier = data.upModifier * (1+charge);

        Vector3 explosionPoint = target.transform.position; 
        // 1. 반경 내 모든 물체 감지
        Collider[] colliders = Physics.OverlapSphere(explosionPoint, radius);
       
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
                rb.AddExplosionForce(force, explosionPoint, radius, upModifier, ForceMode.Impulse);

                Debug.Log("넉백");
            }
           


        }
    }
}
