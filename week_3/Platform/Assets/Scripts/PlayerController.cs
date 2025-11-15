using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed = 5f;

    public enum FacingDirection
    {
        left, right
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.
        Vector2 playerInput = new Vector2();
        MovementUpdate(playerInput);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        if (Input.GetKey(KeyCode.A))
        {
          rb.MovePosition(rb.position + Vector2.left * moveSpeed * Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
          rb.MovePosition(rb.position + Vector2.right * moveSpeed * Time.fixedDeltaTime);
        }

    }

    public bool IsWalking()
    {
        return false;
    }
    public bool IsGrounded()
    {
        return false;
    }

    public FacingDirection GetFacingDirection()
    {
        return FacingDirection.left;
    }
}
