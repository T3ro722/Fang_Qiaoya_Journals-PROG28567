using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;

public class SquareSpawner : MonoBehaviour
{
    public Camera cam;//use screen to world
    public float squaresize = 10f;//size of square
    public Vector2 mousescroll;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 pos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y));

        //draw when mouse is down
        if (Input.GetMouseButtonDown(0)) 
          
            {
            Debug.DrawLine(pos + new Vector3(-2, 2, 0), pos + new Vector3(2, 2, 0), Color.white, 1f);
            Debug.DrawLine(pos + new Vector3(-2, -2, 0), pos + new Vector3(2, -2, 0), Color.white, 1f);
            Debug.DrawLine(pos + new Vector3(-2, 2, 0), pos + new Vector3(-2, -2, 0), Color.white, 1f);
            Debug.DrawLine(pos + new Vector3(2, 2, 0), pos + new Vector3(2, -2, 0), Color.white, 1f);
        }

        //draw a transparent square at all times
        transform.position = pos;

        //mouse scroll to change size of square
        mousescroll = Input.mouseScrollDelta;
        transform.localScale = new Vector2(squaresize + mousescroll.y, squaresize + mousescroll.y);

    }
}
