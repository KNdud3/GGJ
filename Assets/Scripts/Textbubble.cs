using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TextBubble : MonoBehaviour
{
    private string[][] dialogueList = { 
        new string[] {"I am NPC1", "I am very sad"}, 
        new string[] {"I am NPC4", "I am very happy"},
        new string[] {"I am NPC3", "I am very shy"},
        new string[] {"I am NPC2", "I am very angry"}
    };
    private int dialogueIndex = 0;
    private int npc = 0;
    public TextMeshProUGUI bubbleText;
    public GameObject elements;
    public float textSpeed;

    public ThirdPersonCamera cameraScript;
    public PlayerMovement playerScript;
    public Interactor playerInteractor;

    private bool triggered = false;
    private bool overTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        elements.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return)) && overTrigger)
        {
            string[] dialogue = dialogueList[npc];
            if (triggered)
            {
                elements.SetActive(true);
                cameraScript.enabled = false; // For testing
                playerScript.enabled = false;
                playerInteractor.enabled = false;
                bubbleText.text = "";
                triggered = false;
                StartCoroutine(SlowPrint());
            }
            else if (dialogueIndex >= dialogue.Length)
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
                    npc++;
                    dialogueIndex = 0;
                    overTrigger = false;
                    cameraScript.enabled = true;
                    playerScript.enabled = true;
                    playerInteractor.enabled = true;
                }
            }
            else // Print out rest of text instantly
            {
                StopAllCoroutines();
                bubbleText.text = dialogue[dialogueIndex];
            }
        }
    }
    // Prints the characters one by one
    IEnumerator SlowPrint()
    {
        foreach (char c in dialogueList[npc][dialogueIndex])
        {
            bubbleText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void called(int num)
    {
        if (npc == num && num < dialogueList.Length)
        {   
            triggered = true;
            overTrigger = true;
        }
    }
}
