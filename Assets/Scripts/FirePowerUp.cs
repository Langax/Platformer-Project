using UnityEngine;

public class FirePowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // When colliding with the Player, call the ActivateFirePower function on it and destroy self.
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Activating Fire power up");
            Destroy(gameObject);
            other.gameObject.GetComponent<PlayerController>().ActivateFirePower();
        }
    }
}
