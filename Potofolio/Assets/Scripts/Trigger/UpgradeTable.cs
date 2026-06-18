using UnityEngine;
using UnityEngine.Rendering;

public class UpgradeTable : TriggerObject
{
    [SerializeField] private Transform RootCanvas;

    private void Start()
    {
        RootCanvas = GameObject.FindGameObjectWithTag("RootCanvas").transform;
        GM.GetUIManager().GetRootCanvas(RootCanvas);

    }
    protected override void Trigger(GameObject entered)
    {
        if ((entered.CompareTag("Player")))
        {
            GM.GetUIManager().CreateUIPanel("Weapon_Panel",
                (go) =>
                {
                    Debug.Log("Weapon_Panel »ý¼ºÇÔ");
                    PanelWeapon panelWeapon = go.GetComponent<PanelWeapon>();
                    PlayerMovement player = entered.GetComponent<PlayerMovement>();
                    player.CanMove = false;
                    if (player != null)
                    {
                        Debug.Log("null ¾Æ´Ô");
                    }
                    panelWeapon.GetPlayer(player);
                });
           
            //player.CanMove = false;
        }

    }
}
