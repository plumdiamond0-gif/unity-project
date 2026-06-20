using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

        CreateMap();
    }

    void CreateMap()
    {
        List<GameObject> randomBlocks = new List<GameObject>(BlockPrefabs);
        shuffle(randomBlocks);
        int[] yRotations =
        {
            0,
            90,
            180,
            270
        };
        
        for (int i = 0;i < randomBlocks.Count;i++)
        {
            int rand = yRotations[Random.Range(0, yRotations.Length)];
            Debug.Log(rand);
            Vector3 rot = new Vector3(0, rand, 0);

            Instantiate(randomBlocks[i], blockPositions[i].position, 
                Quaternion.Euler(rot)
                , blockPositions[i]);
        }
    }

    void shuffle(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int random = Random.Range(0, list.Count);
            GameObject temp = list[i];  
            list[i] = list[random];
            list[random] = temp;
        }
    }


}
