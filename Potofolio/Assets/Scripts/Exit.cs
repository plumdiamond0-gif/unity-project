using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    Button buttonExit;
   [SerializeField]private  GameObject currentPanel;

    public void GetPanel(GameObject panel)
    {
        currentPanel = panel;   
    }

    private void Start()
    {
        buttonExit = GetComponent<Button>();
        buttonExit.onClick.AddListener(RemovePanel);

    }

    void RemovePanel()
    {
        currentPanel.SetActive(false);
    }
}
