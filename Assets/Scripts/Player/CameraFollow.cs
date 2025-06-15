using UnityEngine;

public class CameraFollow : MonoBehaviour {
  public Transform target;
  private Vector3 offset = new Vector3(-10f, 10f, -10f);

  private void LateUpdate() {
    if (target == null) return;
    transform.position = target.position + offset;
    transform.LookAt(target);
  }
}