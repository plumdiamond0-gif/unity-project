using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;



public enum RewardType
{
    Heal,
    DamageUp,
    MaxHpUp,
    AttackSpeedUp
}


[CreateAssetMenu(menuName = "Data/RewardData")]
public class RewardDataTable : ScriptableObject
{
    [System.Serializable]
    public class RewardData
    {
        public string RewardName;
        public Sprite RewardSprite;

        public RewardType RewardType;
        public float value;
    }
    public List<RewardData> rewardDatas = new();
}
