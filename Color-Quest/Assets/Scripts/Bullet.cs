using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;
    public Color bulletColor; // Color of the bullet

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
                Destroy(collision.gameObject);
            }
            else
            {
                // Destroy the obstacle only if it has a different color than the bullet
                if (obsColor == bulletColor)
                {
                    Destroy(collision.gameObject);
                }
            }
        }

        // Destroy the bullet in any case
        Destroy(gameObject);
    }
}
