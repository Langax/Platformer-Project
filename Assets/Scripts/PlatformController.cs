using UnityEngine;

public class PlatformController : MonoBehaviour
{
    // Variable declaration.
    public float enableHeight = 0.0f;
    public GameObject player;

    void Update()
    {
        // Whenever the player is above the specified enableHeight enable the collision box and allow the player to land on it.
        if (player.transform.position.y >= enableHeight)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        // Otherwise, disable the collision box so the player can pass through it.
        else
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
