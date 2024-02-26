using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public int damageAmount = 10; // Amount of damage to apply

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the player
        if (collision.CompareTag("Player"))
        {
            // Get the health component from the player
            // (assuming the player has a health component)

            // Health playerHealth = collision.GetComponent<Health>();
            Debug.Log("Health Damaged: Health = Health -10");

            // If playerHealth is not null, reduce the health by damageAmount
            
            //if (playerHealth != null)
            //{
                // playerHealth.TakeDamage(damageAmount);
            //}
        }
    }
}
