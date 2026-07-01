using UnityEngine;

public class Item : MonoBehaviour
{
    [HideInInspector]
    public ItemData data;
    public ItemType itemType;
    public OutItemType outType;
    public InItemType  inType;

    private void Awake()
    {
        if(itemType == ItemType.InGameItem)
        {
            data = GM.GetPrefabManager().ItemPrefabTable.ItemDatas.Find(x => x.outItemType == outType);
        }
        else
        {
            data = GM.GetPrefabManager().ItemPrefabTable.ItemDatas.Find(x => x.inItemType == inType);
        }

    }
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
                DamageBuff(playerStat);
                break;

            case InItemType.MoveSpeedPlus:
                MoveSpeedBuff(playerStat);
                break;

            case InItemType.AttackSpeedPlus:
                AttackSpeedBuff(playerStat);
                break;

            case InItemType.JumpPowerBuff:
                JumpPowerBuff(playerStat);
                break;

            case InItemType.MaxHpPlus:
                MaxHpPlus(health);
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
        Destroy(gameObject);
    }

    public void Heal(Health health)
    {
        //float healAmount = Random.Range(data.MinHealAmount, data.MaxHealAmount);
        //Debug.Log($"체력 {healAmount}만큼 회복");
        health.Heal(data.HealAmount);
    }
    public void DamageBuff(PlayerStat stat)
    {
        //float damageBuffAmount = Random.Range(data.MinDamageBuffAmount, data.MaxDamageBuffAmount);
        //Debug.Log($"데미지 {damageBuffAmount}만큼 증가");
        stat.BaseDamage *= data.DamageBuffAmount;
    }
    public void MaxHpPlus(Health health)
    {
        //float healAmount = Random.Range(data.MinHealAmount, data.MaxHealAmount);
        //Debug.Log($"최대 체력 {healAmount}만큼 회복");
        health.MaxHp += data.MaxHpBuffAmount;
    }
    public void MoveSpeedBuff(PlayerStat stat)
    {
        //float moveSpeedBuffAmount = Random.Range(data.MinMoveSpeed, data.MaxMoveSpeed);    
        //Debug.Log($"이동속도 {moveSpeedBuffAmount}만큼 증가");
        stat.MoveSpeed *= data.MoveSpeedBuffAmount;
    }
    public void AttackSpeedBuff(PlayerStat stat)
    {
        //float attackSpeedBuffAmount = Random.Range(data.MinAttackSpeed, data.MaxAttackSpeed);
        //Debug.Log($"공격속도 {attackSpeedBuffAmount}만큼 증가");
        stat.AttackDamage *= data.AttackSpeedBuffAmount;
    }

    public void JumpPowerBuff(PlayerStat stat)
    {
        //float jumpAmounBuff = Random.Range(data.MinMoveSpeed, data.MaxMoveSpeed);
        //Debug.Log($"점프량 {jumpAmounBuff}만큼 증가");
        stat.JumpPower *= data.JumpPowerBuffAmount;
    }

}
