using UnityEngine;

public class SceneBase : MonoBehaviour
{
    [SerializeField] private Transform playerSpawnPos;
    private void Awake()
    {
        GM.GetAssetManager().LoadAsset<GameObject>("Player",
            (go)=>
            {
                GameObject player = Instantiate(go, playerSpawnPos.position, 
                    Quaternion.identity);
                PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
                playerAttack.enabled = false;   
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>(); 
                playerMovement.state = PlayerMovement.PlayerState.InBase;
                
                
                Debug.Log("Player ½ºÆùµÊ");
            });
    }

 


}
