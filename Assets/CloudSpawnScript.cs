using UnityEngine;

public class CloudSpawnScript : MonoBehaviour
{
  public GameObject cloud; // The prefab for the cloud
  public float spawnRate = 3; // Time interval between spawns
  private float timer = 0f; // Time when the next cloud will spawn

  public float heightOffset = 10; // Height at which clouds will be spawned


  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    spawnCloud();
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
      spawnCloud();
      timer = 0; // Reset the timer after spawning a Cloud
    }
  }
  
  void spawnCloud()
  {
    float lowestPoint = 0; // Calculate the lowest point for the cloud
    float highestPoint = transform.position.y + heightOffset; // Calculate the highest point for the cloud

    // Instantiate the cloud
    GameObject spawnedCloud = Instantiate(cloud, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    // Randomize the scale of the cloud
    float randomScale = Random.Range(0.5f, 1.5f); // Adjust min/max as desired
    spawnedCloud.transform.localScale = new Vector3(randomScale, randomScale, 1f);
  }
        
}
