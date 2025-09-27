using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Vector3 maxFloatDistance;
    private float moveSpeed = 3f;
    private float arrivalDistance = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMovement();
    }

    public void AsteroidMovement()
    {
        //maxFloatDistance = new Vector3(Random.Range(-60f, 60f), Random.Range(-60f, 60f), 0);
        Vector3 direction = (maxFloatDistance - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        if (Vector3.Distance(transform.position, maxFloatDistance) < arrivalDistance)
        {
            maxFloatDistance = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);

        }
        maxFloatDistance = Vector3.ClampMagnitude(maxFloatDistance, 20f);
    }
}
