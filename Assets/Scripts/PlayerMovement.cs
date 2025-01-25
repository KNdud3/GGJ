using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public float moveSpeed = 5f;
    //public Transform cameraTransform;

    //private Rigidbody rb;
    //private Animator animator;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    animator = GetComponent<Animator>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    // Get movement input (WASD or Arrow keys)
    //    float moveX = Input.GetAxis("Horizontal");
    //    float moveZ = Input.GetAxis("Vertical");

    //    // Calculate movement direction relative to the camera's orientation
    //    Vector3 cameraForward = cameraTransform.forward;
    //    Vector3 cameraRight = cameraTransform.right;

    //    // Set Y to 0 to ignore vertical movement for the camera (avoid movement on the Y axis)
    //    cameraForward.y = 0;
    //    cameraRight.y = 0;

    //    // Normalize the vectors to ensure consistent speed regardless of direction
    //    cameraForward.Normalize();
    //    cameraRight.Normalize();

    //    // Calculate the movement direction based on input and camera orientation
    //    Vector3 moveDirection = (cameraForward * moveZ + cameraRight * moveX).normalized;

    //    // Update animation parameters (i.e., trigger walking or idle animation based on movement)
    //    animator.SetFloat("Speed", moveDirection.magnitude);

    //    // If there is movement, apply the movement and rotation manually with Rigidbody
    //    if (moveDirection.magnitude > 0)
    //    {
    //        // Move the player via Rigidbody (physics-based movement)
    //        rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

    //        // Smoothly rotate the player towards the movement direction
    //        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
    //        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f); // Smooth rotation
    //    }
    //}


    public float moveSpeed = 5f;
    public Transform cameraTransform;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get movement input (WASD or Arrow keys)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate movement direction relative to the camera
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        // Set Y to 0 to avoid moving vertically
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Normalize the vectors to ensure consistent movement speed
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the movement direction based on the input and camera orientation
        Vector3 moveDirection = (cameraForward * moveZ + cameraRight * moveX).normalized;

        // Update animation parameters (Speed = magnitude of movement)
        //animator.SetFloat("Speed", moveDirection.magnitude);

        // If there is movement, apply the movement and rotation
        if (moveDirection.magnitude > 0)
        {
            // Move the player by directly updating the position
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

            // Rotate the player to face the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // Smooth rotation
        }
    }
}
