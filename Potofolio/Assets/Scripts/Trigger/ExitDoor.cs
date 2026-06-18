using UnityEngine;

public class ExitDoor : TriggerObject
{
    private void Start()
    {

    }

    protected override void Trigger(GameObject entered)
    {
        GM.GetUIManager().CreateUIPanel("BaseExit_Panel",
            (go) =>
            {
                Debug.Log($"panelBaseExit load");
                PanelBaseExit panelBaseExit= go.GetComponent<PanelBaseExit>();
                PlayerMovement player = entered.GetComponent<PlayerMovement>();
                player.CanMove = false;
                if (player != null)
                {
                    Debug.Log("null พฦดิ");
                }
                panelBaseExit.GetPlayer(player);
            });
    }

}
