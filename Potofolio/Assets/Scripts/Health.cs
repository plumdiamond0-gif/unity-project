using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float Hp;
    bool isInvincible = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

public void TakeHealth(float value)
    {
        if(isInvincible)
        {
            return;
        }
        StartCoroutine(InvincibleTimer());

        Debug.Log("weff");

        Hp += value;
    }

    IEnumerator InvincibleTimer()
    {
        isInvincible = true;
        yield return new WaitForSeconds(0.5f);
        isInvincible = false;

        yield return null;
    }


}

