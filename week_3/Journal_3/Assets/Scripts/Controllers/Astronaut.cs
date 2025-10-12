using UnityEngine;

public class Astronaut : MonoBehaviour
{
    public GameObject astronautPrefab;
    public Transform astronautTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            releaseAstronaut(1.0f, 5.0f, astronautTransform);
        }
    }

    public void releaseAstronaut(float radius, float speed, Transform target)
    {
        //spawn an astronaut prefab above player's position
        Instantiate(astronautPrefab, transform.position + new Vector3(0, 1), Quaternion.identity);
        //astronaut rotate around player
        float angle = Time.time * speed;
        float x = target.position.x + radius * Mathf.Cos(angle);
        float y = target.position.y + radius * Mathf.Sin(angle);
        transform.position = new Vector3(x, y, 0);
    }
}
