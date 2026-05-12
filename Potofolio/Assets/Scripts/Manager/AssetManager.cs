using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetManager : MonoBehaviour
{
    private readonly Dictionary<string, AsyncOperationHandle> _dicHadles = new Dictionary<string, AsyncOperationHandle>();

    public void LoadAsset<T>(string key, Action<T> callback)
    {
        StartCoroutine(ProcessLoad(key, callback));
    }

    public void LoadScene(string key, Action callback)
    {
        AsyncOperationHandle handle = Addressables.LoadSceneAsync(key);
        handle.Completed += (item) => {
            callback?.Invoke();
        };
    }

    private IEnumerator ProcessLoad<T>(string key, Action<T> callback)
    {
        if (_dicHadles.TryGetValue(key, out AsyncOperationHandle asyncOperationHandle))
        {
            yield return GlobalCallback.SetWaitUntil(() =>
            {
                return asyncOperationHandle.IsDone;
            },
            () =>
            {
                Debug.Log($"리소스 로드 완료: {key}");
                callback?.Invoke((T)asyncOperationHandle.Result);
            });
        }

        AsyncOperationHandle handle = Addressables.LoadAssetAsync<T>(key);
        _dicHadles[key] = handle;

        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log($"리소스 로드 완료: {key}");
            callback?.Invoke((T)handle.Result);
        }
        else
        {
            Debug.LogError($"데이터 로드 실패: {key}");
        }
    }

    public void Release(string key)
    {
        if (_dicHadles.TryGetValue(key, out AsyncOperationHandle asyncOperationHandle))
        {
            Addressables.Release(asyncOperationHandle);
            _dicHadles.Remove(key);
        }

    }

    public void ReleaseAll()
    {
        foreach (var item in _dicHadles)
        {
            Addressables.Release(item);
        }

        _dicHadles.Clear();
    }
}
