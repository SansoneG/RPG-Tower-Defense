using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    [SerializeField]
    private Team team;
    public Team Team => team;

    [SerializeField]
    protected float maxHealth = 100f;
    protected float currentHealth;


    private static List<Unit> defenders = new List<Unit>();
    private static List<Unit> monster = new List<Unit>();
    public static List<Unit> Defenders => defenders;
    public static List<Unit> Monster => monster;

    protected void Awake()
    {
        currentHealth = maxHealth;

        if(team == Team.Defender)
        {
            defenders.Add(this);
        }
        else
        {
            monster.Add(this);
        }
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
        if(team == Team.Defender)
        {
            defenders.Remove(this);
        }
        else
        {
            monster.Remove(this);
        }
        Destroy(gameObject);
    }


}

public enum Team
{
    Defender,
    Monster
}
