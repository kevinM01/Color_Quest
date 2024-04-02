using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class DisappearingText : MonoBehaviour
{
    public List<GameObject> tutorialUIElements; // Drag your UI elements into this list in the inspector
    public GameObject player; // Assign your player GameObject in the inspector

    private Vector3 lastPosition;
    private bool isHidden = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize last position with the player's starting position
        if (player != null)
        {
            lastPosition = player.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player has moved
        if (player != null && !isHidden && Vector3.Distance(player.transform.position, lastPosition) > 0.1f)
        {
            // Hide all tutorial UI elements
            foreach (var uiElement in tutorialUIElements)
            {
                if (uiElement != null)
                {
                    uiElement.SetActive(false);
                }
            }
            isHidden = true; // Set the flag so we don't hide them again
        }

        // Update lastPosition to the player's current position for the next frame
        lastPosition = player.transform.position;
    }
}
