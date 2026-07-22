using UnityEngine;
using UnityEngine.UI;

public class WeaponImageUI : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.sprite = null;
    }
    public void Show(Sprite sprite)
    {
        image.sprite = sprite;  
    }
}
