using UnityEngine;

public class Shield : MonoBehaviour
{

    public Transform shieldTransform;
    public GameObject shieldPrefab;
    public int circlePoints = 12;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Instantiate(shieldPrefab, transform.position, Quaternion.identity);
            rotateShield(3.0f, 2.0f, shieldTransform);
        }
    }

    public void rotateShield(float radius, float speed, Transform target)
    {
        float angleStep = 360f / circlePoints;

        //draw 12 shield prefabs in a circle around the player with the specified radius
        for (int i = 0; i <= circlePoints; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 dire = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            Vector3 point = transform.position + dire * radius;
            if (i > 0)
            {
               Instantiate(shieldPrefab, point, Quaternion.identity);
            }
        }
        float angle = Time.time * speed;
        float x = target.position.x + radius * Mathf.Cos(angle);
        float y = target.position.y + radius * Mathf.Sin(angle);
        transform.position = new Vector3(x, y, 0);
    }
}
