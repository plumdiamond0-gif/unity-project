using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;



public enum RewardType
{
    None,
    Heal,
    DamageBuff,
    AttackSpeedPlus,
    MoveSpeedPlus,
    JumpPowerBuff,
    MaxHpPlus,

}

[System.Serializable]
public class RewardData
{
    public string RewardName;
    public Sprite RewardSprite;

    public float MinValue;
    public float MaxValue;


    public RewardType RewardType;


}

[CreateAssetMenu(menuName = "Data/RewardDataTable")]
public class RewardDataTable : ScriptableObject
{

    public List<RewardData> rewardDatas = new();
}
