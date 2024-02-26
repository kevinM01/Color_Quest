using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool groundedPlayer;
    private float playerSpeed = 5.0f;
    private float jumpHeight = 3.0f;
    private int extraJumps = 1;
    private int jumpsLeft;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Initialize jumpsLeft with extraJumps + 1 to account for the initial ground jump.
        jumpsLeft = extraJumps + 1;
    }

    private void Update()
    {
        groundedPlayer = IsGrounded();

        if (groundedPlayer && Mathf.Approximately(rb.velocity.y, 0f))
        {
            // Reset jumps when grounded and not currently moving upwards
            jumpsLeft = extraJumps + 1; // Allows for the initial jump without consuming extra jumps
        }

        float moveInput = Input.GetAxis("Horizontal");
        // Adjusted for direct velocity setting; removed Time.deltaTime from horizontal movement calculation
        rb.velocity = new Vector2(moveInput * playerSpeed, rb.velocity.y);

        // Double Jump Logic
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) && jumpsLeft > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(2.0f * jumpHeight * Mathf.Abs(Physics2D.gravity.y)));
            jumpsLeft--;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        return hit.collider != null;
    }
}
