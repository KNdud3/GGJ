using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scr : MonoBehaviour
{
    private string[] dialogue = { "This is first", "this is last" };
    private int index = 0;
    public TextMeshProUGUI bubbleText;
    
    // Start is called before the first frame update
    void Start()
    {
        bubbleText.text = "Hello, TMP World!";
    }

    // Update is called once per frame
    void Update()
    {
        bubbleText.text = dialogue[index];
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return))
        {
            index++;
        }
    }
}
