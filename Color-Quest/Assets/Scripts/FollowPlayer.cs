using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float zoomOutSize = 13.0f;  // Orthographic size when zoomed out
    private float originalSize;        // To store the original size
    public float zoomDistance = 20.0f;

    private Vector3 offset = new Vector3(5, 1, -48);
    private Vector3 originalOffset;
    private bool isZoomedOut = false;

    void Start()
    {
        originalOffset = offset;
        originalSize = GetComponent<Camera>().orthographicSize;  // Store the original orthographic size
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ToggleZoom();
        }
        transform.position = player.transform.position + offset;
    }

    void ToggleZoom()
    {
        Camera camera = GetComponent<Camera>();
        if (isZoomedOut)
        {
            // Zoom in (reset to original offset and size)
            offset = originalOffset;
            camera.orthographicSize = originalSize;
            isZoomedOut = false;
        }
        else
        {
            // Zoom out (increase z offset and increase size)
            offset = new Vector3(offset.x, offset.y, originalOffset.z - zoomDistance);
            camera.orthographicSize = zoomOutSize;
            isZoomedOut = true;
        }
    }
} 