using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private GameObject prevLine = null;

    private Vector3 startingPoint;
    private Vector3 endingPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // TouchPhase.Ended
            if (touch.phase == TouchPhase.Began)
            {
                startingPoint = Camera.main.ScreenToWorldPoint(touch.position);
                startingPoint.z = 0f;
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                GameObject.Destroy(prevLine);
            }
            else
            {
                endingPoint = Camera.main.ScreenToWorldPoint(touch.position);
                endingPoint.z = 0f;
                DrawNewLine(startingPoint, endingPoint, Color.white);
            }
        }
    }

    void DrawNewLine(Vector3 start, Vector3 end, Color color)
    {
        GameObject.Destroy(prevLine);
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Unlit/Texture"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        prevLine = myLine;
    }
}
