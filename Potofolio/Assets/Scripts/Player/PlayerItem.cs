using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    [SerializeField] private TMP_Text CoinText;
    float coinNum;
    Dictionary<OutItemType, int> resourcesData = new();
    void Start()
    {
        coinNum = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag("Coin"))
    //    {
    //        coinNum += 10;
    //        UpdateCoin(coinNum);
    //        Destroy(other.gameObject);
    //    }
    //}

    public void UpdateCoin(float coinNum)
    {
        CoinText.text = coinNum.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Item item = other.GetComponent<Item>();
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
                //TODO : 이 딕셔너리에 특정 자원 키값이 없다면 그 자원은 아직 없다는 거겠지

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
