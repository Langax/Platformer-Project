using UnityEngine;

public class CoinBoxInteraction : MonoBehaviour
{
    private bool canGiveCoin = true;
    public int score;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && canGiveCoin)
        {
            // Instantiate coin
            score++;
            Debug.Log("Score: " + score);
            canGiveCoin = false;
            Destroy(gameObject.GetComponent<BoxCollider>());
        }
    }
}
