using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private GameObject prevLine = null;
    private List<GameObject> permanentLines;

    private Vector3 startingPoint;
    private Vector3 endingPoint;

    private GameObject[] circles;

    private bool firstCircle;
    private List<int> visitedCirclesIndexes;

    // Start is called before the first frame update
    void Start()
    {
        circles = GameObject.FindGameObjectsWithTag("Circle");
        firstCircle = true;

        permanentLines = new List<GameObject>();

        visitedCirclesIndexes = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // At the begining of touch set the point as the start of the line and check if circle wasn't hit
            if (touch.phase == TouchPhase.Began)
            {
                startingPoint = Camera.main.ScreenToWorldPoint(touch.position);
                startingPoint.z = 0f;

                CheckForCircleHits(startingPoint);
            }
            // At the end of touch destroy all lines and reset control variables
            else if(touch.phase == TouchPhase.Ended)
            {
                GameObject.Destroy(prevLine);
                RemovePermanentLines();

                firstCircle = true;
                visitedCirclesIndexes.Clear();
            }
            // During the touch draw the line between the starting point and point of touch and check for circle hits
            else
            {
                endingPoint = Camera.main.ScreenToWorldPoint(touch.position);
                endingPoint.z = 0f;
                DrawNewLine(startingPoint, endingPoint, Color.white);

                CheckForCircleHits(endingPoint);
            }
        }
    }

    // For drawing a line from starting point to finger position every frame
    void DrawNewLine(Vector3 start, Vector3 end, Color color)
    {
        // First destroy line from previous frame 
        GameObject.Destroy(prevLine);

        // Create new line
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Unlit/Texture"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        // Set this line to be destroyed next frame
        prevLine = myLine;
    }

    // For drawing lines between circles when user hits them
    void DrawPermanentLine(Vector3 start, Vector3 end, Color color)
    {
        // Create new line
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Unlit/Texture"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        permanentLines.Add(myLine);
    }

    // Removes all lines between circles
    void RemovePermanentLines()
    {
        foreach (GameObject permanentLine in permanentLines)
            GameObject.Destroy(permanentLine);

        permanentLines.Clear();
    }

    // Iterates through all circles to check if touch location hits any
    void CheckForCircleHits(Vector3 location)
    {
        for (int i = 0; i < circles.Length; i++)
        {
            // If distance between touch location and middle of circle is smaller than its radius and circle wasn't visited before
            if (Vector3.Distance(location, circles[i].transform.position) <= 0.5f && !visitedCirclesIndexes.Contains(i))
            {
                // Only draw the line if some other circle has been hit before
                if (!firstCircle)
                    DrawPermanentLine(startingPoint, circles[i].transform.position, Color.white);
                else
                    firstCircle = false;

                // Set the middle of the circle as the new starting point for lines
                startingPoint = circles[i].transform.position;

                // Flag circle as previously visited
                visitedCirclesIndexes.Add(i);
                break;
            }
        }
    }
}
