using System;
using System.Collections;
using UnityEngine;

public class GameManager : SingletonObject<GameManager>
{
    public bool Initialized { get; private set; } = false;
    public Action OnInit { get; private set; }

    public PrefabManager GetPrefabManager { get; private set; } = null;
    public UIManager GetUIManager { get; private set; } = null;
    public AssetManager GetAssetManager { get; private set; } = null;
    public SceneLoadManager GetSceneLoadManager { get; private set; } = null;   
    public PrefabManager Get_PrefabManager()
    {
        return GetPrefabManager;
    }
    public UIManager Get_UIManager()
    {
        return GetUIManager;
    }
    public AssetManager Get_AssetManager()
    {
        return GetAssetManager;
    }
    public SceneLoadManager Get_SceneLoadManager()
    {
        return GetSceneLoadManager;
    }


    void Awake()
    {
        base.Awake();


        //prefabManager = PrefabManager.CreatePrefabManager(prefabManager.gameObject, transform);
        //UIManager = UIManager.CreateUIManager(UIManager.gameObject, transform);


    }

    public void Init(Action OnInit)
    {
        if (Initialized)
        {
            OnInit?.Invoke();
            return;
        }
        this.OnInit = OnInit;
        StartCoroutine(ProcessInit());
    }

    IEnumerator ProcessInit()
    {
        { 
            Debug.Log("매니저 초기화 시작");

            {
                GameObject go = new GameObject("PrefabManager");
                go.transform.parent = transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                GetPrefabManager = go.AddComponent<PrefabManager>();

                Debug.Log("UIManager 초기화 완료");
            }
            {
                GameObject go = new GameObject("UIManager");
                go.transform.parent = transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                GetUIManager = go.AddComponent<UIManager>();

                Debug.Log("PrefabManager 초기화 완료");

            }
            {
                GameObject go = new GameObject("AssetManager");
                go.transform.parent = transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                GetAssetManager = go.AddComponent<AssetManager>();

                Debug.Log("AssetManager 초기화 완료");
            }
            {
                GameObject go = new GameObject("SceneLoadManager");
                go.transform.parent = transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                GetSceneLoadManager = go.AddComponent<SceneLoadManager>();

                Debug.Log("SceneLoadManager 초기화 완료");
            }
            Debug.Log("매니저 초기화 완료");

        }
        Initialized = true;
        OnInit?.Invoke();
        yield return null;
    }

    public GameObject GetPrefab(string prefabname, Vector3 spawnpos, Quaternion spawnrot)
    {
        var data = GetPrefabManager.WeaponPrefabTable.weaponPrafabTableDatas.Find(x => x.WeaponName == prefabname);
        if (data != null)
        {
            GameObject CBcoy = Instantiate(data.WeaponBullet, spawnpos, spawnrot);
            return CBcoy;
        }
        else
        {
            return null;
        }
    }



}
public static class GM
{
    public static PrefabManager GetPrefabManager()
    {

        return GameManager.instance.Get_PrefabManager();
    }
    public static UIManager GetUIManager()
    {
        Debug.Log("GetUIManager");
        return GameManager.instance.Get_UIManager();
    }
    public static AssetManager GetAssetManager()
    {
        Debug.Log("GetUIManager");
        return GameManager.instance.Get_AssetManager();
    }
    public static SceneLoadManager GetSceneLoadManager()
    {
        Debug.Log("GetUIManager");
        return GameManager.instance.Get_SceneLoadManager();
    }
}

