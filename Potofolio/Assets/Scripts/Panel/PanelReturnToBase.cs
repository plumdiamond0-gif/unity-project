using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelReturnToBase : PanelBase
{
    [SerializeField] public Button Exit;
    [SerializeField] private Button ReturnToBase;

    public bool isEnded { get; private set; }

    public override void Show()
    {
        Exit.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;

            Destroy(gameObject);
        });
        ReturnToBase.onClick.AddListener(()=>
        {
            GoToBase();
            GM.GetSoundManager().PlaySFX(AudioType.Button);
            });
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
                800f * Time.deltaTime);

            yield return null;
        }
        Time.timeScale = 0f;

        isEnded = true;
    }
    void GoToBase()
    {
        if (!isEnded)
            return;
        Time.timeScale = 1f;
        Destroy(GameManager.instance.GetPlayer().gameObject);
        GM.GetSceneLoadManager().NextLoadScene("SceneSpaceShip", () =>
        {
            Debug.Log("SceneSpaceShip ¿Ï·á");
        });
    }

}
