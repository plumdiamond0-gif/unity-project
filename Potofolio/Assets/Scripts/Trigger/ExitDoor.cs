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
            GM.GetUIManager().CreateUIPanel<PanelBaseExit>("BaseExit_Panel",
            (go) =>
            {
                canPanel = false;
                Debug.Log($"panelBaseExit load");
                go.Exit.onClick.AddListener(() =>
                {
                    canPanel = true;
                });
                
            });
        }
    }

}
