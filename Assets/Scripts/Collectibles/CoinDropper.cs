using UnityEngine;

public class CoinDropper : MonoBehaviour
{
  [Header("Drop Settings")]
  [SerializeField] private GameObject coinPrefab;
  [SerializeField] private Transform coinSpawnPoint; // Optional

  [Header("Force Settings")]
  [SerializeField] private float popUpForce = 4f;
  [SerializeField] private float randomSpread = 1f;

  private void Awake()
  {
    // If no spawn point is set, default to this object's position
    if (coinSpawnPoint == null)
    {
      coinSpawnPoint = transform;
    }
  }

  public void DropCoin()
  {
    if (coinPrefab == null) return;

    // Spawn the coin
    GameObject coin = Instantiate(
        coinPrefab,
        coinSpawnPoint.position + new Vector3(Random.Range(0f, 1f), 0, Random.Range(0f, 1f)),
        Quaternion.identity
    );

    // Add bounce/pop force
    Rigidbody rb = coin.GetComponent<Rigidbody>();
    if (rb != null)
    {
      rb.useGravity = true;
      rb.isKinematic = false;
      Vector3 force = Vector3.up * popUpForce + Random.onUnitSphere * randomSpread;
      rb.AddForce(force, ForceMode.Impulse);
    }
  }
}
