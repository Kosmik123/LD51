using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private Transform playerTransform;

    public LayerMask whatIsGround, whatIsPlayer;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //float distance = Vector3.Distance(playerTransform.position, transform.position);

        agent.SetDestination(playerTransform.position);
    }


}
