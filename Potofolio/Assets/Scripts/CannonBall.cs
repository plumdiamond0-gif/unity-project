using UnityEngine;
using static WeaponPrefabTable;

public class CannonBall : MonoBehaviour
{
    public float damage;
    public float speed;
    WeaponPrefabTableData data;

    public void SetWeaponData(WeaponPrefabTableData weaponData)
    {
        data = weaponData;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            
            GameObject target = other.gameObject;
            target.GetComponent<EnemyMovement>().TakeDamage(data.damage);
            Debug.Log("Àû¿ë");
            foreach (var effectobjs in data.effects)
            {
                if(effectobjs is IWeaponEffect effect)
                {
                    effect.Apply(target);
                }
            }
        }




        else if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }




}
