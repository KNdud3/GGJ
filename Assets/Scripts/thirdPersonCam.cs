using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player; // Assign the player's Transform in the Inspector
    public Vector3 offset = new Vector3(0, 5, -10); // Default camera position relative to the player
    public float rotationSpeed = 5f; // Speed of camera rotation

    public float zoomSpeed = 2f; // Speed of zooming in/out
    public float minZoom = 2f; // Minimum zoom distance
    public float maxZoom = 15f; // Maximum zoom distance

    private float currentZoom; // Current zoom level
    private float currentX = 0f; // Horizontal rotation
    private float currentY = 0f; // Vertical rotation
    public float yMinLimit = -30f; // Min vertical angle
    public float yMaxLimit = 60f; // Max vertical angle

    void Start()
    {
        // Set initial zoom level based on the offset
        currentZoom = offset.magnitude;
    }

    void LateUpdate()
    {
        if (enabled){
            // Handle mouse scroll input for zooming
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            currentZoom -= scrollInput * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom); // Clamp the zoom level

            // Mouse input for rotation
            currentX += Input.GetAxis("Mouse X") * rotationSpeed;
            currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;

            // Clamp vertical rotation
            currentY = Mathf.Clamp(currentY, yMinLimit, yMaxLimit);

            // Calculate new offset based on zoom
            Vector3 zoomedOffset = offset.normalized * currentZoom;

            // Calculate new camera position and rotation
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            Vector3 position = player.position + rotation * zoomedOffset;

            // Set camera position and look at the player
            transform.position = Vector3.Lerp(transform.position,position,Time.deltaTime*5f);
            transform.LookAt(player.position);
        }
    }
}
