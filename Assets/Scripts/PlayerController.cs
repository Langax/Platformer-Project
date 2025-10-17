using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Variable declaration.
    private Rigidbody rb;
    private Vector2 movementDirection;
    
    private bool canJump = true;
    private float fireTimer = 0.0f;
    private float fireBallCooldown;
    private int score;
    
    public int movementSpeed;
    public int jumpForce;
    public GameObject fireBallPrefab;
    public TextMeshProUGUI scoreText;
    
    void Start()
    {
        // Rigid body assignment.
        rb = GetComponent<Rigidbody>();

        score = 0;
    }
    
    void FixedUpdate()
    {
        // Apply the movement direction to the rigidbody (Normalized by deltaTime) as linearVelocity.
        rb.linearVelocity = new Vector2(movementDirection.x * (movementSpeed * Time.deltaTime), rb.linearVelocity.y);

        // Constantly decrease the fireTimer and fireBallCooldown if they are active.
        if (fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
        }
        if (fireBallCooldown > 0)
        {
            fireBallCooldown -= Time.deltaTime;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        // Obtain the direction as a Vector2 from the InputAction.
        movementDirection = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        // Only jump on the button press, not release.
        if (context.phase == InputActionPhase.Started)
        {
            // If the player isn't still in the air, apply an upward force of jumpForce.
            if (canJump)
            {
                rb.AddForce(0.0f, jumpForce, 0.0f);
                canJump = false;
            }
        }
    }

    // TODO: Make fireball not collide with the player.
    // TODO: Make fireball fire backward or forward depending on player direction.
    public void FireBall(InputAction.CallbackContext context)
    {
        // So long as the fire timer is greater than 0, and it's not on cooldown, shoot a fireball.
        if (fireTimer > 0 && fireBallCooldown <= 0)
        {
            GameObject fireBall = Instantiate(fireBallPrefab, transform.position, transform.rotation);
            fireBallCooldown = 1.0f;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // Reset the jump when the player hits the ground.
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Platform") && !canJump)
        {
            canJump = true;
        }
        // Increase the fireTimer to allow fireballs to be shot.
        else if (other.gameObject.CompareTag("FirePowerUp"))
        {
            Debug.Log("Activating Fire power up");
            Destroy(other.gameObject);
            fireTimer += 10.0f;
        }
    }

    public void IncreaseScore(int increaseAmount)
    {
        if (increaseAmount > 0)
        {
            score++;
            Debug.Log("Score: " + score);
            SetScoreText();
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
