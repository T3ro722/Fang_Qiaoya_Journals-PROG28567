using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum FacingDirection
    {
        left, right
    }
    Rigidbody rb;
    public float moveSpeed = 5f;//max speed in units per second
    public float accelTime = 0.7f; //1s to reach max speed
    public float decelTime = 0.5f; //0.5s to stop from max speed

    private float acceleration;
    private float deceleration;

    public float dashDistance = 3f; //dash distance in units

    public Transform groundCheck;
    public float groundCheckRadius = 0.7f;
    public LayerMask groundLayer;

    public float apexHeight = 3.5f;
    public float apexTime = 0.5f;

    private Vector3 velocity;

    private float gravity;
    private float jumpVel;

    public float buffedjump = 1.5f; // multiplier for jump height when buffed

    public float terminalSpeed = 0.1f;// if player falls faster than this, set velocity to this value

    public float coyoteTime = 0.5f; // time after leaving ground that jump is still allowed
    private float coyoteTimeCounter;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gravity = -2 * apexHeight / (apexTime * apexTime);
        jumpVel = 2 * apexHeight / apexTime;

        acceleration = moveSpeed / accelTime;
        deceleration = moveSpeed / decelTime;
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
           if (GetFacingDirection() == FacingDirection.right)
            {
                transform.position += Vector3.right * dashDistance;
            }
            else
            {
                transform.position += Vector3.left * dashDistance;
            }
        }
     
        MovementUpdate(playerInput);
    }

   
    private void MovementUpdate(Vector2 playerInput)
    {

        float inputX = playerInput.x;

        float targetSpeed = inputX * moveSpeed;
        if (Mathf.Abs(targetSpeed) > Mathf.Abs(velocity.x))
        {
            // Accelerating
            float rate = acceleration;
            velocity.x = Mathf.MoveTowards(velocity.x, targetSpeed, rate * Time.deltaTime);
        }
        else
        {
            // Decelerating
            float rate = deceleration;
            velocity.x = Mathf.MoveTowards(velocity.x, targetSpeed, rate * Time.deltaTime);
        }


        if (IsGrounded())
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        JumpInput(playerInput);


        if (velocity.y < terminalSpeed)
        {
            velocity.y = terminalSpeed;
        }

        transform.position += velocity * Time.deltaTime;



    }

    private void JumpInput(Vector2 playerInput)
    {
        if (Input.GetKey(KeyCode.Q))
        {
            velocity.y = jumpVel * buffedjump;
        }
        else if (playerInput.y == 1 && coyoteTimeCounter > 0f)
        {
            velocity.y = jumpVel;
        }
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
