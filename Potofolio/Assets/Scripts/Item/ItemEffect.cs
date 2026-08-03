using Unity.VisualScripting;
using UnityEngine;

public static class ItemEffect
{
    public static void Use(ItemData data)
    {
        GameObject player = GameManager.instance.GetPlayer();
        PlayerStat playerStat = player.GetComponent<PlayerStat>();  
        Health health = player.GetComponent<Health>();    
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        PlayerAttack attack = player.GetComponent<PlayerAttack>();  


        switch (data.inItemType)
        {
            case InItemType.HealItem:
                float healval = data.HealAmount;
                Heal(health, healval);
                break;

            case InItemType.DamageBuffItem:
                float damval = data.DamageBuffAmount;
                DamageBuff(playerStat, damval);
                break;

            case InItemType.MoveSpeedPlus:
                float moveval = data.MoveSpeedBuffAmount;
                MoveSpeedBuff(playerStat, moveval);
                break;

            case InItemType.AttackSpeedPlus:
                float attackval = data.AttackSpeedBuffAmount;   
                AttackSpeedBuff(playerStat, attackval);
                break;

            case InItemType.JumpPowerBuff:
                float jumpval = data.JumpPowerBuffAmount;   
                JumpPowerBuff(playerStat, jumpval);
                break;

            case InItemType.MaxHpPlus:
                float maxval = data.MaxHpBuffAmount;    
                MaxHpPlus(health, maxval);
                break;

            case InItemType.BeInvincible:
                BeInvincible(health, 5f);
                break;

            case InItemType.RecallBeacon:
                RecallBeacon(movement, 5f);
                break;

            case InItemType.HalfRemove:
                HalfRemove(attack);
                break;

        }
    }


    public static void Restore(OutItemType itemType)
    {
        if (!SaveManager.CurrentData.itemStates.ContainsKey(itemType))
        {
            SaveManager.CurrentData.itemStates[itemType] = 0;
        }
        SaveManager.CurrentData.itemStates[itemType]++;
    }

    public static void Heal(Health health, float val)
    {
        Debug.Log($"체력 {val}만큼 회복");
        health.Heal(val);
    }
    public static void DamageBuff(PlayerStat stat, float val)
    {
        Debug.Log($"데미지 {val}만큼 증가");
        stat.DamageMultiplier *= val;
    }
    public static void MaxHpPlus(Health health, float val)
    {
        Debug.Log($"최대 체력 {val}만큼 회복");
        health.MaxHp += val;
    }
    public static void MoveSpeedBuff(PlayerStat stat, float val)
    {
        Debug.Log($"이동속도 {val}만큼 증가");
        stat.MoveSpeedMultiplier *= val;
    }
    public static void AttackSpeedBuff(PlayerStat stat, float val)
    {
        Debug.Log($"공격속도 {val}만큼 증가");
        stat.AttackSpeedMultiplier *= val;
    }
    public static void JumpPowerBuff(PlayerStat stat, float val)
    {
        Debug.Log($"점프량 {val}만큼 증가");
        stat.JumpPowerMultiplier *= val;
    }
    public static void BeInvincible(Health health, float val)
    {
        health.InviciBuff(val);
    }
    public static void RecallBeacon(PlayerMovement movement, float val)
    {
        movement.RecallBeacon(val); 
    }
    public static void HalfRemove(PlayerAttack playerAttack)
    {
        playerAttack.HalfRemove();
    }
}
