using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    
    [SerializeField]
    private float visionRange = 5f;

    [SerializeField]
    private float attackDamage = 10f;

    [SerializeField]
    private float attackRange = 1f;

    [SerializeField]
    private float attackSpeed = 1f;
    private float attackCooldown;

    [SerializeField]
    private Transform destination;

    [SerializeField]
    private float searchRate = 0.1f;

    private Transform target;
    private Collider targetCollider;

    private NavMeshAgent agent;

    void Start()
    {
        InvokeRepeating("SearchForTarget", 0f, searchRate);
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.position);
    }

    void Update()
    {
        if(attackCooldown > 0)
            attackCooldown -= Time.deltaTime;

        if(target != null)
        {
            var closestPosition = targetCollider.ClosestPointOnBounds(transform.position);
            agent.SetDestination(closestPosition);

            var distance = Vector3.Distance(closestPosition, transform.position);

            if(distance <= attackRange)
            {
                agent.isStopped = true;
                FaceTarget(closestPosition);
                if(attackCooldown <= 0)
                {
                    Debug.Log("Attack");
                    attackCooldown = 1f / attackSpeed;
                    Attack();
                }
            }
            else
            {
                agent.isStopped = false;
            }
        }
        else
        {
            agent.SetDestination(destination.position);
            agent.isStopped = false;
        }        
    }

    private void Attack()
    {
        var unit = target.GetComponent<Unit>();

        if(unit == null)
        {
            Debug.Log("Why does " + gameObject.name + " has no Building COomponent");
            return;
        }
        unit.TakeDamage(attackDamage);
    }

    private void FaceTarget(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void SearchForTarget()
    {
        var defenders = Unit.Defenders;

        float shortestDistance = Mathf.Infinity;
        Unit nearestBuilding = null;
        foreach (var defender in defenders)
        {
            float distanceToBuilding = Vector3.Distance(transform.position, defender.transform.position);
            if(distanceToBuilding < shortestDistance) 
            {
                shortestDistance = distanceToBuilding;
                nearestBuilding = defender;
            }
        }

        if(nearestBuilding != null && shortestDistance <= visionRange) 
        {
            target = nearestBuilding.transform;
            targetCollider = target.gameObject.GetComponentInChildren<Collider>();
        } 
        else
        {
            target = null;
        }
    }

}
