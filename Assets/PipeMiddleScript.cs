using UnityEngine;

public class PipeMiddleScript : MonoBehaviour
{
  public LogicScript logic; // Reference to the LogicScript component
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    if (logic == null)
    {
      Debug.LogError("LogicScript not found! Make sure it is attached to a GameObject with the tag 'Logic'.");
    }
  }

  // Update is called once per frame
  void Update()
  {

  }
    
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.layer == 3 && logic != null && logic.birdIsAlive) // Assuming layer 3 is the bird's layer
    {
      logic.AddScore();
    }
     // Call the AddScore method from LogicScript
  }
  
}
