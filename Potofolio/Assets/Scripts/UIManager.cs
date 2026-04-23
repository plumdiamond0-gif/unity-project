using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Image HealthBarFill;

    public Image AttackGuageBarFill;

    public Health PlayerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
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
