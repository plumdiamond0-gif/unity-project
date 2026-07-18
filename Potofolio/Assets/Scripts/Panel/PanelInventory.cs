using System;
using System.Collections;
using System.Linq;
using UnityEditor.Build.Pipeline.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PanelInventory : PanelBase
{
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
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        StartCoroutine(MovePanel());
    }
    IEnumerator MovePanel()
    {
        for (int i = 0; i < 10; i++)
        {

        }
        Time.timeScale = 0f;

        isEnded = true;
    }

    private void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f;
            boolChange?.Invoke();
            GM.GetSoundManager().PlaySFX(AudioType.SpecialBtn);
            Destroy(gameObject);
        }
    }




}
