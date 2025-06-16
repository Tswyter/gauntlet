using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public DungeonGenerator dungeonGenerator;

    private int playerCoins = 0;
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
    }

    private void Start()
    {
        dungeonGenerator.GenerateDungeon();
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + playerCoins.ToString();
        }
    }
}