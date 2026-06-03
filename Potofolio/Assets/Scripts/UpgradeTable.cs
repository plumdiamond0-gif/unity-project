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
         PlayerMovement playerMovement = entered.GetComponent<PlayerMovement>();
        playerMovement.CanMove = false;
        GM.GetUIManager().CreateUIPanel("Weapon_Panel",
            (go) =>
            {
                Debug.Log("Weapon_Panel »ý¼ºÇÔ");
            });
    }
}
