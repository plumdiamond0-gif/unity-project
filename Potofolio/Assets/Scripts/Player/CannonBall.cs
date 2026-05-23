using UnityEngine;
using static WeaponPrefabTable;

public class CannonBall : MonoBehaviour
{
    WeaponPrefabTableData data;
    float damage;

    public void SetDamage(float finalDamage) {
        damage = finalDamage;
    }
    public void SetWeaponData(WeaponPrefabTableData weaponData)
    {   
        data = weaponData;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Enemy"))
        //{

        //    GameObject target = other.gameObject;
        //    Health EnemyHealth = target.GetComponent<Health>();
        //    if (EnemyHealth != null)
        //    {
        //        EnemyHealth.TakeDamage(damage);
        //        Debug.Log("캐논볼 에너미 데미지" + damage);

        //        //TODO : 왕잠시
        //        Debug.Log("적용");
        //        foreach (var effectobjs in data.effects)
        //        {
        //            if (effectobjs is IWeaponEffect effect)
        //            {
        //                effect.Apply(target);
        //            }
        //        }
        //    }
        //    Destroy(gameObject);
        //}


        //else if (other.CompareTag("Ground") || other.CompareTag("Enemy"))
        //{
        //    Destroy(gameObject);
        //}
    }




}
