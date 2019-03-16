using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float zoomEffectiveness;
    [SerializeField]
    private float minDistance;
    [SerializeField]
    private float maxDistance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float zoom = Input.GetAxisRaw("Mouse ScrollWheel");
        
        //Vector3 direction = player.position - gameObject.transform.position;
        float distance = Vector3.Distance(gameObject.transform.position, player.position);

        if( !((distance <= minDistance && zoom > 0) || (distance >= maxDistance && zoom < 0)) )
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.position, zoom * zoomEffectiveness * Time.deltaTime);
        }

        //gameObject.transform.position += direction.normalized * zoom * zoomEffectiveness * Time.deltaTime;
    }
}
