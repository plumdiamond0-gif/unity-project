using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;
    public void Use(InItemType type, GameObject PlayerInfo)
    {
        switch (type)
        {
            case InItemType.HealItem:
                Health health = PlayerInfo.GetComponent<Health>();
                Heal(health);
                break;

            case InItemType.DamageBuffItem:
                PlayerAttack playerAttack = 
                    PlayerInfo.GetComponent<PlayerAttack>();
                DamageBuff(playerAttack.baseDamage);
                break;

        }
    }

    public void Restore(OutItemType type)
    {
        if(!SaveManager.CurrentData.itemStates.ContainsKey(type))
        {
            SaveManager.CurrentData.itemStates[type] = 0;
        }
        SaveManager.CurrentData.itemStates[type]++;
    }

    public void Heal(Health health)
    {


        health.Heal(data.healAmount);
        Debug.Log($"체력 {data.healAmount}만큼 회복");

    }

    public void DamageBuff(float basePlayerDamage)
    {
        Debug.Log($"데미지 {data.damageBuffAmount}만큼 증가");
        basePlayerDamage += data.damageBuffAmount;
    }



}
