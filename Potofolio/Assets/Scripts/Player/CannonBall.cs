using UnityEngine;
using static WeaponPrefabTable;

public class CannonBall : PoolObject
{
    #region Internal Data
    private WeaponPrefabData _data;
    private float _damage;
    private float _charge;
    Vector3 prevPos;
    #endregion

    #region Public Methods
    public void SetDatas(float finalDamage,
        WeaponPrefabData weaponData, Vector3 pos,float charge)
    {
        _damage = finalDamage;
        _data = weaponData;
        prevPos = pos;
        _charge = charge;
    }
    #endregion

    #region Unity Lifecycle (Physics)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject target = other.gameObject;
            Health enemyHealth = target.GetComponent<Health>();
            if (_data != null && _data.effects != null && enemyHealth != null)
            {
                enemyHealth.TakeDamage(_damage);
                foreach (var effectObj in _data.effects)
                {
                    float level = SaveManager.CurrentData.weaponlevel[_data.weaponState];
                    float multiplier = Mathf.Pow(level, 1.15f);

                    if (multiplier == 0) multiplier = 1f;
                    if (effectObj is IWeaponEffect effect)
                    {
                        effect.GetCharge(_charge);
                        effect.Apply(target, multiplier); 
                    }
                }

            }
            GM.GetBulletManager().projectilePool[_data.weaponState].Return(gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            GM.GetBulletManager().projectilePool[_data.weaponState].Return(gameObject);
        }
    }

    private void Update()
    {
        if (Vector3.Distance(prevPos, transform.position) > 57)
        {
            GM.GetBulletManager().projectilePool[_data.weaponState].Return(gameObject);
        }
    }
    #endregion
}