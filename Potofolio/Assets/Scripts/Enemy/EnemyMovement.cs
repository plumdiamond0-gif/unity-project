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
    //GameObject Coin;
    GameObject player;
    PlayerMovement playerMovement;

    private NavMeshAgent agent;
    private Rigidbody rb;

    public bool isKnockbacking = false;

    public EnemyAttackType attackType;

    static readonly int MoveHash = Animator.StringToHash("Move");
    static readonly int AttackHash = Animator.StringToHash("Attack");
    static readonly int DieHash = Animator.StringToHash("Die");


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
        playerMovement = player.GetComponent<PlayerMovement>();
        agent.SetDestination(player.transform.position);

        data = GM.GetPrefabManager().EnemyPrefabTable.EnemyPrefabDatas.Find(x => x.enemyType == enemyType);
        //Coin = GM.GetPrefabManager().ItemPrefabTable.EnemyDropItems.Find(x => x.ItemName == "MonsterCore").ItemPrefab;

    }
    // Update is called once per frame
    void Update()
    {

        if (currentState == EnemyState.Knockback || currentState == EnemyState.Stun || currentState == EnemyState.Die)
            return;

        if (health.CurrentHp <= 0)
        {
            StartCoroutine(Die());
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
                    if (playerMovement!= null)
                    {
                        anim.SetTrigger(AttackHash);
                        playerMovement.TakeDamage(damage);
                        if(data.effects == null)
                            return;
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
                else if(attackType == EnemyAttackType.distant)
                {
                    anim.SetTrigger(AttackHash);
                    GameObject CBcopy = GM.GetEnemyBulletManager().GetBullet(data.enemyType, FirePos.position, Quaternion.identity);
                    EnemyBall ball = CBcopy.GetComponent<EnemyBall>();
                    ball.SetDamage(damage);
                    ball.SetEnemyData(data);
                    ball.SetPos(FirePos.position);
                    Rigidbody CanonBallRB = CBcopy.GetComponent<Rigidbody>();
                    if (CanonBallRB != null)
                    {
                        CanonBallRB.linearVelocity = Vector3.zero;
                        CanonBallRB.angularVelocity = Vector3.zero;
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
    IEnumerator Die()
    {
        currentState = EnemyState.Die;
        agent.enabled = false;
        anim.SetTrigger(DieHash);
        yield return new WaitForSeconds(2f);
            foreach (var item in data.dropItems.dropItems)
            {
                for (int i = 0; i < item.spawnNum; i++)
                {
                    Vector2 rand = Random.insideUnitCircle;
                    Vector3 spawnPos = transform.position + new Vector3(rand.x, 0, rand.y);
                    GameObject dropObject = GM.GetPrefabManager().
                        ItemPrefabTable.ItemDatas.
                        Find(x => x.outItemType == item.itemType).ItemPrefab;
                    GameObject spawnedObject = Instantiate(dropObject, spawnPos, Quaternion.identity);
                    spawnedObject.transform.localScale *= 0.3f;
                }
            }
            GameManager.instance.GetPlayer().GetComponent<PlayerItem>().GetExp(data.exp);

        Destroy(gameObject);
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
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        GameObject go = GM.GetEffectManager().PlayParticle(ParticleType.Slow, pos, Quaternion.identity, new Vector3(2, 2, 2), transform);
        Debug.Log("Slowed");
        float orispeed = agent.speed;
        agent.speed *= slowAmount;
        yield return new WaitForSeconds(slowTime);
        GM.GetEffectManager().StopParticle(go);
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
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        GameObject go = GM.GetEffectManager().PlayParticle(ParticleType.Stun, pos, Quaternion.identity, new Vector3(2, 2, 2), transform);
        Debug.Log("Slowed");
        agent.isStopped = true;
        yield return new WaitForSeconds(stunTime);
        GM.GetEffectManager().StopParticle(go);
        agent.isStopped = false;
        yield return null;
    }
    public void ApplyFire(float dotDamage, float dotNum)
    {
        if (dotdamRoutine != null)
            dotdamRoutine = null;
        StartCoroutine(Fire(dotDamage, dotNum));
    }
    IEnumerator Fire(float dotDamage, float dotTime)
    {
        GameObject go = GM.GetEffectManager().PlayParticle(ParticleType.Fire, transform.position, Quaternion.identity, Vector3.one, transform);
        for (int i = 0; i < dotTime; i++)
        {
            health.TakeDamage(dotDamage);
            yield return new WaitForSeconds(0.8f);
        }
        GM.GetEffectManager().StopParticle(go);
        yield return null;
    }
    public void ApplyToxic(float dotDamage, float dotNum)
    {
        if (dotdamRoutine != null)
            dotdamRoutine = null;
        StartCoroutine(Toxic(dotDamage, dotNum));
    }
    IEnumerator Toxic(float dotDamage, float dotTime)
    {
        GameObject go = GM.GetEffectManager().PlayParticle(ParticleType.Toxic, transform.position, Quaternion.identity, Vector3.one, transform);
        for (int i = 0; i < dotTime; i++)
        {
            health.TakeDamage(dotDamage);
            yield return new WaitForSeconds(0.8f);
        }
        GM.GetEffectManager().StopParticle(go);
        yield return null;
    }
    public void ApplyRangeDam(float damage)
    {
        health.TakeDamage(damage);
    }






}
