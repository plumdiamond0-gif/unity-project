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

                //GameManager.OnPlayerSpawned?.Invoke(player);

                PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
                playerAttack.CanAttack = false;   
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                playerMovement.canMove = true;
                playerMovement.state = PlayerState.InBase;
                
                
                Debug.Log("Player ½ºÆùµÊ");
            });
    }

 


}
