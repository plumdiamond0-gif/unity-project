using UnityEngine;
using UnityEngine.UI;

public class PanelPlayer : MonoBehaviour
{
    public Image HpUI;
    public GameObject ChargeUI;
    public Image ChargeImageUI;
    public Image ExpFillImageUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        ChargeUI.SetActive(false);
    }
}
