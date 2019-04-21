using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpace : MonoBehaviour
{

    [SerializeField]
    private Transform towerPlacePosition;

    [SerializeField]
    private Tower towerPrefab;

    private Tower currentTower;

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
