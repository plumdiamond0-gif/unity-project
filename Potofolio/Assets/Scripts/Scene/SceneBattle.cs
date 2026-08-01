using UnityEngine;
using UnityEngine.InputSystem;

public class SceneBattle : SceneBase
{
    [SerializeField] private Transform playerSpawnPos;
    [SerializeField] private Transform RootCanvas;
    bool canPanel = true;


    public override void Init()
    {
        GM.GetSoundManager().PlayBGM(AudioType.Battle);
        GameManager.OnPlayerSpawned += GM.GetUIManager().Bind;

        RootCanvas = GameObject.FindGameObjectWithTag("RootCanvas").transform;
        GM.GetUIManager().GetRootCanvas(RootCanvas);

        GM.GetUIManager().CreateUIPanel<PanelPlayer>("Player_Panel",
            (go) =>
            {
                PanelPlayer panelPlayer = go;
                GM.GetUIManager().SaveHUD(go);
                GameManager.OnPlayerPanelSpawned?.Invoke(panelPlayer);


                GM.GetAssetManager().LoadAsset<GameObject>("Player",
            (playerob) =>
            {
                GameObject player = Instantiate(playerob, playerSpawnPos.position,
                    Quaternion.Euler(Vector3.zero));
                GameManager.OnPlayerSpawned?.Invoke(player);
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                playerMovement.canMove = true;
                playerMovement.state = PlayerState.InBattle;
                PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
                playerAttack.CanAttack = true;


            });
            });

    }
    private void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            if(!canPanel)
                return;
            canPanel = false;
            GM.GetUIManager().CreateUIPanel<PanelReturnToBase>("ReturnToBase_Panel", (go) =>
            {
                GM.GetSoundManager().PlaySFX(AudioType.SpecialBtn);
                go.Exit.onClick.AddListener(() =>
                {
                    canPanel = true;
                });
                Debug.Log("ReturnToBase");
            });
        }
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            if (!canPanel)
                return;
            canPanel = false;
            GM.GetUIManager().CreateUIPanel<PanelInventory>("Inventory_Panel", (go) =>
            {
                GM.GetSoundManager().PlaySFX(AudioType.SpecialBtn);
                go.boolChange += ()=>
                { canPanel = true;
                    GM.GetUIManager().RemoveKey("InventoryPanel");
                };
            });
        }

    }
}
