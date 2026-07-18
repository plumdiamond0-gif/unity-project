using UnityEngine;

public class Item : MonoBehaviour
{
    [HideInInspector]public ItemData data;
    public ItemType itemType;
    public OutItemType outType;
    public InItemType  inType;
    public WeaponState bulletType;

    private void Awake()
    {
        if(itemType == ItemType.InGameItem)
        {
            data = GM.GetPrefabManager().ItemPrefabTable.ItemDatas.Find(x => x.inItemType == inType);
            if(inType == InItemType.None)
                data = GM.GetPrefabManager().ItemPrefabTable.ItemDatas.Find(x => x.weaponState == bulletType);
        }
        else
        {
            data = GM.GetPrefabManager().ItemPrefabTable.ItemDatas.Find(x => x.outItemType == outType);
        }

    }
   
}
