using UnityEngine;
using System.Collections;
using UnityEngine.TextCore.Text;

public class PlayerMovement : MonoBehaviour
{
  [Header("Movement Settings")]
  private CharacterController characterController;
  [SerializeField] private float gravity = -9.81f; // Gravity value
  [SerializeField] private float verticalVelocity = 0f; // Jump height
  [SerializeField] private float terminalVelocity = -20f; // Jump height

  public float moveSpeed = 5f;
  public float acceleration = 25f;

  private Vector3 currentVelocity = Vector3.zero;

  [Header("Projectile Settings")]
  public GameObject projectilePrefab;
  public Transform projectileSpawnPoint;
  public float projectileSpeed = 10f;

  private void Awake()
  {
    characterController = GetComponent<CharacterController>();
  }

  private void Update()
  {
    HandleMovement();
    FaceMouse();

    if (Input.GetMouseButtonDown(0))
    {
      ShootProjectile();
    }

    if (Input.GetKeyDown(KeyCode.T))
    {
      SuckUpCollectibles();
    }
  }

  private void HandleMovement()
  {
    float h = Input.GetAxisRaw("Horizontal");
    float v = Input.GetAxisRaw("Vertical");

    Vector3 input = new Vector3(h, 0, v).normalized;
    Vector3 isometricInput = Quaternion.Euler(0, 45, 0) * input;

    // Smooth velocity toward target input
    currentVelocity = Vector3.Lerp(currentVelocity, isometricInput * moveSpeed, acceleration * Time.deltaTime);

    // Apply velocity
    if (characterController.isGrounded && verticalVelocity < 0f)
    {
      verticalVelocity = -1f; // Reset vertical velocity when grounded
    }
    else
    {
      verticalVelocity += gravity * Time.deltaTime; // Apply gravity
      verticalVelocity = Mathf.Max(verticalVelocity, terminalVelocity); // Clamp to terminal velocity
    }

    Vector3 velocityWithGravity = currentVelocity;
    velocityWithGravity.y = verticalVelocity; // Add vertical velocity for gravity
    characterController.Move(velocityWithGravity * Time.deltaTime);
  }

  private void ShootProjectile()
  {
    if (projectilePrefab == null) return;

    Vector3 spawnPos = projectileSpawnPoint != null
        ? projectileSpawnPoint.position
        : transform.position + transform.forward * 0.5f;

    GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

    Projectile projectile = proj.GetComponent<Projectile>();
    if (projectile != null)
    {
      projectile.Initialize(transform.forward * projectileSpeed);
    }
  }

  private void FaceMouse()
  {
    // Create a plane at the player's position, parallel to the ground (XZ plane)
    Plane groundPlane = new Plane(Vector3.up, transform.position);

    // Cast a ray from the camera to the mouse position
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    // Check if the ray intersects the ground plane
    if (groundPlane.Raycast(ray, out float distance))
    {
      // Get the point of intersection
      Vector3 lookPos = ray.GetPoint(distance);

      // Calculate the direction to look at
      Vector3 direction = lookPos - transform.position;
      direction.y = 0f; // Keep the rotation level on the Y-axis

      if (direction.sqrMagnitude > 0.01f) // Avoid zero vector
      {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
      }
    }
  }

  private void SuckUpCollectibles()
  {
    GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
    foreach (GameObject coin in coins)
    {
      StartCoroutine(MoveCoinToPlayer(coin));
    }
  }

  private IEnumerator MoveCoinToPlayer(GameObject coin)
  {
    float speed = 5f; // Adjust the speed as needed

    while (coin != null && Vector3.Distance(coin.transform.position, transform.position) > 0.1f)
    {
      // Move the coin toward the player's position
      coin.transform.position = Vector3.MoveTowards(
          coin.transform.position,
          transform.position,
          speed * Time.deltaTime
      );

      yield return null; // Wait for the next frame
    }

    // Once the coin reaches the player, destroy it or trigger collection logic
    if (coin != null)
    {
      Destroy(coin);

      GameManager gameManager = FindObjectOfType<GameManager>();
      if (gameManager != null)
      {
        gameManager.AddCoins(1); // Assuming 1 coin value per pickup
      }
    }
  }
}