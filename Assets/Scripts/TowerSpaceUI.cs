using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private GameObject upgradeButtonsRoot;

    [SerializeField]
    private GameObject upgradeButton;

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
            CreateUpgradeButtons();
            buildUI.SetActive(false);
            upgradeUI.SetActive(true);
        }
    }

    private void CreateUpgradeButtons()
    {
        var children = new List<GameObject>();
        foreach (Transform child in upgradeButtonsRoot.transform)
        {
            children.Add(child.gameObject);
        }
        foreach (var child in children)
        {
            Destroy(child);
        }

        var nextUpgrades = towerSpace.CurrentTower.GetNextUpgrades();
        int i = 0;
        foreach (var towerUpgrade in nextUpgrades)
        {
            var button = CreateButton(towerUpgrade);
            button.transform.SetParent(upgradeButtonsRoot.transform, false);
            int index = i;
            button.GetComponent<Button>().onClick.AddListener( () => UpgradeTower(index) );
            i++;
        }
    }

    private GameObject CreateButton(TowerLevel towerLevel)
    {
        GameObject button = Instantiate(upgradeButton);
        return button;
    }

    public void BuildTower()
    {
        if(towerSpace.CurrentTower != null)
            return;

        var success = towerSpace.BuildTower();

        if(success)
            Debug.Log("Tower built");
        else
            Debug.Log("There is already a Tower here");
        
        UpdateUI();
    }

    public void SellTower()
    {
        var success = towerSpace.SellTower();

        if(success)
            Debug.Log("Tower sold");
        else
            Debug.Log("There is no Tower here");

        UpdateUI();
    }

    public void UpgradeTower(int index)
    {
        var success = towerSpace.CurrentTower.UpgradeTower(index);

        if(success)
            Debug.Log("Tower upgraded");
        else
            Debug.Log("Couldn't upgrade Tower");

        UpdateUI();
    }

}
