using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image[] Images;
    Sprite[] Sprites; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GetSprite(Sprite sprite)
    {
        Debug.Log("GetSprite");
        for (int i = 0; i < Images.Length; i++)
        {
            if (Images[i].sprite == null)
            {
                Images[i].sprite = sprite;  
                return;
            }
        }
    }
}
