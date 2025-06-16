using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
  public static LevelManager Instance;

  [Header("Player Settings")]
  public GameObject playerPrefab; // Assign the player prefab in the Unity Editor

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

  private void Start()
  {
    Debug.Log("LevelManager Start called.");
    Scene currentScene = SceneManager.GetActiveScene();
    OnSceneLoaded(currentScene, LoadSceneMode.Single);
  }

  private void OnEnable()
  {
    SceneManager.sceneLoaded += OnSceneLoaded;
  }

  private void OnDisable()
  {
    SceneManager.sceneLoaded -= OnSceneLoaded;
  }

  public void RegisterSpawner(Spawner spawner)
  {
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
    StartCoroutine(GoToShopAfterDelay());
  }

  private System.Collections.IEnumerator GoToShopAfterDelay()
  {
    yield return new WaitForSeconds(2f);
    SceneManager.LoadScene("ShopWorld");
  }

  private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {
    Debug.Log($"Scene Loaded: {scene.name}");

    Transform spawnPoint = GameObject.FindWithTag("PlayerSpawn")?.transform;

    Debug.Log("Spawn Point:");

    if (spawnPoint != null)
    {
      Debug.Log("Spawn point found." + spawnPoint.position);

      GameObject player = GameObject.FindWithTag("Player");
      CharacterController characterController = player?.GetComponent<CharacterController>();

      if (player == null)
      {
        Debug.Log("Player not found. Instantiating player...");
        // Instantiate the player if they don't exist in the scene
        player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        player.tag = "Player";
        characterController = player.GetComponent<CharacterController>();
      }
      characterController.Move(spawnPoint.position - player.transform.position);
    }
    else
    {
      Debug.LogWarning("PlayerSpawn not found in the scene!");
    }
  }
}