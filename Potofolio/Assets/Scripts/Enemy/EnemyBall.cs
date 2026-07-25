using UnityEngine;

public class EnemyBall : PoolObject
{
    EnemyPrefabData data;
    float Damage;
    Vector3 prevPos;

    public void SetEnemyData(EnemyPrefabData enemyData)
    {
        data = enemyData;
    }

    public void SetDamage(float damage)
    {
        Damage = damage;    
    }
    public void SetPos(Vector3 pos)
    {
        prevPos = pos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>(); 
                if (player != null)
                {
                player.TakeDamage(Damage);
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
            GM.GetEnemyBulletManager().projectilePool[data.enemyType].Return(gameObject);
        }
        else if(other.CompareTag("Ground"))
        {
            GM.GetEnemyBulletManager().projectilePool[data.enemyType].Return(gameObject);
        }
    }
    private void Update()
    {
        if (Vector3.Distance(prevPos, transform.position) > 57)
        {
            GM.GetEnemyBulletManager().projectilePool[data.enemyType].Return(gameObject);
        }
    }
}
