using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float moveSpeed = 5f;      // Speed of the ball's horizontal movement
    public float jumpForce = 10f;     // Force for the automatic jump when hitting the platform
    public float groundCheckRadius = 0.3f; // Radius to check if the ball is grounded
    public LayerMask platformLayer;   // Platform layer for collision detection
    public Transform groundCheck;     // Transform to check if the ball is on the ground

    private Rigidbody rb;             // Rigidbody for physics-based movement
    private bool isGrounded;          // To check if the ball is grounded
    private Vector3 movementDirection; // Stores the movement direction (left-right)

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Initialize Rigidbody
    }

    void Update()
    {
        // Handle left and right movement using A/D or Arrow keys
        float moveInput = 0f;

        if (Input.GetKey(KeyCode.A)) // Move left
        {
            moveInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D)) // Move right
        {
            moveInput = 1f;
        }

        // Set movement direction
        movementDirection = new Vector3(moveInput, 0f, 0f).normalized;

        // Check if the ball is grounded (on a platform)
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, platformLayer);
    }

    void FixedUpdate()
    {
        // Move the ball left and right based on player input
        rb.MovePosition(transform.position + movementDirection * moveSpeed * Time.fixedDeltaTime);

        // If the ball is grounded, apply the jump automatically once
        if (isGrounded && Mathf.Abs(rb.linearVelocity.y) < 0.1f) // Only jump if it's not already in the air
        {
            Jump();
        }
    }

    // Automatically make the ball jump when it hits the platform
    void Jump()
    {
        // Apply an upward force to make the ball jump
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // Optional: Debug visualization of the ground check area in the editor
    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
