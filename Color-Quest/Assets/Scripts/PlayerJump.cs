using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool groundedPlayer;
    private float playerSpeed = 8.0f;
    private float jumpHeight = 3.0f;
    private int extraJumps = 1;
    private int jumpsLeft;
    public float health;
    public Image healthBar;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI checkpointText;
    public TextMeshProUGUI coinMinus;

    // Sprites for different states
    public Sprite idleSprite;
    public Sprite runningSprite;
    public Sprite jumpingSprite;
    private SpriteRenderer spriteRenderer;

    public int totalCoinsCollected = 0;
    public int healthRegains = 0;

    private Coroutine damageCoroutine;

    private SendToGoogle sendToGoogle;
    private SendHealthDamageToGoogle sendHealthDamageToGoogle;
    private SendCoinXHealthToGoogle sendCoinXHealthToGoogle;
    private SendPlayerPositionToGoogle sendPlayerPositionToGoogle;
    private SendBulletAnalytics sendBulletAnalytics;
    public int bulletsWasted;
    public int bulletsHit;
    private PointCounter pointCounter;

    private PlayerColorChange playerColorChange;
    private Coroutine blinkCoroutine;

    private Vector2 respawnPoint = Vector2.negativeInfinity;
    private bool isRespawning = true;

    private float analyticsTimer = 0f;

    public bool grounded { get; private set; }
    public bool running => Mathf.Abs(rb.velocity.x) > 0.25f;
    public bool jumping{get; private set;}
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public int bulletsFired = 0;
    public int maxBullets = 5;


    private bool hasReachedCheckpoint = false;
    private List<GameObject> collectedCollectibles = new List<GameObject>();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();  // Ensure there's a SpriteRenderer component attached
        jumpsLeft = extraJumps + 1;
        health = 100f;
        UpdateHealthBar();

        sendToGoogle = FindObjectOfType<SendToGoogle>();
        sendHealthDamageToGoogle = FindObjectOfType<SendHealthDamageToGoogle>();
        sendCoinXHealthToGoogle = FindObjectOfType<SendCoinXHealthToGoogle>();
        pointCounter = FindObjectOfType<PointCounter>();
        sendPlayerPositionToGoogle = FindObjectOfType<SendPlayerPositionToGoogle>();
        sendBulletAnalytics = FindObjectOfType<SendBulletAnalytics>();
        playerColorChange = FindObjectOfType<PlayerColorChange>();
        bulletsWasted = 0;
        bulletsHit = 0;
    }

    private void Update()
    {
        if (isRespawning)
        {

        
            groundedPlayer = IsGrounded();

        analyticsTimer += Time.deltaTime;
        if (analyticsTimer >= 2f)
        {
            SendPlayerPositionAnalytics();
            analyticsTimer = 0f;
        }

    // SendPlayerPositionAnalytics();
    if (groundedPlayer && Mathf.Abs(rb.velocity.y) < 0.01f)
    {
        // Reset jumps when grounded and not currently moving upwards
        jumpsLeft = extraJumps + 1; // Allows player for the initial jump without consuming extra jumps
    }

    float moveInput = Input.GetAxis("Horizontal");
    // Adjusted for direct velocity setting; removed Time.deltaTime from horizontal movement calculation
    rb.velocity = new Vector2(moveInput * playerSpeed, rb.velocity.y);
if (rb.velocity.x > 0f) {
            transform.eulerAngles = Vector3.zero;
        } else if (rb.velocity.x < 0f) {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    // Double Jump Logic
    if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && jumpsLeft > 0)
    {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(2.0f * jumpHeight * Mathf.Abs(Physics2D.gravity.y)));
        jumpsLeft--;
    }
    

    /*UpdateHealthText();*/
    UpdateHealthBar();

    if (health <= 0)
    {
        health=0;
        /*UpdateHealthText();*/
        UpdateHealthBar();

        SendCoinXHealthAnalytics();
        CollectAnalytics();
        // gameOverText.enabled = true;

        // StartCoroutine(ShowGameOverTextForThreeSeconds());

        // // Stop the game and display game over text
        // Time.timeScale = 0f; // Stop time to freeze the game
        return; // Exit the update loop
    }

            // // Stop the game and display game over text
            // Time.timeScale = 0f; // Stop time to freeze the game
             // Exit the update loop
            



   if(Input.GetKeyDown(KeyCode.F) && bulletsFired < maxBullets) 
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<SpriteRenderer>().color = GetComponent<PlayerColorChange>().GetColor();
            bullet.GetComponent<Bullet>().bulletColor = bullet.GetComponent<SpriteRenderer>().color;
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.right * bulletSpeed;

            bulletsFired++; // Increment bullets fired counter
        }
    return;
    }
    }


    private void SendPlayerPositionAnalytics()
    {
    Scene currentScene = SceneManager.GetActiveScene();
    // Debug.Log("Send Analytics at: " + Time.deltaTime);
    float x_coord = transform.position.x;
    float y_coord = transform.position.y;

    // Assuming you have correctly assigned the playerColorChange reference
    // either via the inspector or dynamically in Start()
    string color = playerColorChange.GetColorName();

    // Debug.Log("Current X Position of the Player: " + x_coord);
    if (sendPlayerPositionToGoogle != null)
    {
        sendPlayerPositionToGoogle.Send(x_coord, y_coord, color, currentScene.name);
    }
}


    // private IEnumerator ShowGameOverTextForThreeSeconds()
    // {
    //     yield return new WaitForSeconds(3f); // Wait for 3 seconds
    //     gameOverText.enabled = false; // Disable gameOverText after 3 seconds
    // }

    private bool IsGrounded()
    {
        // // Define the layer mask to exclude the "Collectibles" layer
        // // This assumes that "Collectibles" is the name of the layer you want to exclude
        // int layerMask = ~(1 << LayerMask.NameToLayer("Collectibles"));
        
        // // Use the layer mask in the raycast
        // RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, layerMask);
        // return hit.collider != null;

        // Adjust the start position of the raycast to be just below the player's feet (assuming a downward raycast)
        Vector2 rayStart = new Vector2(transform.position.x, transform.position.y - GetComponent<Collider2D>().bounds.extents.y - 0.4f);
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, 0.4f);


        // RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f);
        if (hit.collider != null) {
            // Debug.Log("Hit: " + hit.collider.tag);
        }
        return hit.collider != null && (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("Spike") || hit.collider.CompareTag("movingObs"));
    }

