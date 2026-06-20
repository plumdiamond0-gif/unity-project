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
                Debug.Log("Player ½ºÆùµÊ");

             
            });
    }
}
