using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PanelOpening : PanelBase
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] private float fadeTime;
    [SerializeField]Image image;
    public Sprite[] sprites;
    public int currentNum = 0;
    TMP_Text[] texts;
    public bool IsPlaying { get; private set; }
    public bool IsFinished =>
    currentNum >= sprites.Length;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public override void Init()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
    }

    public override void Show()
    {
        if (currentNum >= sprites.Length)
            return;
        Debug.Log(currentNum);
        image.sprite = sprites[currentNum];
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
        yield return new WaitForSeconds(2f);

        IsPlaying = false;
    }

    //void Update()
    //{
    //    if (CanStart)
    //    {
    //        if (texts[nextNum] != null)
    //        {
    //            float alpha = (Mathf.Sin(Time.time * 3f) + 1) * 0.5f;
    //            texts[nextNum].alpha = alpha;
    //        }
    //    }
    //}

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



    }

}
