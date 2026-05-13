using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GlobalCallback : MonoBehaviour
{
    private static readonly Dictionary<float, WaitForSeconds> _dicWaitForSeconds = new();

    public static WaitForSeconds WaitForSeconds(float sec)
    {
        if(_dicWaitForSeconds.TryGetValue(sec, out WaitForSeconds result) == false)
        {
            result = new WaitForSeconds(sec);
            _dicWaitForSeconds.Add(sec, result);
        }
        return result;
    }

    public static IEnumerator SetTimeOut(float sec, Action callback)
    {
        yield return WaitForSeconds(sec);
        callback.Invoke();
    }

    public static IEnumerator SetWaitUntil(Func<bool> predicate, Action callback)
    {
        yield return new WaitUntil(predicate);
        callback.Invoke();  
    }
}
