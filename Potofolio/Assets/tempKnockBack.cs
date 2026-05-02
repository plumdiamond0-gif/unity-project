using UnityEngine;

public class tempKnockBack : MonoBehaviour
{

    [SerializeField] float radius = 5.0f;       // 폭발 반경
    [SerializeField] float force = 20.0f;       // 폭발 위력
    [SerializeField] float upModifier = 3.0f;   // 위로 띄우는 정도 (이게 있어야 시원하게 날아감)
    [SerializeField] float knockbackTime;
    public GameObject target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Apply(target);
    }

    public void Apply(GameObject target)
    {
        if (target == null)
            return;
        Vector3 explosionPoint = target.transform.position;

        Collider[] colliders = Physics.OverlapSphere(explosionPoint, radius);

        foreach (Collider hit in colliders)
        {

            Rigidbody rb = hit.GetComponent<Rigidbody>();
     
            if (rb != null)
            {
                explosionPoint = target.transform.position - Vector3.up * 1f;
                rb.AddExplosionForce(force, explosionPoint, radius, upModifier, ForceMode.Impulse);

                Debug.Log("넉백");
            }


        }
    }
}
