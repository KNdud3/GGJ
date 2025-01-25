using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class scr : MonoBehaviour
{
    private string[] dialogue = { "This is first", "this is last" };
    private int dialogueIndex = 0;
    public TextMeshProUGUI bubbleText;

    public float textSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        bubbleText.text = "";
        StartCoroutine(SlowPrint());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return))
        {
            if (bubbleText.text == dialogue[dialogueIndex])
            {
                bubbleText.text = "";
                dialogueIndex++;
                if (dialogueIndex >= dialogue.Length)
                {
                    SceneManager.LoadScene(0); // I hard coded this don't mess with the build scene order
                }
                else
                {
                    StartCoroutine(SlowPrint());
                }
            }
            else
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
}
