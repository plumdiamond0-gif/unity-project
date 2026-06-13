using UnityEngine;
using UnityEngine.UI;

public class PanelWeapon : PanelBase
{
    [SerializeField] private Button Upgrade;
    [SerializeField] private Button Unlock;
    PlayerMovement Player;
    [SerializeField] private Button Exit;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Upgrade.onClick.AddListener(UpgradeAppear);
        Unlock.onClick.AddListener(UnlockAppear);
        Exit.onClick.AddListener(MoveAgain);
    }

    public void GetPlayer(PlayerMovement player)
    {
        Debug.Log("wefwe");
        Player = player;    
        if(Player != null)
        {
            Debug.Log("≥Œ æ∆πÃ");
        }
    }

    public void MoveAgain()
    {
        Player.CanMove=true;
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
