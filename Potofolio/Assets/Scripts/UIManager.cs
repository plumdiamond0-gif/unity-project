using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Image HealthBarFill;
    private float MaxHp;
    private float CurrentHp;
    public Image AttackGuageBarFill;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MaxHp = gamemanager.instance.Player.PlayerHp;
        CurrentHp = gamemanager.instance.Player.PlayerHp;
    }

    public void UpdatePlayerHealth()
    {
        CurrentHp = gamemanager.instance.Player.PlayerHp;
        HealthBarFill.fillAmount = CurrentHp/ MaxHp;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
