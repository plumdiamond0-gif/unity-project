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


    public void Awake()
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
                if (item.amount > currentNum * (Mathf.Pow(1.1f,
                            SaveManager.CurrentData.weaponlevel[data.weaponState])))
                    return;
            }
            GM.GetSoundManager().PlaySFX(AudioType.Upgrade);
            foreach (var item in costs.costs)
            {
                SaveManager.CurrentData.itemStates[item.itemType] -= (int)(item.amount * (Mathf.Pow(1.1f,
                            SaveManager.CurrentData.weaponlevel[data.weaponState])));
            }
            SaveManager.CurrentData.weaponlevel[data.weaponState]++;

            PanelWeaponUpgrade panelWeaponUpgrade = transform.parent.GetComponent<PanelWeaponUpgrade>();    
            panelWeaponUpgrade.Show(data.weaponState);
            ShowCosts();

        });
    }
    public void GetData(WeaponPrefabData data)
    {
        level = SaveManager.CurrentData.weaponlevel[data.weaponState];
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
                          SaveManager.CurrentData.itemStates[costData.itemType].ToString("F0") + "/" 
                            + ((costData.amount) * (Mathf.Pow(1.1f, 
                            SaveManager.CurrentData.weaponlevel[data.weaponState]))).ToString("F0");
            }
            else
            {
                images[i].sprite = null;
                texts[i].text = null;
            }

        }
       
    }
}
