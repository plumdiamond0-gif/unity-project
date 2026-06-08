using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PanelReward : PanelBase
{
    RewardDataTable rewardDataTable = new ();

    List<RewardDataTable.RewardData> temps;
    RewardButton[] rewards;

    
   public void ShowResult()
    {
        
        for (int i = 0; i < 3; i++)
        {
            RewardDataTable.RewardData randData =
                rewardDataTable.rewardDatas[Random.Range(0, rewards.Length)];
            rewards[i].GetData(randData);
        }
    }

    
}
