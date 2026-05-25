using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    private Button Button;


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
