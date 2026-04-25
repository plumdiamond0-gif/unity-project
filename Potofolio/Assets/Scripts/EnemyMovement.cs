using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Utilities;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float detectRange = 26f;
    public float damage;
    static readonly int MoveHash = Animator.StringToHash("Move");
    static readonly int AttackHash = Animator.StringToHash("Attack");
    static readonly int CanAttackHash = Animator.StringToHash("CanAttack");




    Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        anim.SetFloat(MoveHash, 0);
        agent = GetComponent<NavMeshAgent>();

        agent.isStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent != null && player != null)
        {
            agent.SetDestination(player.position);
        }


        if (Vector3.Distance(transform.position, player.position) <= detectRange)
        {
            agent.isStopped = false;
            anim.SetFloat(MoveHash, 1f);
        }
        
    }

    public void TakeDamage(float Getdamage)
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerhealth = other.GetComponent<Health>();
            if (playerhealth != null)
            {
                anim.SetTrigger(AttackHash);
                playerhealth.TakeHealth(-damage);
            }
        }
    }




}
