using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    public float moveSpeed = 3f;
    private Transform player;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            transform.LookAt(player);
        }
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(player);
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
        Debug.Log("OnTriggerEnter fired");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy collided with player!");
            TakeDamage(10);
        }
    }
}