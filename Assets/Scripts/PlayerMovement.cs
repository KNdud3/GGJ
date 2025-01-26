using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float stdHeight = 0.7f; //may mess up animation, remove when doing that
    public Transform cameraTransform;

    private Animator animator;

    public LayerMask layerMask;
    public float collisionOffset=0.2f;

    // Start is called before the first frame update
    void Start()
    {
        animator =GetComponentInChildren<Animator>();
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

        // Set the player's height to the standard height
        Vector3 position = transform.position;
        position.y = stdHeight;
        transform.position = position;

        // Update animation parameters (Speed = magnitude of movement)

        animator.SetBool("IsWalking", false);
        // If there is movement, apply the movement and rotation
        if (moveDirection.magnitude > 0)
        {
            

            //// Move the player by directly updating the position
            //transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

            //// Rotate the player to face the movement direction
            //Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // Smooth rotation

            // Check for collisions in the direction of movement
            if (!Physics.Raycast(transform.position, moveDirection, collisionOffset, layerMask))
            {
                animator.SetBool("IsWalking", true);
                // Move the player by directly updating the position
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

                // Rotate the player to face the movement direction
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // Smooth rotation
            }
        }
        ////DEBUG
        //if (Input.GetKeyDown(KeyCode.Keypad1))
        //{
        //    bubbles.called(0);
        //}
        //else if (Input.GetKeyDown(KeyCode.Keypad2))
        //{
        //    bubbles.called(1);
        //}
        //else if (Input.GetKeyDown(KeyCode.Keypad3))
        //{
        //    bubbles.called(2);
        //}
        //else if (Input.GetKeyDown(KeyCode.Keypad4))
        //{
        //    bubbles.called(3);
        //}
    }
}
