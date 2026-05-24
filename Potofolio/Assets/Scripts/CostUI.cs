using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static WeaponPrefabTable;

public class CostUI : MonoBehaviour
{
    //[SerializeField] private List<TMP_Text> texts;
    [SerializeField] private Image[] images = new Image[5];
    [SerializeField] private TMP_Text[] texts = new TMP_Text[5];

    public void ShowCosts(UpgradeCost CostDatas)
    {
        for (int i = 0; i < 5; i++)
        {
            if (i >= CostDatas.costs.Count)
            {
                CostData costData = CostDatas.costs[i];
              

                SaveManager.CurrentData.BlueSlime += 2;
                SaveManager.CurrentData.RedSlime += 2;



                if (costData.amount > 0)
                {
                    switch (costData.itemType)
                    {
                        case OutItemType.RedSlime:
                            {
                                images[i].sprite = costData.CostSprite;
                                texts[i].text = $"{costData.itemType.ToString()}의 수 : {costData.amount} / {SaveManager.CurrentData.RedSlime}";
                                break;
                            }
                        case OutItemType.BlueSlime:
                            {
                                images[i].sprite = costData.CostSprite;
                                texts[i].text = $"{costData.itemType.ToString()}의 수 : {costData.amount} / {SaveManager.CurrentData.BlueSlime}";
                                break;
                            }
                    }

                }

            }

            else
            {
                    images[i].sprite = null;
                    texts[i].text = null;
            }
             
        }


    //    for(int i = 0; i < 5; i++) {
    //    {
    //    //    CostData upgradeCost= WeaponData.upgradeCosts.costs[i];
    //    //        WeaponData  = WeaponData.upgradeCosts.Find(
    //    //x => x.weaponType == targetType);
    
    //    //        UpgradeCost upgradeCost = 
    //    //    sprites[i] = WeaponData.upgradeCosts.costs[i].CostSPrite;

    //    //    string cost = $"{WeaponData.upgradeCosts.costs[i].amount.ToString()} / {GM.GetSaveManager().CurrentData.}";
    //    }
        
            
        

        

    }
        

}
