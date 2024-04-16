using UnityEngine;
using TMPro;

public class BulletsLeftUI : MonoBehaviour
{
    public PlayerJump playerJump; // Reference to the PlayerJump script
    private TextMeshProUGUI textComponent;

    private void Start()
    {
        // Get the TextMeshProUGUI component attached to this object
        textComponent = GetComponent<TextMeshProUGUI>();

        // Make sure the PlayerJump script reference is assigned in the Unity Editor
        if (playerJump == null)
        {
            Debug.LogError("PlayerJump reference is not set in the BulletsLeftUI script!");
        }
    }

    private void Update()
    {
        // Update the text to display the number of bullets left
        if (playerJump != null)
        {
            int bulletsLeft = playerJump.maxBullets - playerJump.bulletsFired;
            textComponent.text = "Bullets: " + bulletsLeft.ToString();
        }
    }
}
