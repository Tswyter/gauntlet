using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
  public int health = 100; // Player's health
  public int coins = 0; // Player's coin count

  private void Start()
  {
    // Initialize player state if needed
  }

  private void Update()
  {
    // Handle player input and movement here
  }

  public void TakeDamage(int damage)
  {
    Debug.Log($"Player took {damage} damage. Current health: {health - damage}");
    health -= damage;
    if (health <= 0)
    {
      Die();
    }
  }

  public void AddCoins(int amount)
  {
    coins += amount;
  }

  public void Die()
  {
    // Handle player death (e.g., respawn, game over)
    Debug.Log("Player has died.");
    // Optionally reset health and coins
    health = 100;
    coins = 0;
    SceneManager.LoadScene("ShopWorld");
  }
}