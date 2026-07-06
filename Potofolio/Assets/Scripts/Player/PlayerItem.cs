using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    [SerializeField] private TMP_Text CoinText;

    Dictionary<OutItemType, int> resourcesData = new();
    public Image ExpFillImageUI;
    public float currentExp = 0;
    public float maxExp;


    public bool canShowPanel = true;
    public void GetExp(float exp)
    {
        currentExp += exp;
        UpdateExpUI();
    }

    void UpdateExpUI()
    {
        while (currentExp >= maxExp )
        {
            if(!canShowPanel)
                return;
            canShowPanel = false;

            currentExp = currentExp - maxExp;

            maxExp *= 1.5f;
            GM.GetUIManager().CreateUIPanel("Reward_Panel",
            (go) =>
            {
                go.SetActive(true);
                PanelReward panel = go.GetComponent<PanelReward>();
                StartCoroutine(CheckEnd(panel));
                
            });
        }

        currentExp = Mathf.Clamp(currentExp, 0, maxExp);
        Debug.Log("Clamp");
        float ratio = currentExp / maxExp;
        ExpFillImageUI.fillAmount = ratio;

    }

    IEnumerator CheckEnd(PanelReward panel)
    {
        while(!panel.isEnded)
        {
            yield return null;
            canShowPanel = true;
            UpdateExpUI();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Item item = other.GetComponent<Item>();
            ItemData data = GM.GetPrefabManager().ItemPrefabTable.ItemDatas.Find(x => x.inItemType == item.inType);

            Debug.Log(item.data.inItemType.ToString());
            if (item.data.itemType == ItemType.InGameItem)
            {
                InItemType itemType = item.data.inItemType;
                item.Use(itemType, gameObject);
            }
            else if (item.data.itemType == ItemType.OutGameItem)
            {
                OutItemType itemType = item.data.outItemType;
                if (!resourcesData.ContainsKey(itemType))
                {
                    Debug.Log("지금 먹은 아이템타입에 " +
                        "해당하는 키값이 없어서 추가함");
                    resourcesData.Add(itemType, 1);
                }
                else
                {
                    Debug.Log($"{itemType.ToString()}값 하나 증가");
                    resourcesData[itemType] += 1;
                }

                //item.Restore(itemType);
            }
            Destroy(other.gameObject);

        }



        //if (other.CompareTag("InventoryItem"))
        //{
        //    InventoryItem item = other.GetComponent<InventoryItem>();
        //    if (item != null)
        //    {
        //        item.Use();
        //    }
        //}

        //else if (other.CompareTag("DataItem"))
        //{
        //  DataItem item = other.GetComponent<DataItem>();
        //    if (item != null)
        //    {
        //        item.GetItem();
        //    }
        //}
    }
}
