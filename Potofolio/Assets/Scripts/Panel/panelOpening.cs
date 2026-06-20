using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PanelOpening : PanelBase
{
    CanvasGroup canvasGroup;
    [SerializeField] private float fadeTime;
    Image image;
    TMP_Text text;
    private int currentNum = 0;
    [SerializeField] AudioClip button;
    [SerializeField] AudioClip bgm;


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
        GM.GetSoundManager().PlayBGM(bgm);

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

        currentNum++;
        IsPlaying = true;

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
        yield return new WaitForSeconds(currentNum == 1 ? 1.5f : 3f);

        IsPlaying = false;
    }

    void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            currentNum = cutScenes.Length;
            GM.GetSoundManager().StopBGM();
            Debug.Log("ÄÆ¾À Á¾·á");
            GM.GetSceneLoadManager().NextLoadScene("SceneBase",
                () =>
                {
                    Debug.Log("SceneBase ·Îµå ¿Ï·á");
                });
            return;
        }
            if (!IsPlaying)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                GM.GetSoundManager().PlaySFX(button);
                if (IsFinished)
                {
                    GM.GetSoundManager().StopBGM();
                    Debug.Log("ÄÆ¾À Á¾·á");
                    GM.GetSceneLoadManager().NextLoadScene("SceneBase",
                        () =>
                        {
                            Debug.Log("SceneBase ·Îµå ¿Ï·á");
                        });
                    return;
                }
                Show();
            }
        }

        int shownIndex = currentNum - 1;

        if (!IsPlaying && shownIndex >= 0)
        {
            if (!string.IsNullOrEmpty(cutScenes[shownIndex].texts))
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


