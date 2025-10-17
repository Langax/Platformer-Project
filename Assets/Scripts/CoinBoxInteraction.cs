using UnityEngine;

public class CoinBoxInteraction : MonoBehaviour
{
    // Variable declaration.
    private bool canGiveCoin = true;
    public int score;
    public GameObject goldCoinPrefab;
    public Transform coinSpawnPoint;

    void OnCollisionEnter(Collision other)
    {
        // When colliding with the player, if it hasn't been hit already then create a GoldCoin, increase the score and Destroy the collider. 
        if (other.gameObject.CompareTag("Player") && canGiveCoin)
        {
            GameObject goldCoin = Instantiate(goldCoinPrefab, coinSpawnPoint.transform.position, coinSpawnPoint.transform.rotation);
            score++;
            canGiveCoin = false;
            Destroy(gameObject.GetComponent<BoxCollider>());
        }
    }
}
