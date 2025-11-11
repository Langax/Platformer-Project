using JetBrains.Annotations;
using NUnit.Framework.Constraints;
using UnityEngine;
using System.Collections;
using Unity.Mathematics;

public class bounce_npc : MonoBehaviour
{
    private bool freeze_movement;
    public bool inverse_move_direction = false;
    private PlayerController player_script;
    private Rigidbody rb;
    private GameObject player;
    private bool is_alive = true;
    private Rigidbody player_rb;
    private int movement_speed = 100; // This can also be seen as "move direction" as it only moves left and right

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        player = GameObject.Find("Player");
        player_rb = player.GetComponent<Rigidbody>();
        player_script = player.GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        if (inverse_move_direction == false)
        {
            if (!freeze_movement) // Moves the NPC, only if it's not told to freeze
            {
                rb.linearVelocity = new Vector2(movement_speed * Time.deltaTime, 0);
            }
            else
            {
                rb.linearVelocity = new Vector3(0,0,0);
            }
        }
        else
        {
             if (!freeze_movement) // Moves the NPC, only if it's not told to freeze
            {
                rb.linearVelocity = new Vector3(0,0,movement_speed * Time.deltaTime);
            }
            else
            {
                rb.linearVelocity = new Vector3(0,0,0);
            }
        }

        if (is_alive == false) // Has to be put into update look or it just falls over lol
        {
            transform.rotation = new quaternion(0, 0, 0, 0); // Yeah not sure why there's 4, resets the rotation for the squashed look
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (is_alive == false) { return; } // Stops code from running if the NPC is dead, stops the player from killing the same NPC twice ( it was possible at one point )
        GameObject collided_object = collision.gameObject;

        if (!collided_object.CompareTag("Player")) //Bounce off object
        {
            movement_speed = -movement_speed; // Basically just get the speed and turn it negative, or if it's negative it turns positive :3
        }
        else if(collided_object.CompareTag("Player")) // Otherwise we hit the player!
        {
            Vector3 player_velocity = player_rb.linearVelocity;

            if (player_velocity.y < -0.01 || player.transform.position.y > transform.position.y) // If the player is falling down, has to be -0.01 to avoid a weird bug found when at just 0
            {
                is_alive = false;
                player_script.IncreaseScore(1);
                movement_speed = 0; // Stops movement
                transform.position = new Vector3(transform.position.x,transform.position.y-(transform.localScale.y / 2),transform.position.z);
                player_rb.AddForce(0, player_script.jumpForce, 0); // Basically just does another jump using the players jump force!
                transform.localScale = new Vector3(transform.localScale.x / 2, transform.localScale.y / 3, transform.localScale.z / 2);
                StartCoroutine(npc_clear_up());
            }
            else // Otherwise damage player
            {
                player_script.TakeDamage(); // Player takes damage, scary!
            }
        }

        //////////////// COROUTINES!!!!!!!!!!

        IEnumerator  npc_clear_up()
        {
            yield return new WaitForSeconds(1); // waits 1 second
            Destroy(gameObject);
            Debug.Log("Player killed the NPC!");
        }
    }
}// end collision

