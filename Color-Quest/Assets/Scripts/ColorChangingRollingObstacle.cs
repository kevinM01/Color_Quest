using UnityEngine;

public class ColorChangingRollingObstacle : MonoBehaviour
{
    [SerializeField] private float colorChangeInterval = 2.0f; // Time between color changes (seconds)
    private Color[] obstacleColors = new Color[] { Color.red, Color.green, Color.blue }; // Array of possible colors
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private PlayerJump playerScript; // Reference to the player script
    private float lastColorChangeTime;
    private Coroutine damageCoroutine;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        playerScript = FindObjectOfType<PlayerJump>(); // Assuming there's only one player in the scene
        lastColorChangeTime = Time.time; // Initialize last color change time
        ChangeColor(); // Set initial random color
    }

    private void Update()
    {
        if (Time.time - lastColorChangeTime >= colorChangeInterval)
        {
            ChangeColor();
            lastColorChangeTime = Time.time; // Update last color change time
        }
    }

    private void ChangeColor()
    {
        spriteRenderer.color = obstacleColors[Random.Range(0, obstacleColors.Length)];
    }

    // Handle collision with player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Make sure your player has the "Player" tag
        {
            var playerColorChange = collision.gameObject.GetComponent<PlayerColorChange>(); // Assuming your player has a PlayerColorChange component
            if (playerColorChange != null)
            {
                if (playerColorChange.GetColor() == spriteRenderer.color)
                {
                    // If the colors match, do not reduce health.
                    Debug.Log("Colors match, no health reduced.");
                }
                else
                {
                    if (damageCoroutine == null)
                    {
                        playerScript.StartReduceHealthCoroutine(); // Assuming you have a method to reduce health. Adjust the value as needed.
                    }
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Make sure your player has the "Player" tag
        {
//            var playerColorChange = collision.gameObject.GetComponent<PlayerColorChange>(); // Assuming your player has a PlayerColorChange component
//            if (playerColorChange != null)
//            {
//                if (playerColorChange.GetColor() == spriteRenderer.color)
//                {
//                    // If the colors match, do not reduce health.
//                    Debug.Log("Colors match, no health reduced.");
//                }
//                else
//                {
//                    if (damageCoroutine != null)
//                    {
                        playerScript.StopReduceHealthCoroutine();
                    }
                }
//            }
//        }
//    }
}
