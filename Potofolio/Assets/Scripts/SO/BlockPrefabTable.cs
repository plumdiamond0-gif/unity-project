using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BlockData
{
    public GameObject blockPrefab;
    public Sprite blockImage;
}

[CreateAssetMenu(menuName = "Data/BlockPrefabTable")]
public class BlockPrefabTable :ScriptableObject
{
    public List<BlockData> blockDatas = new();
}
