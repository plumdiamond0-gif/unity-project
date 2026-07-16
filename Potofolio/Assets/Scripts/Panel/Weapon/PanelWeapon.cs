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
        Upgrade.onClick.AddListener(() =>{UpgradeAppear(); GM.GetSoundManager().PlaySFX(AudioType.Button);});
        Unlock.onClick.AddListener(() => { UnlockAppear();GM.GetSoundManager().PlaySFX(AudioType.Button); });
        Exit.onClick.AddListener(() => { MoveAgain(); GM.GetSoundManager().PlaySFX(AudioType.Button); });
    }

    public void GetPlayer(PlayerMovement player)
    {
        Player = player;    
    }

    public void MoveAgain()
    {
        Player.canMove =true;
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
