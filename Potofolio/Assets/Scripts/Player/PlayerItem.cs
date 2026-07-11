using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[System.Serializable]
public class InventorySlot
{
    public ItemData item;

    public InventorySlot(ItemData item)
    {
        this.item = item;
    }
}

public class PlayerItem : MonoBehaviour
{
    #region UI Elements
    [HideInInspector] public Image expFillImageUI;
    [HideInInspector] public Image[] inventoryImages = new Image[4];
    #endregion

    #region Experience Settings
    [Header("Experience Settings")]
    public float currentExp = 0f;
    public float maxExp = 100f;
    public bool canShowPanel = true;
    #endregion

    #region Internal Data
    private Dictionary<OutItemType, int> _resourcesData = new();
    private InventorySlot[] _inventorySlots = new InventorySlot[4];
    #endregion

    #region Experience Logic
    public void GetExp(float exp)
    {
        currentExp += exp;
        UpdateExpUI();
    }

    private void UpdateExpUI()
    {
        while (currentExp >= maxExp)
        {
            if (!canShowPanel)
                break; 

            canShowPanel = false;
            currentExp -= maxExp;
            maxExp *= 1.5f;

            GM.GetUIManager().CreateUIPanel("Reward_Panel", (go) =>
            {
                go.SetActive(true);
                PanelReward panel = go.GetComponent<PanelReward>();
                if (panel != null)
                {
                    StartCoroutine(CheckEnd(panel));
                }
            });
        }

        currentExp = Mathf.Clamp(currentExp, 0f, maxExp);
        float ratio = (maxExp > 0f) ? (currentExp / maxExp) : 0f;

        if (expFillImageUI != null)
        {
            expFillImageUI.fillAmount = ratio;
        }
    }

    private IEnumerator CheckEnd(PanelReward panel)
    {
        // PanelReward가 끝날 때까지 대기
        while (!panel.isEnded)
        {
            yield return null;
        }

        canShowPanel = true;
        UpdateExpUI(); // 대기하는 동안 쌓인 남은 경험치 추가 정산
    }
    #endregion

    #region Inventory & Item Logic
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Item")) return;

        Item item = other.GetComponent<Item>();
        if (item == null || item.data == null) return;

        ItemData data = item.data;

        if (item.itemType == ItemType.InGameItem)
        {
            if (data.isInvenItem)
            {
                // 인벤토리 빈 슬롯 검색 및 추가
                for (int i = 0; i < inventoryImages.Length; i++)
                {
                    if (inventoryImages[i] != null && inventoryImages[i].sprite == null)
                    {
                        inventoryImages[i].sprite = data.itemSprite;
                        _inventorySlots[i] = new InventorySlot(data);
                        Destroy(other.gameObject);
                        return;
                    }
                }
            }
            else
            {
                ItemEffect.Use(data);
            }
        }
        else if (data.itemType == ItemType.OutGameItem)
        {
            OutItemType itemType = data.outItemType;

            if (!_resourcesData.ContainsKey(itemType))
            {
                _resourcesData.Add(itemType, 1);
            }
            else
            {
                _resourcesData[itemType] += 1;
            }

            ItemEffect.Restore(itemType);
        }

        Destroy(other.gameObject);
    }

    private void Update()
    {
        HandleInventoryInput();
    }

    private void HandleInventoryInput()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame) UseInventoryItem(1);
        if (Keyboard.current.digit2Key.wasPressedThisFrame) UseInventoryItem(2);
        if (Keyboard.current.digit3Key.wasPressedThisFrame) UseInventoryItem(3);
        if (Keyboard.current.digit4Key.wasPressedThisFrame) UseInventoryItem(4);
    }

    private void UseInventoryItem(int slotIndex)
    {
        int arrayIndex = slotIndex - 1;

        if (arrayIndex >= 0 && arrayIndex < _inventorySlots.Length && _inventorySlots[arrayIndex] != null)
        {
            ItemEffect.Use(_inventorySlots[arrayIndex].item);
            _inventorySlots[arrayIndex] = null;

            if (inventoryImages[arrayIndex] != null)
            {
                inventoryImages[arrayIndex].sprite = null;
            }
        }
    }
    #endregion
}