using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float CurrentHp;
    public float MaxHp;
    bool isInvincible = false;
    [SerializeField] private  Image HealthBarFill;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HealthBarFill = GetComponent<Image>();
        CurrentHp = MaxHp;
    }

public void TakeHealth(float value)
    {
        if(isInvincible)
        {
            return;
        }
        CurrentHp += value;
        HealthBarFill.fillAmount = CurrentHp / MaxHp;   
        StartCoroutine(InvincibleTimer());


    }

    IEnumerator InvincibleTimer()
    {
        isInvincible = true;
        yield return new WaitForSeconds(0.5f);
        isInvincible = false;

        yield return null;
    }


}

