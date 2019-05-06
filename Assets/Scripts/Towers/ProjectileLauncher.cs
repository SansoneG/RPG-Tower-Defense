using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour, Weapon
{

    [Header("Stats")]

    // fireRate should maybe come from some sort of stats keeper
    [SerializeField]
    private float attacksPerSecond = 1f;
    private float fireCountdown;

    [SerializeField]
    private Projectile projectilePrefab;

    [SerializeField]
    private float damage = 10;

    [Header("Unity Setup")]

    [SerializeField]
    private Targetter targetter;

    [SerializeField]
    private Transform firePoint;

    void Update()
    {
        fireCountdown -= Time.deltaTime;

        if(targetter.CurrentTarget == null)
            return;

        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / attacksPerSecond;
        }

    }

    private void Shoot()
    {
        Projectile projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.Shoot(targetter.CurrentTarget, damage);
    }

    public float GetDamagePerSecond()
    {
        return damage * attacksPerSecond;
    }

}
