using UnityEngine;
using UnityEngine.UI;

public class CircleClickHandler : MonoBehaviour
{
    // public AudioClip clickSound; 
    // private AudioSource audioSource;
    private bool isDragging = false;
    private Vector3 offset;

    void Start()
    {
        // audioSource = GetComponent<AudioSource>(); // Assumes an AudioSource is attached to the same object
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; // Adjust this value to match the depth canvas
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition) + offset;
        }
    }

    public void OnClick()
    {
        Debug.Log(gameObject.name + " clicked");
        isDragging = true;
        // Offset the position so the circle follows the mouse from where it was clicked
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        offset = transform.position - Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public void OnRelease()
    {
        Debug.Log(gameObject.name + " released");
        isDragging = false;

        // // Play the click sound on release
        // if (audioSource != null && clickSound != null)
        // {
        //     audioSource.PlayOneShot(clickSound);
        // }
    }
}
