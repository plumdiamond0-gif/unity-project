using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerEffectReceiver : MonoBehaviour, IWeaponEffectReceiver
{
    PlayerStat playerStat;
    PlayerMovement playerMovement;
    Health playerHealth;
    Rigidbody rb;

    Coroutine stunRoutine;
    Coroutine dotdamRoutine;
    Coroutine slowRoutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStat = GetComponent<PlayerStat>();
        playerHealth = GetComponent<Health>();
        playerMovement = GetComponent<PlayerMovement>();    
        rb = GetComponent<Rigidbody>(); 
    }

    public void ApplySlow(float slowTime, float slowAmount)
    {
        if (slowRoutine != null) return;
        slowRoutine = StartCoroutine(Slow(slowTime, slowAmount));
    }
    IEnumerator Slow(float slowTime, float slowAmount)
    {
            Vector3 pos = new Vector3(transform.position.x,
                transform.position.y + 1, transform.position.z);
            GameObject go = GM.GetPoolManager().PlayParticle
                (ParticleType.Slow, pos, Quaternion.identity,
                new Vector3(2, 2, 2), transform);
        playerStat.MoveSpeedMultiplier *= slowAmount;
        yield return new WaitForSeconds(slowTime);
        GM.GetPoolManager().StopParticle(go);
        playerStat.MoveSpeedMultiplier /= slowAmount;
        slowRoutine = null;
        yield return null;
    }
    public void ApplyStun(float stunTime)
    {
        if (stunRoutine != null)
            return;
        stunRoutine = StartCoroutine(Stun(stunTime));

    }
    IEnumerator Stun(float stunTime)
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        GameObject go = GM.GetPoolManager().PlayParticle(ParticleType.Stun, pos, Quaternion.identity, new Vector3(2, 2, 2), transform);
        float orispeed = playerStat.MoveSpeedMultiplier;
        Rigidbody Rb = GetComponent<Rigidbody>();
        Rb.linearVelocity = new Vector3(0, Rb.linearVelocity.y, 0);
        playerStat.MoveSpeedMultiplier = 0;
        yield return new WaitForSeconds(stunTime);
        GM.GetPoolManager().StopParticle(go);
        playerStat.MoveSpeedMultiplier = orispeed;
        stunRoutine = null;

        yield return null;
    }
    public void ApplyFire(float dotDamage, float dotNum)
    {
        if (dotdamRoutine != null)
            return;
        dotdamRoutine = StartCoroutine(Fire(dotDamage, dotNum));
    }
    IEnumerator Fire(float dotDamage, float dotTime)
    {
        GameObject go = GM.GetPoolManager().PlayParticle(ParticleType.Fire, 
            transform.position, Quaternion.identity, Vector3.one, transform);
        for (int i = 0; i < dotTime; i++)
        {
            playerHealth.TakeDamage(dotDamage);
            Debug.Log(dotDamage);
            yield return new WaitForSeconds(0.8f);

        }
        GM.GetPoolManager().StopParticle(go);
        dotdamRoutine = null;
        yield return null;
    }

    public void ApplyToxic(float dotDamage, float dotNum)
    {
        if (dotdamRoutine != null)
            return;
        dotdamRoutine = StartCoroutine(Toxic(dotDamage, dotNum));

    }
    IEnumerator Toxic(float dotDamage, float dotTime)
    {
        GameObject go = GM.GetPoolManager().PlayParticle(ParticleType.Toxic, transform.position, Quaternion.identity, Vector3.one, transform);
        Debug.Log("Slowed");
        for (int i = 0; i < dotTime; i++)
        {
            playerHealth.TakeDamage(dotDamage);
            Debug.Log(dotDamage);
            yield return new WaitForSeconds(0.8f);

        }
        GM.GetPoolManager().StopParticle(go);
        dotdamRoutine = null;
        yield return null;

    }
    public void ApplyRangeDam(float damage, float range)
    {
        Vector3 point = transform.position;
        Collider[] colliders = Physics.OverlapSphere(point, range);
        foreach (Collider collider in colliders)
        {
            float dist = Vector3.Distance(point, collider.transform.position);
            float finalRangeDam = range / Mathf.Clamp(dist, 1f, 3f);
            playerHealth.TakeDamage(finalRangeDam);
        }
    }
    public void ApplyKnockBack(float force, float upModifier, float radius, float rangeDam)
    {
        Vector3 explosionPoint = transform.position;
        rb.AddExplosionForce(force, transform.position,
    radius, upModifier, ForceMode.Impulse);
    }

}
