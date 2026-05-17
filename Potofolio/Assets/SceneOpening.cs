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
            return;
        }
        if (nextPanelNum != 0)
        {
            // CanvasGroup canvas = currentPanel.GetComponent<CanvasGroup>();
            // FadeOut(canvas, fadeTime);
            Destroy(currentPanel);
            currentPanel = null;
        }
        string panelName = panelList[nextPanelNum];
        GM.GetUIManager().CreateUIPanel(panelName,
            (go) =>
            {
                nextPanelNum++;
                currentPanel = go;
                CanvasGroup panel = go.GetComponent<CanvasGroup>();
                currnetText = currentPanel.GetComponentInChildren<TMP_Text>();
                Debug.Log($"{panelName} load");
                StartCoroutine(FadeIn(panel, fadeTime));
            });
    }
    IEnumerator FadeIn(CanvasGroup canvasGroup, float fadeTime)
    {
        canvasGroup.alpha = 0;

        float time = 0;

        while (time < fadeTime)
        {
            time += Time.deltaTime;

            canvasGroup.alpha = time / fadeTime;

            yield return null;
        }

        canvasGroup.alpha = 1;
        yield return  GlobalCallback.WaitForSeconds(1f);
        CanStart = true;
    }
    //IEnumerator FadeOut(CanvasGroup canvasGroup, float fadeTime)
    //{
    //    canvasGroup.alpha = 1;

    //    float time = 0;

    //    while (time < fadeTime)
    //    {
    //        time += Time.deltaTime;

    //        canvasGroup.alpha = 1-( time / fadeTime);

    //        yield return null;
    //    }

    //    canvasGroup.alpha = 0;
    //    Destroy(currentPanel);



    //}


    // Update is called once per frame

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
                //Debug.Log("°ÔÀÓ½ÃÀÛ");
                CanStart = false;
            }
           
                
            

        }

    }
}

 


