using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PanelReward : PanelBase
{
    [SerializeField]
    RewardDataTable rewardDataTable;

    List<RewardData> temps;
    RewardButton[] rewards = new RewardButton[3];

    public override void Init()
    {
        gameObject.SetActive(false);
        rewards = GetComponentsInChildren<RewardButton>();
    }

    
    
   public void ShowReward()
    {
        
        for (int i = 0; i < 3; i++)
        {
            RewardData randData =
                rewardDataTable.rewardDatas[Random.Range(0, rewardDataTable.rewardDatas.Count)];
            rewards[i].GetData(randData);


        }
    }

    
}
