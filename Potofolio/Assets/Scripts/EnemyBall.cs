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
                Debug.Log("ÇÃ·¹ÀÌ¾î¿¡°Ô ±ø µ¥¹ÌÂ¢ ¤Ã¤§·æ");
                if (data == null)
                    return;
                foreach (var effectobjs in data.effects)
                {
                    if (effectobjs is IWeaponEffect effect)
                    {
                        Debug.Log("Àû ÀÌÆåÆ® Àû¤·¿ë");
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
