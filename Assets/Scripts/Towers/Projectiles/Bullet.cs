using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{

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

    protected override void DamageEnemy(GameObject go)
    {
        var enemy = go.GetComponent<Enemy>();
        
        if(enemy != null)
            enemy.Damage(damage);

        Destroy(gameObject);
    }

}
