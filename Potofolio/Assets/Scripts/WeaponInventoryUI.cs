
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static WeaponPrefabTable;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
public class WeaponInventoryUI
 : MonoBehaviour
{
    Button[] buttons;
    public Image Upgraded;
    public TMP_Text[] CostTexts;
    //public TMP_Text AP;
    //public TMP_Text AS;
    //public TMP_Text MB;

    public TMP_Text PreLv;
    public TMP_Text AfterLv;

    void Start()
    {
        
        //AP.enabled = false;
        //AS.enabled = false;
        //MB.enabled = false;
        buttons = new Button[transform.childCount];
        PreLv.enabled = false;
        AfterLv.enabled = false;
        Upgraded.sprite = null;
        for (int i = 0; i < transform.childCount; i++)
        {
            buttons[i] = transform.GetChild(i).GetComponent<Button>();
        }
    
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
    public void GetWeaponData(Sprite weponImage, int level, UpgradeCost cost, string name)
    {
      
    }
    public void Show(WeaponPrefabTableData.WeaponState weaponState, bool isActive)
    {
        if(!isActive)
        {
            return;
        }

        WeaponPrefabTableData data =
        GM.GetPrefabManager().WeaponPrefabTable.weaponPrafabTableDatas.Find(
        x => x.weaponState == weaponState);


        //ResultUpdate(data);
        LevelUpdate(data);
        //AP.enabled = true;
        //AS.enabled = true;
        //MB.enabled = true;
        CostUpdate(data);
        Upgraded.sprite = data.WeaponImage;
        PreLv.enabled=true;
        AfterLv.enabled = true; 

    }
    public void CostUpdate(WeaponPrefabTableData data)
    {
        UpgradeCost cost = data.upgradeCosts;
        foreach (var item in cost.costs)
        {
            if (item.amount == 0)
                return;
            else if (item.amount > 0)
            {
                for (int i = 0; i < CostTexts.Length; i++)
                {
                    CostTexts[i].text = $"{item.itemType.ToString()} {item.amount}개 필요";
                }
            }
        }
    }



    //public void ResultUpdate(WeaponPrefabTableData data)
    //{
        

    //    //AP.text = cost;
    //    AS.text = "SaveData의 AS만큼";
    //    MB.text = "SaveData의 MB만큼";
    //}

    public void LevelUpdate(WeaponPrefabTableData data)
    {
        PreLv.text = "0";
        AfterLv.text = "1";
    }



}



