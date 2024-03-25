using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f; // Movement speed of the obstacle
    /*[SerializeField] private float minX = -5.0f; // Minimum boundary for horizontal movement
    [SerializeField] private float maxX = 5.0f; // Maximum boundary for horizontal movement*/
    public float currentX;
    public float startPos;
    private bool movingRight = true; // Flag to track movement direction

    private void Start()
    {
        currentX = transform.position.x; // Store initial position
        startPos = currentX;
    }

    private void Update()
    {
        // Move the obstacle horizontally within boundaries
        if (movingRight)
        {
            currentX += speed * Time.deltaTime;
            if (currentX >= startPos + 5.0f)
            {
                movingRight = false;
            }
        }
        else
        {
            currentX -= speed * Time.deltaTime;
            if (currentX <= startPos - 5.0f)
            {
                movingRight = true;
            }
        }

        transform.position = new Vector3(currentX, transform.position.y, transform.position.z);

        // Handle player collision (assuming Player has a collider with "Player" tag)
        /*if (IsPlayerColliding())
        {
            // Call a function (we'll define this later) to handle player health reduction
            ReducePlayerHealth();
        }*/
    }

    /*private bool IsPlayerColliding()
    {
        // Use Physics2D.OverlapCircleAll to check for colliders within a small radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        foreach (Collider2D collider in colliders)
        {
            // Check if the collider belongs to the "Player" GameObject with the "Player" tag
            if (collider.gameObject.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }


    private void ReducePlayerHealth()
    {
        *//*Debug.Log("Health kaam karoo pilijj...");*//*
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerJump>().ReduceHealthOverTime(); // Adjust damage value as needed
    }*/
}
