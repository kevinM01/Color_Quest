using UnityEngine;

public class CheckpointBehavior : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        // Get the SpriteRenderer component attached to this checkpoint
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ActivateCheckpoint()
    {
        // Change color to yellow
        spriteRenderer.color = Color.yellow;
    }
}
