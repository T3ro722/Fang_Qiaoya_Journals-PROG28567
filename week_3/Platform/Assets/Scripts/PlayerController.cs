using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed = 5f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.8f;
    public LayerMask groundLayer;

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
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public bool IsGrounded()
    {
        

        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRadius, groundLayer);
        if (hit.collider != null)
        {
            Debug.Log("Grounded");
            return true;
            
        }
        Debug.Log("Not Grounded");
        return false;
        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * 0.8f);
    }

    public FacingDirection GetFacingDirection()
    {
        

        if (Input.GetKey(KeyCode.D))
        {
            return FacingDirection.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            return FacingDirection.left;
        }

        return FacingDirection.right; // Default value
    }
}
