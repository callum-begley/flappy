using System.Collections;
using UnityEngine;
// No need for System.Collections for instant flip, but keeping it if you add other coroutines

public class SimpleFlipper : MonoBehaviour
{
  [Header("Flip Settings")]
    [Tooltip("The duration of the flip animation in seconds.")]
    public float flipDuration = 0.5f;

    [Tooltip("The axis around which the object will rotate 180 degrees for the invert. " +
             "Set to Vector3.right (1,0,0) for vertical invert (somersault).")]
    public Vector3 flipAxis = Vector3.right; // Default to Vector3.right for vertical invert

    [Header("Position Change Settings")]
    [Tooltip("The target X position the object will move to during the flip.")]
    public float targetPositionX = -1.3f; // User-specified target X
    [Tooltip("The target Y position the object will move to during the flip.")]
    public float targetPositionY = 0.76f; // User-specified target Y

    private bool isFlipping = false; // To prevent multiple flips at once
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer

    void Awake()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SimpleFlipper: No SpriteRenderer found on this GameObject. Please assign one or ensure it's on the same GameObject.", this);
            enabled = false; // Disable script if no SpriteRenderer is found
        }
    }

  // --- Example Usage (Optional - for quick testing) ---
  // You can remove or comment out this Update method in your final game
  // and call InvertSpriteVertically() from another script (e.g., when a player jumps).
  private LogicScript logic;

  void Start()
  {
    logic = FindObjectOfType<LogicScript>();
    if (logic == null)
    {
      Debug.LogError("SimpleFlipper: LogicScript not found in the scene.");
    }
  }

  public void StartFlip()
    {
        if (!isFlipping) // Only start flip if not already flipping
        {
            StartCoroutine(FlipAndMoveCoroutine());
        }
    }

  void Update()
  {
    if (logic != null && logic.birdIsAlive)
    {
      StartFlip();
    }
  }
    
    private IEnumerator FlipAndMoveCoroutine()
    {
        isFlipping = true; // Set flag to true

        // Store initial rotation and position
        Quaternion startRotation = transform.rotation;
        Vector3 startPosition = transform.position;

        // Calculate target rotation (180 degrees around the specified axis relative to start)
        Quaternion targetRotation = startRotation * Quaternion.Euler(flipAxis * 180f);

        // Define target position (keeping Z constant)
        Vector3 targetPosition = new Vector3(targetPositionX, targetPositionY, startPosition.z);

        float elapsedTime = 0f;

        while (elapsedTime < flipDuration)
        {
            float t = elapsedTime / flipDuration; // Normalized time (0 to 1)

            // Smoothly interpolate rotation using Spherical Linear Interpolation (Slerp)
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            // Smoothly interpolate position using Linear Interpolation (Lerp)
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime; // Increment elapsed time
            yield return null; // Wait for the next frame
        }

        // Ensure the rotation and position are exactly at the target values at the end
        transform.rotation = targetRotation;
        transform.position = targetPosition;

        isFlipping = false; // Reset flag
    }
}