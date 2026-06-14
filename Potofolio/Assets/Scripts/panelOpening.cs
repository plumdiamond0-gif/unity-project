using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PanelOpening : PanelBase
{
    CanvasGroup canvasGroup;
    [SerializeField] private float fadeTime;
    Image image;
    TMP_Text text;
    public int currentNum = 0;
    [System.Serializable]
    public struct CutScenes
    {
        public Sprite sprite;
        public string texts;
    }
    public CutScenes[] cutScenes;
    public bool IsPlaying { get; private set; }
    public bool IsFinished =>
    currentNum >= cutScenes.Length;




    public override void Init()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        image = GetComponent<Image>();
        text = GetComponent<TMP_Text>();
    }

    public override void Show()
    {
        if (currentNum >= cutScenes.Length)
            return;
        if (currentNum == 0)
        {
            text.transform.position = new Vector3(0, 180);
        }
        else
        {
            text.transform.position = new Vector3(0, -260);
        }
        image.sprite = cutScenes[currentNum].sprite;
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
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
    }

    void Update()
    {
        if (!IsPlaying)
        {
            if (cutScenes[currentNum].texts != null)
            {
                text.text = cutScenes[currentNum].texts;
                float alpha = (Mathf.Sin(Time.time * 3f) + 1) * 0.5f;
                text.alpha = alpha;
            }
        }
    }

    public override void Hide()
    {
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        canvasGroup.alpha = 1;

        float time = 0;

        while (time < fadeTime)
        {
            time += Time.deltaTime;

            canvasGroup.alpha = 1 - (time / fadeTime);

            yield return null;
        }

        canvasGroup.alpha = 0;
        Destroy(gameObject);



    }

}


