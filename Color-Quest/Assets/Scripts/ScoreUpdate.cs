using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUpdate : MonoBehaviour
{
    public int points;
    /*public TextMeshProUGUI pointsText;*/

    void Start()
    {
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            points++;
            Debug.Log(points);
            UpdatePointsText();
        }
    }

    void UpdatePointsText()
    {
        // Update the points text UI element
        /*pointsText.text = "Points: " + points.ToString();*/
    }
}
