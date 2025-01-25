using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCscript : MonoBehaviour, IInteractable
{
    public TextBubble bubbles;
    public void Interact()
    {
        bubbles.called();
    }
}
