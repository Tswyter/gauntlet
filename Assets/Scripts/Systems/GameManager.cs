using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public DungeonGenerator dungeonGenerator;

    private int playerCoins = 0;

    public void AddCoins(int amount)
    {
        playerCoins += amount;
        Debug.Log("Coins added: " + amount + ". Total coins: " + playerCoins);
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
    }

    private void Start()
    {
        dungeonGenerator.GenerateDungeon();
    }
}