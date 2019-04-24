using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpaceUI : MonoBehaviour
{

    private Camera mainCamera;

    private TowerSpace towerSpace;

    [SerializeField]
    private Transform rotationPoint;

    [SerializeField]
    private GameObject buildUI;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        rotationPoint.LookAt(rotationPoint.position + mainCamera.transform.rotation * Vector3.forward, 
                    mainCamera.transform.rotation * Vector3.up);
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

    public void BuildTower()
    {
        if(towerSpace.CurrentTower != null)
            return;

        towerSpace.BuildTower();
        
        UpdateUI();
    }

    private void UpdateUI()
    {
        if(towerSpace.CurrentTower == null)
        {
            buildUI.SetActive(true);
        }
        else
        {
            buildUI.SetActive(false);
        }
    }

}
