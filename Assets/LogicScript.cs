using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  public int playerScore = 0; // Player's score
  public Text scoreText; // UI Text to display the score

[ContextMenu("Add Score")]
  public void AddScore()
  {
    playerScore++; // Increment the score
    scoreText.text = playerScore.ToString(); // Update the UI text
  }
}
