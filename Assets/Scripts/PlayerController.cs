using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Variable declaration
    private Rigidbody rb;
    private Vector2 movementDirection;
    public int movementSpeed;
    public int jumpforce;
    public InputActionReference move;

    
    void Start()
    {
        // Rigid body assignment
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Obtain the direction as a Vector2 from the InputAction
        movementDirection = move.action.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        // Apply the movement direction to the rigidbody as linearVelocity.
        rb.linearVelocity = new Vector2(movementDirection.x, movementDirection.y) * movementSpeed;
    }
}
