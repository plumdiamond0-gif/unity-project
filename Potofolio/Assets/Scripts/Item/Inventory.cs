using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Inventory : MonoBehaviour
{
    public Image[] Images;

    private void Start()
    {
       for (int i = 0; i < Images.Length; i++)
        {
            Images[i].enabled = false;  
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GetSprite(Sprite sprite)
    {
        Debug.Log("GetSprite");
        for (int i = 0; i < Images.Length; i++)
        {
            if (Images[i].sprite == null)
            {
                Images[i].sprite = sprite;
                Images[i].enabled = true;
                return;
            }
        }
    }
}
