using UnityEngine;
using UnityEngine.UI;

public class PanelPlayer : MonoBehaviour
{
    public Image HpUI;
    public GameObject ChargeUI;
    public Image ChargeImageUI;
    public GameObject ExpFillUI;
    public Image ExpFillImageUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        ChargeUI.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
