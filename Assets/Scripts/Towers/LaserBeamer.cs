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

    [SerializeField]
    private ParticleSystem impactEffect;

    [SerializeField]
    private Light impactLight;

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
            {
                lineRenderer.enabled = false;
                impactEffect.Stop();
                impactLight.enabled = false;
            }
            return;
        }

        ShowLaser();        

        DamageEnemy();
    }

    private void ShowLaser()
    {
        var targetPos = targetter.CurrentTarget.position;
        targetPos.y = laserPosition.position.y;
        lineRenderer.SetPosition(0, laserPosition.position);
        lineRenderer.SetPosition(1, targetPos);

        Vector3 dir = targetPos - laserPosition.position;
        impactEffect.transform.rotation = Quaternion.LookRotation(-dir);

        impactEffect.transform.position = targetPos;

        if(!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
    }

    private void DamageEnemy()
    {
        var enemy = targetter.CurrentTarget.GetComponent<Unit>();

        if(enemy == null)
            return;

        enemy.TakeDamage(damagePerSecond * Time.deltaTime);
    }

    public float GetDamagePerSecond()
    {
        return damagePerSecond;
    }

}
