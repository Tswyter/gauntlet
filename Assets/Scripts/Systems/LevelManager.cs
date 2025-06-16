using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
  public static LevelManager Instance;
  private List<Spawner> activeSpawners = new List<Spawner>();

  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }
    Instance = this;
    DontDestroyOnLoad(gameObject);
  }

  public void RegisterSpawner(Spawner spawner)
  {
    Debug.Log("Registering spawner: " + spawner.name);
    if (!activeSpawners.Contains(spawner))
    {
      activeSpawners.Add(spawner);
    }
  }

  public void UnregisterSpawner(Spawner spawner)
  {
    if (activeSpawners.Contains(spawner))
    {
      activeSpawners.Remove(spawner);

      if (activeSpawners.Count == 0)
      {
        OnAllSpawnersDestroyed();
      }
    }
  }

  private void OnAllSpawnersDestroyed()
  {
    GameManager.Instance.LevelComplete();
    StartCoroutine(GoToShopAfterDelay()); // Start coroutine to go to shop after a delay
  }

  private System.Collections.IEnumerator GoToShopAfterDelay()
  {
    yield return new WaitForSeconds(2f); // Wait for 2 seconds before going to the shop
    SceneManager.LoadScene("ShopWorld"); // Load the shop scene
  }
}