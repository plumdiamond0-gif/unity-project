using UnityEngine;
using UnityEngine.AI;

public class GroundSensor : MonoBehaviour
{
    EnemyMovement enemyMovement;
    Rigidbody rb;
    NavMeshAgent agent;
    private void Start()
    {
        agent = transform.parent.GetComponent<NavMeshAgent>();
        rb= transform.parent.GetComponent<Rigidbody>();
        enemyMovement = transform.parent.GetComponent<EnemyMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            enemyMovement.isGrounded = true;
            if (enemyMovement.isKnockbacking)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                agent.enabled = true;
                rb.isKinematic = true;
                agent.nextPosition = transform.position;
                enemyMovement.currentState = EnemyState.Chase;
                enemyMovement.isKnockbacking = false;
            }

        }
    }
}
