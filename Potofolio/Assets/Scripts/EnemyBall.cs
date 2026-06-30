using UnityEngine;

public class EnemyBall : MonoBehaviour
{
    EnemyPrefabData data;
    float Damage;

    public void SetEnemyData(EnemyPrefabData enemyData)
    {
        data = enemyData;
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
                if (data == null)
                    return;
                foreach (var effectobjs in data.effects)
                {
                    if (effectobjs is IWeaponEffect effect)
                    {
                        Debug.Log("적 이펙트 적ㅇ용");
                        effect.Apply(other.gameObject, 1);
                    }
                }
            }
                Destroy(gameObject);
        }
        else if(other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

}
