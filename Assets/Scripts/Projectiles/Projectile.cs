using UnityEngine;

public class Projectile : MonoBehaviour
{
  public float speed = 10f;
  public float lifetime = 5f;
  public int damage = 1;

  private Vector3 direction;

  public void Initialize(Vector3 shootDirection)
  {
    direction = shootDirection.normalized;
    Destroy(gameObject, lifetime); // Destroy after lifetime
  }

  private void Update()
  {
    transform.position += direction * speed * Time.deltaTime;
  }

  public void OnTriggerEnter(Collider other)
  {
    Spawner spawner = other.GetComponent<Spawner>();
    Enemy enemy = other.GetComponent<Enemy>();
    if (enemy == null && spawner == null) return; // Ignore if neither is hit
    if (enemy != null)
    {
      enemy.TakeDamage(damage);
      Destroy(gameObject); // Destroy projectile on hit
      return;
    }

    if (spawner == null) return;

    spawner.TakeDamage(damage);
    Destroy(gameObject); // Destroy projectile on hit
  }
}