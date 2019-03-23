using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    [Header("Stats")]

    [SerializeField]
    private float speed = 10f;


    private Vector3 direction;

    public void Shoot(Vector3 dir)
    {
        this.direction = dir;

        Destroy(gameObject, 2f);
    }

    void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

}
