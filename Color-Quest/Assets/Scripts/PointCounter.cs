using UnityEngine;
using TMPro;
using System.Collections;

public class PointCounter : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public int points;
    private PlayerJump playerJump;
    public TextMeshProUGUI firstCoinText;
    private bool flag;

    void Start()
    {
        playerJump = FindObjectOfType<PlayerJump>();
        points = 0;
        UpdatePointsText();
        firstCoinText.enabled = false;
        flag = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            // Increment points when the player collects coins
            points++;
            UpdatePointsText();

            /*// Call SendNewAnalyticsData from PlayerJump script
            if (playerJump != null)
            {
                playerJump.();
            }*/

            if (points == 1 && !flag)
            {
                StartCoroutine(ShowFirstCoinText());
                flag = true;
            }

            // Check if the player has collected 5 points
            if (points >= 5)
            {
                // Invoke a method to increase the player's health by 10
                playerJump.IncreaseHealthByAmount(20);
            }
        }

//
//        if (other.CompareTag("firstCoin"))
//                {
//                    // Increment points when the player collects coins
//                    points++;
//                    UpdatePointsText();
//
//
//
//                    // Check if the player has collected 5 points
//                    if (points >= 5)
//                    {
//                        // Invoke a method to increase the player's health by 10
//                        playerJump.IncreaseHealthByAmount(10);
//                    }
//                }
    }

    public void UpdatePointsText()
    {
        // Update the points text UI element
        pointsText.text = "Coins: " + points.ToString();
    }

    IEnumerator ShowFirstCoinText()
        {
            firstCoinText.enabled = true;
            yield return new WaitForSeconds(4);
            firstCoinText.enabled = false;
        }
}
