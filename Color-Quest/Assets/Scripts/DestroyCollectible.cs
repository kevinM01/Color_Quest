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
        // Destroy(gameObject);
        DisableObject();
    }

    private void DisableObject()
    {
        gameObject.SetActive(false); // Disable the GameObject instead of destroying it
    }
}
