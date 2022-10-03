using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float lookRadius = 10f;
    [SerializeField] private float enemyAngle = 90;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float backwardSpeed;

    private NavMeshAgent agent;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        ChasePlayer();
        CalculateAngleTowardsPlayer();
    }

    private void ChasePlayer()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(playerTransform.position);
        }

        if (CalculateAngleTowardsPlayer() < enemyAngle)
        {
            agent.speed = forwardSpeed;
        }
        else
        {
            agent.speed = backwardSpeed;
        }
    }
    private float CalculateAngleTowardsPlayer()
    {
        Vector3 VectorForward = transform.forward;
        Vector3 relativePosition = playerTransform.position - transform.position;
        float angleValue = Vector3.Angle(VectorForward, relativePosition);
        Debug.Log(angleValue);
        return angleValue;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
