using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TextBubble : MonoBehaviour
{
    private string[] dialogue = { "This is first", "This is last and very long" };
    private int dialogueIndex = 0;
    public TextMeshProUGUI bubbleText;
    public GameObject elements;
    public float textSpeed;

    public ThirdPersonCamera cameraScript;
    public PlayerMovement playerScript;
    private bool triggered = false;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraScript.enabled = false; // For testing
        playerScript.enabled = false;
        bubbleText.text = "";
        StartCoroutine(SlowPrint());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return))
        {
            if (!triggered)
            {
                // Make it so it is in proximity with character (not coded yet)
                triggered = true;
            }

            else
            {
                cameraScript.enabled = false;
                if (dialogueIndex >= dialogue.Length)
                {
                    //pass
                }
                else if (bubbleText.text == dialogue[dialogueIndex])
                {
                    bubbleText.text = ""; // Get ready to print next message once finished
                    dialogueIndex++;
                    if (dialogueIndex < dialogue.Length)
                    {
                        StartCoroutine(SlowPrint());
                    }
                    else
                    {
                        elements.SetActive(false); // Close speech bubble
                        cameraScript.enabled = true;
                        playerScript.enabled = true;
                    }
                }
                else // Print out rest of text instantly
                {
                    StopAllCoroutines();
                    bubbleText.text = dialogue[dialogueIndex];
                }
            }
        }
    }
    // Prints the characters one by one
    IEnumerator SlowPrint()
    {
        foreach (char c in dialogue[dialogueIndex])
        {
            bubbleText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
