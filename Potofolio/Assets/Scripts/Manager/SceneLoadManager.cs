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

        //if (_assetManager == null)
        //{
        //    yield break;
        //}
        //_assetManager.LoadScene(NextSceneName, OnSceneCompleted);
        //yield return handle;
        Debug.Log("BEFOREBEFOREBEFOREBEFOREBEFOREBEFOREBEFOREBEFOREBEFOREBEFOREBEFORE");

        float elapsed = 0f;
        const float minLoadingTime = 2f;

        AsyncOperation handle = SceneManager.LoadSceneAsync(NextSceneName);

        handle.allowSceneActivation = false;

        Debug.Log(handle.progress);
        Debug.Log(handle.isDone);

        while (handle.progress < 0.9f || elapsed < minLoadingTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        handle.allowSceneActivation = true;

        while (!handle.isDone)
            yield return null;

        Debug.Log("AFTERAFTERAFTERAFTERAFTERAFTERAFTERAFTERAFTERAFTERAFTER");

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
