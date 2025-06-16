using UnityEngine;
using System.Collections.Generic;

public class DungeonGenerator : MonoBehaviour
{
  public static DungeonGenerator Instance { get; private set; }

  public GameObject floorTilePrefab;
  public GameObject enemySpawnerPrefab;

  public int width = 15;
  public int height = 15;

  public int maxSpanwers = 2;

  private List<Vector3> CreateFloorTiles()
  {
    List<Vector3> availablePositions = new List<Vector3>();
    for (int x = 0; x < width; x++)
    {
      for (int z = 0; z < height; z++)
      {
        Vector3 position = new Vector3(x, 0, z);
        Instantiate(floorTilePrefab, position, Quaternion.identity, transform);
        availablePositions.Add(position);
      }
    }

    return availablePositions;
  }

  private void CreateEnemySpawners(List<Vector3> availablePositions)
  {
    int spawnerCount = Random.Range(1, maxSpanwers);

    for (int i = 0; i < spawnerCount; i++)
    {
      int index = Random.Range(0, availablePositions.Count);
      Vector3 spawnPos = availablePositions[index];
      Instantiate(enemySpawnerPrefab, spawnPos + Vector3.up * 1f, Quaternion.identity, transform);
      availablePositions.RemoveAt(index);
    }
  }

  public void GenerateDungeon()
  {
    List<Vector3> availablePositions = CreateFloorTiles();
    CreateEnemySpawners(availablePositions);
  }
}