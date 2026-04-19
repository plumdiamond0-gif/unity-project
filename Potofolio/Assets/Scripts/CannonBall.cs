using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float damage;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
            Destroy(gameObject);    
        }
    }
}
