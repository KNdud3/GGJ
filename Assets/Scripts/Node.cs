using UnityEngine;
using System.Collections.Generic;

public class Node : MonoBehaviour
{
    public List<GameObject> connectedNodes = new List<GameObject>();
    public GameObject edgePrefab;
    private List<GameObject> edges = new List<GameObject>();
    private Renderer nodeRenderer;

    private void Start()
    {
        nodeRenderer = GetComponent<Renderer>();
        CreateEdges();
    }

    private void CreateEdges()
    {
        DestroyEdges(); 
        foreach (GameObject connectedNode in connectedNodes)
        {
            if (connectedNode != null)
            {
                GameObject edge = Instantiate(edgePrefab, transform);
                LineRenderer line = edge.GetComponent<LineRenderer>();
                line.SetPosition(0, transform.position);
                line.SetPosition(1, connectedNode.transform.position);
                edges.Add(edge);
            }
        }
    }

    private void OnMouseDown()
    {
        if (connectedNodes.Count == 3)
        {
            PopConnectedNodes();
        }
    }

    private void PopConnectedNodes()
    {
        List<GameObject> nodesToPop = new List<GameObject>(connectedNodes);
        HashSet<GameObject> newConnections = new HashSet<GameObject>();

        
        foreach (GameObject node in nodesToPop)
        {
            Node nodeScript = node.GetComponent<Node>();
            if (nodeScript != null)
            {
                
                foreach (GameObject connection in nodeScript.connectedNodes)
                {
                    if (connection != null && connection != gameObject && !nodesToPop.Contains(connection))
                    {
                        newConnections.Add(connection);
                    }
                }
                
                
                foreach (GameObject connection in nodeScript.connectedNodes)
                {
                    Node connScript = connection?.GetComponent<Node>();
                    if (connScript != null)
                    {
                        connScript.connectedNodes.Remove(node);
                        connScript.DestroyEdges();
                        connScript.CreateEdges();
                    }
                }

                
                nodeScript.DestroyEdges();
                Destroy(node);
            }
        }

        
        connectedNodes.Clear();
        connectedNodes.AddRange(newConnections);
        CreateEdges();
    }

    public void DestroyEdges()
    {
        foreach (GameObject edge in edges)
        {
            if (edge != null)
            {
                Destroy(edge);
            }
        }
        edges.Clear();
    }

    private void OnValidate()
    {
        
        if (Application.isEditor && !Application.isPlaying)
        {
            CreateEdges();
        }
    }
}