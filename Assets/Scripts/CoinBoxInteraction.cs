using UnityEngine;

public class CoinBoxInteraction : MonoBehaviour
{
    private bool canGiveCoin = true;
    public int score;
    public GameObject goldCoinPrefab;
    public Transform coinSpawnPoint;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && canGiveCoin)
        {
            GameObject goldCoin = Instantiate(goldCoinPrefab, coinSpawnPoint.transform.position, coinSpawnPoint.transform.rotation);
            score++;
            Debug.Log("Score: " + score);
            canGiveCoin = false;
            Destroy(gameObject.GetComponent<BoxCollider>());
        }
    }
}
