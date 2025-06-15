using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public float moveSpeed = 5f;
  public float acceleration = 25f;

  private Vector3 currentVelocity = Vector3.zero;


  public GameObject projectilePrefab;
  public Transform projectileSpawnPoint;
  public float projectileSpeed = 10f;

  private void Update()
  {
    float h = Input.GetAxisRaw("Horizontal");
    float v = Input.GetAxisRaw("Vertical");

    Vector3 input = new Vector3(h, 0, v).normalized;
    Vector3 isometricInput = Quaternion.Euler(0, 45, 0) * input;

    // Smooth velocity toward target input
    currentVelocity = Vector3.Lerp(currentVelocity, isometricInput * moveSpeed, acceleration * Time.deltaTime);

    // Apply velocity
    transform.position += currentVelocity * Time.deltaTime;

    FaceMouse();

    if (Input.GetMouseButtonDown(0)) {
      ShootProjectile();
    }
  }

  private void ShootProjectile() {
    Vector3 spawnPos = projectileSpawnPoint != null ? projectileSpawnPoint.position : transform.position + transform.forward * 0.5f;
    GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
    
    Projectile projectile = proj.GetComponent<Projectile>();
    if (projectile != null)
    {
        projectile.Initialize(transform.forward * projectileSpeed);
    }
  }

  private void FaceMouse() {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f)) {
      Vector3 lookPos = hitInfo.point - transform.position;
      lookPos.y = 0f; // Keep the y-axis level
      if (lookPos. sqrMagnitude > 0.01f) { // Avoid zero vector
        Quaternion targetRotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
      }
    }
  }
}