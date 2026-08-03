using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PanelInventory : PanelBase
{
    private bool isMaxed = false;
    private bool isEnded = false;
    Image[] inventories;
    public Action boolChange;

    public override void Init()
    {
        inventories = GetComponentsInChildren<Image>().Where(x => x.gameObject != gameObject).ToArray();
    }
    public override void Show()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        StartCoroutine(MaxiPanel());
    }
    IEnumerator MaxiPanel()
    {
        isEnded = false ;
        transform.localScale = Vector3.one;
        float duration = 0.5f;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = timer / duration;
            float value = Mathf.Pow(t, 2);

            transform.localScale = Vector3.one * value;

            yield return null;
        }

        transform.localScale = Vector3.one;

        Time.timeScale = 0f;
        isEnded = true;
        isMaxed = true;
    }

    IEnumerator MiniPanel()
    {
        isEnded = false;
        transform.localScale = Vector3.one;
        Time.timeScale = 1f;

        float duration = 1f;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = 1 - (timer / duration);
            transform.localScale *= t;  
            yield return null;
        }
        isMaxed = false;
        isEnded = true;
        GM.GetAssetManager().Release("Inventory_Panel");
        boolChange?.Invoke();
        Destroy(gameObject);
    }

    private void Update()
    {
        if(!isEnded) return;
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            GM.GetSoundManager().PlaySFX(AudioType.SpecialBtn);
            StartCoroutine(isMaxed ? MiniPanel() : MaxiPanel());
        }
    }
}
