using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private Transform playerTransform;

    //public LayerMask whatIsGround, whatIsPlayer;

    private NavMeshAgent agent;

    private float lookRadius = 10f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(playerTransform.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