/* New Function*/
//     private bool IsGrounded()
// {
//     // Adjust the start position of the raycast to be just below the player's feet
//     // Adjust the start position of the raycast to be more clearly below the player's feet
// Vector2 rayStart = new Vector2(transform.position.x, transform.position.y - GetComponent<Collider2D>().bounds.extents.y - 0.6f);
    
//     // Define the layer mask to exclude the "Player" layer
// int layerMask = ~(1 << LayerMask.NameToLayer("Player"));

// // Adjust the raycast to use this layer mask
// RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, 0.4f, layerMask);

//     Debug.DrawRay(rayStart, Vector2.down * 1f, Color.red);

//     // Check if the hit is valid and not the player
//     // if (hit.collider != null) {
//     //     Debug.Log("Hit: " + hit.collider.tag);
//     //     if (hit.collider.tag != "Player") {
//     //         return (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("Spike") || hit.collider.CompareTag("movingObs"));
//     //     }
//     // }

//     Debug.DrawRay(rayStart, Vector2.down * 0.4f, Color.red, 1f, false);
// if (hit.collider != null) {
//     Debug.Log("Hit: " + hit.collider.tag + ", GameObject: " + hit.collider.gameObject.name);
// } else {
//     Debug.Log("Raycast did not hit anything.");
// }

