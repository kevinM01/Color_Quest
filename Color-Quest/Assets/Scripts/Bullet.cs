using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("movingObs"))
        {
            // Destroy the collided game object
            Destroy(collision.gameObject);
        }

        // Destroy the bullet
        Destroy(gameObject);
    }
}
