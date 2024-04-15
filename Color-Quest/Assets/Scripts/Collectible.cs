using UnityEngine;

public class Collectible : MonoBehaviour
{
    public Color color;
    private bool isCollected = false;
    
    /* private float collectedTimestamp = -1f;

    public void Collect()
    {
        isCollected = true;
        collectedTimestamp = Time.time;
        gameObject.SetActive(false); // Optionally deactivate the collectible object
    }

    public void ResetCollectible()
    {
        isCollected = false;
        collectedTimestamp = -1f;
        gameObject.SetActive(true); // Optionally activate the collectible object
    }

    public bool IsCollected()
    {
        return isCollected;
    }

    public bool IsCollectedAfterCheckpoint(float checkpointTimestamp)
    {
        return isCollected && collectedTimestamp > checkpointTimestamp;
    }*/

    /*public void Collect()
    {
        gameObject.SetActive(false); // Disable the collectible object
    }*/

    public void Disable()
    {
        // isCollected = true;
        gameObject.SetActive(false); // Disable the GameObject
    }

    public bool IsCollected()
    {
        return !gameObject.activeSelf; // Check if the collectible is disabled
    }
}
