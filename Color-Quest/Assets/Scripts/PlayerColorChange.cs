using UnityEngine;

public class PlayerColorChange : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float timer = 0f;

    public enum Type
    {
        RedCoin,
        BlueCoin,
        GreenCoin,
    }

    public Type type;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime; // Decrease timer based on game time
            Debug.Log(timer);
            if (timer <= 0f) // Timer finished
            {
                ChangeToRandomColor();  // change the color to random of Red, Green, Blue
            }
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
            spriteRenderer.color = newColor;
            timer = 0f;
        }
        else
        {
            spriteRenderer.color = Color.white;
            timer = 5f; // Start timer for 5 seconds

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
}
