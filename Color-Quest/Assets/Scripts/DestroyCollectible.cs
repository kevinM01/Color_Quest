using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        // Get the Collectible component attached to the collectible GameObject
        Collectible collectible = GetComponent<Collectible>();

        if (collectible != null)
        {
            // Call the Disable method on the Collectible component
            collectible.Disable();
        }
        else
        {
            // If Collectible component is not found, disable the GameObject directly
            gameObject.SetActive(false);
        }
    }
}
