using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    public float moveSpeed = 3f;
    private Transform player;
    private CoinDropper coinDropper;

    private void Awake()
    {
        coinDropper = GetComponent<CoinDropper>();
    }

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
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        coinDropper?.DropCoin(); // Drop coins if CoinDropper is attached
        Destroy(gameObject); // For simplicity, just destroy the enemy
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TakeDamage(10);
        }
    }
}