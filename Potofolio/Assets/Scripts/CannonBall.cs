using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float damage;
    public float speed;
    public float KnockBackForce;
    public float KnockBackSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
            Destroy(gameObject);    
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("¹Ð·Á³ª");
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(transform.forward * KnockBackForce, ForceMode.Impulse);
            }
            Destroy(gameObject);

        }
    }
}
