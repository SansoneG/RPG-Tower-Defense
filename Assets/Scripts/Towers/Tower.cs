using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Building
{
  
    // Make this some kind of central statc thing maybe
    // Maybe a scribtableobject, or just a simple class
    public TowerLevel towers;

    private TowerLevel currentTowerLevel;

    private TowerInfo currentTower;

    private TowerSpace towerSpace;

    void Awake()
    {
        SetTower(towers);
    }

    // Dont really like, that the tower needs to know its tower space
    public void SetTowerSpace(TowerSpace towerSpace)
    {
        this.towerSpace = towerSpace;
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

    public List<TowerLevel> GetNextUpgrades()
    {
        return currentTowerLevel.upgradesTo;
    }

    public override void OnDestroy()
    {
        Debug.Log("Something destroyed this tower");
        towerSpace.OnTowerDestroyed();
        Destroy(gameObject);
    }

}

[System.Serializable]
public class TowerLevel 
{

    public TowerInfo towerPrefab;
    public List<TowerLevel> upgradesTo;

}
