using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SceneOpening : MonoBehaviour
{
    public float fadeTime = 2f;
    bool CanStart;
    bool IsStart;
    string canvasName;
    public Canvas Rootcanvas;
    TMP_Text text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CanStart = false;
        IsStart = false;
        canvasName = "GameStart";
        GM.GetUIManager().GetRootCanvas(Rootcanvas.transform);
        GM.GetUIManager().CreateUIPanel(canvasName,
            (go) =>
            {
                CanvasGroup image = go.GetComponent<CanvasGroup>();
                text = go.GetComponentInChildren<TMP_Text>();
                Debug.Log($"{canvasName} load");
                StartCoroutine(FadeIn(image));
            });

       
    }

    IEnumerator FadeIn(CanvasGroup canvasGroup)
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
        CanStart = true;


    }


    // Update is called once per frame

    void Update()
    {

        if (CanStart)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                OpeningStart();
                Debug.Log("게임시작");
            }
            float alpha = (Mathf.Sin(Time.time * 3f) + 1) * 0.5f;
            if(text!= null)
            {
                text.alpha = alpha;

            }
        }

    }

    void OpeningStart()
    {

    }

}
