using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;
    public void Use(InItemType type, GameObject PlayerInfo)
    {
        PlayerStat playerStat =
            PlayerInfo.GetComponent<PlayerStat>();
        Health health = PlayerInfo.GetComponent<Health>();
        switch (type)
        {
         
            case InItemType.HealItem:
                
                Heal(health);
                break;

            case InItemType.DamageBuffItem:
                DamageBuff(playerStat.BaseDamage);
                break;

            case InItemType.MoveSpeedPlus:
                DamageBuff(playerStat.PlayerSpeed);
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
        float healAmount = Random.Range(data.MinHealAmount, data.MaxHealAmount);
        health.Heal(healAmount);
        Debug.Log($"체력 {healAmount}만큼 회복");
    }

    public void DamageBuff(float basePlayerDamage)
    {
        float damageBuffAmount = Random.Range(data.MinDamageBuffAmount, data.MaxDamageBuffAmount);
        Debug.Log($"데미지 {damageBuffAmount}만큼 증가");
        basePlayerDamage *= damageBuffAmount;
    }
    public void MaxHpPlus(Health health)
    {
       // Debug.Log($"체력 {data.healAmount}만큼 회복");
        health.MaxHp += data.MaxHpBuffAmount;
    }

    public void MoveSpeedPus(float BasePlayerSpeed)
    {
       // Debug.Log($"속도 {data.healAmount}만큼 증가");
     //   BasePlayerSpeed *= data.SpeedBuffAmount;
    }

}
