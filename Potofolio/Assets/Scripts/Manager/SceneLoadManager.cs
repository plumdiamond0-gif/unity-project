using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{

    public string NextSceneName { get; private set; }
    public string CurrentSceneName => SceneManager.GetActiveScene().name;
    public void NextLoadScene(string nextSceneName, Action OnSceneCompleted)
    {
        if (string.IsNullOrEmpty(nextSceneName))
            return;
        NextSceneName = nextSceneName;
        StartCoroutine(ProcessEmptyScene(OnSceneCompleted));
    }
    private IEnumerator ProcessEmptyScene(Action OnSceneCompleted)
    {
        yield return SceneManager.LoadSceneAsync("SceneEmpty");
        yield return unloadUnusedAssets();
        StartCoroutine(ProcessNextScene(OnSceneCompleted));
    }

    private IEnumerator ProcessNextScene(Action OnSceneCompleted)
    {
        if (string.IsNullOrEmpty(NextSceneName))
            yield break;
        float elapsed = 0f;
        const float minLoadingTime = 2f;

        AsyncOperation handle = SceneManager.LoadSceneAsync(NextSceneName);

        handle.allowSceneActivation = false;
        
        while (handle.progress < 0.9f || elapsed < minLoadingTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        handle.allowSceneActivation = true;

        while (!handle.isDone)
            yield return null;

        SceneBase sceneBase = (SceneBase)FindAnyObjectByType(typeof(SceneBase));
        sceneBase.Init();
        OnSceneCompleted?.Invoke();

        yield return unloadUnusedAssets();
    }

    private IEnumerator unloadUnusedAssets()
    {
        AsyncOperation async = Resources.UnloadUnusedAssets();
        yield return async;

        GC.Collect();
    }
}
