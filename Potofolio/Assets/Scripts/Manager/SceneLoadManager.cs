using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private AssetManager _assetManager;

    public string NextSceneName { get; private set; }
    public string CurrentSceneName => SceneManager.GetActiveScene().name;

    public void Init(AssetManager assetManager)
    {
        _assetManager = assetManager;
    }

    public void NextLoadScene(string nextSceneName, Action OnSceneCompleted)
    {
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError("nextSceneName IsNullOrEmpty");
            return;
        }

        NextSceneName = nextSceneName;

        StartCoroutine(ProcessEmptyScene(OnSceneCompleted));
    }

    private IEnumerator ProcessEmptyScene(Action OnSceneCompleted)
    {
        yield return unloadUnusedAssets();


        yield return SceneManager.LoadSceneAsync("SceneEmpty");


        StartCoroutine(ProcessNextScene(OnSceneCompleted));
            
        
    }

    private IEnumerator ProcessNextScene(Action OnSceneCompleted)
    {
        if (string.IsNullOrEmpty(NextSceneName))
        {
            Debug.LogError("nextSceneName IsNullOrEmpty");
            yield break;
        }

        if (_assetManager == null)
        {
            yield break;
        }
        //_assetManager.LoadScene(NextSceneName, OnSceneCompleted);
        AsyncOperation handle =  SceneManager.LoadSceneAsync(NextSceneName);
        yield return handle;
        OnSceneCompleted?.Invoke();
        yield return unloadUnusedAssets();
    }

    private IEnumerator unloadUnusedAssets()
    {
        AsyncOperation async = Resources.UnloadUnusedAssets();
        yield return async;

        GC.Collect();
        GC.Collect();
        GC.Collect();
        GC.Collect();
        GC.Collect();
    }
}
