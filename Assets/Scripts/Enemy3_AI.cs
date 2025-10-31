using UnityEngine;

public class Enemy3_AI : MonoBehaviour
{
    // Variable Declaration.
    public float maxY = 1;
    public float minY = -0.5f;

    private bool goingUp = true;
    private float height;

    private float popUpTimer = 0;
    private float popUpDelay = 5;
    private float upSpeed = 0.0006f;
    private float downSpeed = 0.001f;

    void Start()
    {
        height = transform.position.y;
    }

    void Update()
    {
        // Increment the timers each Update.
        popUpTimer += Time.deltaTime;
        // Cooldown of 5 seconds between pop up.
        if (popUpTimer >= popUpDelay)
        {
            // When goingUp is true, increase the height until the max, then begin to decrease the height until minimum, if it is disabled, enable it first.
            // Disable it when it reaches the minimum height, to avoid collision issues.
            if (goingUp)
            {
                if (!gameObject.activeSelf)
                {
                    gameObject.SetActive(true);
                }

                height += upSpeed;
                if (transform.position.y >= maxY)
                {
                    goingUp = false;
                }
            }
            else
            {
                height -= downSpeed;
                if (transform.position.y <= minY)
                {
                    goingUp = true;
                    // TODO: Fix collision here, disabling it no longer works as the script is on the enemy itself not the parent object.
                    // gameObject.SetActive(false);
                    popUpTimer = 0;
                }
            }

            // Apply the height to the object.
            transform.position = new Vector3(transform.position.x, height, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // When colliding with the player, call its TakeDamage() function.
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }
}