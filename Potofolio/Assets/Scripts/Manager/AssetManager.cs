using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class AssetManager : MonoBehaviour
{
    private readonly Dictionary<string, AsyncOperationHandle> _dicHandles = new();
    public void LoadAsset<T>(string key, Action<T> callback)
    { StartCoroutine(ProcessLoad(key, callback)); }
    private IEnumerator ProcessLoad<T>(string key, Action<T> callback)
    {
        if (_dicHandles.TryGetValue(key, out AsyncOperationHandle asyncOperationHandle))
        {
            yield return asyncOperationHandle.IsDone;
            callback.Invoke((T)asyncOperationHandle.Result);
            yield break;
        }
        AsyncOperationHandle handle = Addressables.LoadAssetAsync<T>(key);
        _dicHandles[key] = handle;
        yield return handle;
        if (handle.Status == AsyncOperationStatus.Succeeded)
            callback.Invoke((T)handle.Result);

    }
    public void Release(string key)
    {
        if (_dicHandles.TryGetValue(key, out var handle))
        {
            Addressables.Release(handle);
            _dicHandles.Remove(key);
        }
    }
}
