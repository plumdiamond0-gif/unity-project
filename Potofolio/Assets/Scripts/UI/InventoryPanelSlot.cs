using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class InventoryPanelSlot : MonoBehaviour
{
    Image image;
    TMP_Text text;
    [SerializeField]private OutItemType itemType;

    private void Awake()
    {
        if (itemType == OutItemType.None)
            Destroy(gameObject);
        image = GetComponent<Image>();
        text = GetComponentInChildren<TMP_Text>();
    }
    private void Start()
    {

        image.sprite = GM.GetPrefabManager().ItemPrefabTable.ItemDatas.Find(x => x.outItemType == itemType).itemSprite;
        text.text = SaveManager.CurrentData.itemStates[itemType].ToString();
    }
}
