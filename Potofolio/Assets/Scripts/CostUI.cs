using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static WeaponPrefabTable;

public class CostUI : MonoBehaviour
{
    private Image[] images = new Image[5];
    private TMP_Text[] texts = new TMP_Text[5];
    public void ShowCosts(UpgradeCost costs, int level)
    {
        if (costs == null) return;
        for (int i = 0; i < 5; i++)
        {
            if (i < costs.costs.Count)
            {
                UpgradeCost.CostData costData = costs.costs[i];
                images[i].sprite = costData.CostSprite;
                texts[i].text = costData.itemType.ToString() + ":" +
                    ((costData.amount) * (Mathf.Pow(1.2f, level)) + "/" +
                                   SaveManager.CurrentData.itemStates[costData.itemType].ToString());
            }
            else
            {
                images[i].sprite = null;
                texts[i].text = null;
            }

        }
    }
}
