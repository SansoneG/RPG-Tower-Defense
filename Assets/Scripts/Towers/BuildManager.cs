using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            if(Physics.Raycast(ray, out hitInfo))
            {
                GameObject go = hitInfo.transform.gameObject;

                while(true)
                {
                    if(go.GetComponent<TowerSpace>() != null)
                    {
                        //Debug.Log("We hit a Tower Space");
                        // For now just place the standard tower from the tower space itself
                        go.GetComponent<TowerSpace>().BuildTower();
                        break;
                    }
                    else if(go.transform.parent == null)
                    {
                        //Debug.Log("We hit something else");
                        break;
                    }
                    
                    go = go.transform.parent.gameObject;

                }
            }
        }
    }

}
