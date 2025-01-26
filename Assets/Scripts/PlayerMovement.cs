//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerMovement : MonoBehaviour
//{
//    public float moveSpeed = 5f;
//    public Transform cameraTransform;

//    private Rigidbody rb;
//    // Start is called before the first frame update
//    void Start()
//    {
//        rb = GetComponent<Rigidbody>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        float moveX = Input.GetAxis("Horizontal");
//        float moveZ = Input.GetAxis("Vertical");

//        Vector3 cameraForward = cameraTransform.forward;
//        Vector3 cameraRight = cameraTransform.right;

//        cameraForward.y = 0;
//        cameraRight.y=0;

//        cameraForward.Normalize();
//        cameraRight.Normalize();
//        Vector3 moveDirection = (cameraForward * moveZ + cameraRight * moveX).normalized;

//        if(moveDirection.magnitude > 0)
//        {
//            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

//            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
//            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f); // Smooth rotation
//        }
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform cameraTransform;

    private Animator animator;

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

        // Update animation parameters (Speed = magnitude of movement)

        animator.SetBool("IsWalking", false);
        // If there is movement, apply the movement and rotation
        if (moveDirection.magnitude > 0)
        {
            animator.SetBool("IsWalking", true);

            // Move the player by directly updating the position
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

            // Rotate the player to face the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // Smooth rotation
        }
    }
}
