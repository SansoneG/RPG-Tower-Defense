using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInfo : MonoBehaviour
{
    
    // Maybe this class should have all the ifo about the tower and just give it to the components

    [SerializeField]
    private string name = "Tower";

    [SerializeField]
    private float health = 100;

    private Targetter targetter;

    private Weapon[] weapons;

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
        weapons = GetComponentsInChildren<Weapon>();

        var dmgPerSec = 0f;

        foreach (var weapon in weapons)
        {
            dmgPerSec += weapon.GetDamagePerSecond();
        }

        return dmgPerSec;
    }

}
