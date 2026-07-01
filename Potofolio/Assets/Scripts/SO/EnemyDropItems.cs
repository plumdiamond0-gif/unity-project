using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DropItem
{ 
   public OutItemType itemType;
    public float spawnNum;
}



[CreateAssetMenu(menuName = "Data/EnemyDropItems")]
public class EnemyDropItems : ScriptableObject
{
    public List<DropItem> dropItems =
        new List<DropItem>();
}
