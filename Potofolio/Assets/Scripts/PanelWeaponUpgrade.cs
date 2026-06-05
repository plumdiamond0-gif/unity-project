
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


    public void Show(WeaponState weaponState)
    {
        WeaponPrefabTableData data =
        GM.GetPrefabManager().WeaponPrefabTable.weaponPrafabTableDatas. Find(
        x => x.weaponState == weaponState);
        UpgradeResults results = data.upgradeResults;
        UpgradeCost cost = data.upgradeCosts;
        int level = SaveManager.CurrentData.weaponStates[weaponState];

        upgradeResultUI.ShowResults(data);
        WeaponImageUI.Show(data.WeaponImage);
        upgradeUI.GetData(cost, level);
    }

    public void Blur()
    {

    }

    //public void GetSprite(Sprite sprite)
    //{
    //    Debug.Log("GetSprite");
    //    for (int i = 0; i < Images.Length; i++)
    //    {
    //        if (Images[i].sprite == null)
    //        {
    //            Images[i].sprite = sprite;
    //            Images[i].enabled = true;
    //            return;
    //        }
    //    }
    //}
    //public void UpdateItemData(WeaponPrefabTableData weaponData)
    //{
    //    Sprite sprite = weaponData.WeaponImage;
    //    for (int i = 0; i < weapons.Length; i++)
    //    {
    //        if (Images[i] == null)
    //        {
    //            Images[i].sprite = sprite;
    //        }
    //    }
    //}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //public void GetWeaponData(Sprite weponImage, int level, UpgradeCost cost, string name)
    //{
      
    //}
    

    //public void CostUpdate(WeaponPrefabTableData data)
    //{
    //    UpgradeCost cost = data.upgradeCosts;
    //    foreach (var item in cost.costs)
    //    {
    //        if (item.amount == 0)
    //            return;
    //        else if (item.amount > 0)
    //        {
    //            for (int i = 0; i < CostTexts.Length; i++)
    //            {
    //                CostTexts[i].text = $"{item.itemType.ToString()} {item.amount}개 필요";
    //            }
    //        }
    //    }
    //}



    ////public void ResultUpdate(WeaponPrefabTableData data)
    ////{
        

    ////    //AP.text = cost;
    ////    AS.text = "SaveData의 AS만큼";
    ////    MB.text = "SaveData의 MB만큼";
    ////}

    //public void LevelUpdate(WeaponPrefabTableData data)
    //{
    //    PreLv.text = "0";
    //    AfterLv.text = "1";
    //}



}



