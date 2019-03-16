using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 300;
    [SerializeField]
    private float turnSpeed = 5;

    private Camera mainCamera;
    private CharacterController characterController;
    private Animator animator;

    private void Awake()
    {
        if (Camera.main != null)
        {
            mainCamera = Camera.main;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");    
        var vertical = Input.GetAxisRaw("Vertical");

        // calculate move direction to pass to character
        Vector3 dir = new Vector3(horizontal, 0, vertical);
        if (mainCamera != null)
        {
            // calculate camera relative direction to move:
            var camForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
            dir = vertical * camForward + horizontal * mainCamera.transform.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            dir = new Vector3(horizontal, 0, vertical);
        }
        
        var movement = dir.normalized * Time.deltaTime * moveSpeed;

        characterController.SimpleMove(movement);

        animator.SetFloat("Speed", movement.magnitude);

        if(dir.magnitude > 0)
        {
            Quaternion newDirection = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * turnSpeed);
        }
    }

}
