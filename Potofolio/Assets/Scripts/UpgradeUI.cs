using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    private Button upgradeButton;
    [SerializeField] private CostUI costUI;
    public bool CanClick;

    UpgradeCost currentcosts;
    int currentlevel;
    public void Start()
    {
        upgradeButton = GetComponent<Button>();
        upgradeButton.onClick.AddListener(ShowResults);   
    }
    public void GetData(UpgradeCost costs, int level)
    {
        currentcosts = costs;
        currentlevel = level;
        CanClick = true;
    }

    public void ShowResults()
    {
        if(!CanClick)
            return;
        costUI.ShowCosts(currentcosts, currentlevel);
    }

    //private void Start()
    //{
    //    Button = GetComponent<Button>();
    //    Button.onClick.AddListener();
    //}

    //public void ShowCosts(UpgradeCost CostDatas)
    //{
    //    for (int i = 0; i < ; i++)
    //    {
    //        if (i < CostDatas.costs.Count)
    //        {
    //            CostData costData = CostDatas.costs[i];

    //            if (costData.amount > 0)
    //            {
    //                switch (costData.itemType)
    //                {
    //                    case OutItemType.RedSlime:
    //                        {
    //                            images[i].sprite = costData.CostSprite;
    //                            texts[i].text = $"{costData.itemType.ToString()}의 수 : {costData.amount} / {SaveManager.CurrentData.RedSlime}";
    //                            break;
    //                        }
    //                    case OutItemType.BlueSlime:
    //                        {
    //                            images[i].sprite = costData.CostSprite;
    //                            texts[i].text = $"{costData.itemType.ToString()}의 수 : {costData.amount} / {SaveManager.CurrentData.BlueSlime}";
    //                            break;
    //                        }
    //                }

    //            }

    //        }

    //        else
    //        {
    //            images[i].sprite = null;
    //            texts[i].text = null;
    //        }

    //    }

    //    void Show()
    //{

    //}
}
