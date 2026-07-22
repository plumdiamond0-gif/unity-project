
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static WeaponPrefabTable;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
public class PanelWeaponUpgrade
 : PanelBase
{
    //[SerializeField] private WeaponListUI weaponListUI;
    [SerializeField] private UpgradeResultUI upgradeResultUI;
    [SerializeField] private UpgradeUI upgradeUI;
    [SerializeField] private WeaponImageUI WeaponImageUI;
    [SerializeField] private CostUI costUI;
    [SerializeField] private GameObject ItemList;

    WeaponButton[] weaponButtons;

    private void Start()
    {
        weaponButtons = ItemList.GetComponentsInChildren<WeaponButton>();
        foreach (var item in weaponButtons)
        {
            item.BeActive();
            item.Button.onClick.AddListener(() => { costUI.gameObject.SetActive(false); });
        }
        Show(WeaponState.Base);
    }

    public void Show(WeaponState weaponState)
    {
        WeaponPrefabData data =
        GM.GetPrefabManager().WeaponPrefabTable.weaponPrafabTableDatas. Find(
        x => x.weaponState == weaponState);
        UpgradeResults results = data.upgradeResults;
        UpgradeCost cost = data.upgradeCosts;
        int level = SaveManager.CurrentData.weaponlevel[weaponState];
        upgradeResultUI.ShowResults(data);
        WeaponImageUI.Show(data.WeaponImage);
        upgradeUI.GetData(data, level);
    }



}



