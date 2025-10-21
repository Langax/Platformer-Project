using UnityEngine;

public class FireBallMovement : MonoBehaviour
{
    // Variable declaration.
    private Rigidbody rb;
    private float angle = -45;
    private int force = 8;

    // For use later when enemy death will increase score.
    public PlayerController playerController;
    
    void Start()
    { 
        // Immediately add a force to the Fireball to push it away from the Player.
        rb = GetComponent<Rigidbody>();
        rb.AddForce(90.0f, 0.0f, 0.0f);
    }
    
    void OnCollisionEnter(Collision other)
    {
        // When the fireball hits the ground, reset its momentum and apply a 45-degree angle force.
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Platform"))
        {
            rb.linearVelocity = Vector3.zero;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            rb.AddForce(rotation * Vector2.up * force, ForceMode.Impulse);
        }
        // When the fireball hits the wall or a pipe, destroy it.
        else if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Pipe"))
        {
            Destroy(gameObject);
        }
        // TODO: Collision with Enemies (destroy self & enemy).
        // 
    }
}
