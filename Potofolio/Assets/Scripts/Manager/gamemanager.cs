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
    public SaveManager GetSaveManager { get; private set; }=null;
    public SoundManager GetSoundManager { get; private set; } = null;
    public PoolManager GetPoolManager { get; private set; } 

    public static Action<GameObject> OnPlayerSpawned;
    public static Action<PanelPlayer> OnPlayerPanelSpawned;



    GameObject player;


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
    public SaveManager Get_SaveManager()
    {
        return GetSaveManager;
    }
    public SoundManager Get_SoundManager()
    {
        return GetSoundManager;
    }
    public PoolManager Get_EffectManager()
    {
        return GetPoolManager;
    }

    void Awake()
    {
        base.Awake();
        OnPlayerSpawned += SavePlayer;
    }
    void SavePlayer(GameObject go)
    {
        Debug.Log("플레이어 저장");
        player = go;
    }
    public GameObject GetPlayer()
    {
        return player;
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
                GameObject go = new GameObject("AssetManager");
                go.transform.parent = transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                GetAssetManager = go.AddComponent<AssetManager>();

                Debug.Log("AssetManager 초기화 완료");
            }
            {
                GameObject go = new GameObject("PrefabManager");
                go.transform.parent = transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                GetPrefabManager = go.AddComponent<PrefabManager>();

                bool done = false;

                GetPrefabManager.Init(() =>
                {
                    done = true;
                });
                yield return new WaitUntil(() => done);
                Debug.Log("PrefabManager 초기화 완료");
            }
            {
                GameObject go = new GameObject("UIManager");
                go.transform.parent = transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                GetUIManager = go.AddComponent<UIManager>();
                OnPlayerSpawned += GetUIManager.Bind;

                Debug.Log("UIManager 초기화 완료");

            }

            {
                GameObject go = new GameObject("SceneLoadManager");
                go.transform.parent = transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                GetSceneLoadManager = go.AddComponent<SceneLoadManager>();

                Debug.Log("SceneLoadManager 초기화 완료");
            }

            {
                GameObject go = new GameObject("SaveManager");
                go.transform.parent = transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                GetSaveManager = go.AddComponent<SaveManager>();
                GetSaveManager.Init();

                Debug.Log("SaveManager 초기화 완료");
            }
            {
                GameObject go = new GameObject("SoundManager");
                go.transform.parent = transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                GetSoundManager = go.AddComponent<SoundManager>();
                GetSoundManager.Init();

                Debug.Log("SoundManager 초기화 완료");
            }
            {
                GameObject go = new GameObject("GetPoolManager");
                go.transform.parent = transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                GetPoolManager = go.AddComponent<PoolManager>();
                GetPoolManager.Init();
                Debug.Log("PoolManager 초기화 완료");
            }
            Debug.Log("매니저 초기화 완료");

        }
        Initialized = true;
        OnInit?.Invoke();
        yield return null;
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
        return GameManager.instance.Get_UIManager();
    }
    public static AssetManager GetAssetManager()
    {
        return GameManager.instance.Get_AssetManager();
    }
    public static SceneLoadManager GetSceneLoadManager()
    {
        return GameManager.instance.Get_SceneLoadManager();
    }
    public static SaveManager GetSaveManager()
    {
        return GameManager.instance.Get_SaveManager();
    }
    public static SoundManager GetSoundManager()
    {
        return GameManager.instance.Get_SoundManager();
    }
    public static PoolManager GetPoolManager()
    {
        return GameManager.instance.Get_EffectManager();
    }
}

