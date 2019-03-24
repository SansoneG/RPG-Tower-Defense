using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Transform target;

    public virtual void Shoot(Transform target)
    {
        this.target = target;
    }

}
