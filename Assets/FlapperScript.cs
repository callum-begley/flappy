using UnityEngine;
using System.Collections; // Required for Coroutines

public class SimpleFlipper : MonoBehaviour
{
    [Header("Flip Settings")]
    [Tooltip("The duration of a single flap (half-cycle) animation in seconds. Total animation time is 2 * flipDuration.")]
    public float flipDuration = 0.15f; // Adjusted for a single flap, total will be double

    [Tooltip("The interval between the start of each full flap cycle in seconds.")]
    public float flipInterval = 0.3f; // Time between the start of one full flap cycle and the next

    [Tooltip("The axis around which the object will rotate 180 degrees for the invert. " +
             "Set to Vector3.right (1,0,0) for vertical invert (somersault).")]
    public Vector3 flipAxis = Vector3.right; // Default to Vector3.right for vertical invert

    [Header("Local Position Offset Settings")]
    [Tooltip("The target LOCAL X position OFFSET relative to the parent at the peak of the flap.")]
    public float targetLocalOffsetX = -1.3f; // User-specified target LOCAL X offset
    [Tooltip("The target LOCAL Y position OFFSET relative to the parent at the peak of the flap.")]
    public float targetLocalOffsetY = 0.76f; // User-specified target LOCAL Y offset

    private bool isFlipping = false; // To prevent multiple flips at once
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    private LogicScript logic; // Reference to the LogicScript
    private float nextFlipTime = 0f; // Timer for the next flip

    void Awake()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            enabled = false; // Disable script if no SpriteRenderer is found
        }
    }

    void Start()
    {
        logic = FindFirstObjectByType<LogicScript>();
        if (logic == null)
        {
            Debug.LogError("SimpleFlipper: LogicScript not found in the scene.");
        }
        // Initialize nextFlipTime so the first flip can happen immediately or after the first interval
        nextFlipTime = Time.time;
    }

    /// <summary>
    /// Call this method to start a single smooth flapping animation cycle.
    /// </summary>
    public void StartFlip()
    {
        if (!isFlipping) // Only start flip if not already flipping
        {
            StartCoroutine(FlipAndMoveCoroutine());
        }
    }

    void Update()
    {
        // Check if the bird is alive AND if enough time has passed for the next flip cycle
        if (logic != null && logic.birdIsAlive && Time.time >= nextFlipTime)
        {
            // Only start a new flip cycle if one isn't already in progress
            if (!isFlipping)
            {
                StartFlip();
                // Set the time for the next flip cycle
                nextFlipTime = Time.time + flipInterval;
            }
        }
        // If the bird is not alive, you might want to reset nextFlipTime
        // so that when it becomes alive again, the flip starts immediately.
        else if (logic != null && !logic.birdIsAlive)
        {
            nextFlipTime = Time.time; // Reset timer when bird is not alive
        }
    }
        
    private IEnumerator FlipAndMoveCoroutine()
    {
        isFlipping = true; // Set flag to true

        // Store initial LOCAL rotation and LOCAL position for the start of the full cycle
        Quaternion originalStartRotation = transform.localRotation;
        Vector3 originalStartLocalPosition = transform.localPosition;

        // --- PHASE 1: Flip to target rotation and position ---
        Quaternion midRotation = originalStartRotation * Quaternion.Euler(flipAxis * 180f);
        Vector3 midLocalPosition = new Vector3(targetLocalOffsetX, targetLocalOffsetY, originalStartLocalPosition.z);

        float elapsedTime = 0f;
        while (elapsedTime < flipDuration)
        {
            float t = elapsedTime / flipDuration; // Normalized time (0 to 1)

            transform.localRotation = Quaternion.Slerp(originalStartRotation, midRotation, t);
            transform.localPosition = Vector3.Lerp(originalStartLocalPosition, midLocalPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure phase 1 ends precisely
        transform.localRotation = midRotation;
        transform.localPosition = midLocalPosition;


        // --- PHASE 2: Flip back to original rotation and position ---
        Quaternion endRotation = originalStartRotation; // Back to original orientation
        Vector3 endLocalPosition = originalStartLocalPosition; // Back to original position

        elapsedTime = 0f; // Reset elapsed time for the second phase
        while (elapsedTime < flipDuration) // Use the same duration for the return
        {
            float t = elapsedTime / flipDuration; // Normalized time (0 to 1)

            // Lerp back from mid-point to original start
            transform.localRotation = Quaternion.Slerp(midRotation, endRotation, t);
            transform.localPosition = Vector3.Lerp(midLocalPosition, endLocalPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure phase 2 ends precisely
        transform.localRotation = endRotation;
        transform.localPosition = endLocalPosition;

        isFlipping = false; // Reset flag after the full cycle
    }
}
