using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{

    [Header("Stats")]

    // fireRate should maybe come from some sort of stats keeper
    [SerializeField]
    private float fireRate = 1f;
    private float fireCountdown;

    // make this a prjectile, which needs already some stuff/methods
    [SerializeField]
    private GameObject projectilePrefab;

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
            fireCountdown = 1f / fireRate;
        }

    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // this is later a base projectile class?
        Bullet bullet = projectile.GetComponent<Bullet>();
        var direction = (targetter.CurrentTarget.position - firePoint.position).normalized;
        direction.y = 0;
        bullet.Shoot(direction);
    }

}
