using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{




    public Health PlayerHealth;

    public Inventory Inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static UIManager CreateUIManager(GameObject res, Transform parent)
    {
        if (res == null)
        {
            return null;
        }
        GameObject gameObject = Instantiate(res, parent);

        return gameObject.GetComponent<UIManager>();
    }



  
}
