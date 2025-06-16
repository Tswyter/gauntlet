using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonPortal : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      // Load the dungeon scene
      SceneManager.LoadScene("DungeonWorld");
    }
  }
}