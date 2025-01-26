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
        ValidateFirstLevelConnections();
        CreateEdges();
    }

    private void OnDisable()
    {
        if (edges != null)
        {
            DestroyEdges();
        }
    }
    private void ValidateFirstLevelConnections()
    {
        var currentNodes = new List<GameObject>(connectedNodes);
        foreach (var node in currentNodes)
        {
            if (node == null) continue;
            var nodeScript = node.GetComponent<Node>();
            if (nodeScript != null && !nodeScript.connectedNodes.Contains(gameObject))
            {
                nodeScript.connectedNodes.Add(gameObject);
            }
        }
    }
    private void CreateEdges()
    {
        if (!Application.isPlaying) return;
        if (edges == null) edges = new List<GameObject>();
        
        DestroyEdges();

        foreach (var node in connectedNodes)
        {
            if (node == null) continue;

            var edge = Instantiate(edgePrefab, transform);
            if (edge != null)
            {
                var line = edge.GetComponent<LineRenderer>();
                if (line != null)
                {
                    line.SetPosition(0, transform.position);
                    line.SetPosition(1, node.transform.position);
                    edges.Add(edge);
                }
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
    var nodesToPop = new List<GameObject>(connectedNodes);
    var newConnections = new HashSet<GameObject>();

    foreach (var node in nodesToPop)
    {
        var nodeScript = node.GetComponent<Node>();
        if (nodeScript == null) continue;

        foreach (var connection in nodeScript.connectedNodes)
        {
            if (connection == null || connection == gameObject || nodesToPop.Contains(connection))
                continue;

            newConnections.Add(connection);
            var connScript = connection.GetComponent<Node>();
            if (connScript != null)
            {
                connScript.connectedNodes.Remove(node);
                if (!connScript.connectedNodes.Contains(gameObject))
                {
                    connScript.connectedNodes.Add(gameObject);
                }
            }
        }

        nodeScript.DestroyEdges();
        Destroy(node);
    }

    connectedNodes.Clear();
    connectedNodes.AddRange(newConnections);
    CreateEdges();
    }

    public void DestroyEdges()
    {
        foreach (var edge in edges)
        {
            if (edge != null)
            {
                if (Application.isPlaying)
                    Destroy(edge);
                else
                    DestroyImmediate(edge);
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