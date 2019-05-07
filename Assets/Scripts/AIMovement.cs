using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    
    [SerializeField]
    private float attackRange = 1f;

    [SerializeField]
    private Transform destination;
    private Collider destCollider;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.position);
        destCollider = destination.gameObject.GetComponentInChildren<Collider>();
    }

    void Update()
    {
        var distance = Vector3.Distance(destCollider.ClosestPointOnBounds(transform.position), transform.position);

        if(distance <= attackRange)
        {
            Debug.Log("!");
            agent.isStopped = true;
        }
    }

}
