using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public Transform planetTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
            OrbitalMotion(1.0f, 5.0f, planetTransform);
        
    }

    public void OrbitalMotion(float radius, float speed, Transform target)
    {
        float angle = Time.time * speed;
        float x = target.position.x + radius * Mathf.Cos(angle);
        float y = target.position.y + radius * Mathf.Sin(angle);
        transform.position = new Vector3(x, y, 0);
    }
}
