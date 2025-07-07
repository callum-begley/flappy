using UnityEngine;

public class bird_script : MonoBehaviour
{

  public Rigidbody2D myRigidbody;
  public float flapStrength = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
    {
        
    }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            myRigidbody.linearVelocity = Vector2.up * flapStrength;
        }
        // Uncomment the line below to set a constant upward velocity
        //
    
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocity.x, 5f);
        // }
    }
}
