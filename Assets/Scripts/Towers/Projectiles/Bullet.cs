using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{

    [SerializeField]
    private GameObject impactEffect;

    private Vector3 direction;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public override void Shoot(Transform target, float damage)
    {
        base.Shoot(target, damage);

        var dir = target.position - transform.position;
        dir.y = 0;
        this.direction = dir.normalized;
    }

    protected override void HitTarget(Unit unit)
    {        
        unit.TakeDamage(damage);

        var effect = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 2);

        Destroy(gameObject);
    }

}
