using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    
    [SerializeField]
    protected float maxHealth = 100f;

    protected float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        CheckDeath();
    }

    private void CheckDeath()
    {
        if(currentHealth <= 0)
        {
            OnDestroy();
        }
    }

    public virtual void OnDestroy()
    {
        Destroy(gameObject);
    }

}
