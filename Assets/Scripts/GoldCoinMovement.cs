using UnityEngine;

public class GoldCoinMovement : MonoBehaviour
{
    // Variable declaration.
    private float timeUntilDestroy = 0.8f;
    private float timeSinceSpawn;
    public int boostPower = 30;
    Rigidbody rb;
    
    void Start()
    {
        // Assign the rigid body and apply an upward force to shoot the coin out of the box.
        rb = GetComponent<Rigidbody>();
        rb.AddForce(0.0f, boostPower, 0.0f);
    }

    void Update()
    {
        // After the coin reaches its apex, destroy it.
        timeSinceSpawn += Time.deltaTime;

        if (timeSinceSpawn > timeUntilDestroy)
        {
            Destroy(gameObject);
        }
    }
}
