using UnityEngine;
using UnityEngine.UI;

public class PlayerColorChange : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private float timer = 0f;
    private Color previousColor; // Store the previous color before the timer
    public Image timerImage;
    public GameObject tokenBarGameObject;
    public Image previousColorBar; // Reference to the UI Image component representing the previous color

    public enum Type
    {
        RedCoin,
        BlueCoin,
        GreenCoin,
    }

    public Type type;

    void Start()
    {
        tokenBarGameObject.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
        previousColor = spriteRenderer.color; // Initialize with initial color
    }

    // void Update()
    // {
    //     if (timer > 0f)
    //     {
    //         timer -= Time.deltaTime; // Decrease timer based on game time
    //         // Debug.Log(timer);
    //         if (timer <= 0f) // Timer finished
    //         {
    //             ChangeToPreviousColor(); // Change back to the previous color
    //         }
    //     }
    // }

    void Update()
{
    if (timer > 0f)
    {
        timer -= Time.deltaTime;
        if (timerImage != null)
        {
            timerImage.fillAmount = timer / 5f; // Assuming a 5-second timer for simplicity
        }

        if (timer <= 0f)
        {
            ChangeToPreviousColor();
            if (tokenBarGameObject != null) 
            {
                tokenBarGameObject.SetActive(false); // Disable the GameObject when the timer is done
            }
        }
    }
}

    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
    }


    void ChangeToPreviousColor()
    {
        spriteRenderer.color = previousColor; // Set back to the stored previous color
        if (previousColorBar != null)
        {
            previousColorBar.color = spriteRenderer.color; // Update color bar to match current color
        }
        if (tokenBarGameObject != null)
        {
            tokenBarGameObject.SetActive(false); // Disable the GameObject when the timer is done
        }
    }

    void ChangeToRandomColor()
    {
        GameManager.Instance.AssignRandomColor(gameObject); // Use gameObject as the object to color
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))
        {
            ChangePlayerColor(other.GetComponent<SpriteRenderer>().color);
        }
        /*else if (other.CompareTag("Coin"))
        {
            *//*ChangePlayerColor(other.GetComponent<SpriteRenderer>().color);*//*
            UpdateScore(other.GetComponent<SpriteRenderer>())
        }*/
    }

    void ChangePlayerColor(Color newColor)
    {
        if (newColor == Color.red || newColor == Color.green || newColor == Color.blue)
        {
            tokenBarGameObject.SetActive(false);
            timerImage.fillAmount = 1;
            spriteRenderer.color = newColor;
            timer = 0f; // Start timer for 5 seconds
        }
        else
        {
            previousColor = spriteRenderer.color; // Store the current color before changing
            spriteRenderer.color = Color.white;
            timer = 5f; // Start timer for 5 seconds (optional, adjust based on your logic)
            if (tokenBarGameObject != null) 
            {
                tokenBarGameObject.SetActive(true); // Make sure it's visible when the timer starts
            }
            // Update previous color bar (assuming it's a filled image):
            if (previousColorBar != null)
            {
                previousColorBar.color = previousColor;
            }
        }
    }

    private void Collect(PlayerColorChange player)
    {
        Color playerColor = player.GetColor();
        Color coinColor = GetCoinColor();

        if (playerColor == coinColor)
        {
            ChangeColor(coinColor);
        }

        Destroy(gameObject);
    }

    Color GetCoinColor()
    {
        switch (type)
        {
            case Type.RedCoin:
                return Color.red;

            case Type.GreenCoin:
                return Color.green;

            case Type.BlueCoin:
                return Color.blue;

            default:
                return Color.white;
        }
    }

    void ChangeColor(Color newColor)
    {
        spriteRenderer.color = newColor;
    }

    public Color GetColor()
    {
        return spriteRenderer.color;
    }


    public string GetColorName()
{
    Color color = spriteRenderer.color;
    if (color == Color.red) return "Red";
    else if (color == Color.green) return "Green";
    else if (color == Color.blue) return "Blue";
    else if (color == Color.white) return "White";
    // Add more colors as needed
    else return "Unknown"; // Fallback for any color not explicitly checked
}

}
