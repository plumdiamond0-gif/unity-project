using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float CurrentHp;
    public float MaxHp;
    bool isInvincible = false;
    public Image HealthBarFill;
    public float InvincibleTime = 0.1f;

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
    public void MaxHpPlus(float value)
    {
        MaxHp += value;
        CurrentHp = Mathf.Clamp(CurrentHp, 0, MaxHp);

        HealthBarFill.fillAmount = CurrentHp / MaxHp;
    }
    IEnumerator InvincibleTimer()
    {
        isInvincible = true;
        yield return new WaitForSeconds(InvincibleTime);
        isInvincible = false;

        yield return null;
    }

    public void InviciBuff(float val)
    {
        StartCoroutine(InviTime(val));
    }
    IEnumerator InviTime(float val)
    {
        InvincibleTime = val;
        yield return new WaitForSeconds(InvincibleTime);
        InvincibleTime = 0.1f;
    }


}

