using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUI : TowerUI
{

    public void Select(TowerSpace ts)
    {
        transform.position = ts.towerPlacePosition.position;
        gameObject.SetActive(true);
    }

    public void Deselect()
    {
        gameObject.SetActive(false);
    }

}
