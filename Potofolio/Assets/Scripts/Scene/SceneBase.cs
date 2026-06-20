using UnityEngine;

public class SceneBase : MonoBehaviour
{
    [SerializeField] private Transform playerSpawnPos;
    [SerializeField] AudioClip Bgm;


    private void Awake()
    {
        GM.GetSoundManager().PlayBGM(Bgm);
        GM.GetAssetManager().LoadAsset<GameObject>("Player",
            (go)=>
            {
                GameObject player = Instantiate(go, playerSpawnPos.position, 
                    Quaternion.identity);
                PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
                playerAttack.enabled = false;   
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                playerMovement.CanMove = true;
                playerMovement.state = PlayerMovement.PlayerState.InBase;
                
                
                Debug.Log("Player ½ºÆùµÊ");
            });
    }

 


}
