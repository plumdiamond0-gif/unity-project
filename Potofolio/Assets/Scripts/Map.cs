using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    GameObject[] BlockPrefabs;

    Transform[] blockPositions;

    

    private void Start()
    {
        GameManager.OnPlayerPanelSpawned += CreateMap;
        blockPositions = new Transform[transform.childCount];
        for (int i = 0; i < blockPositions.Length; i++)
        {
            blockPositions[i] = transform.GetChild(i);
        }

    }

    void CreateMap(PanelPlayer playerPanel)
    {
        List<BlockData> randomBlocks = new();
        foreach (var item in GM.GetPrefabManager().BlockPrefabTable.blockDatas)
        {
            randomBlocks.Add(item);
        }
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
            Vector3 rot = new Vector3(0, rand, 0);

             playerPanel.miniMapImages[i].sprite = randomBlocks[i].blockImage;

            GameObject block = Instantiate(randomBlocks[i].blockPrefab, blockPositions[i].position, 
                Quaternion.Euler(rot)
                , blockPositions[i]);
            block.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }

        GameManager.OnPlayerPanelSpawned -= CreateMap;


    }

    void shuffle(List<BlockData> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int random = Random.Range(0, list.Count);
            BlockData temp = list[i];  
            list[i] = list[random];
            list[random] = temp;
        }
    }


}
