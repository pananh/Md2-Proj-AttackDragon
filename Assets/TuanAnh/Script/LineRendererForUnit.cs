using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererForUnit : MonoBehaviour
{

    LineRenderer lineRenderer;
    Vector3 oldPosition;
    float minDistance = 0.1f;
    bool isMoving = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 0;
        oldPosition = transform.position;

    }

    void Update()
    {
        isMoving = changingPosition();
        if ( isMoving ) 
        {
            oldPosition = transform .position;
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount-1,transform.position);

            if (lineRenderer.positionCount > 50)
            {
                lineRenderer.positionCount = 0;
            }

        }
    }


    private bool changingPosition()
    {
        if ( Vector3.SqrMagnitude(oldPosition - transform.position) < minDistance)
        { return false; }
        return true;
    }


}
