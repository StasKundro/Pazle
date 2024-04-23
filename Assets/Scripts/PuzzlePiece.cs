using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzlePiece : MonoBehaviour
{
    // The position the puzzle piece should be in when the puzzle is solved
    public Vector2 targetPosition;

    // The offset from the puzzle piece's current position to its grab position
    private Vector2 grabPositionOffset;

    // Whether or not the puzzle piece is currently being grabbed by the player
    private bool isGrabbed = false;

    // Whether the puzzle piece is already in the correct position
    private bool isPlaced = false;

    public AudioClip audioClip;
    public AudioSource source;

    // Reference to the puzzle manager
    private PuzzleManager puzzleManager;

    public float targetAspectRatio = 16f / 9f;

    // Define the X and Y constraints for movement
    public float minX = -5.5f;
    public float maxX = 5.5f;
    public float minY = -3f;
    public float maxY = 3f;

    void Start()
    {
        // Find the PuzzleManager in the scene
        puzzleManager = FindObjectOfType<PuzzleManager>();

        Camera.main.aspect = targetAspectRatio;
    }

    void Update()
    {
        // If the puzzle piece is being grabbed and not already in place, follow the mouse cursor
        if (isGrabbed && !isPlaced)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Clamp the position within the defined constraints
            float clampedX = Mathf.Clamp(mousePosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(mousePosition.y, minY, maxY);

            transform.position = new Vector2(clampedX, clampedY) + grabPositionOffset;
        }
    }

    void OnMouseDown()
    {
        // Set isGrabbed to true and calculate the grab position offset
        if (!isPlaced)
        {
            source.clip = audioClip;
            source.Play();
            isGrabbed = true;
            grabPositionOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void OnMouseUp()
    {
        // Set isGrabbed to false and check if the puzzle piece is in the correct position
        if (isGrabbed && !isPlaced)
        {
            source.clip = audioClip;
            source.Play();
            isGrabbed = false;
            CheckPosition();
        }
    }

    void CheckPosition()
    {
        // Calculate the distance between the puzzle piece and its target position
        float distance = Vector2.Distance(transform.position, targetPosition);

        // If the distance is within a certain threshold, snap the puzzle piece to its target position
        if (distance < 0.5f)
        {
            transform.position = targetPosition;
            isPlaced = true;
            // Notify the puzzle manager that a piece is in the correct position
            puzzleManager.PiecePlaced();
        }
    }

    // Method to reset the puzzle piece state
    public void ResetPiece()
    {
        isPlaced = false;
    }
}