//     return false;
// }



    void OnCollisionEnter2D(Collision2D  other)
    {
        /*Debug.Log("Inside Spike Collider");*/
        if (other.gameObject.CompareTag("Spike"))
        {
        /*Debug.Log("Collided with Spike");*/
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

    public void StartReduceHealthCoroutine()
    {
        if (damageCoroutine == null) // Check if the coroutine is already running
        {
            damageCoroutine = StartCoroutine(ReduceHealthOverTime());
        }
    }

    public void StopReduceHealthCoroutine()
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }


    public IEnumerator ReduceHealthOverTime()
    {
        while (true)
        {
            health -= 20f;
            UpdateHealthText();
            UpdateHealthBar();
            IncreaseHealthByAmount(20);
            /*sendHealthDamageToGoogle();*/
            Scene currentScene = SceneManager.GetActiveScene();
            // Debug.Log("Current Scene Name: " + currentScene.name);
            float x_coord = transform.position.x;
            float y_coord = transform.position.y;

            if (sendHealthDamageToGoogle != null)
            {
                sendHealthDamageToGoogle.Send(x_coord, y_coord, currentScene.name);
            }

            if (health <= 0)
            {
                SendCoinXHealthAnalytics();
                CollectAnalytics();
                if (IsValidRespawnPoint() && pointCounter.points > 0)
                {
                    RespawnPlayer();
                    Debug.Log("Player respawned at checkpoint");
                }
                else
                {
                    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                    SceneManager.LoadScene(currentSceneIndex);
                    Debug.Log("Player respawned at the beginning of the level");
                }

                health = 100;
                break;
            }
            yield return new WaitForSeconds(1.5f);
        }
    }

    public void SendCoinXHealthAnalytics()
    {
        totalCoinsCollected = pointCounter.points + (healthRegains * 5);
        if (sendCoinXHealthToGoogle != null)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
            sendCoinXHealthToGoogle.Send(totalCoinsCollected, healthRegains, sceneName);
        }
    }

    public void UpdateHealthText()
    {
        healthText.text = "Health: " + health.ToString() + "%";

        if (health <= 20)
        {
            if (blinkCoroutine == null)
            {
                // Start the coroutine only if it's not already running
                blinkCoroutine = StartCoroutine(BlinkHealthText());
            }
        }
        else
        {
            // If health is greater than or equal to 10, ensure the text is visible and stop the coroutine
            healthText.enabled = true;
            if (blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine);
                blinkCoroutine = null;
            }
        }
    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = health / 100f; // Scale health value to fill amount

        if (health <= 20)
        {
            if (blinkCoroutine == null)
            {
                // Start the coroutine only if it's not already running
                blinkCoroutine = StartCoroutine(BlinkHealthText());
            }
        }
    }

    public void IncreaseHealthByAmount(float amount)
    {
        if (pointCounter != null)
        {
            if (pointCounter.points >= 5) // Check if the player has at least 5 points
            {
                // Check if the player's health is less than 100
                if (health < 100)
                {
                    // Calculate the new health after adding 20
                    float newHealth = health + 20;
                    // If the new health exceeds 100, set it to 100
                    health = Mathf.Min(newHealth, 100);
                    /*UpdateHealthText();*/
                    UpdateHealthBar();

                    healthRegains += 1;

                    // Decrease points by 5 only if health increased
                    pointCounter.points -= 5;
                    pointCounter.points = Mathf.Max(0, pointCounter.points); // Ensure points cannot go below 0
                    pointCounter.UpdatePointsText(); // Call the UpdatePointsText method
                }
            }
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Fall")) // Check if the player has fallen
        {
            Debug.Log("Colided with Fall"+ respawnPoint);
            SendCoinXHealthAnalytics();
            // Debug.Log("Reocrded");
            CollectAnalytics();
            // Respawn the player at the checkpoint if available, else reload the scene
            if (IsValidRespawnPoint() && pointCounter.points > 0)
            {
                RespawnPlayer();
                Debug.Log("Player respawned at checkpoint");
            }
            else
            {
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneIndex);
                Debug.Log("Player respawned at the beginning of the level");
            }
        }
        if (col.CompareTag("Checkpoint")) // Check if the player has crossed a checkpoint
        {
            hasReachedCheckpoint = true;            
            // Update the respawn point to the position of the checkpoint
            respawnPoint = col.transform.position;
            Debug.Log("Checkpoint reached");
            StartCoroutine(ShowCheckpointMessage());
        }
        if (col.CompareTag("Collectible"))
        {
            Collectible collectible = col.GetComponent<Collectible>();
            Debug.Log("Hello in Collectible if... on TriggerrrEnterrr");
            /*if (collectible != null && !collectible.IsCollected())*/
            /*if (collectible != null)
            {*/
                // Collect the collectible
                collectedCollectibles.Add(col.gameObject);
                Debug.Log("Hello buddyyy");
                collectible.Disable(); // Disable the collectible GameObject
                Debug.Log("Collectible collected.");
            //}
        }
    }

    IEnumerator ShowCheckpointMessage()
    {
        checkpointText.enabled = true;
        checkpointText.text = "Checkpoint Reached";

        yield return new WaitForSeconds(2f);

        checkpointText.enabled = false; 
    }

    IEnumerator ShowCoinMinusMsg()
    {
        yield return new WaitForSeconds(2f);

        coinMinus.enabled = true;
        coinMinus.text = "[Coins -1]";

        yield return new WaitForSeconds(2f);

        coinMinus.enabled = false;
    }

    bool IsValidRespawnPoint()
    {
        // Check if the respawnPoint is not set to negative infinity
        return respawnPoint.x != Mathf.NegativeInfinity && respawnPoint.y != Mathf.NegativeInfinity;
    }


    void RespawnPlayer()
    {
        pointCounter.points--;
        pointCounter.UpdatePointsText();
        // Set player position to the stored checkpoint position
        isRespawning = false;
        StartCoroutine(RespawnWithDelay(0.5f)); // Call the coroutine with a 1-second delay

        if (hasReachedCheckpoint && collectedCollectibles.Count > 0)
        {
            Debug.Log("Hello in respawn playerrr");
            // Enable all collected collectibles upon respawn
            foreach (GameObject collectible in collectedCollectibles)
            {
                collectible.SetActive(true);
            }
        }
    }

    IEnumerator RespawnWithDelay(float delay)
    {
        rb.velocity = Vector2.zero; // Set velocity to zero to stop movement
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        isRespawning = true;
        StartCoroutine(ShowCoinMinusMsg());

        transform.position = respawnPoint;
        pointCounter.BlinkPointsText(3.0f);
    }

    // private IEnumerator ReloadSceneAfterDelay(float delay)
    // {
    //     Time.timeScale = 0f;
    //     yield return new WaitForSeconds(delay);
    //     // Reload the scene
    //     int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    //     SceneManager.LoadScene(currentSceneIndex);

    //     // Collect analytics
    //     CollectAnalytics();
    // }


    public void CollectAnalytics()
    {
        // x coord of the player at the time of his dehant (rip player, awks!)
        // collect current level at time of death
        //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(currentSceneIndex);
        Scene currentScene = SceneManager.GetActiveScene();
        // Debug.Log("Current Scene Name: " + currentScene.name);
        float x_coord = transform.position.x;
        float y_coord = transform.position.y;
        // Debug.Log("Current X Position of the Player: " + x_coord);
        if (sendToGoogle != null)
        {
            sendToGoogle.Send(x_coord, y_coord, currentScene.name);
        }

        if (sendBulletAnalytics != null)
        {
            Debug.Log("What is UPppppp");
            sendBulletAnalytics.Send(currentScene.name, (this.maxBullets - this.bulletsHit));
        }
    }

    private IEnumerator BlinkHealthText()
    {
        while (true)
        {
            healthText.enabled = !healthText.enabled;
            yield return new WaitForSeconds(0.5f); // Toggle every 0.5 seconds
        }
    }
}