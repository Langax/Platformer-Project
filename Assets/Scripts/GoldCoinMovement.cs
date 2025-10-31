using System.Collections;
using UnityEngine;

public class GoldCoinMovement : MonoBehaviour
{
    // Variable declaration.
    private float timeUntilDestroy = 0.5f;
    public int boostPower = 90;
    Rigidbody rb;
    
    IEnumerator Start()
    {
        // Assign the rigid body and apply an upward force to shoot the coin out of the box.
        rb = GetComponent<Rigidbody>();
        rb.AddForce(0.0f, boostPower, 0.0f);
        yield return StartCoroutine("DestroyCoin");
    }

    IEnumerator DestroyCoin()
    {
        // Coroutine that destroys the coin after timeUntilDestroy seconds have passed. (At the apex of the coin height)
        yield return new WaitForSeconds(timeUntilDestroy);

        Destroy(gameObject);
    }
}
