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
    
    void OnTriggerEnter(Collider other)
    {
        GameObject collided_object = other.gameObject;
        if (collided_object != player) { return; } // Stops the code if the player isn't the one hitting it
        player_script.health += 1;
        Destroy(gameObject);
    }// end collision
}
