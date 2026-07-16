using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    Button buttonExit;
   [SerializeField]private  GameObject currentPanel;


    private void Start()
    {
        buttonExit = GetComponent<Button>();
        currentPanel = transform.parent.gameObject;
        buttonExit.onClick.AddListener(() => { RemovePanel(); GM.GetSoundManager().PlaySFX(AudioType.Button); });

    }

    void RemovePanel()
    {
        currentPanel.SetActive(false);
    }
}
