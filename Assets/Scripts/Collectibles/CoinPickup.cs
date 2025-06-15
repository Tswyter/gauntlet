using UnityEngine;

public class CoinPickup : MonoBehaviour
{
  public int coinValue = 1; // Value of the coin pickup

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      GameManager gameManager = FindObjectOfType<GameManager>();
      if (gameManager != null)
      {
        gameManager.AddCoins(coinValue); // Add coins to the player's total
        Debug.Log("Coin picked up! Value: " + coinValue);
      }

      Destroy(gameObject); // Destroy the coin pickup after collection
    }
  }
}