using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PanelOpening : PanelBase
{
    CanvasGroup canvasGroup;
    [SerializeField] private float fadeTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Init()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void Show()
    {
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
