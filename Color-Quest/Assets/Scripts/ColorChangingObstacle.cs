using UnityEngine;
using System.Collections.Generic;

public class ColorChangingObstacle : MonoBehaviour
{
    [SerializeField] private float colorChangeInterval = 2.0f; // Time between color changes (seconds)
    private List<Color> obstacleColors = new List<Color>() { Color.red, Color.green, Color.blue }; // List of possible colors
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private PlayerColorChange playerColorChange;
    private float lastColorChangeTime;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerColorChange = FindObjectOfType<PlayerColorChange>(); // Assuming there's only one PlayerColorChange script in the scene
        lastColorChangeTime = Time.time; // Initialize last color change time
        ChangeColor(); // Set initial random color
    }

    private void Update()
    {
        if (Time.time - lastColorChangeTime >= colorChangeInterval)
        {
            ChangeColor();
            lastColorChangeTime = Time.time; // Update last color change time
        }

        UpdateColliderBasedOnColorMatch();
    }

    private void ChangeColor()
    {
        spriteRenderer.color = obstacleColors[Random.Range(0, obstacleColors.Count)];
    }

    private void UpdateColliderBasedOnColorMatch()
    {
        if (playerColorChange != null && playerColorChange.GetColor() == spriteRenderer.color)
        {
            // Matched colors, set collider size to the minimum
            boxCollider.size = new Vector2(0.0001f, 0.0001f);
        }
        else
        {
            // Colors don't match, set collider size to default
            boxCollider.size = new Vector2(1f, 1f); // Adjust this size according to your needs
        }
    }
}
