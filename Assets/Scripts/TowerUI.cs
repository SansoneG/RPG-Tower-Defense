using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUI : MonoBehaviour
{

    private Camera mainCamera;

    [SerializeField]
    private Transform rotationPoint;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        rotationPoint.LookAt(rotationPoint.position + mainCamera.transform.rotation * Vector3.forward, 
                    mainCamera.transform.rotation * Vector3.up);
    }

}
