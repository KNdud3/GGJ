using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TextBubble : MonoBehaviour
{
    private string[][] dialogueList = { // When you can solve
        new string[] {"Oh hey, I'm sorry I dont really feel like talking right now"}, 
        new string[] {"I am NPC4", "I am very happy"},
        new string[] {"Why are you staring at me?"},
        new string[] {"Whats your problem", "Just go away"}
    };
    private string[][] dialogueList2 = {  // Before you can solve
    new string[] {"If you see this something is wrong"}, 
    new string[] {"You are very weird","You can't talk and don't know how to be happy"},
    new string[] {"ahh..","uhh.."},
    new string[] {"Whats your deal, cat got your tounge?","Go talk to blondie or something"}
    };
    private string[][] dialogueListSolv = { // After you solve
    new string[] {"Thanks a lot", "Sometimes you just need someone to talk to you know"}, 
    new string[] {"Sorry for being so rude","I understand now people can all be happy in different ways"},
    new string[] {"I get really shy around new people but seeing you make the effort gives me some confidence"},
    new string[] {"I dont know why I was so mad earlier", "Thanks for bearing with me"}
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
                    dialogueIndex = 0;
                    overTrigger = false;
                    cameraScript.enabled = true;
                    playerScript.enabled = true;
                    playerInteractor.enabled = true;
                    if (correctNpc){
                        correctNpc = false;
                        npc++;
                        SceneManager.LoadScene(sceneName:"Puzzle");
                    }
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
