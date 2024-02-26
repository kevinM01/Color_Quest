using UnityEngine;

public class PlayerColorChange : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

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
        spriteRenderer.color = newColor;
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
