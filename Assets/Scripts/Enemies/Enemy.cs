using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    public float moveSpeed = 3f;
    private Rigidbody rb;
    private Transform player;
    private CoinDropper coinDropper;

    private void Awake()
    {
        coinDropper = GetComponent<CoinDropper>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            transform.LookAt(player);
        }
        currentHealth = maxHealth;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f;
        Vector3 move = direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + move);
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
            other.GetComponent<Player>()?.TakeDamage(1); // Assuming Player has a TakeDamage method
        }
    }
}