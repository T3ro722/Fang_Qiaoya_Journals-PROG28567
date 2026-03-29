using UnityEngine;

public class test : MonoBehaviour
{

    public float speed = 5f;

    private Rigidbody rb;
    private Vector3 movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // A/D
        float moveZ = Input.GetAxis("Vertical");   // W/S

        movement = new Vector3(-moveX, 0f, -moveZ);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);//move the player based on the movement vector and speed, multiplied by Time.fixedDeltaTime to ensure consistent movement regardless of frame rate
    }
}
