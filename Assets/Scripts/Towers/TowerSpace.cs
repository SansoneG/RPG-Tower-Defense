using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpace : MonoBehaviour
{

    [SerializeField]
    public Transform towerPlacePosition;

    [SerializeField]
    private Tower towerPrefab;

    private Tower currentTower;
    public Tower CurrentTower
    {
        get
        {
            return currentTower;
        }
        private set
        {
            currentTower = value;
        }
    }

    public bool BuildTower() 
    {
        if(currentTower != null)
        {
            return false;
        }
        currentTower = Instantiate(towerPrefab, towerPlacePosition.position, Quaternion.identity);
        currentTower.transform.SetParent(transform);
        currentTower.SetTowerSpace(this);
        return true;
    }

    public bool SellTower()
    {
        if(currentTower == null)
            return false;

        currentTower.SellTower();

        currentTower = null;
        return true;
    }

    public void OnTowerDestroyed()
    {
        currentTower = null;
    }

}
