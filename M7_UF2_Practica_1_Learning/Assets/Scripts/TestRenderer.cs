using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRenderer : MonoBehaviour
{

    public LineRenderer lineRenderer;
    public Transform object1;
    public Transform object2;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {

        lineRenderer.SetPosition(0, object1.position);
        lineRenderer.SetPosition(1, object2.position);

    }
}
