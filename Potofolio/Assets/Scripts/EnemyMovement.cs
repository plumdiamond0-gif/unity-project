using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Rendering.Universal;
using static Unity.Cinemachine.CinemachineTargetGroup;
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

public enum EnemyAttackType
{
    close,
    distant
}
public class EnemyMovement : MonoBehaviour, IWeaponEffectReceiver
{
    public EnemyState currentState;
    public EnemyType enemyType;

    //[SerializeField] private GameObject player;
    [SerializeField] private float detectRange;
    [SerializeField] private float attackRange;
    [SerializeField] float damage;
    [SerializeField] private float CoinNum;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackCoolTime;


    Health health;
    GameObject Coin;
    GameObject player;

    private NavMeshAgent agent;
    private Rigidbody rb;

    public bool isKnockbacking = false;

    public EnemyAttackType attackType;

    static readonly int MoveHash = Animator.StringToHash("Move");
    static readonly int AttackHash = Animator.StringToHash("Attack");
    static readonly int CanAttackHash = Animator.StringToHash("CanAttack");

    //bool isSlow;
    //bool isStun;
    //bool isdotdam;
    //bool isdot;

    Coroutine slowRoutine;
    Coroutine stunRoutine;
    Coroutine dotdamRoutine;

    public Transform FirePos;


    public bool isGrounded;
    bool canAttack;

    Animator anim;

    EnemyPrefabData data;
    
    void Awake()
    {
        canAttack = true;
        health = GetComponent<Health>();    
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; 
        anim = GetComponentInChildren<Animator>();

        anim.SetFloat(MoveHash, 0);

        player = transform.parent.GetComponent<Generator>().player;
        agent.SetDestination(player.transform.position);

        data = GM.GetPrefabManager().EnemyPrefabTable.EnemyPrefabDatas.Find(x => x.enemyType == enemyType);
        Coin = GM.GetPrefabManager().ItemPrefabTable.OutItemDatas.Find(x => x.ItemName == "MonsterCore").ItemPrefab;

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
        if (player == null)
            return;
        float dist = Vector3.Distance(transform.position, player.transform.position);

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
                agent.SetDestination(player.transform.position);
                anim.SetFloat(MoveHash, 1);
                break;
            case EnemyState.Attack:
                if (!canAttack)
                    return;
                canAttack = false;
                StartCoroutine(AttackCoolTime());
                if (attackType == EnemyAttackType.close)
                {
                    Health playerhealth = player.GetComponent<Health>();
                    if (playerhealth != null)
                    {
                        anim.SetTrigger(AttackHash);
                        playerhealth.TakeDamage(damage);
                        foreach (var effectobjs in data.effects)
                        {
                            if (effectobjs is IWeaponEffect effect)
                            {
                                effect.Apply(player, 1);
                            }
                        }


                    }
                    agent.ResetPath();
                }
                else
                {
                    anim.SetTrigger(AttackHash);

                    GameObject CBcopy = Instantiate(data.EnemyBullet, FirePos.position, Quaternion.identity);
                    EnemyBall ball = CBcopy.GetComponent<EnemyBall>();
                    ball.SetDamage(damage);
                    Rigidbody CanonBallRB = CBcopy.GetComponent<Rigidbody>();
                    if (CanonBallRB != null)
                    {
                        Vector3 shootDir = (player.transform.position - FirePos.position).normalized;
                        CanonBallRB.AddForce(
                            shootDir * attackSpeed,
                            ForceMode.Impulse
                        );

                    }
                    agent.ResetPath();

                }
                break;

            default:
                break;
        }

    }
    IEnumerator AttackCoolTime()
    {
        yield return new WaitForSeconds(attackCoolTime);
        canAttack = true;
    }
    private void ChangeState(EnemyState newState)
    {
        if (currentState == newState)
        { return; }
        currentState = newState;

    }
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
    public void ApplySlow(float slowTime, float slowAmount)
    {
        if(stunRoutine != null)
            return;
        if(slowRoutine != null)
        {
            slowRoutine = null;
        }
        slowRoutine = StartCoroutine(Slow(slowTime, slowAmount));

    }
    IEnumerator Slow(float slowTime, float slowAmount)
    {
        Debug.Log("Slowed");
        float orispeed = agent.speed;
        agent.speed *= slowAmount;
        yield return new WaitForSeconds(slowTime);
        agent.speed = orispeed;
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
        agent.isStopped = true;
        yield return new WaitForSeconds(stunTime);
        agent.isStopped = false;
        yield return null;
    }
    public void ApplyDotDam(float dotDamage, float dotNum)
    {
        if(dotdamRoutine != null)   
            dotdamRoutine = null;
        StartCoroutine(Dotdam(dotDamage, dotNum));

    }
    IEnumerator Dotdam(float dotDamage, float dotTime)
    {
        Debug.Log("Slowed");
        for (int i = 0; i < dotTime; i++)
        {
            
            TakeDamage(dotDamage);
            Debug.Log(dotDamage);
            yield return new WaitForSeconds(0.8f);
            
        }
        yield return null;
    }

  



}
