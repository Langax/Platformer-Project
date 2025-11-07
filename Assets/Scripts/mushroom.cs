using UnityEngine;


public class mushroom : MonoBehaviour
{

    private GameObject player;
    private PlayerController player_script;

    void Start()
    {
        player = GameObject.Find("Player");
        player_script = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnCollisionEnter(Collision collision)
    {
        GameObject collided_object = collision.gameObject;
        Debug.Log("HIT!");
        if (collided_object != player) { return; } // Stops the code if the player isn't the one hitting it
        Debug.Log("HIT!");
        player_script.health += 1;
        Destroy(gameObject);
    }// end collision
}
