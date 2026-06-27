using UnityEngine;
using static WeaponPrefabTable;

public class CannonBall : MonoBehaviour
{
    WeaponPrefabData data;
    float damage;

    public void SetDamage(float finalDamage) {
        damage = finalDamage;
    }
    public void SetWeaponData(WeaponPrefabData weaponData)
    {   
        data = weaponData;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

            GameObject target = other.gameObject;
            Health EnemyHealth = target.GetComponent<Health>();
            if (EnemyHealth != null)
            {
                EnemyHealth.TakeDamage(damage);
                Debug.Log("캐논볼 에너미 데미지" + damage);

                //TODO : 왕잠시
                foreach (var effectobjs in data.effects)
                {
                    float level = SaveManager.CurrentData.weaponlevel[data.weaponState];
                    float multiplier = Mathf.Pow(level, 1.15f);
                    if (effectobjs is IWeaponEffect effect)
                    {
                        if (multiplier == 0)
                            multiplier = 1;
                        effect.Apply(target, multiplier);
                        Debug.Log("적용");

                    }
                }
            }
            Destroy(gameObject);
        }


        else if (other.CompareTag("Ground") || other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }




}
