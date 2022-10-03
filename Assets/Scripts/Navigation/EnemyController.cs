using NaughtyAttributes;
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
    [SerializeField] private Battler battler;
    [SerializeField] private Animator animator;

    public Timer timer;

    public Transform container;

    public float restTime;
    [SerializeField, ReadOnly]
    private float restState;


    [SerializeField, ReadOnly]
    private float currentSpeed;

    private NavMeshAgent agent;

    private void Awake()
    {
        battler.OnDied += Die;
        timer.Interval = Random.Range(10, 20);
    }

    public void Die()
    {
        container.gameObject.SetActive(false);
        agent.enabled = false;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        restState += Time.deltaTime;
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if (distance <= 2 * battler.AttackRange)
        {
            Attack();
        }
        else if(distance <= lookRadius)
        {
            ChasePlayer();
        }

        CalculateAngleTowardsPlayer();

        currentSpeed = agent.velocity.magnitude;
        animator.SetFloat("Speed", currentSpeed);
    }

    private void Attack()
    {
        if (restState > restTime)
        {
            restState = 0;
            battler.DoAttack();
        }
    }

    private void ChasePlayer()
    {
        if (agent.isActiveAndEnabled)
                agent.SetDestination(playerTransform.position);

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
        return angleValue;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void Respawn()
    {
        container.gameObject.SetActive(true);
        agent.enabled = true;
        battler.Health.FullRestore();
    }    

}
