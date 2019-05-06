using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserBeamer : MonoBehaviour, Weapon
{
    
    [SerializeField]
    private float damagePerSecond = 2f;

    [SerializeField]
    private Transform laserPosition;

    [SerializeField]
    private Targetter targetter;

    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if(targetter.CurrentTarget == null)
        {
            if(lineRenderer.enabled)
                lineRenderer.enabled = false;
            return;
        }

        var targetPos = targetter.CurrentTarget.position;
        targetPos.y = laserPosition.position.y;
        lineRenderer.SetPosition(0, laserPosition.position);
        lineRenderer.SetPosition(1, targetPos);

        if(!lineRenderer.enabled)
            lineRenderer.enabled = true;

        DamageEnemy();
    }

    private void DamageEnemy()
    {
        var enemy = targetter.CurrentTarget.GetComponent<Enemy>();

        if(enemy == null)
            return;

        enemy.TakeDamage(damagePerSecond * Time.deltaTime);
    }

    public float GetDamagePerSecond()
    {
        return damagePerSecond;
    }

}
