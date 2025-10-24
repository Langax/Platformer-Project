using UnityEngine;

public class Enemy3_AI : MonoBehaviour
{
    // Variable Declaration.
    public GameObject popOutEnemy;
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
        height = popOutEnemy.transform.position.y;
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
                if (!popOutEnemy.activeSelf)
                {
                    popOutEnemy.SetActive(true);
                }

                height += upSpeed;

                if (popOutEnemy.transform.position.y >= maxY)
                {
                    goingUp = false;
                }
            }
            else
            {
                height -= downSpeed;
                if (popOutEnemy.transform.position.y <= minY)
                {
                    goingUp = true;
                    popOutEnemy.SetActive(false);
                    popUpTimer = 0;
                }
            }

            // Apply the height to the object.
            popOutEnemy.transform.position = new Vector3(popOutEnemy.transform.position.x, height, popOutEnemy.transform.position.z);
        }
    }
}