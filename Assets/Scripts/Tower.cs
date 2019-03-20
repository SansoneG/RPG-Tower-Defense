using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField]
    private float range = 10f;

    [SerializeField]
    private string enemyTag = "Enemy";

    [SerializeField]
    private Transform partToRotate;
    [SerializeField]
    private float turnSpeed = 10f;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null) 
            return;

        UpdateRotation();        
    }

    private void UpdateTarget()
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
            target = nearestEnemy.transform;
        } 
        else
        {
            target = null;
        }
    }

    private void UpdateRotation() 
    {
        var dir = target.position - transform.position;
        var lookRotation = Quaternion.LookRotation(dir);
        var rotation = (Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed)).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        if(target != null) 
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }

}
