using UnityEngine;

public class bird_script : MonoBehaviour
{

  public Rigidbody2D myRigidbody;

  public AudioClip flapSound; // Reference to the flap sound GameObject
                            // The Rigidbody2D component for the bird

  public LogicScript logic; // Reference to the LogicScript component
                            // The strength of the flap when the space key is pressed
  public float flapStrength = 10f;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space) == true && logic.birdIsAlive)
    {
      // Check if the bird is alive before allowing it to flap
      myRigidbody.linearVelocity = Vector2.up * flapStrength; // Apply an upward force to the bird
      flapSound = Resources.Load<AudioClip>("flap"); // Load the flap sound from Resources
      AudioSource.PlayClipAtPoint(flapSound, transform.position); // Play the flap sound   
    }
    if (transform.position.y < -13) // Check if the bird has fallen below a certain point
    {
      logic.birdIsAlive = false; // Set the bird's alive status to false
      logic.GameOver(); // Call the GameOver method from LogicScript
    }
    if (!logic.birdIsAlive)
    {
      myRigidbody.linearVelocity = Vector2.zero; // Stop the bird's movement if it is not alive
      transform.position = transform.position + Vector3.left * logic.moveSpeed * Time.deltaTime;
      myRigidbody.gravityScale = 0; // Disable gravity to stop the bird from falling
      myRigidbody.bodyType = RigidbodyType2D.Kinematic; // Make the Rigidbody kinematic to stop physics interactions
    }
    
  }
  private void OnCollisionEnter2D(Collision2D collision)
  {
      logic.birdIsAlive = false; // Set the bird's alive status to false
      logic.GameOver(); // Call the GameOver method from LogicScript
  }
}
