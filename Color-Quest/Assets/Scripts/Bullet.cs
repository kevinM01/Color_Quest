using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;
    public Color bulletColor; // Color of the bullet
    // public int bulletsWasted = 0;

    public PlayerJump playerJump;
    private PointCounter pointCounter;

    private void Start()
    {
        playerJump = FindObjectOfType<PlayerJump>();
        pointCounter = FindObjectOfType<PointCounter>();
    }

    void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("movingObs"))
        {
            // Access the SpriteRenderer to check the color of the obstacle
            Color obsColor = collision.gameObject.GetComponent<SpriteRenderer>().color;
            // Check if the player color is white
            if (bulletColor == Color.white)
            {
                // If the player color is white, destroy the obstacle regardless of its color
                playerJump.bulletsHit++;
                // increase 2 points per moving obstacle destroyed
                pointCounter.points += 2;
                Debug.Log("Increase 2 points - white");
                pointCounter.UpdatePointsText();
                Destroy(collision.gameObject);
            }
            else
            {
                // Destroy the obstacle only if it has a different color than the bullet
                if (obsColor != bulletColor)
                {
                    playerJump.bulletsHit++;
                    pointCounter.points += 2;
                    Debug.Log("Increase 2 points - not white");
                    pointCounter.UpdatePointsText();
                    Destroy(collision.gameObject);
                }
                else
                {
                    playerJump.bulletsWasted++;
                }
            }
        }
        else{
            playerJump.bulletsWasted++;
        }

        // Destroy the bullet in any case
        Destroy(gameObject);
    }
}
