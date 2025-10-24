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
    private float movementDelay = 0.10f;
    private float movementTimer = 0;

    void Start()
    {
        height = popOutEnemy.transform.position.y;
    }

    void Update()
    {
        // Increment the timers each Update.
        popUpTimer += Time.deltaTime;
        movementTimer += Time.deltaTime;

        // Cooldown of 5 seconds between pop up.
        if (popUpTimer >= popUpDelay)
        {
            // Slight cooldown before each movement increment to ensure a smooth rise/fall.
            if (movementTimer >= movementDelay)
            {
                // When goingUp is true, increase the height until the max, then begin to decrease the height until minimum.
                if (goingUp)
                {
                    if (!popOutEnemy.activeSelf)
                    {
                        popOutEnemy.SetActive(true);
                    }

                    height += 0.0006f;

                    if (popOutEnemy.transform.position.y >= maxY)
                    {
                        goingUp = false;
                    }
                }
                else
                {
                    height -= 0.001f;
                    if (popOutEnemy.transform.position.y <= minY)
                    {
                        goingUp = true;
                        popOutEnemy.SetActive(false);
                        popUpTimer = 0;
                    }
                }
            }

            // Apply the height to the object before waiting for the movementDelay.
            popOutEnemy.transform.position = new Vector3(popOutEnemy.transform.position.x, height, popOutEnemy.transform.position.z);
        }
    }
}