using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private float zoomEffectiveness = 1000f;
    [SerializeField]
    private float minDistance = 10f;
    [SerializeField]
    private float maxDistance = 50f;
    [SerializeField]
    private float defaultDeadZoneDepth = 10f;
    [SerializeField]
    private float defaultZDamping = 5f;
    [SerializeField]
    private float timeUntilAfterZoom = 0.1f;
    private CinemachineFramingTransposer vcamTransposer;
    private float zoom;

    // Start is called before the first frame update
    void Start()
    {
        vcamTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        zoom = Input.GetAxisRaw("Mouse ScrollWheel");

        if( (zoom != 0) && vcamTransposer.m_CameraDistance >= minDistance && vcamTransposer.m_CameraDistance <= maxDistance )
        {
            StartCoroutine( afterZoom() );
            vcamTransposer.m_DeadZoneDepth = 0;
            vcamTransposer.m_ZDamping = 1;
            vcamTransposer.m_CameraDistance -= zoom * zoomEffectiveness * Time.deltaTime;
        }
        
        vcamTransposer.m_CameraDistance = Mathf.Clamp(vcamTransposer.m_CameraDistance, minDistance, maxDistance);
    }

    IEnumerator afterZoom()
    {
        yield return new WaitForSeconds(timeUntilAfterZoom);
        vcamTransposer.m_DeadZoneDepth = defaultDeadZoneDepth;
        vcamTransposer.m_ZDamping = defaultZDamping;
    }

}
