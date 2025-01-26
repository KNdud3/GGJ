using UnityEngine;

public class Node : MonoBehaviour
{
    public GameObject[] connectedNodes; 
    public GameObject edgePrefab; 
    private Renderer nodeRenderer;

    private void Start()
    {
        
        nodeRenderer = GetComponent<Renderer>();
        CreateEdges();
    }

    private void CreateEdges()
    {
        foreach (GameObject connectedNode in connectedNodes)
        {
            
            GameObject edge = Instantiate(edgePrefab, transform);

           
            LineRenderer line = edge.GetComponent<LineRenderer>();
            line.SetPosition(0, transform.position); 
            line.SetPosition(1, connectedNode.transform.position); 
        }
    }

    private void OnMouseDown()
    {
        
        HighlightNode(true);
        HighlightConnections(true);

        
        Debug.Log($"{gameObject.name} clicked. Connected nodes: {connectedNodes.Length}");
    }

    private void OnMouseUp()
    {
       
        HighlightNode(false);
        HighlightConnections(false);
    }

    private void HighlightNode(bool highlight)
    {
        
        if (highlight)
            nodeRenderer.material.color = Color.yellow; 
        else
            nodeRenderer.material.color = Color.white; 
    }

    private void HighlightConnections(bool highlight)
    {
        
        foreach (GameObject node in connectedNodes)
        {
            Renderer connectedRenderer = node.GetComponent<Renderer>();
            if (highlight)
                connectedRenderer.material.color = Color.green; 
            else
                connectedRenderer.material.color = Color.white; 
        }
    }
}
