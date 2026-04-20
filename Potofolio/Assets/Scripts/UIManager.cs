using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Image HealthBarFill;

    public Image AttackGuageBarFill;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    public void UpdatePlayerHealth(float MaxHp, float CurrentHp)
    {
        CurrentHp = gamemanager.instance.PlayerMovement.PlayerHp;
        HealthBarFill.fillAmount = CurrentHp/ MaxHp;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
