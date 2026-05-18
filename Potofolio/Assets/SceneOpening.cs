using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SceneOpening : MonoBehaviour
{
    bool CanStart;
    public Canvas Rootcanvas;

    GameObject currentPanel;
    TMP_Text currnetText;

    List<string> panelList = new List<string>
    {
        "GameStart",
        "cutScene_1",
        "cutScene_2",
        "cutScene_3",
        "cutScene_4",
    };

    int nextPanelNum = 0;

    void Start()
    {
        CanStart = false;
        GM.GetUIManager().GetRootCanvas(Rootcanvas.transform);
        panelNext(2f);
    }
    void panelNext(float fadeTime)
    {
        if(nextPanelNum >= panelList.Count)
        {
            Debug.Log("ÄÆ¾À Á¾·á");
            GM.GetSceneLoadManager().NextLoadScene("SceneBase",
                () =>
                {
                    Debug.Log("SceneBase ·Îµå ¿Ï·á");
                });
            return;
        }
        if (nextPanelNum != 0)
        {
            Destroy(currentPanel);
            currentPanel = null;
        }
        string panelName = panelList[nextPanelNum];
        GM.GetUIManager().CreateUIPanel(panelName,
            (go) =>
            {

                nextPanelNum++;
                currentPanel = go;
                Debug.Log($"{panelName} load");
                StartCoroutine(WaitStart());
            });
    }
    IEnumerator WaitStart()
    {
        CanStart = false;

        yield return GlobalCallback.WaitForSeconds(2f);

        CanStart = true;
    }
    void Update()
    {
        if (CanStart)
        {
            if (currnetText != null)
            {
                float alpha = (Mathf.Sin(Time.time * 3f) + 1) * 0.5f;
                currnetText.alpha = alpha;
            }
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                panelNext(1f);
                CanStart = false;
            }
           
                
            

        }

    }
}

 


