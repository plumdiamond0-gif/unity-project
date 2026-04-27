using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Utilities;
using static UnityEngine.GraphicsBuffer;

public enum EnemyState
{
    Idle,
    Chase,
    Attack,
    Knockback,
    Stun
}
public class EnemyMovement : MonoBehaviour
{
    public EnemyState currentState;

    public GameObject player;
    Transform playerTrans;
    public float detectRange = 10f;
    public float attackRange = 2f;

    private NavMeshAgent agent;
    private Rigidbody rb;

    private bool isKnockbacking = false;

    public float damage;
    static readonly int MoveHash = Animator.StringToHash("Move");
    static readonly int AttackHash = Animator.StringToHash("Attack");
    static readonly int CanAttackHash = Animator.StringToHash("CanAttack");


    bool isGrounded;

    Animator anim;

    void Awake()
    {

        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        playerTrans = player.transform;
        rb.isKinematic = true; // 평소엔 물리 OFF
        anim = GetComponentInChildren<Animator>();

        anim.SetFloat(MoveHash, 0);

        agent.isStopped = true;
    }


    // Update is called once per frame
    void Update()
    {

     
        if (currentState == EnemyState.Knockback || currentState == EnemyState.Stun)
            return;

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
        if(agent.enabled == false)
            { return; }
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

    }

    public void ApplyKnockBack(float knockbackTime)
    {
        if (isKnockbacking)
            return;
        StartCoroutine(KnockbackRoutine(knockbackTime));
    }
    IEnumerator KnockbackRoutine(float time)
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
        //yield return null;
        if(isGrounded == true)
        {
            Debug.Log("바각");
            agent.enabled = true;
            agent.nextPosition = transform.position;
            currentState = EnemyState.Chase;
            isKnockbacking = false;
        }






    }
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
        }
 
    }


}
