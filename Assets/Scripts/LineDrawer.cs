using UnityEngine;

public class DrawLines : MonoBehaviour
{
    public Transform A;
    public Transform B;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        UpdateLine();
    }

    void UpdateLine()
    {
        lineRenderer.SetPosition(0, A.position);
        lineRenderer.SetPosition(1, B.position);
    }
}
