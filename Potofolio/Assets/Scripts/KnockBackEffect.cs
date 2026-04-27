using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Weapon/Effects/KnockBack")]
public class KnockBackEffect : ScriptableObject, IWeaponEffect
{

    [SerializeField] float radius = 5.0f;       // 폭발 반경
    [SerializeField] float force = 20.0f;       // 폭발 위력
    [SerializeField] float upModifier = 3.0f;   // 위로 띄우는 정도 (이게 있어야 시원하게 날아감)
    [SerializeField] float knockbackTime;

    public void Apply(GameObject target)
    {
        if (target == null)
            return;
        Vector3 explosionPoint = target.transform.position; 
        // 1. 반경 내 모든 물체 감지
        Collider[] colliders = Physics.OverlapSphere(explosionPoint, radius);
       
        foreach (Collider hit in colliders)
        {
            //if(!hit.gameObject.CompareTag("Enemy"))
            //{
            //    return;
            //}
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                explosionPoint = target.transform.position - Vector3.up * 1f;
                // 3. 폭발 물리 적용 (중심점에서 밖으로 날려버림)
                rb.AddExplosionForce(force, explosionPoint, radius, upModifier, ForceMode.Impulse);

                Debug.Log("넉백");
            }
            EnemyMovement enemy = hit.GetComponent<EnemyMovement>();
            if (enemy != null)
            {
                enemy.ApplyKnockBack(knockbackTime);
            }


        }
    }
}
