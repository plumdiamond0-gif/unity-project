using UnityEngine;
using UnityEngine.UI;

public class PanelReturnToBase : PanelBase
{
    [SerializeField] private Button Exit;
    [SerializeField] private Button ReturnToBase;
    void Start()
    {
        Exit.onClick.AddListener(()=>
        {
            Destroy(gameObject);
        });
        ReturnToBase.onClick.AddListener(GoToBase);
    }
    void GoToBase()
    {
        Destroy(GameManager.instance.GetPlayer().gameObject);
        GM.GetSceneLoadManager().NextLoadScene("SceneBase", () =>
        {
            Debug.Log("SceneBase ¿Ï·á");
        });
    }
}
