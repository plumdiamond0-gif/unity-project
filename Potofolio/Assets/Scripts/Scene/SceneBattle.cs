using UnityEngine;

public class SceneBattle : MonoBehaviour
{
    [SerializeField] private Transform playerSpawnPos;
    [SerializeField] AudioClip Bgm;


    private void Awake()
    {
        //GM.GetSoundManager().PlayBGM(Bgm);
        GM.GetAssetManager().LoadAsset<GameObject>("Player",
            (go) =>
            {
                GameObject player = Instantiate(go, playerSpawnPos.position,
                    Quaternion.identity);
                GameManager.instance.GetPlayer(player);
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                playerMovement.CanMove = true;
                playerMovement.state = PlayerMovement.PlayerState.InBattle;
                Debug.Log("Player ½ºÆùµÊ");
            });
    }
}
