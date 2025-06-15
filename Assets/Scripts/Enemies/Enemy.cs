using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    p[ublic float moveSpeed = 3f;
    pirvate Transform player;]

    private void Start()
    {
      GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        currentHealth = maxHealth;
    }

    void Update() {
      if (player == null) return;

      Vector3 direction = (player.position - Transform.position).normalized;
      transform.position += direction * moveSpeed * Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy took damage: " + damage + " Current Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle death logic, e.g., play animation, drop loot, etc.
        Destroy(gameObject); // For simplicity, just destroy the enemy
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.compareTag("Player")) {
        Debug.Log("Enemy collided with player!");
        Destroy(gameObject); // Destroy enemy on collision with player
       }
    }
}