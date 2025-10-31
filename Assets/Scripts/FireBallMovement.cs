using UnityEngine;

public class FireBallMovement : MonoBehaviour
{
    // Variable declaration.
    private Rigidbody rb;
    private float angle = 45;
    private int force = 8;
    private new GameObject camera;
    private GameObject player;
    
    void Start()
    {
        player = GameObject.Find("Player");
        camera = GameObject.Find("camera");
        rb = GetComponent<Rigidbody>();

        // Immediately add a force to the Fireball to push it away from the Player.
        // Applied in the direction that the player is facing/the world is rotated to (Currently limited to 2 directions).
        if (player.transform.rotation.y == 0)
        {
            rb.AddForce(200.0f, -190.0f, 0.0f);
        }
        else
        {
            rb.AddForce(0.0f, -190.0f, 200.0f);
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        // When the fireball hits the ground, reset its momentum and apply a 45-degree angle force.
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Platform"))
        {
            rb.linearVelocity = Vector3.zero;
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            if (camera.transform.rotation.y == 0)
            {
                 rotation = Quaternion.Euler(0, 0, -angle);
            }
            else
            {
                rotation = Quaternion.Euler(angle, 0, 0);
            }
            rb.AddForce(rotation * Vector2.up * force, ForceMode.Impulse);
        }
        // When the fireball hits the wall or a pipe, destroy it.
        else if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Pipe") || other.gameObject.CompareTag("DownPipe"))
        {
            Destroy(gameObject);
        }
        // When colliding with an enemy, destroy it and the fireball, then increase the score by 2.
        else if (other.gameObject.CompareTag("Enemy")){
            Destroy(gameObject);
            Destroy(other.gameObject);
            player.GetComponent<PlayerController>().IncreaseScore(2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // The PopUpEnemy is a trigger so it needs a separate section. Destroy it + self and increase score.
        if (other.gameObject.CompareTag("PopUpEnemy"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            player.GetComponent<PlayerController>().IncreaseScore(3);
        }
    }
}
