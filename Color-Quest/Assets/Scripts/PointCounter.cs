using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI;
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
        Debug.Log(points);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(points);
        if (other.CompareTag("Coin"))
        {
            // Increment points when the player collects coins
            points++;
            Debug.Log(points);
            UpdatePointsText();
        }
    }

//    public void IncreasePoints(int amount)
//    {
//        points+=amount;
//        UpdatePointsText();
//    }

    void UpdatePointsText()
    {
        // Update the points text UI element
        pointsText.text = "Points: " + points.ToString();
    }
}