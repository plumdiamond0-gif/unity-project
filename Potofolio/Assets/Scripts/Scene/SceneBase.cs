using UnityEngine;

public class SceneBase : MonoBehaviour
{
    [SerializeField] private Transform playerSpawnPos;
    [SerializeField] AudioClip Bgm;
    [SerializeField] private Transform RootCanvas;


    private void Awake()
    {
        RootCanvas = GameObject.FindGameObjectWithTag("RootCanvas").transform;
        GM.GetUIManager().GetRootCanvas(RootCanvas);

        GM.GetSoundManager().PlayBGM(Bgm);
        GM.GetAssetManager().LoadAsset<GameObject>("Player",
            (go)=>
            {
                GameObject player = Instantiate(go, playerSpawnPos.position, 
                    Quaternion.identity);
                GameManager.OnPlayerSpawned?.Invoke(player);
                PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
                playerAttack.enabled = false;   
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                playerMovement.CanMove = true;
                playerMovement.state = PlayerMovement.PlayerState.InBase;
                
                
                Debug.Log("Player ½ºÆùµÊ");
            });
    }

 


}
