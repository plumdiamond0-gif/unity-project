using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private readonly GameObject prefab;
    private readonly Queue<GameObject> poolObjects = new();
    private readonly Transform parent;
    public ObjectPool(GameObject _object, int count, Transform parent) 
    {
        prefab = _object;
        this.parent = parent;
        for (int i = 0; i < count; ++i)
        {
            GameObject go = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
            poolObjects.Enqueue(go);
            go.SetActive(false);
        }
    }
    public GameObject Get()
    {
        if (poolObjects.Count > 0)
        {
            GameObject go = poolObjects.Dequeue();
            go.SetActive(true);
            return go;
        }
        else
        {
            GameObject go = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
            go.SetActive(true);
            return go;
        }
    }
    public void Return(GameObject go)
    {
        go.transform.SetParent(parent);
        go.SetActive(false);
        poolObjects.Enqueue(go);
    }
}
