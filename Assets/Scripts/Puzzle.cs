using UnityEngine;
using System.Collections.Generic;

public class Puzzle : MonoBehaviour
{
    private List<Node> nodes = new List<Node>();
    
    private void Start()
    {
        nodes.AddRange(GetComponentsInChildren<Node>());
    }

    private void Update()
    {
        if (nodes.Count > 0)
        {
            nodes.RemoveAll(node => node == null);
            if (nodes.Count == 1)
            {
                PuzzleComplete();
            }
        }
    }

    private void PuzzleComplete()
    {
        gameObject.SetActive(false);
        GameManager.Instance.OnPuzzleComplete();
    }
}