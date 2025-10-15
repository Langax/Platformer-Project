using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Variable declaration
    private Rigidbody rb;
    private Vector2 movementDirection;
    private bool canJump = true;
    public int movementSpeed;
    public int jumpForce;
    
    void Start()
    {
        // Rigid body assignment
        rb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        // Apply the movement direction to the rigidbody (Normalized by deltaTime) as linearVelocity.
        rb.linearVelocity = new Vector2(movementDirection.x * (movementSpeed * Time.deltaTime), rb.linearVelocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        // Obtain the direction as a Vector2 from the InputAction
        movementDirection = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        // If the player isn't still in the air, apply an upward force of jumpForce
        if (canJump)
        {
            rb.AddForce(0.0f, jumpForce, 0.0f);
            canJump = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // Reset the jump when the player hits the ground 
        if (other.gameObject.CompareTag("Ground") && !canJump)
        {
            canJump = true;
        }
    }
}
