using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
  public int maxHealth = 10;
  private int currentHealth;

  public GameObject enemyPrefab; // Prefab to spawn
  public float spawnInterval = 5f; // Time between spawns
  private float spawnTimer;

  private IEnumerator Start()
  {
    while (LevelManager.Instance == null)
    {
      // Wait for LevelManager to be initialized
      yield return null;
    }
    LevelManager.Instance.RegisterSpawner(this);
    currentHealth = maxHealth;
    spawnTimer = spawnInterval; // Initialize the spawn timer
    StartCoroutine(SpawnLoop());
  }

  IEnumerator SpawnLoop()
  {
    yield return new WaitForSeconds(Random.Range(0.5f, 3f));

    while (true)
    {
      SpawnEnemy();
      float delay = Random.Range(0.5f, 3f);
      yield return new WaitForSeconds(delay);
    }
  }

  private void Update()
  {
    // Update the spawn timer and spawn enemies if it's time
    spawnTimer -= Time.deltaTime;
    if (spawnTimer <= 0f)
    {
      SpawnEnemy();
      spawnTimer = spawnInterval; // Reset the timer
    }
  }

  private void SpawnEnemy()
  {
    Vector3 spawnPos = transform.position + Vector3.up * 0.5f;
    Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
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
    // Handle death logic, e.g., play animation, drop loot, etc.
    LevelManager.Instance.UnregisterSpawner(this);
    Destroy(gameObject); // For simplicity, just destroy the spawner
  }
}