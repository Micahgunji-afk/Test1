using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float moveSpeed = 10f;  // Adjust the rolling speed of the ball
    public float turnSpeed = 100f;  // Adjust the turning speed of the ball
    public Rigidbody rb;  // Rigidbody component of the ball

    private Vector3 moveDirection;
    private float horizontalInput;
    private float verticalInput;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();  // Ensure Rigidbody is assigned
        }
    }

    void Update()
    {
        // Get player input for movement (WASD or arrow keys)
        horizontalInput = Input.GetAxis("Horizontal");  // A/D or Left/Right arrow
        verticalInput = Input.GetAxis("Vertical");  // W/S or Up/Down arrow

        // Calculate direction and move
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Apply rolling movement
        if (moveDirection.magnitude >= 0.1f)
        {
            MoveBall();
            TurnBall();
        }
    }

    // Move the ball in the desired direction
    void MoveBall()
    {
        // Apply force in the direction the ball is facing
        Vector3 moveForce = moveDirection * moveSpeed * Time.deltaTime;
        rb.AddForce(moveForce, ForceMode.VelocityChange);
    }

    // Turn the ball based on horizontal input
    void TurnBall()
    {
        float turn = horizontalInput * turnSpeed * Time.deltaTime;

        // Rotate the ball around its Y-axis (vertical axis)
        transform.Rotate(0f, turn, 0f);
    }
}
