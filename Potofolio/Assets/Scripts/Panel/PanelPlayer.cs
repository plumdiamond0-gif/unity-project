using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelPlayer : MonoBehaviour
{
    public Image HpUI;
    public GameObject ChargeUI;
    public Image ChargeImageUI;
    public Image ExpFillImageUI;
    public Image WeapomImage;
    public TMP_Text WeapomText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        ChargeUI.SetActive(false);
    }
}
