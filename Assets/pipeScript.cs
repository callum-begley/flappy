using UnityEngine;

public class pipeScript : MonoBehaviour
{

  public LogicScript logic; // Reference to the LogicScript component
  public float deadzone = -45; // Distance from the left edge of the screen to destroy the pipe
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
  }

  // Update is called once per frame
  void Update()
  {
    transform.position = transform.position + Vector3.left * logic.moveSpeed * Time.deltaTime;
    if (transform.position.x < deadzone) // Assuming -45 is the off-screen position
    {
      Destroy(gameObject); // Destroy the pipe when it goes off-screen
    }
  }
}