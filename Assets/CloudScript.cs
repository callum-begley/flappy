using UnityEngine;

public class CloudScript : MonoBehaviour
{

  public float moveSpeed = 4f; // Speed at which the pipe moves left
  public float deadzone = -45; // Distance from the left edge of the screen to destroy the pipe
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    transform.position = transform.position + Vector3.left * moveSpeed * Time.deltaTime;
    if (transform.position.x < deadzone) // Assuming -45 is the off-screen position
    {
      Destroy(gameObject); // Destroy the pipe when it goes off-screen
    }
  }
}