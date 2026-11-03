using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coins = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coin"))
        {
            coins += 1;

            // Gör coinen osynlig
            other.gameObject.SetActive(false);

            Debug.Log("Coins: " + coins);
        }
    }
}