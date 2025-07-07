using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
  public GameObject pipe; // The prefab for the pipe
  public float spawnRate = 2f; // Time interval between spawns
  private float timer = 0; // Time when the next pipe will spawn

  public float heightOffset = 10; // Height at which pipes will be spawned


  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    spawnPipe();
  }

  // Update is called once per frame
  void Update()
  {
    if (timer < spawnRate)
    {
      timer += Time.deltaTime; // Increment the timer
    }
    else
    {
      spawnPipe();
      timer = 0; // Reset the timer after spawning a pipe
    }
  }
  
  void spawnPipe()
  {
    float lowestPoint = transform.position.y - heightOffset; // Calculate the lowest point for the pipe
    float highestPoint = transform.position.y + heightOffset; // Calculate the highest point for the pipe
    Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
  }
        
}
