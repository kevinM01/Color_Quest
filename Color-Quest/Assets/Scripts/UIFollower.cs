using UnityEngine;

public class UIFollower : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset = new Vector3(0,1,0); // Offset from the player's position

    

    void Update()
    {
        if (player != null)
        {
            // Update the position of the UI element to follow the player, with an offset if needed
            transform.position = player.position + offset;
        }
    }
}
