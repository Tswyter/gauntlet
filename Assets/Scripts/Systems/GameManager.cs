using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int playerCoins = 0;
    private int difficultyLevel = 1;

    [SerializeField] private TextMeshProUGUI coinText;

    public void AddCoins(int amount)
    {
        playerCoins += amount;
        UpdateCoinUI();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (SceneManager.GetActiveScene().name == "Bootstrap")
        {
            SceneManager.LoadScene("DungeonWorld");
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        DungeonGenerator dungeonGenerator = FindObjectOfType<DungeonGenerator>();
        if (dungeonGenerator != null)
        {
            dungeonGenerator.GenerateDungeon();
        }

        GameObject player = GameObject.FindWithTag("Player");
        Transform spawnPoint = GameObject.Find("PlayerSpawn")?.transform;
        if (spawnPoint != null)
        {
            player.transform.position = spawnPoint.position;
        }
        else
        {
            player.transform.position = new Vector3(0, 0, 0); // Default spawn position if no spawn point found
        }

        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
        if (cameraFollow != null && player != null)
        {
            cameraFollow.target = player.transform;
        }
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + playerCoins.ToString();
        }
    }

    public void LevelComplete()
    {
        // Handle level completion logic here
        difficultyLevel++;
        Debug.Log("Level Complete! Current Difficulty Level: " + difficultyLevel);
        // You can also load the next scene or show a UI panel here
    }

    public void HandlePlayerDeath()
    {
        playerCoins = 0;
        difficultyLevel = 1;

        SceneManager.LoadScene("ShopWorld");
    }
}