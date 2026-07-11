using UnityEngine;

public class ExitDoor : TriggerObject
{
    private void Start()
    {

    }

    protected override void Trigger(GameObject entered)
    {
        if ((entered.CompareTag("Player")))
        {
            GM.GetUIManager().CreateUIPanel("BaseExit_Panel",
            (go) =>
            {
                Debug.Log($"panelBaseExit load");
                PanelBaseExit panelBaseExit = go.GetComponent<PanelBaseExit>();
                
            });
        }
    }

}
