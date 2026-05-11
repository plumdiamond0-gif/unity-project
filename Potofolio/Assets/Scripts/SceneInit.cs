using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class SceneInit : MonoBehaviour
{
  
    private void Awake()
    {
        GameManager.instance.Init( () =>
        {
            Debug.Log("여기서 씬 이동");
        });
    }








    //public CanvasGroup canvasGroup;
    //public float fadeTime = 2f;
    //bool CanStart;
    //public TMP_Text text;
    //bool IsStart;

    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //    CanStart = false;
    //    IsStart = false;
    //    StartCoroutine(FadeIn());
    //}

    //IEnumerator FadeIn()
    //{
    //    canvasGroup.alpha = 0;

    //    float time = 0;

    //    while (time < fadeTime)
    //    {
    //        time += Time.deltaTime;

    //        canvasGroup.alpha = time / fadeTime;

    //        yield return null;
    //    }

    //    canvasGroup.alpha = 1;
    //    CanStart = true;


    //}


    //// Update is called once per frame

    //void Update()
    //{

    //    if (!CanStart) 
    //        return;
    //    if (Keyboard.current.spaceKey.wasPressedThisFrame)
    //    {
    //        Debug.Log("게임시작");
    //    }
    //    float alpha = (Mathf.Sin(Time.time * 3f) + 1) * 0.5f;
    //    text.alpha = alpha;


    //}

}
