using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  public int playerScore = 0; // Player's score
  public Text scoreText; // UI Text to display the score

  public float moveSpeed = 5f; // Speed at which the pipes moves left

  public bool birdIsAlive = true; // Flag to check if the bird is alive
                            // The Rigidbody2D component for the bird

  public GameObject gameOverScreen; // Reference to the Game Over screen UI

  [ContextMenu("Add Score")]
  public void AddScore()
  {
    playerScore++; // Increment the score
    scoreText.text = playerScore.ToString(); // Update the UI text
    if (playerScore == 10) // Check if the score reaches a certain threshold
    {
      moveSpeed += 2f; // Increase the speed of the pipes
    }
    if (playerScore == 20) // Check if the score reaches a certain threshold
    {
      moveSpeed += 2f; // Increase the speed of the pipes
    }
    if (playerScore == 30) // Check if the score reaches a certain threshold
    {
      moveSpeed += 1f; // Increase the speed of the pipes
    }
    if (playerScore == 40) // Check if the score reaches a certain threshold
    {
      moveSpeed += 1f; // Increase the speed of the pipes
    }
    if (playerScore == 50) // Check if the score reaches a certain threshold
    {
      moveSpeed += 1f; // Increase the speed of the pipes
    }
  }

  public void RestartGame()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    playerScore = 0; // Reset the score
    scoreText.text = playerScore.ToString(); // Update the UI text
    // Additional logic to reset the game can be added here
  }
  
  public void GameOver()
  {
    // Logic for game over can be added here, such as showing a game over screen
    gameOverScreen.SetActive(true); // Show the Game Over screen
    Debug.Log("Game Over! Your score: " + playerScore);
  }
}
