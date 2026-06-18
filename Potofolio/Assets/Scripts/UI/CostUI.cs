using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static WeaponPrefabTable;

public class CostUI : MonoBehaviour
{
    [SerializeField] private Image[] images = new Image[5];
    [SerializeField] private TMP_Text[] texts = new TMP_Text[5];
    [SerializeField] private Exit exitButton;


    public void Start()
    {
        gameObject.SetActive(false);    
        images = GetComponentsInChildren<Image>().
        Where(x => x.gameObject != gameObject).ToArray();
        texts = GetComponentsInChildren<TMP_Text>().
            Where(x => x.gameObject != gameObject).ToArray();
        for (int i = 0; i < 5; i++)
        {
            images[i].sprite = null;
            texts[i].text = null;

        }

    }
    public void ShowCosts(UpgradeCost costs, int level)
    {
        gameObject.SetActive(true);
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
