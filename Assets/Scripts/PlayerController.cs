using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // Variable declaration.
    private Rigidbody rb;
    private Vector3 movementDirection;
    private bool canJump = true;
    private float fireTimer = 0.0f;
    private float fireBallCooldown = 0;
    private int score;
    private int lives = 3;
    private int raycastLength = 10;
    private Vector3 platformCurrentPos;
    private Vector3 platformPreviousPos;
    private bool hit_i_frames = false;
    private GameObject camera;
    
    public int movementSpeed;
    public int jumpForce;
    public int health;
    public bool stoodOnPipe = false;
    public GameObject fireBallPrefab;
    public TextMeshProUGUI scoreText;
    public Transform fireBallSpawnLocation;


    void Start()
    {
        // Rigid body assignment.
        rb = GetComponent<Rigidbody>();
        camera = GameObject.Find("camera");

        score = 0;
    }

    void Update()
    {
        // Keep the rotation fixed.
        transform.rotation = new Quaternion(0, camera.transform.rotation.y, 0, 1);

        DoRayCast();
 
    }
    
    //==========================================================================
    //MOVEMENT & INPUTS
    //==========================================================================
    void FixedUpdate()
    {
        // Apply the movement direction to the rigidbody (Normalized by deltaTime) as linearVelocity.
        if (camera.transform.rotation.y == 0)
        {
            rb.linearVelocity = new Vector3(movementDirection.x * (movementSpeed * Time.deltaTime), rb.linearVelocity.y, 0);
        }
        // If the camera has turned, move along the Z axis instead.
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, movementDirection.x * (movementSpeed * Time.deltaTime));
        }


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
        movementDirection = context.ReadValue<Vector3>();
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
            //gameObject.transform.position = bossRoomSpawnLocation.position;
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
    // Millie here, commented out the code and just made it restart the level if you run out of HP instead of a life system

    //void restart_level()
    //{
    //    // When the players health reaches 0, remove a life and reset the position/health of the player, when all three lives are gone, game over.
    //    lives -= 1;
    //    if (lives <= 0)
    //    {
    //        Debug.Log("Game Over");
    //    }
    //    else
    //    {
    //        transform.position = new Vector3(-8.67f, 0.0f, 0.0f);
    //        health = 3;
    //        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    //    }
    //}

    public void TakeDamage()
    {
        // Basically I frames means no hit! Simple stuff
        if (hit_i_frames == false)
        {
            hit_i_frames = true;
            Debug.Log("Player damaged!");
            health--;
            // Removed size and position changes due to bugs and redundencies
            if (health <= 0)
            {
                Debug.Log("Player has died");
                //RemoveLife();
                hit_i_frames = false; // If the co-routine was above it would start playing the I frame animation before the level reloaded
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                StartCoroutine(I_Frames()); // Starts the wait and I frame animation
            }
        }
    }

    IEnumerator  I_Frames() // Holds the I frames for a set duration, this is a co routine basically meaning it runs ALONGSIDE the code it's attached to :3
    {
        MeshRenderer mR = GetComponent<MeshRenderer>(); // This stuff took me awhile to figure out, still not 100% on it all but it'll do

        for (float i = 0; i <= 12; i++) // Waits a total of 3 seconds, 12 times but with a 0.25 second wait between each
        {
            if (i % 2 == 0) // Even
            {
                GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            }
            else // Odd
            {
                GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 0f, 0.5f);
            }
            yield return new WaitForSeconds(0.25f); // Tells the code to wait
        }
        
        hit_i_frames = false;
     }

    //==========================================================================
    //POWER UPS
    //==========================================================================

    public void ActivateFirePower()
    {
        fireTimer += 10.0f;
    }


    //==========================================================================
    //RAY-CASTING
    //==========================================================================

    private int DoRayCast()
    {
        // Create the ray cast
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up);
        bool rayHit = Physics.Raycast(ray, out hit, raycastLength);

        if (rayHit)
        {
            // When it detects a Moving platform, update the position variables and obtain the delta, then apply that to the player to have the player move with the platform
            if (hit.collider.CompareTag("MovingPlatform"))
            {
                platformPreviousPos = platformCurrentPos;
                platformCurrentPos = hit.collider.transform.position;

                
                Vector3 platformMovementDelta = platformCurrentPos - platformPreviousPos;
                
                // Discard values that are too high to avoid buggy teleporting
                if (platformMovementDelta.x <= 0.07 && platformMovementDelta.x >= -0.07)
                {
                    transform.position += platformMovementDelta;
                }

                return 1;
            }
        }

        return 0;
    }
    
}
