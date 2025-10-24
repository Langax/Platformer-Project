using UnityEngine;

public class DownPipeController : MonoBehaviour
{
    private void OnCollisionStay(Collision other)
    {
        // Set stoodOnPipe to true whilst the player is stood on TOP of the pipe.
        if (other.gameObject.CompareTag("Player"))
        {
            // Ensure that the player is stood on TOP of the pipe.
            float positionDifference = transform.position.x - other.transform.position.x;
            if (positionDifference < 0.9 && positionDifference > -0.9)
            {
                other.gameObject.GetComponent<PlayerController>().stoodOnPipe = true;
            }
        }
    }
    private void OnCollisionExit(Collision other)
    {
        // When the player jumps off the DownPipe, reset stoodOnPipe to false.
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().stoodOnPipe = false;
        }
    }
}
