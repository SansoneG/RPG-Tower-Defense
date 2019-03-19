using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float rotationSpeed = 5f;
    [SerializeField]
    private float zoomEffectiveness = 1000f;
    [SerializeField]
    private float minDistance = 10f;
    [SerializeField]
    private float maxDistance = 50f;
    [SerializeField]
    private float timeUntilAfterZoom = 0.1f;
    [SerializeField]
    private float timeUntilAfterOrbit = 0.05f;
    private CinemachineFramingTransposer vcamTransposer;
    private float defaultDeadZoneDepth;
    private float defaultDeadZoneHeight;
    private float defaultDeadZoneWidth;
    private float defaultXDamping;
    private float defaultYDamping;
    private float defaultZDamping;
    private float defaultLookaheadTime;
    private float defaultLookaheadSmoothing;
    private float zoom;

    // Start is called before the first frame update
    void Start()
    {
        vcamTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        defaultDeadZoneHeight = vcamTransposer.m_DeadZoneHeight;
        defaultDeadZoneDepth = vcamTransposer.m_DeadZoneDepth;
        defaultDeadZoneWidth = vcamTransposer.m_DeadZoneWidth;
        defaultXDamping = vcamTransposer.m_XDamping;
        defaultYDamping = vcamTransposer.m_YDamping;
        defaultZDamping = vcamTransposer.m_ZDamping;
        defaultLookaheadTime = vcamTransposer.m_LookaheadTime;
        defaultLookaheadSmoothing = vcamTransposer.m_LookaheadSmoothing;
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

        if(Input.GetMouseButton(1))
        {
            float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
            float verticalRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
            
            setTransposerToZero();

            gameObject.transform.RotateAround(player.position, Vector3.up, horizontalRotation);
            gameObject.transform.RotateAround(player.position, Vector3.right, verticalRotation);

            resetZRotation();
        }

        if(Input.GetMouseButtonUp(1))
        {
            StartCoroutine( afterOrbit() );
        }
    }

    IEnumerator afterZoom()
    {
        yield return new WaitForSeconds(timeUntilAfterZoom);
        vcamTransposer.m_DeadZoneDepth = defaultDeadZoneDepth;
        vcamTransposer.m_ZDamping = defaultZDamping;
    }

    IEnumerator afterOrbit()
    {
        yield return new WaitForSeconds(timeUntilAfterOrbit);
        vcamTransposer.m_DeadZoneHeight = defaultDeadZoneHeight;
        vcamTransposer.m_DeadZoneDepth = defaultDeadZoneDepth;
        vcamTransposer.m_DeadZoneWidth = defaultDeadZoneWidth;
        vcamTransposer.m_XDamping = defaultXDamping;
        vcamTransposer.m_YDamping = defaultYDamping;
        vcamTransposer.m_ZDamping = defaultZDamping;
        vcamTransposer.m_LookaheadTime = defaultLookaheadTime;
        vcamTransposer.m_LookaheadSmoothing = defaultLookaheadSmoothing;

    }

    void setTransposerToZero()
    {
        vcamTransposer.m_DeadZoneHeight = vcamTransposer.m_DeadZoneDepth = vcamTransposer.m_DeadZoneWidth = 0;
        vcamTransposer.m_XDamping = vcamTransposer.m_YDamping = vcamTransposer.m_ZDamping = 0;
        vcamTransposer.m_LookaheadTime = vcamTransposer.m_LookaheadSmoothing = 0;
    }

    void resetZRotation()
    {
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
        transform.rotation = q;
    }
}
