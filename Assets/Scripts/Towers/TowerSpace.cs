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

    public void BuildTower() 
    {
        if(currentTower != null)
        {
            Debug.Log("There is already a tower");
            return;
        }
        currentTower = Instantiate(towerPrefab, towerPlacePosition.position, Quaternion.identity);
        currentTower.transform.SetParent(transform);
        Debug.Log("Tower built");
    }

}
