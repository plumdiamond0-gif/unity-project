using UnityEngine;

public class SceneBattle : MonoBehaviour
{
    [SerializeField] private Transform playerSpawnPos;
    [SerializeField] AudioClip Bgm;
    [SerializeField] private Transform RootCanvas;


    private void Awake()
    {
        GameManager.OnPlayerSpawned += GM.GetUIManager().Bind;

        RootCanvas = GameObject.FindGameObjectWithTag("RootCanvas").transform;
        GM.GetUIManager().GetRootCanvas(RootCanvas);

        GM.GetUIManager().CreateUIPanel("Player_Panel",
            (go) =>
            {
                PanelPlayer panelPlayer = go.GetComponent<PanelPlayer>();
                GM.GetUIManager().SaveHUD(go.GetComponent<PanelPlayer>());
                GameManager.OnPlayerPanelSpawned?.Invoke(panelPlayer);


                GM.GetAssetManager().LoadAsset<GameObject>("Player",
            (playerob) =>
            {
                GameObject player = Instantiate(playerob, playerSpawnPos.position,
                    Quaternion.Euler(Vector3.zero));
                GameManager.OnPlayerSpawned?.Invoke(player);
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                playerMovement.CanMove = true;
                playerMovement.state = PlayerMovement.PlayerState.InBattle;


            });
            });

    }
}
