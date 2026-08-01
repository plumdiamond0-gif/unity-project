using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SceneOpening : SceneBase
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
    public override void Init()
    {
        GM.GetUIManager().GetRootCanvas(Rootcanvas.transform);
        GM.GetUIManager().CreateUIPanel<PanelOpening>("GameStart",
              (go) =>
              {
                  panelOpening = go;
                  Debug.Log($"panelOpening load");
              });
        GM.GetSoundManager().PlayBGM(AudioType.Opening);
    }
}

 


