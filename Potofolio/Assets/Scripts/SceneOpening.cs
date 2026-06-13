using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SceneOpening : MonoBehaviour
{
    public Canvas Rootcanvas;

    [SerializeField]PanelOpening panelOpening;
    //List<string> panelList = new List<string>
    //{
    //    "GameStart",
    //    "cutScene_1",
    //    "cutScene_2",
    //    "cutScene_3",
    //    "cutScene_4",
    //};
    void Start()
    {
        GM.GetUIManager().GetRootCanvas(Rootcanvas.transform);
        GM.GetUIManager().CreateUIPanel("GameStart",
              (go) =>
              {
                  panelOpening = go.GetComponent<PanelOpening>();
                  Debug.Log($"panelOpening load");

              });
      

        //panelNext();
    }
    //void panelNext()
    //{
    //    if(nextSpriteNum >= sprites.Length)
    //    {
    //        Debug.Log("ÄÆ¾À Á¾·á");
    //        GM.GetSceneLoadManager().NextLoadScene("SceneBase",
    //            () =>
    //            {
    //                Debug.Log("SceneBase ·Îµå ¿Ï·á");
    //            });
    //        return;
    //    }
    //    if (nextPanelNum != 0)
    //    {
    //        //Destroy(currentPanel);
    //        //currentPanel = null;
    //    }
    //    string panelName = panelList[nextPanelNum];
    //    GM.GetUIManager().CreateUIPanel(panelName,
    //        (go) =>
    //        {

    //            nextPanelNum++;
    //            currentPanel = go;
    //            Debug.Log($"{panelName} load");
    //            StartCoroutine(WaitStart());
    //        });
    //}
    //IEnumerator WaitStart()
    //{
    //   panelOpening.CanStart = false;

    //    yield return GlobalCallback.WaitForSeconds(2f);

    //    panelOpening.CanStart = true;
    //}
    void Update()
    {
        if (panelOpening != null)
        {
            if (!panelOpening.IsPlaying)
            {
                if (Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                    if (panelOpening.IsFinished)
                    {
                        Debug.Log("ÄÆ¾À Á¾·á");
                        GM.GetSceneLoadManager().NextLoadScene("SceneBase",
                            () =>
                            {
                                Debug.Log("SceneBase ·Îµå ¿Ï·á");
                            });
                        return;
                    }
                    panelOpening.Show();
                }
            }

        }
    }
}

 


