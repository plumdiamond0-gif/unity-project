using UnityEngine;

public class SaveDesk : TriggerObject
{
    protected override void Trigger(GameObject entered)
    {
        if (!entered.CompareTag("Player"))
        { return; }
        GM.GetUIManager().CreateUIPanel<PanelSave>("Save_Panel",
                (go) =>
                {
                    PlayerMovement player = entered.GetComponent<PlayerMovement>();
                    player.canMove = false;
                    go.GetPlayer(player);
                });
    }

}
