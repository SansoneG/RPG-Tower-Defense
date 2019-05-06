using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{

    [SerializeField]
    private GameObject impactEffect;
    
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

    protected override void HitTarget(GameObject go)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var collider in colliders)
        {
            var enemy = collider.gameObject.GetComponent<Enemy>();
        
            if(enemy != null)
                enemy.TakeDamage(damage);
        }

        var effect = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 2);

        Destroy(gameObject);
    }

}
