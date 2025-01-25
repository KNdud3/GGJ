using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {  
            Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractRange) && hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)) 
            {
                interactObj.Interact();
            }

        }
    }
}
