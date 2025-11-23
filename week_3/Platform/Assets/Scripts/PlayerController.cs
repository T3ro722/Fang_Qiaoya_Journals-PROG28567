using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum FacingDirection
    {
        left, right
    }
    Rigidbody rb;
    public float moveSpeed = 5f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.7f;
    public LayerMask groundLayer;

    public float apexHeight = 3.5f;
    public float apexTime = 0.5f;

    private Vector3 velocity;

    private float gravity;
    private float jumpVel;

    public float terminalSpeed = 0.1f;// if player falls faster than this, set velocity to this value


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gravity = -2 * apexHeight / (apexTime * apexTime);
        jumpVel = 2 * apexHeight / apexTime;
    }

    // Update is called once per frame
    void Update()
    {
        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.
        Vector2 playerInput = new()
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = Input.GetButtonDown("Jump") ? 1 : 0
        };
        MovementUpdate(playerInput);
    }

   
    private void MovementUpdate(Vector2 playerInput)
    {
        
        if (playerInput.x == 0)
        {
            velocity.x = 0;
        }
        else
        {
            velocity.x = playerInput.x * moveSpeed;
        }

        JumpInput(playerInput);

        if (velocity.y < terminalSpeed)
        {
            velocity.y = terminalSpeed;
        }

        transform.position += velocity * Time.deltaTime;



    }

    private void JumpInput(Vector2 playerInput)
    {
        if (IsGrounded() && playerInput.y == 1)
            velocity.y = jumpVel;
        else if (!IsGrounded())
            velocity.y += gravity * Time.deltaTime;
        else
            velocity.y = 0;
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
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * 0.7f);
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
