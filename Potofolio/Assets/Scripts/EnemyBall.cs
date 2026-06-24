using UnityEngine;

public class EnemyBall : MonoBehaviour
{
    float Damage;
    public void GetDamage(float damage)
    {
        Damage = damage;    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health player = other.GetComponent<Health>();   
            Health playerhealth = player.GetComponent<Health>();
            if (playerhealth != null)
            {
                playerhealth.TakeDamage(Damage);
            }
            Destroy(gameObject);
        }
        else if(other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

}
