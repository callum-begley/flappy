using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  public int playerScore = 0; // Player's score
  public Text scoreText; // UI Text to display the score

  public bool birdIsAlive = true; // Flag to check if the bird is alive
                            // The Rigidbody2D component for the bird

  public GameObject gameOverScreen; // Reference to the Game Over screen UI

  [ContextMenu("Add Score")]
  public void AddScore()
  {
    playerScore++; // Increment the score
    scoreText.text = playerScore.ToString(); // Update the UI text
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
