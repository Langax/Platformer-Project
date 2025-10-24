using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    // Variable declaration.
    public bool activateOnStep = false;
    public bool movingLeftAndRight = false;
    public float heightMax;
    public float heightMin;
    public float xMax;
    public float xMin;
    public float LRSpeed;
    public float UDSpeed;

    private Rigidbody rb;
    private bool movingRight = true;
    private bool movingUp = true;
    private float platformX;
    private float platformY;

    void Start()
    {
        platformX = transform.position.x;
        platformY = transform.position.y;
    }

    void Update()
    {
        // Each update move left/right or up/down depending on which is specified in the editor. If it's only activate on step, the logic is moved to OnCollisionStay.
        if (!activateOnStep)
        {
            if (movingLeftAndRight)
            {
                MoveLeftAndRight();
            }
            else
            {
                MoveUpAndDown();
            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        // Whilst the player is stood on the platform, move it left/right or up/down.
        if (activateOnStep)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (movingLeftAndRight)
                {
                    MoveLeftAndRight();
                }
                else
                {
                    MoveUpAndDown();
                }
            }
        }
    }

    void MoveLeftAndRight()
    {
        // Start by moving right at a rate of LRSpeed, when it reaches xMax start to move left instead.
        if (movingRight)
        {
            platformX += (LRSpeed * Time.deltaTime);
            if (platformX >= xMax)
            {
                movingRight = false;
            }
        }
        else
        {
            platformX -= (LRSpeed * Time.deltaTime);
            if (platformX <= xMin)
            {
                movingRight = true;
            }
        }
        // Apply the new position.
        transform.position = new Vector3(platformX, transform.position.y, transform.position.z);
    }

    void MoveUpAndDown()
    {
        // Start by moving up at a rate of UDSpeed, when it reaches heightMax, start moving down instead.
        if (movingUp)
        {
            platformY += (UDSpeed * Time.deltaTime);
            if (platformY >= heightMax)
            {
                movingUp = false;
            }
        }
        else
        {
            platformY -= (UDSpeed * Time.deltaTime);
            if (platformY <= heightMin)
            {
                movingUp = true;
            }
        }
        // Apply the new position.
        transform.position = new Vector3(transform.position.x, platformY, transform.position.z);
    }
}
