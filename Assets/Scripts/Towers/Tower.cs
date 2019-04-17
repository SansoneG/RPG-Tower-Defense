﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
  
    // Make this some kind of central statc thing maybe
    // Maybe a scribtableobject, or just a simple class
    public TowerLevel towers;

    private TowerLevel currentTowerLevel;

    private GameObject currentTower;

    void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        SetTower(towers);
    }

    public bool UpgradeTower(int nextTowerIndex)
    {
        if(nextTowerIndex < currentTowerLevel.upgradesTo.Count)
        {
            var newTower = currentTowerLevel.upgradesTo[nextTowerIndex];
            SetTower(newTower);
            return true;
        }
        return false;
    }

    public void SellTower()
    {
        Destroy(gameObject);
    }

    private void SetTower(TowerLevel tower)
    {
        currentTowerLevel = tower;

        if(currentTowerLevel != null)
            Destroy(currentTower);

        currentTower = Instantiate(currentTowerLevel.towerPrefab, transform.position, transform.rotation);
        currentTower.transform.SetParent(transform);
    }

}

[System.Serializable]
public class TowerLevel 
{

    public GameObject towerPrefab;
    public List<TowerLevel> upgradesTo;

}
