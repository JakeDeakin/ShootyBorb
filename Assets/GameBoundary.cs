using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoundary : MonoBehaviour
{
    private Camera maincam;
    
    // Start is called before the first frame update
    void Start()
    {
        maincam = GameObject.Find("Main Camera").GetComponent<Camera>();

        GenerateBoundaryCollider();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateBoundaryCollider()
    {
        Vector2[] boundaryPoints;
        boundaryPoints = this.GetComponent<EdgeCollider2D>().points;
        boundaryPoints[0] = maincam.ViewportToWorldPoint(new Vector3(0f, 0f, maincam.nearClipPlane));
        boundaryPoints[1] = maincam.ViewportToWorldPoint(new Vector3(0f, 1f, maincam.nearClipPlane));
        boundaryPoints[2] = maincam.ViewportToWorldPoint(new Vector3(1f, 1f, maincam.nearClipPlane));
        boundaryPoints[3] = maincam.ViewportToWorldPoint(new Vector3(1f, 0f, maincam.nearClipPlane));
        boundaryPoints[4] = maincam.ViewportToWorldPoint(new Vector3(0f, 0f, maincam.nearClipPlane));
        this.GetComponent<EdgeCollider2D>().points = boundaryPoints;
    }
}
