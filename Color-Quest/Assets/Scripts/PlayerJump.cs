using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool groundedPlayer;
    private float playerSpeed = 5.0f;
    private float jumpHeight = 3.0f;
    private int extraJumps = 1;
    private int jumpsLeft;
    public float health;
    public TextMeshProUGUI healthText;
    private Coroutine damageCoroutine;

    private SendToGoogle sendToGoogle;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Initialize jumpsLeft with extraJumps + 1 to account for the initial ground jump.
        jumpsLeft = extraJumps + 1;
        health = 100f;
        UpdateHealthText();

        sendToGoogle = FindObjectOfType<SendToGoogle>();
    }

    private void Update()
    {
        groundedPlayer = IsGrounded();

        if (groundedPlayer && Mathf.Approximately(rb.velocity.y, 0f))
        {
            // Reset jumps when grounded and not currently moving upwards
            jumpsLeft = extraJumps + 1; // Allows player for the initial jump without consuming extra jumps
        }

        float moveInput = Input.GetAxis("Horizontal");
        // Adjusted for direct velocity setting; removed Time.deltaTime from horizontal movement calculation
        rb.velocity = new Vector2(moveInput * playerSpeed, rb.velocity.y);

        // Double Jump Logic
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpsLeft > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(2.0f * jumpHeight * Mathf.Abs(Physics2D.gravity.y)));
            jumpsLeft--;
        }

        if (health <= 0)
        {
            health=0;
            UpdateHealthText();

            CollectAnalytics();

            // Stop the game and display game over text
            Time.timeScale = 0f; // Stop time to freeze the game
            return; // Exit the update loop
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        return hit.collider != null;
    }

    void OnCollisionEnter2D(Collision2D  other)
    {
        Debug.Log("Inside Spike Collder");
        if (other.gameObject.CompareTag("Spike"))
        {
        Debug.Log("Collided with Spike");
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(ReduceHealthOverTime());
            }
        }
    }

    void OnCollisionExit2D(Collision2D  other)
    {
        if (other.gameObject.CompareTag("Spike"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    IEnumerator ReduceHealthOverTime()
    {
        while (true)
        {
            health -= 10f;
            UpdateHealthText();
            if (health <= 0)
            {
                // Handle player death here if needed
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                health = 100;
                SceneManager.LoadScene(currentSceneIndex);

                CollectAnalytics();

                break;
            }
            yield return new WaitForSeconds(3f);
        }
    }

    public void UpdateHealthText()
    {
        healthText.text = "Health: " + health.ToString() + "%";
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Fall")
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);

            CollectAnalytics();
        }
    }

    private void CollectAnalytics()
    {
        // x coord of the player at the time of his dehant (rip player, awks!)
        // collect current level at time of death
        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log("Current Scene Name: " + currentScene.name.GetType());
        float x_coord = transform.position.x;
        Debug.Log("Current X Position of the Player: " + x_coord);
        if (sendToGoogle != null)
        {
            sendToGoogle.Send(x_coord, currentScene.name);
        }
    }


}
