using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text redCounterText;
    public Text greenCounterText;
    public Text blueCounterText;

    private int redCount;
    private int greenCount;
    private int blueCount;

    public GameObject objectToColor; // Reference to the game object you want to color

    private Color[] availableColors = { Color.red, Color.green, Color.blue };

    void Start()
    {
        // Call a method to assign a random color to the object
        AssignRandomColor(objectToColor);
    }

    void AssignRandomColor(GameObject obj)
    {
        if (obj != null)
        {
            // Generate a random index
            int randomIndex = Random.Range(0, availableColors.Length);

            // Assign the color corresponding to the random index to the object
            obj.GetComponent<SpriteRenderer>().color = availableColors[randomIndex];
        }
        else
        {
            Debug.LogWarning("No object to color assigned to the GameManager.");
        }
    }

    void Awake()
    {
        Instance = this;
    }

    public void DestroyGameInstance()
    {
        Application.Quit();

    }
}
