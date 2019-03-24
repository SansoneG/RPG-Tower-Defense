using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    
    [Header("Stats")]

    [SerializeField]
    private float speed = 10f;


    private Vector3 direction;

    public override void Shoot(Transform target)
    {
        base.Shoot(target);

        var dir = target.position - transform.position;
        dir.y = 0;
        this.direction = dir.normalized;

        Destroy(gameObject, 2f);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

}
