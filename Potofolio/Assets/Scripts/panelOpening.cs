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
        text = GetComponentInChildren<TMP_Text>();
    }

    public override void Show()
    {

        if (currentNum >= cutScenes.Length)
            return;
        if (currentNum == 0)
        {
            text.transform.localPosition = new Vector3(0, 180);
        }
        else
        {
            text.transform.localPosition = new Vector3(0, -260);
        }
        image.sprite = cutScenes[currentNum].sprite;
        text.text = cutScenes[currentNum].texts;
        text.alpha = 1;

        StartCoroutine(FadeIn());
        currentNum++;

    }
    IEnumerator FadeIn()
    {
        IsPlaying = true;

        canvasGroup.alpha = 0;

        float time = 0;

        while (time < fadeTime)
        {
            time += Time.deltaTime;

            canvasGroup.alpha = time / fadeTime;

            yield return null;
        }

        canvasGroup.alpha = 1;
        if(currentNum == 0)
        yield return new WaitForSeconds(1.5f);

        else
            yield return new WaitForSeconds(3f);

        IsPlaying = false;
    }

    void Update()
    {
        if (!IsPlaying && currentNum < cutScenes.Length)
        {
            if (cutScenes[currentNum].texts != null)
            {
              
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


