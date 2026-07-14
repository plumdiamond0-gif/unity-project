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
    [SerializeField] private Button upgradeBtn;
    WeaponPrefabData data;
    int level;


    public void Start()
    {
        gameObject.SetActive(false);    
        for (int i = 0; i < 5; i++)
        {
            images[i].sprite = null;
            texts[i].text = null;
        }
        upgradeBtn.onClick.AddListener(() =>
        {
            UpgradeCost costs = data.upgradeCosts;
            foreach (var item in costs.costs)
            {
                int currentNum = SaveManager.CurrentData.itemStates[item.itemType];
                if (item.amount > currentNum)
                    return;
            }
            foreach (var item in costs.costs)
            {
                SaveManager.CurrentData.itemStates[item.itemType] -= item.amount;
            }
            SaveManager.CurrentData.weaponlevel[data.weaponState]++;

            Debug.Log(SaveManager.CurrentData.weaponlevel[data.weaponState]);
            ShowCosts();

        });
    }
    public void GetData(WeaponPrefabData data, int level)
    {
        this.level = level;
        this.data = data;
        ShowCosts();
    }

    public void ShowCosts()
    {
        gameObject.SetActive(true);
        if (data == null) return;
        UpgradeCost costs = data.upgradeCosts;
        for (int i = 0; i < 5; i++)
        {
            if (i < costs.costs.Count)
            {
                UpgradeCost.CostData costData = costs.costs[i];
                images[i].sprite = costData.CostSprite;
                texts[i].text = costData.itemType.ToString() + ":" +
                          SaveManager.CurrentData.itemStates[costData.itemType].ToString() + "/" 
                            + ((costData.amount) * (Mathf.Pow(1.2f, level)));
            }
            else
            {
                images[i].sprite = null;
                texts[i].text = null;
            }

        }
       
    }
}
