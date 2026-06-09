using UnityEngine;

public class GoBattleGate : TriggerObject
{
    [SerializeField] private Transform playerSpawnPos;

    protected override void Trigger(GameObject entered)
    {
        GM.GetSceneLoadManager().
            NextLoadScene("SceneBattle",
            () =>
            {
                GM.GetAssetManager().LoadAsset<GameObject>("Player",
            (go) =>
            {
                GameObject player = Instantiate(go, playerSpawnPos.position,
                    Quaternion.identity);
                GameManager.instance.GetPlayer(go);
                Debug.Log("Player ½ºÆùµÊ");
            });
            });
    }
}
