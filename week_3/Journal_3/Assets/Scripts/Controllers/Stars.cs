using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    [SerializeField] List<Transform> starTransforms = new List<Transform>();
    float drawingTime = 5.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DrawConstellation();
    }

    public void DrawConstellation()
    {
        if (starTransforms.Count < 5) return;
        Debug.DrawLine(starTransforms[0].position, starTransforms[1].position, Color.white, drawingTime);
        Debug.DrawLine(starTransforms[1].position, starTransforms[2].position, Color.white, drawingTime);
        Debug.DrawLine(starTransforms[2].position, starTransforms[3].position, Color.white, drawingTime);
        Debug.DrawLine(starTransforms[3].position, starTransforms[4].position, Color.white, drawingTime);
    }
}
