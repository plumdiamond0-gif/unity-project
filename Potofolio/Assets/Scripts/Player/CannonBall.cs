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
        if(other.CompareTag("Enemy"))
        {
            
            GameObject target = other.gameObject;
            target.GetComponent<EnemyMovement>().TakeDamage(damage);
            Debug.Log("Àû¿ë");
            foreach (var effectobjs in data.effects)
            {
                if(effectobjs is IWeaponEffect effect)
                {
                    effect.Apply(target);
                }
            }
        }




        else if (other.CompareTag("Ground") || other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }




}
