using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PanelReward : PanelBase
{
    [SerializeField]
    RewardDataTable rewardDataTable;

    List<RewardDataTable.RewardData> temps;
    RewardButton[] rewards;

    public override void Init()
    {
        rewards = GetComponentsInChildren<RewardButton>();
    }
    
   public void ShowResult()
    {
        
        for (int i = 0; i < 3; i++)
        {
            RewardDataTable.RewardData randData =
                rewardDataTable.rewardDatas[Random.Range(0, rewardDataTable.rewardDatas.Count)];
            rewards[i].GetData(randData);
        }
    }

    
}
