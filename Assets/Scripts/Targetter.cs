using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetter : MonoBehaviour
{

    private Transform currentTarget;

    [Header("Stats")]

    // range should maybe come from some sort of stats keeper
    [SerializeField]
    private float range = 10f;


    [Header("Unity Setup Fields")]

    [SerializeField]
    private string enemyTag = "Enemy";

    [SerializeField]
    private float searchRate = 0.1f;

    [SerializeField]
    private Transform partToRotate;
    [SerializeField]
    private float turnSpeed = 10f;

    void Start()
    {
        InvokeRepeating("SearchForTarget", 0f, searchRate);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTarget == null) 
            return;

        UpdateRotation();
    }

    private void SearchForTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (var enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance) 
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range) 
        {
            currentTarget = nearestEnemy.transform;
        } 
        else
        {
            currentTarget = null;
        }
    }

    private void UpdateRotation() 
    {
        var dir = currentTarget.position - transform.position;
        var lookRotation = Quaternion.LookRotation(dir);
        var rotation = (Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed)).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        if(currentTarget != null) 
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, currentTarget.position);
        }
    }

}
