using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


class InventorySlot
{
    public ItemData item;
    public InventorySlot(ItemData item)
    {
        this.item = item;
    }
}
public class PlayerItem : MonoBehaviour
{
    [SerializeField] private TMP_Text CoinText;

    Dictionary<OutItemType, int> resourcesData = new();
    public Image ExpFillImageUI;
    public float currentExp = 0;
    public float maxExp;

    public Image[] Inventory = new Image[4];

    InventorySlot[] inventorySlots = new InventorySlot[4];
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
            ItemData data = item.data;

            if (item.itemType == ItemType.InGameItem)
            {
                InItemType itemType = item.inType;
                if (data.isInvenItem)
                {
                    for (int i = 0; i < Inventory.Length; i++)
                    {
                        if (Inventory[i].sprite == null)
                        {
                            Inventory[i].sprite = data.itemSprite;
                            Debug.Log("weqgwergqergrwegwerhergher");
                            inventorySlots[i] = new InventorySlot(data);
                            Destroy(other.gameObject);
                            return;
                        }
                    }
                }
                else
                    ItemEffect.Use(data);

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
                ItemEffect.Restore(itemType);

            }
            Destroy(other.gameObject);

        }

    }
    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            UseInvenItem(1);
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            UseInvenItem(2);
        }

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            UseInvenItem(3);
        }

        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            UseInvenItem(4);
        }
    }

    void UseInvenItem(int num)
        {
            if (inventorySlots[num-1] != null)
            {
                ItemEffect.Use(inventorySlots[num - 1].item);
                inventorySlots[num - 1] = null;
                Inventory[num - 1].sprite = null;
            }

        }

    
}
