using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInfo : MonoBehaviour
{
    
    // Maybe this class should have all the ifo about the tower and just give it to the components

    [SerializeField]
    private string name = "Tower";

    private Targetter targetter;

    private ProjectileLauncher[] launchers;

    public string GetName()
    {
        return name;
    }

    public float GetRange()
    {
        if(targetter == null)
            targetter = GetComponentInChildren<Targetter>();

        return targetter.GetRange();
    }

    public float GetDamagePerSecond()
    {
        // For now just get the projectile launcher, cause there arent any others
        launchers = GetComponentsInChildren<ProjectileLauncher>();

        var dmgPerSec = 0f;

        foreach (var launcher in launchers)
        {
            Debug.Log("!");
            dmgPerSec += launcher.GetAttacksPerSecond() * launcher.GetDamage();
        }

        return dmgPerSec;
    }

}
