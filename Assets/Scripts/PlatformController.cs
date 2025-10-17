using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float enableHeight = 0.0f;
    public GameObject player;

    void Update()
    {
        if (player.transform.position.y >= enableHeight)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
