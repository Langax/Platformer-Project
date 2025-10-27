using JetBrains.Annotations;
using NUnit.Framework.Constraints;
using UnityEngine;

public class bounce_npc : MonoBehaviour
{
    private Rigidbody rb;
    private int movement_speed = 100; // This can also be seen as "move direction" as it only moves left and right

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movement_speed * Time.deltaTime, 0);
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        GameObject collided_object = collision.gameObject;
        if (collided_object.name != "Player") //Bounce off object
        {
            Debug.Log("Hit a wall...");
            movement_speed = -movement_speed;
        }
        else // Otherwise we hit the player!
        {
            Debug.Log("Yay! The player!");

            Rigidbody player_rb = collided_object.GetComponent<Rigidbody>();
            Vector3 player_velocity = player_rb.linearVelocity;

            if (player_velocity.y < -0.01) // If the player is falling down, has to be -0.01 to avoid a weird bug found when at just 0
            {
                Destroy(gameObject);
            }
            else // Otherwise damage player
            {
                //Damage code grrr
            }
        }
        

    }// end collision
}
