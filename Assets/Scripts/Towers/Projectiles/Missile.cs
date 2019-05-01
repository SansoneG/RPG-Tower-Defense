using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{

    [SerializeField]
    private float radius = 2f;

    // Update is called once per frame
    void Update()
    {
        if(target != null) 
        {
            var dir = target.position - transform.position;
            dir.y = 0;
            dir.Normalize();

            var lookTarget = target.position;
            lookTarget.y = transform.position.y;
            transform.LookAt(lookTarget);

            transform.Translate(dir * speed * Time.deltaTime, Space.World);
        }
        else 
        {
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        }
    }

    public override void Shoot(Transform target, float damage)
    {
        base.Shoot(target, damage);
    }

    protected override void DamageEnemy(GameObject go)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var collider in colliders)
        {
            var enemy = collider.gameObject.GetComponent<Enemy>();
        
            if(enemy != null)
                enemy.Damage(damage);
        }

        Destroy(gameObject);
    }

}
