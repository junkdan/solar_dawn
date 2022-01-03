using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHelper : MonoBehaviour
{
    private float width;
    private LineRenderer lineRenderer;
    private float magnitude;
    private float multiplier = 1000f;
    private float rate = 1.0001f;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        width = lineRenderer.startWidth;
        lineRenderer.material.mainTextureScale = new Vector2(1f / width, 1.0f);
        //Debug.Log("Pos0" + lineRenderer.GetPosition(0));
        //Debug.Log("Pos1" + lineRenderer.GetPosition(1));
        //Debug.Log("Diff" + (lineRenderer.GetPosition(0) - lineRenderer.GetPosition(1)));
        //Debug.Log("SqrMgn" + );
        magnitude = (lineRenderer.GetPosition(0) - lineRenderer.GetPosition(1)).sqrMagnitude * multiplier;
        //Debug.Log("mgn: " + magnitude + " - pos0: " + lineRenderer.GetPosition(0) + " - pos1: " + lineRenderer.GetPosition(1));
        if(magnitude < 95f)
        {
            Vector3 pos = lineRenderer.GetPosition(1);
            lineRenderer.SetPosition(1, pos * rate);
        } else if(magnitude == 95f) {
            return;
        } else if (magnitude < 400f)
        {
            Vector3 pos = lineRenderer.GetPosition(1);
            lineRenderer.SetPosition(1, pos * rate);
        }
        else if (magnitude == 400f)
        {
            return;
        }
        else if (magnitude < 881f)
        {
            Vector3 pos = lineRenderer.GetPosition(1);
            lineRenderer.SetPosition(1, pos * rate);
        }
        else if (magnitude == 881f)
        {
            return;
        }
        else if (magnitude < 1599f)
        {
            Vector3 pos = lineRenderer.GetPosition(1);
            lineRenderer.SetPosition(1, pos * rate);
        }
        else if (magnitude == 1599f)
        {
            return;
        } else
        {
            return;
        }

    }
}
