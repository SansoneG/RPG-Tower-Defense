using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For now this is a dummy enemy script, to test some tower functionality
public class Enemy : MonoBehaviour
{
    
    public float health = 100;

    public void TakeDamage(float damage)
    {
        this.health -= damage;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
