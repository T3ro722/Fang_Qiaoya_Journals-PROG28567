using UnityEngine;
using System.Collections;

public class Pipeline : MonoBehaviour
{
    Vector2 mousePos;
    Vector2 mousePosNew;
    float timer = 0.0f;
    float holdTime = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))//start of click
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//mouse position
            timer = 0.0f;//reset timer

            Debug.Log(mousePos);
        }

        if (Input.GetMouseButton(0))//during click
        {
            timer += Time.deltaTime;
            if (timer >= holdTime)
            {
                timer = 0.0f;//reset timer
                mousePosNew = Camera.main.ScreenToWorldPoint(Input.mousePosition);//mouse position
                Debug.DrawLine(new Vector3(mousePos.x, mousePos.y, 0), new Vector3(mousePosNew.x, mousePosNew.y, 0), Color.red, 5f);
            }
        }
        
        if (Input.GetMouseButtonUp(0))//end of click (part c)
        {
        print(mousePos + mousePosNew);
        }
    }
}
