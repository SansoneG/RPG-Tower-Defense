using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    protected float speed = 10f;

    protected Transform target;

    protected float damage;

    public virtual void Shoot(Transform target, float damage)
    {
        this.target = target;
        this.damage = damage;

        // this should somehow correspond to the range
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider collider)
    {
        var go = collider.gameObject;

        if(go.CompareTag("Enemy"))
        {
            //Debug.Log("Hit an Enemy");
            HitTarget(go);
        }
        else
        {
            //Debug.Log("Hit something else");
            // For now just do nothing
        }
    }

    protected virtual void HitTarget(GameObject go)
    {
        Destroy(gameObject);
    }

}
