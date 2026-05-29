using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject[] BlockPrefabs;

    Transform[] blockPositions;

    private void Start()
    {
        blockPositions = new Transform[transform.childCount];
        for (int i = 0; i < blockPositions.Length; i++)
        {
            blockPositions[i] = transform.GetChild(i);
        }
        BlockPrefabs = GetComponentsInChildren<GameObject>();

        CreateMap();
    }

    void CreateMap()
    {
        List<GameObject> randomBlocks = new List<GameObject>(BlockPrefabs);
        shuffle(randomBlocks);
        for (int i = 0;i < randomBlocks.Count;i++)
        {
            Instantiate(randomBlocks[i], blockPositions[i].position, 
                Quaternion.identity, blockPositions[i]);
        }
    }

    void shuffle(List<GameObject> list)
    {
        for (int i = 0;i < list.Count;i++)
        {
            int random = Random.Range(0, list.Count);
            GameObject temp = list[i];  
            list[i] = list[random];
            list[random] = temp;
        }
    }


}
