using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject[] spawnObjects;
    Transform[] spawnPos;
    int spawnCount;

    private void Start()
    {
        spawnPos = GetComponentsInChildren<Transform>();

        Spawn();
    }

    void Spawn()
    {
        List<Transform> list = new List<Transform>();
        for (int i = 0; i < spawnPos.Length; i++)
        {
            if(spawnPos[i] != transform)
            {
                list.Add(spawnPos[i]);  
            }
        }


        Shuffle(list);
    }

    void Shuffle(List<Transform> transforms)
    {
        for (int i = 0; i < transforms.Count; i++)
        {

        }
    }

}
