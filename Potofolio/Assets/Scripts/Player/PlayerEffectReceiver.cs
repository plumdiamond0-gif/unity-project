using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectReceiver : MonoBehaviour, IWeaponEffectReceiver
{
    PlayerStat playerStat;
    PlayerMovement playerMovement;
    Health playerHealth;

    Coroutine stunRoutine;
    Coroutine dotdamRoutine;
    Coroutine slowRoutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStat = GetComponent<PlayerStat>();
        playerHealth = GetComponent<Health>();
    }

    public void ApplySlow(float slowTime, float slowAmount)
    {
        if (stunRoutine != null)
            return;
        if (slowRoutine != null)
        {
            slowRoutine = null;
        }
        slowRoutine = StartCoroutine(Slow(slowTime, slowAmount));

    }
    IEnumerator Slow(float slowTime, float slowAmount)
    {
        playerStat.MoveSpeed *= slowAmount;
        yield return new WaitForSeconds(slowTime);
        playerStat.MoveSpeed /= slowAmount;

        yield return null;
    }
    public void ApplyStun(float stunTime)
    {
        if (stunRoutine != null)
            stunRoutine = null;
        StartCoroutine(Stun(stunTime));

    }
    IEnumerator Stun(float stunTime)
    {
        Debug.Log("Slowed");
        playerMovement.CanMove = false;
        yield return new WaitForSeconds(stunTime);
        playerMovement.CanMove = true;
        yield return null;
    }
    public void ApplyDotDam(float dotDamage, float dotNum)
    {
        if (dotdamRoutine != null)
            dotdamRoutine = null;
        StartCoroutine(Dotdam(dotDamage, dotNum));

    }
    IEnumerator Dotdam(float dotDamage, float dotTime)
    {
        Debug.Log("Slowed");
        for (int i = 0; i < dotTime; i++)
        {
            playerHealth.TakeDamage(dotDamage);
            Debug.Log(dotDamage);
            yield return new WaitForSeconds(0.8f);

        }
        yield return null;
    }

}
