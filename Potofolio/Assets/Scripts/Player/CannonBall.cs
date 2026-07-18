using UnityEngine;
using static WeaponPrefabTable;

public class CannonBall : MonoBehaviour
{
    #region Internal Data
    private WeaponPrefabData _data;
    private float _damage;
    #endregion

    #region Public Methods
    public void SetDamage(float finalDamage)
    {
        _damage = finalDamage;
    }
    public void SetWeaponData(WeaponPrefabData weaponData)
    {
        _data = weaponData;
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
                    if (effectObj is IWeaponEffect effect) effect.Apply(target, multiplier);
                }

            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
    #endregion
}