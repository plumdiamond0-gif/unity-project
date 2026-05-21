using UnityEngine;
using UnityEngine.UI;

public class PanelWeapon : PanelBase
{
    [SerializeField] private Transform RootCanvas;
    [SerializeField] private Button Upgrade;
    [SerializeField] private Button Unlock;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GM.GetUIManager().GetRootCanvas(RootCanvas);
        Upgrade.onClick.AddListener(UpgradeAppear);
        Unlock.onClick.AddListener(UnlockAppear);
    }

    void UpgradeAppear()
    {
        
        GM.GetUIManager().CreateUIPanel("Upgrade_Panel", (go) =>
        {
            Debug.Log("Upgrade_Panel ∫∏ø©¡‹");
        });
    }
    void UnlockAppear()
    {
        GM.GetUIManager().CreateUIPanel("Unlock_Panel", (go) =>
        {
            Debug.Log("Unlock_Panel ∫∏ø©¡‹");
        });
    }
}
