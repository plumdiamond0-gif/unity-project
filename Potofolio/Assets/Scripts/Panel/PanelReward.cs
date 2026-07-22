using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelReward : PanelBase
{
    [SerializeField]
    RewardDataTable rewardDataTable;

    List<RewardData> temps;
    Button[] buttons = new Button[3];
    RewardButton[] rewards = new RewardButton[3];

    bool isMoved;

    public bool isEnded {get; private set; }

    public override void Init()
    {
        rewards = GetComponentsInChildren<RewardButton>();
        transform.localPosition = new Vector3(0, 1000, 0);
        isMoved = (transform.position == Vector3.zero);
        for (int i = 0; i < 3; i++)
        {
            buttons[i] = rewards[i].gameObject.GetComponent<Button>();
        }
        StartCoroutine(MovePanel());

    }
    IEnumerator MovePanel()
    {
        foreach (var item in buttons)
            item.interactable = false;  
        while (transform.localPosition != Vector3.zero)
        {
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition,
                Vector3.zero,
                800f * Time.deltaTime);

            yield return null;
        }
        ShowReward(); 
        foreach (var item in rewards)
            item.GetComponent<Button>().interactable = true;
    }

    public void ShowReward()
    {
        Time.timeScale = 0f;

        for (int i = 0; i < 3; i++)
        {
            RewardData randData =
                rewardDataTable.rewardDatas[Random.Range(0, rewardDataTable.rewardDatas.Count)];
            rewards[i].GetData(randData);
        }

       isEnded = true;
    }

    
}
