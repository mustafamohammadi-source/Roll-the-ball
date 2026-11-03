using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinPickup : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public int coins = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coin"))
        {
            coins += 1;
            coinText.text = " You have " + coins.ToString() + " coins ";

            other.gameObject.SetActive(false);

            Debug.Log("Coins: " + coins);
        }
    }
}