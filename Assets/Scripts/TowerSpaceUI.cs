using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpaceUI : MonoBehaviour
{

    private Camera mainCamera;

    private TowerSpace towerSpace;

    [SerializeField]
    private Transform[] rotationPoints;

    [SerializeField]
    private GameObject buildUI;

    [SerializeField]
    private GameObject upgradeUI;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        foreach(var rotationPoint in rotationPoints)
        {
            rotationPoint.LookAt(rotationPoint.position + mainCamera.transform.rotation * Vector3.forward, 
                    mainCamera.transform.rotation * Vector3.up);
        }
    }

    public void Select(TowerSpace ts)
    {
        towerSpace = ts;
        transform.position = ts.towerPlacePosition.position;
        gameObject.SetActive(true);

        UpdateUI();
    }

    public void Deselect()
    {
        gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        if(towerSpace.CurrentTower == null)
        {
            buildUI.SetActive(true);
            upgradeUI.SetActive(false);
        }
        else
        {
            buildUI.SetActive(false);
            upgradeUI.SetActive(true);
        }
    }

    public void BuildTower()
    {
        if(towerSpace.CurrentTower != null)
            return;

        towerSpace.BuildTower();
        
        UpdateUI();
    }

    public void SellTower()
    {
        towerSpace.SellTower();

        UpdateUI();
    }

}
