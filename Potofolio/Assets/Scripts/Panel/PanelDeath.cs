using System.Collections;
using UnityEngine;

public class PanelDeath : PanelBase
{
    public override void Show()
    {
        transform.localPosition = new Vector3(0, 1000, 0);
        StartCoroutine(MovePanel());
    }
    IEnumerator MovePanel()
    {
        while (transform.localPosition != Vector3.zero)
        {
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition,
                Vector3.zero,
                2600f * Time.deltaTime);

            yield return null;
        }
        yield return new WaitForSeconds(1);
        GoToBase();
    }
    void GoToBase()
    {
        GM.GetSceneLoadManager().NextLoadScene("SceneSpaceShip", () =>
        {
            Debug.Log("SceneSpaceShip ¿Ï·á");
        });
    }
}
