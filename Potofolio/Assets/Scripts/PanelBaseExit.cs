using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PanelBaseExit : PanelBase
{
    [SerializeField] private Button Exit;
    [SerializeField] private Button GoBattle;


    public bool isEnded { get; private set; }

    public override void Init()
    {
        Exit.onClick.AddListener(() =>
        {
            GameManager.instance.GetPlayer().
         GetComponent<PlayerMovement>().canMove = true;
        });
        GoBattle.onClick.AddListener(MoveToSceneBattle);

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
        Time.timeScale = 0f;
        isEnded = true;


    }
    void MoveToSceneBattle()
    {
        if(!isEnded)
            return;
        GM.GetSceneLoadManager().NextLoadScene("SceneBattle", () =>
        {
            Debug.Log("SceneBattle ¿Ï·á");
        });
    }
}
