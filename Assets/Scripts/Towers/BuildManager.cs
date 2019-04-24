using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{

    private TowerSpace selectedTowerSpace;

    [SerializeField]
    private BuildUI buildUI;

    void Update()
    {

        if(EventSystem.current.IsPointerOverGameObject())
            return;

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            if(Physics.Raycast(ray, out hitInfo))
            {
                GameObject go = hitInfo.transform.gameObject;

                while(true)
                {
                    var ts = go.GetComponent<TowerSpace>();

                    if(ts != null)
                    {
                        //Debug.Log("We hit a Tower Space");
                        SelectTowerSpace(ts);
                        break;
                    }
                    else if(go.transform.parent == null)
                    {
                        //Debug.Log("We hit something else");
                        Deselect();
                        break;
                    }
                    
                    go = go.transform.parent.gameObject;

                }
            }
        }
    }

    private void SelectTowerSpace(TowerSpace towerSpace)
    {
        selectedTowerSpace = towerSpace;

        if(selectedTowerSpace.CurrentTower == null)
        {
            buildUI.Select(selectedTowerSpace);
        }
    }

    private void Deselect()
    {
        selectedTowerSpace = null;

        buildUI.Deselect();
    }

    public void BuildTower()
    {
        if(selectedTowerSpace.CurrentTower != null)
            return;

        selectedTowerSpace.BuildTower();

        Deselect();
    }

}
