using UnityEngine;

public class SceneBattle : MonoBehaviour
{
    [SerializeField] private Transform playerSpawnPos;
    [SerializeField] AudioClip Bgm;
    [SerializeField] private Transform RootCanvas;


    private void Awake()
    {
        RootCanvas = GameObject.FindGameObjectWithTag("RootCanvas").transform;
        GM.GetUIManager().GetRootCanvas(RootCanvas);

        GM.GetUIManager().CreateUIPanel("Player_Panel",
            (go) =>
            {
                GM.GetUIManager().GetHUD(go.GetComponent<PanelPlayer>());
            });
        GM.GetAssetManager().LoadAsset<GameObject>("Player",
            (go) =>
            {
                GameObject player = Instantiate(go, playerSpawnPos.position,
                    Quaternion.identity);
                GameManager.OnPlayerSpawned?.Invoke(player);
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                playerMovement.CanMove = true;
                playerMovement.state = PlayerMovement.PlayerState.InBattle;
                Debug.Log("Player ½ºÆùµÊ");
            });



    }
}
