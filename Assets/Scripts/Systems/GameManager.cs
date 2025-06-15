using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public DungeonGenerator dungeonGenerator;

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