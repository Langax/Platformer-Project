using UnityEngine;

public class Enemy3_AI : MonoBehaviour
{
    // Variable Declaration.
    public Transform popOutEnemy;
    public float maxY = 1;
    public float minY = 0;

    private bool goingUp = true;
    private float height;

    private float popUpTimer = 0;
    private float popUpDelay = 5;
    private float movementDelay = 0.10f;
    private float movementTimer = 0;

    void Start()
    {
        height = popOutEnemy.position.y;
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
                    height += 0.01f;
                    if (popOutEnemy.position.y >= maxY)
                    {
                        goingUp = false;
                    }
                }
                else
                {
                    height -= 0.01f;
                    if (popOutEnemy.position.y <= minY)
                    {
                        goingUp = true;
                        popUpTimer = 0;
                    }
                }
            }

            // Apply the height to the object before waiting for the movementDelay.
            popOutEnemy.position = new Vector3(popOutEnemy.position.x, height, popOutEnemy.position.z);
        }
    }
}