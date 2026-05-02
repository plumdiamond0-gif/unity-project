using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Image HealthBarFill;

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

    public void UpdatePlayerHealth(float MaxHp, float CurrentHp)
    {
        CurrentHp = PlayerHealth.Hp;
        HealthBarFill.fillAmount = CurrentHp/ MaxHp;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
