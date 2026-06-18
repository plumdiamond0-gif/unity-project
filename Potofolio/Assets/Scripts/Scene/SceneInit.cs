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
            OnNextSceneLoad();

        });
    }

    private void OnNextSceneLoad()
    {
        GM.GetSceneLoadManager().NextLoadScene("SceneBase", () =>
            {
                Debug.Log("SceneBase 완료");
            });
    }






}
