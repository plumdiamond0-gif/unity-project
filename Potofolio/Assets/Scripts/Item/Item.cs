using UnityEngine;

public abstract class Item : MonoBehaviour
{
    //float dir = 1;

    bool isused = false;
    public Sprite sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    public void Use()
    {
        if(isused)
        {
            return;
        }
        isused = true;
        Onuse();
        GM.GetUIManager().Inventory.GetSprite(sprite);


    }

    public virtual void Onuse()
    {

    }    

    // Update is called once per frame
   
}
