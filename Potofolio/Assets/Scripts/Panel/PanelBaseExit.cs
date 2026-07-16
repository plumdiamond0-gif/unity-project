using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PanelBaseExit : PanelBase
{
    [SerializeField] public Button Exit;
    [SerializeField] private Button GoBattle;


    public bool isEnded { get; private set; }

    public override void Init()
    {
        Exit.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;

            Destroy(gameObject);
        });
        GoBattle.onClick.AddListener(()=>
        {
            MoveToSceneBattle();
            GM.GetSoundManager().PlaySFX(AudioType.Button);
        } );

        transform.localPosition = new Vector3(0, 1000, 0);

        StartCoroutine(MovePanel());

    }
    IEnumerator MovePanel()
    {
        while (transform.localPosition != Vector3.zero)
        {
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition,
                Vector3.zero,
                800f * Time.deltaTime);

            yield return null;
        }
        isEnded = true;
        Time.timeScale = 0f;


    }
    void MoveToSceneBattle()
    {
        if(!isEnded)
            return;
        Time.timeScale = 1f;

        GM.GetSceneLoadManager().NextLoadScene("SceneBattle", () =>
        { 
            Debug.Log("SceneBattle ¿Ï·á");
        });
    }
}
