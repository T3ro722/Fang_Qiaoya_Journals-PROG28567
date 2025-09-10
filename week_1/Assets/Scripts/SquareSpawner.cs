using UnityEngine;
using UnityEngine.InputSystem.XR;

public class SquareSpawner : MonoBehaviour
{
    public Camera cam;//use screen to world

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
    }
}
