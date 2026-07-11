using UnityEngine;
using UnityEngine.UI;

public class PanelBaseExit : MonoBehaviour
{
    [SerializeField] private Button Exit;
    [SerializeField] private Button GoBattle;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Exit.onClick.AddListener(()=>
        {
            GameManager.instance.GetPlayer().
         GetComponent<PlayerMovement>().canMove = true;
        });
        GoBattle.onClick.AddListener(MoveToSceneBattle);
    }
    void MoveToSceneBattle()
    {
        GM.GetSceneLoadManager().NextLoadScene("SceneBattle", () =>
        {
            Debug.Log("SceneBattle ¿Ï·á");
        });
    }
    


    // Update is called once per frame
    void Update()
    {
        
    }
}
