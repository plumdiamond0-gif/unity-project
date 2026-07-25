using UnityEngine;
using UnityEngine.Rendering;

public class UpgradeTable : TriggerObject
{

    private void Start()
    {


    }
    protected override void Trigger(GameObject entered)
    {
        if ((entered.CompareTag("Player")))
        {
            GM.GetUIManager().CreateUIPanel<PanelWeapon>("Weapon_Panel",
                (go) =>
                {
                    Debug.Log("Weapon_Panel »ý¼ºÇÔ");
                    PlayerMovement player = entered.GetComponent<PlayerMovement>();
                    player.canMove = false;
                    if (player != null)
                    {
                        Debug.Log("null ¾Æ´Ô");
                    }
                    go.GetPlayer(player);
                });
           
            //player.CanMove = false;
        }

    }
}
