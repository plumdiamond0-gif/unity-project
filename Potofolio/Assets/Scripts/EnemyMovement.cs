using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Rendering.Universal;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public enum EnemyState
{
    Idle,
    Chase,
    Attack,
    Knockback,
    Stun,
    Die
}
public class EnemyMovement : MonoBehaviour
{
    public EnemyState currentState;

    [SerializeField] private GameObject player;
    Transform playerTrans;
    [SerializeField] private float detectRange = 20f;
    [SerializeField] private float attackRange = 5f;
    Health health;
    [SerializeField] private GameObject Coin;
    [SerializeField] private float CoinNum;


    private NavMeshAgent agent;
    private Rigidbody rb;

    private bool isKnockbacking = false;

    public float damage;

    static readonly int MoveHash = Animator.StringToHash("Move");
    static readonly int AttackHash = Animator.StringToHash("Attack");
    static readonly int CanAttackHash = Animator.StringToHash("CanAttack");

    bool isSlow;
    bool isStun;
    bool isdotdam;
    bool isdot;



    bool isGrounded;

    Animator anim;

    void Awake()
    {
        health = GetComponent<Health>();    
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        playerTrans = player.transform;
        rb.isKinematic = true; 
        anim = GetComponentInChildren<Animator>();

        anim.SetFloat(MoveHash, 0);

        agent.isStopped = true;
    }


    // Update is called once per frame
    void Update()
    {

        if (currentState == EnemyState.Knockback || currentState == EnemyState.Stun || currentState == EnemyState.Die)
            return;

        if (health.CurrentHp <= 0)
        {
            Die();
            return;
        }

        float dist = Vector3.Distance(transform.position, playerTrans.position);

        if (dist > detectRange)
            ChangeState(EnemyState.Idle);
        else if (dist > attackRange)
            ChangeState(EnemyState.Chase);
        else
            ChangeState(EnemyState.Attack);

        HandleState();



        /*if (agent != null && player != null)
        //{
        //    agent.SetDestination(player.position);
        //}
        매 프레임마다 계속 플레이어 위치 목적지로 삼아 코드가 매우 무거워짐*/

    }

    private void HandleState()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                if (isKnockbacking)
                    return;
                agent.ResetPath();
                anim.SetFloat(MoveHash, 0);
                break;
            case EnemyState.Chase:
                agent.SetDestination(playerTrans.position);
                anim.SetFloat(MoveHash, 1);
                break;
            case EnemyState.Attack:
                Health playerhealth = player.GetComponent<Health>();
                if (playerhealth != null)
                {
                    anim.SetTrigger(AttackHash);
                    playerhealth.TakeHealth(-damage);
                }
                agent.ResetPath();
                break;

            default:
                break;
        }

    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState == newState)
        { return; }
        currentState = newState;

    }

    public void TakeDamage(float Getdamage)
    {
        health.CurrentHp -= Getdamage;
    }

    public void ApplyKnockBack()
    {
        if (isKnockbacking)
            return;
        isKnockbacking = true;
        currentState = EnemyState.Knockback;
        agent.enabled = false;
        rb.isKinematic = false;
    }


    /* IEnumerator KnockbackRoutine(float time)
     {
         isGrounded = false;
         isKnockbacking = true;
         currentState = EnemyState.Knockback;
         agent.enabled = false;
         rb.isKinematic = false;

         yield return new WaitForSeconds(time);
         rb.linearVelocity = Vector3.zero;
         rb.angularVelocity = Vector3.zero;
         rb.isKinematic = true;




         NavMeshHit hit;
         if (NavMesh.SamplePosition(transform.position, out hit, 2000f, NavMesh.AllAreas))
         {
             agent.Warp(hit.position);
         }



     }*/
    /*private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerhealth = other.GetComponent<Health>();
            if (playerhealth != null)
            {
                anim.SetTrigger(AttackHash);
                Debug.Log("wfe");
                playerhealth.TakeHealth(-damage);
            }
        }
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
            isGrounded = true;
            if(isKnockbacking)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                agent.enabled = true;
                rb.isKinematic = true;
                agent.nextPosition = transform.position;
                currentState = EnemyState.Chase;
                isKnockbacking = false;
            }

        }
    }

    public void Die()
    {
        currentState = EnemyState.Die;
        float distance = 1.2f;
      
        for (int i = 0; i < CoinNum; i++)
        {
            Vector2 rand = Random.insideUnitCircle.normalized; // 랜덤 방향
            Vector3 offset = new Vector3(rand.x, 0, rand.y) * distance;
            Vector3 spawnPos = transform.position + offset;
            Instantiate(Coin, spawnPos, Quaternion.identity);
        }
        Destroy(gameObject); 

    }

    public void beSlow(float slowTime)
    {
        if(isSlow)
        {
            return;
        }
        StartCoroutine(Slow(slowTime));

    }

    IEnumerator Slow(float slowTime)
    {
        isSlow = true;
        Debug.Log("Slowed");
        float orispeed = agent.speed;
        agent.speed = 1;
        yield return new WaitForSeconds(slowTime);
        agent.speed = orispeed;
        isSlow = false;
        yield return null;
    }

    public void beStun(float stunTime)
    {
        if (isStun)
        {
            return;
        }
        StartCoroutine(Slow(stunTime));

    }

    IEnumerator Stun(float stunTime)
    {
        isStun = true;
        Debug.Log("Slowed");
        agent.isStopped = true;
        yield return new WaitForSeconds(stunTime);
        isStun = false;
        agent.isStopped = false;
        yield return null;
    }

    public void dotdam(float dotDamage, float dotTime)
    {
        if (isdotdam)
        {
            return;
        }
        StartCoroutine(_dotdma(dotDamage, dotTime));

    }

    IEnumerator _dotdma(float dotDamage, float dotTime)
    {
        isdotdam = true;
        Debug.Log("Slowed");
        for (int i = 0; i < dotTime; i++)
        {
            
            TakeDamage(dotDamage);
            Debug.Log(dotDamage);
            yield return new WaitForSeconds(0.8f);
            
        }
        isdotdam = false;
        yield return null;
    }

  



}
