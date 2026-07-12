using UnityEngine;

public class Item : MonoBehaviour
{
    [HideInInspector]public ItemData data;
    public ItemType itemType;
    public OutItemType outType;
    public InItemType  inType;

    private void Awake()
    {
        if(itemType == ItemType.InGameItem)
        {
            data = GM.GetPrefabManager().ItemPrefabTable.ItemDatas.Find(x => x.inItemType == inType);

        }
        else
        {
            data = GM.GetPrefabManager().ItemPrefabTable.ItemDatas.Find(x => x.outItemType == outType);

        }

    }
   
}
