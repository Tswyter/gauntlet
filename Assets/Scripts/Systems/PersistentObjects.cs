using UnityEngine;

public class PersistentObjects : MonoBehaviour
{
  public static PersistentObjects Instance;

  private void Awake()
  {
    DontDestroyOnLoad(gameObject);
  }

  // Add any persistent objects or methods here
}