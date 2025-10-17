using UnityEngine;

public class GoldCoinMovement : MonoBehaviour
{
    private float timeUntilDestroy = 0.8f;
    private float timeSinceSpawn;
    public int boostPower = 30;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(0.0f, boostPower, 0.0f);
    }

    void Update()
    {
        timeSinceSpawn += Time.deltaTime;

        if (timeSinceSpawn > timeUntilDestroy)
        {
            Destroy(gameObject);
        }
    }
}
