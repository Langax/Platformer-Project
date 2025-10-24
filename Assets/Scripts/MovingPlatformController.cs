using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
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

        Debug.Log("Height minimum: " + heightMin);
    }

    void Update()
    {
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
        if (movingRight)
        {
            platformX += LRSpeed;
            if (platformX >= xMax)
            {
                movingRight = false;
            }
        }
        else
        {
            platformX -= LRSpeed;
            if (platformX <= xMin)
            {
                movingRight = true;
            }
        }
        transform.position = new Vector3(platformX, transform.position.y, transform.position.z);
    }

    void MoveUpAndDown()
    {
        if (movingUp)
        {
            platformY += UDSpeed;
            if (platformY >= heightMax)
            {
                movingUp = false;
            }
        }
        else
        {
            platformY -= UDSpeed;
            if (platformY <= heightMin)
            {
                movingUp = true;
            }
        }
        transform.position = new Vector3(transform.position.x, platformY, transform.position.z);
    }
}
