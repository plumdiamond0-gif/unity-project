using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;
using static UpgradeResults;
using static WeaponPrefabTable;

public class UpgradeResultUI : MonoBehaviour
{
    //[SerializeField] private List<TMP_Text> texts;
    private Image[] images = new Image[5];
    private TMP_Text[] texts = new TMP_Text[5];
    [SerializeField] private TMP_Text preLv;
    [SerializeField] private TMP_Text AftLv;


    public void Start()
    {
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

    public void ShowResults(WeaponPrefabData data)
    {
        if (data == null) return;
        UpgradeResults results = data.upgradeResults;
        WeaponState state = data.weaponState;
        int level = SaveManager.CurrentData.weaponlevel[state];
        preLv.text = level.ToString();
        AftLv.text = (level+1).ToString();
        for (int i = 0; i < 5; i++)
        {
            if (i < results.results.Count)
            {
                images[i].sprite = results.results[i].sprite;
                texts[i].text = results.results[i].type.ToString() + ":" + (results.results[i].amount) *
                                   Mathf.Pow(1.15f, level) + "->" + (results.results[i].amount) *
                                   Mathf.Pow(1.15f, level + 1);

            }

            else
            {
                images[i].sprite = null;
                texts[i].text = null;
            }
            
        }
    }
}
        //임의로 설정해놓은 수
        //{
        //    if (i < upgradeResults.results.Count)
        //    {
        //        UpgradeResultsData ResultsData = 
        //            upgradeResults.results[i];

        //            switch (ResultsData.type)
        //            {
        //                case ResultType.None:
        //                    {
        //                        break;
        //                    }
        //                case ResultType.Damage:
        //                    {
        //                        images[i].sprite = ResultsData.sprite;
        //                        texts[i].text = $"{ResultsData.amount * (ResultsData.type[])} / " +
        //                        $"{SaveManager.CurrentData.weaponStates}";
        //                        break;
        //                    }
        //            case ResultType.Speed:
        //                {
        //                    images[i].sprite = ResultsData.sprite;
        //                    texts[i].text = $"{ResultsData.amount} / {SaveManager.CurrentData.}";
        //                    break;
        //                }
        //            case ResultType.BulletNum:
        //                {
        //                    images[i].sprite = ResultsData.sprite;
        //                    texts[i].text = $"{ResultsData.amount} / {SaveManager.CurrentData.}";
        //                    break;
        //                }


        //            }

                

        //    }

            //else
            //{
            //        images[i].sprite = null;
            //        texts[i].text = null;
            //}
             
        


    //    for(int i = 0; i < 5; i++) {
    //    {
    //    //    CostData upgradeCost= WeaponData.upgradeCosts.costs[i];
    //    //        WeaponData  = WeaponData.upgradeCosts.Find(
    //    //x => x.weaponType == targetType);
    
    //    //        UpgradeCost upgradeCost = 
    //    //    sprites[i] = WeaponData.upgradeCosts.costs[i].CostSPrite;

    //    //    string cost = $"{WeaponData.upgradeCosts.costs[i].amount.ToString()} / {GM.GetSaveManager().CurrentData.}";
    //    }
        
            
        

        

    
        


