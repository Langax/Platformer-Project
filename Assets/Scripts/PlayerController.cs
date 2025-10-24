using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Variable declaration.
    private Rigidbody rb;
    private Vector2 movementDirection;
    private bool canJump = true;
    private float fireTimer = 0.0f;
    private float fireBallCooldown;
    private int score;
    private int lives = 3;
    
    public int movementSpeed;
    public int jumpForce;
    public int health;
    public bool stoodOnPipe = false;
    public GameObject fireBallPrefab;
    public TextMeshProUGUI scoreText;
    public Transform fireBallSpawnLocation;
    public Transform bossRoomSpawnLocation;


    void Start()
    {
        // Rigid body assignment.
        rb = GetComponent<Rigidbody>();

        score = 0;
    }

    void Update()
    {
        // Keep the rotation fixed.
        transform.rotation = new Quaternion(0, 0, 0, 1);
    }
    
    //==========================================================================
    //MOVEMENT & INPUTS
    //==========================================================================
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

    public void Crouch(InputAction.CallbackContext context)
    {
        // Teleport the player to the bossRoom if they crouch on the DownPipe
        if (stoodOnPipe)
        {
            gameObject.transform.position = bossRoomSpawnLocation.position;
        }
    }
    
    // TODO: Make fireball fire backward or forward depending on player direction.
    public void FireBall(InputAction.CallbackContext context)
    {
        // So long as the fire timer is greater than 0, and it's not on cooldown, shoot a fireball.
        if (fireTimer > 0 && fireBallCooldown <= 0)
        {
            GameObject fireBall = Instantiate(fireBallPrefab, fireBallSpawnLocation.transform.position, fireBallSpawnLocation.transform.rotation);
            fireBallCooldown = 1.0f;
        }
    }


    //==========================================================================
    //COLLISIONS
    //==========================================================================
    void OnCollisionEnter(Collision other)
    {
        // Reset the jump when the player hits the ground (or similar).
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Platform") && !canJump || other.gameObject.CompareTag("MovingPlatform") && !canJump)
        {
            canJump = true;
        }
    }
    
    //==========================================================================
    //SCORE & DEATH
    //==========================================================================

    public void IncreaseScore(int increaseAmount)
    {
        // Increment the score by an inputted amount and re-set the score text.
        if (increaseAmount > 0)
        {
            score += increaseAmount;
            Debug.Log("Score: " + score);
            SetScoreText();
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    // TODO: Add three lives, each death removes one and resets the level ?
    void RemoveLife()
    {
        // When the players health reaches 0, remove a life and reset the position/health of the player, when all three lives are gone, game over.
        lives -= 1;
        if (lives <= 0)
        {
            Debug.Log("Game Over");
        }
        else
        {
            transform.position = new Vector3(-8.67f, 0.0f, 0.0f);
            health = 3;
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    public void TakeDamage()
    {
        // Reduce the health by 1 and shrink the player by 0.2f, lower the position of the player at the same time to remove mutliple hits from a single collision.
        health--;
        transform.localScale = new Vector3(transform.localScale.x - 0.2f, transform.localScale.y - 0.2f, transform.localScale.z - 0.2f);
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        if (health <= 0)
        {
            Debug.Log("Player has died");
            RemoveLife();
        }
    }

    //==========================================================================
    //POWER UPS
    //==========================================================================

    public void ActivateFirePower()
    {
        fireTimer += 10.0f;
    }
}
