using UnityEngine;

public class ExitDoor : TriggerObject
{
    bool canPanel = true;
    protected override void Trigger(GameObject entered)
    {
        if(!canPanel)
            return;
        if ((entered.CompareTag("Player")))
        {
            GM.GetUIManager().CreateUIPanel("BaseExit_Panel",
            (go) =>
            {
                canPanel = false;
                Debug.Log($"panelBaseExit load");
                PanelBaseExit panelBaseExit = go.GetComponent<PanelBaseExit>();
                panelBaseExit.Exit.onClick.AddListener(() =>
                {
                    canPanel = true;
                });
                
            });
        }
    }

}
