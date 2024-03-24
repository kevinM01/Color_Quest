using UnityEngine;
using System.Collections.Generic;

public class ColorChangeWall : MonoBehaviour
{
    [SerializeField] private float colorChangeInterval = 2.0f; // Time between color changes (seconds)
    private List<Color> obstacleColors = new List<Color>() { Color.red, Color.green, Color.blue }; // List of possible colors
    private SpriteRenderer obstacleSpriteRenderer;
    private float lastColorChangeTime;

    private void Start()
    {
        obstacleSpriteRenderer = GetComponent<SpriteRenderer>();
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
    }

    private void ChangeColor()
    {
        obstacleSpriteRenderer.color = obstacleColors[Random.Range(0, obstacleColors.Count)];
    }
}
