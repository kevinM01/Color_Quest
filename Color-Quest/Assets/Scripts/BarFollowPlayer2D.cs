/*using UnityEngine;
using UnityEngine.UI;

public class TierBarFollowPlayer2D : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // Reference to the player's transform
    [SerializeField] private RectTransform tierBarRectTransform; // Reference to the RectTransform of the tier bar UI element
    [SerializeField] private Vector2 offset; // Offset to position the tier bar relative to the player in 2D space

    private void LateUpdate() // Use LateUpdate for smoother positioning after player movement
    {
        if (playerTransform != null && tierBarRectTransform != null)
        {
            // Calculate the desired position for the tier bar
            Vector3 desiredPosition = playerTransform.position + (Vector3)offset;

            // Update the tier bar's anchored position
            tierBarRectTransform.anchoredPosition = desiredPosition;
        }
        else
        {
            Debug.LogError("Missing references in TierBarFollowPlayer script: playerTransform or tierBarRectTransform");
        }
    }
}
*/