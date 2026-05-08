using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{



    public GameObject AttackGuageBar;

    public Image AttackGuageBarFill;

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



    public void ChargeBarActive(bool canCharge)
    {
        if (canCharge)
        {
            AttackGuageBar.SetActive(true);
        }
        else
        {
            AttackGuageBar.SetActive(false);
        }
    }
}
