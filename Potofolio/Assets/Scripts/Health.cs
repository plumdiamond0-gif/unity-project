using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float CurrentHp;
    public float MaxHp;
    bool isInvincible = false;
    public Image HealthBarFill;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        CurrentHp = MaxHp;
    }

public void TakeDamage(float value)
    {
        if(isInvincible)
        {
            return;
        }
        CurrentHp -= value;
        CurrentHp = Mathf.Clamp(CurrentHp, 0, MaxHp);
        HealthBarFill.fillAmount = CurrentHp / MaxHp;   
        StartCoroutine(InvincibleTimer());


    }

    public void Heal(float value)
    {
        
        CurrentHp += value;
        CurrentHp = Mathf.Clamp(CurrentHp, 0, MaxHp);

        HealthBarFill.fillAmount = CurrentHp / MaxHp;


    }
        IEnumerator InvincibleTimer()
    {
        isInvincible = true;
        yield return new WaitForSeconds(0.5f);
        isInvincible = false;

        yield return null;
    }


}

