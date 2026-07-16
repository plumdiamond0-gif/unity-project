using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static WeaponPrefabTable;

public class UnlockCostUI : MonoBehaviour
{
    [SerializeField] private Image[] images = new Image[5];
    [SerializeField] private TMP_Text[] texts = new TMP_Text[5];
    [SerializeField] private Exit exitButton;
    [SerializeField] private Button unlockBtn;

    WeaponPrefabData weapondata;

    public void Start()
    {
        gameObject.SetActive(false);
        for (int i = 0; i < 5; i++)
        {
            images[i].sprite = null;
            texts[i].text = null;
        }
        unlockBtn.onClick.AddListener(() =>
        {
            if (SaveManager.CurrentData.weaponActive[weapondata.weaponState])
                return;
            UnlockCost costs = weapondata.unlockedCosts;
            foreach (var item in costs.costs)
            {
                int currentNum = SaveManager.CurrentData.itemStates[item.itemType];
                if (item.amount > currentNum)
                    return;
            }
            GM.GetSoundManager().PlaySFX(AudioType.Unlock);
            foreach (var item in costs.costs)
            {
                SaveManager.CurrentData.itemStates[item.itemType] -= item.amount;
            }
            SaveManager.CurrentData.weaponActive[weapondata.weaponState] = true;
            PanelWeaponUnlock panelWeaponUnlock = transform.parent.GetComponent<PanelWeaponUnlock>();
            panelWeaponUnlock.RefreshUI();

            gameObject.SetActive(false);

        });
    }

    public void ShowCosts(WeaponPrefabData data)
    {
        if (data == null) return;
        gameObject.SetActive(true);
        weapondata = data;
        UnlockCost costs = data.unlockedCosts;
        for (int i = 0; i < 5; i++)
        {
            if (i < costs.costs.Count)
            {
                UnlockCost.CostData costData = costs.costs[i];
                images[i].sprite = costData.CostSprite;
                texts[i].text = costData.itemType.ToString() + ":" +
                          SaveManager.CurrentData.itemStates[costData.itemType].ToString() + "/"
                            + ((costData.amount));
            }
            else
            {
                images[i].sprite = null;
                texts[i].text = null;
            }

        }

    }
}
