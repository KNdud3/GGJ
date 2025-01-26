using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TextBubble : MonoBehaviour
{
    private string[][] dialogueList = { // When you can solve
        new string[] {"I am NPC1", "I am very sad"}, 
        new string[] {"I am NPC4", "I am very happy"},
        new string[] {"I am NPC3", "I am very shy"},
        new string[] {"I am NPC2", "I am very angry"}
    };
    private string[][] dialogueList2 = {  // Before you can solve
    new string[] {"If you see this something is wrong"}, 
    new string[] {"You are very weird","You can't talk and don't know how to be happy"},
    new string[] {"AHHHHHHH don't talk to me","I don't like meeting new people"},
    new string[] {"FGJHJKIYGGBJKIF", "CAN'T YOU SEE I'M MAD"}
    };
    private string[][] dialogueListSolv = { // After you solve
    new string[] {"Thank you for helping me","Speaking to you let me understand my feelings"}, 
    new string[] {"Sorry for being so rude","I understand now people can all be happy in different ways"},
    new string[] {"I see I shouldn't judge a book by its cover", "I will try and overcome my shyness","I hope you can do the same"},
    new string[] {"Wow speaking to you really allowed me to cool down", "Talking is a really powerful thing"}
    };
    private int dialogueIndex = 0;
    private int npc = 0;
    private string[] dialogue;
    public TextMeshProUGUI bubbleText;
    public GameObject elements;
    public float textSpeed;

    public ThirdPersonCamera cameraScript;
    public PlayerMovement playerScript;
    public Interactor playerInteractor;

    private bool triggered = false;
    private bool overTrigger = false;
    private bool correctNpc = false;

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
                    if (correctNpc){
                        correctNpc = false;
                        npc++;
                    }
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
        foreach (char c in dialogue[dialogueIndex])
        {
            bubbleText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void called(int num)
    {
        if (npc == num)
        {   
            dialogue = dialogueList[num];
            correctNpc = true;
        }
        else if (num > npc)
        {
            dialogue = dialogueList2[num];
        }
        else
        {
            dialogue = dialogueListSolv[num];
        }
        triggered = true;
        overTrigger = true;
    }
}
