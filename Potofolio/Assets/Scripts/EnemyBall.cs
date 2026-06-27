using UnityEngine;

public class EnemyBall : MonoBehaviour
{
    EnemyPrefabTable data;
    float Damage;

    public void SetWeaponData(EnemyPrefabTable weaponData)
    {
        data = weaponData;
    }

    public void SetDamage(float damage)
    {
        Damage = damage;    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerhealth = other.GetComponent<Health>();
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
