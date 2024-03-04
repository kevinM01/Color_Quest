using UnityEngine;
using TMPro;

public class PointCounter : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public int points;
    private PlayerJump playerJump;

    void Start()
    {
        playerJump = FindObjectOfType<PlayerJump>();
        points = 0;
        UpdatePointsText();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            // Increment points when the player collects coins
            points++;
            UpdatePointsText();

            // Check if the player has collected 5 points
            if (points >= 5)
            {
                // Invoke a method to increase the player's health by 10
                playerJump.IncreaseHealthByAmount(10);
            }
        }
    }

    public void UpdatePointsText()
    {
        // Update the points text UI element
        pointsText.text = "Points: " + points.ToString();
    }
}
