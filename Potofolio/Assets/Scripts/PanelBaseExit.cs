using UnityEngine;
using UnityEngine.UI;

public class PanelBaseExit : MonoBehaviour
{
    [SerializeField] private Button Exit;
    [SerializeField] private Button GoBattle;


    PlayerMovement Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Exit.onClick.AddListener(MoveAgain);
        GoBattle.onClick.AddListener(MoveToSceneBattle);
    }
    void MoveToSceneBattle()
    {
        GM.GetSceneLoadManager().NextLoadScene("SceneBattle", () =>
        {
            Debug.Log("SceneBattle 완료");
        });
    }
    
    public void GetPlayer(PlayerMovement player)
    {
        Player = player;
    }

    public void MoveAgain()
    {
        Debug.Log("다시 움직ㄴㅇ라");
        Player.CanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
