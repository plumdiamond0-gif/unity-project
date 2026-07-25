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
    public EffectManager GetEffectManager { get; private set; } 
    public BulletManager GetBulletManager { get; private set; } = null;    
    public EnemyBulletManager GetEnemyBulletManager { get; private set; } = null;

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
    public EffectManager Get_EffectManager()
    {
        return GetEffectManager;
    }
    public BulletManager Get_BulletManager()
    {
        return GetBulletManager;
    }
    public EnemyBulletManager Get_EnemyBulletManager()
    {
        return GetEnemyBulletManager;
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
                GameObject go = new GameObject("EffectManager");
                go.transform.parent = transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                GetEffectManager = go.AddComponent<EffectManager>();
                Debug.Log("EffectManager 초기화 완료");
            }
            {
                GameObject go = new GameObject("BulletManager");
                go.transform.parent = transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                GetBulletManager = go.AddComponent<BulletManager>();
                Debug.Log("BulletManager 초기화 완료");
            }
            {
                GameObject go = new GameObject("EnemyBulletManager");
                go.transform.parent = transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                GetEnemyBulletManager = go.AddComponent<EnemyBulletManager>();
                Debug.Log("EnemyBulletManager 초기화 완료");
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
        Debug.Log("GetSceneLoadManager");
        return GameManager.instance.Get_SceneLoadManager();
    }
    public static SaveManager GetSaveManager()
    {
        Debug.Log("GetSaveManager");
        return GameManager.instance.Get_SaveManager();
    }
    public static SoundManager GetSoundManager()
    {
        Debug.Log("GetSoundManager");
        return GameManager.instance.Get_SoundManager();
    }
    public static EffectManager GetEffectManager()
    {
        Debug.Log("GetEffectManager");
        return GameManager.instance.Get_EffectManager();
    }
    public static BulletManager GetBulletManager()
    {
        Debug.Log("GetBulletManager");
        return GameManager.instance.Get_BulletManager();
    }
    public static EnemyBulletManager GetEnemyBulletManager()
    {
        Debug.Log("GetEnemyBulletManager");
        return GameManager.instance.Get_EnemyBulletManager();
    }


}

